
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    public UserCompleteController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }
    [HttpGet("test/{testValue}")]
    public string[] Test(string testValue)
    {
        string[] responseArray = new string[]{"test1","test2",testValue};
        return responseArray;
    }

    [HttpGet("GetUsers/{userId}/{isActive}")]
    public IEnumerable<UserComplete> GetUsers(int userId,bool isActive)
    {
        string sql = @"EXEC TutorialAppSchema.spUsers_Get";
        string parameters = "";
        if(userId != 0)
        {
            parameters += ", @UserId = " + userId.ToString();
        }
        if(isActive)
        {
            parameters += ", @Active = " + isActive.ToString();
        }

        sql += parameters.Substring(1);
        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);
        return users;   
    }

 

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.Users
            ([FirstName]
            ,[LastName]
            ,[Email]
            ,[Gender]
            ,[Active])
            VALUES(" +
                "'" + user.FirstName +
                "', '" + user.LastName + 
                "','" + user.Email +
                "','" + user.Gender +
                "','" + user.Active+
                "')";
       if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to add user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql =@"UPDATE TutorialAppSchema.Users
        SET  [FirstName]='" + user.FirstName +
         "', [LastName]='" + user.LastName + 
         "',[Email]='" + user.Email +
         "',[Gender]='" + user.Gender +
         "',[Active]='" + user.Active+
         "' WHERE UserId=" + user.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Fialed to update user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.Users where UserId=" +userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }


    [HttpPost("UserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary
            ([UserId]
            ,[Salary])
            VALUES(" +
                userSalary.UserId.ToString() +
                ", " + userSalary.Salary.ToString() +
                ")";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to add user salary");
    }

    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId=" +userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to delete User Salary");
    }

    [HttpPut("UserSalary")]
    public IActionResult PutUserSalary(UserSalary userSalary)
    {
        string sql = @"UPDATE TutorialAppSchema.UserSalary SET Salary="+userSalary.Salary+" WHERE UserId="+ userSalary.UserId;
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to update User Salary");
    }
}