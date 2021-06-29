using MeetingManager.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Tests
{
    [TestClass]
    public class MeetingModelValidationTests
    {
        [TestMethod]
        public void ValidateFromDate_ReturnsFalse()
        {
            var meetingModel = new MeetingRequestModel() { Description = "Testqwe",
                From = new DateTime(2020, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 18, 0, 0),
                Participants = new List<int>()
            };
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(meetingModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(meetingModel, context, results, true);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateTillDate_ReturnsFalse()
        {
            var meetingModel = new MeetingRequestModel() { Description = "Testqwe",
                From = new DateTime(2022, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 16, 0, 0),
                Participants = new List<int>()
            };
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(meetingModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(meetingModel, context, results, true);
            Assert.IsFalse(isValid);
        }
        
        [TestMethod]
        public void ValidateModel_ReturnsTrue()
        {
            var meetingModel = new MeetingRequestModel() { Description = "Testqwe",
                From = new DateTime(2022, 3, 15, 17, 0, 0),
                Till = new DateTime(2022, 3, 15, 18, 0, 0),
                Participants = new List<int>()
            };
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(meetingModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(meetingModel, context, results, true);
            Assert.IsTrue(isValid);
        }
    }
}
