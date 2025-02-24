using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class CD_Usuarios
    {
        public List<Usuario> Lista()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oconexionb = new SqlConnection(Conexion.Conecctions))
                {
                    oconexionb.Open(); // Abrimos conexión antes de usar el comando

                    string query = "SELECT IdUsuario, Nombre, Apellidos, Correo, Clave, Reestablecer,Eliminado, Activo FROM Usuario where Eliminado=0";

                    using (SqlCommand cmd = new SqlCommand(query, oconexionb))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Eliminado = Convert.ToBoolean(dr["Eliminado"])

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de usuarios: " + ex.Message);
                lista = new List<Usuario>(); // Aseguramos que la lista no sea nula
            }

            return lista;
        }

        // Crearemos un metodo para el registro de datos de usuarios


        public int Registrar_User(Usuario obj, out string Codigo)
        {
            int Id_Gen = 0;
            Codigo = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    // Usaremos el procedimiento almacenado para realizar la inserción
                    SqlCommand cmd = new SqlCommand("SP_RegistrarUsuario", Conexionn);

                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    // Parámetros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    Conexionn.Open();
                    cmd.ExecuteNonQuery();

                    // Obtener valores de parámetros de salida
                    Id_Gen = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Codigo = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception cosap)
            {
                Id_Gen = 0;
                Codigo = cosap.Message;
            }
            return Id_Gen;
        }




        // Crearemos un metodo para actualizar los usuarios 

        public bool Actualizar_User(Usuario obj, out string Codigo)
        {
            bool Resultado = false;
            Codigo = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarUsuario", Conexionn);

                    // Agregar los parámetros de entrada
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    // Parámetros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    Conexionn.Open();
                    cmd.ExecuteNonQuery();

                    // Recuperar los valores de los parámetros de salida
                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Codigo = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception cosap)
            {
                Codigo = cosap.Message;
            }

            return Resultado;
        }



        public bool Desactivar_User(Usuario obj, out string Mensaje)
        {
            bool Resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    // Usamos el procedimiento almacenado para realizar la desactivación
                    SqlCommand cmd = new SqlCommand("SP_Desactivar", Conexionn);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;  // Cambié Codigo por Mensaje
                    cmd.CommandType = CommandType.StoredProcedure;
                    Conexionn.Open();
                    cmd.ExecuteNonQuery();

                    // Recuperamos los valores de los parámetros de salida
                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();  // Usamos el parámetro de salida correcto
                }
            }
            catch (Exception cosap)
            {
                Mensaje = cosap.Message;
            }
            return Resultado;
        }







    }

}
