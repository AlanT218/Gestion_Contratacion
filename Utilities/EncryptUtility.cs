using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Proyecto_Gestion.Utilities
{
    public class EncryptUtility
    {
        //    // Matriz de caracteres permitidos (incluye los caracteres especiales que necesitas)
        //    private static readonly char[] charSet = ".,_*abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        //    // Diccionario para almacenar el mapeo aleatorio de cada carácter a un número o cadena
        //    private static readonly Dictionary<char, string> charToRandomMap = new Dictionary<char, string>();
        //    private static readonly Dictionary<string, char> randomToCharMap = new Dictionary<string, char>();

        //    // Inicializa el mapeo aleatorio
        //    static EncryptUtility()
        //    {
        //        Random random = new Random();

        //        // Generar valores aleatorios únicos y asignarlos a cada carácter
        //        HashSet<string> usedValues = new HashSet<string>();
        //        foreach (char c in charSet)
        //        {
        //            string randomValue;
        //            do
        //            {
        //                randomValue = random.Next(1000, 9999).ToString(); // Genera un número aleatorio de 4 dígitos
        //            } while (usedValues.Contains(randomValue)); // Asegura que el valor no se repita

        //            usedValues.Add(randomValue);
        //            charToRandomMap[c] = randomValue;
        //            randomToCharMap[randomValue] = c; // Mapeo inverso para desencriptar
        //        }
        //    }

        //    // Método de cifrado 
        //    public static string EncryptPassword(string password)
        //    {
        //        StringBuilder encrypted = new StringBuilder();

        //        foreach (char c in password)
        //        {
        //            if (charToRandomMap.TryGetValue(c, out string randomValue))
        //            {
        //                encrypted.Append(randomValue); // Agrega el valor aleatorio correspondiente al carácter
        //            }
        //            else
        //            {
        //                throw new ArgumentException($"El carácter '{c}' no está permitido en la contraseña.");
        //            }
        //        }

        //        return encrypted.ToString();
        //    }

        //    // Método de desencriptado
        //    public static string DecryptPassword(string encryptedPassword)
        //    {
        //        StringBuilder decrypted = new StringBuilder();

        //        for (int i = 0; i < encryptedPassword.Length; i += 4)
        //        {
        //            string part = encryptedPassword.Substring(i, 4);

        //            if (randomToCharMap.TryGetValue(part, out char originalChar))
        //            {
        //                decrypted.Append(originalChar); // Agrega el carácter original correspondiente al valor aleatorio
        //            }
        //            else
        //            {
        //                throw new ArgumentException($"El valor encriptado '{part}' no es válido.");
        //            }
        //        }

        //        return decrypted.ToString();
        //    }
        //}


        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
