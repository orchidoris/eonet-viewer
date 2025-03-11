CREATE TABLE [eonet].[CategoryLayer] (
    [CategoryId] VARCHAR (16)  NOT NULL,
    [LayerId]    NCHAR (64)    NOT NULL,
    [ValidFrom]  DATETIME2 (2) NOT NULL,
    [ValidTo]    DATETIME2 (2) NOT NULL,
    CONSTRAINT [PK_CategoryLayer_CategoryId_LayerId] PRIMARY KEY CLUSTERED ([CategoryId] ASC, [LayerId] ASC)
);

