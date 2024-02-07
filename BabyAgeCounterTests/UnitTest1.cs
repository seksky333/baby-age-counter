using BabyAgeCounter.Server.Controllers;
using BabyAgeCounter.Server.data;
using Microsoft.EntityFrameworkCore;

namespace BabyAgeCounterTests;

public class Tests
{
    private BabyController _controller;

    [SetUp]
    public void Setup()
    {
        var dbContextOption = new DbContextOptions();
        var babyContext = new BabyContext();
        _controller = new BabyController();
    }

    [Test]
    public void Test1()
    {
        RestClient client = new RestClient(baseUrl);

        Assert.Pass();
    }
}