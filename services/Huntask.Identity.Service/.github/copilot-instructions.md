# Copilot Coding Instructions: Microsoft .NET C# Coding Conventions

1. **General Principles**
    - Write code that is correct, clear, consistent, and easy to maintain.
    - Prefer modern C# language features and avoid outdated constructs.
    - Use async/await for I/O-bound operations and LINQ for collections.

2. **Naming and Layout**
    - Use PascalCase for class, record, and method names.
    - Use camelCase for local variables and parameters (except record primary constructor parameters, which use PascalCase).
    - Use two spaces for indentation, not tabs.
    - Use the Allman style for braces (open/close on their own lines).
    - Limit lines to 65 characters for readability.

3. **Namespaces and Usings**
    - Use file-scoped namespace declarations.
    - Place all using directives outside the namespace declaration.

4. **Variables and Types**
    - Use implicit typing (`var`) only when the type is obvious from the right side.
    - Use explicit types when the type is not clear.
    - Use language keywords (`string`, `int`, etc.) instead of .NET type names.

5. **Strings**
    - Use string interpolation for concatenation.
    - Prefer raw string literals for multi-line or escape-heavy strings.
    - Use StringBuilder for appending strings in loops.

6. **Collections and Initialization**
    - Use collection expressions to initialize collections.
    - Use object initializers for object creation.

7. **Delegates and Events**
    - Use `Func<>` and `Action<>` instead of custom delegate types when possible.
    - Use lambda expressions for event handlers that do not need to be removed.

8. **Exception Handling**
    - Use try-catch for exception handling; only catch exceptions you can handle.
    - Use specific exception types, not general `Exception`.
    - Use the `using` statement or new using declaration syntax for disposables.

9. **Operators and Expressions**
    - Use `&&` and `||` for logical operations, not `&` or `|` for comparisons.
    - Use parentheses to clarify complex expressions.

10. **LINQ**
    - Use meaningful variable names in queries.
    - Use implicit typing for LINQ query variables.
    - Align query clauses under the `from` clause.
    - Use `where` before other query clauses.

11. **Comments**
    - Use single-line comments (`//`) for brief explanations.
    - Use XML comments for public APIs.
    - Place comments on their own line, start with uppercase, end with a period.

12. **Security**
    - Follow secure coding guidelines as per Microsoft documentation.
