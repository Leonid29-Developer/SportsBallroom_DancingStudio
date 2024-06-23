using SportsBallroom_DancingStudio.Directories;
using SportsBallroom_DancingStudio.Forms.AssigningData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SportsBallroom_DancingStudio.Forms
{
    public partial class Main : Form
    {
        /// <summary> Конструктор </summary>
        public Main()
        {
            InitializeComponent();

            // Назначение возраста пользователя 
            if (!Authorization.Admin)
                using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get AgeGroup ID]", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@ID", SqlDbType.VarChar, 10).Value = Authorization.UserID;
                        Command.Parameters.Add("@DateNow", SqlDbType.Date).Value = DateTime.Now.Date;

                        SqlDataReader Reader = Command.ExecuteReader();
                        while (Reader.Read()) UserAgeGroup = (string)Reader["AgeGroup"];
                    }

                    Connection.Close();
                }
        }

        /// <summary> Возраст пользователя </summary>
        private string UserAgeGroup { get; set; } = null;

        /// <summary> Список занятий </summary>
        private List<Lesson> Lessons_List { get; set; } = new List<Lesson>();

        /// <summary> Обработка события. Первое отображение формы </summary>
        private void Main_Shown(object sender, EventArgs e)
            => OutData();

        /// <summary> Вывод расписания занятий </summary>
        private void OutData()
        {
            try
            {
                Table.Controls.Clear();

                // Вычисление размера ячеек
                Size Cell = new Size(120, 90);

                // Заполнение заголовков
                {
                    // Угловой столбец
                    {
                        Panel Head_X = new Panel()
                        {
                            Size = new Size((int)((double)Cell.Width * 1.25), Cell.Height),
                            Margin = new Padding(0),
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        {
                            PictureBox Line = new PictureBox()
                            {
                                Image = new Bitmap(Head_X.Size.Width, Head_X.Size.Height),
                                Dock = DockStyle.Fill
                            };
                            {
                                Graphics E = Graphics.FromImage(Line.Image);
                                E.DrawLine(new Pen(Color.Black, 2f), new Point(-1, -1), new Point(Head_X.Size.Width, Head_X.Size.Height));
                            }
                            Head_X.Controls.Add(Line);

                            Label HorizontalHeadings_Name = new Label()
                            {
                                TextAlign = ContentAlignment.MiddleCenter,
                                Text = "Дата",
                                Font = new Font("Times New Roman", 16),
                                Size = new Size(Line.Size.Width / 2 - 1, Line.Size.Height / 2 - 1),
                                Left = Line.Size.Width / 2
                            };
                            Line.Controls.Add(HorizontalHeadings_Name);

                            Label VerticalHeadings_Name = new Label()
                            {
                                TextAlign = ContentAlignment.MiddleCenter,
                                Text = "Время",
                                Font = new Font("Times New Roman", 16),
                                Size = new Size(Line.Size.Width / 2 - 1, Line.Size.Height / 2 - 1),
                                Top = Line.Size.Height / 2
                            };
                            Line.Controls.Add(VerticalHeadings_Name);
                        }
                        Table.Controls.Add(Head_X);
                    }

                    // Заголовки по горизонтали
                    {
                        for (int I1 = 0; I1 < 14; I1++)
                        {
                            DateTime Date = DateTime.Today.AddDays(I1);

                            Panel Head_Horizontal = new Panel()
                            {
                                Size = Cell,
                                Margin = new Padding(0),
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            {
                                Label Headings_Name = new Label()
                                {
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = $"{Date.Day}.{Date.Month}.{Date.Year}\n{Lesson.GetDay(Date.DayOfWeek)}",
                                    Font = new Font("Times New Roman", 14),
                                    Dock = DockStyle.Fill,
                                    BorderStyle = BorderStyle.FixedSingle
                                };
                                Head_Horizontal.Controls.Add(Headings_Name);
                            }
                            Table.Controls.Add(Head_Horizontal);
                        }
                    }
                }

                DateTime TodayTime = DateTime.Now.Date; // Текущая дата
                Lessons_List = Lessons_Calculation(TodayTime);

                // Основная часть
                for (int I1 = 1; I1 < 7; I1++)
                {
                    // Заголовки по вертикали
                    {
                        Panel Head_Vertical = new Panel()
                        {
                            Size = new Size((int)((double)Cell.Width * 1.25), Cell.Height),
                            Margin = new Padding(0),
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        {
                            Label Headings_Name = new Label()
                            {
                                TextAlign = ContentAlignment.MiddleCenter,
                                Text = Lesson.GetTime(I1),
                                Font = new Font("Times New Roman", 14),
                                Dock = DockStyle.Fill,
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            Head_Vertical.Controls.Add(Headings_Name);
                        }
                        Table.Controls.Add(Head_Vertical);
                    }

                    // Занятия
                    for (int I2 = 0; I2 < 14; I2++)
                    {
                        Panel Panel_Lesson = new Panel()
                        {
                            Size = Cell,
                            Margin = new Padding(0),
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        {
                            int IndexLesson = I2 * 6 + (I1 - 1);
                            Lesson UsingLesson = Lessons_List[IndexLesson];
                            if (UsingLesson.Date == TodayTime.AddDays(I2) & UsingLesson.NumberStartTime == I1)
                            {

                                if (UsingLesson.IDAgeGroup == null)
                                {
                                    CreateControl(Panel_Lesson, "None", IndexLesson);
                                }
                                else if (Authorization.Admin)
                                {
                                    CreateControl(Panel_Lesson, "Admin", IndexLesson);
                                }
                                else if (UsingLesson.IDAgeGroup == UserAgeGroup & UsingLesson.TypeDancing != TypesDancing.None)
                                {
                                    CreateControl(Panel_Lesson, "User", IndexLesson);
                                }
                                else
                                {
                                    CreateControl(Panel_Lesson, "None", IndexLesson);
                                }
                            }
                        }
                        Table.Controls.Add(Panel_Lesson);
                    }
                }

                Table.AutoScroll = false; Table.AutoScroll = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> Получение даты понедельника относительно введенной даты </summary>
        /// <param name="UsingDate">Дата</param>
        private DateTime SetDayMonday(DateTime UsingDate)
        {
            switch (UsingDate.DayOfWeek)
            {
                case DayOfWeek.Tuesday: return UsingDate.AddDays(-1);
                case DayOfWeek.Wednesday: return UsingDate.AddDays(-2);
                case DayOfWeek.Thursday: return UsingDate.AddDays(-3);
                case DayOfWeek.Friday: return UsingDate.AddDays(-4);
                case DayOfWeek.Saturday: return UsingDate.AddDays(-5);
                case DayOfWeek.Sunday: return UsingDate.AddDays(-6);
                default: return UsingDate;
            }
        }

        /// <summary> Получение списка занятий </summary>
        /// <param name="TodayTime">Текущая дата</param>
        private List<Lesson> Lessons_Calculation(DateTime TodayTime)
        {
            List<Lesson> Result = new List<Lesson>();

            // Вычисление порядкового номера недели от начала года
            int NumberWeek = 0;
            {
                DateTime TempTodayTime = TodayTime;
                DateTime StartTime = new DateTime(TempTodayTime.Year, 1, 1);

                TempTodayTime = SetDayMonday(TempTodayTime);
                StartTime = SetDayMonday(StartTime);

                while (StartTime.Date <= TempTodayTime.Date)
                {
                    NumberWeek++; StartTime = StartTime.AddDays(7);
                }
            }

            // Заполнение списка занятий от текущей даты
            for (int I1 = 0, AddWeek = 7; I1 < 14; I1++)
            {
                // Проверка на переход на следующую неделю
                if (TodayTime.AddDays(I1) == SetDayMonday(TodayTime.AddDays(AddWeek)))
                {
                    NumberWeek++; AddWeek += 7;
                }

                for (int I2 = 1; I2 < 7; I2++)
                {
                    // Проверка на существование занятия в базе данных
                    using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        Connection.Open();

                        using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get Lesson > TypeDancing]", Connection))
                        {
                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = TodayTime.AddDays(I1);
                            Command.Parameters.Add("@StartTime", SqlDbType.Time, 0).Value = Lesson.GetTime(I2).Split(' ')[0];

                            SqlDataReader Reader = Command.ExecuteReader();
                            while (Reader.Read())
                            {
                                Result.Add(new Lesson
                               (NumberWeek % 2 == 0 ? true : false, TodayTime.AddDays(I1).DayOfWeek, TodayTime.AddDays(I1), Convert.ToInt16((string)Reader["Number StartTime"])));

                                Result.Last().SetTypeDancing
                                    ((TypesDancing)Enum.GetValues(typeof(TypesDancing)).GetValue(Convert.ToInt16((string)Reader["Number TypeDancing"])));
                            }

                            if (Reader.HasRows) continue;
                        }

                        Connection.Close();
                    }

                    // Заполнение пустыми ячейками
                    {
                        Result.Add(new Lesson
                               (NumberWeek % 2 == 0 ? true : false, TodayTime.AddDays(I1).DayOfWeek, TodayTime.AddDays(I1), I2));

                        Result.Last().SetTypeDancing(TypesDancing.None); continue;
                    }
                }
            }

            return Result;
        }

        /// <summary> Заполнение элемента управления «Panel», согласно типу </summary>
        /// <param name="UsingPanel">Заполняемый элемент управления</param>
        /// <param name="Type">Тип заполнения</param>
        /// <param name="Index">Индекс занятия в списке</param>
        private void CreateControl(Panel UsingPanel, string Type, int Index)
        {
            Lesson UsingLesson = Lessons_List[Index];

            switch (Type)
            {
                case "None":
                    {
                        Label Label_Lesson = new Label()
                        {
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Times New Roman", 14),
                            Dock = DockStyle.Fill,
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        UsingPanel.Controls.Add(Label_Lesson);
                    }
                    break;

                case "Admin":
                    {
                        Label Label_Lesson_AgeGroup = new Label()
                        {
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Times New Roman", 12),
                            Dock = DockStyle.Top,
                            Size = new Size(UsingPanel.Width, UsingPanel.Height / 2),
                            Name = Index.ToString(),
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        {
                            Label_Lesson_AgeGroup.BackColor = Lesson.GetAgeGroup_Color(UsingLesson.IDAgeGroup);
                            Label_Lesson_AgeGroup.Text = $"{Lesson.GetAgeGroup_Name(UsingLesson.IDAgeGroup)}\n" +
                                $"от {Lesson.GetAgeGroup_Age(UsingLesson.IDAgeGroup)[0]} " +
                                $"до {Lesson.GetAgeGroup_Age(UsingLesson.IDAgeGroup)[1]} лет";
                        }
                        Label_Lesson_AgeGroup.Click += ControlSet_Click;
                        UsingPanel.Controls.Add(Label_Lesson_AgeGroup);

                        Label Label_Lesson_TypeDancing = new Label()
                        {
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Times New Roman", 12),
                            Dock = DockStyle.Bottom,
                            Size = new Size(UsingPanel.Width, UsingPanel.Height / 2),
                            Name = Index.ToString(),
                            BorderStyle = BorderStyle.FixedSingle
                        };
                        {
                            Label_Lesson_TypeDancing.BackColor = Lesson.GetAgeGroup_Color(UsingLesson.IDAgeGroup);
                            Label_Lesson_TypeDancing.Text = Lesson.GetTypeDancing(UsingLesson.TypeDancing);
                        }
                        Label_Lesson_TypeDancing.Click += ControlSet_Click;
                        UsingPanel.Controls.Add(Label_Lesson_TypeDancing);
                    }
                    break;

                case "User":
                    {
                        int IndexLesson = -1, // Индекс занятия
                            CountRecord = -1, // Количество бронирований на занятие
                            MaxCountRecord = -1; // Вместимость бронирований на занятие
                        bool ActiveReservations = false; // Наличие брони на занятие у пользователя

                        using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
                        {
                            // Получение индекса занятия
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get Lesson > Index]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@Date", SqlDbType.Date).Value = UsingLesson.Date;
                                    Command.Parameters.Add("@StartTime", SqlDbType.Time, 0).Value = Lesson.GetTime(UsingLesson.NumberStartTime).Split(' ')[0];

                                    SqlDataReader Reader = Command.ExecuteReader();
                                    while (Reader.Read()) IndexLesson = (int)Reader["Index"];
                                }

                                Connection.Close();
                            }

                            // Получение вместительности и количества бронирований на текущие занятие
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get CountRecords]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@IndexLesson", SqlDbType.Int).Value = IndexLesson;

                                    SqlDataReader Reader = Command.ExecuteReader(); while (Reader.Read())
                                    {
                                        CountRecord = (int)Reader["Count"]; MaxCountRecord = (int)Reader["MaxCount"];
                                    }
                                }

                                Connection.Close();
                            }

                            // Проверка на наличие брони на данное занятие у пользователя
                            if (CountRecord < MaxCountRecord)
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get RecordReservation]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@IndexLesson", SqlDbType.Int).Value = IndexLesson;
                                    Command.Parameters.Add("@UserID", SqlDbType.VarChar, 10).Value = Authorization.UserID;

                                    SqlDataReader Reader = Command.ExecuteReader(); ActiveReservations = Reader.HasRows;
                                }

                                Connection.Close();
                            }
                        }

                        // Вывод информации о занятие на интерфейс
                        if (CountRecord < MaxCountRecord)
                        {
                            string Names = ActiveReservations ? $"Y{Index}" : $"N{Index}";

                            PictureBox IconActive = new PictureBox()
                            {
                                BackColor = Lesson.GetAgeGroup_Color(UsingLesson.IDAgeGroup),
                                Size = new Size(UsingPanel.Height / 4, UsingPanel.Height / 4),
                                Name = Names
                            };
                            {
                                IconActive.Image = new Bitmap(IconActive.Size.Width, IconActive.Size.Height);
                                Graphics E = Graphics.FromImage(IconActive.Image);
                                SolidBrush EllipseColor = ActiveReservations ? new SolidBrush(Color.Red) : new SolidBrush(Color.White);
                                E.FillEllipse(EllipseColor, 2, 2, IconActive.Size.Width - 7, IconActive.Size.Height - 7);
                                E.DrawEllipse(new Pen(Color.Black, 2), 2, 2, IconActive.Size.Width - 7, IconActive.Size.Height - 7);
                            }
                            IconActive.Click += ControlSet_Click;
                            UsingPanel.Controls.Add(IconActive);

                            Label Label_Active = new Label()
                            {
                                BackColor = Lesson.GetAgeGroup_Color(UsingLesson.IDAgeGroup),
                                TextAlign = ContentAlignment.MiddleLeft,
                                Font = new Font("Times New Roman", 11),
                                Size = new Size(UsingPanel.Width - IconActive.Width, IconActive.Height),
                                Name = Names,
                                Text = "Нет брони",
                                Left = IconActive.Width
                            };
                            if (ActiveReservations) Label_Active.Text = "Бронь";
                            Label_Active.Click += ControlSet_Click;
                            UsingPanel.Controls.Add(Label_Active);

                            Label Label_Lesson_TypeDancing = new Label()
                            {
                                BackColor = Lesson.GetAgeGroup_Color(UsingLesson.IDAgeGroup),
                                TextAlign = ContentAlignment.MiddleCenter,
                                Font = new Font("Times New Roman", 13, FontStyle.Underline),
                                Size = new Size(UsingPanel.Width, UsingPanel.Height / 2),
                                Text = Lesson.GetTypeDancing(UsingLesson.TypeDancing),
                                Name = Names,
                                Top = UsingPanel.Height / 4
                            };
                            Label_Lesson_TypeDancing.Click += ControlSet_Click;
                            UsingPanel.Controls.Add(Label_Lesson_TypeDancing);

                            Label Label_CountReservations = new Label()
                            {
                                BackColor = Lesson.GetAgeGroup_Color(UsingLesson.IDAgeGroup),
                                TextAlign = ContentAlignment.MiddleCenter,
                                Font = new Font("Times New Roman", 11),
                                Dock = DockStyle.Bottom,
                                Size = new Size(UsingPanel.Width, UsingPanel.Height / 4),
                                Text = $"Мест {CountRecord} / {MaxCountRecord}",
                                Name = Names
                            };
                            Label_CountReservations.Click += ControlSet_Click;
                            UsingPanel.Controls.Add(Label_CountReservations);
                        }
                    }
                    break;
            }
        }

        /// <summary> Получение индекса занятия в базе данных </summary>
        /// <param name="Date">Дата проведения занятия</param>
        /// <param name="StartTime">Временной промежуток проведения занятия</param>
        private int GetIndexLesson(DateTime Date, string StartTime)
        {
            int IndexLesson = -1; // Индекс занятия

            using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
            {
                // Получение индекса занятия 
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get Lesson > Index]", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = Date;
                        Command.Parameters.Add("@StartTime", SqlDbType.Time, 0).Value = StartTime;

                        SqlDataReader Reader = Command.ExecuteReader();
                        while (Reader.Read()) IndexLesson = (int)Reader["Index"];
                    }

                    Connection.Close();
                }
            }

            return IndexLesson;
        }

        /// <summary>
        /// Обработка события «Panel» </br> 
        /// Нажатие на элемент управления
        /// </summary>
        private void ControlSet_Click(object sender, EventArgs e)
        {
            Control UsingControl = (Control)sender;

            try
            {
                if (Authorization.Admin) // Пользователь администратор
                {
                    Lesson UsingLesson = Lessons_List[Convert.ToInt16(UsingControl.Name)];

                    FormAdmin.Type = UsingLesson.TypeDancing; new FormAdmin().ShowDialog();

                    using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        if (UsingLesson.TypeDancing == TypesDancing.None) // Добавление в новое занятие
                        {
                            if (FormAdmin.Type != TypesDancing.None)
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Add Lesson]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@Evennumbered_week", SqlDbType.Char, 1).Value = UsingLesson.EvennumberedWeek ? "Y" : "N";
                                    Command.Parameters.Add("@DayWeek", SqlDbType.NVarChar, 12).Value = Lesson.GetDay(UsingLesson.Date.DayOfWeek);
                                    Command.Parameters.Add("@StartTime", SqlDbType.Time, 0).Value = Lesson.GetTime(UsingLesson.NumberStartTime).Split(' ')[0];
                                    Command.Parameters.Add("@Date", SqlDbType.Date).Value = UsingLesson.Date;
                                    Command.Parameters.Add("@Dance", SqlDbType.NVarChar, 12).Value = Lesson.GetTypeDancing(FormAdmin.Type); ;

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                        }
                        else
                        {
                            int IndexLesson = // Индекс занятия
                                            GetIndexLesson(UsingLesson.Date, Lesson.GetTime(UsingLesson.NumberStartTime).Split(' ')[0]);

                            if (FormAdmin.Type == TypesDancing.None) // Удаление в существующем занятие
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Remove Lesson]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@IndexLesson", SqlDbType.Int).Value = IndexLesson;

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                            else // Обновление в существующем занятие
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Update Lesson > TypeDancing]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@IndexLesson", SqlDbType.Int).Value = IndexLesson;
                                    Command.Parameters.Add("@Dance", SqlDbType.NVarChar, 12).Value = Lesson.GetTypeDancing(FormAdmin.Type);

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                        }
                    }
                }
                else // Обычный пользователь
                {
                    string MessageHead = UsingControl.Name[0] == 'Y' ?
                        "отмену бронирования" : "бронирование";
                    string MessageText = UsingControl.Name[0] == 'Y' ?
                        "отменить бронирование занятия" : "Уверены, что хотите забронировать занятие";

                    if (DialogResult.Yes ==
                        MessageBox.Show($"Уверены, что хотите {MessageText}?", $"Подтвердите {MessageHead}", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        Lesson UsingLesson = Lessons_List[Convert.ToInt16(UsingControl.Name.Remove(0, 1))];

                        using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
                        {
                            int IndexLesson = // Индекс занятия
                                GetIndexLesson(UsingLesson.Date, Lesson.GetTime(UsingLesson.NumberStartTime).Split(' ')[0]);

                            if (UsingControl.Name[0] == 'Y') // Отмена бронирования
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Remove RecordReservation]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@IndexLesson", SqlDbType.Int).Value = IndexLesson;
                                    Command.Parameters.Add("@UserID", SqlDbType.VarChar, 10).Value = Authorization.UserID;

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                            else // Бронирование
                            {
                                Connection.Open();

                                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Add RecordReservation]", Connection))
                                {
                                    Command.CommandType = CommandType.StoredProcedure;

                                    Command.Parameters.Add("@IndexLesson", SqlDbType.Int).Value = IndexLesson;
                                    Command.Parameters.Add("@UserID", SqlDbType.VarChar, 10).Value = Authorization.UserID;

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            OutData();
        }
    }
}