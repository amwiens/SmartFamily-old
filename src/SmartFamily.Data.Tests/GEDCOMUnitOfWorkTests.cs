using Moq;

using SmarFamily.TestUtilities.Models;

using SmartFamily.Core;

using System;

using Xunit;

namespace SmartFamily.Data.Tests
{
    public class GEDCOMUnitOfWorkTests
    {
        [Fact]
        public void Constructor_Throws_On_Empty_Path()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new GEDCOMUnitOfWork(string.Empty));
        }

        [Fact]
        public void Constructor_Overload_Throws_On_Null_Database()
        {
            // Arrange
            IGEDCOMStore database = null;

            // Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GEDCOMUnitOfWork(database));
        }

        [Fact]
        public void Commit_Calls_Store_SaveChanges()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var unitOfWork = new GEDCOMUnitOfWork(mockStore.Object);

            // Act
            unitOfWork.Commit();

            // Assert
            mockStore.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetRepository_Throws_If_T_Not_Recognized()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var unitOfWork = new GEDCOMUnitOfWork(mockStore.Object);

            // Act, Assert
            Assert.Throws<NotImplementedException>(() => unitOfWork.GetRepository<Dog>());
        }

        [Fact]
        public void GetLinqRepository_Returns_IndividualRepository_If_T_Is_Individual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var unitOfWork = new GEDCOMUnitOfWork(mockStore.Object);

            // Act
            var rep = unitOfWork.GetRepository<Individual>();

            // Assert
            Assert.IsType<GEDCOMIndividualRepository>(rep);
        }

        [Fact]
        public void GetLinqRepository_Returns_FamilyRepository_If_T_Is_Family()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var unitOfWork = new GEDCOMUnitOfWork(mockStore.Object);

            // Act
            var rep = unitOfWork.GetRepository<Family>();

            // Assert
            Assert.IsType<GEDCOMFamilyRepository>(rep);
        }

        [Fact]
        public void GetRepository_Throws_If_T_Is_Neither_Family_Individual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var unitOfWork = new GEDCOMUnitOfWork(mockStore.Object);

            // Act, Assert
            Assert.Throws<NotImplementedException>(() => unitOfWork.GetRepository<Note>());
        }
    }
}