using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIFATECForms
{
    public partial class Form1 : Form
    {
        static string PastaDaAplicacao = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        static string BasePath = Path.Combine(PastaDaAplicacao, "BasePath");
        static string APP = Path.Combine(BasePath, "APP");
        static string AREA_ALTITUDE_SUPERIOR_1800 = Path.Combine(BasePath, "AREA_ALTITUDE_SUPERIOR_1800");
        static string AREA_CONSOLIDADA = Path.Combine(BasePath, "AREA_CONSOLIDADA");
        static string AREA_DECLIVIDADE_MAIOR_45 = Path.Combine(BasePath, "AREA_DECLIVIDADE_MAIOR_45");
        static string AREA_IMOVEL = Path.Combine(BasePath, "AREA_IMOVEL");
        static string AREA_POUSIO = Path.Combine(BasePath, "AREA_POUSIO");
        static string AREA_TOPO_MORRO = Path.Combine(BasePath, "AREA_TOPO_MORRO");
        static string BANHADO = Path.Combine(BasePath, "BANHADO");
        static string BORDA_CHAPADA = Path.Combine(BasePath, "BORDA_CHAPADA");
        static string HIDROGRAFIA = Path.Combine(BasePath, "HIDROGRAFIA");
        static string MANGUEZAL = Path.Combine(BasePath, "MANGUEZAL");
        static string NASCENTE_OLHO_DAGUA = Path.Combine(BasePath, "NASCENTE_OLHO_DAGUA");
        static string RESERVA_LEGAL = Path.Combine(BasePath, "RESERVA_LEGAL");
        static string RESTINGA = Path.Combine(BasePath, "RESTINGA");
        static string SERVIDAO_ADMINISTRATIVA = Path.Combine(BasePath, "SERVIDAO_ADMINISTRATIVA");
        static string USO_RESTRITO = Path.Combine(BasePath, "USO_RESTRITO");
        static string VEGETACAO_NATIVA = Path.Combine(BasePath, "VEGETACAO_NATIVA");
        static string VEREDA = Path.Combine(BasePath, "VEREDA");

        int ContagemClick = 0;
        
        string ArquivoSHAPE = "";

        //string[] Pastas = { APP, AREA_CONSOLIDADA , AREA_DECLIVIDADE_MAIOR_45 , AREA_IMOVEL, AREA_POUSIO, AREA_TOPO_MORRO };

        List<string> listFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscaClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //listFiles.Clear();
            // ListView.

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtNomeArquivo.Text = ofd.FileName;
            }
        }

        private void label3_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            Arquivo zip = new Arquivo();

            DisplayImgSearch();
            IncluiItems();

            zip.CriarPasta(BasePath);
            zip.CriarPasta(APP);
            zip.CriarPasta(AREA_ALTITUDE_SUPERIOR_1800);
            zip.CriarPasta(AREA_CONSOLIDADA);
            zip.CriarPasta(AREA_DECLIVIDADE_MAIOR_45);
            zip.CriarPasta(AREA_IMOVEL);
            zip.CriarPasta(AREA_POUSIO);
            zip.CriarPasta(AREA_TOPO_MORRO);
            zip.CriarPasta(BANHADO);
            zip.CriarPasta(BORDA_CHAPADA);
            zip.CriarPasta(HIDROGRAFIA);
            zip.CriarPasta(MANGUEZAL);
            zip.CriarPasta(NASCENTE_OLHO_DAGUA);
            zip.CriarPasta(RESERVA_LEGAL);
            zip.CriarPasta(RESTINGA);
            zip.CriarPasta(SERVIDAO_ADMINISTRATIVA);
            zip.CriarPasta(USO_RESTRITO);
            zip.CriarPasta(VEGETACAO_NATIVA);
            zip.CriarPasta(VEREDA);

        }

        private void IncluiItems()
        {
            string[] tiposArquivo = { ".zip", ".dbf", ".prj", ".shp", ".shx" };
            foreach (var i in tiposArquivo)
            {
                cbSelecionarTipoArquivo.Items.Add(i);
            }
        }

        public void DisplayImgSearch()
        {
            labelImgSearch.Image = Image.FromFile(BasePath + @"/Searchpng.png");
            labelImgSearch.AutoSize = false;
            labelImgSearch.Size = labelImgSearch.Image.Size;
            labelImgSearch.ImageAlign = ContentAlignment.MiddleCenter;
            labelImgSearch.Text = "";
            labelImgSearch.BackColor = Color.Transparent;
        }

        internal static void ExtractToDirectory(string zipPath, string path)
        {
            throw new NotImplementedException();
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }





        private void cbSelecionarTipoArquivo_Click(object sender, EventArgs e)
        {
            ContagemClick++;

            if (ContagemClick > 0 && ContagemClick < 2)
            {
                cbSelecionarTipoArquivo.Text = "";
            }
        }
        
        private void cbSelecionarTipoArquivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Arquivo zip = new Arquivo();
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Multiselect = true;
                ofd.Title = "Selecionar Arquivos";
                //ofd.InitialDirectory = NomePasta; se eu der esse comando eu inicio a funcao ShowDialog() dentro já dessa pasta
                ofd.Filter = "|*" + cbSelecionarTipoArquivo.Text;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.RestoreDirectory = true;
                //ofd.ReadOnlyChecked = true;
                //ofd.ShowReadOnly = true; // se eu deixar essa funcao verdadeira eu so vou poder visualizar o arquivo

                DialogResult dr = ofd.ShowDialog(); // aqui eu abro a busca do arquivo na maquina

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // Le os arquivos selecionados 

                    foreach (String arquivo in ofd.FileNames)
                    {
                        ArquivoSHAPE = txtNomeArquivo.Text += arquivo;
                    }

                    using (ZipArchive archive = ZipFile.OpenRead(ArquivoSHAPE))
                    {
                        // se for tentar por foreach eles tem tipos diferentes e não daria pra usar o tipo ZipArchiveEntry
                        //List<ZipArchive> zipArchives = new List<ZipArchive>();

                      

                        foreach (ZipArchiveEntry  entry in archive.Entries)
                        {
                            try
                            {
                                if(IsDirectoryEmpty(APP) == true && entry.FullName == "APP.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, APP);
                                }
                                
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(AREA_ALTITUDE_SUPERIOR_1800) == true && entry.FullName == "AREA_ALTITUDE_SUPERIOR_1800.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, AREA_ALTITUDE_SUPERIOR_1800);
                                }

                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(AREA_CONSOLIDADA) == true && entry.FullName == "AREA_CONSOLIDADA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, AREA_CONSOLIDADA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(AREA_DECLIVIDADE_MAIOR_45) == true && entry.FullName == "AREA_DECLIVIDADE_MAIOR_45.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, AREA_DECLIVIDADE_MAIOR_45);
                                }
                                
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(AREA_IMOVEL) == true && entry.FullName == "AREA_IMOVEL.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, AREA_IMOVEL);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(AREA_POUSIO) == true && entry.FullName == "AREA_POUSIO.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, AREA_POUSIO);
                                }
                            }
                            catch { }
                            try
                            {

                                if (IsDirectoryEmpty(AREA_TOPO_MORRO) == true && entry.FullName == "AREA_TOPO_MORRO.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, AREA_TOPO_MORRO);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(BANHADO) == true && entry.FullName == "BANHADO.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, BANHADO);
                                }
                                
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(BORDA_CHAPADA) == true && entry.FullName == "BORDA_CHAPADA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, BORDA_CHAPADA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(HIDROGRAFIA) == true && entry.FullName == "HIDROGRAFIA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, HIDROGRAFIA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(MANGUEZAL) == true && entry.FullName == "MANGUEZAL.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, MANGUEZAL);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(NASCENTE_OLHO_DAGUA) == true && entry.FullName == "NASCENTE_OLHO_DAGUA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, NASCENTE_OLHO_DAGUA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(RESERVA_LEGAL) == true && entry.FullName == "RESERVA_LEGAL.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, RESERVA_LEGAL);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(RESTINGA) == true && entry.FullName == "RESTINGA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, RESTINGA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(SERVIDAO_ADMINISTRATIVA) == true && entry.FullName == "SERVIDAO_ADMINISTRATIVA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, SERVIDAO_ADMINISTRATIVA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(USO_RESTRITO) == true && entry.FullName == "USO_RESTRITO.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, USO_RESTRITO);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(VEGETACAO_NATIVA) == true && entry.FullName == "VEGETACAO_NATIVA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, VEGETACAO_NATIVA);
                                }
                            }
                            catch { }
                            try
                            {
                                if (IsDirectoryEmpty(VEREDA) == true && entry.FullName == "VEREDA.zip")
                                {
                                    zip.ExtrairArquivo(entry.FullName, VEREDA);
                                }
                            }
                            catch { }

                            try
                            {
                                zip.DeletarArquivos2(BasePath);
                            }
                            catch { }

                           // break;

                            // passando so uma vez pelo try eu so coloco os arquivos presentes na camada APP



                        }
                    }

                    //arquivo seria o a pasta zip que queria

                    listFiles.Clear();
                    listView1.Items.Clear();

                    FileInfo fi = new FileInfo(ArquivoSHAPE);// aqui começo a mostrar o arquivo na tela
                    listFiles.Add(fi.FullName);
                    listView1.Items.Add(fi.Name, imageList1.Images.Count - 1);

                    //FileInfo file = new FileInfo(ArquivoSHAPE);
                    //file.MoveTo();



                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //eu so aciono esse metodo quando dou um click no arquivo que foi exposto
            if (listView1.FocusedItem != null)
            {

            }
            //Process.Start(listFiles[listView1.FocusedItem.Index]);// aqui ele abre o arquivo com todas as pastas dentro dele
        }

    }
}
