using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VtM.Models;

namespace VtM.Data
{
    public class ApplicationDbContext : IdentityDbContext<VtMUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Background>? Backgrounds { get; set; }
        public DbSet<BloodPotency>? BloodPotencies { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Character>? Characters { get; set; }
        public DbSet<CharacterSkill>? CharactersSkills { get; set; }
        public DbSet<Chronicle>? Chronicles { get; set; }
        public DbSet<Clan>? Clans { get; set; }
        public DbSet<Coterie>? Coteries { get; set; }
        public DbSet<CoterieTenet>? coterieTenets { get; set; }
        public DbSet<Discipline>? Disciplines { get; set; }
        public DbSet<DisciplineLevel>? DisciplineLevels { get; set; }
        public DbSet<DisciplinePower>? DisciplinePowers { get; set; }
        public DbSet<Flaw>? Flaws { get; set; }
        public DbSet<Haven>? Havens { get; set; }
        public DbSet<HavenFlaw>? HavenFlaws { get; set; }
        public DbSet<HavenImage>? HavenImages { get; set; }
        public DbSet<HavenMerit>? HavenMerits { get; set; }
        public DbSet<LoreSheet>? LoreSheets { get; set; }
        public DbSet<LoreSheetPart>? LoreSheetParts { get; set; }
        public DbSet<Merit>? Merits { get; set; }
        public DbSet<Note>? Notes { get; set; }
        public DbSet<Possession>? Possessions { get; set; }
        public DbSet<PredatorType>? PredatorTypes { get; set; }
        public DbSet<Ritual>? Rituals { get; set; }
        public DbSet<Skill>? Skills { get; set; }
        public DbSet<SkillSpecialization>? SkillSpecializations { get; set; }
        public DbSet<ThinBloodAlchemy>? ThinBloodAlchemies { get; set; }
        public DbSet<TouchstoneConviction>? TouchstoneConvictions { get; set; }
        public DbSet<VtMUser>? VtMUsers { get; set; }
        public DbSet<Weapon>? Weapons { get; set; }













    }
}