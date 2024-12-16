using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;


namespace New_Life.Stages
{
    public partial class MainPage : Page
    {
        private int mainTaskCount = 0; // Счетчик главных задач

        public MainPage()
        {
            InitializeComponent();
            HighlightDay("Monday");
        }

        // Подсветка текущего дня
        private void HighlightDay(string day)
        {
            DayTitle.Text = $"Задачи на {GetDayName(day)}";
            DailyTasksList.Items.Clear();
        }

        // Добавление задачи
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                DailyTasksList.Items.Add(TaskInput.Text);
                TaskInput.Clear();
            }
        }

        // Редактирование задачи по двойному клику
        private void EditTask(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DailyTasksList.SelectedItem is string task)
            {
                var editDialog = new EditTaskDialog(task);
                if (editDialog.ShowDialog() == true) // Ожидаем завершения диалога
                {
                    string updatedTask = editDialog.UpdatedTask;
                    if (!string.IsNullOrWhiteSpace(updatedTask))
                    {
                        int index = DailyTasksList.SelectedIndex;
                        DailyTasksList.Items[index] = updatedTask;
                    }
                }
            }
        }

        // Перенос задачи в "главные задачи"
        private void MarkAsMainTask(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string task = clickedButton.Tag as string;

            if (!string.IsNullOrWhiteSpace(task))
            {
                mainTaskCount++;
                switch (mainTaskCount)
                {
                    case 1:
                        MainTask1.Text = task;
                        break;
                    case 2:
                        MainTask2.Text = task;
                        break;
                    case 3:
                        MainTask3.Text = task;
                        mainTaskCount = 0; // Сброс после заполнения 3 главных задач
                        break;
                }
                DailyTasksList.Items.Remove(task);
            }
        }

        // Получение имени дня недели
        private string GetDayName(string day)
        {
            switch (day)
            {
                case "Monday": return "Понедельник";
                case "Tuesday": return "Вторник";
                case "Wednesday": return "Среда";
                case "Thursday": return "Четверг";
                case "Friday": return "Пятница";
                case "Saturday": return "Суббота";
                case "Sunday": return "Воскресенье";
                default: return "Неизвестный день";
            }
        }
        private void TaskInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TaskInput.Text == "Введите задачу...")
            {
                TaskInput.Text = "";
                TaskInput.Foreground = Brushes.Black;
            }
        }
        private void TaskInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                TaskInput.Text = "Введите задачу...";
                TaskInput.Foreground = Brushes.Gray;
            }
        }

        // Заглушка для обработчика TaskMenuOpening
        private void TaskMenuOpening(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Контекстное меню открывается...");
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string dayTag = clickedButton.Tag.ToString();
            HighlightDay(dayTag);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Профиль пользователя", "Профиль");
        }
    }
}

