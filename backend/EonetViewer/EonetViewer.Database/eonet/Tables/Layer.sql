CREATE TABLE [eonet].[Layer] (
    [LayerId]       TINYINT       NOT NULL,
    [ValidFrom]     DATETIME2 (2) NOT NULL,
    [ValidTo]       DATETIME2 (2) NOT NULL,
    [LayerKey]      VARCHAR (64)  NOT NULL,
    [ServiceUrl]    VARCHAR (64)  NOT NULL,
    [ServiceTypeId] VARCHAR (16)  NOT NULL,
    [Format]        VARCHAR (16)  NULL,
    [TileMatrixSet] NCHAR (4)     NULL,
    CONSTRAINT [PK_Layer_LayerId] PRIMARY KEY CLUSTERED ([LayerId] ASC),
    CONSTRAINT [UQ_Layer_LayerKey] UNIQUE NONCLUSTERED ([LayerId] ASC)
);

