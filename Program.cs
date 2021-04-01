using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using ZipFile = System.IO.Compression.ZipFile;

namespace TesteAPI
{
    class Program : ArquivoZip
    {
        //Para usar a classe ZipFile, você deve fazer referência ao assembly System.IO.Compression.FileSystem em seu projeto.
        static void Main(string[] args)
        {

            string path = @"c:\Teste"; // Esse é o nome do diretorio que vai ser criado No disco local
            string zipPath = @"C:\Users\Agostin\Desktop\FATEC - Banco de Dados\SHAPE_3518305.zip";
            string PastaFinal = "\\Teste\\VariavelImportante";
            string ZipAreaImovel = @"c:\Teste\AREA_IMOVEL.zip";

            ArquivoZip zip = new ArquivoZip();

            try
            {
                zip.CriarPasta(path, zipPath);
            }

            catch {}

            Console.WriteLine(Directory.GetFiles(path));


            try
            {
                ZipFile.ExtractToDirectory(zipPath, path); //Extrai os arquivos do zip Shape e manda para o diretorio
            }
            catch { }
            

            // criar outra pasta para guardar a variavel AREA_IMOVEL
            try
            {
                zip.CriarPasta(PastaFinal, ZipAreaImovel);
            }

            catch (Exception e)
            {
                Console.WriteLine("A pasta ja existe", e.ToString());
            }


            try
            {
                ZipFile.ExtractToDirectory(ZipAreaImovel, PastaFinal); //Extrai os arquivos do zip Shape e manda para o diretorio
            }
            catch{}
            









        }
    }
}
