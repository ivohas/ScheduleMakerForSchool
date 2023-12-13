namespace Schedule.Web.ViewModels.TeacherAssignment
{
    public class TeacherAssignmentViewModel
    {
        public TeacherAssignmentViewModel()
        {
            this.teacherAssignments = new Dictionary<string, List<string>>();
        }
        public Dictionary<string, List<string>> teacherAssignments { get; set; }
    }
}
