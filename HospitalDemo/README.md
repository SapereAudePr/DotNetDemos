# HospitalDemo

A Clean Architecture Web API (.NET 10, Minimal APIs) modeling a hospital domain, built to practice a wide range of backend concepts in one place.

## Concepts I'm practicing here

- **EF Core inheritance mapping strategies, side by side** — the domain (Hospital → Department → Personnel → Doctor/Nurse/Technician/Receptionist/Janitor) is implemented twice, once per strategy, each with its own `DbContext`, entities, migrations, and endpoints:
  - **TPH (Table-Per-Hierarchy):** `/tph/hospitals`, `/tph/departments`
  - **TPT (Table-Per-Type):** `/tpt/hospitals`, `/tpt/departments`
- **Global exception handling** *(most recently added)* — implemented via ASP.NET Core's `IExceptionHandler` pipeline instead of custom middleware:
  - `AppExceptionHandler` catches known, app-defined exceptions (an abstract `AppException` base with a `StatusCode`, e.g. `NoEntityFoundException` → 404) and turns them into a `ProblemDetails` response.
  - `FallbackExceptionHandler` catches anything unhandled, logs it, and returns a generic 500 `ProblemDetails` response so internals are never leaked to the client.
  - Both are registered with `AddExceptionHandler<T>()` and wired up with `app.UseExceptionHandler()` + `AddProblemDetails()` in `Program.cs`.
- **Rich domain models** — entities like `Hospital` use private setters/backing fields and expose behavior through methods (`UpdateAddress`, `UpdatePhoneNumber`, etc.) with guard clauses, instead of anemic public-setter DTOs.
- **A custom, fluent validation layer** (`Validator<T>` / `PropertyValidator<T,TProp>`) with rules like `NotNull`, `NotEmpty`, `MaxLength`, `Matches`, `Min`/`Max` — built from scratch rather than pulling in FluentValidation, as a way to understand how that kind of API works under the hood.
- **The `Result<T>` pattern** for representing success/`NotFound`/`BadRequest`/`ValidationFailure` outcomes from the Application layer without throwing exceptions for expected flows.
- **EF Core interceptors** — `AuditInterceptor` automatically stamps created/updated audit fields on `AuditableEntity` on save.
- Repository pattern (`IRepository`, `IDepartmentTptRepository`) and dependency injection setup in `Infrastructure/DependencyInjection.cs`.


## Tech stack

- .NET 10 / ASP.NET Core Minimal APIs
- Entity Framework Core 10 (SQL Server)
- Scalar (OpenAPI UI)
- xUnit-style unit tests with hand-rolled fakes