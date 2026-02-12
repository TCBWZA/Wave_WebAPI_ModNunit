# Student Testing Guide - WebAPI_ModNunit Project

## Overview

Welcome to the WebAPI_ModNunit project! This project is designed to teach you the fundamentals of **unit testing** and **clean code practices** using the .NET 8 framework.

---

## Learning Objectives

By completing this project, you will:

### Testing & Quality Assurance
✓ Write effective unit tests using **NUnit** framework  
✓ Understand the **Arrange-Act-Assert (AAA)** pattern  
✓ Learn assertion methods and how to verify code behaviour  
✓ Write tests for both positive and negative scenarios  
✓ Practice **Test-Driven Development (TDD)** principles  

### Clean Code Practices
✓ Identify and fix **null reference issues**  
✓ Implement proper **input validation**  
✓ Apply **defensive programming** techniques  
✓ Write meaningful exception messages  
✓ Follow the **Single Responsibility Principle**  
✓ Create readable, maintainable code  

### Real-World Skills
✓ Work with **Entity Framework Core** and data access patterns  
✓ Understand **repository pattern** for data abstraction  
✓ Work with **DTOs** (Data Transfer Objects) for API design  
✓ Implement **validation** in API controllers  
✓ Test **async/await** patterns  
✓ Write tests for **edge cases** and **boundary conditions**  

---

## Project Structure

The application contains **intentional errors** in key areas:

### ❌ Code with Intentional Errors (Your Testing Arena)

**Repositories:**
- `CustomerRepository.cs` - Missing null checks and AsNoTracking issues
- `ProductRepository.cs` - No parameter validation for pagination
- `OrderRepository.cs` - Error handling issues

**Controllers:**
- `ProductsController.cs` - Missing ID validation

**Services & Utilities:**
- `MappingExtensions.cs` - Null reference vulnerabilities
- `OrderDtoValidators.cs` - Incomplete validation logic

### ✅ Production-Quality Code (Learn from These)

**Configuration & Seeding:**
- `SeedSettings.cs` - Excellent example of input validation
- `Bogus.cs` - Professional data generation with error handling
- `Program.cs` - Proper exception handling during startup

---

## Introduction to Unit Testing

### What is Unit Testing?

A **unit test** is an automated test that:
- Tests a **single piece of code** (a method or function)
- Runs quickly and independently
- Verifies the code behaves as expected
- Can be run thousands of times without side effects

### The Arrange-Act-Assert (AAA) Pattern

Every good unit test follows three steps:

```csharp
[Test]
public void GetById_WithValidId_ReturnsProduct()
{
    // ARRANGE - Set up test data and dependencies
    var productRepository = new ProductRepository(dbContext);
    long productId = 1;
    
    // ACT - Execute the method being tested
    var result = await productRepository.GetByIdAsync(productId);
    
    // ASSERT - Verify the result is correct
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Id, Is.EqualTo(productId));
}
```

---

## NUnit Assertions

### Basic NUnit Assertions

NUnit uses the **Assert** class with the **Constraint Model** for assertions. Here are the most common patterns:

#### 1. Equality Assertions

```csharp
// Check if values are equal
Assert.That(actual, Is.EqualTo(expected));

// Check if values are NOT equal
Assert.That(actual, Is.Not.EqualTo(unexpected));

// Example
var product = new Product { Id = 1, Name = "Widget" };
Assert.That(product.Id, Is.EqualTo(1));
Assert.That(product.Name, Is.EqualTo("Widget"));
```

#### 2. Null Assertions

```csharp
// Check if value is null
Assert.That(value, Is.Null);

// Check if value is NOT null
Assert.That(value, Is.Not.Null);

// Example
var customer = await repository.GetByIdAsync(1);
Assert.That(customer, Is.Not.Null);
```

#### 3. Boolean Assertions

```csharp
// Check if condition is true
Assert.That(condition, Is.True);

// Check if condition is false
Assert.That(condition, Is.False);

// Example
bool exists = await repository.ExistsAsync(1);
Assert.That(exists, Is.True);
```

#### 4. Comparison Assertions

```csharp
// Greater than
Assert.That(value, Is.GreaterThan(0));

// Less than
Assert.That(value, Is.LessThan(100));

// Greater than or equal
Assert.That(value, Is.GreaterThanOrEqualTo(0));

// Example
var (items, totalCount) = await repository.GetPagedAsync(1, 10);
Assert.That(totalCount, Is.GreaterThanOrEqualTo(0));
Assert.That(items.Count, Is.LessThanOrEqualTo(10));
```

#### 5. Collection Assertions

