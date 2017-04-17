SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] (Username, Password, CreationDate, Name, Email)
VALUES ('manager', 'manager', SYSDATETIME(), 'manager test', 'manager@email.com')
SET IDENTITY_INSERT [dbo].[Users] OFF
