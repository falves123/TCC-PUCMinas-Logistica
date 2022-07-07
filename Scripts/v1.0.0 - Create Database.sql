CREATE TABLE Partner_Type(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Description VARCHAR(255)

	CONSTRAINT Partner_type_Id_PK PRIMARY KEY (Id)
	
);

CREATE TABLE Partner(
	Id BIGINT NOT NULL IDENTITY(1,1),
	CnpjCpf VARCHAR(14),
	Name VARCHAR(60),
	Description VARCHAR(255),
	PartnerTypeId BIGINT NOT NULL

	CONSTRAINT partner_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Partner_IdType_FK FOREIGN KEY (PartnerTypeId) REFERENCES Partner_Type(Id)
);

CREATE TABLE Customer(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Partner_Id BIGINT NOT NULL,

	CONSTRAINT Customer_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Customer_Party_Id_FK FOREIGN KEY (Partner_Id) REFERENCES Partner(Id)
);

CREATE TABLE Provider(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Partner_Id BIGINT NOT NULL,

	CONSTRAINT Provider_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Provider_Partner_Id_FK FOREIGN KEY (Partner_Id) REFERENCES Partner(Id)
);

CREATE TABLE Wharehouse(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Partner_Id BIGINT NOT NULL,

	CONSTRAINT Wharehouse_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Wharehouse_Party_Id_FK FOREIGN KEY (Partner_Id) REFERENCES Partner(Id)
);

CREATE TABLE Contact(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Partner_Id BIGINT NOT NULL,
	Description VARCHAR(60),
	Locator VARCHAR(255),
	ContactType INT,
	IsPrimary BIT,

	CONSTRAINT Contact_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Contact_Party_Id_FK FOREIGN KEY (Partner_Id) REFERENCES Partner(Id)
);

CREATE TABLE Address(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Partner_Id BIGINT NOT NULL,
	Street VARCHAR(60),
	StreetNumber VARCHAR(6),
	District VARCHAR(60),
	State VARCHAR(60),
	City VARCHAR(60),
	Country VARCHAR(60),
	BuildingCompliment VARCHAR(60),
	ZipCode VARCHAR(8),
	Latitude DECIMAL(18,6),
	Longitude DECIMAL(18,6),
	IsPrimary BIT,

	CONSTRAINT Address_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Address_Party_Id_FK FOREIGN KEY (Partner_Id) REFERENCES Partner(Id)
);

CREATE TABLE Shipping(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Notes VARCHAR(255),
	ServiceOrder VARCHAR(60),
	OriginCustomer_Id BIGINT NOT NULL,
	DestinationCustomer_Id BIGINT NOT NULL,
	TransDate DATE,
	SerialNumber VARCHAR(60),
	DestinationAddress_Id BIGINT NOT NULL,

	CONSTRAINT Shipping_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Shipping_OriginCustomer_Id_FK FOREIGN KEY (OriginCustomer_Id) REFERENCES Partner(Id),
	CONSTRAINT Shipping_DestinationCustomer_Id_FK FOREIGN KEY (DestinationCustomer_Id) REFERENCES Partner(Id),
	CONSTRAINT Shipping_DestinationAddress_Id_FK FOREIGN KEY (DestinationAddress_Id) REFERENCES Address(Id),
);

CREATE TABLE Product(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Sku VARCHAR(20),
	Quantity DECIMAL(18,3),
	UnitType INT,
	PriceUnit DECIMAL(18,2),
	Weight INT,
	Width INT,
	Length INT,
	Height INT,
	Wharehouse_Id BIGINT,
	Shipping_Id BIGINT NOT NULL,

	CONSTRAINT Product_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Product_Wharehouse_Id_FK FOREIGN KEY (Wharehouse_Id) REFERENCES Wharehouse(Id),
	CONSTRAINT Product_Shipping_Id_FK FOREIGN KEY (Shipping_Id) REFERENCES Shipping(Id)
);

CREATE TABLE Step(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Name VARCHAR(60),
	Description VARCHAR(255),
	Sequence INT,
	IsDelivered BIT,

	CONSTRAINT Step_Id_PK PRIMARY KEY (Id),
);

CREATE TABLE ShippingHistory(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Description VARCHAR(255),
	Step_Id BIGINT,
	Shipping_Id BIGINT,

	CONSTRAINT ShippingHistory_Id_PK PRIMARY KEY (Id),
	CONSTRAINT ShippingHistory_Step_Id_FK FOREIGN KEY (Step_Id) REFERENCES Step(Id),
	CONSTRAINT ShippingHistory_Charge_Id_FK FOREIGN KEY (Shipping_Id) REFERENCES Shipping(Id)
);