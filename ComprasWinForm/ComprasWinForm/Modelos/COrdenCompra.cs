using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasWinForm.Modelos
{
    class COrdenCompra : CEntidad
    {

        public readonly int? id;
        public readonly int solicitud;
        public readonly int estado;

        public COrdenCompra(int id) => this.id = id;

        public COrdenCompra(int? id, int solicitud, int estado)
        {
            this.id = id;
            this.solicitud = solicitud;
            this.estado = estado;
        }
        public async override Task<int> Delete() =>
            await ExecuteCommand($"UPDATE ORDEN_COMPRA SET ESTADO =2 WHERE ID ={id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand("INSERT INTO ORDEN_COMPRA VALUES(@SOLICITUD,@ESTADO)", GetParameters());

        public async override Task<int> Update() =>
            await ExecuteCommand("UPDATE ORDEN_COMPRA SET SOLICITUD = @SOLICITUD, ESTADO =@ESTADO", GetParameters());

        public static async Task<DataTable> Select(string searchString = null)
        {
            string query = "SELECT * FROM ORDEN_COMPRA " + searchString;
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlDataReader reader = await ExecuteReader(query, con))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Solicitud");
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
            SqlParameter solicitud = new SqlParameter("@SOLICITUD", this.solicitud);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);

            id.DbType = DbType.Int32;
            estado.DbType = DbType.Int32;
            solicitud.DbType = DbType.Int32;

            return (this.id.HasValue) ?
                new List<SqlParameter>() { id, solicitud, estado } :
                new List<SqlParameter>() { solicitud, estado };
        }
    }
}
