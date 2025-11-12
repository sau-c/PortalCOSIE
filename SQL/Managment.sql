use PortalCOSIE
--Identity
select * from AspNetUsers
select * from AspNetRoles
select * from AspNetUserRoles
--select * from AspNetRoleClaims
--select * from AspNetUserClaims
--select * from AspNetUserLogins
--select * from AspNetUserTokens

--Dominio
select * from Carrera
select * from EstadoTramite
select * from TipoTramite
select * from EstadoDocumento
select * from Tramite
select * from Personal
select * from Alumno
select * from Usuario
select * from Tramite
select * from PeriodoConfig
select * from SesionCOSIE
select * from FechaRecepcion

--Modulo autenticacion
select * from Alumno
select * from Usuario
select * from AspNetUsers


DELETE FROM Alumno
DELETE FROM Usuario
DELETE FROM AspNetUsers WHERE UserName LIKE '%sau%'
--DELETE FROM AspNetUsers WHERE UserName = 'scastanedaz1900@alumno.ipn.mx'

--DELETE FROM AspNetUsers
--DELETE FROM AspNetRoles
--DELETE FROM AspNetUserRoles
--DELETE FROM Usuario 
--DELETE FROM Alumno

--UPDATE AspNetUsers
--SET UserName = 'hola'
--WHERE Email = 'saulcaz01@gmail.com'

UPDATE PeriodoConfig
SET AnioFin = 2026
WHERE Email = 'saulcaz01@gmail.com'


--%%%%%%%%%%%%%%%%%%%%%%%%
--%%%% ELIMINAR TABLAS %%%
--%%%%%%%%%%%%%%%%%%%%%%%%
--DROP TABLE dbo.Documento
--GO
--DROP TABLE dbo.EstadoDocumento
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



--SELECT 'sqlserver' dbms,t.TABLE_CATALOG,t.TABLE_SCHEMA,t.TABLE_NAME,c.COLUMN_NAME,c.ORDINAL_POSITION,c.DATA_TYPE,c.CHARACTER_MAXIMUM_LENGTH,n.CONSTRAINT_TYPE,k2.TABLE_SCHEMA,k2.TABLE_NAME,k2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLES t LEFT JOIN INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_CATALOG=c.TABLE_CATALOG AND t.TABLE_SCHEMA=c.TABLE_SCHEMA AND t.TABLE_NAME=c.TABLE_NAME LEFT JOIN(INFORMATION_SCHEMA.KEY_COLUMN_USAGE k JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS n ON k.CONSTRAINT_CATALOG=n.CONSTRAINT_CATALOG AND k.CONSTRAINT_SCHEMA=n.CONSTRAINT_SCHEMA AND k.CONSTRAINT_NAME=n.CONSTRAINT_NAME LEFT JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON k.CONSTRAINT_CATALOG=r.CONSTRAINT_CATALOG AND k.CONSTRAINT_SCHEMA=r.CONSTRAINT_SCHEMA AND k.CONSTRAINT_NAME=r.CONSTRAINT_NAME)ON c.TABLE_CATALOG=k.TABLE_CATALOG AND c.TABLE_SCHEMA=k.TABLE_SCHEMA AND c.TABLE_NAME=k.TABLE_NAME AND c.COLUMN_NAME=k.COLUMN_NAME LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k2 ON k.ORDINAL_POSITION=k2.ORDINAL_POSITION AND r.UNIQUE_CONSTRAINT_CATALOG=k2.CONSTRAINT_CATALOG AND r.UNIQUE_CONSTRAINT_SCHEMA=k2.CONSTRAINT_SCHEMA AND r.UNIQUE_CONSTRAINT_NAME=k2.CONSTRAINT_NAME WHERE t.TABLE_TYPE='BASE TABLE';
