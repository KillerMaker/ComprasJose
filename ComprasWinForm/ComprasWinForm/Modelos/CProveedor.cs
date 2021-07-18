using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasWinForm.Modelos
{
    class CProveedor : CEntidad
    {
        public readonly int? id;
        public readonly string rnc;
        public readonly string nombreComercial;
        public readonly int estado;

        public CProveedor(int id) => this.id = id;
        public CProveedor(int? id, string rnc, string nombreComercial, int estado)
        {
            this.id = id;
            this.rnc = rnc;
            this.nombreComercial = nombreComercial;
            this.estado = estado;
        }

        public async override Task<int> Delete() =>
            await ExecuteCommand($"UPDATE PROVEEDOR SET ESTADO = 2 WHERE ID = {id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand(@"INSERT INTO PROVEEDOR VALUES
                                        (@RNC,@NOMBRE_COMERCIAL,@ESTADO)", GetParameters());

        public async override Task<int> Update() =>
            await ExecuteCommand(@"UPDATE PROVEEDOR SET 
                                        NOMBRE = @NOMBRE_COMERCIAL, ESTADO = @ESTADO WHERE ID = @ID", GetParameters());

        public async static Task<DataTable> Select(string searchString = null)
        {
            string query = "SELECT * FROM VISTA_PROVEEDOR " + searchString;
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlDataReader reader = await ExecuteReader(query, con))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("RNC");
                    table.Columns.Add("Nombre Comercial");
                    table.Columns.Add("Estado");

                    while (await reader.ReadAsync())
                    {
                        table.Rows.Add(new string[]
                        {
                            reader[0].ToString(),
                            reader[1].ToString(),
                            reader[2].ToString(),
                            reader[3].ToString(),
                        });
                    }

                    return table;
                }
            }
        }

        public override List<SqlParameter> GetParameters()
        {
            SqlParameter id = new SqlParameter("@ID", this.id);
            SqlParameter rnc = new SqlParameter("@RNC", this.rnc);
            SqlParameter nombreComercial = new SqlParameter("@NOMBRE_COMERCIAL", this.nombreComercial);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);

            id.DbType = DbType.Int32;
            estado.DbType = DbType.Int32;
            rnc.DbType = DbType.String;
            nombreComercial.DbType = DbType.String;


            return (this.id.HasValue) ?
                new List<SqlParameter>() { id, rnc, nombreComercial, estado } :
                new List<SqlParameter>() { rnc, nombreComercial, estado };
        }
    }
}
