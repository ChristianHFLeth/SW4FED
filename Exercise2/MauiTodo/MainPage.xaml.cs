using MauiTodo.Models;
namespace MauiTodo;

public partial class MainPage : ContentPage
{
	string _toDoListData = string.Empty;
	readonly Database _database;

	public MainPage()
	{
		InitializeComponent();
		_database = new Database();
		_ = Initialize();
	}

	private async Task Initialize()
	{
		var todos = await _database.GetTodos();
		foreach (var todo in todos)
		{
			_toDoListData += $"{todo.Title} - Due: {todo.Due}\n";
		}
		TodosLabel.Text = _toDoListData;
	}

	async void Button_Clicked(object sender, EventArgs e)
	{
		var todo = new TodoItem
		{
			Title = TaskEntry.Text,
			Due = DueDatePicker.Date
		};
		var inserted = await _database.Addtodo(todo);

		if(inserted!=0)
		{
			_toDoListData += $"{todo.Title} - Due: {todo.Due}\n";
			TodosLabel.Text = _toDoListData;

			TaskEntry.Text = string.Empty;
			DueDatePicker.Date = DateTime.Now;
		}
	}
	
}

