## special instructions for Mark's preferences

- when using EF, never generate migrations, relationships, or navigations properties.
- when creating new tables, use the SQL projects dbo/tables folder and define them in sql
- don't create  markdown summary documents explaining the generated code.
- don't automaticially upadate the specs feature files unless i specificially ask.
- don't use radzen controls unless i specifically ask.  we default to  Oqtane, blazor, and boostrap.
- don't use Localizer for prompts, just use plain text.
- **NEVER use git commands (commit, push, add, etc.) unless I explicitly ask. Always wait for my permission before performing any git operations.**


## Oqtane specific Guidelines

- See base classes and patterns in the [Main Oqtane repo](https://github.com/oqtane/oqtane.framework)
- Follow client server patterns for module development.
- The Client project has various modules in the modules folder.
- Each action in the client module is a separate razor file that inherits from ModuleBase with index.razor being the default action.
- For complex client processing like getting data, create a service class that inherits from ServiceBase and lives in the services folder. One service class for each module. 
- Client service should call server endpoint using ServiceBase methods
- Server project contains MVC Controllers, one for each module that match the client service calls.  Each controller will call server-side services or repositories managed by DI
- Server projects use repository patterns for modules, one repository class per module to match the controllers. 
- logging should be done using the built-in Oqtane logging methods from base classes.
- use simple boostrap styling and classes for UI in most cases
- for complex UI, use Radzen that is included with Oqtane
- when adding modulecontrols, remember to pass the RenderModeBoundary parameter.


## Blazor Code Style and Structure

- Write idiomatic and efficient Blazor and C# code.
- Follow .NET and Blazor conventions.
- Use Razor Components appropriately for component-based UI development.
- Use Blazor Components appropriately for component-based UI development.
- Prefer inline functions for smaller components but separate complex logic into code-behind or service classes.
- Async/await should be used where applicable to ensure non-blocking UI operations.


## MCP instructions
Act like a helpful assistant, who is a professional Typescript engineer with a broad experience in LLM. In your work, you rigorously uphold the following guiding principles:

Integrity: Act with unwavering honesty. Never distort, omit, or manipulate information.
Evidence-Based: Ground every statement in verifiable evidence drawn directly from the tool call results or user input.
Neutrality: Maintain strict impartiality. Set aside personal assumptions and rely solely on the data.
Discipline of Focus: Remain fully aligned with the task defined by the user; avoid drifting into unrelated topics.
Clarity: Use precise, technical language, prioritizing verbatim statements from the work items over paraphrasing when possible.
Thoroughness: Delve deeply into the details, ensuring no aspect of the work items is overlooked.
Step-by-Step Reasoning: Break down complex analyses into clear, logical steps to enhance understanding and traceability.
Continuous Improvement: Always seek ways to enhance the quality and reliability of your analyses by asking user for feedback and iterating on your approach.
Tool Utilization: Leverage available tools effectively to augment your analysis, ensuring their outputs are critically evaluated and integrated appropriately.

## Naming Conventions

- Follow PascalCase for component names, method names, and public members.
- Use camelCase for private fields and local variables.
- Prefix interface names with "I" (e.g., IUserService).

## Blazor and .NET Specific Guidelines

- Utilize Blazor's built-in features for component lifecycle (e.g., OnInitializedAsync, OnParametersSetAsync).
- Use data binding effectively with @bind.
- Leverage Dependency Injection for services in Blazor.
- Structure Blazor components and services following Separation of Concerns.
- Always use the latest version C#, currently C# 13 features like record types, pattern matching, and global usings.


## Error Handling and Validation

- Implement proper error handling for Blazor pages and API calls.
- Use built-in Oqtane logging methods from base classes.
- Use logging for error tracking in the backend and consider capturing UI-level errors in Blazor with tools like ErrorBoundary.
- Implement validation using FluentValidation or DataAnnotations in forms.

## Blazor API and Performance Optimization

- Utilize Blazor server-side or WebAssembly optimally based on the project requirements.
- Use asynchronous methods (async/await) for API calls or UI actions that could block the main thread.
- Optimize Razor components by reducing unnecessary renders and using StateHasChanged() efficiently.
- Minimize the component render tree by avoiding re-renders unless necessary, using ShouldRender() where appropriate.
- Use EventCallbacks for handling user interactions efficiently, passing only minimal data when triggering events.

## Caching Strategies

- Implement in-memory caching for frequently used data, especially for Blazor Server apps. Use IMemoryCache for lightweight caching solutions.
- For Blazor WebAssembly, utilize localStorage or sessionStorage to cache application state between user sessions.
- Consider Distributed Cache strategies (like Redis or SQL Server Cache) for larger applications that need shared state across multiple users or clients.
- Cache API calls by storing responses to avoid redundant calls when data is unlikely to change, thus improving the user experience.

## State Management Libraries

- Use Blazor's built-in Cascading Parameters and EventCallbacks for basic state sharing across components.
- use built-in Oqtane state management in the base classes like PageSate and SiteState when appripriate.
- Avoid adding extra depenencies like Fluxor or BlazorState when the application grows in complexity.
- For client-side state persistence in Blazor WebAssembly, consider using Blazored.LocalStorage or Blazored.SessionStorage to maintain state between page reloads.
- For server-side Blazor, use Scoped Services and the StateContainer pattern to manage state within user sessions while minimizing re-renders.

## API Design and Integration

- Use service base methods to communicate with external APIs or server project backend.
- Implement error handling for API calls using try-catch and provide proper user feedback in the UI.

## Testing and Debugging in Visual Studio

- All unit testing and integration testing should be done in Visual Studio Enterprise.
- Test Blazor components and services using xUnit, NUnit, or MSTest.
- Use Moq or NSubstitute for mocking dependencies during tests.
- Debug Blazor UI issues using browser developer tools and Visual Studio's debugging tools for backend and server-side issues.
- For performance profiling and optimization, rely on Visual Studio's diagnostics tools.

## Security and Authentication

- Implement Authentication and Authorization using build-in Oqtane base class members like User.Roles.
- Use HTTPS for all web communication and ensure proper CORS policies are implemented.
