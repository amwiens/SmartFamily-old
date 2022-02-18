using Moq;

using SmartFamily.Core;

using System;
using System.Collections.Generic;

using Xunit;

namespace SmartFamily.Data.Tests
{
    public class GEDCOMIndividualRepositoryTests
    {
        [Fact]
        public void Constructor_Throws_On_Null_Database()
        {
            // Arrange
            IGEDCOMStore database = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new GEDCOMIndividualRepository(database));
        }

        [Fact]
        public void Add_Throws_On_Null_Individual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMIndividualRepository(mockStore.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => rep.Add(null));
        }

        [Fact]
        public void Add_Calls_Store_AddIndividual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMIndividualRepository(mockStore.Object);
            var individual = new Individual();

            // Act
            rep.Add(individual);

            // Assert
            mockStore.Verify(s => s.AddIndividual(individual));
        }

        [Fact]
        public void Delete_Throws_On_Null_Individual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMIndividualRepository(mockStore.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => rep.Delete(null));
        }

        [Fact]
        public void Delete_Calls_Store_DeleteIndividual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMIndividualRepository(mockStore.Object);
            var individual = new Individual();

            // Act
            rep.Delete(individual);

            // Assert
            mockStore.Verify(s => s.DeleteIndividual(individual));
        }

        [Fact]
        public void GetAll_Calls_Store_Individuals()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            mockStore.Setup(s => s.Individuals).Returns(() => new List<Individual>());
            var rep = new GEDCOMIndividualRepository(mockStore.Object);

            // Act
            var individuals = rep.GetAll();

            // Assert
            mockStore.Verify(s => s.Individuals);
        }

        [Fact]
        public void Update_Throws_On_Null_Individual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMIndividualRepository(mockStore.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => rep.Update(null));
        }

        [Fact]
        public void Update_Calls_Store_UpdateIndividual()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMIndividualRepository(mockStore.Object);
            var individual = new Individual();

            // Act
            rep.Update(individual);

            // Assert
            mockStore.Verify(s => s.UpdateIndividual(individual));
        }
    }
}