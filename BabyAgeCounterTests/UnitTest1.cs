using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.Repositories;
using Moq;

namespace BabyAgeCounterTests;

public class Tests
{
    private Mock<IBabyRepository> babyRepo;
    private List<BabyEntity> babyList;

    [SetUp]
    public void Setup()
    {
        babyRepo = new Mock<IBabyRepository>();
        babyList = new List<BabyEntity>
        {
            new()
            {
                Id = new Guid(),
                DueDate = DateTime.Today,
                Age = DateTime.Today,
            }
        };

        // _controller = new BabyController();

        // var dbContextOption = new DbContextOptions();
        // var babyContext = new BabyContext();
        // _controller = new BabyController();
    }

    [Test]
    public void Test1()
    {
        /*
         * Test service layer
         */
        
        babyRepo.Setup(repo => repo.FindAll()).ReturnsAsync(babyList);
        //var result = controller.Findall()
        //check result 
        Console.WriteLine(babyList.Count);

        Assert.Pass();
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