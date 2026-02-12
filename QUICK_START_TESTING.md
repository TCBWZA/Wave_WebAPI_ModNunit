# Quick Start - Setting Up Your Test Project

## 30-Second Setup

### 1. Create Test Project
```bash
dotnet new nunit -n WebAPI_ModNunit.Tests
```

### 2. Add to Solution
```bash
dotnet sln add WebAPI_ModNunit.Tests/WebAPI_ModNunit.Tests.csproj
```

### 3. Add Project Reference
```bash
cd WebAPI_ModNunit.Tests
dotnet add reference ../WebAPI_ModNunit/WebAPI_ModNunit.csproj
```

### 4. (Optional) Add FluentAssertions
```bash
dotnet add package FluentAssertions
```

### 5. Run Tests
```bash
dotnet test
```

---

## Common NUnit Assertions Cheat Sheet

```csharp
// Equality
Assert.That(actual, Is.EqualTo(expected));
Assert.That(actual, Is.Not.EqualTo(unexpected));

// Null
Assert.That(value, Is.Null);
Assert.That(value, Is.Not.Null);

// Boolean
Assert.That(condition, Is.True);
Assert.That(condition, Is.False);

// Numbers
Assert.That(value, Is.GreaterThan(0));
Assert.That(value, Is.LessThan(100));
Assert.That(value, Is.GreaterThanOrEqualTo(0));
Assert.That(value, Is.LessThanOrEqualTo(100));

// Collections
Assert.That(list, Is.Empty);
Assert.That(list, Is.Not.Empty);
Assert.That(list, Has.Count.EqualTo(5));
Assert.That(list, Does.Contain(item));

// Strings
Assert.That(text, Does.Contain("substring"));
Assert.That(text, Does.StartWith("prefix"));
Assert.That(text, Does.EndWith("suffix"));

// Exceptions
Assert.ThrowsAsync<ArgumentNullException>(() => method());
Assert.DoesNotThrowAsync(() => method());
```

---

## Common FluentAssertions Cheat Sheet

```csharp
// Equality
actual.Should().Be(expected);
actual.Should().NotBe(unexpected);

// Null
value.Should().BeNull();
value.Should().NotBeNull();

// Boolean
condition.Should().BeTrue();
condition.Should().BeFalse();

// Numbers
value.Should().BeGreaterThan(0);
value.Should().BeLessThan(100);
value.Should().BeGreaterThanOrEqualTo(0);
value.Should().BeLessThanOrEqualTo(100);

// Collections
list.Should().BeEmpty();
list.Should().NotBeEmpty();
list.Should().HaveCount(5);
list.Should().Contain(item);

// Strings
text.Should().Contain("substring");
text.Should().StartWith("prefix");
text.Should().EndWith("suffix");

// Exceptions
await FluentActions.Invoking(() => method())
    .Should().ThrowAsync<ArgumentNullException>();
```

---

## Test File Template

```csharp
using NUnit.Framework;
using WebAPI_ModNunit.Models;
using WebAPI_ModNunit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_ModNunit.Tests.Repositories
{
    [TestFixture]
    public class [EntityName]RepositoryTests
    {
        private [EntityName]Repository _repository;
        private AppDbContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            _context = new AppDbContext(options);
            _repository = new [EntityName]Repository(_context);
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
}
```

---

## Finding Intentional Errors to Test

Check these files for intentional errors marked with `// TUTOR NOTE: Intentional Error`:

1. **CustomerRepository.cs**
   - GetByIdAsync - Missing AsNoTracking()
   - UpdateAsync - No null checking

2. **ProductRepository.cs**
   - GetPagedAsync - No parameter validation

3. **ProductsController.cs**
   - GetById - No ID validation

4. **MappingExtensions.cs**
   - OrderDto ToDto() - No null checks

5. **OrderDtoValidators.cs**
   - CreateOrderDtoValidator - Incomplete OrderItems validation

---

## Project File Dependencies

When you create your test project, ensure it has these dependencies in the `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="NUnit" Version="4.0.1" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.6.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.0" />
</ItemGroup>

<ItemGroup>
    <ProjectReference Include="../WebAPI_ModNunit/WebAPI_ModNunit.csproj" />
</ItemGroup>
```

