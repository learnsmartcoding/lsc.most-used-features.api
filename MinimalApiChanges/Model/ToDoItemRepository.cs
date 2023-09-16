namespace MinimalApiChanges.Model
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private static List<ToDoItem>? _allToDoItems;

        public List<ToDoItem>? GetAllToDoItems()
        {
            if (_allToDoItems == null)
            {
                InitializeData();
            }

            return _allToDoItems;
        }

        public void AddToDoItem(ToDoItem toDoItem)
        {
            _allToDoItems?.Add(toDoItem);
        }

        private void InitializeData()
        {
            _allToDoItems = new List<ToDoItem>()
                {
                    new ToDoItem(){Title = "Get milk", Description="Task to get milk in the morning", Completed=true },
                    new ToDoItem(){Title = "Learn Python", Description="Learn python for future"},
                    new ToDoItem(){Title = "Create an app with help of Learn Smart Coding", Description="Good source of learning content.", Completed=true},
                    new ToDoItem(){Title = "Put up Christmas tree", Description="December is closer now."}
                };
        }
    }
}
