using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
     public class CD_Cliente
    {

        public int RegistrarCliente(Cliente obj, out string Mensaje)
        {
            int Id_Gen = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Conexion.Conecctions))
                {

                    SqlCommand cmd = new SqlCommand("SP_registrarCliente", sqlConnection);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    // Parámetros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    Id_Gen = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception Error_1)
            {
                Id_Gen = 0;
                Mensaje = Error_1.Message;
            }
            return Id_Gen;
        } //Si

        public bool Actuaizar_Cliente(Cliente obj, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarCliente", Conexionn);

                    // Agregar los parámetros de entrada
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
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
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception cosap)
            {
                Mensaje = cosap.Message;
            }

            return Resultado;
        } // NO

        public string AccesoCliente(string Correo, string hash, out string resumen)
        {
            resumen = string.Empty; // Mensaje por defecto
            string query = "SELECT Activo, Reestablecer,Intentos Clave FROM Cliente WHERE Correo = @Correo";

            using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
            {
                SqlCommand cmd = new SqlCommand(query, Conexionn);
                cmd.Parameters.AddWithValue("@Correo", Correo);
                Conexionn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Si encontró un usuario con ese email
                    {
                        bool activo = Convert.ToBoolean(reader["Activo"]);
                        bool restablecer = Convert.ToBoolean(reader["Reestablecer"]);
                        int Intentos = Convert.ToInt32(reader["Intentos"]);
                        string hashGuardado = reader["Clave"].ToString();

                        if (!activo)
                        {
                            resumen = "Estas desactivado, Contacta directamente con soporte";
                        }
                        else if (restablecer)
                        {
                            resumen = "Antes de seguir necesitaras reestablecer tu contraseña. \n" +
                                "se envió un correo para el restablecimiento ...";


                        }
                        else if (hash == hashGuardado)
                        {

                            using (SqlConnection ConeCC = new SqlConnection(Conexion.Conecctions))
                            {
                                string query2 = "UPDATE Cliente SET Intentos = 0 WHERE Correo = @Correo";
                                SqlCommand cmd2 = new SqlCommand(query2, ConeCC);
                                cmd2.Parameters.AddWithValue("@Correo", Correo);
                                ConeCC.Open();
                                cmd2.ExecuteNonQuery();
                                ConeCC.Close();
                            }
                            resumen = "Login";
                        }
                        else if (hash != hashGuardado) 
                        { 
                            resumen = "Bloquear";
                        }
                        
                        else if (Intentos > 3)
                        {
                            resumen = "Bloqueado";
                        }
                        else
                        {
                            resumen = "Correo electronico o contraseña incorrectos";
                        }
                    }
                }
            }

            return resumen;
        } // si

        public bool BloquearCliente(string Correo)
        {
            int Intentos = 0;
            try
            {
                using (SqlConnection Conex = new SqlConnection(Conexion.Conecctions))
                {
                    string query = "SELECT Intentos FROM Cliente WHERE Correo = @Correo";
                    SqlCommand cmd = new SqlCommand(query, Conex);
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    Conex.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Intentos = reader.GetInt32(0);
                    }

                    Conex.Close();
                    if (Intentos <= 3)
                    {
                        Intentos++;
                        using (SqlConnection ConeCC = new SqlConnection(Conexion.Conecctions))
                        {
                            string query2;
                            if (Intentos == 3)
                            {
                                query2 = "UPDATE Cliente SET Intentos = @Intentos,Activo=0 WHERE Correo = @Correo";
                            }
                            else
                            {
                                query2 = "UPDATE Cliente SET Intentos = @Intentos WHERE Correo = @Correo";

                            }
                                
                            SqlCommand cmd2 = new SqlCommand(query2, ConeCC);
                            cmd2.Parameters.AddWithValue("@Correo", Correo);
                            cmd2.Parameters.AddWithValue("@Intentos", Intentos);
                            ConeCC.Open();
                            cmd2.ExecuteNonQuery();
                            ConeCC.Close();
                        }
                        return false; 
                    }

                    return true; 
                }
            }
            catch (Exception)
            {
                return true; 
            }
        }



        public bool VerificarCorreo(string Correo)
        {

            try
            {

                using (SqlConnection Conextion = new SqlConnection(Conexion.Conecctions))
                {
                    string query = "SELECT COUNT(*) FROM  Cliente Correo = @Correo";
                    SqlCommand cmd = new SqlCommand(query, Conextion);
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    Conextion.Open();

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                    if (cantidad > 0)
                    {

                        string queryActualizar = "UPDATE Cliente SET Reestablecer = 1 WHERE Correo = @Correo";
                        SqlCommand cmdActualizar = new SqlCommand(queryActualizar, Conextion);
                        cmdActualizar.Parameters.AddWithValue("@Correo", Correo);
                        cmdActualizar.ExecuteNonQuery();

                        return true; // Devuelve true porque el correo existe y se actualizó el campo
                    }
                    return false; // Devuelve false si el correo no existe


                }

            }
            catch (Exception)
            {
                return false;
            }



        }

        public bool AlmacenarTokens(string Correo, string Tocken, DateTime Fecha_Expiracion)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(Conexion.Conecctions))
                {

                    string query = " Insert Into Tokens (Correo, Token, Fecha_Expiracion) values (@Correo , @Token, @Fecha_Expiracion)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    cmd.Parameters.AddWithValue("@Token", Tocken);
                    cmd.Parameters.AddWithValue("@Fecha_Expiracion", Fecha_Expiracion);

                    connection.Open();

                    int Cantidad = cmd.ExecuteNonQuery();
                    if (Cantidad != 0)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception)
            {

                return false;
            }







        }


        public bool VerificarTokens(string token)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.Conecctions))
                {
                    string query = "SELECT Fecha_Expiracion FROM tokens WHERE Token = @Token";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Token", token);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        DateTime fechaExpiracion = Convert.ToDateTime(reader["Fecha_Expiracion"]);



                        if (fechaExpiracion < DateTime.Now)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar el token: " + ex.Message);
            }

            return false;
        }

        public bool CambiarContrasena(string token, string nuevaContrasena, out string Mensaje)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.Conecctions))
                {
                    SqlCommand cmd = new SqlCommand("Actualizar_Contrasena", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@NuevaContrasena", nuevaContrasena); // ¡Recuerda cifrarla antes!

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Mensaje = "1";
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar la contraseña: " + ex.Message);
                Mensaje = "Error Critico en la conexion ";
                return false;
            }
        }

    }
}