```csharp
// Check if collection is empty
Assert.That(collection, Is.Empty);

// Check if collection has items
Assert.That(collection, Is.Not.Empty);

// Check collection count
Assert.That(collection, Has.Count.EqualTo(5));

// Check if collection contains item
Assert.That(collection, Does.Contain(item));

// Example
var products = await repository.GetAllAsync();
Assert.That(products, Is.Not.Empty);
Assert.That(products, Has.Count.GreaterThan(0));
```

#### 6. Exception Assertions

```csharp
// Check if exception is thrown
Assert.ThrowsAsync<ArgumentNullException>(() => 
    repository.CreateAsync(null));

// Check if exception is NOT thrown
Assert.DoesNotThrowAsync(() => 
    repository.CreateAsync(validProduct));

// Example
var ex = Assert.ThrowsAsync<ArgumentException>(() =>
    repository.UpdateAsync(null));
Assert.That(ex.Message, Does.Contain("cannot be null"));
```

#### 7. String Assertions

```csharp
// Check if string contains text
Assert.That(text, Does.Contain("substring"));

// Check if string starts with
Assert.That(text, Does.StartWith("prefix"));

// Check if string ends with
Assert.That(text, Does.EndWith("suffix"));

// Check string is empty
Assert.That(text, Is.Empty);

// Example
var email = "john@example.com";
Assert.That(email, Does.Contain("@"));
```

---

## Fluent Assertions

**FluentAssertions** is an alternative assertion library that provides a **fluent, readable syntax** for assertions.

### Why FluentAssertions?

✓ **More readable** - Reads like English  
✓ **Better error messages** - Tells you exactly what failed  
✓ **Chainable** - Multiple assertions on one line  
✓ **Less verbose** - Fewer parentheses and arguments  

### Fluent Assertions Syntax

#### Installation

First, install the NuGet package:
```
dotnet add package FluentAssertions
```

#### Basic Usage

```csharp
using FluentAssertions;

[Test]
public void GetById_WithValidId_ReturnsProduct()
{
    // ARRANGE
    var product = new Product { Id = 1, Name = "Widget" };
    
    // ACT & ASSERT (Fluent style)
    product.Id.Should().Be(1);
    product.Name.Should().Be("Widget");
    product.Should().NotBeNull();
}
```

### NUnit vs FluentAssertions - Side by Side

#### Equality

```csharp
// NUnit
Assert.That(product.Id, Is.EqualTo(1));

// Fluent Assertions
product.Id.Should().Be(1);
```

#### Null Checks

```csharp
// NUnit
Assert.That(customer, Is.Not.Null);

// Fluent Assertions
customer.Should().NotBeNull();
```

#### Collections

```csharp
// NUnit
Assert.That(products, Has.Count.EqualTo(5));
Assert.That(products, Does.Contain(product));

// Fluent Assertions
products.Should().HaveCount(5);
products.Should().Contain(product);
```

#### Exceptions

```csharp
// NUnit
Assert.ThrowsAsync<ArgumentNullException>(() => 
    repository.CreateAsync(null));

// Fluent Assertions
await FluentActions.Invoking(() => repository.CreateAsync(null))
    .Should().ThrowAsync<ArgumentNullException>();
```

#### Strings

```csharp
// NUnit
Assert.That(text, Does.Contain("hello"));

// Fluent Assertions
text.Should().Contain("hello");
```

#### Comparisons

```csharp
// NUnit
Assert.That(count, Is.GreaterThan(0));
Assert.That(price, Is.LessThanOrEqualTo(100));

// Fluent Assertions
count.Should().BeGreaterThan(0);
price.Should().BeLessThanOrEqualTo(100);
```

### Which Should You Use?

**For this project, we recommend:**
- **Start with NUnit** - It's built-in and sufficient for learning
- **Learn FluentAssertions later** - Better error messages and readability
- **Mix them** - Both can be used in the same project

---

## Creating Your Test Project

### Step 1: Create Test Project from Command Line

Open a terminal in your solution directory and run:

```bash
dotnet new nunit -n WebAPI_ModNunit.Tests
```

This creates a new test project with NUnit framework already configured.

### Step 2: Add Test Project to Solution

```bash
dotnet sln add WebAPI_ModNunit.Tests/WebAPI_ModNunit.Tests.csproj
```

### Step 3: Add Project Reference

Add a reference to the main project in your test project:

```bash
cd WebAPI_ModNunit.Tests
dotnet add reference ../WebAPI_ModNunit/WebAPI_ModNunit.csproj
```

### Step 4: Verify Structure

Your test project should now have:

