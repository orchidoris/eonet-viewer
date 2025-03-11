CREATE TABLE [eonet].[Source] (
    [SourceId]            TINYINT       NOT NULL,
    [ValidFrom]           DATETIME2 (2) NOT NULL,
    [ValidTo]             DATETIME2 (2) NOT NULL,
    [SourceKey]           VARCHAR (16)  NOT NULL,
    [Title]               VARCHAR (128) NOT NULL,
    [ExternalHomepageUrl] VARCHAR (128) NOT NULL,
    [EventsUrl]           VARCHAR (64)  NOT NULL,
    CONSTRAINT [PK_Source_SourceId] PRIMARY KEY CLUSTERED ([SourceId] ASC),
    CONSTRAINT [UQ_Source_SourceKey] UNIQUE NONCLUSTERED ([SourceKey] ASC)
);

