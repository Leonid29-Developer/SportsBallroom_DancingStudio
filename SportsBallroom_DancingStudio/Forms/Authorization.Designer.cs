namespace SportsBallroom_DancingStudio.Forms
{
    partial class Authorization
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Login = new System.Windows.Forms.TextBox();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_Enter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Button_Register = new System.Windows.Forms.Label();
            this.Password_HideOrShow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Password_HideOrShow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(119, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Авторизация";
            // 
            // TB_Login
            // 
            this.TB_Login.Location = new System.Drawing.Point(93, 146);
            this.TB_Login.MaxLength = 32;
            this.TB_Login.Name = "TB_Login";
            this.TB_Login.Size = new System.Drawing.Size(230, 29);
            this.TB_Login.TabIndex = 2;
            this.TB_Login.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(93, 222);
            this.TB_Password.MaxLength = 32;
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.PasswordChar = '✵';
            this.TB_Password.Size = new System.Drawing.Size(230, 29);
            this.TB_Password.TabIndex = 4;
            this.TB_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(93, 198);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Пароль";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_Enter
            // 
            this.Button_Enter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Button_Enter.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Button_Enter.Location = new System.Drawing.Point(102, 289);
            this.Button_Enter.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Button_Enter.Name = "Button_Enter";
            this.Button_Enter.Size = new System.Drawing.Size(209, 37);
            this.Button_Enter.TabIndex = 5;
            this.Button_Enter.Text = "Авторизироваться";
            this.Button_Enter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Button_Enter.Click += new System.EventHandler(this.Button_Enter_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(93, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Логин";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_Register
            // 
            this.Button_Register.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Button_Register.ForeColor = System.Drawing.Color.Indigo;
            this.Button_Register.Location = new System.Drawing.Point(102, 328);
            this.Button_Register.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Button_Register.Name = "Button_Register";
            this.Button_Register.Size = new System.Drawing.Size(209, 35);
            this.Button_Register.TabIndex = 8;
            this.Button_Register.Text = "Зарегистрироваться";
            this.Button_Register.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Button_Register.Click += new System.EventHandler(this.Button_Register_Click);
            // 
            // Password_HideOrShow
            // 
            this.Password_HideOrShow.BackgroundImage = global::SportsBallroom_DancingStudio.Properties.Resources.EyeClose;
            this.Password_HideOrShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Password_HideOrShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Password_HideOrShow.Location = new System.Drawing.Point(58, 222);
            this.Password_HideOrShow.Name = "Password_HideOrShow";
            this.Password_HideOrShow.Size = new System.Drawing.Size(29, 29);
            this.Password_HideOrShow.TabIndex = 9;
            this.Password_HideOrShow.TabStop = false;
            this.Password_HideOrShow.Click += new System.EventHandler(this.Password_HideOrShow_Click);
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(422, 388);
            this.Controls.Add(this.Password_HideOrShow);
            this.Controls.Add(this.Button_Register);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Button_Enter);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_Login);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.Password_HideOrShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Login;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Button_Enter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Button_Register;
        private System.Windows.Forms.PictureBox Password_HideOrShow;
    }
}