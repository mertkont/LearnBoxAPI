using Business.Contracts;
using Business.Services;
using DataAccess.DTOs;
using DataAccess.Models;

namespace LearnBoxApi.Tests;

[CollectionDefinition("CategoryCollection")]
public class CategoryServiceTests
{
    public Mock<CategoryService> MockCategoryService { get; set; }

    private Category CreateTestCategory()
    {
        string[] categories = { "Category 1", "Category 2", "Category 3" };

        return new Category
        {
            CategoryId = GetRandomId(),
            CategoryName = categories[GetRandomIndex(categories.Length)]
        };
    }

    private int GetRandomId()
    {
        return new Random().Next(1, 500);
    }

    private int GetRandomIndex(int length)
    {
        return new Random().Next(0, length);
    }

    [Fact]
    public void GetCategories_ReturnsCategories()
    {
        var mock = new Mock<ICategoryService>();

        var testCategories = new List<Category> { CreateTestCategory() };

        mock.Setup(x => x.GetAllCategories()).Returns(testCategories);

        // Use mock object
        var result = mock.Object.GetAllCategories();

        Assert.Equal(testCategories, result);
    }

    [Fact]
    public void GetById_ReturnsExpectedCategory()
    {
        var mock = new Mock<ICategoryService>();

        // Arrange
        var testId = GetRandomId();
        var testCategory = CreateTestCategory();
        testCategory.CategoryId = testId;

        mock.Setup(x => x.GetCategoryById(testId)).Returns(testCategory);

        // Act
        var result = mock.Object.GetCategoryById(testId);

        // Assert 
        Assert.Equal(testId, result.CategoryId);
    }

    [Fact]
    public void AddCategory_AddsToList()
    {
        var mock = new Mock<ICategoryService>();

        // Arrange 
        int expectedId = GetRandomId();
        var returnedCategory = new Category { CategoryId = expectedId };
        mock.Setup(x => x.GetCategoryById(expectedId)).Returns(returnedCategory);

        // Act
        mock.Object.AddCategory(new Category());

        // Assert
        var addedCategory = mock.Object.GetCategoryById(expectedId);

        Assert.NotNull(addedCategory);
        Assert.Equal(expectedId, addedCategory.CategoryId);
    }

    [Fact]
    public void UpdateCategory_UpdatesCategory()
    {
        var mock = new Mock<ICategoryService>();

        // Arrange
        var testCategory = new Category(){ CategoryId = GetRandomId(), CategoryName = "Test Category"};

        mock.Setup(x => x.GetCategoryById(testCategory.CategoryId)).Returns(testCategory); 

        // Act
        testCategory.CategoryName = "Test Category";
        mock.Object.UpdateCategory(new Category());

        // Assert
        var result = mock.Object.GetCategoryById(testCategory.CategoryId);

        Assert.True(result.CategoryName == testCategory.CategoryName);
    }

    [Fact]
    public void DeleteCategory_RemovesFromList()
    {
        var mock = new Mock<ICategoryService>();
        var deletedId = GetRandomId();
        mock.Setup(x => x.DeleteCategory(deletedId)); 
        mock.Object.DeleteCategory(deletedId);
        var result = mock.Object.GetCategoryById(deletedId);
        Assert.Null(result);
    }
}