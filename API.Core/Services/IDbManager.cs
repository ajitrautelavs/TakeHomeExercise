using System.Data;

namespace API.Core.Services
{
    public interface IDbManager
    {
        IDbConnection GetConnection();
        IDbConnection GetOpenConnection();
    }
}
