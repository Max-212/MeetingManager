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
                cfg.CreateMap<MeetingModel, Meeting>();
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

            mock.Setup(m => m.CreateAsync(It.IsAny<MeetingModel>()))
                .Returns((MeetingModel meeting) => Task.Run(() => meeting));

            mock.Setup(m => m.UpdateAsync(It.IsAny<MeetingModel>()))
                .Returns((MeetingModel meetingModel) => Task.Run(() =>
                {
                    if(GetMeeting(meetingModel.Id) != null)
                    {
                        return meetingModel;
                    }
                    return null;
                }));

            mock.Setup(m => m.AddPartitipantAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int meetingId, int userId) => Task.Run(() =>
                {
                    var meeting = GetMeeting(meetingId);
                    if(meeting != null)
                    {
                        return mapper.Map<MeetingModel>(meeting);
                    }
                    return null;
                }));

            mock.Setup(m => m.RemovePartitipantAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int meetingId, int userId) => Task.Run(() =>
                {
                    var meeting = GetMeeting(meetingId);
                    if (meeting != null)
                    {
                        return mapper.Map<MeetingModel>(meeting);
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
            var meetingModel = new MeetingModel() { Description = "Testqwe", From = new DateTime(2022, 3, 15, 17, 0, 0), Till = new DateTime(2022, 3, 15, 18, 0, 0) };
            var actionResult = await meetingController.CreateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateMeeting_ReturnsOk()
        {
            var meetingModel = new MeetingModel() { Id = 1, Description = "Testqwe", From = new DateTime(2022, 3, 15, 17, 0, 0), Till = new DateTime(2022, 3, 15, 18, 0, 0) };
            var actionResult = await meetingController.UpdateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateMeeting_ReturnsNotFound()
        {
            var meetingModel = new MeetingModel() { Id = 7, Description = "Testqwe", From = new DateTime(2022, 3, 15, 17, 0, 0), Till = new DateTime(2022, 3, 15, 18, 0, 0) };
            var actionResult = await meetingController.UpdateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateMeeting_ReturnsBadRequest()
        {
            var meetingModel = new MeetingModel() {Description = "Testqwe", From = new DateTime(2022, 3, 15, 17, 0, 0), Till = new DateTime(2022, 3, 15, 18, 0, 0) };
            var actionResult = await meetingController.UpdateMeeting(meetingModel);
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddPartitipant_ReturnsOk()
        {
            var actionResult = await meetingController.AddPartitipant(2, 15);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AddPartitipant_ReturnsNotFound()
        {
            var actionResult = await meetingController.AddPartitipant(8, 15);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RemovePartitipant_ReturnsOk()
        {
            var actionResult = await meetingController.RemovePartitipant(2, 15);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RemovePartitipant_ReturnsNotFound()
        {
            var actionResult = await meetingController.RemovePartitipant(8, 15);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}
