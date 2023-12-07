using BookFindingAndRatingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Schedule.Data;
using Schedule.Data.Models;
using Schedule.Services.Data.Interfaces;
using Schedule.Web.ViewModels.ApplicationUsers;
using Schedule.Web.ViewModels.Consultation;

namespace Schedule.Services.Data
{
    public class ConsultaionService : IConsultaionService
    {
        private readonly ScheduleDbContext _dbContext;
        public ConsultaionService(ScheduleDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task ClearDatabase()
        {
            this._dbContext.Consultations.RemoveRange(this._dbContext.Consultations);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task RecordInfoInTheDb(CreateConsultationViewModel info)
        {
            List<Consultation> consultation = info.Consultations
                .Select(a => new Consultation
                {
                    Teacher = this.GetOrCreateTeacher(a.TeacherName),
                    Subject = this.GetOrCreateSubject(a.SubjectName),
                    StartHour = a.StartHour,
                    TimeLenght = a.TimeLapse,
                    Day = a.Day
                })
                .ToList();
            this._dbContext.AddRange(consultation);
            await this._dbContext.SaveChangesAsync();
        }
        private Teacher GetOrCreateTeacher(string teacherName)
        {
            Teacher existingTeacher = this._dbContext.Teachers.FirstOrDefault(x => x.Name == teacherName);

            if (existingTeacher == null)
            {
                // Teacher doesn't exist, create a new one
                Teacher newTeacher = new Teacher { Id = new Guid(), Name = teacherName };
                this._dbContext.Teachers.Add(newTeacher);
                this._dbContext.SaveChanges(); // Save changes to the database
                return newTeacher;
            }

            return existingTeacher;
        }
        private Subject GetOrCreateSubject(string subjectName)
        {
            Subject existingSubject = this._dbContext.Subjects.FirstOrDefault(x => x.Name == subjectName);

            if (existingSubject == null)
            {
                // Subject doesn't exist, create a new one
                Subject newSubject = new Subject { Id = new Guid(), Name = subjectName };
                this._dbContext.Subjects.Add(newSubject);
                this._dbContext.SaveChanges(); // Save changes to the database
                return newSubject;
            }

            return existingSubject;
        }

        public async Task<List<Consultation>> GetAllConsultations()
        {
            return await this._dbContext.Consultations.Include(x => x.Teacher).ThenInclude(x => x.Subjects).ToListAsync();
        }

        public async Task<Dictionary<string, List<Consultation>>> ScheduleTheConsultationByDays(List<Consultation> consultations)
        {
            Dictionary<string, List<Consultation>> consultationSchedule = new Dictionary<string, List<Consultation>>();
            foreach (var consultation in consultations)
            {
                switch (consultation.Day)
                {
                    case "Monday":
                    case "Tuesday":
                    case "Wednesday":
                    case "Thursday":
                    case "Friday":
                    case "Saturday":
                    case "Sunday":
                        consultationSchedule[consultation.Day] = consultationSchedule.ContainsKey(consultation.Day) ? consultationSchedule[consultation.Day] : new List<Consultation> { consultation };
                        break;
                    default:
                        consultationSchedule["No day"] = consultationSchedule.ContainsKey("No day") ? consultationSchedule["No day"] : new List<Consultation> { consultation };
                        break;
                }
            }
            return consultationSchedule;
        }

        public async Task<Consultation> GetConsultationByIdAsync(Guid id)
        {
            return await this._dbContext.Consultations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddConsultataionToTheUser(Consultation consultation, string? userId)
        {
            UserConsultation uc = new UserConsultation()
            {
                Consultation = consultation,
                ConsultationID = consultation.Id,
                ApplicationUserID = Guid.Parse(userId),
                ApplicationUser = await this._dbContext.Users.FirstAsync(u=> u.Id.ToString() == userId)
            };
            this._dbContext.UsersConsultation.AddRange(uc);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<List<Consultation>> GetAllMyConsultationsAsync(string? userID)
        {
            return await this._dbContext.UsersConsultation
                .Where(uc=> uc.ApplicationUserID.ToString() == userID)
                .Select(x=> new Consultation
                { 
                    Id = x.ConsultationID,
                    Day = x.Consultation.Day,
                    StartHour = x.Consultation.StartHour,
                    Teacher = x.Consultation.Teacher,
                    Subject = x.Consultation.Subject,
                    TimeLenght = x.Consultation.TimeLenght                  
                })
                .ToListAsync();
        }

        public async Task RemoveConsultationFromUser(string? userId, Consultation consultation)
        {
            var userConsultation = await this._dbContext
                .UsersConsultation
                .FirstOrDefaultAsync(uc=> uc.ConsultationID == consultation.Id 
                && uc.ApplicationUserID.ToString() == userId);

            if (userConsultation != null)
            {
                this._dbContext.UsersConsultation.Remove(userConsultation);
                await this._dbContext.SaveChangesAsync();
            }
        }

        public async Task<ApplicationUserViewModel[]> GetAllParticipantceForAConsultationByIdAsync(Guid id)
        {
            return await this._dbContext.UsersConsultation.Where(x => x.ConsultationID == id)
                 .Select(x => new ApplicationUserViewModel
                 {
                     Username = x.ApplicationUser.UserName,
                     Phonenumber = x.ApplicationUser.PhoneNumber
                 }).ToArrayAsync();
        }
    }
}
