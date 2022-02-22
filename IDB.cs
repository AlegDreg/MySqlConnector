using System.Data;
using System.Threading.Tasks;

namespace MySqlConnector
{
    internal interface IDB
    {
        Task<List<T>> GetDataTable<T>(string request);
        bool DoRequest(string request);
    }
}
