CREATE TABLE [eonet].[Magnitude] (
    [MagnitudeId]  TINYINT       NOT NULL,
    [ValidFrom]    DATETIME2 (2) NOT NULL,
    [ValidTo]      DATETIME2 (2) NOT NULL,
    [MagnitudeKey] VARCHAR (8)   NOT NULL,
    [Title]        VARCHAR (32)  NOT NULL,
    [Unit]         VARCHAR (8)   NOT NULL,
    [Description]  VARCHAR (512) NOT NULL,
    [EventsUrl]    VARCHAR (64)  NOT NULL,
    CONSTRAINT [PK_Magnitude_MagnitudeId] PRIMARY KEY CLUSTERED ([MagnitudeId] ASC),
    CONSTRAINT [UQ_Magnitude_MagnitudeKey] UNIQUE NONCLUSTERED ([MagnitudeKey] ASC)
);

