
/*
    Script de creación de base de datos: Universidad (v1.2 - con datos)
    Compatibilidad: Microsoft SQL Server 2016+
    Contenido:
      - Crea BD Universidad (si no existe)
      - Crea tablas: Carrera y Estudiante (relación 1:N)
      - PK, FK, índices, restricciones
      - Inserta 10 registros en cada tabla
*/

---------------------------------------------
-- 1) Crear base de datos (si no existe)
---------------------------------------------
IF DB_ID(N'Universidad') IS NULL
BEGIN
    PRINT 'Creando base de datos Universidad...';
    CREATE DATABASE Universidad;
END
ELSE
BEGIN
    PRINT 'La base de datos Universidad ya existe.';
END
GO

---------------------------------------------
-- 2) Usar la base de datos
---------------------------------------------
USE Universidad;
GO

---------------------------------------------
-- 3) Limpieza segura (opcional)
---------------------------------------------
IF OBJECT_ID(N'dbo.Estudiante', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Estudiante;
END
IF OBJECT_ID(N'dbo.Carrera', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Carrera;
END
GO

---------------------------------------------
-- 4) Crear tabla: Carrera
---------------------------------------------
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE dbo.Carrera
(
    Id        INT IDENTITY(1,1) NOT NULL
        CONSTRAINT PK_Carrera PRIMARY KEY CLUSTERED,
    Nombre    NVARCHAR(150) NOT NULL
        CONSTRAINT UQ_Carrera_Nombre UNIQUE,
    FechaCreacion  DATETIME2(0) NOT NULL 
        CONSTRAINT DF_Carrera_FechaCreacion DEFAULT (SYSUTCDATETIME())
);
GO

---------------------------------------------
-- 5) Crear tabla: Estudiante
---------------------------------------------
CREATE TABLE dbo.Estudiante
(
    Id               INT IDENTITY(1,1) NOT NULL
        CONSTRAINT PK_Estudiante PRIMARY KEY CLUSTERED,

    Paterno          NVARCHAR(100) NOT NULL,
    Materno          NVARCHAR(100) NULL,
    Nombres          NVARCHAR(150) NOT NULL,

    FechaNacimiento  DATE NOT NULL
        CONSTRAINT CK_Estudiante_FechaNacimiento_Rango
            CHECK (FechaNacimiento >= ('1900-01-01') AND FechaNacimiento <= CAST(GETDATE() AS DATE)),

    Correo           NVARCHAR(254) NOT NULL
        CONSTRAINT CK_Estudiante_Correo_Formato
            CHECK (Correo LIKE '%_@_%._%'),

    CarreraId        INT NOT NULL,

    FechaRegistro    DATETIME2(0) NOT NULL 
        CONSTRAINT DF_Estudiante_FechaRegistro DEFAULT (SYSUTCDATETIME())
);
GO

-- UNIQUE en Correo a nivel de tabla
ALTER TABLE dbo.Estudiante
    ADD CONSTRAINT UQ_Estudiante_Correo UNIQUE (Correo);
GO

---------------------------------------------
-- 6) Clave foránea + Índices
---------------------------------------------
ALTER TABLE dbo.Estudiante
    ADD CONSTRAINT FK_Estudiante_Carrera
        FOREIGN KEY (CarreraId)
        REFERENCES dbo.Carrera (Id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION;
GO

CREATE NONCLUSTERED INDEX IX_Estudiante_CarreraId
    ON dbo.Estudiante (CarreraId);
GO

CREATE NONCLUSTERED INDEX IX_Estudiante_Apellidos
    ON dbo.Estudiante (Paterno, Materno);
GO

---------------------------------------------
-- 7) Datos de ejemplo: 10 registros por tabla
---------------------------------------------

-- 7.1 Carreras (10)
INSERT INTO dbo.Carrera (Nombre) VALUES
(N'Ingeniería de Sistemas'),
(N'Administración'),
(N'Derecho'),
(N'Contabilidad'),
(N'Medicina'),
(N'Arquitectura'),
(N'Psicología'),
(N'Ingeniería Civil'),
(N'Marketing'),
(N'Educación');
GO

-- 7.2 Estudiantes (10) - cada uno con una CarreraId existente del 1 al 10
INSERT INTO dbo.Estudiante
(Paterno, Materno, Nombres, FechaNacimiento, Correo, CarreraId)
VALUES
(N'García',   N'López',     N'Andrea Paola',    '2003-05-12', N'andrea.garcia@example.edu', 1),
(N'Ramos',    N'Quispe',    N'Luis Alberto',    '2002-11-30', N'luis.ramos@example.edu',    2),
(N'Fernández',N'Pérez',     N'Carla Sofía',     '2004-02-18', N'carla.fernandez@example.edu',3),
(N'Salazar',  N'Castro',    N'Jorge Enrique',   '2001-07-25', N'jorge.salazar@example.edu', 4),
(N'Mendoza',  N'Vargas',    N'Valeria Lucía',   '2003-09-09', N'valeria.mendoza@example.edu',5),
(N'Chávez',   N'Navarro',   N'Diego Armando',   '2000-12-01', N'diego.chavez@example.edu',  6),
(N'Paredes',  N'Ríos',      N'Camila Antonia',  '2002-04-07', N'camila.paredes@example.edu',7),
(N'Ruiz',     N'Aguilar',   N'Sebastián I.',    '1999-08-19', N'sebastian.ruiz@example.edu',8),
(N'Flores',   N'Guzmán',    N'Lucía Mariela',   '2004-10-03', N'lucia.flores@example.edu',  9),
(N'Peña',     N'Rojas',     N'Bruno Esteban',   '2001-03-16', N'bruno.pena@example.edu',    10);
GO

---------------------------------------------
-- 8) Metadatos útiles (versionado simple)
---------------------------------------------
IF OBJECT_ID(N'dbo.__Meta', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.__Meta
    (
        Clave NVARCHAR(100) NOT NULL PRIMARY KEY,
        Valor NVARCHAR(4000) NOT NULL,
        Fecha DATETIME2(0) NOT NULL CONSTRAINT DF_Meta_Fecha DEFAULT (SYSUTCDATETIME())
    );
END
GO

MERGE dbo.__Meta AS T
USING (SELECT N'Universidad.SchemaVersion' AS Clave, N'1.2.0' AS Valor) AS S
ON (T.Clave = S.Clave)
WHEN MATCHED THEN UPDATE SET Valor = S.Valor, Fecha = SYSUTCDATETIME()
WHEN NOT MATCHED THEN INSERT (Clave, Valor) VALUES (S.Clave, S.Valor);
GO

PRINT 'Script completado con 10 registros por tabla.';
