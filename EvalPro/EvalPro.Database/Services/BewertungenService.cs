using EvalPro.Database.DTOs;
using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;
using EvalPro.Database.Interfaces.Services;

namespace EvalPro.Database.Services;

public class BewertungenService(IBewertungRepository bewertungRepository, IKriteriumRepository kriteriumRepository, IPrueflingRepository prueflingRepository) : IBewertungenService
{
    public BewertungDTO? GetBewertungDtoById(int bewertungId)
    {
        var bewertung = bewertungRepository.GetById(bewertungId);

        if (bewertung == null)
            return null;
        
        var pruefling = prueflingRepository.GetById(bewertung.PrueflingId);
        
        if (pruefling == null)
            return null;

        IEnumerable<Kriterium> kriterien = new List<Kriterium>();

        foreach (var bewertungenKriterienId in bewertung.KriterienIds)
        {
            var kriterium = kriteriumRepository.GetById(bewertungenKriterienId);
            if (kriterium == null)
                continue;
            
            kriterien = kriterien.Append(kriterium);
        }

        return new BewertungDTO
        {
            Id = bewertung.Id,
            Gesammtkommentar = bewertung.Gesammtkommentar,
            istDoku = bewertung.istDoku,
            istPraesi = bewertung.istPraesi,
            KriterienIds = bewertung.KriterienIds,
            Kriterien = kriterien,
            PrueflingId = bewertung.PrueflingId,
            Pruefling = pruefling,
        };
    }
}