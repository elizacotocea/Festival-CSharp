namespace app.client
{
    partial class mainPage
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
            this.numeCBox = new System.Windows.Forms.TextBox();
            this.locuriCBox = new System.Windows.Forms.TextBox();
            this.numeBox = new System.Windows.Forms.Label();
            this.locuriBox = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.listaShow = new System.Windows.Forms.ListView();
            this.sId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sLocatie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sNrAvailableSeats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sNrSoldSeats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listaArtist = new System.Windows.Forms.ListView();
            this.aId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.aArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.aLocatie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.aOra = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.aNrAvailableSeats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numeCBox
            // 
            this.numeCBox.Location = new System.Drawing.Point(280, 419);
            this.numeCBox.Name = "numeCBox";
            this.numeCBox.Size = new System.Drawing.Size(168, 22);
            this.numeCBox.TabIndex = 2;
            // 
            // locuriCBox
            // 
            this.locuriCBox.Location = new System.Drawing.Point(280, 460);
            this.locuriCBox.Name = "locuriCBox";
            this.locuriCBox.Size = new System.Drawing.Size(168, 22);
            this.locuriCBox.TabIndex = 3;
            // 
            // numeBox
            // 
            this.numeBox.AutoSize = true;
            this.numeBox.Location = new System.Drawing.Point(143, 419);
            this.numeBox.Name = "numeBox";
            this.numeBox.Size = new System.Drawing.Size(121, 17);
            this.numeBox.TabIndex = 4;
            this.numeBox.Text = "Nume cumparator";
            // 
            // locuriBox
            // 
            this.locuriBox.AutoSize = true;
            this.locuriBox.Location = new System.Drawing.Point(143, 465);
            this.locuriBox.Name = "locuriBox";
            this.locuriBox.Size = new System.Drawing.Size(128, 17);
            this.locuriBox.TabIndex = 5;
            this.locuriBox.Text = "Numar locuri dorite";
            this.locuriBox.Click += new System.EventHandler(this.locuriBox_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(193, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 39);
            this.button1.TabIndex = 6;
            this.button1.Text = "Generare bilet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(850, 389);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cauta artistii care au spectacol la o anumita ora";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(782, 424);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Introduceti data:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(919, 424);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(954, 481);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 41);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cauta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listaShow
            // 
            this.listaShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sId,
            this.sArtist,
            this.sLocatie,
            this.sData,
            this.sNrAvailableSeats,
            this.sNrSoldSeats});
            this.listaShow.FullRowSelect = true;
            this.listaShow.HideSelection = false;
            this.listaShow.Location = new System.Drawing.Point(41, 36);
            this.listaShow.Name = "listaShow";
            this.listaShow.Size = new System.Drawing.Size(616, 303);
            this.listaShow.TabIndex = 11;
            this.listaShow.UseCompatibleStateImageBehavior = false;
            this.listaShow.View = System.Windows.Forms.View.Details;
            // 
            // sId
            // 
            this.sId.Text = "Id";
            this.sId.Width = 0;
            // 
            // sArtist
            // 
            this.sArtist.Text = "Artist";
            this.sArtist.Width = 93;
            // 
            // sLocatie
            // 
            this.sLocatie.Text = "Locatie";
            this.sLocatie.Width = 79;
            // 
            // sData
            // 
            this.sData.Text = "Data";
            this.sData.Width = 92;
            // 
            // sNrAvailableSeats
            // 
            this.sNrAvailableSeats.Text = "Loc. disponibile";
            this.sNrAvailableSeats.Width = 108;
            // 
            // sNrSoldSeats
            // 
            this.sNrSoldSeats.Text = "Loc. vandute";
            this.sNrSoldSeats.Width = 177;
            // 
            // listaArtist
            // 
            this.listaArtist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.aId,
            this.aArtist,
            this.aLocatie,
            this.aOra,
            this.aNrAvailableSeats});
            this.listaArtist.HideSelection = false;
            this.listaArtist.Location = new System.Drawing.Point(775, 43);
            this.listaArtist.Name = "listaArtist";
            this.listaArtist.Size = new System.Drawing.Size(429, 296);
            this.listaArtist.TabIndex = 12;
            this.listaArtist.UseCompatibleStateImageBehavior = false;
            this.listaArtist.View = System.Windows.Forms.View.Details;
            // 
            // aId
            // 
            this.aId.Text = "aId";
            this.aId.Width = 0;
            // 
            // aArtist
            // 
            this.aArtist.Text = "Artist";
            this.aArtist.Width = 62;
            // 
            // aLocatie
            // 
            this.aLocatie.Text = "Locatie";
            this.aLocatie.Width = 108;
            // 
            // aOra
            // 
            this.aOra.Text = "Ora";
            this.aOra.Width = 86;
            // 
            // aNrAvailableSeats
            // 
            this.aNrAvailableSeats.Text = "Loc. disponibile";
            this.aNrAvailableSeats.Width = 221;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(626, 506);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(148, 49);
            this.button3.TabIndex = 13;
            this.button3.Text = "Log out";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // mainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 581);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listaArtist);
            this.Controls.Add(this.listaShow);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.locuriBox);
            this.Controls.Add(this.numeBox);
            this.Controls.Add(this.locuriCBox);
            this.Controls.Add(this.numeCBox);
            this.Name = "mainPage";
            this.Text = "mainPage";
            this.Load += new System.EventHandler(this.mainPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox numeCBox;
        private System.Windows.Forms.TextBox locuriCBox;
        private System.Windows.Forms.Label numeBox;
        private System.Windows.Forms.Label locuriBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listaShow;
        private System.Windows.Forms.ColumnHeader sId;
        private System.Windows.Forms.ColumnHeader sArtist;
        private System.Windows.Forms.ColumnHeader sLocatie;
        private System.Windows.Forms.ColumnHeader sData;
        private System.Windows.Forms.ColumnHeader sNrAvailableSeats;
        private System.Windows.Forms.ColumnHeader sNrSoldSeats;
        private System.Windows.Forms.ListView listaArtist;
        private System.Windows.Forms.ColumnHeader aId;
        private System.Windows.Forms.ColumnHeader aArtist;
        private System.Windows.Forms.ColumnHeader aLocatie;
        private System.Windows.Forms.ColumnHeader aOra;
        private System.Windows.Forms.ColumnHeader aNrAvailableSeats;
        private System.Windows.Forms.Button button3;
    }
}