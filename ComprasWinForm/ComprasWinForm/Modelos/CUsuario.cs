using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace ComprasWinForm.Modelos
{
    class CUsuario : CEntidad
    {
        public readonly int? id;
        public readonly string nombre;
        public readonly string clave;
        public readonly int tipoUsuario;
        public readonly int estado;

        public CUsuario(int id) => this.id = id;
        public CUsuario(int? id,string nombre, string clave,int tipoUsuario,int estado)
        {
            this.id = id;
            this.nombre = nombre;
            this.clave = clave;
            this.tipoUsuario = tipoUsuario;
            this.estado = estado;
        }

        public async override Task<int> Delete() =>
            await ExecuteCommand($"UPDATE USUARIO SET ESTADO = 2 WHERE ID = {id}");

        public async override Task<int> Insert() =>
            await ExecuteCommand(@"INSERT INTO USUARIO VALUES(@NOMBRE,@CLAVE,@TIPO_USUARIO,@ESTADO)");

        public async override Task<int> Update()=>
             await ExecuteCommand(@"UPDATE USUARIO SET NOMBRE_USUARIO = @NOMBRE, CLAVE =@CLAVE, TIPO_USUARIO=@TIPO_USUARIO, ESTADO=@ESTADO WHERE ID = @ID");

        public async static Task<bool>Login(string nombreUsuario,string clave)
        {
            string query = $"SELECT NOMBRE_USUARIO,CLAVE FROM USUARIO WHERE NOMBRE_USUARIO='{nombreUsuario}' AND CLAVE ='{clave}'";
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlDataReader reader = await ExecuteReader(query, con))
                {
                    try
                    {
                        if (reader.FieldCount > 0)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    
                }
            }
        }

        public override List<SqlParameter> GetParameters()
        {
            SqlParameter id = new SqlParameter("@ID", this.id);
            SqlParameter nombre = new SqlParameter("@NOMBRE", this.nombre);
            SqlParameter clave = new SqlParameter("CLAVE", this.clave);
            SqlParameter tipoUsuario = new SqlParameter("TIPO_USUARIO", this.tipoUsuario);
            SqlParameter estado = new SqlParameter("@ESTADO", this.estado);

            id.DbType = DbType.Int32;
            estado.DbType = DbType.Int32;
            nombre.DbType = DbType.String;
            clave.DbType = DbType.String;
            tipoUsuario.DbType = DbType.Int32;

            return (this.id.HasValue) ?
                new List<SqlParameter>() { id, clave, nombre, estado } :
                new List<SqlParameter>() { nombre, clave, estado };
        }
    }
}
