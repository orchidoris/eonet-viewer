CREATE TABLE [eonet].[GeometryType] (
    [GeometryTypeId] TINYINT       NOT NULL,
    [ValidFrom]      DATETIME2 (2) NOT NULL,
    [Name]           VARCHAR (7)   NOT NULL,
    CONSTRAINT [PK_GeometryType_GeometryTypeId] PRIMARY KEY CLUSTERED ([GeometryTypeId] ASC),
    CONSTRAINT [UQ_GeometryType_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

