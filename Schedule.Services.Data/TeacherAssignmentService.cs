using Schedule.Data;
using Schedule.Data.Models;
using Schedule.Services.Data.Interfaces;
using Schedule.Web.ViewModels.Schedule;
using System.Text;

namespace Schedule.Services.Data
{
    public class TeacherAssignmentService : ITeacherAssignmentService
    {
        private readonly ScheduleDbContext _dbContext;

        public TeacherAssignmentService(ScheduleDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<string> GiveATeacherClasses(ClassesViewModel classes, TeacherViewModel teachers)
        {
           
            AssignTeachers(classes, teachers);
            return Task.FromResult(PrintTeacherAssignments(teachers.Teachers));
        }
        public static void AssignTeachers(ClassesViewModel classes, TeacherViewModel teachers) { 
            foreach (var schoolClass in classes.Classes)
            {
                foreach (var subject in schoolClass.SubjectPerWeeks)
                {
                    var eligibleTeachers = GetTheTeacherWitchIsBest(teachers.Teachers, subject.SubjectName);

                    //var assignedTeacher = AssignClassToTeacher(schoolClass, eligibleTeachers, subject.Key);
                    var assignedTeacher = eligibleTeachers.FirstOrDefault();
                    if (assignedTeacher != null)
                    {

                        assignedTeacher.AssignedHours += subject.HoursPerWeek;
                        schoolClass.Assigned = true;

                        assignedTeacher.AssignedClasses.Add((schoolClass.Name, subject.SubjectName));
                    }
                }
            }

            // Distribute remaining hours if all teachers have met their minimum requirements
            if (AllTeachersMetMinimum(teachers.Teachers))
            {
                var remainingHours = CalculateRemainingHours(teachers.Teachers);

                foreach (var teacher in teachers.Teachers.OrderBy(t => t.AssignedHours))
                {
                    if (remainingHours > 0)
                    {
                        var additionalHours = Math.Min(remainingHours, teacher.NeededHours - teacher.AssignedHours);
                        teacher.AssignedHours += additionalHours;

                        // Console.WriteLine($"Assigned additional {additionalHours} hours to {teacher.Name}");
                        remainingHours -= additionalHours;
                    }
                }
            }
        }
        private static List<TeacherDetailsViewModel> GetTheTeacherWitchIsBest(List<TeacherDetailsViewModel> teachers, string subjectName)
        {
            teachers = teachers.Where(x => x.Subjects.Contains(subjectName)).ToList();
            if (teachers.Where(x => x.AssignedHours < x.NeededHours).Count() > 0)
            {
                teachers = teachers.Where(x => x.AssignedHours < x.NeededHours).ToList();
            }

            return teachers
                         .Where(t => t.Subjects.Contains(subjectName))
                         .OrderBy(t => t.AssignedHours - t.NeededHours)
                         .ToList();
        }

        private static TeacherDetailsViewModel AssignClassToTeacher(ClassesDetailsViewModel schoolClass, List<TeacherDetailsViewModel> eligibleTeachers, string subject)
        {
            foreach (var teacher in eligibleTeachers)
            {
                if (!teacher.AssignedClasses.Any(ac => ac.ClassName == schoolClass.Name && ac.Subject == subject))
                {
                    return teacher;
                }
            }

            return null;
        }

        private static IEnumerable<string> GetAllSubjects(List<ClassesDetailsViewModel> classes)
        {
            return classes.SelectMany(c => c.SubjectPerWeeks.Select(x=> x.SubjectName)).Distinct();
        }

        private static bool AllTeachersMetMinimum(List<TeacherDetailsViewModel> teachers)
        {
            return teachers.All(t => t.AssignedHours >= t.NeededHours);
        }

        private static int CalculateRemainingHours(List<TeacherDetailsViewModel> teachers)
        {
            return teachers.Sum(t => t.NeededHours - t.AssignedHours);
        }
        public static string PrintTeacherAssignments(List<TeacherDetailsViewModel> teachers)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var teacher in teachers)
            {
                sb.AppendLine($"{teacher.Name}: {string.Join(", ", teacher.AssignedClasses.Select(ac => $"{ac.Subject} ({ac.ClassName})"))} ({string.Join(", ", teacher.Subjects)})");
            }

            sb.AppendLine("\nSummary:");
            foreach (var teacher in teachers)
            {
                sb.AppendLine($"{teacher.Name}: Minimum Hours: {teacher.NeededHours}, Assigned Hours: {teacher.AssignedHours}");
            }
            return sb.ToString();
        }
      
    }
}
