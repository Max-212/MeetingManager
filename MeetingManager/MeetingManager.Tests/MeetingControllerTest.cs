using AutoMapper;
using MeetingManager.API.Controllers;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace MeetingManager.Tests
{
    [TestClass]
    public class MeetingControllerTest
    {
        private IMapper mapper;

        private IMeetingService mockMeetingService;

        private MeetingController meetingController;

        private List<Meeting> meetings;

        private List<User> users;

        [TestInitialize]
        public void TestInitialize()
        {
            initializeMeetings();
            initializeMapper();
            Mock<IMeetingService> mockService = new Mock<IMeetingService>();
            setupMock(mockService);
            mockMeetingService = mockService.Object;
            meetingController = new MeetingController(mockMeetingService);
        }

        private void initializeMeetings()
        {
            meetings = new List<Meeting>()
            {
                new Meeting {Id = 1, Description = "Test1", Till = new DateTime(2022,3,15,17,0,0), From = new DateTime(2022,3,15,16,30,0)},
                new Meeting {Id = 2, Description = "Test2", Till = new DateTime(2022,3,15,17,0,0), From = new DateTime(2022,3,15,16,30,0)},
                new Meeting {Id = 3, Description = "Test3", Till = new DateTime(2022,3,15,17,0,0), From = new DateTime(2022,3,15,16,30,0)}
            };
        }

        private void initializeMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Meeting, MeetingModel>();
                cfg.CreateMap<MeetingRequestModel, Meeting>();
            });
            mapper = new Mapper(config);
        }

        private void setupMock(Mock<IMeetingService> mock)
        {
            mock.Setup(m => m.GetAllAsync())
                .Returns(Task.Run(() => mapper.Map<List<MeetingModel>>(meetings)));

            mock.Setup(m => m.GetOneAsync(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() =>
                mapper.Map<MeetingModel>(GetMeeting(id))));

            mock.Setup(m => m.CreateAsync(It.IsAny<MeetingRequestModel>()))
                .Returns((MeetingRequestModel meeting) => Task.Run(() => new MeetingModel()));

            mock.Setup(m => m.UpdateAsync(It.IsAny<MeetingRequestModel>()))
                .Returns((MeetingRequestModel meetingModel) => Task.Run(() =>
                {
                    if(GetMeeting(meetingModel.Id) != null)
                    {
                        return new MeetingModel();
                    }
                    return null;
                }));
        }

        private Meeting GetMeeting(int id)
        {
            return meetings.FirstOrDefault(m => m.Id == id);
        }

        [TestMethod]
        public async Task GetAllMeetings_ReturnsOk()
        {
            var actionResult = await meetingController.GetAllMeetings();
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMeetingById_ReturnsOk()
        {
            var actionResult = await meetingController.GetMeeting(2);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMeetingById_ReturnsNotFound()
        {
            var actionResult = await meetingController.GetMeeting(7);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddMeeting_ReturnsOk()
        {
            var meetingModel = new MeetingRequestModel() { Description = "Testqwe",
                From = new DateTime(2022, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 18, 0, 0),
                Participants = new List<int>()
            };
            var actionResult = await meetingController.CreateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateMeeting_ReturnsOk()
        {
            var meetingModel = new MeetingRequestModel() { Id = 1, 
                Description = "Testqwe",
                From = new DateTime(2022, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 18, 0, 0),
                Participants = new List<int>() 
            };
            var actionResult = await meetingController.UpdateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateMeeting_ReturnsNotFound()
        {
            var meetingModel = new MeetingRequestModel()
            {
                Id = 7,
                Description = "Testqwe",
                From = new DateTime(2022, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 18, 0, 0),
                Participants = new List<int>()
            };
            var actionResult = await meetingController.UpdateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateMeeting_ReturnsBadRequest()
        {
            var meetingModel = new MeetingRequestModel()
            {
                Description = "Testqwe",
                From = new DateTime(2022, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 18, 0, 0),
                Participants = new List<int>()
            };
            var actionResult = await meetingController.UpdateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

    }
}
