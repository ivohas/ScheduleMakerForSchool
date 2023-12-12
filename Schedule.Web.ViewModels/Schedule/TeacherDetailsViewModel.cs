public class TeacherDetailsViewModel
{
    public TeacherDetailsViewModel()
    {
        this.Subjects = new List<string>();
    }

    public string Name { get; set; }
    public int? NeededHours { get; set; }
    public List<string> Subjects { get; set; }
}