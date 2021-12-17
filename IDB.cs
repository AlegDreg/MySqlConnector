using System.Data;
using System.Threading.Tasks;

namespace MySqlConnector
{
    internal interface IDB
    {
        Task<DataTable> GetDataTable(string request);
        bool DoRequest(string request);
    }
}