```
WebAPI_ModNunit.Tests/
├── WebAPI_ModNunit.Tests.csproj
├── UnitTest1.cs (template file - you can delete this)
└── (your test files will go here)
```

### Step 5: Install FluentAssertions (Optional)

If you want to use FluentAssertions alongside NUnit:

```bash
cd WebAPI_ModNunit.Tests
dotnet add package FluentAssertions
```

### Step 6: Run Tests

```bash
dotnet test
```

---

## Your First Test

Here's a template for writing your first test:

```csharp
using NUnit.Framework;
using WebAPI_ModNunit.Models;
using WebAPI_ModNunit.Repositories;

namespace WebAPI_ModNunit.Tests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductRepository _repository;
        private AppDbContext _context;

        [SetUp]
        public void SetUp()
        {
            // ARRANGE - Set up test fixtures
            // Create an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            _context = new AppDbContext(options);
            _repository = new ProductRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_WithValidId_ReturnsProduct()
        {
            // ARRANGE
            var product = new Product 
            { 
                Name = "Test Product",
                ProductCode = Guid.NewGuid()
            };
            await _repository.CreateAsync(product);

            // ACT
            var result = await _repository.GetByIdAsync(product.Id);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Test Product"));
        }

        [Test]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            // ACT
            var result = await _repository.GetByIdAsync(999);

            // ASSERT
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_WithNullProduct_ThrowsException()
        {
            // ACT & ASSERT
            Assert.ThrowsAsync<ArgumentNullException>(() => 
                _repository.CreateAsync(null));
        }
    }
}
```

---

## Testing Checklist

When writing tests for the intentional errors in this project, verify:

### ✓ Null Handling
- [ ] Does the method throw an exception when null is passed?
- [ ] Is the exception message meaningful?
- [ ] Does it handle null return values correctly?

### ✓ Input Validation
- [ ] Does the method validate parameter ranges?
- [ ] Does it reject invalid values (0, negative numbers)?
- [ ] Does it throw ArgumentException with good messages?

### ✓ Boundary Conditions
- [ ] Test with minimum valid value (1 for IDs)
- [ ] Test with maximum safe value
- [ ] Test with edge cases (0, -1, empty string)

### ✓ State Changes
- [ ] Verify objects are correctly created/updated
- [ ] Ensure data persistence works
- [ ] Check that related entities are handled properly

### ✓ Error Handling
- [ ] Verify exceptions are thrown for error conditions
- [ ] Check exception messages are helpful
- [ ] Ensure no silent failures

---

## Common Testing Patterns

### Testing Async Methods

```csharp
[Test]
public async Task GetByIdAsync_WithValidId_ReturnsProduct()
{
    var result = await _repository.GetByIdAsync(1);
    Assert.That(result, Is.Not.Null);
}
```

### Testing Collection Results

```csharp
[Test]
public async Task GetAllAsync_ReturnsAllProducts()
{
    var products = await _repository.GetAllAsync();
    Assert.That(products, Is.Not.Empty);
    Assert.That(products, Has.Count.GreaterThan(0));
}
```

### Testing Pagination

```csharp
[Test]
public async Task GetPagedAsync_WithValidPage_ReturnsPaginatedResults()
{
    // Act
    var (items, totalCount) = await _repository.GetPagedAsync(1, 10);
    
    // Assert
    Assert.That(items, Has.Count.LessThanOrEqualTo(10));
    Assert.That(totalCount, Is.GreaterThanOrEqualTo(0));
}
```

### Testing Exceptions with Messages

```csharp
[Test]
public void CreateAsync_WithNullProduct_ThrowsWithMessage()
{
    var ex = Assert.ThrowsAsync<ArgumentNullException>(() => 
        _repository.CreateAsync(null));
    
    Assert.That(ex.Message, Does.Contain("cannot be null"));
}
```

---

## Next Steps

1. **Create the test project** following Step 1-6 above
2. **Run the template tests** to verify setup works
3. **Write your first test** using the template provided
4. **Identify the intentional errors** in the domain code
5. **Write tests that catch each error**
6. **Fix the code** to make tests pass
7. **Refactor** for cleaner, more maintainable code

---

## Resources

### NUnit Documentation
- Official Docs: https://docs.nunit.org/
- Assertion Models: https://docs.nunit.org/articles/2.0-migration/breaking-changes.html

### FluentAssertions Documentation
- Official Docs: https://fluentassertions.com/
- Cheat Sheet: https://fluentassertions.com/introduction

### Best Practices
- Microsoft Unit Testing: https://learn.microsoft.com/en-us/dotnet/core/testing/
- Test-Driven Development: https://www.agilealliance.org/glossary/tdd/

---

**Happy Testing!** 🧪✅

