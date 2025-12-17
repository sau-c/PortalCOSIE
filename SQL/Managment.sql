use PortalCOSIE

--Generar diagrama relacional LucidChart
--SELECT 'sqlserver' dbms,t.TABLE_CATALOG,t.TABLE_SCHEMA,t.TABLE_NAME,c.COLUMN_NAME,c.ORDINAL_POSITION,c.DATA_TYPE,c.CHARACTER_MAXIMUM_LENGTH,n.CONSTRAINT_TYPE,k2.TABLE_SCHEMA,k2.TABLE_NAME,k2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLES t LEFT JOIN INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_CATALOG=c.TABLE_CATALOG AND t.TABLE_SCHEMA=c.TABLE_SCHEMA AND t.TABLE_NAME=c.TABLE_NAME LEFT JOIN(INFORMATION_SCHEMA.KEY_COLUMN_USAGE k JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS n ON k.CONSTRAINT_CATALOG=n.CONSTRAINT_CATALOG AND k.CONSTRAINT_SCHEMA=n.CONSTRAINT_SCHEMA AND k.CONSTRAINT_NAME=n.CONSTRAINT_NAME LEFT JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON k.CONSTRAINT_CATALOG=r.CONSTRAINT_CATALOG AND k.CONSTRAINT_SCHEMA=r.CONSTRAINT_SCHEMA AND k.CONSTRAINT_NAME=r.CONSTRAINT_NAME)ON c.TABLE_CATALOG=k.TABLE_CATALOG AND c.TABLE_SCHEMA=k.TABLE_SCHEMA AND c.TABLE_NAME=k.TABLE_NAME AND c.COLUMN_NAME=k.COLUMN_NAME LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k2 ON k.ORDINAL_POSITION=k2.ORDINAL_POSITION AND r.UNIQUE_CONSTRAINT_CATALOG=k2.CONSTRAINT_CATALOG AND r.UNIQUE_CONSTRAINT_SCHEMA=k2.CONSTRAINT_SCHEMA AND r.UNIQUE_CONSTRAINT_NAME=k2.CONSTRAINT_NAME WHERE t.TABLE_TYPE='BASE TABLE';

--Identity
select * from AspNetUsers
select * from AspNetRoles
select * from AspNetUserRoles
--select * from AspNetRoleClaims
--select * from AspNetUserClaims
--select * from AspNetUserLogins
--select * from AspNetUserTokens

-- Dominio --
select * from Bitacora
-- Carrera
select * from Carrera
select * from UnidadAprendizaje
-- Tramite
select * from Tramite
select * from TramiteCTCE
select * from UnidadReprobada
select * from EstadoTramite
select * from TipoTramite
--Documentos
select * from Documento
select * from EstadoDocumento
select * from Personal
select * from Alumno
select * from Usuario
select * from PeriodoConfig
select * from SesionCOSIE
select * from FechaRecepcion

--Modulo autenticacion
select * from Alumno
select * from Personal
select * from Usuario
select * from AspNetUsers

EXEC poblarBase

EXEC agregarAlumno
    @Email = 'sau.contacto@gmail.com',
    @PhoneNumber = '5512795297',
    @Nombre = 'Saul',
    @ApellidoPaterno = 'CASTANEDA',
    @ApellidoMaterno = 'Zepeda',
    @NumeroBoleta = '2020640595',
    @PeriodoIngreso = '2020/1';

--DELETE FROM Alumno
--DELETE FROM Usuario
--DELETE FROM AspNetUsers WHERE UserName LIKE '%sau%'
--DELETE FROM AspNetUsers WHERE UserName = 'scastanedaz1900@alumno.ipn.mx'


--UPDATE AspNetUsers
--SET PasswordHash = 'AQAAAAIAAYagAAAAEEFVNy7BXpX/CAk6xy34b/DL+ArNM1nyqosz6s/D/s1nCGtfehBnilfy5KlZeJTZ0Q=='
--WHERE Id = 'guid-personal-coord'

--UPDATE PeriodoConfig
--SET PeriodoActual = '1'
--WHERE Id = 1


--%%%%%%%%%%%%%%%%%%%%%%%%
--%%%% ELIMINAR TABLAS %%%
--%%%%%%%%%%%%%%%%%%%%%%%%
--DROP TABLE dbo.Bitacora
--GO
--DROP TABLE dbo.PeriodoConfig
--GO
--DROP TABLE dbo.FechaRecepcion
--GO
--DROP TABLE dbo.SesionCOSIE
--GO
--DROP TABLE dbo.Documento
--GO
--DROP TABLE dbo.TipoDocumento
--GO
--DROP TABLE dbo.EstadoDocumento
--GO
--DROP TABLE dbo.UnidadReprobada
--GO
--DROP TABLE dbo.TramiteCTCE
--GO
--DROP TABLE dbo.Tramite
--GO
--DROP TABLE dbo.EstadoTramite
--GO
--DROP TABLE dbo.Personal
--GO
--DROP TABLE dbo.Alumno
--GO
--DROP TABLE dbo.Usuario
--GO
--DROP TABLE dbo.UnidadAprendizaje
--GO
--DROP TABLE dbo.Carrera
--GO
--DROP TABLE dbo.TipoTramite
--GO
--DROP TABLE dbo.AspNetUserClaims
--GO
--DROP TABLE dbo.AspNetUserTokens
--GO
--DROP TABLE dbo.AspNetUserLogins
--GO
--DROP TABLE dbo.AspNetRoleClaims
--GO
--DROP TABLE dbo.AspNetUserRoles
--GO
--DROP TABLE dbo.AspNetRoles
--GO
--DROP TABLE dbo.AspNetUsers
--GO


--INSERT INTO FechaRecepcion(SesionId, Fecha, EsInactivo)
--VALUES (1, '2025/11/11',0)

SELECT * FROM Tramite
SELECT * FROM TramiteCTCE
SELECT * FROM UnidadReprobada


SELECT T.ID, UA.Nombre, c.Nombre FROM Tramite AS T
INNER JOIN TramiteCTCE AS D ON T.Id = D.Id
INNER JOIN UnidadReprobada AS UR ON D.ID = UR.TramiteCTCEId
INNER JOIN UnidadAprendizaje AS UA ON UR.UnidadAprendizajeId = UA.Id
INNER JOIN Carrera AS C ON UA.CarreraId = C.Id

