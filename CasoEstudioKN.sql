-- Crear base de datos
IF DB_ID('CasoEstudioKN') IS NULL
    CREATE DATABASE CasoEstudioKN;
GO

USE CasoEstudioKN;
GO

-- Tabla: CasasSistema
CREATE TABLE CasasSistema(
    IdCasa BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    DescripcionCasa VARCHAR(30) NOT NULL,
    PrecioCasa Decimal (10,2) NOT NULL,
    UsuarioAlquiler VARCHAR(30),
    FechaAlquiler DateTime
);


INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa],[PrecioCasa],[UsuarioAlquiler],[FechaAlquiler])
VALUES ('Casa en San José',190000,null,null)
INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa],[PrecioCasa],[UsuarioAlquiler],[FechaAlquiler])
VALUES ('Casa en Alajuela',145000,null,null)
INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa],[PrecioCasa],[UsuarioAlquiler],[FechaAlquiler])
VALUES ('Casa en Cartago',115000,null,null)
INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa],[PrecioCasa],[UsuarioAlquiler],[FechaAlquiler])
VALUES ('Casa en Heredia',122000,null,null)
INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa],[PrecioCasa],[UsuarioAlquiler],[FechaAlquiler])
VALUES ('Casa en Guanacaste',105000,null,null)