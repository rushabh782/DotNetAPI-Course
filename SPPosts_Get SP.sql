CREATE OR ALTER PROCEDURE TutorialAppSchema.spPosts_Get
@UserId INT = NULL,
@SearchValue NVARCHAR(MAX) = NULL,
@PostId INT = NULL
AS
BEGIN 
   Select * from TutorialAppSchema.Posts
   WHERE Posts.UserId = ISNULL(@UserId,Posts.UserId)
       AND Posts.PostId = ISNULL(@PostId,Posts.PostId)
       AND (@SearchValue IS NULL
           OR Posts.PostContent LIKE '%' +@SearchValue + '%'
           OR Posts.PostTitle LIKE '%' +@SearchValue + '%')
END
GO

EXEC TutorialAppSchema.spPosts_Get @UserId =  21,@searchValue = 'Post' ; 
GO

EXEC TutorialAppSchema.spPosts_Get @UserId =  21,@searchValue = 'Post',@PostId = 2 ; 
GO

EXEC TutorialAppSchema.spPosts_Get  @PostId = 3 ; 
GO