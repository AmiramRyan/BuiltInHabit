namespace BuiltInHabit.Backend.DTOs
{
    public class UpdateHabitRequest
    {
        public string FieldName { get; set; }
        public object NewValue { get; set; }
    }
}
