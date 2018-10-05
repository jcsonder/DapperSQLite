using System.Data;

namespace Persistence
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
