using System.Windows;

namespace New_Life.Stages
{
    public partial class EditTaskDialog : Window
    {
        public string UpdatedTask { get; private set; }

        public EditTaskDialog(string task)
        {
            InitializeComponent();
            TaskInput.Text = task;
            TaskInput.Focus();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            UpdatedTask = TaskInput.Text;
            DialogResult = true; // Устанавливаем результат диалога как успешный
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Устанавливаем результат диалога как отмененный
            Close();
        }
    }
}
