using OneDayOneDev.Command;
using OneDayOneDev.Command.Interface;
using OnedayOneDev_Shared.Service;
using OneDayOneDev.Utils;
using OneDayOneDev_DayThirteen;
using OnedayOneDev_Shared;
using OnedayOneDev_Shared.Utils;
using OneDayOneDev.http;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Identification;

namespace OneDayOneDev
{
    public partial class MainForm : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly TaskRules _taskRules;
        private readonly CommandManager _commandManager = new();

        private bool IsConnected = false;

        private readonly ApiClient _api;

        private ICommand? cmd = null;
        public MainForm(TaskRules _taskRules, SystemDateTimeProvider _dateTimeProvider)
        {

            InitializeComponent();

            this._dateTimeProvider = _dateTimeProvider;
            this._taskRules = _taskRules;


            this.FormClosing += Form1_FormClosing;
            AuthHandler.SessionExpired += OnSessionExpired;

            _api = new ApiClient("https://localhost:7180/");
        }

        private void OnSessionExpired()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnSessionExpired));
                return;
            }

            MessageBox.Show("Votre session a expiré");

            IsConnected = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {



            this.Text = "OneDayOneDev";
            ListeTache.AutoGenerateColumns = true;
            RafraichirList();
            RafraichirBoutons();
            apiexpiretime.Start();
        }

        private async void RafraichirList()
        {
            if (!IsConnected)
            {
                ListeTache.DataSource = null;
                return;
            }
                
            try
            {
                ListeTache.DataSource = null;
                var Result = await _api.GetTaskList();

                if (Result.itemsLists == null || Result.itemsLists?.Count == 0)
                {
                    MessageBox.Show("Aucune tâches prévues");
                    return;
                }
                ListeTache.DataSource = Result.itemsLists;



                foreach (DataGridViewRow row in ListeTache.Rows)
                {
                    var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                    if (parsable)
                    {
                        var taskFromRow = await _api.GetTaskByIdAsync(id);

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
            using (var addForm = new Ajout(this._api, this._commandManager, this._dateTimeProvider))
            {
                addForm.Text = "Ajouter une tâche";
                addForm.ShowDialog(this);
                RafraichirList();
                RafraichirBoutons();
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

                    using (var addForm = new Ajout(this._api, this._commandManager, this._dateTimeProvider, task))
                    {
                        addForm.Text = "Modifier une tâche";
                        addForm.ShowDialog(this);


                        RafraichirList();
                        RafraichirBoutons();
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
                        cmd = new DeleteTaskCommand(this._api, id);

                        var deletetask = await _commandManager.Execute(cmd);

                        MessageBox.Show(deletetask.Message);
                    }

                }


            }


            RafraichirList();
            RafraichirBoutons();
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
                        cmd = new CompletTaskCommand(this._api, id);

                        var EndTask = await _commandManager.Execute(cmd);
                        MessageBox.Show(EndTask.Message);
                    }

                }


            }


            RafraichirList();
            RafraichirBoutons();
        }





        private async void ListeTache_DoubleClic(object sender, DataGridViewCellEventArgs e)
        {
            BTNModifier_Click(sender, e);
        }

        private async void BTNRedo_Click(object sender, EventArgs e)
        {
            _commandManager.RedoAsync();
            RafraichirList();
            RafraichirBoutons();
        }

        private async void BTNUndo_Click(object sender, EventArgs e)
        {
            _commandManager.Undo();
            RafraichirList();
            RafraichirBoutons();
        }

        private async void RafraichirBoutonUndoRedo()
        {
            BTNRedo.Enabled = _commandManager.CanRedo();
            BTNUndo.Enabled = _commandManager.CanUndo();

            BTNUndo.Text = (_commandManager.GetUndoNbr() > 0) ? $"Undo ({_commandManager.GetUndoNbr()})" : "Undo";
            BTNRedo.Text = (_commandManager.GetRedoNbr() > 0) ? $"Redo ({_commandManager.GetRedoNbr()})" : "Redo";

        }
        private async void RafraichirBoutons()
        {

            var user = IsConnected == false ? null : await _api.GetCurrentUser();
            
            BTNAjouter.Enabled = IsConnected;
            BTNDelete.Enabled = (user != null && user.Role != UserRole.ADMIN) ? false : IsConnected; ;
            BTNModifier.Enabled = (user != null && user.Role != UserRole.ADMIN) ? false : IsConnected; ;
            BTNOver.Enabled = IsConnected;

            if(!IsConnected)
            {
                ConnectionButton.Enabled = true;
            }
            RafraichirBoutonUndoRedo();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListeTache_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void ConnectionButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            {
                MessageBox.Show("Le nom d'utilisateur et le mot de passe doivent être renseignié");
                return;
            }
            try
            {
                var ok = await _api.LoginAsync(UserNameTextBox.Text, PasswordTextBox.Text);

                if (!ok)
                {
                    MessageBox.Show("Identifiants invalides");
                    return;
                }

                IsConnected = true;
                ConnectionButton.Enabled = false;
                UserNameTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;
                RafraichirBoutons();

                RafraichirList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }

        private async void apiexpiretime_Tick(object sender, EventArgs e)
        {
            var remaining = AuthSession.GetRemainingTime();

            if (remaining == TimeSpan.Zero)
            {
                labelSession.Text = "Session expirée";
                IsConnected = false;
                RafraichirBoutons();
                RafraichirList();
                return;
            }

            var user = IsConnected == false ? null : await _api.GetCurrentUser();


            labelSession.Text = (user == null) ? string.Empty : $"{user.UserName} {((user.Role == UserRole.ADMIN) ? user.Role : string.Empty)} => ";

            labelSession.Text += $"Session : {remaining:mm\\:ss}";

            if (remaining.TotalSeconds < 60)
                labelSession.ForeColor = Color.Red;
            else
                labelSession.ForeColor = Color.Black;
        }
    }
}
