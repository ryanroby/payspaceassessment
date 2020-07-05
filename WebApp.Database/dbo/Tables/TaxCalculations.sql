CREATE TABLE [dbo].[TaxCalculations] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [PostalCode]      NVARCHAR (4) NULL,
    [Salary]          VARCHAR (50) NULL,
    [CalculationType] VARCHAR (50) NULL,
    [DateCalculated]  DATETIME     NULL,
    [Tax]             DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

