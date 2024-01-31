using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VirtuagyDevTaskUnitTests.Services;
using VirtuagymDevTask.Controllers;
using VirtuagymDevTask.Data;
using VirtuagymDevTask.Models;
using Xunit;

namespace VirtuagyDevTaskUnitTests
{
    public class UserControllerTest
    {
        UserController _controller;
        IGymRepository _repo;

        public UserControllerTest()
        {
            _repo = new GymRepositoryFake();
            _controller = new UserController(_repo);
        }

        [Fact]
        public void GetAll_WhenCall_ReturnAllUsers()
        {
            //Act
            var okResult = _controller.GetAll().Result as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnOkResult()
        {
            //Act
            var items = _controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(items.Result);
        }

        [Fact]
        public void GetById_WhenCall_ReturnNotFound()
        {
            //Act
            var item = _controller.Get(6);

            //Assert
            Assert.IsType<NotFoundResult>(item.Result);
        }
    }
}
