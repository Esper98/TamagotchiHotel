    MERGE INTO dbo.HotelKamer  AS Target
USING (values 

	(1, 2, 'VechtKamer'),
	(2, 2, 'RustKamer'),
	(3, 2, 'GameKamer'),
	(4, 2, 'VerfKamer'),
	(5, 2, 'WerkKamer')

)AS Source (Id, HoeveelheidBedden, KamerType)
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, HoeveelheidBedden, KamerType)
    VALUES (Id, HoeveelheidBedden, KamerType)
WHEN MATCHED THEN
    UPDATE SET
		HoeveelheidBedden = Source.HoeveelheidBedden,
		KamerType = Source.KamerType;
