using Moq;

using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Services;

using System;

using Xunit;

namespace SmartFamily.DomainServices.Tests
{
    public class FamilyTreeServiceFactoryTests
    {
        private FamilyTreeServiceFactory _serviceFactory;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public FamilyTreeServiceFactoryTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void FamilyTreeServiceFactory_Constructor_Throws_If_UnitOfWork_Argument_Is_Null()
        {
            // Arrange

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new FamilyTreeServiceFactory(null));
        }

        [Fact]
        public void CreateFamilyService_Returns_FamilyService()
        {
            // Arrange
            _serviceFactory = new FamilyTreeServiceFactory(_mockUnitOfWork.Object);

            // Act
            var service = _serviceFactory.CreateFamilyService();

            // Assert
            Assert.IsType<FamilyService>(service);
        }

        [Fact]
        public void CreateIndividualService_Returns_IndividualService()
        {
            // Arrange
            _serviceFactory = new FamilyTreeServiceFactory(_mockUnitOfWork.Object);

            // Act
            var service = _serviceFactory.CreateIndividualService();

            // Assert
            Assert.IsType<IndividualService>(service);
        }

        [Fact]
        public void CreateTreeService_Returns_TreeService()
        {
            // Arrange
            _serviceFactory = new FamilyTreeServiceFactory(_mockUnitOfWork.Object);

            // Act
            var service = _serviceFactory.CreateTreeService();

            // Assert
            Assert.IsType<TreeService>(service);
        }
    }
}