using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SportsBallroom_DancingStudio.Forms
{
    public partial class Authorization : Form
    {
        /// <summary> Конструктор </summary>
        public Authorization() => InitializeComponent();

        /// <summary>  Показан или спрятан пароль </summary>
        private bool HideOrShow = true;

        /// <summary> Идентификатор пользователя, находящегося в данной сессии </summary>
        public static string UserID { get; private set; } = null;

        /// <summary> Отражает права авторизированного пользователя </summary>
        public static bool Admin { get; private set; }

        /// <summary> Показать или спрятать ввод пароля </summary>
        private void Password_HideOrShow_Click(object sender, EventArgs e)
        {
            if (HideOrShow)
            {
                HideOrShow = false;
                TB_Password.PasswordChar = TB_Login.PasswordChar;
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeOpen;
            }
            else
            {
                HideOrShow = true;
                TB_Password.PasswordChar = '✵';
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeClose;
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Авторизация
        /// </summary>
        private void Button_Enter_Click(object sender, EventArgs e)
        {
            UserID = null;

            try
            {
                using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connect.Open();

                    using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Authentication]", Connect))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32).Value = TB_Login.Text;
                        Command.Parameters.Add("@Password", SqlDbType.NVarChar, 32).Value = TB_Password.Text;

                        SqlDataReader Reader = Command.ExecuteReader(); if (Reader.HasRows)
                        {
                            while (Reader.Read())
                            {
                                UserID = (string)Reader.GetValue(0);
                                Admin = ((string)Reader.GetValue(1) == "Y") ? true : false;
                            }
                        }
                    }

                    Connect.Close();
                }

                if (UserID == null)
                    MessageBox.Show($"Неверные данные аутентификации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                { Hide(); new Main().ShowDialog(); Show(); }
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Регистрация
        /// </summary>
        private void Button_Register_Click(object sender, EventArgs e)
        { Hide(); new Registration().ShowDialog(); Show(); }
    }
}