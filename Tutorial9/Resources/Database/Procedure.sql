CREATE OR ALTER PROCEDURE AddProductToWarehouse @IdProduct INT, @IdWarehouse INT, @Amount INT, @CreatedAt DATETIME
AS
BEGIN

    DECLARE @IdProductFromDb INT, @IdOrder INT, @Price DECIMAL(25, 2);

    SELECT TOP 1 @IdOrder = o.IdOrder
    FROM [Order] o
             LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder
    WHERE o.IdProduct = @IdProduct
      AND o.Amount = @Amount
      AND pw.IdProductWarehouse IS NULL
      AND o.CreatedAt < @CreatedAt;

    SELECT @IdProductFromDb = Product.IdProduct, @Price = Product.Price FROM Product WHERE IdProduct = @IdProduct

    IF @IdProductFromDb IS NULL
        BEGIN
            RAISERROR ('Invalid parameter: Provided IdProduct does not exist', 18, 0);
        END;

    IF @IdOrder IS NULL
        BEGIN
            RAISERROR ('Invalid parameter: There is no order to fullfill', 18, 0);
        END;

    IF NOT EXISTS(SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse)
        BEGIN
            RAISERROR ('Invalid parameter: Provided IdWarehouse does not exist', 18, 0);
        END;

    SET XACT_ABORT ON;
    BEGIN TRAN;
    BEGIN TRY

        UPDATE [Order]
        SET FulfilledAt = GETDATE()
        WHERE IdOrder = @IdOrder;

        INSERT INTO Product_Warehouse
        (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
        VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Amount * @Price, @CreatedAt);

        DECLARE @NewId INT = CAST(SCOPE_IDENTITY() AS INT);

        COMMIT TRAN;

        SELECT @NewId AS NewId;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END