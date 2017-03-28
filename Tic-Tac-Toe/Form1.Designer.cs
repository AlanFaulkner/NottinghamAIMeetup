namespace Tic_Tac_Toe
{
    partial class userInterface
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
            this.playbutton = new System.Windows.Forms.Button();
            this.backgroundWorker_AIMove = new System.ComponentModel.BackgroundWorker();
            this.player1label = new System.Windows.Forms.Label();
            this.Player1 = new System.Windows.Forms.ComboBox();
            this.Player2 = new System.Windows.Forms.ComboBox();
            this.Player2label = new System.Windows.Forms.Label();
            this.NetFilenameP1 = new System.Windows.Forms.TextBox();
            this.LoadP1Net = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoadP2Net = new System.Windows.Forms.Button();
            this.NetFilenameP2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // playbutton
            // 
            this.playbutton.Location = new System.Drawing.Point(319, 304);
            this.playbutton.Name = "playbutton";
            this.playbutton.Size = new System.Drawing.Size(75, 23);
            this.playbutton.TabIndex = 0;
            this.playbutton.Text = "Play";
            this.playbutton.UseVisualStyleBackColor = true;
            this.playbutton.Click += new System.EventHandler(this.playbutton_Click);
            // 
            // backgroundWorker_AIMove
            // 
            this.backgroundWorker_AIMove.WorkerReportsProgress = true;
            this.backgroundWorker_AIMove.WorkerSupportsCancellation = true;
            this.backgroundWorker_AIMove.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_AIMove_DoWork);
            this.backgroundWorker_AIMove.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_AIMove_Complete);
            // 
            // player1label
            // 
            this.player1label.AutoSize = true;
            this.player1label.Location = new System.Drawing.Point(305, 123);
            this.player1label.Name = "player1label";
            this.player1label.Size = new System.Drawing.Size(45, 13);
            this.player1label.TabIndex = 1;
            this.player1label.Text = "Player 1";
            // 
            // Player1
            // 
            this.Player1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Player1.FormattingEnabled = true;
            this.Player1.Location = new System.Drawing.Point(357, 119);
            this.Player1.Name = "Player1";
            this.Player1.Size = new System.Drawing.Size(97, 21);
            this.Player1.TabIndex = 2;
            // 
            // Player2
            // 
            this.Player2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Player2.FormattingEnabled = true;
            this.Player2.Items.AddRange(new object[] {
            "Human",
            "Random",
            "Easy",
            "Hard",
            "Min-Max",
            "Neural Network"});
            this.Player2.Location = new System.Drawing.Point(357, 210);
            this.Player2.Name = "Player2";
            this.Player2.Size = new System.Drawing.Size(97, 21);
            this.Player2.TabIndex = 4;
            // 
            // Player2label
            // 
            this.Player2label.AutoSize = true;
            this.Player2label.Location = new System.Drawing.Point(305, 214);
            this.Player2label.Name = "Player2label";
            this.Player2label.Size = new System.Drawing.Size(45, 13);
            this.Player2label.TabIndex = 3;
            this.Player2label.Text = "Player 2";
            // 
            // NetFilenameP1
            // 
            this.NetFilenameP1.Enabled = false;
            this.NetFilenameP1.Location = new System.Drawing.Point(357, 146);
            this.NetFilenameP1.Name = "NetFilenameP1";
            this.NetFilenameP1.Size = new System.Drawing.Size(97, 20);
            this.NetFilenameP1.TabIndex = 5;
            // 
            // LoadP1Net
            // 
            this.LoadP1Net.Enabled = false;
            this.LoadP1Net.Location = new System.Drawing.Point(410, 172);
            this.LoadP1Net.Name = "LoadP1Net";
            this.LoadP1Net.Size = new System.Drawing.Size(44, 23);
            this.LoadP1Net.TabIndex = 6;
            this.LoadP1Net.Text = "Load";
            this.LoadP1Net.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(305, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filename";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(305, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Filename";
            // 
            // LoadP2Net
            // 
            this.LoadP2Net.Enabled = false;
            this.LoadP2Net.Location = new System.Drawing.Point(410, 263);
            this.LoadP2Net.Name = "LoadP2Net";
            this.LoadP2Net.Size = new System.Drawing.Size(44, 23);
            this.LoadP2Net.TabIndex = 9;
            this.LoadP2Net.Text = "Load";
            this.LoadP2Net.UseVisualStyleBackColor = true;
            // 
            // NetFilenameP2
            // 
            this.NetFilenameP2.Enabled = false;
            this.NetFilenameP2.Location = new System.Drawing.Point(357, 237);
            this.NetFilenameP2.Name = "NetFilenameP2";
            this.NetFilenameP2.Size = new System.Drawing.Size(97, 20);
            this.NetFilenameP2.TabIndex = 8;
            // 
            // userInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoadP2Net);
            this.Controls.Add(this.NetFilenameP2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoadP1Net);
            this.Controls.Add(this.NetFilenameP1);
            this.Controls.Add(this.Player2);
            this.Controls.Add(this.Player2label);
            this.Controls.Add(this.Player1);
            this.Controls.Add(this.player1label);
            this.Controls.Add(this.playbutton);
            this.Name = "userInterface";
            this.Text = "Tic-Tac-Toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playbutton;
        private System.ComponentModel.BackgroundWorker backgroundWorker_AIMove;
        private System.Windows.Forms.Label player1label;
        private System.Windows.Forms.ComboBox Player1;
        private System.Windows.Forms.ComboBox Player2;
        private System.Windows.Forms.Label Player2label;
        private System.Windows.Forms.TextBox NetFilenameP1;
        private System.Windows.Forms.Button LoadP1Net;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoadP2Net;
        private System.Windows.Forms.TextBox NetFilenameP2;
    }
}

