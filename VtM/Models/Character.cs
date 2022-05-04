using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VtM.Enums;

namespace VtM.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        public string? Name { get; set; }
        public string? Concept { get; set; }
        public int? ChronicleId { get; set; }

        public string? Ambition { get; set; }
        public string? Desire { get; set; }
        public int? PredatorTypeId { get; set; }

        public int? ClanId { get; set; }
        public int? Generation { get; set; }
        public string? Sire { get; set; }

        //-- Image --//
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; }
        
        [DisplayName("FileName")]
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }

        [DisplayName("File Extention")]
        public string? FileContentType { get; set; }



        //-- Attributes --//

        //-- Physical --//
        [Range(1, 5)]
        public int Strength { get; set; }
        [Range(1, 5)]
        public int Dexterity { get; set; }
        [Range(1, 5)]
        public int Stamina { get; set; }
        //-- Social --//
        [Range(1, 5)]
        public int Charisma { get; set; }
        [Range(1, 5)]
        public int Manipulation { get; set; }
        [Range(1, 5)]
        public int Composure { get; set; }
        //-- Mental --//
        [Range(1, 5)]
        public int Intelligence { get; set; }
        [Range(1, 5)]
        public int Wits { get; set; }
        [Range(1, 5)]
        public int Resolve { get; set; }

        //-- Skills --//
        public virtual ICollection<CharacterSkill> CharacterSkills { get; set; } = new HashSet<CharacterSkill>();

        //-- Touchstones & Convictions --//
        public virtual ICollection<TouchstoneConviction> TouchstoneConvictions { get; set; } = new HashSet<TouchstoneConviction>();

        //-- Disciplines --//
        public virtual ICollection<DisciplinePower> DisciplinePowers { get; set; } = new HashSet<DisciplinePower>();
        public virtual ICollection<DisciplineLevel> DisciplineLevels { get; set; } = new HashSet<DisciplineLevel>();

        //-- Health --//
        public int SuperficialDamageTaken { get; set; }
        public int AggravatedDamageTaken { get; set; }
        //-- Willpower --//
        public int SuperficialWillpowerDamageTaken { get; set; }
        public int AggravatedWillpowerDamageTaken { get; set; }
        [NotMapped]
        public int UnspendWillpower { get { return Composure + Resolve - SuperficialDamageTaken - AggravatedDamageTaken; } } 

        //-- Humanity --//
        [Range(1,10)]
        public int Humanity { get; set; }
        public int Stains { get; set; }
        
        //-- Hunger --//
        [Range(0,5)]
        public int? Hunger { get; set; }
        
        //-- Blood Potency --//
        [Range(0,10)]
        public int? BloodPotencyId { get; set; }

        //-- Resonance --//
        public Resonance ResonanceType { get; set; }

        //-- Biography --//
        public int TrueAge { get; set; }

        public int ApparentAge { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset DateOfDeath { get; set; }

        public string? Appearance { get; set; }
        public string? DistinguishingFeatures { get; set; }
        public string? History { get; set; }

        //-- Experience --//
        public int ExperienceTotal { get; set; }
        public int ExperienceSpent { get; set; }
        [NotMapped]
        public int ExperienceLeft { get { return ExperienceTotal - ExperienceSpent; } }



        public Publicity CharacterPublicity { get; set; }
        public int? CoterieId { get; set; }

        public virtual ICollection<Weapon>? Weapons { get; set; } = new HashSet<Weapon>();

        public int? HavenId { get; set; }
        public virtual Haven? Haven { get; set; }

        public virtual ICollection<Ritual>? Rituals { get; set; } = new HashSet<Ritual>();
        public virtual ICollection<ThinBloodAlchemy>? ThinBloodAlchemies { get; set; } = new HashSet<ThinBloodAlchemy>();

        public ThinBloodDistillationMethod ThinBloodDistillationMethod { get; set; }
        public virtual ICollection<LoreSheetPart> LoreSheetParts { get; set; } = new HashSet<LoreSheetPart>();

        //-- Navigational Properties --//
        public virtual Chronicle? Chronicle { get; set; }
        public virtual Coterie? Coterie { get; set; }
        public virtual PredatorType? PredatorType { get; set; }
        public virtual Clan Clan { get; set; } = null!;
        public virtual VtMUser User { get; set; } = null!;
        public virtual BloodPotency? BloodPotency { get; set; }

        public virtual ICollection<Possession> Possessions { get; set; } = new HashSet<Possession>();
        public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();
        public virtual ICollection<Flaw> Flaw { get; set; } = new HashSet<Flaw>();
        public virtual ICollection<Background> Backgrounds { get; set; } = new HashSet<Background>();
        public virtual ICollection<Merit> Merits { get; set; } = new HashSet<Merit>();
    }

}
