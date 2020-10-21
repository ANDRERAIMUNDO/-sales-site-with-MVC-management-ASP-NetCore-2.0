using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LojaVirtual.Models.Libres.Texto
{
    public class KeyGenerator
    {
        public static string GetRandomNumber(int size)
        {
            var rand = new Random();
            var bytes = new byte[5];
            
            rand.NextBytes(bytes);
            StringBuilder result = new StringBuilder(size);
            foreach (byte byteValue in bytes)
                result.Append(byteValue);

            return result.ToString();
        }
    }
}
//gerando 8 numeros inteiros