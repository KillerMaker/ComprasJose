using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasWinForm.Modelos
{
    class CMarca : CEntidad
    {
        public readonly int? id;
        public readonly string nombre;
        public readonly int estado;

        public CMarca(int id) => this.id = id;

        public CMarca(int? id,string nombre,int estado)
        {
            this.id = id;
            this.nombre = nombre;
            this.estado = estado;
        }
        public async override Task<int> Delete() =>
            await ExecuteCommand($"UPDATE MARCA SET ESTADO =2 WHERE ID ={id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand("INSERT INTO MARCA VALUES(@NOMBRE,@ESTADO)", GetParameters());

        public async override Task<int> Update() =>
            await ExecuteCommand("UPDATE MARCA SET NOMBRE = @NOMBRE, ESTADO =@ESTADO", GetParameters());

        public static async Task<DataTable> Select(string searchString = null)
        {
            string query = "SELECT * FROM VISTA_MARCA " + searchString;
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlDataReader reader = await ExecuteReader(query, con))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Nombre");
                    table.Columns.Add("Estado");


                    while (await reader.ReadAsync())
                    {
                        table.Rows.Add(new string[]
                        {
                            reader[0].ToString(),
                            reader[1].ToString(),
                            reader[2].ToString()
                        });
                    }

                    return table;
                }
            }
        }

        public override List<SqlParameter> GetParameters()
        {
            SqlParameter id = new SqlParameter("@ID", this.id);
            SqlParameter nombre = new SqlParameter("@NOMBRE", this.nombre);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);

            id.DbType = DbType.Int32;
            estado.DbType = DbType.Int32;
            nombre.DbType = DbType.String;

            return (this.id.HasValue) ?
                new List<SqlParameter>() { id, nombre, estado } :
                new List<SqlParameter>() { nombre, estado };
        }
    }
}
