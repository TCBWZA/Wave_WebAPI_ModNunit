# WebAPI_ModNunit.Tests - Unit Test Project

WebAPI_ModNunit is where you'll write tests to catch intentional bugs in the main application code. You should also write tests for other things that will work. A primer has been generated to guide you.

## Project Structure

```
WebAPI_ModNunit.Tests/
├── Repositories/
│   ├── ProductRepositoryExampleTests.cs     ← START HERE! Worked example
│   ├── CustomerRepositoryTests.cs           ← Your test file
│   ├── OrderRepositoryTests.cs              ← Your test file
│   └── ...
├── Controllers/
│   ├── ProductsControllerTests.cs           ← Your test file
│   ├── CustomersControllerTests.cs          ← Your test file
│   └── ...
├── Mappings/
│   └── MappingExtensionsTests.cs            ← Your test file
├── Validators/
│   └── OrderDtoValidatorsTests.cs           ← Your test file
└── WebAPI_ModNunit.Tests.csproj
```

## Getting Started

### 1. Understand the Example Tests

Start by reading and running `ProductRepositoryExampleTests.cs`:

```bash
dotnet test --filter "ProductRepositoryExampleTests"
```

This file demonstrates:
- ✓ How to set up test fixtures with NUnit
- ✓ How to use an in-memory database for testing
- ✓ How to write successful scenario tests
- ✓ How to write failure scenario tests
- ✓ How to test exceptions

### 2. Identify Intentional Errors

Look for `// TUTOR NOTE: Intentional Error` comments in:
- `WebAPI_ModNunit/Repositories/*.cs` - Repository bugs
- `WebAPI_ModNunit/Controllers/*.cs` - Controller bugs  
- `WebAPI_ModNunit/Mappings/*.cs` - Mapping bugs
- `WebAPI_ModNunit/Validators/*.cs` - Validation bugs

### 3. Write Tests to Catch Bugs

Follow this pattern:
1. **Write a test** that expects the current bug behavior
2. **Run the test** - it should FAIL
3. **Fix the code** in the main project
4. **Run the test** again - it should PASS ✓

This is **Test-Driven Development (TDD)**!

### 4. Create Test Classes

Create a test class for each component you want to test:

```csharp
[TestFixture]
public class CustomerRepositoryTests
{
    private CustomerRepository _repository;
    private AppDbContext _context;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        _context = new AppDbContext(options);
        _repository = new CustomerRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }

    [Test]
    public async Task [MethodName]_With[Condition]_[ExpectedBehavior]()
    {
        // ARRANGE
        
        // ACT
        
        // ASSERT
    }
}
```

## Running Tests

### Run all tests
```bash
dotnet test
```

### Run specific test class
```bash
dotnet test --filter "ProductRepositoryTests"
```

### Run with verbose output
```bash
dotnet test --verbosity detailed
```

### Run a single test method
```bash
dotnet test --filter "GetByIdAsync_WithValidId_ReturnsProduct"
```

## Testing Checklist

For each component with intentional errors, ensure you write tests for:

### ✓ Null Handling
- [ ] Null parameter throws ArgumentNullException
- [ ] Exception message is helpful
- [ ] Null return values are handled correctly

### ✓ Input Validation
- [ ] Invalid parameters (0, negative, empty) are rejected
- [ ] ArgumentException is thrown with descriptive message
- [ ] Boundary values are validated (min/max)

### ✓ Success Scenarios
- [ ] Valid input returns expected result
- [ ] Data is persisted correctly
- [ ] Related entities are updated properly

### ✓ Failure Scenarios
- [ ] Not-found returns null or throws appropriate exception
- [ ] Empty collections return empty list
- [ ] Invalid data is rejected

### ✓ Edge Cases
- [ ] Very large values (pagination with huge page size)
- [ ] Empty/whitespace strings
- [ ] Default/zero values
- [ ] Boundary conditions

## Intentional Errors to Find and Fix

### Repository Errors

**CustomerRepository.GetByIdAsync()**
- ❌ Missing `AsNoTracking()` for read-only operations
- ✓ Fix: Add `AsNoTracking()` before `FirstOrDefaultAsync()`

