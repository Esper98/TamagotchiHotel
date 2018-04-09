MERGE INTO dbo.Type  AS Target
USING (values 

	('RustKamer'),
	('VechtKamer'),
	('GameKamer'),
	('VerfKamer'),
	('WerkKamer')

)AS Source (KamerType)
ON Target.KamerType = Source.KamerType
WHEN NOT MATCHED BY TARGET THEN
    INSERT (KamerType)
VALUES (KamerType);