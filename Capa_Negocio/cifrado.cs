using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class cifrado
    {


        public static string Cifrado_Password(string clave)
        {
            StringBuilder SB = new StringBuilder();

            using (SHA256 hash = SHA256.Create())  
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(clave));

                foreach (byte b in result)
                {
                    SB.Append(b.ToString("X2")); 
                }
            }
            return SB.ToString();
        }


        public static string CifrarPassword_(string clave, out string salt)
        {
            
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);  

             
            var pbkdf2 = new Rfc2898DeriveBytes(clave, saltBytes, 100000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);

            return Convert.ToBase64String(hashBytes);  
        }


        public static bool ValidarPassword(string claveIngresada, string hashAlmacenado, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(claveIngresada, saltBytes, 100000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);

            string nuevoHash = Convert.ToBase64String(hashBytes);
            return nuevoHash == hashAlmacenado;  
        
        }



    }
}