**ProductRepository.GetPagedAsync()**
- ❌ No validation for `page` and `pageSize` parameters
- ✓ Fix: Add validation to ensure page >= 1 and pageSize > 0

**MappingExtensions.OrderDto()**
- ❌ No null check on `order` parameter
- ❌ No null checks on `BillingAddress` and `DeliveryAddress`
- ✓ Fix: Add null checks and handle gracefully

**OrderDtoValidators**
- ❌ OrderItems validation incomplete
- ❌ No validation for quantity > 0 or price >= 0
- ✓ Fix: Add comprehensive item validation rules

## Useful Testing Patterns

### Testing Async Methods
```csharp
[Test]
public async Task MethodName_WithValidInput_ReturnsExpectedResult()
{
    var result = await _repository.GetByIdAsync(1);
    Assert.That(result, Is.Not.Null);
}
```

### Testing Exceptions
```csharp
[Test]
public void MethodName_WithInvalidInput_ThrowsException()
{
    var ex = Assert.ThrowsAsync<ArgumentException>(() =>
        _repository.CreateAsync(null));
    
    Assert.That(ex.Message, Does.Contain("cannot be null"));
}
```

### Testing Collections
```csharp
[Test]
public async Task GetAllAsync_ReturnsAllItems()
{
    var result = await _repository.GetAllAsync();
    
    Assert.That(result, Is.Not.Empty);
    Assert.That(result, Has.Count.GreaterThan(0));
    Assert.That(result, Does.Contain(expectedItem));
}
```

### Testing Pagination
```csharp
[Test]
public async Task GetPagedAsync_WithValidPage_ReturnsPaginatedResults()
{
    var (items, totalCount) = await _repository.GetPagedAsync(1, 10);
    
    Assert.That(items, Has.Count.LessThanOrEqualTo(10));
    Assert.That(totalCount, Is.GreaterThanOrEqualTo(0));
}
```

## Assertion Libraries

This project includes both:
- **NUnit** - Standard assertion library
- **FluentAssertions** - More readable assertions (optional)

### NUnit Example
```csharp
Assert.That(value, Is.EqualTo(5));
Assert.That(text, Does.Contain("hello"));
Assert.That(list, Has.Count.EqualTo(3));
```

### FluentAssertions Example
```csharp
value.Should().Be(5);
text.Should().Contain("hello");
list.Should().HaveCount(3);
```

Both work great! Use whichever you prefer.

## Learning Resources

### In This Project
- `ProductRepositoryExampleTests.cs` - Detailed example with explanations
- `STUDENT_TESTING_GUIDE.md` - Comprehensive testing guide
- `QUICK_START_TESTING.md` - Quick reference

### Official Documentation
- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [Microsoft Unit Testing Guide](https://learn.microsoft.com/en-us/dotnet/core/testing/)

## Common Mistakes to Avoid

❌ **Don't** test implementation details
✓ **Do** test behavior and results

❌ **Don't** forget to clean up in TearDown
✓ **Do** dispose resources properly

❌ **Don't** have dependencies between tests
✓ **Do** make each test independent

❌ **Don't** use hard-coded IDs
✓ **Do** create test data that's independent of database state

❌ **Don't** test multiple things in one test
✓ **Do** follow the Single Responsibility Principle

## Debugging Failed Tests

When a test fails:

1. **Read the error message carefully** - It often tells you what went wrong
2. **Check the ASSERT statements** - Did the actual value not match expected?
3. **Use Debug mode** - Set breakpoints in test and step through code
4. **Verify test data** - Is your ARRANGE section correct?
5. **Check the main code** - Does it have the bug the test is checking for?

## Next Steps

1. ✓ Run `ProductRepositoryExampleTests.cs` to understand the pattern
2. ✓ Create test files for each repository/controller
3. ✓ Write tests that identify the intentional errors
4. ✓ Fix the bugs in the main code
5. ✓ Make all tests pass ✅

---

**Happy Testing!** 🧪

Remember: Good tests make you confident your code works. Great tests catch bugs before they reach production!

