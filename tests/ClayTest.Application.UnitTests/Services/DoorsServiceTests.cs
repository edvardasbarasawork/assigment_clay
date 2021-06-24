using ClayTest.Application.Exceptions;
using ClayTest.Application.Services.Entities;
using ClayTest.Application.Services.Interfaces;
using ClayTest.Core.Entities;
using ClayTest.DataAccess.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ClayTest.Application.UnitTests.Services
{
    [TestClass]
    public class DoorsServiceTests
    {
        // TODO: add tests

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Door with Id was not found.")]
        public async Task PatchAsync_Should_Throw_NotFoundException()
        {
            // Arrange

            var doorPatch = new DoorPatch()
            {
                Id = Guid.NewGuid(),
                Closed = true
            };

            var claimService = new Mock<IClaimService>();
            var doorEventLogsRepository = new Mock<IDoorEventLogsRepository>();
            var doorsRepository = new Mock<IDoorsRepository>();
            doorsRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Door>(null));

            var companiesService = new Application.Services.DoorsService(
                claimService.Object, doorsRepository.Object, doorEventLogsRepository.Object);

            // Act
            var result = await companiesService.PatchAsync(doorPatch);

            // Assert - exception
        }
    }
}
