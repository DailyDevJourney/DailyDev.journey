namespace OneDayOneDev_DayTwelve
{
    public partial class Form1 : Form
    {
        private SystemDateTimeProvider _dateTimeProvider;
        public TaskService taskService;
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

        private void label1_Click(object sender, EventArgs e)
        {

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

            foreach (var item in taskService.GetTaskList())
            {
                dataGridView1.Rows.Add(item.id,item.Title,item.Iscompleted, item.CreatedAt?.ToString("dd/MM/yyyy"), item.DueDate?.ToString("dd/MM/yyyy"),item.OverDate?.ToString("dd/MM/yyyy"), item.Priority);
                
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Enum.TryParse(PriorityComboBox?.SelectedItem?.ToString(), out TaskPriority enumValue))
            {
                var dateCheck = DateTextBox.Text.Replace(" ",string.Empty);
                var result = taskService.CreateNewTask(TitleTextBox.Text, dateCheck == "//" ? string.Empty : DateTextBox.Text, enumValue);

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RafraichirList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si fermeture via la croix, Alt+F4, fermeture Windows, etc.
            if (e.CloseReason == CloseReason.UserClosing)
            {
                taskService.SaveData();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ResetField()
        {
            TitleTextBox.Text = string.Empty;
            DateTextBox.Text = string.Empty;
            PriorityComboBox.SelectedIndex = 0;
        }
    }
}
