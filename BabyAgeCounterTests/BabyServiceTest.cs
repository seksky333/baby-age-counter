using System.Diagnostics.CodeAnalysis;
using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.Repositories;
using BabyAgeCounter.Server.Services;
using BabyAgeCounter.Server.utilities;
using Moq;

namespace BabyAgeCounterTests;

[SuppressMessage("Assertion",
    "NUnit2005:Consider using Assert.That(actual, Is.EqualTo(expected)) instead of Assert.AreEqual(expected, actual)")]
public class Tests
{
    private Mock<IBabyRepository> _babyRepo;
    private List<BabyEntity> _babyList;

    [SetUp]
    public void Setup()
    {
        _babyRepo = new Mock<IBabyRepository>();
        _babyList = new List<BabyEntity>
        {
            new()
            {
                Id = new Guid(),
                DueDate = DateTime.Today,
                Age = DateTime.Today
            }
        };
    }

    [Test]
    public void TestGetBabyDto()
    {
        var expectedDate = DateTimeConverter.ToUtcMillis(DateTime.Today);
        _babyRepo.Setup(service => service.FindAll()).ReturnsAsync(_babyList);
        var babyService = new BabyService(_babyRepo.Object);
        var babyList = babyService.FindAll().Result;
        var baby = babyList.First();

        Assert.AreEqual(1, babyList.Count);
        Assert.AreEqual(expectedDate, baby.DueDate);
        Assert.AreEqual(expectedDate, baby.Age);
    }

    [Test]
    public void Test2()
    {
        /*
         * Web API Controllers
         * The action returns the correct type of response.
         * Invalid parameters return the correct error response.
         * The action calls the correct method on the repository or service layer.
         * https://learn.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api
         */
    }

    [Test]
    public void Test3()
    {
        /*
         * Test EF Core
         * Using DbContext with In-Memory
         */
    }
}