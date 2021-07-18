using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasWinForm.Modelos
{
    class CArticulo : CEntidad
    {
        public readonly int? id;
        public readonly string nombre;
        public readonly int existencia;
        public readonly int marca;
        public readonly int unidadMedida;
        public readonly int estado;

        public CArticulo(int id) => this.id = id;

        public CArticulo(int? id,string nombre,int existencia,int marca,int unidadMedida,int estado)
        {
            this.id = id;
            this.nombre = nombre;
            this.existencia = existencia;
            this.marca = marca;
            this.unidadMedida = unidadMedida;
            this.estado = estado;
        }

        public async override Task<int> Delete() =>
            await ExecuteCommand($"UPDATE ARTICULO SET ESTADO = 2 WHERE ID ={id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand(@"INSERT INTO ARTICULO VALUES(@NOMBRE,@EXISTENCIA,@MARCA,@UNIDAD_MEDIDA,@ESTADO)", GetParameters());

        public async override Task<int> Update() =>
            await ExecuteCommand("UPDATE ARTICULO SET NOMBRE = @NOMBRE, EXISTENCIA = @EXISTENCIA, MARCA = @MARCA, UNIDAD_MEDIDA=@UNIDAD_MEDIDA WHERE ID = @ID",GetParameters());

        public async static Task<DataTable> Select(string searchString = null)
        {
            string query = "SELECT * FROM VISTA_ARTICULO" + searchString;
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlDataReader reader = await ExecuteReader(query, con))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Nombre");
                    table.Columns.Add("Existencia");
                    table.Columns.Add("Marca");
                    table.Columns.Add("UnidadMedida");
                    table.Columns.Add("Estado");

                    while (await reader.ReadAsync())
                    {
                        table.Rows.Add(new string[]
                        {
                            reader[0].ToString(),
                            reader[1].ToString(),
                            reader[2].ToString(),
                            reader[3].ToString(),
                            reader[4].ToString(),
                            reader[5].ToString()
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
            SqlParameter existencia = new SqlParameter("@EXISTENCIA", this.existencia);
            SqlParameter marca = new SqlParameter("@MARCA", this.marca);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);
            SqlParameter unidadMedida = new SqlParameter("@UNIDAD_MEDIDA", this.unidadMedida);

            id.DbType = DbType.Int32;
            nombre.DbType = DbType.String;
            estado.DbType = DbType.Int32;
            existencia.DbType = DbType.Int32;
            marca.DbType = DbType.Int32;
            unidadMedida.DbType = DbType.Int32;

            return (this.id.HasValue) ?
                new List<SqlParameter>() { id, nombre,estado,existencia, marca,unidadMedida } :
                new List<SqlParameter>() { nombre, estado, existencia, marca, unidadMedida };
        }

    }
}
