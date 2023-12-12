public class TeacherDetailsViewModel
{
    public TeacherDetailsViewModel()
    {
        this.Subjects = new List<string>();
        this.AssignedHours = 0;
    }

    public string Name { get; set; }
    public int NeededHours { get; set; }
    public List<string> Subjects { get; set; }
    public int AssignedHours { get; set; }
    public List<(string ClassName, string Subject)> AssignedClasses { get; set; } = new List<(string, string)>();
}