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
        private readonly TaskRepository _taskRepository;
        private readonly TaskItem? task;

        public Ajout(TaskRepository _taskRepository, SystemDateTimeProvider _dateTimeProvider,TaskItem? task = null)
        {
            InitializeComponent();
            this.FormClosing += Ajout_FormClosing;
            this._dateTimeProvider = _dateTimeProvider;
            this._taskRepository = _taskRepository;
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

            OverCheckBox.Visible = (this.task == null ) ? false : true;
            if (this.task != null)
            {
                BTNAdd.Text = "Modifier";
                TitleTextBox.Text = this.task.Title;
                DueDateTextBox.Text = this.task.DueDate == null ? string.Empty : this.task.DueDate.ToString();
                OverCheckBox.Checked = this.task.Iscompleted;
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
                OperationResult? result = null;
                if( this.task != null)
                {
                    if(this.task.Iscompleted && !OverCheckBox.Checked)
                    {
                        var answer = MessageBox.Show($"La tâche n° {this.task.id} est terminée , souhaitez-vous la reprendre ?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                        if (answer == DialogResult.OK)
                        {
                            result = _taskRepository.UpdateTask(this.task.id, TitleTextBox.Text, dueDate, OverCheckBox.Checked, enumValue);
                        }
                        else
                        {
                            OverCheckBox.Checked = this.task.Iscompleted;
                        }
                    }
                    else
                    {
                        result = _taskRepository.UpdateTask(this.task.id, TitleTextBox.Text, dueDate, OverCheckBox.Checked, enumValue);
                    }
                        
                }
                else
                {
                    result = _taskRepository.AddTask(TitleTextBox.Text, dueDate, enumValue);
                }

                MessageBox.Show(result.message, (result.succes) ? (this.task == null) ? "Création réussie" : "Mise à jour réussie" : (this.task == null) ? "Erreur pendant la création" : "Erreur pendant la mise à jour");
                
                if (result.succes)
                {

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Erreur dans la sélection de la proprieté", "Erreur");
            }
            
            
        }

        private void Ajout_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
