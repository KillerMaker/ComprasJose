using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ComprasWinForm.Modelos
{
    abstract class CEntidad
    {
        public static readonly string stringConnection = "Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=COMPRAS-PROJECT;Integrated Security=True";
        
        public abstract Task<int> Insert();
        public abstract Task<int> Update();
        public abstract Task<int> Delete();
       
        public async Task<int> ExecuteCommand(string query,List<SqlParameter>parameters=null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(stringConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        if(parameters!=null)
                            command.Parameters.AddRange(parameters.ToArray());

                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<SqlDataReader>ExecuteReader(string query,SqlConnection con)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    await con.OpenAsync();
                    return await command.ExecuteReaderAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public abstract List<SqlParameter> GetParameters();
    }
}
