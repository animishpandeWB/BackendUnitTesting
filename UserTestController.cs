using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using EF_Tutorial.Interface;
using EF_Tutorial.Models;
using EF_Tutorial.Controllers;
using EF_Tutorial.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackendUnitTesting
{
    public class UserTestController
    {

        private readonly Mock<IUser> userInterface;
        public UserTestController()
        {
            userInterface = new Mock<IUser>();
        }

        [Fact]
        public void GetUsers_UserList()
        {
            //arrange
            var userList = UserDataList();
            userInterface.Setup(x => x.GetUsers())
                .Returns(userList);
            
            var userController = new UserController(userInterface.Object);

            //act
            var userResult = userController.GetUsers();
            var okResult = userResult as OkObjectResult;

            //assert
            Assert.NotNull(userResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(userList, okResult.Value);
        }

        [Fact]
        public void GetUserById_User()
        {
            //arrange
            var userList = UserDataList();
            userInterface.Setup(x => x.GetUserById(0))
                .Returns(userList[0]);
            
            var userController = new UserController(userInterface.Object);
            //act
            var userIdResult = userController.GetUsersById(0);
            var okResult = userIdResult as OkObjectResult;

            //assert
            Assert.NotNull(userIdResult);
            // Console.WriteLine(userIdResult);
            // Assert.NotNull(okResult.Value);
            // Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void CreateUser_User()
        {
            //arrange
            var userList = UserDataList();
            userInterface.Setup(x => x.CreateUser(userList[0]))
                .Returns(true);

            var userController = new UserController(userInterface.Object);

            //act
            var userCreateResult = userController.CreateUser(userList[0]);
            var okResult = userCreateResult as OkObjectResult;
            //assert
            Assert.NotNull(userCreateResult);
            Assert.NotNull(okResult);
        }
        

        private List<User> UserDataList()
        {
            List<User> userData = new List<User>
            {
                new User 
                {
                    Id = 1,
                    UserId = 1,
                    Username = "test1",
                    Email = "test1@123.com",
                    Password = "test1",
                    Pumps = null
                },
                new User 
                {
                    Id = 2,
                    UserId = 2,
                    Username = "test2",
                    Email = "test2@123.com",
                    Password = "test2",
                    Pumps = null
                }
            };
            return userData;
        }
    }
}