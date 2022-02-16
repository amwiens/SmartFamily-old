using Moq;

using SmartFamily.Core;

using System;
using System.Collections.Generic;

using Xunit;

namespace SmartFamily.Data.Tests
{
    public class GEDCOMFamilyRepositoryTests
    {
        [Fact]
        public void Constructor_Throws_On_Null_Database()
        {
            // Arrange
            IGEDCOMStore database = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new GEDCOMFamilyRepository(database));
        }

        [Fact]
        public void Add_Throws_On_Null_Family()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMFamilyRepository(mockStore.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => rep.Add(null));
        }

        [Fact]
        public void Add_Calls_Store_AddFamily()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMFamilyRepository(mockStore.Object);
            var family = new Family();

            // Act
            rep.Add(family);

            // Assert
            mockStore.Verify(s => s.AddFamily(family));
        }

        [Fact]
        public void Delete_Throws_On_Null_Family()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMFamilyRepository(mockStore.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => rep.Delete(null));
        }

        [Fact]
        public void Delete_Calls_Store_DeleteFamily()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMFamilyRepository(mockStore.Object);
            var family = new Family();

            // Act
            rep.Delete(family);

            // Assert
            mockStore.Verify(s => s.DeleteFamily(family));
        }

        [Fact]
        public void GetAll_Calls_Store_Families()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            mockStore.Setup(s => s.Families).Returns(() => new List<Family>());
            var rep = new GEDCOMFamilyRepository(mockStore.Object);

            // Act
            var families = rep.GetAll();

            // Assert
            mockStore.Verify(s => s.Families);
        }

        [Fact]
        public void Update_Throws_On_Null_Family()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMFamilyRepository(mockStore.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => rep.Update(null));
        }

        [Fact]
        public void Update_Calls_Store_UpdateFamily()
        {
            // Arrange
            var mockStore = new Mock<IGEDCOMStore>();
            var rep = new GEDCOMFamilyRepository(mockStore.Object);
            var family = new Family();

            // Act
            rep.Update(family);

            // Assert
            mockStore.Verify(s => s.UpdateFamily(family));
        }
    }
}