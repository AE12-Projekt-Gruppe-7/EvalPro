using EvalPro.Database.Interfaces.Repository;
using EvalPro.Database.Repository;

static class DependencyInjection
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
    }
} 