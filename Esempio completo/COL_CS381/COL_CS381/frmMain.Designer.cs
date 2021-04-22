
namespace COL_CS381
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolSerial = new System.IO.Ports.SerialPort(this.components);
            this.btnAvviaCollaudo = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.boardSerial = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblResult = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbOperatore = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCellPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBoardPort = new System.Windows.Forms.ComboBox();
            this.cbToolPort = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNumberReport = new System.Windows.Forms.Label();
            this.cellSerial = new System.IO.Ports.SerialPort(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apriCartellaReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rilevaPorteAutomarticamenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inviaPacchettoReportAChemitecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolSerial
            // 
            this.toolSerial.PortName = "COM7";
            // 
            // btnAvviaCollaudo
            // 
            this.btnAvviaCollaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvviaCollaudo.Location = new System.Drawing.Point(213, 32);
            this.btnAvviaCollaudo.Name = "btnAvviaCollaudo";
            this.btnAvviaCollaudo.Size = new System.Drawing.Size(117, 107);
            this.btnAvviaCollaudo.TabIndex = 0;
            this.btnAvviaCollaudo.Text = "AVVIA COLLAUDO";
            this.btnAvviaCollaudo.UseVisualStyleBackColor = true;
            this.btnAvviaCollaudo.Click += new System.EventHandler(this.btnAvviaCollaudo_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(336, 32);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(664, 449);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.TextChanged += new System.EventHandler(this.tbOutput_TextChanged);
            // 
            // boardSerial
            // 
            this.boardSerial.PortName = "COM3";
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(12, 496);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 20);
            this.lblResult.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbOperatore);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbCellPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbBoardPort);
            this.groupBox1.Controls.Add(this.cbToolPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 135);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Porte seriali";
            // 
            // tbOperatore
            // 
            this.tbOperatore.Location = new System.Drawing.Point(100, 100);
            this.tbOperatore.Name = "tbOperatore";
            this.tbOperatore.Size = new System.Drawing.Size(84, 20);
            this.tbOperatore.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Operatore";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Seriale cella";
            // 
            // cbCellPort
            // 
            this.cbCellPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCellPort.FormattingEnabled = true;
            this.cbCellPort.Location = new System.Drawing.Point(119, 73);
            this.cbCellPort.Name = "cbCellPort";
            this.cbCellPort.Size = new System.Drawing.Size(65, 21);
            this.cbCellPort.TabIndex = 7;
            this.cbCellPort.Click += new System.EventHandler(this.cbCellPort_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Scheda sotto test";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tool di collaudo";
            // 
            // cbBoardPort
            // 
            this.cbBoardPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoardPort.FormattingEnabled = true;
            this.cbBoardPort.Location = new System.Drawing.Point(119, 46);
            this.cbBoardPort.Name = "cbBoardPort";
            this.cbBoardPort.Size = new System.Drawing.Size(65, 21);
            this.cbBoardPort.TabIndex = 4;
            this.cbBoardPort.Click += new System.EventHandler(this.cbBoardPort_Click);
            // 
            // cbToolPort
            // 
            this.cbToolPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToolPort.FormattingEnabled = true;
            this.cbToolPort.Location = new System.Drawing.Point(119, 19);
            this.cbToolPort.Name = "cbToolPort";
            this.cbToolPort.Size = new System.Drawing.Size(65, 21);
            this.cbToolPort.TabIndex = 0;
            this.cbToolPort.Click += new System.EventHandler(this.cbToolPort_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::COL_CS381.Properties.Resources.Senzanome;
            this.pictureBox1.Location = new System.Drawing.Point(8, 420);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(322, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblNumberReport
            // 
            this.lblNumberReport.AutoSize = true;
            this.lblNumberReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberReport.Location = new System.Drawing.Point(15, 155);
            this.lblNumberReport.Name = "lblNumberReport";
            this.lblNumberReport.Size = new System.Drawing.Size(0, 20);
            this.lblNumberReport.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1011, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apriCartellaReportToolStripMenuItem,
            this.rilevaPorteAutomarticamenteToolStripMenuItem,
            this.inviaPacchettoReportAChemitecToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // apriCartellaReportToolStripMenuItem
            // 
            this.apriCartellaReportToolStripMenuItem.Name = "apriCartellaReportToolStripMenuItem";
            this.apriCartellaReportToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.apriCartellaReportToolStripMenuItem.Text = "Apri cartella Report";
            this.apriCartellaReportToolStripMenuItem.Click += new System.EventHandler(this.apriCartellaReportToolStripMenuItem_Click);
            // 
            // rilevaPorteAutomarticamenteToolStripMenuItem
            // 
            this.rilevaPorteAutomarticamenteToolStripMenuItem.Name = "rilevaPorteAutomarticamenteToolStripMenuItem";
            this.rilevaPorteAutomarticamenteToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.rilevaPorteAutomarticamenteToolStripMenuItem.Text = "Rileva porte automarticamente";
            this.rilevaPorteAutomarticamenteToolStripMenuItem.Click += new System.EventHandler(this.rilevaPorteAutomarticamenteToolStripMenuItem_Click);
            // 
            // inviaPacchettoReportAChemitecToolStripMenuItem
            // 
            this.inviaPacchettoReportAChemitecToolStripMenuItem.Name = "inviaPacchettoReportAChemitecToolStripMenuItem";
            this.inviaPacchettoReportAChemitecToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.inviaPacchettoReportAChemitecToolStripMenuItem.Text = "Invia pacchetto report a Chemitec";
            this.inviaPacchettoReportAChemitecToolStripMenuItem.Click += new System.EventHandler(this.inviaPacchettoReportAChemitecToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 530);
            this.Controls.Add(this.lblNumberReport);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.btnAvviaCollaudo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Collaudo 4001";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort toolSerial;
        private System.Windows.Forms.Button btnAvviaCollaudo;
        private System.Windows.Forms.TextBox tbOutput;
        private System.IO.Ports.SerialPort boardSerial;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCellPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBoardPort;
        private System.Windows.Forms.ComboBox cbToolPort;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbOperatore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNumberReport;
        private System.IO.Ports.SerialPort cellSerial;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apriCartellaReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rilevaPorteAutomarticamenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inviaPacchettoReportAChemitecToolStripMenuItem;
    }
}

