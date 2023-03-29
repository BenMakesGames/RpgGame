A "Service" is a class that:
1. Contains one or more related methods
2. _Might_ be used by multiple pages, and/or other services
3. Depends on one or more other services (such as the `IDbContextFactory`, for database access)
   * If a method does not depend on a service, it can be a "static method", instead; see the `Functions` folder for examples of this.

Services must be registered in the `Program.cs` file. Refer to that file for information on how to do this.

Once registered, a service can be by .razor pages and components, and by other services.

To use a service in a .razor page or component, use the `[Inject]` attribute. For example, suppose you created a new `PetService`:

```c#
@code {
    [Inject] private PetService PetService { get; set; } = null!;
}
```

To use a service in another service, include it in the constructor. For example, suppose `MyService` wants to use methods from `PetService`:

```c#
class MyService
{
    private PetService PetService { get; }
    
    public MyService(PetService petService)
    {
        PetService = petService;
    }
}
```

Using services this way is called "dependency injection". Why it's useful to do organize code this way is a larger topic; Google or ask an AI for more information on the pros and cons of dependency injection.
