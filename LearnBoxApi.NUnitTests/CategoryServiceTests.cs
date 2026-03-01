using Business.Services;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace LearnBoxApi.NUnitTests;

[TestFixture]
public class CategoryServiceTests
{
    private Mock<ICategoryDal> _mockCategoryDal = null!;
    private CategoryService _categoryService = null!;

    [SetUp]
    public void SetUp()
    {
        _mockCategoryDal = new Mock<ICategoryDal>();
        _categoryService = new CategoryService(_mockCategoryDal.Object);
    }

    [Test]
    public void GetAllCategories_ReturnsAllCategories()
    {
        var expected = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Science" },
            new Category { CategoryId = 2, CategoryName = "History" }
        };
        _mockCategoryDal.Setup(d => d.GetAllCategories()).Returns(expected);

        var result = _categoryService.GetAllCategories();

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result, Is.EqualTo(expected));
        _mockCategoryDal.Verify(d => d.GetAllCategories(), Times.Once);
    }

    [Test]
    public void GetCategoryById_ValidId_ReturnsCategory()
    {
        var expected = new Category { CategoryId = 3, CategoryName = "Math" };
        _mockCategoryDal.Setup(d => d.GetCategoryById(3)).Returns(expected);

        var result = _categoryService.GetCategoryById(3);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.CategoryId, Is.EqualTo(3));
        Assert.That(result.CategoryName, Is.EqualTo("Math"));
        _mockCategoryDal.Verify(d => d.GetCategoryById(3), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    public void GetCategoryById_InvalidId_ThrowsArgumentException(int invalidId)
    {
        Assert.Throws<ArgumentException>(() => _categoryService.GetCategoryById(invalidId));
        _mockCategoryDal.Verify(d => d.GetCategoryById(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void AddCategory_ValidCategory_CallsDal()
    {
        var category = new Category { CategoryName = "New Category" };

        _categoryService.AddCategory(category);

        _mockCategoryDal.Verify(d => d.AddCategory(category), Times.Once);
    }

    [Test]
    public void AddCategory_NullCategory_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _categoryService.AddCategory(null!));
        _mockCategoryDal.Verify(d => d.AddCategory(It.IsAny<Category>()), Times.Never);
    }

    [Test]
    public void UpdateCategory_ValidCategory_CallsDal()
    {
        var category = new Category { CategoryId = 1, CategoryName = "Updated" };

        _categoryService.UpdateCategory(category);

        _mockCategoryDal.Verify(d => d.UpdateCategory(category), Times.Once);
    }

    [Test]
    public void UpdateCategory_NullCategory_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _categoryService.UpdateCategory(null!));
        _mockCategoryDal.Verify(d => d.UpdateCategory(It.IsAny<Category>()), Times.Never);
    }

    [Test]
    public void DeleteCategory_ValidId_CallsDal()
    {
        _categoryService.DeleteCategory(5);

        _mockCategoryDal.Verify(d => d.DeleteCategory(5), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void DeleteCategory_InvalidId_ThrowsArgumentException(int invalidId)
    {
        Assert.Throws<ArgumentException>(() => _categoryService.DeleteCategory(invalidId));
        _mockCategoryDal.Verify(d => d.DeleteCategory(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void GetAllCategories_EmptyList_ReturnsEmptyList()
    {
        _mockCategoryDal.Setup(d => d.GetAllCategories()).Returns(new List<Category>());

        var result = _categoryService.GetAllCategories();

        Assert.That(result, Is.Empty);
    }
}
