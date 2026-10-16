using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part2.Controllers;
using POE_Part2.Database;
using POE_Part2.Models;
using Xunit;

namespace POE_Part2.Tests
{
    public class RaceDayUnitTests
    {
        private RaceDayContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new RaceDayContext(options);
        }

        [Fact]
        public async Task GetCategories_ReturnsOkResult_WithCategories()
        {
            // Arrange
            using var context = GetInMemoryContext();
            context.Categories.Add(new Category { CategoryName = "10km Run", DistanceKm = 10 });
            await context.SaveChangesAsync();

            var controller = new CategoriesController(context);

            // Act
            var result = await controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var categories = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
            Assert.Single(categories);
        }

        [Fact]
        public async Task EnrolInEvent_ReturnsConflict_WhenAlreadyEnrolled()
        {
            // Arrange
            using var context = GetInMemoryContext();
            context.EventEnrolments.Add(new EventEnrolment { EventID = 1, ParticipantID = 1, CategoryID = 1 });
            await context.SaveChangesAsync();

            var controller = new EnrolmentsController(context);
            var duplicateEnrolment = new EventEnrolment { ParticipantID = 1, CategoryID = 1 };

            // Act
            var result = await controller.EnrolInEvent(1, duplicateEnrolment);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}