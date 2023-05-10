// Use these namespace statements at the top of your test file

using FakeItEasy;
using CryptoCurrency.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using CryptoCurrency.Models;
using CryptoCurrency.Dto;
using Microsoft.AspNetCore.Mvc;
using CryptoCurrency.Controllers;
using Moq;

namespace CryptoCurrencyApp.Tests.Controllers
{
    // The name of your test class should be the name of your controller followed by "Tests"
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUser> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        // Arrange: Create mock objects for IUser and IMapper
        public UserControllerTests()
        {
            // Create a new instance of each mock object for each test method
            _userServiceMock = new Mock<IUser>();
            _mapperMock = new Mock<IMapper>();
            // Create a new instance of your controller using the mock objects
            _controller = new UserController(_userServiceMock.Object, _mapperMock.Object);
        }

        [Fact]//This attribute indicates that this is a test method
        public void Get_ReturnsOkResult_WithListOfUserDtos()
        {
            // Arrange: Create a list of users to return from the IUser mock object
            var users = new List<User> { new User { Id = 1,FirstName= "John",LastName="Olsen",Email="OJ@gmail.com",Password="hghfgjdfg9@" } };
            _userServiceMock.Setup(u => u.GetUsers()).Returns(users);
            // Arrange: Create a list of user DTOs to return from the IMapper mock object
            var userDtos = new List<UserDto> { new UserDto { Id = 1, FirstName = "John", LastName = "Olsen", Email = "OJ@gmail.com", Password = "hghfgjdfg9@" } };
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);


            // Act: Call the Get method on the UserController
            var result = _controller.Get();

            // Assert: Check that the result is an OkObjectResult with the expected list of user DTOs
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUserDtos = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(userDtos.Count, returnedUserDtos.Count());
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;
            _userServiceMock.Setup(u => u.GetUser(invalidId)).Returns(null as User);

            // Act
            var result = _controller.Get(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_WithValidUserDto_CreatesNewUser()
        {
            // Arrange
            var userDto = new UserDto { FirstName = "John" };
            var user = new User { FirstName = "John" };
            _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);

            // Act
            var result = _controller.Post(userDto);

            // Assert
            _userServiceMock.Verify(u => u.CreateUser(user), Moq.Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Put_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;
            var userDto = new UserDto { FirstName= "John" };
            _userServiceMock.Setup(u => u.GetUser(invalidId)).Returns(null as User);

            // Act
            var result = _controller.Put(invalidId, userDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;
            _userServiceMock.Setup(u => u.GetUser(invalidId)).Returns(null as User);

            // Act
            var result = _controller.Delete(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe" },
            new User { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
            var userDtos = new List<UserDto>
        {
            new UserDto { Id = 1, FirstName = "John", LastName = "Doe" },
            new UserDto { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
            _userServiceMock.Setup(x => x.GetUsers()).Returns(users);
            _mapperMock.Setup(x => x.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserDtos = Assert.IsType<List<UserDto>>(okResult.Value);
            Assert.Equal(userDtos, actualUserDtos);
        }
    }
}






