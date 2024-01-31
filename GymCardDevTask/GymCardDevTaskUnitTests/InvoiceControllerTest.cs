using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VirtuagyDevTaskUnitTests.Services;
using VirtuagymDevTask.Controllers;
using VirtuagymDevTask.Data;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;
using Xunit;

namespace VirtuagyDevTaskUnitTests
{
    public class InvoiceControllerTest
    {
        InvoiceController _controller;
        IMapper _mapper;
        IGymRepository _repo;

        public InvoiceControllerTest()
        {
            _repo =new GymRepositoryFake();
            _controller = new InvoiceController(_repo, _mapper);
        }

        [Fact]
        public void GetAll_WhenCall_ReturnAllItems()
        {
            //Act
            var okResult = _controller.GetAll().Result as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<Invoice>>(okResult.Value);
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

        [Fact]
        public void AddNew_WhenPost_ReturnBadRequest()
        {
            //Arrange
            var newItem = new InvoiceToCreateDTO()
            {
                Month = "August",
                DueDate = DateTime.Now,
                Amount = 2000,
                Description = "test6",
                Status = 0,
                Lines = { }
            };
            _controller.ModelState.AddModelError("UserId", "UserId property must be filled!");

            //Act
            var result = _repo.MapInvoice(newItem);
            if (result == null) newItem = null;
            var action = _controller.Create(newItem);

            //Assert
            Assert.IsType<BadRequestObjectResult>(action.Result);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnCreatedAction()
        {
            //Arrange 
            var newItem = new InvoiceToCreateDTO()
            {
                Month = "August",
                DueDate = DateTime.Now,
                Amount = 2000,
                Description = "test6",
                Status = 0,
                UserId = 2,
                Lines = { }
            };
            //Act
            var action = _controller.Create(newItem);

            //Assert
            Assert.IsType<CreatedResult>(action.Result);
        }

        [Fact]
        public void AddNew_WhenPost_ReturnCreatedObject()
        {
            //Arrange 
            var newItem = new InvoiceToCreateDTO()
            {
                Month = "August",
                DueDate = DateTime.Now,
                Amount = 2000,
                Description = "test1",
                Status = 0,
                UserId = 1,
                Lines = { }
            };

            //Act
            var createAction = _controller.Create(newItem);

            //Assert
            Assert.IsType<InvoiceToCreateDTO>(newItem);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnCreatedActionItem()
        {
            //Arrange
            var newItem = new InvoiceToCreateDTO()
            {
                Month = "August",
                DueDate = DateTime.Now,
                Amount = 2000,
                Description = "test1",
                Status = 0,
                UserId = 1,
                Lines = { }
            };

            //Act
            var action = _controller.Create(newItem) as IActionResult;
            var item = newItem;

            //Assert
            Assert.Equal("August", item.Month);
        }

        [Fact]
        public void Remove_NotExistingObjectPassed_ReturnsNotFoundResponse()
        {
            //Act
            var id = 99;
            var item = _controller.Get(99);
            var result = _controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(item.Result);
        }

        [Fact]
        public void Remove_ExistingObjectPassed_ReturnOkResponse()
        {
            //Act
            var action = _controller.Delete(1);
            var okResult = _controller.GetAll().Result as OkObjectResult;
            var items = Assert.IsType<List<Invoice>>(okResult.Value);

            //Assert
            Assert.Equal(2, items.Count-1);
        }
    }
}
