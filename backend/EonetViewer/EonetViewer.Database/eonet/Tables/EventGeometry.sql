CREATE TABLE [eonet].[EventGeometry] (
    [EventGeometryId] INT                NOT NULL,
    [ValidFrom]       DATETIME2 (2)      NOT NULL,
    [ValidTo]         DATETIME2 (2)      NOT NULL,
    [EventId]         INT                NOT NULL,
    [Date]            DATETIMEOFFSET (2) NOT NULL,
    [GeometryTypeId]  TINYINT            NOT NULL,
    [MagnitudeId]     TINYINT            NOT NULL,
    [MagnitudeValue]  FLOAT (53)         NOT NULL,
    CONSTRAINT [PK_EventGeometry_EventGeometryId] PRIMARY KEY CLUSTERED ([EventGeometryId] ASC),
    CONSTRAINT [FK_EventGeometry_Event] FOREIGN KEY ([EventId]) REFERENCES [eonet].[Event] ([EventId]),
    CONSTRAINT [FK_EventGeometry_GeometryType] FOREIGN KEY ([GeometryTypeId]) REFERENCES [eonet].[GeometryType] ([GeometryTypeId]),
    CONSTRAINT [FK_EventGeometry_Magnitude1] FOREIGN KEY ([MagnitudeId]) REFERENCES [eonet].[Magnitude] ([MagnitudeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_EventGeometry_EventId]
    ON [eonet].[EventGeometry]([EventId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EventGeometry_GeometryTypeId]
    ON [eonet].[EventGeometry]([GeometryTypeId] ASC);

