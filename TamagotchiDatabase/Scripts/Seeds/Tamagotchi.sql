    MERGE INTO dbo.Tamagotchi  AS Target
USING (values 

	(1, null, 'Vecht1', 'Grijs', 0, 100, 0, 100, 0, 0),
	(2, null, 'Vecht2', 'Zwart', 0, 100, 0, 100, 0, 0),

	(3, null, 'Rust1', 'Grijs', 0, 100, 0, 100, 0, 0),
	(4, null, 'Rust2', 'Zwart', 0, 100, 0, 100, 0, 0),

	(5, null, 'Game1', 'Grijs', 0, 100, 0, 100, 0, 0),
	(6, null, 'Game2', 'Zwart', 0, 100, 0, 100, 0, 0),

	(7, null, 'Werk1', 'Grijs', 0, 100, 0, 100, 0, 0),
	(8, null, 'Werk2', 'Zwart', 0, 100, 0, 100, 0, 0),

	(9, null, 'Verf1', 'Grijs', 0, 100, 0, 100, 0, 0),
	(10, null, 'Verf2', 'Zwart', 0, 100, 0, 100, 0, 0)

)AS Source (Id, HotelKamerID, Naam, Kleur, Leeftijd, Centjes, Level, Gezondheid, Verveling, Dood)
ON Target.ID = Source.ID
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, HotelKamerID, Naam, Kleur, Leeftijd, Centjes, Level, Gezondheid, Verveling, Dood)
    VALUES (Id, HotelKamerID, Naam, Kleur, Leeftijd, Centjes, Level, Gezondheid, Verveling, Dood)
WHEN MATCHED THEN
    UPDATE SET
		HotelKamerID = Source.HotelKamerID,
		Naam = Source.Naam,
		Kleur  = Source.Kleur,
		Leeftijd = Source.Leeftijd,
		Centjes  = Source.Centjes,
		Level = Source.Level,
		Gezondheid = Source.Gezondheid,
		Verveling  = Source.Verveling,
		Dood = Source.Dood;
