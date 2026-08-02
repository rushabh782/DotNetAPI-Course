SELECT TOP (1000) [UserId]
      ,[JobTitle]
      ,[Department]
  FROM [DotNetCourseDatabase].[TutorialAppSchema].[UserJobInfo]


  INSERT INTO [DotNetCourseDatabase].[TutorialAppSchema].[UserJobInfo]
    ([UserId], [JobTitle], [Department])
VALUES
    (1, 'Software Engineer', 'Engineering'),
    (2, 'Senior Software Engineer', 'Engineering'),
    (3, 'QA Engineer', 'Quality Assurance'),
    (4, 'Business Analyst', 'Business Analysis'),
    (5, 'Project Manager', 'Project Management'),
    (6, 'UI/UX Designer', 'Design'),
    (7, 'DevOps Engineer', 'Infrastructure'),
    (8, 'Database Administrator', 'Database'),
    (9, 'HR Executive', 'Human Resources'),
    (10, 'Technical Support Engineer', 'Customer Support');