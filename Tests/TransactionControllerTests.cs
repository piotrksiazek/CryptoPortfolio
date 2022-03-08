using Api.Controllers;
using Api.Extensions;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    internal class TransactionBasicSetup
    {
        public Mock<IUnitOfWork> UnitOfWorkMock = new Mock<IUnitOfWork>();
        public Mock<IBalanceService> BalanceServiceMock = new Mock<IBalanceService>();
        public Mock<IClaimsRetriever> CaimsRetrieverMock = new Mock<IClaimsRetriever>();
    }
    public class TransactionControllerTests
    {
        [Fact]
        public async Task GetTransactions_WithUnexistingTransactions_ReturnsNotFound()
        {
            //Arrange
            var objects = new TransactionBasicSetup();
            objects.UnitOfWorkMock.Setup(uow => uow.TransactionRepository.GetUserTransactions(It.IsAny<string>())).ReturnsAsync((List<Transaction>)null);
            objects.CaimsRetrieverMock.Setup(cr => cr.GetUserId(It.IsAny<HttpContext>())).Returns(It.IsAny<string>());
            var controller = new TransactionController(objects.UnitOfWorkMock.Object, objects.BalanceServiceMock.Object, objects.CaimsRetrieverMock.Object);
            //Act
            var result = await controller.GetTransactions();

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetTransactions_WithUnexistingTransactions_ReturnsOk()
        {
            //Arrange
            var objects = new TransactionBasicSetup();
            objects.UnitOfWorkMock.Setup(uow => uow.TransactionRepository.GetUserTransactions(It.IsAny<string>())).ReturnsAsync(new List<Transaction> { new Transaction() });
            objects.CaimsRetrieverMock.Setup(cr => cr.GetUserId(It.IsAny<HttpContext>())).Returns(It.IsAny<string>());
            var controller = new TransactionController(objects.UnitOfWorkMock.Object, objects.BalanceServiceMock.Object, objects.CaimsRetrieverMock.Object);
            //Act
            var result = await controller.GetTransactions();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}