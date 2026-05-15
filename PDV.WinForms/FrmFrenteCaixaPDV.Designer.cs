namespace PDV.WinForms
{
    partial class FrmFrenteCaixaPDV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMenu = new Panel();
            lblStatusCaixa = new Label();
            btnNovaVenda = new Button();
            lblTotalGeral = new Label();
            dgvItens = new DataGridView();
            lblTotalItem = new Label();
            lblPrecoUnitario = new Label();
            lblQuantidade = new Label();
            txtTotalItem = new TextBox();
            txtPrecoUnitario = new TextBox();
            txtQuantidade = new TextBox();
            btnAdicionar = new Button();
            txtCodigoBarras = new TextBox();
            lblCodicoBarras = new Label();
            pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItens).BeginInit();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = SystemColors.GradientInactiveCaption;
            pnlMenu.Controls.Add(lblStatusCaixa);
            pnlMenu.Location = new Point(-3, -6);
            pnlMenu.Margin = new Padding(3, 4, 3, 4);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(936, 39);
            pnlMenu.TabIndex = 14;
            // 
            // lblStatusCaixa
            // 
            lblStatusCaixa.AutoSize = true;
            lblStatusCaixa.Font = new Font("Bahnschrift Condensed", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatusCaixa.Location = new Point(10, 1);
            lblStatusCaixa.Name = "lblStatusCaixa";
            lblStatusCaixa.Size = new Size(160, 39);
            lblStatusCaixa.TabIndex = 0;
            lblStatusCaixa.Text = "CAIXA ABERTO";
            // 
            // btnNovaVenda
            // 
            btnNovaVenda.Location = new Point(13, 451);
            btnNovaVenda.Name = "btnNovaVenda";
            btnNovaVenda.Size = new Size(126, 34);
            btnNovaVenda.TabIndex = 26;
            btnNovaVenda.Text = "Nova Venda";
            btnNovaVenda.UseVisualStyleBackColor = true;
            // 
            // lblTotalGeral
            // 
            lblTotalGeral.AutoSize = true;
            lblTotalGeral.Font = new Font("Bahnschrift Condensed", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalGeral.ForeColor = Color.CornflowerBlue;
            lblTotalGeral.Location = new Point(679, 427);
            lblTotalGeral.Name = "lblTotalGeral";
            lblTotalGeral.Size = new Size(236, 58);
            lblTotalGeral.TabIndex = 25;
            lblTotalGeral.Text = "Total: R$ 0,00";
            // 
            // dgvItens
            // 
            dgvItens.AllowUserToAddRows = false;
            dgvItens.AllowUserToDeleteRows = false;
            dgvItens.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvItens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItens.BackgroundColor = SystemColors.ControlLightLight;
            dgvItens.BorderStyle = BorderStyle.Fixed3D;
            dgvItens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItens.Location = new Point(279, 47);
            dgvItens.Name = "dgvItens";
            dgvItens.ReadOnly = true;
            dgvItens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItens.Size = new Size(636, 353);
            dgvItens.TabIndex = 24;
            // 
            // lblTotalItem
            // 
            lblTotalItem.AutoSize = true;
            lblTotalItem.Font = new Font("Bahnschrift Condensed", 12F);
            lblTotalItem.Location = new Point(13, 268);
            lblTotalItem.Name = "lblTotalItem";
            lblTotalItem.Size = new Size(35, 19);
            lblTotalItem.TabIndex = 23;
            lblTotalItem.Text = "Total";
            // 
            // lblPrecoUnitario
            // 
            lblPrecoUnitario.AutoSize = true;
            lblPrecoUnitario.Font = new Font("Bahnschrift Condensed", 12F);
            lblPrecoUnitario.Location = new Point(13, 232);
            lblPrecoUnitario.Name = "lblPrecoUnitario";
            lblPrecoUnitario.Size = new Size(84, 19);
            lblPrecoUnitario.TabIndex = 22;
            lblPrecoUnitario.Text = "Preço Unitário";
            // 
            // lblQuantidade
            // 
            lblQuantidade.AutoSize = true;
            lblQuantidade.Font = new Font("Bahnschrift Condensed", 12F);
            lblQuantidade.Location = new Point(13, 195);
            lblQuantidade.Name = "lblQuantidade";
            lblQuantidade.Size = new Size(69, 19);
            lblQuantidade.TabIndex = 21;
            lblQuantidade.Text = "Quantidade";
            // 
            // txtTotalItem
            // 
            txtTotalItem.Location = new Point(97, 258);
            txtTotalItem.Margin = new Padding(3, 4, 3, 4);
            txtTotalItem.Name = "txtTotalItem";
            txtTotalItem.ReadOnly = true;
            txtTotalItem.Size = new Size(127, 23);
            txtTotalItem.TabIndex = 20;
            // 
            // txtPrecoUnitario
            // 
            txtPrecoUnitario.Location = new Point(97, 222);
            txtPrecoUnitario.Margin = new Padding(3, 4, 3, 4);
            txtPrecoUnitario.Name = "txtPrecoUnitario";
            txtPrecoUnitario.ReadOnly = true;
            txtPrecoUnitario.Size = new Size(127, 23);
            txtPrecoUnitario.TabIndex = 19;
            // 
            // txtQuantidade
            // 
            txtQuantidade.Location = new Point(97, 185);
            txtQuantidade.Margin = new Padding(3, 4, 3, 4);
            txtQuantidade.Name = "txtQuantidade";
            txtQuantidade.ReadOnly = true;
            txtQuantidade.Size = new Size(127, 23);
            txtQuantidade.TabIndex = 18;
            // 
            // btnAdicionar
            // 
            btnAdicionar.Location = new Point(154, 127);
            btnAdicionar.Margin = new Padding(3, 4, 3, 4);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(69, 29);
            btnAdicionar.TabIndex = 17;
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.UseVisualStyleBackColor = true;
            // 
            // txtCodigoBarras
            // 
            txtCodigoBarras.Location = new Point(7, 90);
            txtCodigoBarras.Margin = new Padding(3, 4, 3, 4);
            txtCodigoBarras.Name = "txtCodigoBarras";
            txtCodigoBarras.Size = new Size(217, 23);
            txtCodigoBarras.TabIndex = 16;
            // 
            // lblCodicoBarras
            // 
            lblCodicoBarras.AutoSize = true;
            lblCodicoBarras.Font = new Font("Bahnschrift Condensed", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCodicoBarras.Location = new Point(3, 48);
            lblCodicoBarras.Name = "lblCodicoBarras";
            lblCodicoBarras.Size = new Size(257, 23);
            lblCodicoBarras.TabIndex = 15;
            lblCodicoBarras.Text = "Código de Barras/Código Interno/Nome:";
            // 
            // FrmFrenteCaixaPDV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(929, 534);
            Controls.Add(pnlMenu);
            Controls.Add(btnNovaVenda);
            Controls.Add(lblTotalGeral);
            Controls.Add(dgvItens);
            Controls.Add(lblTotalItem);
            Controls.Add(lblPrecoUnitario);
            Controls.Add(lblQuantidade);
            Controls.Add(txtTotalItem);
            Controls.Add(txtPrecoUnitario);
            Controls.Add(txtQuantidade);
            Controls.Add(btnAdicionar);
            Controls.Add(txtCodigoBarras);
            Controls.Add(lblCodicoBarras);
            Name = "FrmFrenteCaixaPDV";
            Text = "FrmFrenteCaixaPDV";
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItens).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlMenu;
        private Label lblStatusCaixa;
        private Button btnNovaVenda;
        private Label lblTotalGeral;
        private DataGridView dgvItens;
        private Label lblTotalItem;
        private Label lblPrecoUnitario;
        private Label lblQuantidade;
        private TextBox txtTotalItem;
        private TextBox txtPrecoUnitario;
        private TextBox txtQuantidade;
        private Button btnAdicionar;
        private TextBox txtCodigoBarras;
        private Label lblCodicoBarras;
    }
}