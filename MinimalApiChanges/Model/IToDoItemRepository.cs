namespace MinimalApiChanges.Model
{
    public interface IToDoItemRepository
    {
        List<ToDoItem>? GetAllToDoItems();
        void AddToDoItem(ToDoItem toDoItem);
    }
}