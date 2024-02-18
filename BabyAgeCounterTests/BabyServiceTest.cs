using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BabyAgeCounter.Server.DTOs;
using BabyAgeCounter.Server.Mapper;
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
    private Mock<IBabyRepository>? _babyRepo;
    private List<BabyEntity>? _babyList;
    private readonly Guid _baby1Id = Guid.NewGuid();
    private readonly Guid _baby2Id = Guid.NewGuid();
    private readonly Guid _baby3Id = Guid.NewGuid();
    private BabyService? _babyService;


    [SetUp]
    public void Setup()
    {
        _babyRepo = new Mock<IBabyRepository>();
        _babyList = new List<BabyEntity>
        {
            new()
            {
                Id = _baby1Id,
                DueDate = DateTime.Today,
                Age = DateTime.Today
            },
            new()
            {
                Id = _baby2Id,
                DueDate = DateTime.Today.AddDays(1),
                Age = DateTime.Today.AddDays(1)
            },
            new()
            {
                Id = _baby3Id,
                DueDate = DateTime.Today.AddDays(2),
                Age = DateTime.Today.AddDays(2)
            }
        };

        _babyRepo.Setup(service => service.FindAll()).ReturnsAsync(_babyList);
        _babyRepo.Setup(service => service.FindById(It.IsAny<Guid>())).ReturnsAsync(
            (Guid babyId) => _babyList.FirstOrDefault(e => e.Id.CompareTo(babyId) == 0)
        );
        _babyService = new BabyService(_babyRepo.Object, MapperTestInstance.GetTestMapper());
    }

    [Test]
    public void TestFindAllBabies()
    {
        var expectedDate = DateTimeConverter.ToUtcMillis(DateTime.Today);

        var babyList = _babyService?.FindAll().Result;
        var baby = babyList?.First();

        Assert.AreEqual(3, babyList?.Count);
        Assert.AreEqual(expectedDate, baby?.DueDate);
        Assert.AreEqual(expectedDate, baby?.Age);
    }

    [Test]
    public void TestFindBabyById()
    {
        var expectedDate = DateTimeConverter.ToUtcMillis(DateTime.Today.AddDays(1));
        var baby = _babyService?.FindById(_baby2Id).Result;

        Assert.AreEqual(expectedDate, baby.DueDate);
        Assert.AreEqual(expectedDate, baby.Age);
    }

    [Test]
    public void TestAddBaby()
    {
        var baby4Id = Guid.NewGuid();
        var newBaby = new BabyDto()
        {
            Id = baby4Id.ToString(),
            DueDate = DateTimeConverter.ToUtcMillis(DateTime.Today.AddDays(3)),
            Age = DateTimeConverter.ToUtcMillis(DateTime.Today.AddDays(3))
        };
        var expectedBaby = new BabyEntity()
        {
            Id = baby4Id,
            DueDate = new DateTime(newBaby.DueDate),
            Age = new DateTime(newBaby.Age)
        };
        _babyService?.AddBaby(newBaby);
        _babyRepo?.Verify(iRepo => iRepo.Add(expectedBaby), Times.Once);
    }

    [Test]
    public void TestUpdateBaby()
    {
        var newBabyDueDate = DateTimeConverter.ToUtcMillis(DateTime.Today.AddDays(10));
        var expectedBaby = new BabyEntity()
        {
            Id = _baby2Id,
            DueDate = new DateTime(newBabyDueDate),
            Age = new DateTime(DateTimeConverter.ToUtcMillis(DateTime.Today.AddDays(1)))
        };

        var baby = _babyService?.FindById(_baby2Id).Result;
        if (baby == null) throw new InvalidOperationException("Baby should not be null!");
        baby.DueDate = newBabyDueDate;

        _babyService?.UpdateBaby(baby);
        _babyRepo?.Verify(iRepo => iRepo.Update(expectedBaby), Times.Once);
    }

    [Test]
    public void TestRemoveBaby()
    {
        _babyService?.RemoveBaby(_baby1Id);
        _babyRepo?.Verify(iRepo => iRepo.Remove(_baby1Id), Times.Once);
    }
}
