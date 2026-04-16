using DobuMartial_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DobuMartial_project.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Instructor>().HasData(
                new Instructor {InstructorID=1, Name="Mauricio Gomez", Description="Gym Owner\nHead Martial arts coach", Image=$"{img}mauricio.jpg" },
                new Instructor {InstructorID=2, Name="Sarah Nova", Description = Descs["Inst"]["assistant"] , Image=$"{img}sarah.jpg"},
                new Instructor { InstructorID=3,Name="Guy Victory", Description= Descs["Inst"]["assistant"], Image=$"{img}guy.jpg"},
                new Instructor { InstructorID=4, Name="Morris Davis",Description= Descs["Inst"]["assistant"], Image=$"{img}morris.jpg"},
                new Instructor { InstructorID=5, Name="Traci Santiago", Description= Descs["Inst"]["fitness"], Image=$"{img}traci.jpg" },
                new Instructor {InstructorID=6, Name="Harpeet Kaur", Description = Descs["Inst"]["fitness"], Image=$"{img}harpeet.jpg" }
            );
        }

        public DbSet<Instructor> Instructors { get; set; }
    }
}
