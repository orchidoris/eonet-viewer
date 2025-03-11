CREATE TABLE [eonet].[EventSource] (
    [EventId]                INT           NOT NULL,
    [SourceId]               TINYINT       NOT NULL,
    [ValidFrom]              DATETIME2 (2) NOT NULL,
    [ValidTo]                DATETIME2 (2) NOT NULL,
    [ExternalSourceEventUrl] VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_EventSource_EventId_SourceId] PRIMARY KEY CLUSTERED ([EventId] ASC, [SourceId] ASC),
    CONSTRAINT [FK_EventSource_Event] FOREIGN KEY ([EventId]) REFERENCES [eonet].[Event] ([EventId]),
    CONSTRAINT [FK_EventSource_Source] FOREIGN KEY ([SourceId]) REFERENCES [eonet].[Source] ([SourceId])
);

