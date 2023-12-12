using Schedule.Data;
using Schedule.Data.Models;
using Schedule.Services.Data.Interfaces;
using Schedule.Web.ViewModels.Schedule;
using Microsoft.EntityFrameworkCore;
using Schedule.Web.ViewModels;

namespace Schedule.Services.Data
{
    public class ScheduleService : IScheduleService
    {
        private readonly ScheduleDbContext _dbContext;

        public ScheduleService(ScheduleDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task ClearAllClassesInTheDb()
        {
            _dbContext.Classes.RemoveRange(_dbContext.Classes);
            await _dbContext.SaveChangesAsync();
        }

        // Clears all the info about subjects, before you add new
        public async Task ClearAllSubjectsInTheDbAsync()
        {
            _dbContext.Subjects.RemoveRange(_dbContext.Subjects);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearAllTeachersAsync()
        {
            _dbContext.Teachers.RemoveRange(_dbContext.Teachers);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ClassesViewModel> GetAllClassesAsync()
        {
            ClassesViewModel classesViewModel = new ClassesViewModel();
            classesViewModel.Classes = await _dbContext
                .Classes
                .Select(x => new ClassesDetailsViewModel
                {
                    Name = x.Name,
                    SubjectPerWeeks = x.SubjectsPerWeeks
                    .Select(y => new SubjectPerWeekViewModel
                    {
                        HoursPerWeek = y.HourSPerWeek,
                        SubjectName = y.SubjectName
                    }).ToList()
                })
                .ToListAsync();
            return classesViewModel;
        }

        public async Task<ICollection<string>> GetAllSubjectsAsync()
        {
            return await _dbContext.Subjects.Select(x => x.Name).ToListAsync();
        }

        public async Task<TeacherViewModel> GetAllTeachersAsync()
        {
            TeacherViewModel teachersViewModel = new TeacherViewModel();
            teachersViewModel.Teachers = await this._dbContext
                .Teachers
                .Include(x => x.Subjects)
                .Select(x => new TeacherDetailsViewModel
                {
                    Name = x.Name,
                    NeededHours = x.NeededHours,
                    Subjects = x.Subjects.Select(x => x.Name).ToList()
                })
                .ToListAsync();
            return teachersViewModel;
        }

        public async Task RecordClassesInDbAsync(ClassesViewModel classModel)
        {
            List<SubjectsPerWeek> subjectsPerWeeks = new List<SubjectsPerWeek>();

            List<Class> classes = classModel.Classes.Select
                (x => new Class
                {
                    Name = x.Name,
                    SubjectsPerWeeks = x.SubjectPerWeeks.Select(x => new SubjectsPerWeek
                    {
                        HourSPerWeek = int.Parse(x.HoursPerWeek.ToString()),
                        SubjectName = x.SubjectName,
                    }).ToList()

                }).ToList();
            _dbContext.Classes.AddRange(classes);
            await _dbContext.SaveChangesAsync();
        }

        // Put all the subjects to the DB
        public async Task RecordSubjectInfoAsync(SubjectViewModel viewModel)
        {
            List<Subject> info =
                viewModel
                .Names
                .Select(x => new Subject
                {
                    Name = x.ToString()
                }).AsEnumerable()
                .ToList();
            _dbContext.Subjects.AddRange(info);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RecordTeachersInfoAsync(TeacherViewModel info)
        {
            List<Teacher> teachers = info
                .Teachers
                .Select(t => new Teacher
                {
                    Name = t.Name,
                    NeededHours = t.NeededHours,
                    Subjects = t.Subjects
                    .Select(s => new Subject
                    {
                        Name = s.ToString(),
                    }).ToArray()

                }).ToList();
            _dbContext.Teachers.AddRange(teachers);
            await _dbContext.SaveChangesAsync();
        }
    }
}
