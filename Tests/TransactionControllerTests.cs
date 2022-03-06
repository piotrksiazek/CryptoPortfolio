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
    public class TransactionControllerTests
    {
        [Fact]
        public async Task GetTransactions_WithUnexistingTransactions_ReturnsNotFound()
        {
            //Arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            unitOfWorkStub.Setup(uow => uow.TransactionRepository.GetUserTransactions(It.IsAny<string>())).ReturnsAsync((List<Transaction>)null);

            var balanceServiceStub = new Mock<IBalanceService>();

            var claimsRetriever = new Mock<IClaimsRetriever>();
            claimsRetriever.Setup(cr => cr.GetUserId(It.IsAny<HttpContext>())).Returns(It.IsAny<string>());

            var controller = new TransactionController(unitOfWorkStub.Object, balanceServiceStub.Object, claimsRetriever.Object);

            //Act
            var result = await controller.GetTransactions();

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}