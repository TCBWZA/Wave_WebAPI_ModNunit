# WebAPI_ModNunit - Complete Learning Path

## 📚 Documentation Overview

This project includes comprehensive documentation to help you learn unit testing and clean code practices. Here's where to find everything:

### For Students Starting Out

1. **START HERE**: `QUICK_START_TESTING.md`
   - 30-second project setup instructions
   - Cheat sheets for NUnit and FluentAssertions
   - Copy-paste command examples

2. **THEN READ**: `STUDENT_TESTING_GUIDE.md`
   - Comprehensive introduction to unit testing
   - NUnit assertion patterns explained
   - NUnit vs FluentAssertions comparison
   - Step-by-step test project creation

3. **WORK THROUGH**: `WebAPI_ModNunit.Tests/Repositories/ProductRepositoryExampleTests.cs`
   - Detailed, commented example tests
   - Shows how to structure test files
   - Demonstrates all major testing patterns
   - Includes instructions for finding and fixing intentional errors

4. **REFERENCE**: `WebAPI_ModNunit.Tests/README.md`
   - Project structure overview
   - Running tests commands
   - Testing checklist
   - Common testing patterns


## 🎯 Learning Path

### Step 1: Setup (5 minutes)
```bash
1. Follow steps in QUICK_START_TESTING.md
2. Run: dotnet new nunit -n WebAPI_ModNunit.Tests
3. Run: dotnet sln add WebAPI_ModNunit.Tests/WebAPI_ModNunit.Tests.csproj
4. Run: dotnet test
```

### Step 2: Understand (30 minutes)
- Read `STUDENT_TESTING_GUIDE.md` sections 1-3
- Understand what unit testing is
- Learn the Arrange-Act-Assert pattern
- Review NUnit assertion syntax

### Step 3: Learn by Example (45 minutes)
- Read `ProductRepositoryExampleTests.cs` line by line
- Understand each test's purpose
- Run the tests: `dotnet test`
- See which tests pass and which fail

### Step 4: Write Your Own Tests (ongoing)
- Copy test structure from example
- Create test classes for each repository
- Write tests that identify intentional errors
- Fix the code to make tests pass

### Step 5: Explore Variations (ongoing)
- Try FluentAssertions syntax
- Test edge cases and boundary conditions
- Write tests for controllers
- Test validation logic

---

## 🐛 Intentional Errors to Find and Fix

The application contains specific intentional errors for you to discover through testing:

### ✓ Repositories

| File | Error | How to Test |
|------|-------|-----------|
| `CustomerRepository.GetByIdAsync()` | Missing `AsNoTracking()` | Test that read operations don't track entities |
| `ProductRepository.GetPagedAsync()` | No parameter validation | Test with page=0 or pageSize=0 |
| `CustomerRepository.UpdateAsync()` | No null check | Test passing null and expect exception |

### ✓ Controllers

| File | Error | How to Test |
|------|-------|-----------|
| `ProductsController.GetById()` | No ID validation | Test with negative or zero ID |

### ✓ Mappings

| File | Error | How to Test |
|------|-------|-----------|
| `MappingExtensions.OrderDto()` | No null checks | Test with null order/addresses |

### ✓ Validators

| File | Error | How to Test |
|------|-------|-----------|
| `OrderDtoValidators` | Incomplete OrderItems validation | Test with quantity=0 or price<0 |

---

## 📋 Example: From Test to Fix

### Step 1: Write a Test (Based on ProductRepositoryExampleTests)

```csharp
[Test]
public void GetPagedAsync_WithZeroPage_ThrowsArgumentException()
{
    var ex = Assert.ThrowsAsync<ArgumentException>(() =>
        _repository.GetPagedAsync(0, 10));
    
    Assert.That(ex.Message, Does.Contain("page"));
}
```

### Step 2: Run the Test

```bash
dotnet test --filter "GetPagedAsync_WithZeroPage"
```

