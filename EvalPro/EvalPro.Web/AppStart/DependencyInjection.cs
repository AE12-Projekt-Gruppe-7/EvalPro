using EvalPro.Database.Interfaces.Repository;
using EvalPro.Database.Repository;

namespace EvalPro.Web.AppStart;

static class DependencyInjection
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddSingleton<IBewertungRepository, BewertungRepository>();
        services.AddSingleton<IFachgespraechRepository, FachgespraechRepository>();
        
        
    }
}