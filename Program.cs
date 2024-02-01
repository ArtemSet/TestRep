using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class MyForm : Form
{
    public List<string> DataBaseTasks { get; set; }
    public int CountTask { get; set; }
    public Label CountLabel { get; set; }

    public MyForm()
    {
        InitializeComponent();

        this.Width = 500;
        this.Height = 500;
    }

    private void InitializeComponent()
    {
        CountTask = 0;
        DataBaseTasks = new List<string>();

        Button ViewTasks = new Button();
        Button AddTask = new Button();
        CountLabel = new Label();

        AddTask.Text = "Добавить Задачу";
        AddTask.Click += Add_Click;
        AddTask.Location = new Point(190, 100);
        AddTask.Size = new Size(100, 40);

        ViewTasks.Text = "Посмотреть Задачи";
        ViewTasks.Click += ViewTasks_Click;
        ViewTasks.Location = new Point(190, 170);
        ViewTasks.Size = new Size(100, 40);

        CountLabel.Text = $"Количество Задач: {CountTask}";
        CountLabel.Size = new Size(150, 40);
        CountLabel.Location = new Point(10, 10);

        Controls.Add(AddTask);
        Controls.Add(ViewTasks);
        Controls.Add(CountLabel);
    }

    private void ViewTasks_Click(object sender, EventArgs e)
    {
        ViewTasksForm tasksForm = new ViewTasksForm(this);
        tasksForm.Show();
    }

    private void Add_Click(object sender, EventArgs e)
    {
        AddForm Add = new AddForm(this);
        Add.Show();
    }
}

public partial class AddForm : Form
{
    private MyForm parentForm;

    public TextBox NameTask { get; set; }

    public AddForm(MyForm parent)
    {
        parentForm = parent;
        InitializeComponent();

        this.Width = 400;
        this.Height = 400;
    }

    private void InitializeComponent()
    {
        Label label = new Label();
        NameTask = new TextBox();
        Button SaveButton = new Button();

        NameTask.Text = "Печатай";
        NameTask.Location = new Point(120, 147);

        label.Text = "Имя задачи:";
        label.Location = new Point(20, 150);

        SaveButton.Text = "Сохранение";
        SaveButton.Click += Save_Click;
        SaveButton.Location = new Point(10, 330);

        Controls.Add(label);
        Controls.Add(NameTask);
        Controls.Add(SaveButton);
    }

    private void Save_Click(object sender, EventArgs e)
    {
        parentForm.DataBaseTasks.Add(NameTask.Text);
        parentForm.CountTask += 1;

        parentForm.CountLabel.Text = $"Количество Задач: {parentForm.CountTask}";

        this.Close();
    }
}

public partial class ViewTasksForm : Form
{
    private MyForm parentForm;

    public ViewTasksForm(MyForm parent)
    {
        parentForm = parent;
        InitializeComponent();

        this.Width = 400;
        this.Height = 400;
    }

    private void InitializeComponent()
    {
        string[] tasksArray = new string[parentForm.DataBaseTasks.Count];

        for (int i = 0; i < parentForm.DataBaseTasks.Count; i++)
        {
            tasksArray[i] = parentForm.DataBaseTasks[i];
        }

        for (int i = 0; i < tasksArray.Length; i++)
        {
            Label writeTaskLabel = new Label();
            writeTaskLabel.Text = i + 1 + ": " + tasksArray[i];
            writeTaskLabel.Location = new Point(150, 50 + i * 30);
            Controls.Add(writeTaskLabel);
        }
    }
}

internal class Program
{
    [STAThread] // Атрибут для работы с формами в однопоточном режиме
    static void Main()
    {
        Application.EnableVisualStyles(); // Включение визуальных стилей
        Application.SetCompatibleTextRenderingDefault(false); // Отключение совместимого текстового рендеринга

        Application.Run(new MyForm()); // Запуск приложения с указанием формы
    }
}