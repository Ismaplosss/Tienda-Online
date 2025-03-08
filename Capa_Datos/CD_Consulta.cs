using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Consulta
    {



        public List<Reportes> Lista_Reportes(string fecha_Inicio, string fecha_Final, string Id)
        {
            List<Reportes> lista = new List<Reportes>();

            try
            {
                using (SqlConnection Conexionn = new SqlConnection(Conexion.Conecctions))
                {
                    // Usaremos el procedimiento almacenado para realizar la inserción
                    SqlCommand cmd = new SqlCommand("SP_Reporte_Ventas", Conexionn);

                    cmd.Parameters.AddWithValue("Fecha_Inicio", fecha_Inicio);
                    cmd.Parameters.AddWithValue("Fecha_Final", fecha_Final);
                    cmd.Parameters.AddWithValue("Id_Transaccion", Id);

                    cmd.CommandType = CommandType.StoredProcedure;
                    Conexionn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reportes
                            {

                                Fecha_venta = dr["Fecha_venta"].ToString(),
                                Cliente = dr["Cliente"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                Id_Transaccion = dr["Id_Transaccion"].ToString()

                            });
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de categorias: " + ex.Message);
                lista = new List<Reportes>();
            }

            return lista;
        }





        public Consulta ListarConsulta()
        {
            Consulta lista = new Consulta();

            try
            {
                using (SqlConnection oconexionb = new SqlConnection(Conexion.Conecctions))
                {
                  
                    using (SqlCommand cmd = new SqlCommand("SP_Consultar_Lista", oconexionb))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Especificar que es un procedimiento almacenado
                        oconexionb.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())

                                lista = new Consulta()
                                {
                                    TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                    TotalVenta = Convert.ToInt32(dr["TotalVentas"]),
                                    TotalProducto = Convert.ToInt32(dr["TotalProductos"])
                                };
                          
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de panel : " + ex.Message);
                Console.WriteLine(ex.Message);
                lista = new Consulta();
            }
            
            return lista;

        }




    }
}
