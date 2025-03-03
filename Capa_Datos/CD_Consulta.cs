using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Consulta
    {

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
