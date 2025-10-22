Create database BibliotecaMetropolisDB;
GO

USE BibliotecaMetropolisDB;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Autor] (
    [idAutor] int NOT NULL IDENTITY,
    [Nombres] nvarchar(100) NOT NULL,
    [Apellidos] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Autor] PRIMARY KEY ([idAutor])
);

CREATE TABLE [Editorial] (
    [IdEdit] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    CONSTRAINT [PK_Editorial] PRIMARY KEY ([IdEdit])
);

CREATE TABLE [Pais] (
    [IdPais] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Pais] PRIMARY KEY ([IdPais])
);

CREATE TABLE [TipoRecurso] (
    [IdTipoR] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    CONSTRAINT [PK_TipoRecurso] PRIMARY KEY ([IdTipoR])
);

CREATE TABLE [Recurso] (
    [IdRec] int NOT NULL IDENTITY,
    [IdPais] int NOT NULL,
    [IdTipoR] int NOT NULL,
    [IdEdit] int NOT NULL,
    [Titulo] nvarchar(255) NOT NULL,
    [annopublic] int NULL,
    [Edicion] nvarchar(50) NULL,
    [PalabrasBusqueda] nvarchar(500) NULL,
    [Descripcion] nvarchar(1000) NULL,
    [AutoridAutor] int NULL,
    CONSTRAINT [PK_Recurso] PRIMARY KEY ([IdRec]),
    CONSTRAINT [FK_Recurso_Autor_AutoridAutor] FOREIGN KEY ([AutoridAutor]) REFERENCES [Autor] ([idAutor]),
    CONSTRAINT [FK_Recurso_Editorial_IdEdit] FOREIGN KEY ([IdEdit]) REFERENCES [Editorial] ([IdEdit]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Recurso_Pais_IdPais] FOREIGN KEY ([IdPais]) REFERENCES [Pais] ([IdPais]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Recurso_TipoRecurso_IdTipoR] FOREIGN KEY ([IdTipoR]) REFERENCES [TipoRecurso] ([IdTipoR]) ON DELETE NO ACTION
);

CREATE TABLE [AutoresRecursos] (
    [IdRec] int NOT NULL,
    [idAutor] int NOT NULL,
    [EsPrincipal] bit NOT NULL,
    CONSTRAINT [PK_AutoresRecursos] PRIMARY KEY ([IdRec], [idAutor]),
    CONSTRAINT [FK_AutoresRecursos_Autor_idAutor] FOREIGN KEY ([idAutor]) REFERENCES [Autor] ([idAutor]) ON DELETE CASCADE,
    CONSTRAINT [FK_AutoresRecursos_Recurso_IdRec] FOREIGN KEY ([IdRec]) REFERENCES [Recurso] ([IdRec]) ON DELETE CASCADE
);

CREATE INDEX [IX_AutoresRecursos_idAutor] ON [AutoresRecursos] ([idAutor]);

CREATE INDEX [IX_Recurso_AutoridAutor] ON [Recurso] ([AutoridAutor]);

CREATE INDEX [IX_Recurso_IdEdit] ON [Recurso] ([IdEdit]);

CREATE INDEX [IX_Recurso_IdPais] ON [Recurso] ([IdPais]);

CREATE INDEX [IX_Recurso_IdTipoR] ON [Recurso] ([IdTipoR]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251022030109_InitialCreate_Clean', N'9.0.10');

COMMIT;
GO

INSERT INTO Pais (nombre) VALUES
('Estados Unidos'),
('Espa�a'),
('M�xico'),
('Argentina'),
('Colombia'),
('Reino Unido'),
('Francia');

INSERT INTO Editorial (Nombre, description) VALUES
('Penguin Random House', 'Una de las mayores empresas editoriales del mundo'),
('Planeta', 'Grupo editorial l�der en Espa�a y Am�rica Latina'),
('McGraw-Hill', 'Editorial especializada en libros educativos y t�cnicos'),
('Anaya', 'Grupo editorial espa�ol con amplio cat�logo educativo'),
('Fondo de Cultura Econ�mica', 'Editorial mexicana especializada en ciencias sociales');

INSERT INTO TipoRecurso (nombre, description) VALUES
('Libro', 'Publicaci�n impresa o digital de contenido literario o acad�mico'),
('Revista', 'Publicaci�n peri�dica sobre temas espec�ficos'),
('Art�culo', 'Texto breve sobre un tema particular'),
('Tesis', 'Trabajo de investigaci�n acad�mica'),
('Video', 'Contenido multimedia en formato audiovisual');

INSERT INTO Autor (nombres, apellidos) VALUES
('Gabriel', 'Garc�a M�rquez'),
('Isabel', 'Allende'),
('Mario', 'Vargas Llosa'),
('Laura', 'Esquivel'),
('Julio', 'Cort�zar'),
('Carlos', 'Fuentes'),
('Elena', 'Poniatowska');

INSERT INTO Recurso (idPais, idTipoR, idEdit, titulo, annopublic, edicion, palabrasbusqueda, descripcion) VALUES
(3, 1, 2, 'Cien a�os de soledad', 1967, '1ra edici�n', 'realismo m�gico, Macondo, familia Buend�a', 'Novela que narra la historia de la familia Buend�a en el pueblo ficticio de Macondo'),
(2, 1, 4, 'La casa de los esp�ritus', 1982, '2da edici�n', 'familia Trueba, realismo m�gico, Chile', 'Novela que sigue la vida de la familia Trueba a trav�s de cuatro generaciones'),
(1, 1, 3, 'Cien problemas de f�sica', 2020, '3ra edici�n', 'f�sica, problemas, ejercicios, educaci�n', 'Libro de problemas de f�sica para nivel universitario'),
(4, 4, 5, 'An�lisis de la literatura latinoamericana', 2018, '1ra edici�n', 'tesis, literatura, an�lisis, cr�tica', 'Tesis doctoral sobre tendencias en la literatura latinoamericana contempor�nea');

INSERT INTO AutoresRecursos (idRec, idAutor, EsPrincipal) VALUES
(1, 1, 1),  -- Garc�a M�rquez principal en Cien a�os de soledad
(2, 2, 1),  -- Isabel Allende principal en La casa de los esp�ritus
(1, 3, 0),  -- Vargas Llosa coautor (ejemplo)
(3, 6, 1),  -- Carlos Fuentes principal en libro de f�sica (ejemplo)
(4, 7, 1);  -- Elena Poniatowska principal en tesis

