namespace OneDayOneDev_DayTwelve
{
    public partial class Form1 : Form
    {
        private SystemDateTimeProvider _dateTimeProvider;
        private TaskService taskService;
        public Form1()
        {
            InitializeComponent();

            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dateTimeProvider = new SystemDateTimeProvider();
            taskService = new TaskService(_dateTimeProvider);

            foreach (var menuInfoValues in Enum.GetNames<TaskPriority>())
            {
                PriorityComboBox.Items.Add(menuInfoValues);
            }

            if (PriorityComboBox.Items.Count > 0)
            {
                PriorityComboBox.SelectedIndex = 0;
            }

            RafraichirList();

        }

        private void RafraichirList()
        {
            if (taskService.GetTaskList().Count > 0 && dataGridView1.ColumnCount == 0)
            {
                dataGridView1.Columns.Add("ID", "identifiant");
                dataGridView1.Columns.Add("Title", "Titre");
                dataGridView1.Columns.Add("IsCompletd", "Status");
                dataGridView1.Columns.Add("CreatedAt", "Creer le");
                dataGridView1.Columns.Add("DueDate", "Echéance le");
                dataGridView1.Columns.Add("OverDate", "fini le");
                dataGridView1.Columns.Add("Priority", "Priorité");
            }

            dataGridView1.Rows.Clear();
            var taskList = taskService.GetTaskList();
            foreach (var item in taskList)
            {
                dataGridView1.Rows.Add(item.id,item.Title,item.Iscompleted, item.CreatedAt?.ToString("dd/MM/yyyy"), item.DueDate?.ToString("dd/MM/yyyy"),item.OverDate?.ToString("dd/MM/yyyy"), item.Priority);
                
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Enum.TryParse(PriorityComboBox?.SelectedItem?.ToString(), out TaskPriority enumValue))
            {
                string? dueDate = DateTextBox.MaskCompleted ? DateTextBox.Text : null;
                var result = taskService.CreateNewTask(TitleTextBox.Text, dueDate, enumValue);

                if (result.succes)
                {
                    RafraichirList();
                    ResetField();
                    MessageBox.Show("Tâche créer");

                }
                else
                {
                    MessageBox.Show(result.message);
                }
            }
            else
            {
                MessageBox.Show("Erreur dans la sélection de la proprieté");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            RafraichirList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            taskService.SaveData();
        }

        private void ResetField()
        {
            TitleTextBox.Text = string.Empty;
            DateTextBox.Text = string.Empty;
            PriorityComboBox.SelectedIndex = 0;
        }
    }
}
