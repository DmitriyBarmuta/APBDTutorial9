IF NOT EXISTS (SELECT *
               FROM INFORMATION_SCHEMA.TABLES
               WHERE TABLE_NAME = 'Order')
    BEGIN
        CREATE TABLE "Order"
        (
            IdOrder     INT      NOT NULL IDENTITY,
            IdProduct   INT      NOT NULL,
            Amount      INT      NOT NULL,
            CreatedAt   DATETIME NOT NULL,
            FulfilledAt DATETIME NULL,
            CONSTRAINT Order_pk PRIMARY KEY (IdOrder)
        );
    END

IF
    NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Product')
    BEGIN
        CREATE TABLE Product
        (
            IdProduct   INT            NOT NULL IDENTITY,
            Name        NVARCHAR(200)  NOT NULL,
            Description NVARCHAR(200)  NOT NULL,
            Price       NUMERIC(25, 2) NOT NULL,
            CONSTRAINT Product_pk PRIMARY KEY (IdProduct)
        );
    END

IF
    NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Warehouse')
    BEGIN
        CREATE TABLE Warehouse
        (
            IdWarehouse INT           NOT NULL IDENTITY,
            Name        NVARCHAR(200) NOT NULL,
            Address     NVARCHAR(200) NOT NULL,
            CONSTRAINT Warehouse_pk PRIMARY KEY (IdWarehouse)
        );
    END

IF
    NOT EXISTS (SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Product_Warehouse')
    BEGIN
        CREATE TABLE Product_Warehouse
        (
            IdProductWarehouse INT            NOT NULL IDENTITY,
            IdWarehouse        INT            NOT NULL,
            IdProduct          INT            NOT NULL,
            IdOrder            INT            NOT NULL,
            Amount             INT            NOT NULL,
            Price              NUMERIC(25, 2) NOT NULL,
            CreatedAt          DATETIME       NOT NULL,
            CONSTRAINT Product_Warehouse_pk PRIMARY KEY (IdProductWarehouse)
        );
    END

-- Add foreign keys only if they do not exist
IF
    NOT EXISTS (SELECT *
                FROM sys.foreign_keys
                WHERE name = 'Product_Warehouse_Order')
    BEGIN
        ALTER TABLE Product_Warehouse
            ADD CONSTRAINT Product_Warehouse_Order
                FOREIGN KEY (IdOrder)
                    REFERENCES "Order" (IdOrder);
    END

IF
    NOT EXISTS (SELECT *
                FROM sys.foreign_keys
                WHERE name = 'Receipt_Product')
    BEGIN
        ALTER TABLE "Order"
            ADD CONSTRAINT Receipt_Product
                FOREIGN KEY (IdProduct)
                    REFERENCES Product (IdProduct);
    END

IF
    NOT EXISTS (SELECT *
                FROM sys.foreign_keys
                WHERE name = '_Product')
    BEGIN
        ALTER TABLE Product_Warehouse
            ADD CONSTRAINT _Product
                FOREIGN KEY (IdProduct)
                    REFERENCES Product (IdProduct);
    END

IF
    NOT EXISTS (SELECT *
                FROM sys.foreign_keys
                WHERE name = '_Warehouse')
    BEGIN
        ALTER TABLE Product_Warehouse
            ADD CONSTRAINT _Warehouse
                FOREIGN KEY (IdWarehouse)
                    REFERENCES Warehouse (IdWarehouse);
    END