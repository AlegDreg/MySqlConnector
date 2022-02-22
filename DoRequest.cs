using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MySqlConnector
{
    public class LogReg : IDB
    {
        private DB DB;

        public LogReg(DB dB)
        {
            DB = dB;
        }

        public async Task<List<T>> GetDataTable<T>(string request)
        {
            DataTable dataTable = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            MySqlCommand mySqlCommand = new MySqlCommand(request, DB.GetConnection());

            mySqlDataAdapter.SelectCommand = mySqlCommand;

            DB.OpenConnection();

            try
            {
                await mySqlDataAdapter.FillAsync(dataTable);
            }
            catch (Exception ex)
            {
                //
            }

            DB.CloseConnection();

            if (dataTable.Rows.Count < 1)
                return null;

            return ConvertToList<T>(dataTable);
        }

        private List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        System.Reflection.PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }

        public bool DoRequest(string request)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(request, DB.GetConnection());

                DB.OpenConnection();

                if (mySqlCommand.ExecuteNonQuery() == 1)
                {
                    DB.CloseConnection();
                    return true;
                }
                else
                {
                    DB.CloseConnection();
                    return false;
                }
            }
            catch (Exception ex)
            {
                DB.CloseConnection();
                return false;
            }
        }
    }
}
