GO

INSERT INTO Warehouse(Name, Address)
VALUES ('Warsaw', 'Kwiatowa 12');

GO

INSERT INTO Product(Name, Description, Price)
VALUES ('Abacavir', '', 25.5),
       ('Acyclovir', '', 45.0),
       ('Allopurinol', '', 30.8);

GO

INSERT INTO "Order"(IdProduct, Amount, CreatedAt)
VALUES ((SELECT IdProduct FROM Product WHERE Name = 'Abacavir'), 125, GETDATE());