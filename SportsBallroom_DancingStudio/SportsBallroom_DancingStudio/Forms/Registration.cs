using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SportsBallroom_DancingStudio.Forms
{
    public partial class Registration : Form
    {
        /// <summary> Конструктор </summary>
        public Registration() => InitializeComponent();

        /// <summary>  Показан или спрятан пароль </summary>
        private bool HideOrShow = true;

        /// <summary> Показать или спрятать ввод пароля </summary>
        private void Password_HideOrShow_Click(object sender, EventArgs e)
        {
            if (HideOrShow)
            {
                HideOrShow = false;
                TB_Password.PasswordChar = TB_Login.PasswordChar;
                TB_PasswordAgain.PasswordChar = TB_Login.PasswordChar;
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeOpen;
            }
            else
            {
                HideOrShow = true;
                TB_Password.PasswordChar = '✵';
                TB_PasswordAgain.PasswordChar = '✵';
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeClose;
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Регистрация
        /// </summary>
        private void Button_Register_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrorMessage = "";
                bool T = true;

                // Проверка доступности Логина
                using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connect.Open();

                    using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[CheckingAvailabilityLogin]", Connect))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32).Value = TB_Login.Text;

                        SqlDataReader Reader = Command.ExecuteReader(); if (Reader.HasRows)
                        { T = false; ErrorMessage += "Логин не доступен\n"; }
                    }

                    Connect.Close();
                }

                // Проверка паролей на совпадение
                if (TB_Password.Text != TB_PasswordAgain.Text)
                { T = false; ErrorMessage += "Пароли должны совпадать\n"; }

                // Проверка на пустые поля
                if (TB_Login.Text == "" | TB_Password.Text == "" | TB_PasswordAgain.Text == "" |
                    TB_Surname.Text == "" | TB_Name.Text == "" | TB_MiddleName.Text == "" | TB_Datebirth.Text.Contains(" "))
                { T = false; ErrorMessage += "Все поля должны быть заполнены"; }

                if (T)
                {
                    using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        Connect.Open();

                        using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Registration]", Connect))
                        {
                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32).Value = TB_Login.Text;
                            Command.Parameters.Add("@Password", SqlDbType.NVarChar, 32).Value = TB_Password.Text;
                            Command.Parameters.Add("@Surname", SqlDbType.NVarChar, 30).Value = TB_Surname.Text;
                            Command.Parameters.Add("@Name", SqlDbType.NVarChar, 30).Value = TB_Name.Text;
                            Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 30).Value = TB_MiddleName.Text;
                            Command.Parameters.Add("@Datebirth", SqlDbType.Date, 30).Value = TB_Datebirth.Text;

                            Command.ExecuteNonQuery();
                        }
                        Connect.Close();
                    }

                    MessageBox.Show("Успешная регистрация", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show($"Регистрация завершилась с ошибкой:\n{ErrorMessage}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}