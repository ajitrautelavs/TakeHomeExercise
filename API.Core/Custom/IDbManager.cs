using System.Data;

namespace API.Core.Custom
{
    public interface IDbManager
    {
        IDbConnection GetConnection();
        IDbConnection GetOpenConnection();
    }
}
