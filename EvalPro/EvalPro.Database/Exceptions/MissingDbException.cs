namespace EvalPro.Database.Exceptions;

[Serializable]
public class MissingDbException : Exception
{
    public MissingDbException()
    {
        throw new MissingDbException("Could not find Database, no action was taken");
    }
    
    public MissingDbException(string message) : base(message)
    {}  
}