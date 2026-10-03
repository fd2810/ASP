# Practical 2 – Creating Testing Projects in ASP.NET Core / MVC

**Aim:** Create a unit test project using **xUnit**, write tests for a Calculator class, and run them in Visual Studio.

---

## Step 1: Create Main ASP.NET Project

1. Open **Visual Studio 2019**.
2. Click **Create a new project**.
3. Select **ASP.NET Core Web Application** (or ASP.NET Web Application .NET Framework).
4. Click **Next**.
5. Configure:
   - **Project Name:** `StudentManagement`
   - Location: Choose any folder
6. Click **Create**.
7. Select **MVC** template → **Create**.

---

## Step 2: Add Test Project

1. In **Solution Explorer**, right-click the **Solution**.
2. Select **Add → New Project**.
3. Search for **xUnit Test Project (.NET Core)** (or .NET Framework).
4. Click **Next**.
5. **Project Name:** `StudentManagement.Tests`
6. Click **Create**.

Your solution now contains:

```
StudentManagement
StudentManagement.Tests
```

---

## Step 3: Add Project Reference

1. Right-click `StudentManagement.Tests`.
2. Select **Add → Project Reference**.
3. Check `StudentManagement`.
4. Click **OK**.

---

## Step 4: Install NuGet Packages (if needed)

Right-click `StudentManagement.Tests` → **Manage NuGet Packages** → Install:

- `xunit`
- `xunit.runner.visualstudio`
- `Microsoft.NET.Test.Sdk`

---

## Step 5: Create Calculator Class (Main Project)

### File: `Calculator.cs` (inside StudentManagement project)

```csharp
using System;

namespace StudentManagement
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }
    }
}
```

---

## Step 6: Create Unit Test Class

### File: `CalculatorTests.cs` (inside StudentManagement.Tests)

```csharp
using Xunit;
using StudentManagement;

namespace StudentManagement.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            // Arrange
            Calculator c = new Calculator();

            // Act
            int result = c.Add(10, 20);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void Multiply_TwoNumbers_ReturnsProduct()
        {
            // Arrange
            Calculator c = new Calculator();

            // Act
            int result = c.Multiply(5, 4);

            // Assert
            Assert.Equal(20, result);
        }

        // Multiple test cases using Theory
        [Theory]
        [InlineData(10, 20, 30)]
        [InlineData(5, 5, 10)]
        [InlineData(100, 50, 150)]
        public void Add_TestCases(int a, int b, int expected)
        {
            Calculator c = new Calculator();
            int result = c.Add(a, b);
            Assert.Equal(expected, result);
        }
    }
}
```

---

## Step 7: Run the Tests

1. Open **Test → Test Explorer**.
2. Click **Run All**.

**Expected Output:**
```
Passed: 3 (or more)
Failed: 0
```

Green check marks = successful tests.

---

## Extra Example – Failed Test

```csharp
[Fact]
public void Add_Test_ShouldFail()
{
    Calculator c = new Calculator();
    int result = c.Add(10, 20);
    Assert.Equal(50, result);   // Wrong expected value
}
```

Result:
```
Failed
Expected: 50
Actual: 30
```

---

## Understanding AAA Pattern

| Phase   | Meaning                              |
|---------|--------------------------------------|
| Arrange | Create objects and prepare data      |
| Act     | Call the method being tested         |
| Assert  | Verify the result is correct         |

---

## Project Structure

```
Solution
├── StudentManagement
│   └── Calculator.cs
└── StudentManagement.Tests
    └── CalculatorTests.cs
```
