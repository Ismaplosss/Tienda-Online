using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{

    
   public  class Cn_Categoria
    {
        CD_Categoria categorias = new CD_Categoria();

        public List<categoria> Lista_Categoria()
        {
            return categorias.Lista_Categoria();
        }


        public int Nuevo_Ingreso_Categorias( categoria categoria, out string codigo)
        {
            codigo = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || (string.IsNullOrWhiteSpace(categoria.Descripcion)))
            {
                codigo = "Error, Descripcion faltante!";
            }
            if(string.IsNullOrEmpty(categoria.Fecha_Registro)     || (string.IsNullOrWhiteSpace(categoria.Fecha_Registro)))
            {
                codigo = "Error,  Fecha no correcta!";
            }

          
            if ( string.IsNullOrEmpty(codigo))
            {

                return categorias.Registrar_Categoria(categoria, out codigo);

            }else
            {
                return  0;
            }






        }


        public bool Actualizar_Categoria(categoria categoria, out string  codigo)
        {
            codigo = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || (string.IsNullOrWhiteSpace(categoria.Descripcion)))
            {
                codigo = "Error, Descripcion faltante!";
            }
            if (string.IsNullOrEmpty(categoria.Fecha_Registro) || (string.IsNullOrWhiteSpace(categoria.Fecha_Registro)))
            {
                codigo = "Error,  Fecha no correcta!";
            }

            if (string.IsNullOrEmpty(codigo))
            {
                return categorias.Actualizar_Categoria(categoria, out codigo);
            }
            else
            {
                return false;
            }
        }


    }
}
