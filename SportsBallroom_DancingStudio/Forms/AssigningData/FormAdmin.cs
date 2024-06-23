using SportsBallroom_DancingStudio.Directories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SportsBallroom_DancingStudio.Forms.AssigningData
{
    public partial class FormAdmin : Form
    {
        /// <summary> Конструктор </summary>
        public FormAdmin() => InitializeComponent();

        /// <summary> Обработка события. Первое отображение формы </summary>
        private void FormAdmin_Shown(object sender, EventArgs e)
            => OutData();

        /// <summary> Тип танца </summary>
        public static TypesDancing Type { get; set; }

        /// <summary> Обновление данных </summary>
        private void OutData()
        {
            ActiveOrDeactive(SetTango1); ActiveOrDeactive(SetPosadobl5);
            ActiveOrDeactive(SetRumba2); ActiveOrDeactive(SetFoxtrot6);
            ActiveOrDeactive(SetSamba3); ActiveOrDeactive(SetWaltz7);
            ActiveOrDeactive(SetJive4); ActiveOrDeactive(SetCha_Cha_Cha8);
        }

        /// <summary> Изменение параметров элемента управления </summary>
        /// <param name="UsingControlSet">Элемент управления назначение типа танца</param>
        private void ActiveOrDeactive(Control UsingControlSet)
        {
            if (UsingControlSet.Name.Remove(UsingControlSet.Name.Length - 1, 1).Remove(0, 3) == Type.ToString())
            {
                UsingControlSet.Size = new Size(294, 54);
                UsingControlSet.Location = new Point(3, 3);
            }
            else
            {
                UsingControlSet.Size = new Size(300, 60);
                UsingControlSet.Location = new Point(0, 0);
            }
        }

        /// <summary> Активация типа танца </summary>
        private void ActivatingType_Click(object sender, EventArgs e)
        {
            Control UsingControl = (Control)sender;

            TypesDancing SetType = (TypesDancing)Enum.GetValues(typeof(TypesDancing)).
                GetValue(Convert.ToInt16($"{UsingControl.Name[UsingControl.Name.Length - 1]}"));

            if (Type == SetType) Type = TypesDancing.None;
            else Type = SetType; OutData();
        }

        /// <summary> Применение изменений и закрытие формы </summary>
        private void Button_Enter_Click(object sender, EventArgs e) => Close();
    }
}