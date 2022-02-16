using Moq;

using SmarFamily.TestUtilities;

using SmartFamily.Core.Collections;
using SmartFamily.Core.Common;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace SmartFamily.DomainServices.Tests
{
    public abstract class EntityServiceBaseTests<TEntity, TService> where TEntity : Entity where TService : IEntityService<TEntity>
    {
        private TService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public EntityServiceBaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void Service_Constructor_Throws_If_UnitOfWork_Argument_Is_Null()
        {
            Assert.Throws<TargetInvocationException>(() => CreateService(null));
        }

        [Fact]
        public void Service_Add_Throws_On_Null_Entity()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => _service.Add(null));
        }

        [Fact]
        public void Service_Add_Calls_Entity_Add_Method_With_The_Same_Entity_It_Received()
        {
            // Create test data
            var newEntity = NewEntity();

            // Create Mock
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Act
            _service.Add(newEntity);

            // Assert
            mockRepository.Verify(r => r.Add(newEntity));
        }

        [Fact]
        public void Service_Add_Calls_UnitOfWork_Commit_Method()
        {
            // Create test data
            var newEntity = NewEntity();

            // Create Mock
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Act
            _service.Add(newEntity);

            // Assert
            _mockUnitOfWork.Verify(db => db.Commit());
        }

        [Fact]
        public void Service_Delete_Throws_On_Null_Source()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => _service.Delete(null));
        }

        [Fact]
        public void Service_Delete_Calls_Repository_Delete_Method_With_The_Same_Entity_It_Received()
        {
            // Create test data
            var newEntity = NewEntity();

            // Create Mock
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Act
            _service.Delete(newEntity);

            // Assert
            mockRepository.Verify(r => r.Delete(newEntity));
        }

        [Fact]
        public void Service_Delete_Calls_UnitOfWork_Commit_Method()
        {
            // Create test data
            var newEntity = NewEntity();

            // Create Mock
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Act
            _service.Delete(newEntity);

            // Assert
            _mockUnitOfWork.Verify(db => db.Commit());
        }

        [Fact]
        public void Service_Get_Throws_On_Null_TreeId()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => _service.Get(It.IsAny<string>(), string.Empty));
        }

        [Fact]
        public void Service_Get_Throws_On_Null_Id()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => _service.Get(string.Empty, It.IsAny<string>()));
        }

        [Fact]
        public void Service_Get_Calls_Source_Get()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            _service = CreateService(_mockUnitOfWork.Object);
            const string id = TestConstants.ID_Exists;

            // Act
            _service.Get(id, TestConstants.TREE_Id);

            // Assert
            VerifyFind(mockRepository);
        }

        [Fact]
        public void Service_Get_Returns_Entity_On_Valid_Id()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork, GetEntities, TestConstants.PAGE_TotalCount);

            _service = CreateService(_mockUnitOfWork.Object);
            const string id = TestConstants.ID_Exists;

            // Act
            var individual = _service.Get(id, TestConstants.TREE_Id);

            // Assert
            Assert.IsType<TEntity>(individual);
        }

        [Fact]
        public void Service_Get_Returns_Null_On_InValid_Id()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork, GetEntities, TestConstants.PAGE_NotFound);

            _service = CreateService(_mockUnitOfWork.Object);
            const string id = TestConstants.ID_NotFound;

            // Act
            var individual = _service.Get(id, TestConstants.TREE_Id);

            // Assert
            Assert.Null(individual);
        }

        [Fact]
        public void Service_Get_Overload_Throws_On_Null_TreeId()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => _service.Get(string.Empty));
        }

        [Fact]
        public void Service_Get_Overload_Calls_Repository_Get()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            _service = CreateService(_mockUnitOfWork.Object);
            const string treeId = TestConstants.TREE_Id;

            // Act
            _service.Get(treeId);

            // Assert
            VerifyFind(mockRepository);
        }

        [Fact]
        public void Service_Get_Overload_Returns_List_Of_Entities()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork, GetEntities, TestConstants.PAGE_TotalCount);

            _service = CreateService(_mockUnitOfWork.Object);
            const string treeId = TestConstants.TREE_Id;

            // Act
            var sources = _service.Get(treeId);

            // Assert
            Assert.IsType<IEnumerable<TEntity>>(sources);
            Assert.Equal(TestConstants.PAGE_TotalCount, sources.Count());
        }

        [Fact]
        public void Service_Get_ByPage_Overload_Throws_On_Null_TreeId()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => _service.Get(string.Empty, t => true, 0, TestConstants.PAGE_RecordCount));
        }

        [Fact]
        public void Service_Get_ByPage_Overload_Calls_Repository_Find()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            _service = CreateService(_mockUnitOfWork.Object);
            const string treeId = TestConstants.TREE_Id;

            // Act
            _service.Get(treeId, t => true, 0, TestConstants.PAGE_RecordCount);

            // Assert
            VerifyFind(mockRepository);
        }

        [Fact]
        public void Service_Get_ByPage_Overload_Returns_PagedList_Of_Sources()
        {
            // Arrange
            var mockRepository = SetUpRepository(_mockUnitOfWork, GetEntities, TestConstants.PAGE_TotalCount);

            _service = CreateService(_mockUnitOfWork.Object);
            const string treeId = TestConstants.TREE_Id;

            // Act
            var sources = _service.Get(treeId, t => true, 0, TestConstants.PAGE_RecordCount);

            // Assert
            Assert.IsType<IPagedList<TEntity>>(sources);
            Assert.Equal(TestConstants.PAGE_TotalCount, sources.TotalCount);
            Assert.Equal(TestConstants.PAGE_RecordCount, sources.PageSize);
        }

        [Fact]
        public void Service_Update_Throws_On_Null_Entity()
        {
            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => _service.Update(null));
        }

        [Fact]
        public void Service_Update_Calls_Source_Update_Method_With_The_Same_Entity_It_Received()
        {
            // Create test data
            var entity = UpdateEntity();

            // Create Mock
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Act
            _service.Update(entity);

            // Assert
            mockRepository.Verify(r => r.Update(entity));
        }

        [Fact]
        public void Service_Update_Calls_UnitOfWork_Commit_Method()
        {
            // Create test data
            var entity = UpdateEntity();

            // Create Mock
            var mockRepository = SetUpRepository(_mockUnitOfWork);

            // Arrange
            _service = CreateService(_mockUnitOfWork.Object);

            // Act
            _service.Update(entity);

            // Assert
            _mockUnitOfWork.Verify(db => db.Commit());
        }

        protected TService CreateService(IUnitOfWork unitOfWork)
        {
            return (TService)Activator.CreateInstance(typeof(TService), new object[] { unitOfWork });
        }

        protected abstract TEntity NewEntity();

        protected abstract IEnumerable<TEntity> GetEntities(int count);

        protected abstract TEntity UpdateEntity();

        private Mock<IRepository<TEntity>> SetUpRepository(Mock<IUnitOfWork> mockUnitOfWork)
        {
            var mockRepository = new Mock<IRepository<TEntity>>();
            mockUnitOfWork.Setup(u => u.GetRepository<TEntity>()).Returns(mockRepository.Object);

            return mockRepository;
        }

        private Mock<IRepository<TEntity>> SetUpRepository(Mock<IUnitOfWork> mockUnitOfWork, Func<int, IEnumerable<TEntity>> getEntities, int entityCount)
        {
            var mockRepository = new Mock<IRepository<TEntity>>();
            mockRepository.Setup(r => r.Find(It.IsAny<Func<TEntity, bool>>())).Returns(getEntities(entityCount));

            mockUnitOfWork.Setup(u => u.GetRepository<TEntity>()).Returns(mockRepository.Object);

            return mockRepository;
        }

        private void VerifyFind(Mock<IRepository<TEntity>> mockRepository)
        {
            mockRepository.Verify(r => r.Find(It.IsAny<Func<TEntity, bool>>()));
        }
    }
}