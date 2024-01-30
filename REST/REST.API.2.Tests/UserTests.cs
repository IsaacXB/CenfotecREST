using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using REST.API._2.Controllers;
using REST.Database.Context;
using REST.Database.Models;
using REST.Database.Services;
using System.Text;

namespace REST.API._2.Tests
{
    public class UserTests
    {
        private readonly UserController _userController;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;


        public UserTests()
        {
            var dbOption = new DbContextOptionsBuilder()
                 .UseSqlServer("Data Source=ALIENWARER15\\SQLEXPRESS;Initial Catalog=cenfotec;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
                .Options;

            var userContext = new REST.Database.Context.UserContext(dbOption);

            _userService = new REST.Database.Services.UserService(userContext);
            _userController = new UserController(_userService);

        }

        [Fact]
        public void CanGetUsersIsOk()
        {
            var result = _userController.Index();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CanGetUsersData()
        {
            var result = (OkObjectResult) _userController.Index();
            var usersData = Assert.IsAssignableFrom<DbSet<User>>(result.Value);

            Assert.True(usersData.Any());
        }

        [Theory]
        [InlineData(1)]
        public async void CanGetUserByIdAsyncIsOk(int userId)
        {
            var result = await _userController.IndexAsync(userId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void CanValidateUserExistByIdAsyncIsOk(int userId)
        {
            var result = await _userController.IndexAsync(userId);
            var okObjectResult = result as OkObjectResult;

            var user = Assert.IsType<User>(okObjectResult?.Value);           
            Assert.True(user != null);
            Assert.Equal(user?.Id, userId);
        }

        [Theory]
        [InlineData(0)]
        public async void CanValidateUserNotFoundAsyncIsOk(int userId)
        {
            var result = await _userController.IndexAsync(userId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}