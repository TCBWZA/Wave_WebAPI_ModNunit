# GitHub Copilot Instructions for .NET 8 Application

## Project Overview

### Purpose and Goals
<!-- Add a brief description of what this project does and its main goals -->

### Target Framework
- **.NET Version**: .NET 8
- **Platform**: Windows x64
- **Application Type**: Web API

---

## Code Style and Conventions

### Language Standards
<!-- Specify C# version, nullable reference types usage, etc. -->
- **C# Version**: 12
- **Nullable Reference Types**: Enable
- **Target Culture**: UK

### Naming Conventions
<!-- Document your naming standards -->
- **Classes**: Best Practice
- **Methods**: Best Practice
- **Variables**: Best Practice
- **Constants**: Best Practice
- **Private Fields**: Best Practice

### Formatting and Indentation
<!-- Specify spacing, indentation style, etc. -->
- **Indentation**: 4 spaces
- **Line Length**: 120
- **Braces**: Allman style

### Code Organization
<!-- How should code be organized within files and projects -->
- **Using Statements**: 
- **Namespace Organization**: 
- **File Structure**: 

---

## Architecture and Design Patterns

### Project Structure
Each class should be in its own file, and files should be organized into folders based on their functionality (e.g., Controllers, Services, Models).
Extensions methods should be placed in a separate folder named "Extensions" to maintain a clean and organized project structure.
Global Functions should be defined in a dedicated folder named "GlobalFunctions" to ensure they are easily accessible and maintainable across the project.
Basic clean code rules should be followed, such as keeping methods short and focused, avoiding deep nesting, and using meaningful names for variables and methods.



### Dependency Injection
<!-- Explain your DI setup and conventions -->
Use constructor injection for all dependencies. Avoid using service locators or static service access.

---

## Testing Guidelines

### Testing Framework

This is an example for students to apply tests to. They functions and are to be fully implemented but with specific intentional errors to practice ASSERT based tests.

---

## Async and Concurrency

### Async/Await Patterns
All calls should use Async/Await patterns to ensure non-blocking code. Avoid using .Result or .Wait() on asynchronous calls.

### Threading Considerations
None

### Cancellation Tokens
Use cancellation tokens for long-running operations to allow for graceful shutdowns and responsiveness.

---

## Dependency Management

### Key Dependencies
<!-- List critical NuGet packages and their purpose -->
- **Package Name**: Purpose
- 

### Version Constraints
<!-- Any specific version requirements -->

### Updating Dependencies
<!-- Guidelines for updating NuGet packages -->

---

## Security Practices

### Authentication and Authorization
Not Applicable

### Input Validation
This should be done using Data Annotations for model validation in ASP.NET Core. Ensure that all user input is validated to prevent common vulnerabilities such as SQL injection and cross-site scripting (XSS).
Where there is a complex validation requirement, custom validation attributes should be implemented to encapsulate the validation logic and keep the code clean.


### Sensitive Data Handling
Not Applicable

### Common Security Patterns
Basic security best practices should be followed, such as using HTTPS, implementing proper error handling to avoid information disclosure, and regularly updating dependencies to patch known vulnerabilities.

---

## Performance Considerations
Not Applicable
### Performance Priorities
There are none as this is more focused on learning tests and clean code practices rather than performance optimization.

### Optimization Guidelines
<!-- Key performance optimization rules -->

### Memory Management
<!-- Any specific memory management concerns -->

### Caching Strategy
<!-- If applicable, describe caching approach -->

---

## Logging and Monitoring

### Logging Framework
ILogger should be used for logging throughout the application. This allows for consistent logging practices and easy integration with various logging providers.

### Log Levels and Usage
Allow and add different levels of logging (e.g., Information, Warning, Error) to provide insights into the application's behavior and help with troubleshooting.

### Structured Logging
Use structured logging to capture relevant context and make it easier to query logs for specific events or issues.

---

## Error Handling

### Exception Handling Strategy
Functions should be designed to throw exceptions when they encounter errors, rather than returning error codes. This allows for clearer and more maintainable error handling. Exceptions should be specific and provide meaningful messages to aid in debugging.
Main loops should be wrapped in try-catch blocks to handle exceptions gracefully and prevent the application from crashing. This ensures that errors are logged appropriately and that the application can continue running or shut down gracefully as needed.

### Custom Exceptions
<!-- Any project-specific custom exceptions -->

### Error Messages
<!-- Guidelines for error message clarity -->

---

## Common Patterns and Examples

### Service Implementation Pattern
<!-- Example of how to structure services -->

