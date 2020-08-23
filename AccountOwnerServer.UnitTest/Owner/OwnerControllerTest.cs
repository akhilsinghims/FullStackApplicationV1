using AccountOwnerServer.Controllers;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace AccountOwnerServer.xUnitTest.Owner
{

    public class OwnerControllerTest
    {
        private OwnerController ownerController;
        private ILoggerManager logger;
        private IMapper mapper;
        public OwnerControllerTest()
        {
            IRepositoryWrapper repositoryWrapper = new RepositoryWrapper();
            
            logger = new LoggerManager();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Entities.Models.Owner, OwnerDto>();
            });
            mapper = config.CreateMapper();

            ownerController = new OwnerController(logger, repositoryWrapper, mapper);
        }


        [Fact]
        public async Task Get_Owners_ReturnOkResult()
        {
            var _ownerList = new List<Entities.Models.Owner>()
            {
                new Entities.Models.Owner(){ Id=new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),Name="Akhil Kumar",  Address="M1-106"},
                new Entities.Models.Owner(){Id=new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),Name="Saurabh Pander",Address="Pune"}
            };

            //Arrange
            var mockRepo = new Mock<IRepositoryWrapper>();
            mockRepo.Setup(repo => repo.Owner.GetAllOwnersAsync()).ReturnsAsync(_ownerList);
            var controller = new OwnerController(logger, mockRepo.Object, mapper);

            //Act
            IActionResult result = await controller.GetAllOwners();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_Owners_InternalServerError()
        {
            var _ownerList = new List<Entities.Models.Owner>()
            {
                new Entities.Models.Owner(){ Id=new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),Name="Akhil Kumar",  Address="M1-106"},
                new Entities.Models.Owner(){Id=new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),Name="Saurabh Pander",Address="Pune"}
            };

            //Arrange
            var mockRepo = new Mock<IRepositoryWrapper>();
            mockRepo.Setup(repo => repo.Owner.GetAllOwnersAsync()).ReturnsAsync(_ownerList);

            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<AccountDto, Entities.Models.Owner>();
            });
            mapper = config.CreateMapper();
            var controller = new OwnerController(logger, mockRepo.Object, mapper);

            //Act
            IActionResult actionResult = await controller.GetAllOwners();

            //Assert            
            var httpError = Assert.IsType<Microsoft.AspNetCore.Mvc.ObjectResult>(actionResult);
            Assert.Equal("Internal server error", httpError.Value);
        }
    }
}
