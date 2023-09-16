namespace MinimalApiChanges.Model
{
    public class ToDoItem
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Completed { get; set; }

    }
}
