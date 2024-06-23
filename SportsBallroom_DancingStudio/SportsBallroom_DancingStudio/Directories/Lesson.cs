using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace SportsBallroom_DancingStudio.Directories
{
    /// <summary> Информация о занятие </summary>
    public class Lesson
    {
        /// <summary> Четность недели </summary>
        public bool EvennumberedWeek { get; private set; } = false;

        /// <summary> Номер дня недели </summary>
        public DayOfWeek DayWeek { get; private set; } = DayOfWeek.Monday;

        /// <summary> Дата занятия </summary>
        public DateTime Date { get; private set; } = DateTime.Now;

        /// <summary> Номер временного промежутка проведения занятия </summary>
        public int NumberStartTime { get; private set; } = 1;

        /// <summary> Индификатор возрастной категории занятия </summary>
        public string IDAgeGroup { get; private set; } = null;

        /// <summary> Тип танца, обучаемый на занятие </summary>
        public TypesDancing TypeDancing { get; private set; } = TypesDancing.None;


        /// <summary> Конструктор </summary>
        /// <param name="SetEvennumberedWeek">Четность недели</param>
        /// <param name="SetNumberDay">Номер дня недели</param>
        /// <param name="SetDate">Дата занятия</param>
        /// <param name="SetNumberStartTime">Номер временного промежутка проведения занятия</param>
        public Lesson(bool SetEvennumberedWeek, DayOfWeek SetDayWeek, DateTime SetDate, int SetNumberStartTime)
        {
            EvennumberedWeek = SetEvennumberedWeek; DayWeek = SetDayWeek;
            Date = SetDate; NumberStartTime = SetNumberStartTime;

            using (SqlConnection Connection = new SqlConnection("Data Source='';Integrated Security=True"))
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("[SportsBallroom_DancingStudio].[dbo].[Get AgeGroup From ClassSchedule]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@Evennumbered_week", SqlDbType.Char, 1).Value = EvennumberedWeek ? 'Y' : 'N';
                    Command.Parameters.Add("@DayWeek", SqlDbType.NVarChar, 12).Value = GetDay(SetDayWeek);
                    Command.Parameters.Add("@StartTime", SqlDbType.Time, 0).Value = GetTime(NumberStartTime).Split(' ')[0];

                    SqlDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read()) IDAgeGroup = Reader["AgeGroup"].ToString();
                }

                Connection.Close();
            }
        }

        /// <summary> Назначение типа танца, обучаемого на занятие </summary>
        public void SetTypeDancing(TypesDancing SetTypeDancing)
            => TypeDancing = SetTypeDancing;

        // Получение информации 

        /// <summary> Получение наименования дня недели на русском языке </summary>
        /// <param name="DayENG">Наименование дня недели на английском языке</param>
        public static string GetDay(DayOfWeek DayENG)
        {
            switch (DayENG)
            {
                case DayOfWeek.Monday: return "Понедельник";
                case DayOfWeek.Tuesday: return "Вторник";
                case DayOfWeek.Wednesday: return "Среда";
                case DayOfWeek.Thursday: return "Четверг";
                case DayOfWeek.Friday: return "Пятница";
                case DayOfWeek.Saturday: return "Суббота";
                case DayOfWeek.Sunday: return "Воскресенье";
                default: return "Ошибка";
            }
        }

        /// <summary> Получение временного промежутка проведения занятия </summary>
        /// <param name="Number">Номер временного промежутка проведения занятия</param>
        public static string GetTime(int Number)
        {
            switch (Number)
            {
                case 1: return "8:00 - 9:30";
                case 2: return "10:00 - 11:30";
                case 3: return "12:00 - 13:30";
                case 4: return "14:30 - 16:00";
                case 5: return "17:30 - 19:00";
                case 6: return "19:30 - 21:00";
                default: return "Ошибка";
            }
        }

        /// <summary> Получение наименования возрастной группы </summary>
        /// <param name="ID">Индификатор возрастной категории занятия</param>
        public static string GetAgeGroup_Name(string ID)
        {
            switch (ID[0])
            {
                case 'A': return "Дети";
                case 'B': return "Юниоры";
                case 'C': return "Молодежь";
                case 'D': return "Взрослые";
                case 'E': return "Сеньоры";
                default: return "Ошибка";
            }
        }

        /// <summary> Получение наименования возрастной группы </summary>
        /// <param name="ID">Индификатор возрастной категории занятия</param>
        public static int[] GetAgeGroup_Age(string ID)
        {
            int[] Result = new int[2];
            Result[0] = 0; Result[1] = 0;

            switch (ID)
            {
                case "A1":
                    { Result[1] = 7; }
                    break;
                case "A2":
                    { Result[0] = 8; Result[1] = 9; }
                    break;
                case "A3":
                    { Result[0] = 10; Result[1] = 11; }
                    break;
                case "B1":
                    { Result[0] = 12; Result[1] = 13; }
                    break;
                case "B2":
                    { Result[0] = 14; Result[1] = 15; }
                    break;
                case "C1":
                    { Result[0] = 16; Result[1] = 18; }
                    break;
                case "C2":
                    { Result[0] = 19; Result[1] = 21; }
                    break;
                case "D0":
                    { Result[0] = 22; Result[1] = 35; }
                    break;
                case "E0":
                    { Result[0] = 36; Result[1] = 46; }
                    break;
            }

            return Result;
        }

        /// <summary> Получение цвета по возрастной категории </summary>
        /// <param name="ID">Индификатор возрастной категории занятия</param>
        public static Color GetAgeGroup_Color(string ID)
        {
            switch (ID)
            {
                case "A1": return ColorTranslator.FromHtml("#FCE4D6");
                case "A2": return ColorTranslator.FromHtml("#F8CBAD");
                case "A3": return ColorTranslator.FromHtml("#F4B084");
                case "B1": return ColorTranslator.FromHtml("#FFE699");
                case "B2": return ColorTranslator.FromHtml("#FFD966");
                case "C1": return ColorTranslator.FromHtml("#C6E0B4");
                case "C2": return ColorTranslator.FromHtml("#A9D08E");
                case "D0": return ColorTranslator.FromHtml("#8EA9DB");
                case "E0": return ColorTranslator.FromHtml("#C9C9C9");
                default: return ColorTranslator.FromHtml("#FFFFFF");
            }
        }

        /// <summary> Получение типа танца на русском языке </summary>
        /// <param name="TypeDancingENG">Наименование типа танца на английском языке</param>
        public static string GetTypeDancing(TypesDancing TypeDancingENG)
        {
            switch (TypeDancingENG)
            {
                case TypesDancing.None: return "Не назначено";
                case TypesDancing.Tango: return "Танго";
                case TypesDancing.Rumba: return "Румба";
                case TypesDancing.Samba: return "Самба";
                case TypesDancing.Jive: return "Джайв";
                case TypesDancing.Posadobl: return "Посадобль";
                case TypesDancing.Foxtrot: return "Фокстрот";
                case TypesDancing.Waltz: return "Вальс";
                case TypesDancing.Cha_Cha_Cha: return "Ча-Ча-Ча";
                default: return "Ошибка";
            }
        }
    }
}