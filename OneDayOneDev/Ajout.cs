using OneDayOneDev;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneDayOneDev_DayThirteen
{
    public partial class Ajout : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly TaskService taskService;
        private readonly TaskItem? task;

        public Ajout(TaskService taskService, SystemDateTimeProvider _dateTimeProvider,TaskItem? task = null)
        {
            InitializeComponent();
            this.FormClosing += Ajout_FormClosing;
            this._dateTimeProvider = _dateTimeProvider;
            this.taskService = taskService;
            this.task = task;
        }
        

        private void Ajout_Load(object sender, EventArgs e)
        {
            

            foreach (var menuInfoValues in Enum.GetNames<TaskPriority>())
            {
                ProprietyComboBox.Items.Add(menuInfoValues);
            }

            if (ProprietyComboBox.Items.Count > 0)
            {
                ProprietyComboBox.SelectedIndex = 0;
            }

            if (this.task != null)
            {
                BTNAdd.Text = "Modifier";
                TitleTextBox.Text = this.task.Title;
                DueDateTextBox.Text = this.task.DueDate == null ? string.Empty : this.task.DueDate.ToString();
                if (Enum.TryParse(ProprietyComboBox?.SelectedItem?.ToString(), out TaskPriority enumValue))
                {
                    ProprietyComboBox.SelectedIndex = enumValue.GetNumber();
                }
            }
        }

        private void BTNCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNAdd_Click(object sender, EventArgs e)
        {
            if (Enum.TryParse(ProprietyComboBox?.SelectedItem?.ToString(), out TaskPriority enumValue))
            {
                string? dueDate = DueDateTextBox.MaskCompleted ? DueDateTextBox.Text : null;
                var result = taskService.CreateNewTask(TitleTextBox.Text, dueDate, enumValue);
                MessageBox.Show(result.message,(result.succes) ? "Création réussi" : "Erreur pendant la création");
                if (result.succes)
                {
                    
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Erreur dans la sélection de la proprieté","Erreur");
            }
        }

        private void Ajout_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
