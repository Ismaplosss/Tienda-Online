using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Cn_Usuario
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();
        private cifrado cifrado = new cifrado();
        private Correo Correo = new Correo();


        public List<Usuario> Listar()
        {
            return objCapaDato.Lista();
        }


        

        public int Registrar_User(Usuario obj, out string Codigo)
        {
            Codigo = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || (string.IsNullOrWhiteSpace(obj.Nombre)))
            {
                Codigo = "El nombre no es correcto";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || (string.IsNullOrWhiteSpace(obj.Correo)))
            {
                Codigo = "El Correo no es correcto";
            }
           else  if (string.IsNullOrEmpty(obj.Apellidos) || (string.IsNullOrWhiteSpace(obj.Apellidos)))
            {
                Codigo = "Apellidos Incorrecto ";
            }


            if (string.IsNullOrEmpty(Codigo))
            {

                string Static_Pass = "Test123";

                obj.Clave = cifrado.Cifrado_Password(Static_Pass);


                string asunto = "Creacion de contraseña ";
                string Mensaje = "<h3>Su cuenta fue creada este correo sera para la creacion de tu contraseña:</h3> <br /> <p> <a href=\"#\">Reestablecer tu contraseña</a></p>";
                bool respuesta = EnviarCorreo(obj.Correo, asunto, Mensaje);
                if (respuesta)
                { 
                    return objCapaDato.Registrar_User(obj, out Codigo);
                }
                else
                {
                    return 0;
                    Mensaje = "Error de correo";
                }

              
            }
            else
            {
                return 0;
            }
        }


        public bool Actualizar_User(Usuario obj, out string Codigo)
        {

            Codigo = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || (string.IsNullOrWhiteSpace(obj.Nombre)))
            {
                Codigo = "El nombre no es correcto";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || (string.IsNullOrWhiteSpace(obj.Correo)))
            {
                Codigo = "El Correo no es correcto";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || (string.IsNullOrWhiteSpace(obj.Apellidos)))
            {
                Codigo = "Apellidos Incorrecto ";
            }


            if (string.IsNullOrEmpty(Codigo))
            {
                return objCapaDato.Actualizar_User(obj, out Codigo);

            }
            else return false;


           
        }


        public bool Desactivar_User(Usuario obj, out string Codigo)
        {
            return objCapaDato.Desactivar_User(obj, out Codigo);
        }


        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool confirmacion = Correo.Enviar_Correo( correo, asunto,  mensaje);
            return confirmacion;
        }

    }
}
