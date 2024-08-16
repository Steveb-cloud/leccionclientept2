using leccionclientept2.Modelos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace leccionclientept2.Comunes
{
    public class ConexionDB
    {
        public static class ConexionBD
        {
            public static SqlConnection conexion;

            public static SqlConnection abrirConexion()
            {
                conexion = new SqlConnection("Data Source=PC13_LAB1\\SQLEXPRESS; Initial Catalog=ClientesSP; Integrated Security=True; Trust Server Certificate=true");
                conexion.Open();
                return conexion;
            }

            public static List<Cliente> GetClientes()
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = abrirConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_CLIENTES";

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                return fillClientes(ds);
            }

            public static Cliente GetCliente(int id)
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = abrirConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_CLIENTE";
                cmd.Parameters.AddWithValue("PI_ID_CLIENTE", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                return fillClientes(ds)[0];
            }

            public static void PostCliente(Cliente objCliente)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = abrirConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INS_CLIENTE";
                cmd.Parameters.AddWithValue("PV_NOMBRE", objCliente.Nombre);
                cmd.Parameters.AddWithValue("PV_APELLIDO", objCliente.Apellido);
                cmd.Parameters.AddWithValue("PD_FECHA_CREACION", objCliente.FechaCreacion);

                cmd.ExecuteNonQuery();
            }

            public static void PutCliente(int clienteModificacion, Cliente objCliente)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = abrirConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_UPD_CLIENTE";
                cmd.Parameters.AddWithValue("PI_ID_CLIENTE", objCliente.Idc);
                cmd.Parameters.AddWithValue("PV_NOMBRE", objCliente.Nombre);
                cmd.Parameters.AddWithValue("PV_APELLIDO", objCliente.Apellido);
                cmd.Parameters.AddWithValue("PD_FECHA_CREACION", objCliente.FechaCreacion);
                cmd.Parameters.AddWithValue("PI_USUARIO_MODIFICACION", clienteModificacion);

                cmd.ExecuteNonQuery();
            }

            public static void DeleteCliente(int idCliente, int idUsuarioModificacion)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = abrirConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_DEL_CLIENTE";
                cmd.Parameters.AddWithValue("PI_ID_CLIENTE", idCliente);
                cmd.Parameters.AddWithValue("PI_USUARIO_MODIFICACION", idUsuarioModificacion);

                cmd.ExecuteNonQuery();
            }

            private static List<Cliente> fillClientes(DataSet ds)
            {
                List<Cliente> lrespuesta = new List<Cliente>();
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    Cliente objCliente = new Cliente();
                    objCliente.Idc = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_CLIENTE"].ToString());
                    objCliente.Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString();
                    objCliente.Apellido = ds.Tables[0].Rows[i]["APELLIDO"].ToString();
                    objCliente.FechaCreacion = Convert.ToDateTime(ds.Tables[0].Rows[i]["FECHA_CREACION"].ToString());
                    lrespuesta.Add(objCliente);
                }
                return lrespuesta;
            }
        }

    }
}
