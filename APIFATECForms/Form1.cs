using EGIS.ShapeFileLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace APIFATECForms
{
    public partial class Form1 : Form
    {
        public static string PastaDaAplicacao = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string BasePath = Path.Combine(PastaDaAplicacao, "BasePath");
        public static string AREA_IMOVEL = Path.Combine(BasePath, "AREA_IMOVEL");

        static string dbfFileName = @"C:\Users\Agostin\Desktop\Grupo-JavaPastry-BranchTeste\grupo02-javapastry\APIFATECForms\BasePath\SHAPEFILE_BRASIL\BR_UF_2020.dbf";
        static string constr = "Provider = VFPOLEDB.1; Data Source =" + Directory.GetParent(dbfFileName).FullName;

        private Color CorMapaInicial = Color.LightGray;
        private Color CorVerdeDiferente = Color.SeaGreen;
        private Color CorVerde = Color.ForestGreen;
        private Color CorAzul = Color.AliceBlue;
        private Color CorPreta = Color.Black;
        private Color CorBranca = Color.White;

        Font font1 = new Font("Century Gothic", 9, FontStyle.Bold, GraphicsUnit.Pixel);

        int ContagemClickImgPasta = 0;
        int ClickTxtArquivoPExtrair = 0;
        string ShapeFileZipado = "";

        public static List<BR_UF_2020> ListaRegiaoNorte = new List<BR_UF_2020>();
        public static List<BR_UF_2020> ListaRegiaoNordeste = new List<BR_UF_2020>();
        public static List<BR_UF_2020> ListaRegiaoCentroOeste = new List<BR_UF_2020>();
        public static List<BR_UF_2020> ListaRegiaoSudeste = new List<BR_UF_2020>();
        public static List<BR_UF_2020> ListaRegiaoSul = new List<BR_UF_2020>();

        public object ObjTipoObjClasseForm1 = new object();

        EGIS.ShapeFileLib.ShapeFile sf;

        public Form1()
        {
            InitializeComponent();
            ContagemClickImgPasta = ContagemClickImgPasta + 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Arquivo zip = new Arquivo();

            DisplayImgSearch();

            zip.CriarPasta(BasePath);
            zip.CriarPasta(AREA_IMOVEL);

            OpenShapefile(@"C:\Users\Agostin\Desktop\Grupo-JavaPastry-BranchTeste\grupo02-javapastry\APIFATECForms\BasePath\SHAPEFILE_BRASIL\BR_UF_2020.shp");

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

        public void CaixaDeMensagem()
        {
            MessageBox.Show("Insira um tipo de arquivo válido.", "Erro",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);

            txtArquivoPExtrair.Text = "";
        }

        public void OpenShapefile(string path)
        {
            this.sfMap1.ClearShapeFiles();

            try
            {
                ConexaoDados dados = new ConexaoDados();
                // try pra caso o usuario digite o arquivo de texto que nao tem extensao .shp
                this.sfMap1.AddShapeFile(path, "ShapeFile", ""); // o ultimo parametro nao pode ser nulo
                RenderMap();
                dados.OpenConnection(constr);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

        public void RefreshMap()
        {
            // verificar esse metodo pq ele converte para o tamanho de zoom 13 e nao o atual que a imagem esta
            double zoomLevel = 13;
            this.sfMap1.ZoomLevel = zoomLevel;
        }

        public void CenterMap()
        {
            ZoomMap();
            PointD point = new PointD(-51.5,-14.38); // quanto maior o valor do y, mais o mapa desce 
            this.sfMap1.CentrePoint2D= point;
        }

        public void ZoomMap()
        {
            double zoomLevel = 20;
            this.sfMap1.ZoomLevel = zoomLevel;
        }

        public void RenderMap()
        {
            sf = this.sfMap1[0];
            
            sf.RenderSettings.FillColor = CorMapaInicial; // aqui eu seto a cor do mapa inteiro
            sf.RenderSettings.Font = font1; // aqui eu defino a fonte a ser usada no mapa
            sf.RenderSettings.SelectFillColor = CorVerdeDiferente; // aqui eu defino a cor que vai ficar a regiao do mapa que fi clicada no getrecord
            sf.RenderSettings.OutlineColor = CorBranca; // define a cor das bordas nao selecionadas
            sf.RenderSettings.SelectOutlineColor = CorAzul; // aqui eu defino a cor do mapa quando selecionado // aqui eu defino a cor das bordas
            sf.RenderSettings.FieldName = sf.RenderSettings.DbfReader.GetFieldNames()[1]; // selecionando 1 eu pego todos os nomes das UFs
            RefreshMap();
        }


        private void btnTresPontosClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtNomeArquivo.Text = ofd.FileName; // printa o nome do arquivo selecionado
            }

        }

        private void ImgPastaFechadaClick(object sender, EventArgs e)
        {
            // funcao para dar movimento na imagem da pasta
            // o contagem click inicia com valor 1, assim que inicializamos os componentes do forms.
            // iniciando com o valor 1 a imagem da pasta "iria abrir", sabendo que ela inicia fechada.

            ImgPastaFechada.Size = ImgPastaFechada.Size;
            ImgPastaFechada.BackColor = Color.Transparent;
            ImgPastaFechada.Image = Image.FromFile(BasePath + @"/ImgPastaAberta-removebg-preview-redimensionada.png");

            LabelBaseDeDados.BorderStyle = BorderStyle.FixedSingle;
            LabelRegiaoNorte.Visible = true;
            LabelRegiaoNordeste.Visible = true;
            LabelRegiaoCentroOeste.Visible = true;
            LabelRegiaoSudeste.Visible = true;
            LabelRegiaoSul.Visible = true;

            if (ContagemClickImgPasta > 1)
            {
                ImgPastaFechada.Image = Image.FromFile(BasePath + @"/ImgPastaFechada-removebg-preview-redimensionada.png");
                ContagemClickImgPasta = ContagemClickImgPasta - 1;
                LabelRegiaoNorte.Visible = false;
                LabelRegiaoNordeste.Visible = false;
                LabelRegiaoCentroOeste.Visible = false;
                LabelRegiaoSudeste.Visible = false;
                LabelRegiaoSul.Visible = false;
            }

            if (ContagemClickImgPasta == 0)
            {
                ImgPastaFechada.Image = Image.FromFile(BasePath + @"/ImgPastaFechada-removebg-preview-redimensionada.png");
                ContagemClickImgPasta = ContagemClickImgPasta - 1;
                LabelRegiaoNorte.Visible = false;
                LabelRegiaoNordeste.Visible = false;
                LabelRegiaoCentroOeste.Visible = false;
                LabelRegiaoSudeste.Visible = false;
                LabelRegiaoSul.Visible = false;
            }

            ContagemClickImgPasta = ContagemClickImgPasta - 1;

            if (ContagemClickImgPasta == -2)
            {
                ContagemClickImgPasta = 1;
            }

        }

        private void BaseDeDadosClick(object sender, EventArgs e)
        {
            ImgPastaFechadaClick(sender, e);
            LabelBaseDeDados.BorderStyle = BorderStyle.Fixed3D; ;
            LabelRegiaoNorte.BorderStyle = BorderStyle.None;
            LabelRegiaoNordeste.BorderStyle = BorderStyle.None;
            LabelRegiaoCentroOeste.BorderStyle = BorderStyle.None;
            LabelRegiaoSul.BorderStyle = BorderStyle.None;
            LabelRegiaoSudeste.BorderStyle = BorderStyle.None;

            sf.ClearSelectedRecords();
            RefreshMap();

        }

        private void txtArquivoPExtrairKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Arquivo zip = new Arquivo();
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Title = "Busca de Arquivo";
                //ofd.InitialDirectory = NomePasta; se eu der esse comando eu inicio a funcao ShowDialog() dentro já dessa pasta
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.RestoreDirectory = true;
                ofd.FileName = txtArquivoPExtrair.Text.Trim().ToUpper();
                ofd.Filter = "*(.zip)|*zip";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (String arquivo in ofd.FileNames)
                    {
                        ShapeFileZipado = txtNomeArquivo.Text += arquivo;
                    }

                    try
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(ShapeFileZipado))
                        {
                            Arquivo arquivo = new Arquivo();
                            zip.DeletarArquivos(AREA_IMOVEL);
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                try
                                {
                                    if (IsDirectoryEmpty(AREA_IMOVEL) == true && entry.FullName == "AREA_IMOVEL.zip")
                                    {

                                        zip.ExtrairArquivo(entry.FullName, AREA_IMOVEL);
                                        // DEPOIS QUE EXTRAI OS DOCUMENTOS EU TENHO QUE POPULAR O BANCO
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    //CaixaDeMensagem();
                }

            }
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private void txtArquivoPExtrairClick(object sender, EventArgs e)
        {
            ClickTxtArquivoPExtrair++;

            if (txtArquivoPExtrair.Text != "" && ClickTxtArquivoPExtrair == 1)
            {
                txtArquivoPExtrair.Text = "";
            }

            if (txtArquivoPExtrair.Text != "" && ClickTxtArquivoPExtrair == 3)
            {
                txtArquivoPExtrair.Text = txtArquivoPExtrair.Text;
            }

            ClickTxtArquivoPExtrair = ClickTxtArquivoPExtrair + 1;

            if (ClickTxtArquivoPExtrair == 4)
            {
                ClickTxtArquivoPExtrair = 2;
            }

        }

        private void labelImgSearch_Click(object sender, EventArgs e)
        {
            txtArquivoPExtrair.Text = "";
        }

        private void btnBuscarClick(object sender, EventArgs e)
        {

        }

        private void sfMap1_MouseClick(object sender, MouseEventArgs e)
        {
            // como o proprio nome ja diz, essa funcao é executada assim que eu dou um click em algum lugar do mapa
            // essa funcao e responsavel por pegar todas as informaçoes do local que ela clicou

            if (sfMap1.ShapeFileCount == 0) return;

            int recordIndex = sfMap1.GetShapeIndexAtPixelCoord(0, e.Location, 8);
            if (recordIndex >= 0)
            {
                string[] recordAttributes = sfMap1[0].GetAttributeFieldValues(recordIndex);// pego os atributos dentro dauqle lugar que foi clicado
                string[] attributeNames = sfMap1[0].GetAttributeFieldNames();
                StringBuilder sb = new StringBuilder();
                for (int n = 0; n < attributeNames.Length; ++n)
                {
                    sb.Append(attributeNames[n]).Append(':' + " ").AppendLine(recordAttributes[n].Trim());
                }
                MessageBox.Show(this, sb.ToString(), "Atributos da seleção", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }


        private void RegiaoNorte_Click(object sender, EventArgs e)
        {
            sf.ClearSelectedRecords();

            LabelBaseDeDados.BorderStyle = BorderStyle.None;
            LabelRegiaoNorte.BorderStyle = BorderStyle.Fixed3D;
            LabelRegiaoNordeste.BorderStyle = BorderStyle.None;
            LabelRegiaoCentroOeste.BorderStyle = BorderStyle.None;
            LabelRegiaoSul.BorderStyle = BorderStyle.None;
            LabelRegiaoSudeste.BorderStyle = BorderStyle.None;

            List<int> RegiaoNorte = new List<int>();

            foreach (var uf in ListaRegiaoNorte)
            {
                if (uf.nm_regiao == "Norte")
                {
                    RegiaoNorte.Add(uf.cd_uf);
                }
            }

            foreach (var item in RegiaoNorte)
            {
                if (item == 11)
                {
                    int primeiro = item - 11;
                    sf.SelectRecord(primeiro, true);
                }
                int i = item - 10;

                sf.SelectRecord(i, true);
            }

            RefreshMap();

        }

        private void RegiaoNordeste_Click(object sender, EventArgs e)
        {
            sf.ClearSelectedRecords();

            LabelBaseDeDados.BorderStyle = BorderStyle.None;
            LabelRegiaoNorte.BorderStyle = BorderStyle.None;
            LabelRegiaoNordeste.BorderStyle = BorderStyle.Fixed3D;
            LabelRegiaoCentroOeste.BorderStyle = BorderStyle.None;
            LabelRegiaoSul.BorderStyle = BorderStyle.None;
            LabelRegiaoSudeste.BorderStyle = BorderStyle.None;

            List<int> RegiaoNordeste = new List<int>();

            foreach (var uf in ListaRegiaoNordeste)
            {
                if (uf.nm_regiao == "Nordeste")
                {
                    RegiaoNordeste.Add(uf.cd_uf);
                }
            }

            foreach (var item in RegiaoNordeste)
            {

                int i = item - 14;

                sf.SelectRecord(i, true);
            }

            RefreshMap();

        }

        private void RegiaoCentroOeste_Click(object sender, EventArgs e)
        {
            sf.ClearSelectedRecords();

            LabelBaseDeDados.BorderStyle = BorderStyle.None;
            LabelRegiaoNorte.BorderStyle = BorderStyle.None;
            LabelRegiaoNordeste.BorderStyle = BorderStyle.None;
            LabelRegiaoCentroOeste.BorderStyle = BorderStyle.Fixed3D;
            LabelRegiaoSul.BorderStyle = BorderStyle.None;
            LabelRegiaoSudeste.BorderStyle = BorderStyle.None;

            List<int> RegiaoCentroOeste = new List<int>();

            foreach (var uf in ListaRegiaoCentroOeste)
            {
                if (uf.nm_regiao == "Centro-oeste")
                {
                    RegiaoCentroOeste.Add(uf.cd_uf);
                }
            }

            foreach (var item in RegiaoCentroOeste)
            {
                int i = item - 27;

                sf.SelectRecord(i, true);
            }

            RefreshMap();

        }

        private void RegiaoSudeste_Click(object sender, EventArgs e)
        {
            sf.ClearSelectedRecords();

            LabelBaseDeDados.BorderStyle = BorderStyle.None;
            LabelRegiaoNorte.BorderStyle = BorderStyle.None;
            LabelRegiaoNordeste.BorderStyle = BorderStyle.None;
            LabelRegiaoCentroOeste.BorderStyle = BorderStyle.None;
            LabelRegiaoSul.BorderStyle = BorderStyle.None;
            LabelRegiaoSudeste.BorderStyle = BorderStyle.Fixed3D;

            List<int> RegiaoSudeste = new List<int>();

            foreach (var uf in ListaRegiaoSudeste)
            {
                if (uf.nm_regiao == "Sudeste")
                {
                    RegiaoSudeste.Add(uf.cd_uf);
                }
            }

            foreach (var item in RegiaoSudeste)
            {
                if (item == 35)
                {
                    int ii = item - 16;
                    sf.SelectRecord(ii, true);
                }

                int i = item - 15;
                if (i < 20)
                {
                    sf.SelectRecord(i, true);
                }
            }

            RefreshMap();

        }

        private void LabelRegiaoSul_Click(object sender, EventArgs e)
        {
            sf.ClearSelectedRecords();

            LabelBaseDeDados.BorderStyle = BorderStyle.None;
            LabelRegiaoNorte.BorderStyle = BorderStyle.None;
            LabelRegiaoNordeste.BorderStyle = BorderStyle.None;
            LabelRegiaoCentroOeste.BorderStyle = BorderStyle.None;
            LabelRegiaoSul.BorderStyle = BorderStyle.Fixed3D;
            LabelRegiaoSudeste.BorderStyle = BorderStyle.None;

            List<int> RegiaoSul = new List<int>();

            foreach (var uf in ListaRegiaoSul)
            {
                if (uf.nm_regiao == "Sul")
                {
                    RegiaoSul.Add(uf.cd_uf);
                }
            }

            foreach (var item in RegiaoSul)
            {
                int i = item - 21;
                sf.SelectRecord(i, true);
            }

            RefreshMap();

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            LabelBaseDeDados.BorderStyle = BorderStyle.None;    
        }

        private void ImgCenter_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(ImgCenter, "Centralizar mapa");
        }

        private void ImgCenter_Click(object sender, EventArgs e)
        {
            CenterMap();
            RefreshMap();
        }

        private void ImgZoomIn_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(ImgZoomIn, "Mais zoom");

        }

        private void ImgZoomOut_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(ImgZoomOut, "Menos zoom");
        }
    }
}
