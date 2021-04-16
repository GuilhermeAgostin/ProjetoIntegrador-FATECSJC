using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace APIFATECForms
{
    class Arquivo
    {
        

        public void CriarPasta(string Pasta)
        {        
            // seu eu criar um metodo que ja inicializa criando todas as pastas ficaria dificil pra conseguir extrair tudo
            if (Directory.Exists(Pasta))
            {
            }
            else
            {
                Directory.CreateDirectory(Pasta);
                Console.WriteLine("\nFoi gerada a pasta {0} dentro do projeto - {1}.", Pasta, Directory.GetCreationTime(Pasta));
            }
        }

        public void ExtrairArquivo(string zipPath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public void DeletarArquivos()
        {
            if (System.IO.File.Exists(@"C:\Users\Public\DeleteTest\test.txt"))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(@"C:\Users\Public\DeleteTest\test.txt");
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }

        public void DeletarArquivos2(string pasta)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(pasta);

            foreach (FileInfo file in di.GetFiles())
            {
                if(file.Extension == ".zip")
                {
                    file.Delete();
                }
                
            }
          
        }


       

    }
}
