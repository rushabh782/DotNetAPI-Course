USE DotNetCourseDatabase
GO

ALTER PROCEDURE TutorialAppSchema.spUsers_Get
@UserId INT = NULL
AS
BEGIN
    SELECT  Users.UserId,
         Users.FirstName,
         Users.LastName,
         Users.Email,
         Users.Gender,
         Users.Active,
         UserSalary.Salary,
         UserJobInfo.Department,
         UserJobInfo.JobTitle 
         FROM TutorialAppSchema.Users AS Users
         LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
             ON UserSalary.UserId = Users.UserId
         LEFT JOIN TutorialAppSchema.UserJobInfo
             ON UserJobInfo.UserId = Users.UserId
         where Users.UserId = ISNULL(@UserId,Users.UserId)
END
GO

EXEC TutorialAppSchema.spUsers_Get  1
GO