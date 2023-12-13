using Schedule.Data;
using Schedule.Data.Models;
using Schedule.Services.Data.Interfaces;
using Schedule.Web.ViewModels.Schedule;
using Schedule.Web.ViewModels.TeacherAssignment;
using System.Runtime.CompilerServices;
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
     
        public static void AssignTeachers(ClassesViewModel classes, TeacherViewModel teachers)
        {
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

        private static bool AllTeachersMetMinimum(List<TeacherDetailsViewModel> teachers)
        {
            return teachers.All(t => t.AssignedHours >= t.NeededHours);
        }

        private static int CalculateRemainingHours(List<TeacherDetailsViewModel> teachers)
        {
            return teachers.Sum(t => t.NeededHours - t.AssignedHours);
        }
        public static TeacherAssignmentViewModel PrintTeacherAssignments(List<TeacherDetailsViewModel> teachers)
        {
            TeacherAssignmentViewModel teacherAssignments = new TeacherAssignmentViewModel();
            
            foreach (var teacher in teachers)
            {
               teacherAssignments.teacherAssignments[teacher.Name]= teacher.AssignedClasses.Select(ac => $"{ac.Subject} ({ac.ClassName})").ToList();
            }
            return teacherAssignments;
        }

        public Task<TeacherAssignmentViewModel> GiveATeacherClassesAsync(ClassesViewModel classes, TeacherViewModel teachers)
        {
            AssignTeachers(classes, teachers);
            return Task.FromResult(PrintTeacherAssignments(teachers.Teachers));
        }
    }
}
