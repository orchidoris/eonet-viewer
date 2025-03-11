CREATE TABLE [eonet].[Event] (
    [EventId]     INT                NOT NULL,
    [ValidFrom]   DATETIME2 (2)      NOT NULL,
    [ValidTo]     DATETIME2 (2)      NOT NULL,
    [EventKey]    VARCHAR (16)       NOT NULL,
    [Title]       VARCHAR (256)      NOT NULL,
    [Description] VARCHAR (1024)     NULL,
    [Url]         VARCHAR (64)       NOT NULL,
    [ClosedDate]  DATETIMEOFFSET (2) NULL,
    CONSTRAINT [PK_Event_EventId] PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT [UQ_Event_EventKey] UNIQUE NONCLUSTERED ([EventKey] ASC)
);

