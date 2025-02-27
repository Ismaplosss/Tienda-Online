using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capa_Negocio
{
   public class Cn_Producto
    {

        CD_Productos D_Productos = new CD_Productos();


        public List<Producto> Lista_Productos()
        {
            return D_Productos.Listar_Producto();
        }


        public int Registrar_Producto(Producto producto, out string codigo)
        {
            codigo = string.Empty;


            // Validación del nombre
            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
            {
                codigo = "El nombre es obligatorio!";
                
            }

            // Validación de la descripción
            if (string.IsNullOrEmpty(producto.Descripcion) || string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                codigo = "La descripción es obligatoria!";
              
            }

            // Obtener el Id de la marca por su descripción
            var Marca = ObtenerIdMarcaPorNombre(producto.OMarca.Descripcion);
            if (Marca == 0)
            {
                codigo = "Error: No se encontró la marca especificada.";
                
            }

            // Obtener el Id de la categoría por su descripción
            var Categoria = ObtenerIdCategoriaPorNombre(producto.OCategoria.Descripcion); 
            if (Categoria == 0)
            {
                codigo = "Error: No se encontró la categoría especificada.";
               
            }

            // Validación del precio
            if (producto.Precio <= 0) // Asegúrate de que el precio sea positivo
            {
                codigo = "Error: El precio debe ser mayor a 0.";
              
            }

            // Validación del stock
            if (producto.Stock < 0) // El stock no debe ser negativo
            {
                codigo = "Error: El stock no puede ser negativo.";
          
            }

            // Validación de la ruta de imagen
            if (string.IsNullOrEmpty(producto.Ruta_Imagen) || string.IsNullOrWhiteSpace(producto.Ruta_Imagen))
            {
                codigo = "Error: La ruta de la imagen es obligatoria.";
            
            }

            // Validación del nombre de imagen
            if (string.IsNullOrEmpty(producto.Nombre_Imagen) || string.IsNullOrWhiteSpace(producto.Nombre_Imagen))
            {
                codigo = "Error: El nombre de la imagen es obligatorio.";
               
            }


            //if (string.IsNullOrEmpty(producto.Fecha_Registro) || string.IsNullOrWhiteSpace(producto.Fecha_Registro))

            //{
            //    codigo = "Error: La fecha de registro no es válida.";
              
            //}

            producto.OMarca.IdMarca = Marca;
            producto.OCategoria.IdCategoria = Categoria;

            if (string.IsNullOrEmpty(codigo))
            {
                return D_Productos.Registrar_Producto(producto, out codigo);
            }
            else
            {
                return 0;
            }


        }



        public bool Actualizar_Producto(Producto producto, out string codigo)
        {
            codigo = string.Empty;


            // Validación del nombre
            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
            {
                codigo = "El nombre es obligatorio!";

            }

            // Validación de la descripción
            if (string.IsNullOrEmpty(producto.Descripcion) || string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                codigo = "La descripción es obligatoria!";

            }

            // Obtener el Id de la marca por su descripción
            var Marca = ObtenerIdMarcaPorNombre(producto.OMarca.Descripcion);
            if (Marca == 0)
            {
                codigo = "Error: No se encontró la marca especificada.";

            }

            // Obtener el Id de la categoría por su descripción
            var Categoria = ObtenerIdCategoriaPorNombre(producto.OCategoria.Descripcion);
            if (Categoria == 0)
            {
                codigo = "Error: No se encontró la categoría especificada.";

            }

            // Validación del precio
            if (producto.Precio <= 0) // Asegúrate de que el precio sea positivo
            {
                codigo = "Error: El precio debe ser mayor a 0.";

            }

            // Validación del stock
            if (producto.Stock < 0) // El stock no debe ser negativo
            {
                codigo = "Error: El stock no puede ser negativo.";

            }

            // Validación de la ruta de imagen
            if (string.IsNullOrEmpty(producto.Ruta_Imagen) || string.IsNullOrWhiteSpace(producto.Ruta_Imagen))
            {
                codigo = "Error: La ruta de la imagen es obligatoria.";

            }

            // Validación del nombre de imagen
            if (string.IsNullOrEmpty(producto.Nombre_Imagen) || string.IsNullOrWhiteSpace(producto.Nombre_Imagen))
            {
                codigo = "Error: El nombre de la imagen es obligatorio.";

            }


            //if (string.IsNullOrEmpty(producto.Fecha_Registro) || string.IsNullOrWhiteSpace(producto.Fecha_Registro))

            //{
            //    codigo = "Error: La fecha de registro no es válida.";

            //}

            producto.OMarca.IdMarca = Marca;
            producto.OCategoria.IdCategoria = Categoria;

            if (string.IsNullOrEmpty(codigo))
            {
                return D_Productos.Actualizar_Producto(producto, out codigo);
            }
            else
            {
                return false;
            }


        }





        public int ObtenerIdMarcaPorNombre(string nombreMarca)
        {
            List<Marca> listaMarcas = new CD_Marca().Lista_Marca(); // Trae todas las marcas de la BD
            var marca = listaMarcas.FirstOrDefault(m => m.Descripcion == nombreMarca);

            return marca != null ? marca.IdMarca : 0; // Retorna el ID o 0 si no se encuentra
        }

        public int ObtenerIdCategoriaPorNombre(string NombreCategoria)
        {
            List<categoria> listacategoria = new CD_Categoria().Lista_Categoria(); ; // Trae todas las marcas de la BD
            var Categoria = listacategoria.FirstOrDefault(m => m.Descripcion == NombreCategoria);

            return Categoria != null ? Categoria.IdCategoria : 0; // Retorna el ID o 0 si no se encuentra
        }



    }
}
