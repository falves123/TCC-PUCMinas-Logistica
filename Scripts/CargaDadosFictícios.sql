-- limpando as  as tabelas.

delete Wharehouse;
delete ShippingHistory;
delete Product;
delete Shipping;
delete Address;
delete Contact;
delete Customer;
delete Provider;
delete Partner;
delete Step;
delete Partner_Type;

-- realizando a carga inicial nas tabelas.

INSERT INTO Partner_Type ( Description) VALUES 
	('Wharehouse'),
	('Transportadora'),
	('Customer');

INSERT INTO Step (Name, Description, Sequence, IsDelivered) VALUES 
	('EntradaMercadoria', 'A mercadoria foi recebido em nossas instala��es.', 10, 0),
	('Transferencia', 'A mercadoria esta em transfer�ncia entre as filiais.', 20, 0),
	('Separacao', 'A mercadoria encontra-se em separa��o', 30, 0),
	('SaidaMercadoria', 'Saiu para entrega.', 40, 0),
	('Entregue', 'Mercadoria entregue.', 50, 1);

INSERT INTO Partner (CnpjCpf, Name, Description, PartnerTypeId) VALUES
	--Cpf
	('81277274070', 'Pessoa Ficticia 01', '', 3),
	('15530503047', 'Pessoa Ficticia 02', '', 3),
	('15748762013', 'Pessoa Ficticia 03', '', 3),
	('17315877088', 'Pessoa Ficticia 04', '', 3),
	('81953883028', 'Pessoa Ficticia 05', '', 3),
	('94944974060', 'Pessoa Ficticia 06', '', 3),
	('62183679085', 'Pessoa Ficticia 07', '', 3),
	('89853911000', 'Pessoa Ficticia 08', '', 3),
	('31510400044', 'Pessoa Ficticia 09', '', 3),
	('15366936004', 'Pessoa Ficticia 10', '', 3),
	('36655366003', 'Pessoa Ficticia 11', '', 3),
	('60143100025', 'Pessoa Ficticia 12', '', 3),
	('18826813043', 'Pessoa Ficticia 13', '', 3),
	('47701585052', 'Pessoa Ficticia 14', '', 3),
	('25305075033', 'Pessoa Ficticia 15', '', 3),
	('31898572020', 'Pessoa Ficticia 16', '', 3),
	('45702643011', 'Pessoa Ficticia 17', '', 3),
	('19933672096', 'Pessoa Ficticia 18', '', 3),
	('01159874018', 'Pessoa Ficticia 19', '', 3),
	('41372733086', 'Pessoa Ficticia 20', '', 3),
	--Cnpj - Wharehouse
	('34255913000170', 'Wharehouse Ficticia 01', '', 1),
	('55743392000170', 'Wharehouse Ficticia 02', '', 1),
	('39457748000153', 'Wharehouse Ficticia 03', '', 1),
	('79933730000103', 'Wharehouse Ficticia 04', '', 1),
	('39197445000149', 'Wharehouse Ficticia 05', '', 1),
	--Cnpj - Provider
	('54255913000170', 'Transportadora Ficticia 01', '', 2),
	('54743392000170', 'Transportadora Ficticia 02', '', 2),
	('30457748000153', 'Transportadora Ficticia 03', '', 2),
	('32197445000149', 'Transportadora Ficticia 05', '', 2);

DECLARE @countCPF INT = IDENT_CURRENT('Partner') - 24;
WHILE @countCPF < IDENT_CURRENT('Partner') + 1
BEGIN
	INSERT INTO Customer (Partner_Id) VALUES (@countCPF)

	SET @countCPF += 1;
END;


DECLARE @countCNPJ INT = IDENT_CURRENT('Partner') - 4;
WHILE @countCNPJ < IDENT_CURRENT('Partner') + 1
BEGIN
	INSERT INTO Provider(Partner_Id) VALUES (@countCNPJ)

	SET @countCNPJ += 1;
END;

DECLARE @count INT = 0;
DECLARE @lopping INT = 50;

WHILE @count < @lopping
BEGIN
	DECLARE @PartyRandom INT = (IDENT_CURRENT('Partner') - 24 + (SELECT ABS(CHECKSUM(NEWID()) % (20 - 1))))
	DECLARE @PartyRandomCnpj INT = (IDENT_CURRENT('Partner') - 4 + (SELECT ABS(CHECKSUM(NEWID()) % (5 - 1))))
	
	INSERT INTO Address (Partner_Id, Street, StreetNumber, District, City, State, Country, BuildingCompliment, ZipCode, Latitude, Longitude, IsPrimary) VALUES
		(@PartyRandom, CONCAT('Rua Ficticia ', @count) , '0001', 'Centro', 'Caxias do Sul', 'RS', 'BRA', '', '', NULL, NULL, 0)

	INSERT INTO Contact (Locator, Description, ContactType, Partner_Id  ) VALUES
		('(54) 99999-9999', CONCAT('Contato ', @count), 0, @PartyRandom)

	INSERT INTO Shipping (Notes, ServiceOrder, SerialNumber, TransDate, DestinationAddress_Id, DestinationCustomer_Id, OriginCustomer_Id) VALUES
		('', @count, @count, GETDATE(), IDENT_CURRENT('Address'), @PartyRandom, @PartyRandomCnpj)

	INSERT INTO Product (Sku, Quantity, UnitType, PriceUnit, Weight, Width, Length, Height, Shipping_Id) VALUES
		(@count, 1, 0, 299.90, 1000, 50, 50, 20, IDENT_CURRENT('Shipping'))

	DECLARE @countHistory INT = 1;
	WHILE @countHistory <= 5
	BEGIN
		INSERT INTO ShippingHistory (Description, Step_Id, Shipping_Id) VALUES
			('', (SELECT Id FROM Step WHERE Sequence = @countHistory * 10), IDENT_CURRENT('Shipping'))
		
		SET @countHistory += 1;
	END;

	SET @count += 1;
END;