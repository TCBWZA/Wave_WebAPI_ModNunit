// STUDENT EXAMPLE TEST FILE
// This is a worked example showing how to write unit tests for the ProductRepository
// Copy this structure and adapt it for your own tests!

using NUnit.Framework;
using WebAPI_ModNunit.Models;
using WebAPI_ModNunit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_ModNunit.Tests.Repositories
{
    /// <summary>
    /// Example test class for ProductRepository.
    /// 
    /// This demonstrates:
    /// - How to set up a test class with NUnit
    /// - Using an in-memory database for testing
    /// - Writing multiple related tests
    /// - Testing both success and failure scenarios
    /// - Using the Arrange-Act-Assert pattern
    /// </summary>
    [TestFixture]
    public class ProductRepositoryExampleTests
    {
        private ProductRepository _repository;
        private AppDbContext _context;

        /// <summary>
        /// SetUp runs before EACH test method.
        /// Use it to initialize test fixtures (database, repository, etc.)
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Create an in-memory database for testing
            // Using Guid.NewGuid() ensures each test gets a fresh database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            _context = new AppDbContext(options);
            _repository = new ProductRepository(_context);
        }

        /// <summary>
        /// TearDown runs after EACH test method.
        /// Use it to clean up resources (dispose context, close connections, etc.)
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        // ==================== SUCCESSFUL SCENARIOS ====================

        /// <summary>
        /// Test: GetByIdAsync returns a product when given a valid ID
        /// 
        /// This test demonstrates:
        /// - Creating test data (ARRANGE)
        /// - Calling the method under test (ACT)
        /// - Verifying the result (ASSERT)
        /// </summary>
        [Test]
        public async Task GetByIdAsync_WithValidId_ReturnsProduct()
        {
            // ARRANGE
            var product = new Product
            {
                Name = "Test Widget",
                ProductCode = Guid.NewGuid()
            };
            await _repository.CreateAsync(product);

            // ACT
            var result = await _repository.GetByIdAsync(product.Id);

            // ASSERT
            Assert.That(result, Is.Not.Null, "Product should not be null");
            Assert.That(result.Id, Is.EqualTo(product.Id), "Product ID should match");
            Assert.That(result.Name, Is.EqualTo("Test Widget"), "Product name should match");
        }

        /// <summary>
        /// Test: GetAllAsync returns all products in the database
        /// </summary>
        [Test]
        public async Task GetAllAsync_WithMultipleProducts_ReturnsAllProducts()
        {
            // ARRANGE - Create multiple products
            var products = new List<Product>
            {
                new Product { Name = "Product 1", ProductCode = Guid.NewGuid() },
                new Product { Name = "Product 2", ProductCode = Guid.NewGuid() },
                new Product { Name = "Product 3", ProductCode = Guid.NewGuid() }
            };

            foreach (var product in products)
            {
                await _repository.CreateAsync(product);
            }

            // ACT
            var result = await _repository.GetAllAsync();

            // ASSERT
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result, Has.Count.EqualTo(3), "Should return all 3 products");
            Assert.That(result, Does.Contain(products[0]), "Should contain first product");
        }

        /// <summary>
        /// Test: GetByProductCodeAsync finds a product by its GUID
        /// </summary>
        [Test]
        public async Task GetByProductCodeAsync_WithValidCode_ReturnsProduct()
        {
            // ARRANGE
            var productCode = Guid.NewGuid();
            var product = new Product
            {
                Name = "Unique Widget",
                ProductCode = productCode
            };
            await _repository.CreateAsync(product);

            // ACT
            var result = await _repository.GetByProductCodeAsync(productCode);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ProductCode, Is.EqualTo(productCode));
            Assert.That(result.Name, Is.EqualTo("Unique Widget"));
        }

        /// <summary>
        /// Test: SearchAsync filters products by name
        /// </summary>
        [Test]
        public async Task SearchAsync_WithNameFilter_ReturnsMatchingProducts()
        {
            // ARRANGE
            await _repository.CreateAsync(new Product { Name = "Widget", ProductCode = Guid.NewGuid() });
            await _repository.CreateAsync(new Product { Name = "Gadget", ProductCode = Guid.NewGuid() });
            await _repository.CreateAsync(new Product { Name = "Widget Pro", ProductCode = Guid.NewGuid() });

            // ACT
            var results = await _repository.SearchAsync("Widget", null);

            // ASSERT
            Assert.That(results, Has.Count.EqualTo(2), "Should find 2 widgets");
            Assert.That(results, Does.All.Matches<Product>(p => p.Name.Contains("Widget")));
        }

        // ==================== FAILURE SCENARIOS ====================

        /// <summary>
        /// Test: GetByIdAsync returns null when ID doesn't exist
        /// This verifies the method handles "not found" gracefully
        /// </summary>
        [Test]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            // ACT
            var result = await _repository.GetByIdAsync(999);

            // ASSERT
            Assert.That(result, Is.Null, "Should return null for non-existent ID");
        }

        /// <summary>
        /// Test: GetAllAsync returns empty list when no products exist
        /// </summary>
        [Test]
        public async Task GetAllAsync_WithNoProducts_ReturnsEmptyList()
        {
            // ACT
            var result = await _repository.GetAllAsync();

            // ASSERT
            Assert.That(result, Is.Empty, "Should return empty list");
            Assert.That(result, Has.Count.EqualTo(0));
        }

        /// <summary>
        /// Test: GetByProductCodeAsync returns null for non-existent code
        /// </summary>
        [Test]
        public async Task GetByProductCodeAsync_WithInvalidCode_ReturnsNull()
        {
            // ACT
            var result = await _repository.GetByProductCodeAsync(Guid.NewGuid());

            // ASSERT
            Assert.That(result, Is.Null, "Should return null for non-existent code");
        }

        // ==================== EDGE CASES & ERROR CONDITIONS ====================

        /// <summary>
        /// Test: CreateAsync throws ArgumentNullException when product is null
        /// 
        /// This tests the error handling that SHOULD be in ProductRepository.CreateAsync
        /// If the repository doesn't have this check, this test will FAIL - that's what we want!
        /// Your job is to make this test PASS by fixing the code.
        /// </summary>
        [Test]
        public void CreateAsync_WithNullProduct_ThrowsArgumentNullException()
        {
            // ACT & ASSERT
            // This test expects an exception to be thrown
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() =>
                _repository.CreateAsync(null));

            // Verify the exception message is helpful
            Assert.That(ex.Message, Does.Contain("cannot be null"), 
                "Exception message should explain what went wrong");
        }

        /// <summary>
        /// Test: CreateAsync throws when product name is empty
        /// Demonstrates testing validation rules
        /// </summary>
        [Test]
        public async Task CreateAsync_WithEmptyName_ThrowsException()
        {
            // ARRANGE
            var product = new Product
            {
                Name = "",  // Invalid - empty name
                ProductCode = Guid.NewGuid()
            };

            // ACT & ASSERT
            // The model validation should catch this
            // (If using Data Annotations on the Product class)
            var exception = Assert.ThrowsAsync<Exception>(() =>
                _repository.CreateAsync(product));

            Assert.That(exception, Is.Not.Null);
        }

        /// <summary>
        /// Test: SearchAsync with null parameters returns all products
        /// Demonstrates how methods should handle optional parameters
        /// </summary>
        [Test]
        public async Task SearchAsync_WithNoFilters_ReturnsAllProducts()
        {
            // ARRANGE
            await _repository.CreateAsync(new Product { Name = "Product A", ProductCode = Guid.NewGuid() });
            await _repository.CreateAsync(new Product { Name = "Product B", ProductCode = Guid.NewGuid() });

            // ACT
            var results = await _repository.SearchAsync(null, null);

            // ASSERT
            Assert.That(results, Has.Count.EqualTo(2), "Should return all products when no filters");
        }

        // ==================== PAGINATION TESTS ====================

        /// <summary>
        /// Test: GetPagedAsync returns correct page of products
        /// This test identifies the bug: GetPagedAsync doesn't validate page/pageSize!
        /// </summary>
        [Test]
        public async Task GetPagedAsync_WithValidParameters_ReturnsPagedResults()
        {
            // ARRANGE - Create 25 products
            for (int i = 1; i <= 25; i++)
            {
                await _repository.CreateAsync(new Product
                {
                    Name = $"Product {i}",
                    ProductCode = Guid.NewGuid()
                });
            }

            // ACT - Get first page with 10 items
            var (items, totalCount) = await _repository.GetPagedAsync(1, 10);

            // ASSERT
            Assert.That(totalCount, Is.EqualTo(25), "Total count should be 25");
            Assert.That(items, Has.Count.EqualTo(10), "Page 1 should have 10 items");
        }

        /// <summary>
        /// Test: GetPagedAsync with page=0 should throw exception
        /// THIS TEST WILL FAIL because there's no validation!
        /// Your job is to add validation and make this test pass.
        /// 
        /// TUTOR NOTE FOR STUDENTS:
        /// This test identifies an intentional error:
        /// ProductRepository.GetPagedAsync doesn't validate that page >= 1
        /// Write code to fix this and make the test pass!
        /// </summary>
        [Test]
        public void GetPagedAsync_WithZeroPage_ThrowsArgumentException()
        {
            // ACT & ASSERT
            // This should throw an exception for invalid page number
            var ex = Assert.ThrowsAsync<ArgumentException>(() =>
                _repository.GetPagedAsync(0, 10));

            Assert.That(ex.Message, Does.Contain("page"), 
                "Error message should mention 'page'");
        }

        /// <summary>
        /// Test: GetPagedAsync with negative pageSize should throw
        /// Another validation error that needs fixing!
        /// </summary>
        [Test]
        public void GetPagedAsync_WithNegativePageSize_ThrowsArgumentException()
        {
            // ACT & ASSERT
            var ex = Assert.ThrowsAsync<ArgumentException>(() =>
                _repository.GetPagedAsync(1, -5));

            Assert.That(ex.Message, Does.Contain("pageSize"));
        }

        /// <summary>
        /// Test: GetPagedAsync with very large pageSize
        /// Edge case: what if someone requests 10,000 items per page?
        /// </summary>
        [Test]
        public async Task GetPagedAsync_WithLargePageSize_ReturnsAllAvailable()
        {
            // ARRANGE - Create 5 products
            for (int i = 1; i <= 5; i++)
            {
                await _repository.CreateAsync(new Product
                {
                    Name = $"Product {i}",
                    ProductCode = Guid.NewGuid()
                });
            }

            // ACT
            var (items, totalCount) = await _repository.GetPagedAsync(1, 1000);

            // ASSERT
            Assert.That(items, Has.Count.EqualTo(5), "Should return all 5 items even though we asked for 1000");
            Assert.That(totalCount, Is.EqualTo(5));
        }
    }
}

// ==================== HOW TO USE THIS FILE ====================

/*
STEP 1: Copy this file to your test project
  Location: WebAPI_ModNunit.Tests/Repositories/ProductRepositoryExampleTests.cs

STEP 2: Run the tests to see which ones FAIL
  Command: dotnet test

STEP 3: Read the FAILING tests carefully
  - Tests with "Intentional Error" comments show bugs in the code
  - Your job is to FIX the ProductRepository to make these tests PASS

STEP 4: Update ProductRepository.cs to fix the bugs
  - GetPagedAsync needs parameter validation
  - CreateAsync needs null checking (it already does, but verify!)

STEP 5: Run tests again
  Command: dotnet test
  
STEP 6: When all tests PASS, you've fixed the bugs!

STEP 7: Write your own tests for other repositories
  - Copy this structure
  - Adapt for CustomerRepository, OrderRepository, etc.
  - Look for "TUTOR NOTE: Intentional Error" comments in the code
  - Write tests that would catch these errors

STEP 8: Fix each bug and make tests pass
  - This is Test-Driven Development (TDD)!
*/

