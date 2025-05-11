INSERT INTO Warehouse(Name, Address)
VALUES ('Warsaw', 'Kwiatowa 12');

INSERT INTO Product(Name, Description, Price)
VALUES ('Abacavir', '', 25.5),
       ('Acyclovir', '', 45.0),
       ('Allopurinol', '', 30.8);

INSERT INTO "Order"(IdProduct, Amount, CreatedAt)
VALUES ((SELECT IdProduct FROM Product WHERE Name = 'Abacavir'), 125, GETDATE());