using NUnit.Framework;
using SpiderCoordinates.Controllers;
using SpiderCoordinates.Models;
using SpiderCoordinatesTest.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace SpiderCoordinatesTest
{
    [TestFixture]
    public class SpiderCoordinatesTest
    {
        private readonly Mock<ILogger<HomeController>> mockHomeControllerLogger;
        private readonly SpiderCoordinatesViewModel _validRequest = new SpiderCoordinatesViewModel()
        {
            MatrixCoordinates = "4,5",
            StartingPoint = "3,3,L",
            Direction = "FFLR"

        };
        private readonly SpiderCoordinatesViewModel _InvalidDirectionRequest = new SpiderCoordinatesViewModel()
        {
            MatrixCoordinates = "5,5",
            StartingPoint = "3,3,L",
            Direction = "ABC"

        };
        private readonly SpiderCoordinatesViewModel _InvalidRequest = new SpiderCoordinatesViewModel()
        {
            MatrixCoordinates = "5,5",
            StartingPoint = "a,a,L",
            Direction = "ABC"

        };
        private readonly SpiderCoordinatesViewModel _PathOutofMatrixRequest = new SpiderCoordinatesViewModel()
        {
            MatrixCoordinates = "5,5",
            StartingPoint = "5,5,L",
            Direction = "FFLLRRR"

        };
        public SpiderCoordinatesTest()
        {
            mockHomeControllerLogger = new Mock<ILogger<HomeController>>();
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Description(
          @" Given request has valid  inputs")]
        public void CheckValidRequest()
        {
            // Arrange
            HomeController controller = new HomeController(
                mockHomeControllerLogger.Object
                );
            var finalCoordinates = controller.GetSpiderCoordinatesValue(_validRequest);
            Assert.IsNotNull(finalCoordinates);
            Assert.AreEqual("3,5,up", finalCoordinates);

        }
        [Test]
        [Description(
          @" Given request has invalid Direction inputs")]
        public  void CheckInValidDirectionRequest()
        {
            // Arrange
            HomeController controller = new HomeController(
                mockHomeControllerLogger.Object
                );
            string finalCoordinates =   controller.GetSpiderCoordinatesValue(_InvalidDirectionRequest);
            // Assert
        
            Assert.AreEqual("3,3,up", finalCoordinates);
        }
        [Test]
        [Description(
          @" Given request has invalid inputs")]
        public void CheckInValidRequest()
        {
            // Arrange
            HomeController controller = new HomeController(
                mockHomeControllerLogger.Object
                );
            string finalCoordinates = controller.GetSpiderCoordinatesValue(_InvalidRequest);
            // Assert

            Assert.IsNull(finalCoordinates);
        }

        [Test]
        [Description(
          @" Given request has values with Path Out of Matrix")]
        public void CheckPathOutofMatrixRequest()
        {
            // Arrange
            HomeController controller = new HomeController(
                mockHomeControllerLogger.Object
                );
            string finalCoordinates = controller.GetSpiderCoordinatesValue(_PathOutofMatrixRequest);
            // Assert

            Assert.AreEqual(finalCoordinates,"Path Out of Matrix");
        }

        [Test]
        [Description(
          @" Given values for the input test the validations")]
        public void verifyclassattributes()
        {
            CheckModelValidations cmv = new CheckModelValidations();
            var spider = new SpiderCoordinatesViewModel
            {
                MatrixCoordinates = "5,5",
                StartingPoint = "3,3,L",
                // Direction = "LLR"
                Direction = "LLR"

            };
            var errorcount = cmv.myValidation(spider).Count();
            Assert.AreEqual(0, errorcount);


        }
    }
}