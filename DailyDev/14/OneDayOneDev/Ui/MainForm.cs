using OneDayOneDev.Command;
using OneDayOneDev.Command.Interface;
using OnedayOneDev_Shared.Service;
using OneDayOneDev.Utils;
using OneDayOneDev_DayThirteen;
using OnedayOneDev_Shared;
using OnedayOneDev_Shared.Utils;
using OneDayOneDev.http;
using OnedayOneDev_Shared.ResultData;

namespace OneDayOneDev
{
    public partial class MainForm : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly TaskRules _taskRules;
        private readonly TaskService _taskService;
        private readonly CommandManager _commandManager = new();

        private readonly ApiClient _api;

        private ICommand? cmd = null;
        public MainForm(TaskService taskservice, TaskRules _taskRules, SystemDateTimeProvider _dateTimeProvider)
        {

            InitializeComponent();

            this._dateTimeProvider = _dateTimeProvider;
            this._taskService = taskservice;
            this._taskRules = _taskRules;


            this.FormClosing += Form1_FormClosing;

            _api = new ApiClient("https://localhost:7180/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            FileHandler temp = new FileHandler(_dateTimeProvider);
            
            var tasks = temp.LoadTaskData();
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    this._taskService.CreateNewTask(task);
                }

                temp.DeleteFile(temp.TaskDataPath);

            }
            this.Text = "OneDayOneDev";
            ListeTache.AutoGenerateColumns = true;
            RafraichirList();
            RafraichirBoutonUndoRedo();
        }

        private async void RafraichirList()
        {
            try
            {
                ListeTache.DataSource = null;
                var Result = await _api.GetTaskList();
                ListeTache.DataSource = Result.tasks;



                foreach (DataGridViewRow row in ListeTache.Rows)
                {
                    var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                    if (parsable)
                    {
                        var taskFromRow = this._taskService.GetTaskById(id);

                        if (taskFromRow != null)
                        {
                            if (this._taskRules.IsTaskLate(taskFromRow, _dateTimeProvider.Today))
                            {
                                row.DefaultCellStyle.BackColor = Color.LightCoral;
                            }

                            if (taskFromRow.Iscompleted)
                            {
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

        }



        private async void button2_Click(object sender, EventArgs e)
        {
            RafraichirList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private DataGridViewRow? GetSelectedRow()
        {

            DataGridViewRow? row = null;
            if (ListeTache.CurrentCell != null)
            {
                var actualPlace = ListeTache.CurrentCell.RowIndex;
                row = ListeTache.Rows[actualPlace];
            }
            else if (ListeTache.CurrentRow != null)
            {
                row = ListeTache.CurrentRow;
            }

            return row;
        }

        private async void BTNAjouter_Click(object sender, EventArgs e)
        {
            using (var addForm = new Ajout(this._api, this._commandManager, this._taskService, this._dateTimeProvider))
            {
                addForm.Text = "Ajouter une tâche";
                addForm.ShowDialog(this);
                RafraichirList();
                RafraichirBoutonUndoRedo();
            }

        }
        private async void BTNModifier_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectedRow();


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {
                    var task = await this._api.GetTaskByIdAsync(id);

                    using (var addForm = new Ajout(this._api, this._commandManager, this._taskService, this._dateTimeProvider, task))
                    {
                        addForm.Text = "Modifier une tâche";
                        addForm.ShowDialog(this);


                        RafraichirList();
                        RafraichirBoutonUndoRedo();
                    }
                }


            }


        }
        private async void BTNDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectedRow();


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    var result = MessageBox.Show($"souhaitez-vous supprimer la tâche n° {id}?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        cmd = new DeleteTaskCommand(this._api, this._taskService, id);

                        var deletetask = await _commandManager.Execute(cmd);

                        MessageBox.Show(deletetask.Message);
                    }

                }


            }


            RafraichirList();
            RafraichirBoutonUndoRedo();
        }


        private async void BTNOver_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = GetSelectedRow();

            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    var result = MessageBox.Show($"souhaitez-vous terminée la tâche n° {id}?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        cmd = new CompletTaskCommand(this._api, this._taskService, id);

                        var EndTask = await _commandManager.Execute(cmd);
                        MessageBox.Show(EndTask.Message);
                    }

                }


            }


            RafraichirList();
            RafraichirBoutonUndoRedo();
        }





        private async void ListeTache_DoubleClic(object sender, DataGridViewCellEventArgs e)
        {
            BTNModifier_Click(sender, e);
        }

        private async void BTNRedo_Click(object sender, EventArgs e)
        {
            _commandManager.RedoAsync();
            RafraichirList();
            RafraichirBoutonUndoRedo();
        }

        private async void BTNUndo_Click(object sender, EventArgs e)
        {
            _commandManager.Undo();
            RafraichirList();
            RafraichirBoutonUndoRedo();
        }

        private async  void RafraichirBoutonUndoRedo()
        {
            BTNRedo.Enabled = _commandManager.CanRedo();
            BTNUndo.Enabled = _commandManager.CanUndo();

            BTNUndo.Text = (_commandManager.GetUndoNbr() > 0) ? $"Undo ({_commandManager.GetUndoNbr()})" : "Undo";
            BTNRedo.Text = (_commandManager.GetRedoNbr() > 0) ? $"Redo ({_commandManager.GetRedoNbr()})" : "Redo";
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
