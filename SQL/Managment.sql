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
select * from PeriodoConfig

--Modulo autenticacion
select * from Alumno
select * from Usuario

select * from AspNetUsers


--DELETE FROM AspNetUsers
--DELETE FROM AspNetRoles
--DELETE FROM AspNetUserRoles
--DELETE FROM Usuario 
--DELETE FROM Alumno

--UPDATE AspNetUsers
--SET UserName = 'hola'
--WHERE Email = 'saulcaz01@gmail.com'

--UPDATE Alumno
--SET CarreraId = 2
--WHERE Email = 'saulcaz01@gmail.com'


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
