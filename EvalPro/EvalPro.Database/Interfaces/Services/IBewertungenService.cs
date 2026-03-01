using EvalPro.Database.DTOs;

namespace EvalPro.Database.Interfaces.Services;

public interface IBewertungenService
{
    public BewertungDTO? GetBewertungDtoById(int bewertungId);
}