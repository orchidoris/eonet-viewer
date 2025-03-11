CREATE TABLE [eonet].[Category] (
    [CategoryId]  TINYINT       NOT NULL,
    [ValidFrom]   DATETIME2 (2) NOT NULL,
    [ValidTo]     DATETIME2 (2) NOT NULL,
    [CategoryKey] VARCHAR (16)  NOT NULL,
    [IsActive]    BIT           NOT NULL,
    [Title]       VARCHAR (32)  NOT NULL,
    [Description] VARCHAR (256) NOT NULL,
    [Url]         VARCHAR (64)  NOT NULL,
    [LayersUrl]   VARCHAR (64)  NOT NULL,
    CONSTRAINT [PK_Category_CategoryId] PRIMARY KEY CLUSTERED ([CategoryId] ASC),
    CONSTRAINT [UQ_Category_CategoryKey] UNIQUE NONCLUSTERED ([CategoryKey] ASC)
);

