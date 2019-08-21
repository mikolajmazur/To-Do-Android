namespace ToDoAndroid.Models
{
    class ToDoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; } = false;

    }
}