CREATE TABLE [eonet].[Position] (
    [PositionId]      INT           NOT NULL,
    [ValidFrom]       DATETIME2 (2) NOT NULL,
    [ValidTo]         DATETIME2 (2) NOT NULL,
    [EventGeometryId] INT           NOT NULL,
    [PositionOrder]   TINYINT       NOT NULL,
    [Latitude]        FLOAT (53)    NOT NULL,
    [Longitude]       FLOAT (53)    NOT NULL,
    [Altitude]        FLOAT (53)    NULL,
    CONSTRAINT [PK_Position_PositionId] PRIMARY KEY CLUSTERED ([PositionId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Position_EventGeometryId]
    ON [eonet].[Position]([EventGeometryId] ASC);

