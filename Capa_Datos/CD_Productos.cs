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
   public  class CD_Productos
    {

        public List<Producto> Listar_Producto()
        {
            List<Producto> Lista = new List<Producto>();
            try
            {
                using (SqlConnection oconexionb = new SqlConnection(Conexion.Conecctions))
              
                {
                    oconexionb.Open();
                    string query = @"
                     SELECT p.IdProducto, p.Nombre, p.Descripcion, 
                      m.IdMarca, m.Descripcion AS MarcaDescripcion, 
                      c.IdCategoria, c.Descripcion AS CategoriaDescripcion, 
                      p.Precio, p.Stock, p.Ruta_Imagen, p.Nombre_Imagen, 
                      p.Activo, p.Fecha_Regitro
                      FROM Producto p
                      INNER JOIN Marca m ON p.IdMarca = m.IdMarca
                      INNER JOIN Categoria c ON p.IdCategoria = c.IdCategoria";

                    using (SqlCommand cmd = new SqlCommand(query, oconexionb))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows) // ← Verifica que haya datos antes de leer
                        {
                            while (dr.Read())
                            {
                                Lista.Add(new Producto
                                {
                                    IdProducto = dr["IdProducto"] != DBNull.Value ? Convert.ToInt32(dr["IdProducto"]) : 0,
                                    Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : "",
                                    Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                                    OMarca = new Marca
                                    {
                                        IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                        Descripcion = dr["MarcaDescripcion"].ToString()
                                    },
                                    OCategoria = new categoria
                                    {
                                        IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                        Descripcion = dr["CategoriaDescripcion"].ToString()
                                    },
                                    Precio = dr["Precio"] != DBNull.Value ? Convert.ToDecimal(dr["Precio"]) : 0m,
                                    Stock = dr["Stock"] != DBNull.Value ? Convert.ToInt32(dr["Stock"]) : 0,
                                    Ruta_Imagen = dr["Ruta_Imagen"] != DBNull.Value ? dr["Ruta_Imagen"].ToString() : "",
                                    Nombre_Imagen = dr["Nombre_Imagen"] != DBNull.Value ? dr["Nombre_Imagen"].ToString() : "",
                                    Activo = dr["Activo"] != DBNull.Value ? Convert.ToBoolean(dr["Activo"]) : false,
                                    Fecha_Registro = dr["Fecha_Regitro"].ToString()
                                });
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los productos: " + ex.Message); // ← Imprime el error real
                Lista = new List<Producto>(); // ← Devuelve una lista vacía en caso de error
            }
            return Lista;
        }



        public int Registrar_Producto(Producto obj, out string Codigo)
        {
            int Id_Gen = 0;
            Codigo = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    // Usaremos el procedimiento almacenado para realizar la inserción
                    SqlCommand cmd = new SqlCommand("SP_RegistrarProducto", Conexionn);

                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.OMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.OCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Ruta_Imagen", obj.Ruta_Imagen);
                    cmd.Parameters.AddWithValue("Nombre_Imagen", obj.Nombre_Imagen);
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


        public bool Actualizar_Producto(Producto obj, out string Codigo)
        {
            bool Resultado = false;
            Codigo = string.Empty;

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    SqlCommand cmd = new SqlCommand("SP_Editar_Productos", Conexionn);

                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.OMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.OCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Ruta_Imagen", obj.Ruta_Imagen);
                    cmd.Parameters.AddWithValue("Nombre_Imagen", obj.Nombre_Imagen);
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
