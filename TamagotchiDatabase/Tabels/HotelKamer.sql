CREATE TABLE [dbo].[HotelKamer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [HoeveelheidBedden] INT NOT NULL DEFAULT 2, 
    [KamerType] NVARCHAR(50) NOT NULL,

	CONSTRAINT [FK_HotelKamer_Type] FOREIGN KEY ([KamerType]) REFERENCES [dbo].[Type] ([KamerType]) ON UPDATE CASCADE

)
