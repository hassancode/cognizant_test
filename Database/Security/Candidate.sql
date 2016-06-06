CREATE USER [DEV-TEST\candidate] FOR LOGIN [DEV-TEST\candidate];
GO
ALTER ROLE [db_datareader] ADD MEMBER [DEV-TEST\candidate];
GO
ALTER ROLE [db_datawriter] ADD MEMBER [DEV-TEST\candidate];
GO
ALTER ROLE [db_owner] ADD MEMBER [DEV-TEST\candidate];
GO