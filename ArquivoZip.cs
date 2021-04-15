using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using ZipFile = ICSharpCode.SharpZipLib.Zip.ZipFile;

namespace TesteAPI
{


    public class ArquivoZip
    {


        internal static void ExtractToDirectory(string zipPath, string path)
        {
            throw new NotImplementedException();
        }

        public void CriarPasta(string path)
        {
            //metodo para criar pasta direto no disco local (C)
            // verifica se a pasta existe
            if (Directory.Exists(path))
            {
                
            }
            else
            {
                // Tenta criar a pasta
                DirectoryInfo DiretorioTeste = Directory.CreateDirectory(path);
                Console.WriteLine("A pasta foi criada com sucesso as {0}.", Directory.GetCreationTime(path));
            }
        }


    }
}
