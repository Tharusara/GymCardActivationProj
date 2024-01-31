using Microsoft.AspNetCore.Mvc;
using VirtuagyDevTaskUnitTests.Services;
using VirtuagymDevTask.Controllers;
using VirtuagymDevTask.Services;
using Xunit;

namespace VirtuagyDevTaskUnitTests
{
    public class CheckInControllerTest
    {
        CheckinController _controller;
        ICheckInService _repo;

        public CheckInControllerTest()
        {
            _repo = new CheckInServiceFake();
            _controller = new CheckinController(_repo);
        }

        [Fact]
        public void CheckIn_WhenCall_Return_BadRequest_with_Membership_Cancel_Message()
        {
            //Act
            var result = _controller.UserChekin(1);

            var message = Assert.IsType<BadRequestObjectResult>(result.Result);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("{ message = Membership Canceled! you can't checkin! }", message.Value.ToString());
        }

        [Fact]
        public void CheckIn_WhenCall_Return_BadRequest_with_NoMembership_Message()
        {
            //Act
            var result = _controller.UserChekin(4);

            var message = Assert.IsType<BadRequestObjectResult>(result.Result);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("{ message = User doesn't have a membership to checkin }", message.Value.ToString());
        }

        [Fact]
        public void CheckIn_WhenCall_Return_BadRequest_with_Membership_Insufficient_Credits_Message()
        {
            //Act
            var result = _controller.UserChekin(2);

            var message = Assert.IsType<BadRequestObjectResult>(result.Result);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("{ message = Insufficient credits! you can't checkin! }", message.Value.ToString());
        }

        [Fact]
        public void CheckIn_WhenCall_Return_BadRequest_with_InvoiceLine_Creation_Error_Message()
        {
            //Act
            var result = _controller.UserChekin(3);

            var message = Assert.IsType<BadRequestObjectResult>(result.Result);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("{ message = Something went wrong when adding Invoice lines }", message.Value.ToString());
        }

        [Fact]
        public void CheckIn_WhenCall_Return_User_NotFound()
        {
            //Act
            var result = _controller.UserChekin(0);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CheckIn_WhenCall_Return_OkResult()
        {
            //Act
            var result = _controller.UserChekin(6);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
