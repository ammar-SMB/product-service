using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

public class ProductsControllerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private ProductsController _ProductsController;

    [SetUp]
    public void Setup()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _ProductsController = new ProductsController(_mockProductRepository.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOk()
    {
        // Arrange
        var Products = new List<Product>
        {
            new Product { Id = 1, Name = "Toyota", Model = "Camry", isActive = false },
            new Product { Id = 2, Name = "Honda", Model = "Accord", isActive = false }
        };
        _mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(Products);

        // Act
        var result = await _ProductsController.GetAll();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var items = okResult.Value as List<Product>;
        Assert.IsNotNull(items);
        Assert.AreEqual(2, items.Count);
    }

    // Add additional tests for GetById, Create, Update, and Delete endpoints
}
