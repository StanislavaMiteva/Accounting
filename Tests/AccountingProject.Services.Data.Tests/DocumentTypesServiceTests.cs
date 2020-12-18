namespace AccountingProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AccountingProject.Data;
    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Data.Repositories;
    using AccountingProject.Services.Data.Tests.TestModels;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class DocumentTypesServiceTests
    {
        [Fact]
        public async Task CreateShouldAddNewDocumentTypeCorrectly()
        {
            var list = new List<DocumentType>();
            var mockRepo = new Mock<IDeletableEntityRepository<DocumentType>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<DocumentType>())).Callback(
                (DocumentType documentType) => list.Add(documentType));
            var service = new DocumentTypesService(mockRepo.Object);
            var input = new CreateDocumentTypeInputModel
            {
                Name = "Test",
            };
            await service.CreateAsync(input);
            var name = list.First().Name;

            Assert.Single(list);
            Assert.Equal("Test", name);
        }

        [Fact]
        public void GetAllShouldReturnCorrectResult()
        {
            var list = new List<DocumentType>
            {
                new DocumentType
                {
                    Id = 5,
                    Name = "Test",
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<DocumentType>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new DocumentTypesService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(DocumentTypePartViewModel).Assembly);
            var actualResult = service.GetAll<DocumentTypeViewModel>().ToList();
            var expectedResult = new List<DocumentTypePartViewModel>
            {
                new DocumentTypePartViewModel { Id = 5, Name = "Test", },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteDocumentType()
        {
            var documentType = new DocumentType
            {
                Id = 5,
                Name = "Test",
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<DocumentType>(dbContext);
            await repo.AddAsync(documentType);
            await repo.SaveChangesAsync();
            var service = new DocumentTypesService(repo);
            await service.DeleteAsync(documentType.Id);
            var documentTypes = repo.AllAsNoTracking().ToList();

            Assert.Empty(documentTypes);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateDocumentType()
        {
            var documentType = new DocumentType
            {
                Id = 5,
                Name = "Test",
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<DocumentType>(dbContext);            
            await repo.AddAsync(documentType);
            await repo.SaveChangesAsync();
            var service = new DocumentTypesService(repo);
            var input = new EditDocumentTypeInputModel
            {
                Id = 5,
                Name = "TestNew",
            };
            await service.UpdateAsync(5, input);

            Assert.Equal("TestNew", documentType.Name);
        }
    }
}
