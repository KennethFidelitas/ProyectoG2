CREATE DATABASE SinpeEmpresarialDB;
USE SinpeEmpresarialDB;

CREATE TABLE Comercios (
    IdComercio INT AUTO_INCREMENT PRIMARY KEY,
    Identificacion VARCHAR(30) NOT NULL,
    TipoIdentificacion INT NOT NULL,
    Nombre VARCHAR(200) NOT NULL,
    TipoDeComercio INT NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    CorreoElectronico VARCHAR(200) NOT NULL,
    Direccion VARCHAR(500) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL
);

CREATE TABLE Cajas (
    IdCaja INT AUTO_INCREMENT PRIMARY KEY,
    IdComercio INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(150) NOT NULL,
    TelefonoSINPE VARCHAR(10) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdComercio) REFERENCES Comercios(IdComercio)
);

CREATE TABLE Sinpes (
    IdSinpe INT AUTO_INCREMENT PRIMARY KEY,
    TelefonoOrigen VARCHAR(10) NOT NULL,
    NombreOrigen VARCHAR(200) NOT NULL,
    TelefonoDestinatario VARCHAR(10) NOT NULL,
    NombreDestinatario VARCHAR(200) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    Descripcion VARCHAR(50) NULL,
    Estado BIT NOT NULL
);

CREATE TABLE Bitacora_Eventos (
    IdEvento INT AUTO_INCREMENT PRIMARY KEY,
    TablaDeEvento VARCHAR(20) NOT NULL,
    TipoDeEvento VARCHAR(20) NOT NULL,
    FechaDeEvento DATETIME NOT NULL,
    DescripcionDeEvento TEXT NOT NULL,
    StackTrace TEXT NOT NULL,
    DatosAnteriores TEXT NULL,
    DatosPosteriores TEXT NULL
);