using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
   public  class CD_Login
    {

        // clase para realizar el login al panel de administracion . pero debo modificar
        // algo en la tabla y son los intentos para bloquear o desactivar
        // el login para este usuario en especificico


        public string AccesoAdministrador(string Correo, string hash, out string resumen)
        {
            resumen = string.Empty; // Mensaje por defecto
            string query = "SELECT Activo, Reestablecer, Clave FROM Usuario WHERE Correo = @Correo";

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
                            resumen = "Login";
                        }
                        else
                        {
                            resumen ="Correo electronico o contraseña incorrectos";
                        }
                    }
                }
            }

            return resumen ;
        }

        public bool VerificarCorreo( string Correo)
        {

            try
            {

                using(SqlConnection Conextion = new SqlConnection(Conexion.Conecctions))
                {
                    string query = "Select * from Usuario Where Correo=@Correo";
                    SqlCommand cmd = new SqlCommand(query, Conextion);
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    Conextion.Open();

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return true;


                }

            }
            catch (Exception ex) 
            {
                return false;
            }

            

        }

        public bool AlmacenarTokens(string Correo,string Tocken, DateTime Fecha_Expiracion)
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
            catch (Exception ex)
            {

                return false;
            }
           





           
        }

    }
}
