# Commit Message Prompt: Conventional Commits

Follow these instructions to write well-formed, short, and descriptive commit messages using the Conventional Commits specification. Use this as a guide for commit message suggestions:

## Format

```
<type>[optional scope]: <short description>

[optional body]
[optional footer]
```

- **type**: The type of change (see below).
- **scope**: (Optional) The area of the codebase affected (e.g., service, module, file).
- **short description**: A concise summary of the change (max 65 characters, lowercase, no period).
- **body**: (Optional) More detailed explanation, wrapped at 72 characters.
- **footer**: (Optional) Issues closed, breaking changes, etc.

## Types
- feat: A new feature
- fix: A bug fix
- docs: Documentation only changes
- style: Changes that do not affect meaning (formatting, etc.)
- refactor: Code change that neither fixes a bug nor adds a feature
- perf: Performance improvement
- test: Adding or correcting tests
- build: Changes to build system or dependencies
- ci: Changes to CI configuration
- chore: Other changes that don't modify src or test files
- revert: Reverts a previous commit

## Examples
- feat(auth): add JWT token validation middleware
- fix(todo): correct null reference in task creation
- docs(readme): update usage instructions
- style: format code with dotnet format
- refactor: extract user service logic
- test(todo): add integration tests for endpoints
- build: update nuget dependencies
- ci: add GitHub Actions workflow
- chore: update .gitignore
- revert: revert "feat(auth): add JWT token validation middleware"

## Tips
- Use the imperative mood ("add", not "added" or "adds").
- Be specific and concise.
- Reference issues in the footer if applicable (e.g., "Closes #123").
- Use lowercase for type and scope.
- Avoid generic messages like "update code" or "fix stuff".

For more details, see [Conventional Commits](https://www.conventionalcommits.org/).
