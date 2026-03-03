using OneDayOneDev.Command;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared;
using OneDayOneDev.Utils;
using OnedayOneDev_Shared.Utils;
using OnedayOneDev_Shared.Utils.Interface;
using OneDayOneDev.http;

namespace OneDayOneDev_DayThirteen
{
    public partial class Ajout : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly OnedayOneDev_Shared.Service.Interface.ITaskService _taskService;
        private readonly CommandManager _commandManager;
        private readonly ApiClient _api;
        private readonly TaskItem? task;

        public Ajout(ApiClient api,CommandManager _cmdManager , SystemDateTimeProvider _dateTimeProvider,TaskItem? task = null)
        {
            InitializeComponent();
            this.FormClosing += Ajout_FormClosing;
            this._dateTimeProvider = _dateTimeProvider;
            this.task = task;
            this._commandManager = _cmdManager;
            this._api = api;
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

        private async void BTNAdd_Click(object sender, EventArgs e)
        {
            if (Enum.TryParse(ProprietyComboBox?.SelectedItem?.ToString(), out TaskPriority enumValue))
            {
                string? dueDate = DueDateTextBox.MaskCompleted ? DueDateTextBox.Text : null;
                Result<TaskItem> result = null;
                if( this.task != null)
                {
                    if(this.task.Iscompleted && !OverCheckBox.Checked)
                    {
                        var answer = MessageBox.Show($"La tâche n° {this.task.id} est terminée , souhaitez-vous la reprendre ?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                        if (answer == DialogResult.OK)
                        {

                            var cmd = new UpdateCommand(this._api,this._taskService, this.task.id, TitleTextBox.Text, dueDate, OverCheckBox.Checked, enumValue);

                            result = await this._commandManager.Execute(cmd);
                        }
                        else
                        {
                            OverCheckBox.Checked = this.task.Iscompleted;
                        }
                    }
                    else
                    {
                        var cmd = new UpdateCommand(this._api,this._taskService, this.task.id, TitleTextBox.Text, dueDate, OverCheckBox.Checked, enumValue);
                        var test = await this._commandManager.Execute(cmd);
                        result = test;
                    }
                        
                }
                else
                {
                    
                    var taskTemp = new TaskItem(TitleTextBox.Text,_dateTimeProvider.Today, IDateTimeProvider.ParseDate(dueDate), OverCheckBox.Checked,null, enumValue);
                    var cmd = new AddTaskCommand(this._api, taskTemp);
                    
                    result = await this._commandManager.Execute(cmd);
                }

                MessageBox.Show(result.Message, (result.Success) ? (this.task == null) ? "Création réussie" : "Mise à jour réussie" : (this.task == null) ? "Erreur pendant la création" : "Erreur pendant la mise à jour");
                
                if (result.Success)
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
