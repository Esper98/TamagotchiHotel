CREATE TABLE [dbo].[Tamagotchi]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[HotelKamerID] INT NULL,  
    [Naam] VARCHAR(10) NOT NULL, 
	[Kleur] VARCHAR(10) NOT NULL, 
    [Leeftijd] INT NOT NULL DEFAULT 0, 
    [Centjes] INT NOT NULL DEFAULT 100, 
    [Level] INT NOT NULL DEFAULT 0, 
    [Gezondheid] INT NOT NULL DEFAULT 100, 
    [Verveling] INT NOT NULL DEFAULT 0, 
    [Dood] BIT NOT NULL DEFAULT 0,

	
    CONSTRAINT [FK_Tamagotchi_HotelKamer] FOREIGN KEY ([HotelKamerID]) REFERENCES [dbo].[HotelKamer] ([Id]) ON UPDATE CASCADE,

)
