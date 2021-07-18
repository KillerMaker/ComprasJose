using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasWinForm.Modelos
{
    class CSolicitud : CEntidad
    {
        public readonly int? id;
        public readonly int empleado;
        public readonly DateTime fecha;
        public readonly int articulo;
        public readonly int cantidad;
        public readonly int unidadMedida;
        public readonly int estado;

        public CSolicitud(int id) => this.id = id;
        public CSolicitud(int? id, int empleado,DateTime fecha,int articulo,int cantidad,int unidadMedida,int estado)
        {
            this.id = id;
            this.empleado = empleado;
            this.fecha = fecha;
            this.articulo = articulo;
            this.cantidad = cantidad;
            this.unidadMedida = unidadMedida;
            this.estado = estado;
        }
        public async override Task<int> Delete() =>
            await ExecuteCommand($"UPDATE SOLICITUD SET ESTADO =2 WHERE ID ={id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand(@"INSERT INTO SOLICITUD VALUES
                                    (@EMPLEADO,@FECHA,@ARTICULO,@CANTIDAD,@UNIDAD_MEDIDA,@ESTADO)", GetParameters());

        public async override Task<int> Update() =>
            await ExecuteCommand(@"UPDATE SOLICITUD SET EMPLEADO = @EMPLEADO, FECHA = @FECHA, 
                                        ARTICULO = @ARTICULO, CANTIDAD=@CANTIDAD, UNIDAD_MEDIDA = @UNIDAD_MEDIDA, ESTADO =@ESTADO WHERE ID =@ID",GetParameters());

        public async static Task<DataTable> Select(string searchString = null)
        {
            string query = "SELECT * FROM VISTA_SOLICITUD" + searchString;
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlDataReader reader = await ExecuteReader(query, con))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Empleado");
                    table.Columns.Add("Fecha");
                    table.Columns.Add("Articulo");
                    table.Columns.Add("Cantidad");
                    table.Columns.Add("Unidad de Medida");
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
                            reader[5].ToString(),
                            reader[6].ToString(),
                        });
                    }
                    return table;
                }
            }
        }

        public override List<SqlParameter> GetParameters()
        {
            SqlParameter id = new SqlParameter("@ID", this.id);
            SqlParameter empleado = new SqlParameter("@EMPLEADO", this.empleado);
            SqlParameter fecha = new SqlParameter("@FECHA", this.fecha);
            SqlParameter articulo = new SqlParameter("@ARTICULO", this.articulo);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);
            SqlParameter cantidad = new SqlParameter("@CANTIDAD", this.cantidad);
            SqlParameter unidadMedida = new SqlParameter("@UNIDAD_MEDIDA", this.unidadMedida);

            id.DbType = DbType.Int32;
            empleado.DbType = DbType.Int32;
            estado.DbType = DbType.Int32;
            articulo.DbType = DbType.String;
            fecha.DbType = DbType.Date;
            cantidad.DbType = DbType.Int32;
            unidadMedida.DbType = DbType.Int32;

            return (this.id.HasValue) ?
                new List<SqlParameter>() { id, empleado, articulo, fecha, estado,cantidad,unidadMedida } :
                new List<SqlParameter>() { empleado, articulo, fecha, estado, cantidad, unidadMedida };
        }



    }
}
