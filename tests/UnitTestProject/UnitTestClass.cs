using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Tecsys.Exercise.Application.Common.Interfaces;
using Tecsys.Exercise.Application.Products.Queries;
using Tecsys.Exercise.Infrastructure.Entities;
using Tecsys.Exercise.Infrastructure.Persistence;
using Tecsys.Exercise.Infrastructure.Services;
using Tecsys.Exercise.WebUI.Controllers;
using Tecsys.Exercise.Application.Common.Exceptions;
using Xunit;
using System.Threading.Tasks;

namespace Tecsys.Exercise.UnitTest
{
    public class UnitTestClass : IClassFixture<OneTimeSetUp>
    {
        private readonly OneTimeSetUp setup;

        public UnitTestClass(OneTimeSetUp setup)
        {
            this.setup = setup;
        }


        // (Use In-Memeory Database According to appsettings.json)
        [Fact]
        public async void GetCarsQueryHandler()
        {
            WingtiptoysDbContext dbContext = await setup.GetDbContext();
            // Arrange
            var request = new GetCarsQuery();
            var sut = new GetCarsQueryHandler(dbContext);

            // Act
            var result = await sut.Handle(request, new System.Threading.CancellationToken());

            // Assert
            result.Count.Should().Be(2);
        }


        [Theory]
        [InlineData("")]
        [InlineData("l")]
        public async void GetProductWithSerachText_ValidationLessthenTwoLength(string searchText)
        {
            // Arrange
            var request = new GetProductWithTextSearchQuery { SearchText = searchText };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await setup.SendAsync(request));
        }

        [Theory]
        [InlineData("ll")]
        [InlineData("lll")]
        public async void GetProductWithSerachText_NoExceptionWhenLengthGratherOrEqualTwo(string searchText)
        {
            // Arrange
            var request = new GetProductWithTextSearchQuery { SearchText = searchText };

            // Act
            Func<Task> function = async () => await setup.SendAsync(request);
            var ex = await Record.ExceptionAsync(function);

            //Assert
            Assert.Null(ex);
        }

    }
}
