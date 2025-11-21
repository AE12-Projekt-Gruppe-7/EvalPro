using EvalPro.Database.Interfaces.Repository;
using EvalPro.Database.Repository;

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
    }
}