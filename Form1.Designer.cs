
namespace APIFATECForms
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNomeArquivo = new System.Windows.Forms.TextBox();
            this.lblPrint = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.imgLabel = new System.Windows.Forms.Label();
            this.labelImgSearch = new System.Windows.Forms.Label();
            this.cbSelecionarTipoArquivo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(516, 388);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 0;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscaClick);
            // 
            // txtNomeArquivo
            // 
            this.txtNomeArquivo.Location = new System.Drawing.Point(207, 425);
            this.txtNomeArquivo.Name = "txtNomeArquivo";
            this.txtNomeArquivo.ReadOnly = true;
            this.txtNomeArquivo.Size = new System.Drawing.Size(418, 20);
            this.txtNomeArquivo.TabIndex = 4;
            // 
            // lblPrint
            // 
            this.lblPrint.AutoSize = true;
            this.lblPrint.Location = new System.Drawing.Point(301, 355);
            this.lblPrint.Name = "lblPrint";
            this.lblPrint.Size = new System.Drawing.Size(208, 13);
            this.lblPrint.TabIndex = 6;
            this.lblPrint.Text = "                                                                   ";
            this.lblPrint.Click += new System.EventHandler(this.label3_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(15, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(756, 372);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imgLabel
            // 
            this.imgLabel.AutoSize = true;
            this.imgLabel.Location = new System.Drawing.Point(21, 396);
            this.imgLabel.Name = "imgLabel";
            this.imgLabel.Size = new System.Drawing.Size(0, 13);
            this.imgLabel.TabIndex = 9;
            // 
            // labelImgSearch
            // 
            this.labelImgSearch.AutoSize = true;
            this.labelImgSearch.Location = new System.Drawing.Point(265, 390);
            this.labelImgSearch.Name = "labelImgSearch";
            this.labelImgSearch.Size = new System.Drawing.Size(29, 13);
            this.labelImgSearch.TabIndex = 11;
            this.labelImgSearch.Text = "label";
            // 
            // cbSelecionarTipoArquivo
            // 
            this.cbSelecionarTipoArquivo.FormattingEnabled = true;
            this.cbSelecionarTipoArquivo.Location = new System.Drawing.Point(300, 390);
            this.cbSelecionarTipoArquivo.Name = "cbSelecionarTipoArquivo";
            this.cbSelecionarTipoArquivo.Size = new System.Drawing.Size(210, 21);
            this.cbSelecionarTipoArquivo.TabIndex = 12;
            this.cbSelecionarTipoArquivo.Text = "Escolha um tipo de arquivo...";
            this.cbSelecionarTipoArquivo.Click += new System.EventHandler(this.cbSelecionarTipoArquivo_Click);
            this.cbSelecionarTipoArquivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSelecionarTipoArquivo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Arquivo selecionado:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSelecionarTipoArquivo);
            this.Controls.Add(this.labelImgSearch);
            this.Controls.Add(this.imgLabel);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblPrint);
            this.Controls.Add(this.txtNomeArquivo);
            this.Controls.Add(this.btnBuscar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNomeArquivo;
        private System.Windows.Forms.Label lblPrint;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label imgLabel;
        private System.Windows.Forms.Label labelImgSearch;
        private System.Windows.Forms.ComboBox cbSelecionarTipoArquivo;
        private System.Windows.Forms.Label label1;
    }
}

