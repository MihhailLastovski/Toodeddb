namespace Toodeddb
{
    partial class Admin
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
            this.components = new System.ComponentModel.Container();
            this.Lisabtn = new System.Windows.Forms.Button();
            this.Uuendabtn = new System.Windows.Forms.Button();
            this.Kustutabtn = new System.Windows.Forms.Button();
            this.kategoorialbl = new System.Windows.Forms.Label();
            this.toodelbl = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.hindlbl = new System.Windows.Forms.Label();
            this.txt_bonus = new System.Windows.Forms.TextBox();
            this.txt_tel = new System.Windows.Forms.TextBox();
            this.txt_isik = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.kliendidBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tooded_DBDataSet = new Toodeddb.Tooded_DBDataSet();
            this.kliendidTableAdapter = new Toodeddb.Tooded_DBDataSetTableAdapters.KliendidTableAdapter();
            this.kliendidBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.tooded_DBDataSet1 = new Toodeddb.Tooded_DBDataSet1();
            this.kliendidBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.kliendidTableAdapter1 = new Toodeddb.Tooded_DBDataSet1TableAdapters.KliendidTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.kliendidBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tooded_DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kliendidBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tooded_DBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kliendidBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // Lisabtn
            // 
            this.Lisabtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Lisabtn.Location = new System.Drawing.Point(592, 25);
            this.Lisabtn.Name = "Lisabtn";
            this.Lisabtn.Size = new System.Drawing.Size(174, 32);
            this.Lisabtn.TabIndex = 27;
            this.Lisabtn.Text = "Lisa";
            this.Lisabtn.UseVisualStyleBackColor = true;
            this.Lisabtn.Click += new System.EventHandler(this.Lisabtn_Click_1);
            // 
            // Uuendabtn
            // 
            this.Uuendabtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Uuendabtn.Location = new System.Drawing.Point(592, 73);
            this.Uuendabtn.Name = "Uuendabtn";
            this.Uuendabtn.Size = new System.Drawing.Size(174, 32);
            this.Uuendabtn.TabIndex = 26;
            this.Uuendabtn.Text = "Uuenda";
            this.Uuendabtn.UseVisualStyleBackColor = true;
            this.Uuendabtn.Click += new System.EventHandler(this.Uuendabtn_Click);
            // 
            // Kustutabtn
            // 
            this.Kustutabtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Kustutabtn.Location = new System.Drawing.Point(592, 122);
            this.Kustutabtn.Name = "Kustutabtn";
            this.Kustutabtn.Size = new System.Drawing.Size(174, 32);
            this.Kustutabtn.TabIndex = 25;
            this.Kustutabtn.Text = "Kustuta";
            this.Kustutabtn.UseVisualStyleBackColor = true;
            this.Kustutabtn.Click += new System.EventHandler(this.Kustutabtn_Click);
            // 
            // kategoorialbl
            // 
            this.kategoorialbl.AutoSize = true;
            this.kategoorialbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.kategoorialbl.Location = new System.Drawing.Point(323, 62);
            this.kategoorialbl.Name = "kategoorialbl";
            this.kategoorialbl.Size = new System.Drawing.Size(91, 20);
            this.kategoorialbl.TabIndex = 21;
            this.kategoorialbl.Text = "Kliendikaart";
            // 
            // toodelbl
            // 
            this.toodelbl.AutoSize = true;
            this.toodelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.toodelbl.Location = new System.Drawing.Point(95, 24);
            this.toodelbl.Name = "toodelbl";
            this.toodelbl.Size = new System.Drawing.Size(38, 18);
            this.toodelbl.TabIndex = 18;
            this.toodelbl.Text = "Nimi";
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_name.Location = new System.Drawing.Point(148, 25);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(142, 20);
            this.txt_name.TabIndex = 17;
            // 
            // hindlbl
            // 
            this.hindlbl.AutoSize = true;
            this.hindlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.hindlbl.Location = new System.Drawing.Point(105, 63);
            this.hindlbl.Name = "hindlbl";
            this.hindlbl.Size = new System.Drawing.Size(28, 18);
            this.hindlbl.TabIndex = 20;
            this.hindlbl.Text = "Tel";
            // 
            // txt_bonus
            // 
            this.txt_bonus.Enabled = false;
            this.txt_bonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_bonus.Location = new System.Drawing.Point(429, 25);
            this.txt_bonus.Name = "txt_bonus";
            this.txt_bonus.Size = new System.Drawing.Size(142, 20);
            this.txt_bonus.TabIndex = 30;
            // 
            // txt_tel
            // 
            this.txt_tel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_tel.Location = new System.Drawing.Point(148, 63);
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Size = new System.Drawing.Size(142, 20);
            this.txt_tel.TabIndex = 31;
            this.txt_tel.Text = "+372";
            // 
            // txt_isik
            // 
            this.txt_isik.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_isik.Location = new System.Drawing.Point(148, 106);
            this.txt_isik.Name = "txt_isik";
            this.txt_isik.Size = new System.Drawing.Size(142, 20);
            this.txt_isik.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(61, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 32;
            this.label1.Text = "Isikukood";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lbl.Location = new System.Drawing.Point(323, 24);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(91, 18);
            this.lbl.TabIndex = 34;
            this.lbl.Text = "Bonus punkt";
            // 
            // kliendidBindingSource
            // 
            this.kliendidBindingSource.DataMember = "Kliendid";
            this.kliendidBindingSource.DataSource = this.tooded_DBDataSet;
            // 
            // tooded_DBDataSet
            // 
            this.tooded_DBDataSet.DataSetName = "Tooded_DBDataSet";
            this.tooded_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // kliendidTableAdapter
            // 
            this.kliendidTableAdapter.ClearBeforeFill = true;
            // 
            // kliendidBindingSource2
            // 
            this.kliendidBindingSource2.DataMember = "Kliendid";
            this.kliendidBindingSource2.DataSource = this.tooded_DBDataSet1;
            // 
            // tooded_DBDataSet1
            // 
            this.tooded_DBDataSet1.DataSetName = "Tooded_DBDataSet1";
            this.tooded_DBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // kliendidBindingSource1
            // 
            this.kliendidBindingSource1.DataMember = "Kliendid";
            this.kliendidBindingSource1.DataSource = this.tooded_DBDataSet;
            // 
            // kliendidTableAdapter1
            // 
            this.kliendidTableAdapter1.ClearBeforeFill = true;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.txt_isik);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_tel);
            this.Controls.Add(this.txt_bonus);
            this.Controls.Add(this.Lisabtn);
            this.Controls.Add(this.Uuendabtn);
            this.Controls.Add(this.Kustutabtn);
            this.Controls.Add(this.kategoorialbl);
            this.Controls.Add(this.hindlbl);
            this.Controls.Add(this.toodelbl);
            this.Controls.Add(this.txt_name);
            this.Name = "Admin";
            this.Text = "Admin";
            ((System.ComponentModel.ISupportInitialize)(this.kliendidBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tooded_DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kliendidBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tooded_DBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kliendidBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Lisabtn;
        private System.Windows.Forms.Button Uuendabtn;
        private System.Windows.Forms.Button Kustutabtn;
        private System.Windows.Forms.Label kategoorialbl;
        private System.Windows.Forms.Label toodelbl;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label hindlbl;
        private System.Windows.Forms.TextBox txt_bonus;
        private System.Windows.Forms.TextBox txt_tel;
        private System.Windows.Forms.TextBox txt_isik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl;
        private Tooded_DBDataSet tooded_DBDataSet;
        private System.Windows.Forms.BindingSource kliendidBindingSource;
        private Tooded_DBDataSetTableAdapters.KliendidTableAdapter kliendidTableAdapter;
        private System.Windows.Forms.BindingSource kliendidBindingSource1;
        private Tooded_DBDataSet1 tooded_DBDataSet1;
        private System.Windows.Forms.BindingSource kliendidBindingSource2;
        private Tooded_DBDataSet1TableAdapters.KliendidTableAdapter kliendidTableAdapter1;
    }
}