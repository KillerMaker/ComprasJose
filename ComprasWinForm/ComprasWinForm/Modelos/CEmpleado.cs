using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ComprasWinForm.Modelos
{
    class CEmpleado : CEntidad
    {
        public readonly int? id;
        public readonly string cedula;
        public readonly string nombre;
        public readonly int departamento;
        public readonly int estado;

        public CEmpleado(int id) => this.id = id;
        public CEmpleado(int? id, string cedula, string nombre, int departamento, int estado)
        {
            this.id = id;
            this.cedula = cedula;
            this.nombre = nombre;
            this.departamento = departamento;
            this.estado=estado;
        }

        public async override Task<int> Delete() => 
            await ExecuteCommand($"UPDATE EMPLEADO SET ESTADO = 2 WHERE ID = {id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand(@"EXEC INSERTA_EMPLEADO
                                        @CEDULA,@NOMBRE,@DEPARTAMENTO,@ESTADO",GetParameters());

        public async override Task<int> Update() =>
            await ExecuteCommand(@"UPDATE EMPLEADO SET 
                                        DEPARTAMENTO = @DEPARTAMENTO, ESTADO WHERE ID = @ESTADO WHERE ID = @ID",GetParameters());

        public async static Task<DataTable>Select(string searchString=null)
        {
            string query = "SELECT * FROM VISTA_EMPLEADO "+searchString;
            using(SqlConnection con= new SqlConnection(stringConnection))
            {
                using(SqlDataReader reader=await ExecuteReader(query,con))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Cedula");
                    table.Columns.Add("Nombre");
                    table.Columns.Add("Departamento");
                    table.Columns.Add("Estado");
                    
                    while(await reader.ReadAsync())
                    {
                        table.Rows.Add(new string[]
                        {
                            reader[0].ToString(),
                            reader[1].ToString(),
                            reader[2].ToString(),
                            reader[3].ToString(),
                            reader[4].ToString()
                        });
                    }

                    return table;
                }
            }
        }

        public override List<SqlParameter> GetParameters()
        {
            SqlParameter id = new SqlParameter("@ID", this.id);
            SqlParameter cedula = new SqlParameter("@CEDULA", this.cedula);
            SqlParameter nombre = new SqlParameter("@NOMBRE", this.nombre);
            SqlParameter departamento = new SqlParameter("@DEPARTAMENTO", this.departamento);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);

            id.DbType = DbType.Int32;
            departamento.DbType = DbType.Int32;
            estado.DbType = DbType.Int32;
            cedula.DbType = DbType.String;
            nombre.DbType = DbType.String;


            return (this.id.HasValue)? 
                new List<SqlParameter>() { id, cedula, nombre, departamento, estado }: 
                new List<SqlParameter>() {cedula, nombre, departamento, estado };
        }
    }
}
