using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
   public  class Cn_Marca
    {
        CD_Marca Marcas = new CD_Marca();


        public List<Marca> Listar_Marca()
        {
            return Marcas.Lista_Marca();
        }


        public int Registrar_Marca(Marca marca, out string codigo)
        {
            codigo = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || (string.IsNullOrWhiteSpace(marca.Descripcion)))
            {
                codigo = "La descripcion faltante!";
            }
            if (string.IsNullOrEmpty(marca.Fecha_Registro) || (string.IsNullOrWhiteSpace(marca.Fecha_Registro)))
            {
                codigo = "La fecha no correcta!";
            }


            if (string.IsNullOrEmpty(codigo))
            {
                return Marcas.Registrar_Marca(marca, out codigo);

            }
            else
            {
                return 0;
            }

        }

        public bool Editar_Marca(Marca marca, out string codigo)
        {

            codigo = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || (string.IsNullOrWhiteSpace(marca.Descripcion)))
            {
                codigo = "La descripcion faltante!";
            }
            if (string.IsNullOrEmpty(marca.Fecha_Registro) || (string.IsNullOrWhiteSpace(marca.Fecha_Registro)))
            {
                codigo = "La fecha no correcta!";
            }

            if (string.IsNullOrEmpty(codigo))
            {
                return Marcas.Actualizar_Categoria(marca, out codigo);
            }
            else
            {
                return false;
            }



        }







    }
}
