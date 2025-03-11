CREATE TABLE [eonet].[EventCategory] (
    [EventId]    INT           NOT NULL,
    [CategoryId] TINYINT       NOT NULL,
    [ValidFrom]  DATETIME2 (2) NOT NULL,
    [ValidTo]    DATETIME2 (2) NOT NULL,
    CONSTRAINT [PK_EventCategory_EventId_CategoryId] PRIMARY KEY CLUSTERED ([EventId] ASC, [CategoryId] ASC),
    CONSTRAINT [FK_EventCategory_Category] FOREIGN KEY ([CategoryId]) REFERENCES [eonet].[Category] ([CategoryId]),
    CONSTRAINT [FK_EventCategory_Event] FOREIGN KEY ([EventId]) REFERENCES [eonet].[Event] ([EventId])
);

