USE [SchoolTimetable]
GO
SET IDENTITY_INSERT [dbo].[BellSchedules] ON 
GO
INSERT [dbo].[BellSchedules] ([Id], [LessonNum], [StartTime], [EndTime]) VALUES (1, 1, CAST(N'08:00:00' AS Time), CAST(N'08:45:00' AS Time))
GO
SET IDENTITY_INSERT [dbo].[BellSchedules] OFF
GO
SET IDENTITY_INSERT [dbo].[SchoolYears] ON 
GO
INSERT [dbo].[SchoolYears] ([Id], [Name], [StartDate], [EndDate], [Active]) VALUES (1, N'2024/2025', CAST(N'2025-04-21T00:00:00.0000000' AS DateTime2), CAST(N'2025-06-01T00:00:00.0000000' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[SchoolYears] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Username], [Name], [Admin], [PasswordHash]) VALUES (1, N'user', N'User', 1, N'$2a$12$DovUadWb8bhKVcMIYXmmtebiwl725RD1P0WjsZ0JqFpgeDsK.tRj2')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 
GO
INSERT [dbo].[Classes] ([Id], [Name]) VALUES (1, N'12.b')
GO
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON 
GO
INSERT [dbo].[Subjects] ([Id], [Name]) VALUES (1, N'Matematika')
GO
SET IDENTITY_INSERT [dbo].[Subjects] OFF
GO
SET IDENTITY_INSERT [dbo].[TimetableLessons] ON 
GO
INSERT [dbo].[TimetableLessons] ([Id], [SubjectId], [TeacherId], [ClassId], [SchoolYearId], [DayNum], [StartDate], [EndDate], [LessonNum]) VALUES (1, 1, 1, 1, 1, 1, CAST(N'2025-04-20T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-22T00:00:00.0000000' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[TimetableLessons] OFF
GO
