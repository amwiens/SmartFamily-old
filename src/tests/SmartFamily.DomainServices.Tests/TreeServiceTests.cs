using Moq;

using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.Core.Collections;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Services;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace SmartFamily.DomainServices.Tests
{
    public class TreeServiceTests
    {
        private TreeService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public TreeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void TreeService_Constructor_Throws_If_UnitOfWork_Argument_Is_Null()
        {
            // Arrange

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new TreeService(null));
        }

        [Fact]
        public void TreeService_Add_Throws_On_Null_Tree()
        {
            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => _service.Add(null));
        }

        [Fact]
        public void TreeService_Add_Calls_Repository_Add_Method_With_The_Same_Tree_Object_It_Received()
        {
            // Create test data
            var newTree = new Tree
            {
                Name = "Foo"
            };

            // Setup Mock
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Add(newTree);

            // Assert
            mockRepository.Verify(r => r.Add(newTree));
        }

        [Fact]
        public void TreeService_Add_Calls_UnitOfWork_Commit_Method()
        {
            // Create test data
            var newTree = new Tree
            {
                Name = "Foo"
            };

            // Create Mock
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Add(newTree);

            // Assert
            _mockUnitOfWork.Verify(db => db.Commit());
        }

        [Fact]
        public void TreeService_Delete_Throws_On_Null_Tree()
        {
            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => _service.Delete(null));
        }

        [Fact]
        public void TreeService_Delete_Calls_Repository_Delete_Method_With_The_Same_Tree_Object_It_Received()
        {
            // Create test data
            var newTree = new Tree
            {
                Name = "Foo"
            };

            // Create Mock
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Delete(newTree);

            // Assert
            mockRepository.Verify(r => r.Delete(newTree));
        }

        [Fact]
        public void TreeService_Delete_Calls_UnitOfWork_Commit_Method()
        {
            // Create test data
            var newTree = new Tree
            {
                Name = "Foo"
            };

            // Create Mock
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Delete(newTree);

            // Assert
            _mockUnitOfWork.Verify(d => d.Commit());
        }

        [Fact]
        public void TreeService_Get_Throws_On_Null_Id()
        {
            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => _service.Get(string.Empty));
        }

        [Fact]
        public void TreeService_Get_Calls_Repository_GetAll()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            mockRepository.Setup(r => r.GetAll()).Returns(GetTrees(TestConstants.PAGE_TotalCount));
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);
            const string id = TestConstants.ID_Exists;

            // Act
            _service.Get(id);

            // Assert
            mockRepository.Verify(r => r.GetAll());
        }

        [Fact]
        public void TreeService_Get_Returns_Tree_On_Valid_Id()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            mockRepository.Setup(r => r.GetAll()).Returns(GetTrees(TestConstants.PAGE_TotalCount));
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);
            const string id = TestConstants.ID_Exists;

            // Act
            Tree tree = _service.Get(id);

            // Assert
            Assert.IsType<Tree>(tree);
        }

        [Fact]
        public void TreeService_Get_Returns_Null_On_Invalid_Id()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            mockRepository.Setup(r => r.GetAll()).Returns(GetTrees(TestConstants.PAGE_TotalCount));
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);
            const string id = TestConstants.ID_NotFound;

            // Act
            var tree = _service.Get(id);

            // Assert
            Assert.Null(tree);
        }

        [Fact]
        public void TreeService_Get_Overload_Calls_Repository_GetAll()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Get();

            // Assert
            mockRepository.Verify(r => r.GetAll());
        }

        [Fact]
        public void TreeService_Get_Overload_Returns_List_Of_Trees()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            mockRepository.Setup(r => r.GetAll()).Returns(GetTrees(TestConstants.PAGE_TotalCount));
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            var trees = _service.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Tree>>(trees);
            Assert.Equal(TestConstants.PAGE_TotalCount, trees.Count());
        }

        [Fact]
        public void TreeService_Get_ByPage_Overload_Calls_Repository_GetAll()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Get(t => true, 0, 5);

            // Assert
            mockRepository.Verify(r => r.GetAll());
        }

        [Fact]
        public void TreeService_Get_ByPage_Overload_Returns_PagedList_Of_Trees()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Tree>>();
            mockRepository.Setup(r => r.GetAll()).Returns(GetTrees(TestConstants.PAGE_TotalCount));
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            var trees = _service.Get(t => true, 0, TestConstants.PAGE_RecordCount);

            // Assert
            Assert.IsAssignableFrom<IPagedList<Tree>>(trees);
            Assert.Equal(TestConstants.PAGE_TotalCount, trees.TotalCount);
            Assert.Equal(TestConstants.PAGE_RecordCount, trees.PageSize);
        }

        [Fact]
        public void TreeService_Update_Throws_On_Null_Tree()
        {
            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => _service.Add(null));
        }

        [Fact]
        public void TreeService_Update_Calls_Repository_Update_Method_With_The_Same_Tree_Object_It_Received()
        {
            // Create test data
            var tree = new Tree
            {
                Name = "Foo"
            };

            // Create Mock
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Update(tree);

            // Assert
            mockRepository.Verify(r => r.Update(tree));
        }

        [Fact]
        public void TreeService_Update_Calls_UnitOfWork_Commit_Method()
        {
            // Create test data
            var tree = new Tree
            {
                Name = "Foo"
            };

            // Create Mock
            var mockRepository = new Mock<IRepository<Tree>>();
            _mockUnitOfWork.Setup(d => d.GetRepository<Tree>()).Returns(mockRepository.Object);

            // Arrange
            _service = new TreeService(_mockUnitOfWork.Object);

            // Act
            _service.Update(tree);

            // Assert
            _mockUnitOfWork.Verify(db => db.Commit());
        }

        private static IQueryable<Tree> GetTrees(int count)
        {
            var trees = new List<Tree>();

            for (int i = 0; i < count; i++)
            {
                trees.Add(new Tree
                {
                    TreeId = i.ToString(),
                    Name = "Foo"
                });
            }

            return trees.AsQueryable();
        }
    }
}