### Repository Pattern
Use where applicable to abstract data access logic and promote separation of concerns. Repositories should be defined as interfaces and implemented in separate classes to allow for easy testing and maintainability.
<!-- If using repositories, document the pattern -->

### API Controller Pattern
<!-- If building APIs, document controller pattern -->

### Configuration Management
appSettings.json should be used to manage application configuration. This allows for centralized management of configuration settings and supports different configurations for various environments (e.g., Development, Staging, Production). Configuration values should be accessed through strongly-typed classes to ensure type safety and ease of use throughout the application.
this should be done using the Options pattern in ASP.NET Core, which provides a clean and organized way to manage configuration settings and promotes separation of concerns.


---

## Documentation Standards

### Code Documentation
	- **Required for**: Instructions, public methods, and any complex logic 
	- **Format**: XML

### README and Documentation
A readme is required it should only detail the purpose of the project and how to run it. It should not include any implementation details or instructions on how to write code for the project as this is meant to be a learning exercise for students to apply their knowledge of clean code practices and testing.
Documentation should focus on the en goal of given the students something to learn NUnit tests and clean code practices rather than providing detailed instructions on how to implement the code. The readme should be concise and to the point, providing just enough information for students to understand the purpose of the project and how to run it.

### Inline Comments
When highlighting a note for students include a comment in the code with the following format: ```csharp //

---

## Environment-Specific Configuration

### Development
Detail what Nuget packages and tools should be used in development, as well as any specific configurations for the development environment.

### Staging
Not Applicable

### Production
Not Applicable

---

## CI/CD and Deployment

### Build Process
<!-- Build and compilation requirements -->

### Deployment Process
<!-- How code is deployed -->

### Version Management
<!-- Versioning scheme -->

---

## Do's and Don'ts

### Do:
- Create demo functions that will fail tests to allow students to practice writing tests and applying clean code principles. - Follow clean code practices, such as keeping methods short and focused, using meaningful names for variables and methods, and avoiding deep nesting. - Use the Options pattern for configuration management to promote separation of concerns and maintainability. - Implement proper error handling by throwing exceptions with meaningful messages and wrapping main loops in try-catch blocks to ensure graceful error handling.
- Include null handling that has been done incorrectly to allow students to practice writing tests that catch null reference exceptions and apply proper null handling techniques.
- Create extension methods that are intentionally designed to be used incorrectly, allowing students to practice writing tests that identify and correct misuse of extension methods.
- Include global functions that have been implemented with intentional errors or design flaws, providing students with the opportunity to write tests that identify and fix issues in global functions, as well as apply clean code principles to improve their design and implementation.
- Include comments in the code that highlight specific issues or areas for improvement, allowing students to practice writing tests that address those issues and apply clean code principles to enhance the overall quality of the codebase.
- Where functions are provided as a starting point for students to practice writing tests, ensure that they are fully implemented but contain specific intentional errors or design flaws. This allows students to apply their knowledge of testing and clean code principles to identify and fix issues in the code, rather than simply writing tests for incomplete or non-functional code.
- Namespaces should be changed to be consistent with the UK target culture, using British English spelling and conventions where applicable. This provides an opportunity for students to practice writing tests that ensure consistency in naming conventions and cultural considerations within the codebase.
- Ensure that the code is straightforward and easy to understand, allowing students to focus on writing tests and applying clean code principles without being distracted by complex logic or advanced C# features. The goal is for students to learn how to write effective tests and apply clean code practices, rather than testing their knowledge of C# syntax or language features.
-

### Don't:
- This is not a test for C# language skills. The idea is for the tests to highlight the importance of writing tests and applying clean code principles, rather than testing students' knowledge of C# syntax or language features. Therefore, the code should be straightforward and not require advanced C# knowledge to understand or work with.
- Include any complex logic or advanced C# features that may distract from the main goal of teaching students how to write tests and apply clean code principles. The focus should be on creating simple, clear code that allows students to easily identify and fix issues through testing.
- Provide detailed instructions or guidance on how to implement the code, as this is meant to be a learning exercise for students to apply their knowledge and problem-solving skills. The code should be self-explanatory and allow students to explore and learn through trial and error, rather than relying on step-by-step instructions.


---

## Additional Resources

### Key Documentation Links
<!-- Links to relevant documentation, wikis, etc. -->
- 

### Team Standards
<!-- Links to team-wide coding standards if applicable -->

### Useful Tools and Commands
<!-- Common commands or tools used in the project -->

---

## Contact and Questions

### Primary Maintainer
<!-- Who to contact for questions -->

### Code Review Standards
<!-- Any specific code review requirements -->

---

**Last Updated**: <!-- Add date when this file is finalized -->
