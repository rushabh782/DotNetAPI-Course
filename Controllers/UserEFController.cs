
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IUserRepository _userRepository;
    public UserEFController(IConfiguration config,IUserRepository userRepository)
    {
        _entityFramework = new DataContextEF(config);
        _userRepository = userRepository;
    }



    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;   
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _userRepository.GetSingleUser(userId);
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = new User();       

          userDb.FirstName = user.FirstName; 
          userDb.LastName = user.LastName; 
          userDb.Email = user.Email; 
          userDb.Gender = user.Gender; 
          userDb.Active = user.Active; 
          _userRepository.AddEntity<User>(userDb);
          
          if(_userRepository.SaveChanges())
            {
                return Ok();
            }
        throw new Exception("Failed to add user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _userRepository.GetSingleUser(user.UserId);

        if(userDb != null)
        {
          userDb.FirstName = user.FirstName; 
          userDb.LastName = user.LastName; 
          userDb.Email = user.Email; 
          userDb.Gender = user.Gender; 
          userDb.Active = user.Active; 
          
          if(_userRepository.SaveChanges())
            {
                return Ok();
            }
        throw new Exception("Failed to update user");
        }
        throw new Exception("User not found");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
       User? userDb = _userRepository.GetSingleUser(userId);

        if(userDb != null)
        {
            _userRepository.RemoveEntity<User>(userDb);

          if(_userRepository.SaveChanges())
            {
                return Ok();
            }
        throw new Exception("Failed to delete user");
        }
        throw new Exception("User not found");
    }
}