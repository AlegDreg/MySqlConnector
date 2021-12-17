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

        public async Task<DataTable> GetDataTable(string request)
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

            return dataTable;
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
