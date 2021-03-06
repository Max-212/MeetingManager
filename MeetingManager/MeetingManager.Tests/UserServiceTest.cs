using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using MeetingManager.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private IMapper mapper;

        private List<User> users;

        private IUserRepository mockUserRepository;

        private IUserService userService;


        [TestInitialize]
        public void TestInitialize()
        {
            //TestData
            users = new List<User>()
            {
                new User { Id = 1, Email = "Test1@mail.com", FirstName = "Test1", LastName = "Test1"},
                new User { Id = 2, Email = "Test2@mail.com", FirstName = "Test2", LastName = "Test2"},
                new User { Id = 3, Email = "Test3@mail.com", FirstName = "Test3", LastName = "Test3"},
                new User { Id = 4, Email = "Test4@mail.com", FirstName = "Test4", LastName = "Test4"}
            };

            //MockRepo
            Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
            SetupMock(mockRepository);
            mockUserRepository = mockRepository.Object;

            //Mapper
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();
            });
            mapper = new Mapper(config);

            //Service
            userService = new UserService(mockUserRepository, mapper);
        }

        [TestMethod]
        public async Task GetById_ReturnsUser()
        {
            var userModel = await userService.GetOneAsync(3);
            Assert.IsNotNull(userModel);
            Assert.IsInstanceOfType(userModel, typeof(UserModel));
            Assert.AreEqual(3, userModel.Id);
        }

        [TestMethod]
        public async Task GetByNotExistingId_ReturnsNull()
        {
            var userModel = await userService.GetOneAsync(7);
            Assert.IsNull(userModel);
        }

        [TestMethod]
        public async Task InsertUser_ReturnsUser()
        {
            var userData = new UserModel() { Email = "Test5@mail.com", FirstName = "Test5", LastName = "Test5"};
            var userModel = await userService.CreateAsync(userData);
            Assert.AreEqual(5, users.Count);
            Assert.AreEqual(userData.FirstName, users[4].FirstName);
        }

        [TestMethod]
        public async Task DeleteUser()
        {
            await userService.DeleteAsync(3);
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public async Task DeleteUserByNotExistingId()
        {
            await userService.DeleteAsync(7);
            Assert.AreEqual(4, users.Count);
        }

        [TestMethod]
        public async Task UpdateUser_ReturnsUser()
        {
            var userData = new UserModel() { Id = 3, Email = "UpdateTest3@mail.com", FirstName = "UpdateTest3", LastName = "UpdateTest3"};
            var userModel = await userService.UpdateAsync(userData);
            Assert.IsNotNull(userModel);
            Assert.AreEqual(userData.Email, users[2].Email);
        }

        [TestMethod]
        public async Task UpdateUserByNotExistingId_ReturnsNull()
        {
            var userData = new UserModel() { Id = 7, Email = "UpdateTest7@mail.com", FirstName = "UpdateTest7", LastName = "UpdateTest7"};
            var userModel = await userService.UpdateAsync(userData);
            Assert.IsNull(userModel);
        }

        private User GetUser(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        private void SetupMock(Mock<IUserRepository> mockRepository)
        {
            mockRepository.Setup(mr => mr.GetOneAsync(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => users.FirstOrDefault(u => u.Id == id)));

            mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<User>()))
                .Returns((User userData) => Task.Run(() =>
                {
                    userData.Id = users.Count;
                    users.Add(userData);
                    return userData;
                }));

            mockRepository.Setup(mr => mr.DeleteAsync(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() =>
                {
                    var user = GetUser(id);
                    if (user != null)
                    {
                        users.Remove(user);
                    }
                }));

            mockRepository.Setup(mr => mr.UpdateAsync(It.IsAny<User>()))
                .Returns((User userData) => Task.Run(() =>
                {
                    var user = GetUser(userData.Id);
                    if(user == null)
                    {
                        return null;
                    }
                    user.Email = userData.Email;
                    user.FirstName = userData.FirstName;
                    user.LastName = userData.LastName;
                    return user;
                }));
        }
    }
}
