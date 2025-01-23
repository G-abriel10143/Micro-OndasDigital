CREATE TABLE [dbo].[ProgramasAquecimentoCustomizados] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [Nome] NVARCHAR(255) NOT NULL,
    [Alimento] NVARCHAR(255) NOT NULL,
    [Potencia] INT NOT NULL,
    [Tempo] TIME NOT NULL,
    [Instrucoes] NVARCHAR(MAX)
);

INSERT INTO [dbo].[ProgramasAquecimentoCustomizados] ([Nome], [Alimento], [Potencia], [Tempo], [Instrucoes])
VALUES ('Aquecimento Rápido', 'Sopa', 8, '00:03:00', 'Aqueça em potência média por 3 minutos, mexendo na metade do tempo.');

INSERT INTO [dbo].[ProgramasAquecimentoCustomizados] ([Nome], [Alimento], [Potencia], [Tempo], [Instrucoes])
VALUES ('Descongelamento', 'Carne', 4, '00:10:00', 'Utilize potência baixa por 10 minutos, virando a carne na metade do tempo.');
