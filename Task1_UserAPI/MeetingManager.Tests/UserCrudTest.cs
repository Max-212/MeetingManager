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
    public class UserCrudTest
    {
        private IMapper mapper;

        private List<User> users;

        private IUserRepository mockUserRepository;

        private IUserService userService;

        public UserCrudTest()
        {
            //TestData
            users = new List<User>()
            {
                new User { Id = 1, Email = "Test1@mail.com", FirstName = "Test1", LastName = "Test1", Password = "HashPassword1"},
                new User { Id = 2, Email = "Test2@mail.com", FirstName = "Test2", LastName = "Test2", Password = "HashPassword2"},
                new User { Id = 3, Email = "Test3@mail.com", FirstName = "Test3", LastName = "Test3", Password = "HashPassword3"},
                new User { Id = 4, Email = "Test4@mail.com", FirstName = "Test4", LastName = "Test4", Password = "HashPassword4"}
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
        public async Task CanReturnAllUsers()
        { 
            var usersModels = await userService.GetAllAsync();
            Assert.AreEqual(4, usersModels.Count);
        }

        [TestMethod]
        public async Task CanReturnUserById()
        {
            var userModel = await userService.GetOneAsync(3);
            Assert.IsNotNull(userModel);
            Assert.IsInstanceOfType(userModel, typeof(UserModel));
            Assert.AreEqual(3, userModel.Id);
        }

        [TestMethod]
        public async Task CanNotReturnUserByNotExistsId()
        {
            var userModel = await userService.GetOneAsync(7);
            Assert.IsNull(userModel);
        }

        [TestMethod]
        public async Task CanInsertUser()
        {
            var userData = new UserModel() { Email = "Test5@mail.com", FirstName = "Test5", LastName = "Test5", Password = "123456789" };
            var userModel = await userService.CreateAsync(userData);
            Assert.AreEqual(5, users.Count);
            Assert.AreEqual(userData.FirstName, users[4].FirstName);
            Assert.AreNotEqual("123456789", users[4].Password);
        }

        [TestMethod]
        public async Task CanDeleteUser()
        {
            await userService.DeleteAsync(3);
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public async Task CanNotDeleteUserByNotExistingId()
        {
            await userService.DeleteAsync(7);
            Assert.AreEqual(4, users.Count);
        }

        [TestMethod]
        public async Task CanUpdateUser()
        {
            var userData = new UserModel() { Id = 3, Email = "UpdateTest3@mail.com", FirstName = "UpdateTest3", LastName = "UpdateTest3", Password = "Update123456789" };
            var userModel = await userService.UpdateAsync(userData);
            Assert.IsNotNull(userModel);
            Assert.AreEqual(userData.Email, users[2].Email);
        }

        [TestMethod]
        public async Task CanNotUpdateUserByNotExistingId()
        {
            var userData = new UserModel() { Id = 7, Email = "UpdateTest7@mail.com", FirstName = "UpdateTest7", LastName = "UpdateTest7", Password = "Update123456789" };
            var userModel = await userService.UpdateAsync(userData);
            Assert.IsNull(userModel);
        }

        private void SetupMock(Mock<IUserRepository> mockRepository)
        {
            mockRepository.Setup(mr => mr.GetAllAsync())
                .Returns(Task.Run(() => users));

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
                    var user = users.FirstOrDefault(u => u.Id == id);
                    if (user != null)
                    {
                        users.Remove(user);
                    }
                }));

            mockRepository.Setup(mr => mr.UpdateAsync(It.IsAny<User>()))
                .Returns((User userData) => Task.Run(() =>
                {
                    var user = users.FirstOrDefault(u => u.Id == userData.Id);
                    if(user == null)
                    {
                        return null;
                    }
                    user.Email = userData.Email;
                    user.FirstName = userData.FirstName;
                    user.LastName = userData.LastName;
                    user.Password = userData.Password;
                    return user;
                }));
        }
    }
}
