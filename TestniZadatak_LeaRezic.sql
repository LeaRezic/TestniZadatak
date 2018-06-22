
-- KREIRANJE BAZE I TABLICA

CREATE DATABASE TestniZadatak_LeaRezic
GO
USE TestniZadatak_LeaRezic

-- za dovođenje u višu normalnu formu mogla bi se izdvojiti zasebna tablica ADRESA koja bi 
-- sadržavala StreetName, StreetNumber (ili zajedno), PostalCode i PlaceID, ali namjerno je
-- odabrana ova strategija za jednostavnije dohvaćanje nauštrb činjenici da će se možda pojaviti koji dupli
-- redak ukoliko u bazi imamo 2 firme na istoj adresi (ali pretpostavka je da je to dovoljno rijedak slučaj)

CREATE TABLE Place
(
	IDPlace int IDENTITY
	,Name nvarchar(50) NOT NULL
	,CONSTRAINT PK_Place PRIMARY KEY (IDPlace)
)

-- PostalCode je nvarchar jer neke države imaju slova (iako su testni podaci samo mjesta u Hr)

CREATE TABLE Company
(
	IDCompany int IDENTITY
	,Name nvarchar(50) NOT NULL
	,CompanyCode nvarchar(25) NOT NULL
	,[Address] nvarchar(100) NOT NULL
	,PostalCode nvarchar(25) NOT NULL
	,PlaceID int NOT NULL
	,CONSTRAINT PK_Company PRIMARY KEY (IDCompany)
	,CONSTRAINT FK_CompanyPlace FOREIGN KEY (PlaceID) REFERENCES Place(IDPlace)
)

-- PUNJENJE BAZE INICIJALNIM PODACIMA (bez pretjerano kompliciranja, pretpostavimo da je baza prazna pa FK ID-jevi odgovaraju)

INSERT INTO Place (Name)
VALUES ('Ilok')
,('Babina Greda')
,('Požega')
,('Koprivnica')
,('Zagreb')
,('Velika Horvatska')
,('Mala Mlaka')
,('Sveti Petar u Šumi')
,('Rijeka')
,('Makarska')
,('Podgora')
,('Dubrovnik')

INSERT INTO Company(Name, CompanyCode, [Address], PostalCode, PlaceID)
VALUES ('Iločki Podrumi', 'C1001', 'Zagrebačka ulica 22', '32236', 1)
,('Gradnja d.o.o', 'C2001', 'Ulica kralja Tomislava 45', '32270', 2)
,('Požeška Slanina', 'C2002', 'Slavonska ulica 15', '34000', 3)
,('Kompjuter d.o.o', 'C1002', 'Trg slobode 1', '48000', 4)
,('Podravka', 'C2003', 'Ulica Ante Starčevića 37', '48000', 4)
,('Mondo-tera', 'C2022', 'Ulica Marijana Čavića 1A', '10000', 5)
,('TestnaFirma1', 'C2004', 'Testna ulica 11', '11111', 6)
,('TestnaFirma2', 'C2005', 'Testna ulica 22', '22222', 7)
,('TestnaFirma3', 'C2006', 'Testna ulica 33', '33333', 8)
,('TestnaFirma4', 'C2007', 'Testna ulica 44', '44444', 9)
,('TestnaFirma5', 'C2008', 'Testna ulica 55', '55555', 10)
,('TestnaFirma6', 'C2009', 'Testna ulica 66', '66666', 11)
,('Hotel Adriatic', 'C2010', 'Ulica ispod Petke 36', '22000', 12)
,('Libertas d.o.o', 'C1003', 'Pile 1', '22000', 12)


-- samo testa radi

SELECT
	c.*
	,p.Name AS Place
FROM Company AS c
INNER JOIN Place AS p ON c.PlaceID = p.IDPlace