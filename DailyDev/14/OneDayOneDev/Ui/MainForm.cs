using Microsoft.EntityFrameworkCore;
using OneDayOneDev.Command;
using OneDayOneDev.Command.Interface;
using OneDayOneDev.Repository;
using OneDayOneDev.Service;
using OneDayOneDev.Utils;
using OneDayOneDev_DayThirteen;
using System.Security.Cryptography.Xml;

namespace OneDayOneDev
{
    public partial class MainForm : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly TaskRules _taskRules;
        private readonly TaskService _taskService;
        private readonly CommandManager _commandManager = new();

        private ICommand? cmd = null;
        public MainForm(TaskService taskservice, TaskRules _taskRules, SystemDateTimeProvider _dateTimeProvider)
        {

            InitializeComponent();

            this._dateTimeProvider = _dateTimeProvider;
            this._taskService = taskservice;
            this._taskRules = _taskRules;


            this.FormClosing += Form1_FormClosing;
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

        private void RafraichirList()
        {


            ListeTache.DataSource = null;
            ListeTache.DataSource = this._taskService.GetTaskList();

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



        private void button2_Click(object sender, EventArgs e)
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

        private void BTNAjouter_Click(object sender, EventArgs e)
        {
            using (var addForm = new Ajout(this._commandManager, this._taskService, this._dateTimeProvider))
            {
                addForm.Text = "Ajouter une tâche";
                addForm.ShowDialog(this);
                RafraichirList();
                RafraichirBoutonUndoRedo();
            }

        }
        private void BTNModifier_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectedRow();


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    using (var addForm = new Ajout(this._commandManager, this._taskService, this._dateTimeProvider, this._taskService.GetTaskById(id)))
                    {
                        addForm.Text = "Modifier une tâche";
                        addForm.ShowDialog(this);


                        RafraichirList();
                        RafraichirBoutonUndoRedo();
                    }
                }


            }


        }
        private void BTNDelete_Click(object sender, EventArgs e)
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
                        cmd = new DeleteTaskCommand(this._taskService, id);

                        var deletetask = _commandManager.Execute(cmd);

                        MessageBox.Show(deletetask.Message);
                    }

                }


            }


            RafraichirList();
            RafraichirBoutonUndoRedo();
        }


        private void BTNOver_Click(object sender, EventArgs e)
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
                        cmd = new CompletTaskCommand(this._taskService, id);

                        var EndTask = _commandManager.Execute(cmd);
                        MessageBox.Show(EndTask.Message);
                    }

                }


            }


            RafraichirList();
            RafraichirBoutonUndoRedo();
        }





        private void ListeTache_DoubleClic(object sender, DataGridViewCellEventArgs e)
        {
            BTNModifier_Click(sender, e);
        }

        private void BTNRedo_Click(object sender, EventArgs e)
        {
            _commandManager.Redo();
            RafraichirList();
            RafraichirBoutonUndoRedo();
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            _commandManager.Undo();
            RafraichirList();
            RafraichirBoutonUndoRedo();
        }

        private void RafraichirBoutonUndoRedo()
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
