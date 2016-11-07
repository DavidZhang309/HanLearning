using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HanLearning.Data
{
    public class HanDatabase : IDisposable
    {
        public SqlConnection Connection { get; private set; }

        public HanDatabase()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HanDatabase"].ConnectionString);
            Connection.Open();
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                DataTable table = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            return ExecuteQuery(query, null);
        }

        public DataSet ExecuteStoredProcedure(string spName, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(spName, Connection) { CommandType = CommandType.StoredProcedure })
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                DataSet set = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(set);
                    return set;
                }
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}