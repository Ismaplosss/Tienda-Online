using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Marca
    {

        public List<Marca> Lista_Marca()
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oconexionb = new SqlConnection(Conexion.Conecctions))
                {
                    oconexionb.Open();

                    string query = "select IdMarca, Descripcion, Activo, Fecha_Registro from Marca";

                    using (SqlCommand cmd = new SqlCommand(query, oconexionb))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca
                            {

                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Fecha_Registro = dr["Fecha_Registro"].ToString()

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de categorias: " + ex.Message);
                lista = new List<Marca>();
            }

            return lista;
        }




        public int Registrar_Marca(Marca obj, out string Codigo)
        {
            int Id_Gen = 0;
            Codigo = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    // Usaremos el procedimiento almacenado para realizar la inserción
                    SqlCommand cmd = new SqlCommand("SP_Registrar_Marca", Conexionn);

                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("Fecha_Registro", obj.Fecha_Registro);

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


        public bool Actualizar_Categoria(Marca obj, out string Codigo)
        {
            bool Resultado = false;
            Codigo = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    SqlCommand cmd = new SqlCommand("SP_Editar_Marca", Conexionn);

                    // Agregar los parámetros de entrada
                    cmd.Parameters.AddWithValue("IdMarca", obj.IdMarca);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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










    }
}
