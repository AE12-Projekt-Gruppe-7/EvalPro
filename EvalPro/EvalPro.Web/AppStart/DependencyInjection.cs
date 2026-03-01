using EvalPro.Database.Interfaces.Repository;
using EvalPro.Database.Interfaces.Services;
using EvalPro.Database.Repository;
using EvalPro.Database.Services;

namespace EvalPro.Web.AppStart;

static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IIdRepository, IdRepository>();
        services.AddSingleton<IBewertungRepository, BewertungRepository>();
        services.AddSingleton<IFachgespraechRepository, FachgespraechRepository>();
        services.AddSingleton<IFrageRepository, FrageRepository>();
        services.AddSingleton<IKriteriumRepository, KriteriumRepository>();
        services.AddSingleton<IPrueflingRepository, PrueflingRepository>();
        services.AddSingleton<IPruefungsausschussRepository,PruefungsausschussRepository>();
        
        services.AddScoped<IBewertungenService, BewertungenService>();
    }
}