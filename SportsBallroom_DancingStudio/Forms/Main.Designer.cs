namespace SportsBallroom_DancingStudio.Forms
{
    partial class Main
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
            this.Table = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // Table
            // 
            this.Table.ColumnCount = 15;
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.Location = new System.Drawing.Point(0, 0);
            this.Table.Name = "Table";
            this.Table.RowCount = 7;
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.Size = new System.Drawing.Size(1367, 648);
            this.Table.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 648);
            this.Controls.Add(this.Table);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Table;
    }
}