**Result**: Test FAILS ❌ (because code doesn't validate page)

### Step 3: Fix the Code

In `ProductRepository.GetPagedAsync()`:

```csharp
public async Task<(List<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
{
    // ADD VALIDATION HERE
    if (page < 1)
        throw new ArgumentException("Page must be >= 1.", nameof(page));
    
    if (pageSize < 1)
        throw new ArgumentException("PageSize must be > 0.", nameof(pageSize));
    
    // ... rest of method
}
```

### Step 4: Run the Test Again

```bash
dotnet test --filter "GetPagedAsync_WithZeroPage"
```

**Result**: Test PASSES ✅

---

## 🏗️ Project Architecture

```
Solution Root/
│
├── WebAPI_ModNunit/                     ← Main Application
│   ├── Controllers/                     ← API endpoints (some bugs)
│   ├── Models/                          ← Domain models (production quality)
│   ├── DTOs/                            ← Data transfer objects
│   ├── Repositories/                    ← Data access (some bugs)
│   ├── Mappings/                        ← Entity-to-DTO mappings (some bugs)
│   ├── Validators/                      ← Validation logic (some bugs)
│   ├── SeedSettings.cs                  ← Validated config (production quality)
│   ├── Bogus.cs                         ← Test data generation (production quality)
│   └── Program.cs                       ← Application setup (production quality)
│
├── WebAPI_ModNunit.Tests/               ← Unit Tests (YOU CREATE THIS!)
│   ├── Repositories/
│   │   ├── ProductRepositoryExampleTests.cs    ← Worked example, READ FIRST!
│   │   ├── CustomerRepositoryTests.cs          ← You create
│   │   ├── OrderRepositoryTests.cs             ← You create
│   │   └── ...
│   ├── Controllers/
│   │   ├── ProductsControllerTests.cs          ← You create
│   │   └── ...
│   ├── Mappings/
│   │   └── MappingExtensionsTests.cs           ← You create
│   ├── Validators/
│   │   └── OrderDtoValidatorsTests.cs          ← You create
│   ├── WebAPI_ModNunit.Tests.csproj
│   └── README.md
│
├── STUDENT_TESTING_GUIDE.md             ← Comprehensive guide (READ THIS!)
├── QUICK_START_TESTING.md               ← Quick reference (START HERE!)
└── README.md                            ← Solution overview
```

---

## 🎓 What You'll Learn

### Testing Concepts
- ✓ Unit testing fundamentals
- ✓ Arrange-Act-Assert pattern
- ✓ Test structure and organization
- ✓ Assertion methods and patterns
- ✓ Exception testing
- ✓ Collection testing
- ✓ Mocking and test data
- ✓ Test-Driven Development (TDD)

### C# / .NET Skills
- ✓ Entity Framework Core data access
- ✓ Repository pattern
- ✓ Async/await patterns
- ✓ In-memory database testing
- ✓ Exception handling
- ✓ Input validation
- ✓ Null handling and defensive programming

### Code Quality
- ✓ Identifying code smells
- ✓ Finding bugs through testing
- ✓ Writing maintainable code
- ✓ Following clean code principles
- ✓ Defensive programming techniques

---

## 📞 Quick Reference

### Most Common Commands

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ProductRepositoryTests"

# Run specific test method
dotnet test --filter "GetByIdAsync_WithValidId"

# Run with detailed output
dotnet test --verbosity detailed

# Watch mode (re-run on file change)
dotnet watch test
```

### Most Common NUnit Assertions

```csharp
Assert.That(actual, Is.EqualTo(expected));
Assert.That(value, Is.Null);
Assert.That(value, Is.Not.Null);
Assert.That(condition, Is.True);
Assert.That(list, Is.Not.Empty);
Assert.That(list, Has.Count.EqualTo(5));
Assert.ThrowsAsync<ArgumentException>(() => method());
```

### Most Common FluentAssertions

```csharp
actual.Should().Be(expected);
value.Should().BeNull();
value.Should().NotBeNull();
condition.Should().BeTrue();
list.Should().NotBeEmpty();
list.Should().HaveCount(5);
```

---

## 🚀 Getting Started Now

### Right Now (Next 5 minutes)

1. Open `QUICK_START_TESTING.md` in your editor
2. Copy-paste the setup commands
3. Create your test project
4. Run `dotnet test` to verify setup

### Next (Next 30 minutes)

1. Open `STUDENT_TESTING_GUIDE.md`
2. Read sections 1-5 (Project Structure through Introduction)
3. Skim the assertion examples

### After That (Next hour)

1. Open `ProductRepositoryExampleTests.cs`
2. Read it line by line
3. Understand what each test does
4. Run `dotnet test` to see results

### Then Start Testing (Ongoing)

1. Create `CustomerRepositoryTests.cs`
2. Follow the pattern from ProductRepositoryExampleTests
3. Write tests that identify bugs
4. Fix the bugs to make tests pass

---

## 💡 Pro Tips

✓ **Run tests frequently** - After every code change  
✓ **Write one test at a time** - Don't write a bunch and then fix  
✓ **Read test output carefully** - It tells you what's wrong  
✓ **Use descriptive test names** - Should explain what's being tested  
✓ **Keep tests independent** - One test's failure shouldn't affect others  
✓ **Test behaviors, not implementation** - Focus on what, not how  
✓ **Don't skip edge cases** - They often hide bugs  

---

## 📚 Additional Resources

- [Microsoft Unit Testing Guide](https://learn.microsoft.com/en-us/dotnet/core/testing/)
- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [Test-Driven Development](https://en.wikipedia.org/wiki/Test-driven_development)

---

## ✅ Success Criteria

By the end of this project, you should:

- ✓ Understand unit testing and its importance
- ✓ Write effective tests using NUnit
- ✓ Use appropriate assertions for different scenarios
- ✓ Follow the Arrange-Act-Assert pattern
- ✓ Find and fix intentional code bugs
- ✓ Apply clean code principles
- ✓ Practice Test-Driven Development

---

**Start with QUICK_START_TESTING.md now!** 🚀

