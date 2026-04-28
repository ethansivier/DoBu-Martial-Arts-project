using DobuMartial_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;

namespace DobuMartial_project.Data
{
    public class ApplicationDbContext :IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       

        public string img = "assets/img/";
        public Dictionary<string, Dictionary<string,string>> Descs = new Dictionary<string, Dictionary<string, string>>()
        {
            {"Inst", new Dictionary<string,string>{
                {"assistant","Assistant Martial Arts Coach" },
                {"fitness","Fitness Coach" }
            }},
        };

        public Class NewClass(int id, string name,  bool privateClass = false, bool kidClass = false)
        {
            return new Class { ClassID = id, Name = name, IsPrivate = privateClass, IsKids = kidClass };
        }
        public Session NewSession(int id, Class c, Day day, TimeOnly start, TimeOnly end)
        {
            return new Session { SessionId = id, ClassId = c.ClassID, DayID = day.DayID, TimeStart = start, TimeEnd = end };
        }

        public Day NewDay(int id, string name)
        {
            return new Day { DayID = id, Name = name };
        }

        public List<int> CList(int NameIndex, int TimeStartH, int TimeStartM, int TimeEndH, int TimeEndM)
        {
            return new List<int> { NameIndex, TimeStartH, TimeStartM, TimeEndH, TimeEndM};
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<Day> Days = new List<Day>
            {
                NewDay(1, "Monday"), NewDay(2, "Tuesday"), NewDay(3, "Wednesday"), NewDay(4, "Thursday"), NewDay(5, "Friday"), NewDay(6, "Saturday"), NewDay(7, "Sunday")
            };
            List<Class> Classes = new List<Class>
            {
                NewClass(1, "Jiu-Jitsu"), NewClass(2, "Muay-Thai"),  NewClass(3, "Private Tuition", true),    NewClass(4, "Open Mat/ Personal Practise"),
                NewClass(5, "Kids Jiu-Jitsu", false, true),  NewClass(6, "Karate"),  NewClass(7, "Judo"),    NewClass(8, "Kids Judo", false, true), 
                NewClass(9, "Kids Jiu-jitsu", false, true),  NewClass(10, "Kids karate", false, true), NewClass(11, "")
            };
            
            List<List<int>> Times = new List<List<int>> //times for each class goes monday - sunday 0 - 6
            {
                new List<int> {06, 30, 7, 30}, new List<int>{08, 00, 10, 00},
                new List<int>{10, 30, 12, 00}, new List<int>{13, 00, 14, 30},
                new List<int>{15, 00, 17, 00}, new List<int>{17, 30, 19, 00},
                new List<int>{19, 00, 21, 00}
            };
            List<List<int>> AllSessions = new List<List<int>>
            {   
                // 0 / "Monday"
                new List<int> {1,2,3,4,5,6,7},
                // 1 / "Tuesday"
                new List<int> {6,3,3,4,8,2,7},
                // 2 / "Wednesday"
                new List<int> {7,3,3,4,10,7,1},
                // 3 / "Thursday"
                new List<int> {1,3,3,4,9,1,6},
                // 4 / "Friday"
                new List<int> {2,1,3,4,8,2,3},
                // 5 / "Saturday"
                new List<int> {11,3,7,6,2,11,11},
                // 6 / "Sunday"
                new List<int> {11,3,6,7,1,11,11},
            }; 

            List<Session> Sessions = new List<Session>();
            int sessionID = 1;
            //loop through all classes, add them to list
            for (int dayid = 0; dayid < AllSessions.Count; dayid++)
            {
                var DayClasses = AllSessions[(int)dayid];
                var class_count = 0;
                foreach (var c in DayClasses)
                {
                    var t = 0;
                    Sessions.Add(NewSession(
                        sessionID++,
                        Classes[c - 1],
                        Days[dayid], // +1 since database starts at 1, not 0
                        new TimeOnly(Times[class_count][t++], Times[class_count][t++]),
                        new TimeOnly(Times[class_count][t++], Times[class_count++][t])
                        ));
                    
                }       
            } 


            builder.Entity<Instructor>().HasData( 
                // instructors
                new Instructor {InstructorID=1, Name="Mauricio Gomez", Description = "Gym Owner\nHead Martial arts coach", Image = $"{img}mauricio.jpg" },
                new Instructor {InstructorID=2, Name="Sarah Nova", Description = Descs["Inst"]["assistant"] , Image = $"{img}sarah.jpg"},
                new Instructor {InstructorID=3, Name="Guy Victory", Description = Descs["Inst"]["assistant"], Image = $"{img}guy.jpg"},
                new Instructor {InstructorID=4, Name="Morris Davis", Description = Descs["Inst"]["assistant"], Image = $"{img}morris.jpg"},
                new Instructor {InstructorID=5, Name="Traci Santiago", Description = Descs["Inst"]["fitness"], Image = $"{img}traci.jpg" },
                new Instructor {InstructorID=6, Name="Harpeet Kaur", Description = Descs["Inst"]["fitness"], Image = $"{img}harpeet.jpg" }
            );
            builder.Entity<Membership>().HasData(
                new Membership { MembershipId = 1, Name = "Basic", MartialArts = 1, Sessions = 2, Price = 25.00},
                new Membership { MembershipId = 2, Name = "Intermediate", MartialArts = 1, Sessions=3, Price = 35.00},
                new Membership { MembershipId = 3, Name = "Advanced", MartialArts = 2, Sessions = 5, Price = 45.00},
                new Membership { MembershipId = 4, Name =  "Elite", MartialArts = 100, Sessions = 100, Price = 60.00},
                new Membership { MembershipId = 5, Name = "Junior Membership", MartialArts = 100, Sessions = 100, Price = 25.00, IsKids = true}
            );
            builder.Entity<Day>().HasData(Days);
            builder.Entity<Class>().HasData(Classes);
            builder.Entity<Session>().HasData(Sessions);

            builder.Entity<User>().HasMany(e => e.Sessions).WithMany(e => e.Users).UsingEntity<UserSession>();
            builder.Entity<Day>().HasMany(e => e.Sessions).WithOne(e => e.Day).HasForeignKey(e => e.DayID);
            builder.Entity<Class>().HasMany(e => e.Sessions).WithOne(e => e.Class).HasForeignKey(e => e.ClassId);
        }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Day> WeekDays { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
