using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VtM.Data;
using VtM.Enums;
using VtM.Models;

namespace VtM.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<VtMUser> _userManager;

        public DataService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<VtMUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedBooksAsync();
            await SeedClans();
            await SeedBloodPotency();
            await SeedDisciplines();
            await SeedDisciplinePower();
            await SeedSkills();
            await SeedChronicle();
            await SeedPredatorTypes();
            await SeedLoreSheets();
            await SeedLoreSheetParts();
            await SeedRituals();
            await SeedThinBloodAlchemy();
        }

        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any()) return;
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_context.Users.Any()) return;

            var user = new VtMUser()
            {
                Email = "Admin@gmail.com",
                UserName = "Admin@gmail.com",
                NickName = "Floki",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user, "Cas&1234");
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            var userST = new VtMUser()
            {
                Email = "StoryTeller@gmail.com",
                UserName = "StoryTeller@gmail.com",
                NickName = "StoryFloki",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(userST, "Cas&1234");
            await _userManager.AddToRoleAsync(userST, Roles.StoryTeller.ToString());

            var userP = new VtMUser()
            {
                Email = "Player@gmail.com",
                UserName = "Player@gmail.com",
                NickName = "PlayerFloki",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };

            await _userManager.CreateAsync(userP, "Cas&1234");
            await _userManager.AddToRoleAsync(userP, Roles.Player.ToString());

            var userG = new VtMUser()
            {
                Email = "Guest@gmail.com",
                UserName = "Guest@gmail.com",
                NickName = "GuestFloki",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(userG, "Cas&1234");
            await _userManager.AddToRoleAsync(userG, Roles.Guest.ToString());

        }

        private async Task SeedBooksAsync()
        {
            if (_context.Books.Any()) return;
            try
            {
                IList<Book> books = new List<Book>() {
                     new Book(){ Title = "N/A" },
                     new Book(){ Title = "Core Rulebook" },
                     new Book(){ Title = "Camarilla" },
                     new Book(){ Title = "Anarch" },
                     new Book(){ Title = "Sabbat" },
                     new Book(){ Title = "Chicago by Night" },
                     new Book(){ Title = "Chigago Folio" },
                     new Book(){ Title = "Cult of the Blood Gods" },
                     new Book(){ Title = "The Fall of London" },
                     new Book(){ Title = "Trails of Ash and Bone" },
                     new Book(){ Title = "Companion" },
                     new Book(){ Title = "Second Inquisition" },
                     new Book(){ Title = "Book of Nod" },
                     new Book(){ Title = "Let the Streets Run Red" },
                     new Book(){ Title = "Children of the Blood" },
                     new Book(){ Title = "Auld Sanguine" }
                };

                await _context.AddRangeAsync(books);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Books.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }
        private async Task SeedClans()
        {
            if (_context.Clans.Any()) return;

            try
            {
                IList<Clan> clans = new List<Clan>()
                {
                    new Clan(){
                        Name = "Human",
                        Bane = "None.",
                        Compulsion = "None.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("N/A")).Id
                    },
                    new Clan(){
                        Name = "Brujah",
                        Bane = "The Blood of the Brujah simmers with barely contained rage, exploding at the slightest provocation.Subtract dice equal to the Bane Severity of the Brujah from any rollto resist fury frenzy. This cannottake the pool below one die.",
                        Compulsion = "The vampire takes a stand against whatever or whomever they see as the status quo in the situation, whether that’s their leader, a viewpoint expressed by a potential vessel, or just the task they were supposed to do at the moment. Until they’ve gone against their orders or expectations, perceived or real, the vampire receives a two dice penalty to all rolls. This Compulsion ends once they’ve managed to either make someone changetheir minds(by force if necessary) or done the opposite of what was expected of them.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Gangrel",
                        Bane = "Gangrel relate to their Beast much as other Kindred relate to the Gangrel: suspicious partnership. In frenzy, Gangrel gain one or more animal features: a physical trait, a smell, or a behavioral tic.These features last for one more night afterward, lingering like a hangover following debauchery.Each feature reduces one Attribute by 1 point – the Storyteller may decide that a forked tongue or bearlike musk reduces Charisma, while batlike ears reduce Resolve(“all those distracting sounds”).If nothing immediately occurs to you, the feature reduces Intelligence or Manipulation. The number of features a Gangrel manifests equals their Bane Severity.If your character Rides the Wave of their frenzy (see p. 219) you can choose only one feature to manifest, thus taking only one penalty to their Attributes.",
                        Compulsion = "Returning to an animalistic state, the vampire regresses to a point where speech is hard, clothes are uncomfortable, and arguments are best settled with teeth and claws. For one scene, the vampire gains a three-dice penalty to all rolls involving Manipulation and Intelligence. They can only speak in one-word sentences during this time.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Malkavian",
                        Bane = "Afflicted by their lineage, all Malkavians are cursed with at least one type of mental derangement. Depending on their history and the state of their mind at death, they may experience delusions, visions of terrible clarity, or something entirely different. When the Malkavian suffers a Bestial Failure or a Compulsion, their curse comes to the fore. Suffer a penalty equal to your character’s Bane Severity to one category of dice pools (Physical, Social, or Mental) for the entire scene.This is in addition to any penalties incurred by Compulsions. You and the Storyteller decide the type of penalty and the exact nature of the character’s affliction during character creation.",
                        Compulsion = "Their extrasensory gifts running wild, the vampire experiences what might be truths or portents, but what others call figments of imagination, dredged up by Hunger. While still functional, the vampire’s mind and perceptions are skewed. They receive a two-dice penalty to rolls involving Dexterity, Manipulation, Composure, and Wits as well as on rolls to resist terror frenzy, for one scene.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Nosferatu",
                        Bane = "Hideous and vile, all Nosferatu count as having the Repulsive Flaw (-2) and can never increase their rating in the Looks Merit. In addition, any attempt to disguise themselves as non-deformed incur a penalty to the dice pool equal to the character’s Bane Severity (this includes Discipline powers like Mask of a Thousand Faces and Impostor’s Guise). Most Nosferatu do not breach the Masquerade by just being seen.They are perceived by mortals to be grotesque and often terrifying, but not always supernaturally so.",
                        Compulsion = "The need to know permeates the vampire. They become consumed with a hunger for secrets, to know that which few or no one knows, almost as strong as that for blood. They also refuse to share secrets with others, except in strict trade for greater ones. All actions not spent working toward learning a secret, no matter how big or small, receive a two-dice penalty.The Compulsion ends when the vampire learns a secret big enough to be considered useful. Sharing this secret is optional.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Toreador",
                        Bane = "Toreador exemplify the old saying that art in the blood takes strange forms. They desire beauty so intensely that they suffer in its absence. While your character finds itself in less than beautiful surroundings, lose the equivalent of their Bane Severity in dice from dice pools to use Disciplines. The Storyteller decides specifically how the beauty or ugliness of the Toreador’s environment (including clothing, blood dolls, etc.) penalizes them, based on the character’s aesthetics. That said, even devotees of the Ashcan School never find normal streets perfectly beautiful. This obsession with aesthetics also causes divas to lose themselves in moments of beauty and a bestial failure often results in a rapt trance, as detailed in the Compulsion rules (p. 208).",
                        Compulsion = "Enraptured by beauty, the vampire becomes temporarily obsessed with a singular gorgeous thing, able to think of nothing else. Pick one feature, such as a person, a song, an artwork, blood spatter, or even a sunrise.Enraptured, the vampire can hardly take their attention from it, and if spoken to, they only talk about that subject.Any other actions receive a two-dice penalty.This Compulsion lasts until they can no longer perceive the beloved object, or the scene ends.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Tremere",
                        Bane = "Once the clan was defined by a rigid hierarchy of Blood Bonds reaching from the top to the bottom of the Pyramid. But after the fall of Vienna, their Blood has recoiled and aborted all such connections. Tremere vitae can no longer Blood Bond other Kindred, though they themselves can be Bound by Kindred from other clans. A Tremere can still bind mortals and ghouls, though the corrupted vitae must be drunk an additional number of times equal to the vampire’s Bane Severity for the bond to form. Some theorize this change is the revenge of the Antediluvian devoured by Tremere, others attribute it to a simple mutation.Regardless, the clan studies their vitae intently to discover if the process can be reversed, and, indeed, determine if they would want to do so.",
                        Compulsion = "Nothing but the best satisfies the vampire. Anything less than exceptional performance instills a profound sense of failure, and they often repeat tasks obsessively to get them “just right.” Until the vampire scores a critical win on a Skill roll or the scene ends, the vampire labors under a two-dice penalty to all dice pools. Reduce the penalty to one die for a repeated action, and remove it entirely on a second repeat.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Ventrue",
                        Bane = "The Ventrue are in possession of rarefied palates. When a Ventrue drinks blood from any mortal outside their preference, a profound exertion of will is required or the blood taken surges back up as scarlet vomit. Preferences range greatly, from Ventrue who can only feed from genuine brunettes, individuals of Swiss descent, or homosexuals, to others who can only feed from soldiers, mortals who suffer from PTSD, or methamphetamine users. With a Resolve + Awareness test (Difficulty 4 or more) your character can sense if a mortal possesses the blood they require. If you want your character to feed from anything but their preferred victim, you must spend Willpower points equal to the character’s Bane Severity.",
                        Compulsion = "The need to rule rears its head in the vampire. They stop at nothing to assume command of a situation. Someone must obey an order from the vampire. Any action not directly associated with leadership receives a two-dice penalty. This Compulsion lasts until an order has been obeyed, though the order must not be supernaturally enforced, such as through Dominate.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){
                        Name = "Caitiff",
                        Bane = "Untouched by the Antediluvians, the Caitiff share no common bane. Caitiff characters begin with the Suspect (•) Flaw and you may not purchase positive Status for them during character creation.The Storyteller may always impose a one or two dice penalty on Social tests against fellow Kindred who know they are Caitiff, regardless of their eventual Status. Further, to improve one of the Disciplines of a Caitiff costs six times the level purchased in experience points.",
                        Compulsion = "None.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){ Name = "Thinblood",
                        Bane = "None.",
                        Compulsion = "None.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Clan(){ Name = "The Ministry",
                        Bane = "The Blood of a Minister abhors the light. When exposed to direct illumination – whether natural or artificial – members of the clan recoil. Ministers receive a penalty equal to their Bane Severity to all dice pools when subjected to bright light directed straight at them. Also, add their Bane Severity to Aggravated damage taken from sunlight.",
                        Compulsion = "Set teaches that everyone’s mind and spirit are bound by invisible chains of their own making. Their Blood chafing at these bindings, the Minister suffers a burning need to break them.The vampire receives a two - dice penalty to all dice pools not relating to enticing someone(including themselves) to break a Chronicle Tenet or personal Conviction, causing at least one Stain and ending this Compulsion.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Anarch")).Id
                    },
                    new Clan(){ Name = "Banu Haqim",
                        Bane = "Banu Haqim are drawn to feed from those deserving punishment. This is especially true for vampire Blood, the very essence of transgression.When one of the Judges tastes the Blood of another Cainite, they find it very hard to stop.Slaking at least one Hunger level with vampiric vitae provokes a Hunger Frenzy test(See Core Rules p. 220) at a Difficulty 2 + Bane Severity.If the test is failed they attempt to gorge themselves on vampire Blood, sometimes until they diablerize their Kindred victim.This presents many problems as the Banu Haqim integrate with the Camarilla, who tend to see the Amaranth as anathema.",
                        Compulsion = "The vampire is compelled to punish anyone seen to transgress against their personal creed, taking their blood as just vengeance for the crime. For one scene, the vampire must slake at least one Hunger from anyone, friend or foe, that acts against a Conviction of theirs.Failing to do so results in a three-dice penalty to all rolls until the Compulsion is satisfied or the scene ends. (If the one fed from is also a vampire, don’t forget to test for Baneinduced Hunger frenzy.).",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Camarilla")).Id
                    },
                    new Clan(){ Name = "Lasombra",
                        Bane = "Anyone seeing the reflection or recording (live and otherwise) of a Lasombra vampire can instantly recognize them for what they are, provided they know what they’re looking for. People with no prior knowledge will know something is wrong, but likely attribute the distortion to irregularities in the reflecting surface or recording errors.Note that this will not hide the identity of the vampire with any certainty, and the Lasombra are no less likely to be caught on surveillance than any other vampire.In addition, use of modern communication technology, including making a simple phone call, requires a Technology test at Difficulty 2 + Bane Severity as microphones have similar problems with the voice of a Lasombra as cameras with their image. Avoiding electronic vampire detection systems is also done at a penalty equal to Bane Severity.",
                        Compulsion = "To the Lasombra, failure is not an option. Their Blood will urge them to any act conceivable to reach their goals, whether in the moment or in Byzantine plots lasting centuries. Any setback is felt profoundly and they quickly escalate to the most ruthless of methods until they achieve their aims. The next time the vampire fails any action they receive a two-dice penalty to any and all rolls until a future attempt at the same action succeeds. Note that the above penalty applies to future attempts at the triggering action as well.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Chicago by Night")).Id
                    },
                    new Clan(){ Name = "Hecata",
                        Bane = "Steeped in death, the fangs of the Hecata bring not bliss, but agony. Victims caught unawares will violently resist unless restrained, and few people submit willingly to the torture that is the Hecata Kiss. When drinking directly from a victim, Hecata may only take harmful drinks, resulting in blood loss (Vampire: The Masquerade, p.212). Unwilling mortals not restrained will try to escape, and even those coerced or willing must succeed in a Stamina +Resolve test against Difficulty 2 + Bane Severity in order not to recoil. Coerced or willing vampire victims of the Hecata bite must make a frenzy test against Difficulty 3 to avoid falling into a terror frenzy.",
                        Compulsion = "The Hecata are possessed of a peculiar curiosity paired with detachment from compassion and empathy, likely due to their frequent dealings with corpses and the wraiths of those who died tragic deaths. Their Blood urges them to study the individuals around them for signs of illness, frailty, or impending death.Until they have either predicted a death without supernatural means or solved the cause of a local one, the vampire suffers a three - dice penalty to other rolls until the scene ends.Note that their conclusions do not need to be absolutely correct, but should stay within the boundaries of the possible.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Cult of the Blood Gods")).Id
                    },
                    new Clan(){ Name = "Giovanni",
                        Bane = "Steeped in death, the fangs of the Hecata bring not bliss, but agony. Victims caught unawares will violently resist unless restrained, and few people submit willingly to the torture that is the Hecata Kiss. When drinking directly from a victim, Hecata may only take harmful drinks, resulting in blood loss (Vampire: The Masquerade, p.212). Unwilling mortals not restrained will try to escape, and even those coerced or willing must succeed in a Stamina +Resolve test against Difficulty 2 + Bane Severity in order not to recoil. Coerced or willing vampire victims of the Hecata bite must make a frenzy test against Difficulty 3 to avoid falling into a terror frenzy.",
                        Compulsion = "The Hecata are possessed of a peculiar curiosity paired with detachment from compassion and empathy, likely due to their frequent dealings with corpses and the wraiths of those who died tragic deaths. Their Blood urges them to study the individuals around them for signs of illness, frailty, or impending death.Until they have either predicted a death without supernatural means or solved the cause of a local one, the vampire suffers a three - dice penalty to other rolls until the scene ends.Note that their conclusions do not need to be absolutely correct, but should stay within the boundaries of the possible.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Cult of the Blood Gods")).Id
                    },
                    new Clan(){ Name = "Tzimisce",
                        Bane = "The Tzimisce are grounded: Each Tzimisce must choose a specific charge — a physical domain, a group of people, an organization, or even something more esoteric — but clearly defined and limited. The Kindred must spend their daysleep surrounded by their chosen charge.Historically this has often meant slumbering in the soil of their land, but it can also mean being surrounded by that which they tonight rule: a certain kind of people, a building deeply tied to their obsession, a local counterculture faction, or other, more outlandish elements.If they do not, they sustain aggravated Willpower damage equal to their Bane Severity upon waking the following night.",
                        Compulsion = "When a Tzimisce suffers a Compulsion, the Kindred becomes obsessed with possessing something in the scene, desiring to add it to their proverbial hoard. This can be anything from an object to a piece of property to an actual person. Any action not taken toward this purpose incurs a two-dice penalty. The Compulsion persists until ownership is established (the Storyteller decides what constitutes ownership in the case of a non-object) or the object of desire becomes unattainable.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Companion")).Id
                    },
                    new Clan(){ Name = "Salubri",
                        Bane = "The Salubri are hunted: Kindred of other clans are especially... appreciative of Salubri vitae. When a non-Salubri partakes of the blood of a Cyclops, they often find it difficult to pull themselves away. Consuming enough to abate at least one Hunger level requires a Hunger Frenzy test (See Vampire: The Masquerade, p. 220) at a Difficulty 2 + the Salubri’s Bane Severity (difficulty 3 + the Salubri’s Bane Severity for Banu Haqim).If the test fails, they just keep consuming, to the point that they may have to be physically fought off. Additionally, the third eye that Saulot opened while on one of his many journeys passes down through the bloodline every time a Salubri Embraces. This third eye is not always recognizably human in origin, and rumors persist of vertical, serpentine pupils, or even wormlike eyespots. While this third eye can be physically covered, such as with a headscarf or hood, it is always present, and no supernatural power can obscure it.Any time a Salubri activates a Discipline power, the third eye weeps vitae, its intensity correlating to the level of the Discipline being used, from welling up to a torrential flow.The blood flow from the third eye triggers a Hunger Frenzy test from nearby vampires with Hunger 4 or more.",
                        Compulsion = "When a Salubri suffers a Compulsion, the Kindred becomes overwhelmed with empathy for a personal problem that afflicts someone in the scene, seeking to further its resolution.The scale of the personal problem isn’t important; the Salubri understands that sometimes suffering is part of a cumulative situation and not an isolated stimulus. Any action not taken toward mitigating that personal tragedy incurs a two-dice penalty. The Compulsion persists until the sufferer’s burden is eased or a more immediate crisis supersedes it, or the end of the scene.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Companion")).Id
                    },
                    new Clan(){ Name = "Ravnos",
                        Bane = "The Ravnos are doomed. The sun’s fire that incinerated their founder rages through the Blood of the clan, erupting from their very flesh if they ever settle down for long.If they slumber in the same place more than once in seven nights, roll a number of dice equal to their Bane Severity.They receive aggravated damage equal to the number of 10’s (critical results) rolled as they are scorched from within.This happens every time they spend the day in a location they’ve already slumbered less than a week before.What constitutes a location in this regard depends on the scope of the chronicle, but unless otherwise stated, two resting places need to be at least a mile apart to avoid triggering the Bane. Furthermore, a mobile haven, such as a movers’ truck, is safe so long as the place where the truck is parked is at least a mile from the last location. Ravnos characters cannot take the No Haven Flaw at character creation.",
                        Compulsion = "The vampire is driven by their Blood to court danger. Haunted as they are by righteous fire burning its way up their lineage, why not? The next time the vampire is faced with a problem to solve, any attempt at a solution short of the most daring or dangerous incurs a two-dice penalty. (Suitably flashy and risky attempts can even merit bonus dice for this occasion.) The Daredevil is free to convince any fellows to do things their way, but is just as likely to go at it alone.The Compulsion persists until the problem is solved or further attempts become impossible.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Companion")).Id
                    },
                    new Clan(){ Name = "Baali",
                        Bane = "?",
                        Compulsion = "?",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("N/A")).Id
                    },

                };

                await _context.AddRangeAsync(clans);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Clans.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedBloodPotency()
        {
            if (_context.BloodPotencies.Any()) return;
            try
            {
                IList<BloodPotency> bloodpotencies = new List<BloodPotency>() {
                     new BloodPotency()
                     {
                         Level = 0,
                         BloodSurge = 1,
                         DamageMendedPerRouse = 1,
                         DisciplinePowerBonues = 0,
                         DisciplineRouseCheckReroll = "None.",
                         BaneSeverity = 0,
                         FeedingPenalty = "No effect."
                     },
                     new BloodPotency()
                     {
                         Level = 1,
                         BloodSurge = 2,
                         DamageMendedPerRouse = 1,
                         DisciplinePowerBonues = 0,
                         DisciplineRouseCheckReroll = "Level 1.",
                         BaneSeverity = 2,
                         FeedingPenalty = "No effect."
                     },
                     new BloodPotency()
                     {
                         Level = 2,
                         BloodSurge = 2,
                         DamageMendedPerRouse = 2,
                         DisciplinePowerBonues = 1,
                         DisciplineRouseCheckReroll = "Level 1.",
                         BaneSeverity = 2,
                         FeedingPenalty = "Animal and bagged blood slakes half Hunger."
                     },
                     new BloodPotency()
                     {
                         Level = 3,
                         BloodSurge = 3,
                         DamageMendedPerRouse = 2,
                         DisciplineRouseCheckReroll = "Level 2 and below.",
                         DisciplinePowerBonues = 1,
                         BaneSeverity = 3,
                         FeedingPenalty = "Animal and bagged blood slakes half Hunger."
                     },
                    new BloodPotency()
                     {
                         Level = 4,
                         BloodSurge = 3,
                         DamageMendedPerRouse = 3,
                         DisciplinePowerBonues = 2,
                         DisciplineRouseCheckReroll = "Level 2 and below.",
                         BaneSeverity = 3,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 1 less Hunger per human."
                     },
                    new BloodPotency()
                     {
                         Level = 5,
                         BloodSurge = 4,
                         DamageMendedPerRouse = 3,
                         DisciplinePowerBonues = 2,
                         DisciplineRouseCheckReroll = "Level 3 and below.",
                         BaneSeverity = 4,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 1 less Hunger per human. Must drain and kill a human to reduce Hunger below 2."
                     },
                     new BloodPotency()
                     {
                         Level = 6,
                         BloodSurge = 4,
                         DamageMendedPerRouse = 3,
                         DisciplinePowerBonues = 3,
                         DisciplineRouseCheckReroll = "Level 3 and below.",
                         BaneSeverity = 4,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 2 less Hunger per human. Must drain and kill a human to reduce Hunger below 2."
                     },
                     new BloodPotency()
                     {
                         Level = 7,
                         BloodSurge = 5,
                         DamageMendedPerRouse = 3,
                         DisciplinePowerBonues = 3,
                         DisciplineRouseCheckReroll = "Level 4 and below.",
                         BaneSeverity = 5,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 2 less Hunger per human. Must drain and kill a human to reduce Hunger below 2."
                     },
                     new BloodPotency()
                     {
                         Level = 8,
                         BloodSurge = 5,
                         DamageMendedPerRouse = 4,
                         DisciplinePowerBonues = 4,
                         DisciplineRouseCheckReroll = "Level 4 and below.",
                         BaneSeverity = 5,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 2 less Hunger per human. Must drain and kill a human to reduce Hunger below 3."
                     },
                     new BloodPotency()
                     {
                         Level = 9,
                         BloodSurge = 6,
                         DamageMendedPerRouse = 4,
                         DisciplinePowerBonues = 4,
                         DisciplineRouseCheckReroll = "Level 5 and below.",
                         BaneSeverity = 6,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 2 less Hunger per human. Must drain and kill a human to reduce Hunger below 3."
                     },
                     new BloodPotency()
                     {
                         Level = 10,
                         BloodSurge = 6,
                         DamageMendedPerRouse = 5,
                         DisciplinePowerBonues = 5,
                         DisciplineRouseCheckReroll = "Level 5 and below.",
                         BaneSeverity = 6,
                         FeedingPenalty = "Animal and bagged blood slake no Hunger. Slake 2 less Hunger per human. Must drain and kill a human to reduce Hunger below 3."
                     }
                };

                await _context.AddRangeAsync(bloodpotencies);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding BloodPotency.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedDisciplines()
        {
            if (_context.Disciplines.Any()) return;
            try
            {
                IList<Discipline> disciplines = new List<Discipline>() {
                     new Discipline(){ Name = "Animalism"},
                     new Discipline(){ Name = "Auspex"},
                     new Discipline(){ Name = "Celerity"},
                     new Discipline(){ Name = "Dominate"},
                     new Discipline(){ Name = "Fortitude"},
                     new Discipline(){ Name = "Obfuscate"},
                     new Discipline(){ Name = "Potence"},
                     new Discipline(){ Name = "Presence"},
                     new Discipline(){ Name = "Protean"},
                     new Discipline(){ Name = "Blood Sorcery"},
                     new Discipline(){ Name = "Oblivion"},
                     new Discipline(){ Name = "Akhu Sorcery"},
                };

                await _context.AddRangeAsync(disciplines);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Disciples.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedDisciplinePower()
        {
            if (_context.DisciplinePowers.Any()) return;
            try
            {
                IList<DisciplinePower> powers = new List<DisciplinePower>() {
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 1,
                         RouseCost = 0,
                         AdditionalCost = null,
                         DisciplinePowerName = "Heightened Senses",
                         DisciplinePowerDescription = "The vampire’s senses sharpen to a preternatural degree, giving them the ability to see in pitch darkness, hear ultrasonic frequencies and smell the fear of cowering prey.",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,                                 
                         RollDescription = "Wits + Resolve",
                         CounterRollDescription = null,
                         System = "The user adds their Auspex rating to all perception rolls. If exposed to extreme sensations, such as loud bangs, flashes of intense light or overpowering smells while the power is active the user must succeed on a Wits + Resolve (Difficulty 3 or more) roll to dampen their senses in time, or the overload causes them to sustain a -3 dice penalty to all perception - based rolls for the rest of the scene.",
                         Duration = "Until deactivated. Having the power active for longer stretches of time without rest(more than a scene), especially so for high - stimulus environments, might necessitate spending Willpower, at the Storyteller’s discretion.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 1,
                         RouseCost = 0,
                         AdditionalCost = null,
                         DisciplinePowerName = "Sense the Unseen",
                         DisciplinePowerDescription = "The senses of the vampire become attuned to dimensions beyond the mundane, allowing them to sense presences otherwise hidden from the naked eye. This can be anything from another vampire using Obfuscate to someone using Auspex to spy upon the character to a ghost in the middle of the room.Dormant Blood Sorcery spells and rituals might also be found with this power, at the Storyteller’s discretion.",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = "Wits + Auspex or Resolve + Auspex",
                         CounterRollDescription = "Wits + Obfuscate",
                         System = "Whenever there’s something supernatural hiding in plain sight, the Storyteller makes a hidden roll of Wits + Auspex against a Difficulty they choose. Against an entity actively trying to stay hidden, the Storyteller can call for a blind roll(“Lisa, roll seven dice for me”) as a contest against the target’s relevant pool. (For example, detecting a vampire using Obfuscate would be a roll of Wits + Auspex vs Wits + Obfuscate) If the vampire actively searches for a hidden supernatural entity, they roll Resolve + Auspex similarly.",
                         Duration = "Passive.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 2,
                         RouseCost = 0,
                         AdditionalCost = "One rouse check if you actively provoke a Premonition.",
                         DisciplinePowerName = "Premonition",
                         DisciplinePowerDescription = "The vampire experiences flashes of insight. These may take the form of raised hackles, sudden inspiration or even vivid visions. While never too precise, these visions can nudge the vampire out of harm’s way or reveal a truth previously overlooked.",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = "Resolve + Auspex",
                         CounterRollDescription = null,
                         System = "Whenever the Storyteller deems it appropriate, this power gives the character a sudden hint that aids them in some way: letting them find a clue they’ve missed or saving them from danger.Whether it gives the character a sudden vision of themselves walking into a trap, an inviting red glow over the second right turn during a chase, or the brief flash of a skeleton beneath the floorboards in the Prince’s office, this power always gives the Storyteller license to subtly speed up play or move the story onto a desired track.The suggested limit is one premonition per scene, even if more than one character has Premonition. ■ The user can also actively provoke a premonition by focusing on a subject, making a Rouse Check and rolling Resolve + Auspex.The number of successes rolled determines the level of insight on the subject, if any.",
                         Duration = "Passive.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 3,
                         RouseCost = 1,
                         AdditionalCost = null,
                         DisciplinePowerName = "Scry the Soul",
                         DisciplinePowerDescription = "By focusing on a person, the vampire can perceive the state of that person’s psyche as a shifting aura of colors. Auras reveal little precise information, but do provide clues regarding many subjects, e.g., emotional state, Resonance, and supernatural traits. If looking for a specific condition, the vampire can cursorily scan the crowd to detect it.Such cursory scans provide no further information",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = "Intelligence + Auspex",
                         CounterRollDescription = "Composure + Subterfuge",
                         System = "Make an Intelligence + Auspex vs Composure + Subterfuge roll. On a win, the Storyteller truthfully answers a number of questions equal to the margin of the roll about the target’s aura and psyche, including: ◻ The emotional state of the subject ◻ The Resonance in the subject’s blood ◻ Whether the subject is a vampire, werewolf, ghoul or any other supernatural being ◻ Whether the subject is under the influence of Blood Sorcery or other magic ◻ Whether the subject has committed diablerie in the last year ◻ A critical win allows discovery of something unexpected, as determined by the Storyteller If scanning a crowd, roll versus a Difficulty determined by the size of the crowd and external distractions, as well as the type of trait being sought. (Finding the vampire in the living room might only be a Difficulty 3, while finding the most nervous person at a crowded rave is most likely Difficulty 6 or higher.)",
                         Duration = "One turn, or Storyteller’s discretion.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 3,
                         RouseCost = 1,
                         AdditionalCost = null,
                         DisciplinePowerName = "Share the Senses",
                         DisciplinePowerDescription = "By reaching out with their mind, the vampire can tap into the senses of another mortal or vampire, seeing, hearing, and feeling what they do.The user still retains their own perceptions and is still aware of their own surroundings, though the effect requires some getting used to. The user decides whether to tap into only one, some, or all of the target’s senses. When used on a stranger this power requires line of sight to initiate.However, it can be used over longer distances on someone who still has some of the user’s Blood in their body.",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = "Resolve + Auspex",
                         CounterRollDescription = null,
                         System = " Roll Resolve + Auspex at Difficulty 3. This Difficulty can go up depending on distraction, distance, and other factors, such as the amount of the user’s Blood that remains in the target. The target usually remains unaware of the intrusion, but Sense the Unseen can allow the passenger to be noticed. To get rid of an unwanted rider, the victim must beat the intruder at a Wits + Resolve vs Wits + Resolve roll. An Auspex user thrown out  this way cannot make another Sharing attempt until the next night.",
                         Duration = "One scene.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 4,
                         RouseCost = 1,
                         AdditionalCost = null,
                         DisciplinePowerName = "Spirit's Touch",
                         DisciplinePowerDescription = "By touching an inanimate object or the ground at a location, the vampire can sense the emotional residue left by those who have handled that object or visited the location in the past. The user gains insight into not only that person, but also what was done and under what circumstances. While rarely crystal clear, the information often provides leads impossible to gain from regular forensics and deduction",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = " Intelligence + Auspex",
                         CounterRollDescription = null,
                         System = " Make an Intelligence + Auspex roll versus a Difficulty depending on the information sought. Gleaning the emotional state of the user of a murder weapon used a few days before is Difficulty 3, but sensing the surroundings in which a 300-year old letter was written approaches Difficulty 6 or higher. Each point of margin on the roll allows the user to sense roughly one additional previous handler and set of circumstances, counting backwards from the most recent.",
                         Duration = "One turn.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 5,
                         RouseCost = 1,
                         AdditionalCost = null,
                         DisciplinePowerName = "Clairvoyance",
                         DisciplinePowerDescription = "By closing their eyes and entering a light trance, the vampire becomes master of its surroundings. In a few minutes it can gather information from roughly a city-block sized area (more if outdoors or less populated) that would normally take many hours, perhaps days of legwork and investigation.Once connected in such a way to their surroundings the vampire can also receive information on anything happening out of the ordinary in the area.",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = " Intelligence + Auspex",
                         CounterRollDescription = null,
                         System = "Roll Intelligence + Auspex against a Difficulty based on the security and level of activity of the area. Using Clairvoyance on one’s own mansion would be Difficulty 3 while an unfamiliar city block in the slums of a major city would amount to 7 or more. The user adds their base Haven rating in extra dice to the pool when using Clairvoyance on their own haven. The Storyteller answers the vampire’s questions about the comings and goings in the area, what people have seen and heard, topics of local gossip, recent major shocks or impressions, and so forth.The player can ask roughly one question per point of margin; answers about deliberately concealed information might consume more than one point.A critical win reveals something major, regardless of the questions asked, assuming there is something to reveal. The vampire can also clairvoyantly monitor events in progress, though this requires them to remain in the area for as long as the effect is active.",
                         Duration = "A few minutes for information gathering, up to a night for vigilance."
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 5,
                         RouseCost = 2,
                         AdditionalCost = null,
                         DisciplinePowerName = "Possession",
                         DisciplinePowerDescription = "With this power the vampire can strip the will of a mortal and completely possess their body, using it as their own.While the mind of the subject remains hidden to the vampire, they can do anything and go anywhere the subject could while the power remains active.Using this, a vampire can even experience the sunlight, food, and physical sexuality long denied them, their host paying the price for whatever abuse the vampire wreaks on their body while riding it.",
                         AmalgamId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Dominate")).Id,
                         AmalgramLevel = 3,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = "Resolve + Auspex",
                         CounterRollDescription = "Resolve + Intelligence",
                         System = "This power can only be used on mortals. If the mortal is a ghoul, they must first be Blood Bound to the user. Before possession can begin, the vampire must have eye contact with their victim (See Dominate, p.254). The user then engages in a Resolve + Auspex vs Resolve + Intelligence conflict with the victim in order to inhabit their body. If the vampire’s player rolls a total failure, the victim becomes immune to further Possession attempts for the duration of the story. Once the vampire inhabits the body of their victim, their own body falls into a torpor - like trance, completely unaware of their surrounding and their own physical state except for Aggravated damage, which breaks the trance and ends the effects. A vampire possessing a mortal can use Auspex, Presence, and Dominate through them.If the user wishes to extend Possession into daytime, they must make a roll to stay awake(p. 219). Failure to stay awake ends the power.Any Aggravated damage to the subject also risks ending the possession – the user must succeed at a Resolve + Auspex roll(Difficulty 2 + damage taken) to stay in control.If the subject dies during Possession, the resulting spiritual trauma immediately causes the user to sustain three levels of Aggravated damage to Willpower. This power does not give the user the ability to read the mind, use the skills, or emulate the manners of the victim.Any skills employed use the possessing vampire’s rating.The user must make a Manipulation + Performance vs Wits + Insight roll to successfully impersonate the victim’s manners, expressions, and the like. Finally, Possession violates the victim even more profoundly than a Blood Bond. The Storyteller should consider awarding Stains for this action.",
                         Duration = "Until ended, voluntarily or involuntarily.",
                     },
                     new DisciplinePower()
                     {
                         DisciplineId = _context.Disciplines.FirstOrDefault(d => d.Name.Equals("Auspex")).Id,
                         DisciplineLevel = 5,
                         RouseCost = 1,
                         AdditionalCost = "One Willpower vs a non-consenting vampire.",
                         DisciplinePowerName = "Telepathy",
                         DisciplinePowerDescription = "At the highest levels of Auspex the vampire can now literally read minds, as well as project their own thoughts into the minds of others. While reading a mortal mind is relatively straightforward, undead minds requires a higher effort to penetrate.",
                         AmalgamId = null,
                         AmalgramLevel = 0,
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id,
                         RollDescription = "Resolve + Auspex",
                         CounterRollDescription = "Wits + Subterfuge",
                         System = "The user is not required to roll any dice to project their thoughts to another, vampire or mortal, though they do require line of sight.To read the mind of a mortal within line of sight, roll Resolve +Auspex vs Wits +Subterfuge while looking into their eyes. (Unless the mortal consents, in which case no roll is required.) A win means that the user can discern surface thoughts as a stream of images, with higher margin allowing the user to probe for more distant or buried memories.A critical win gives a coherent picture of the subject’s current thoughts and intentions.To read the mind of a non - consenting vampire, spend one Willpower point before rolling.",
                         Duration = "Roughly one minute per Rouse Check. Increased to a full scene on consenting subjects.",
                     }

                };

                await _context.AddRangeAsync(powers);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding DisciplinePowers.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedSkills()
        {
            if (_context.Skills.Any()) return;
            try
            {
                IList<Skill> skills = new List<Skill>() {
                    new Skill(){SkillName = "Atheletics", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Brawl", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Craft", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Drive", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Firearms", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Larceny", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Melee", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Stealth", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Survival", SkillType = SkillType.Physical, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },

                    new Skill(){SkillName = "Animal Ken", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Etiquette", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Insight", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Intimidation", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Leadership", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Performance", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Persuasion", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Streetwise", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Subterfuge", SkillType = SkillType.Social, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },

                    new Skill(){SkillName = "Academics", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Awareness", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Finance", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Investigation", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Medicine", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Occult", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Politics", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Science", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id },
                    new Skill(){SkillName = "Technology", SkillType = SkillType.Mental, BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id }
                };

                await _context.AddRangeAsync(skills);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Skills.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedChronicle()
        {
            if (_context.Chronicles.Any()) return;
            try
            {
                Chronicle chronicle = new Chronicle() { Name = "Test By Night.", StoryTellerId = _context.Users.FirstOrDefault(u => u.UserName == "StoryTeller@gmail.com").Id };

                await _context.AddAsync(chronicle);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Chronicle.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedPredatorTypes()
        {
            if (_context.PredatorTypes.Any()) return;
            try
            {
                IList<PredatorType> predatorTypes = new List<PredatorType>() {
                     new PredatorType()
                     {
                         Name = "Alleycat",
                         Description = "A combative assault-feeder, you stalk, overpower, and drink from whomever you can, when you can. You may or may not attempt to threaten or Dominate victims into silence or mask the feeding as a robbery. Think about how you arrived at this direct approach to feeding and what makes you comfortable with an unlife of stalking, attacking, feeding, and escaping. You could have been homeless, an SAS soldier, a cartel hit-man, or a big-game hunter. ■ Add a specialty: Intimidation (Stickups) or Brawl(Grappling) ■ Gain one dot of Celerity or Potence ■ Lose one dot of Humanity ■ Gain three dots of criminal Contacts",
                         HuntingRole = " Strength + Brawl: You take blood by force or threat, stalking, overpowering, and bleedings your victims. If you feed on criminals as a sort of dark knight of the streets, use Wits + Streetwise to find a victim.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Bagger",
                         Description = "You steal, buy, or otherwise procure cold blood rather than hunt, relying on the black market or your skills as a burglar or ambulance chaser. Perhaps you still work the night shift at the hospital. Ventrue may not pick this Predator type. ■ Add a specialty: Larceny(Lockpicking) or Streetwise(Black Market) ■ Gain one dot of Blood Sorcery (Tremere only) or Obfuscate ■ Gain the Feeding Merit: Iron Gullet(•••) ■ Gain the Enemy Flaw: (••)Either someone believes you owe them, or there’s another reason you keep off the streets.",
                         HuntingRole = "Intelligence + Streetwise: You acquire preserved blood rather than hunt, or you feed from the dead or dying. Find your prize, gain access, and purchase or otherwise convince someone with the goods to give you access.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Blood Leech",
                         Description = "You drink from other vampires, either by hunting, coercion or by taking Blood as payment - the only truly moral way of feeding you can think of. Unfortunately, this practice is usually forbidden in Kindred society. It is either risky as all fuck or requires a position of enviable power. ■ Add a specialty: Brawl(Kindred) or Stealth(against Kindred) ■ Gain one dot of Celerity or Protean ■ Lose one dot of Humanity ■ Increase Blood Potency by one ■ Gain the Dark Secret Flaw: (••) Diablerist, or the Shunned Flaw: (••) ■ Gain the Feeding Flaw: (••) Prey Exclusion(mortals)",
                         HuntingRole = " You feed from other vampires; if you make a mistake, you die – either tonight, or in a blood hunt. The Storyteller should not abstract something like this to a set of die rolls.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Cleaver",
                         Description = "You feed covertly from your (or someone’s) mortal family and friends with whom you still maintain ties.The most extreme cleavers adopt children, marry a human, and try to maintain a family life for as long as they can.Add your family to the Relationship Map. Cleavers often go to great lengths to keep the truth of their condition from their family, but some also maintain unwholesome relationships with their own kin. The Camarilla forbids taking a human family in this fashion, and it frowns on cleavers as Masquerade breaches waiting to happen.Wiser Kindred may massacre your family for your own good if they find out your secret and care what happens to you. ■ Add a specialty: Persuasion(Gaslighting) or Subterfuge(Coverups) ■ Gain one dot of Dominate or Animalism ■ Gain the Dark Secret Flaw: (•)Cleaver ■ Gain the Herd Advantage(••)",
                         HuntingRole = " Manipulation + Subterfuge: You take blood covertly from your mortal family or friends. Socialize with your victims, feed from them, and cover it up to groom them for next time.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Consensualist",
                         Description = "You never feed against your victim’s free will. You masquerade as a representative of a charity blood drive, as a blood-drinking kink-lord in the “real vampire community,” or by actually telling your victims what you are and getting their permission to feed. The Camarilla call that last method a Masquerade breach, but many Anarch philosophers consider it an acceptable risk. You could have been anything in life, but a sex-worker, a political organizer, or a lawyer could all be wary of feeding without consent. ■ Add a specialty: Medicine(Phlebotomy) or Persuasion(Vessels) ■ Gain one dot of Auspex or Fortitude ■ Gain one dot of Humanity ■ Gain the Dark Secret Flaw: (•) Masquerade Breacher ■ Gain the Feeding Flaw: (•) Prey Exclusion(nonconsenting)",
                         HuntingRole = " Manipulation + Persuasion: You take blood by consent, under cover of medical work or a shared kink. Cultivate your victims, feed from them, and validate their choice to feed you.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Farmer",
                         Description = "You only feed from animals. Your Hunger constantly gnaws at you, but you have not killed a single human being so far (except perhaps that one time), and you intend to keep it that way. You could have been anyone in life, but your choice speaks to someone obsessed by morality. Perhaps you were an activist, priest, aidworker, or vegan in life, but the choice never to risk a human life is one anyone could arrive at and struggle to maintain. Ventrue may not pick this Predator type. You cannot pick this Predator type if your Blood Potency is 3 or higher. ■ Add a specialty: Animal Ken(Specific Animal) or Survival(Hunting) ■ Gain one dot of Animalism or Protean ■ Gain one dot of Humanity ■ Gain the Feeding Flaw: (••) Farmer",
                         HuntingRole = "Composure + Animal Ken: You feed from animals. Find your quarry, catch your chosen animal, and feed from it.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Osiris",
                         Description = "You are a celebrity among mortals or else you run a cult, a church, or something similar. You feed from your fans or worshippers, who treat you as a deity. You always have access to easy blood, but followers breed problems with the authorities, organized religion, and indeed the Camarilla. In life, you might have been a DJ, a writer, a cultist, a preacher, or a LARP organizer.■ Add a specialty: Occult(specific tradition) or Performance(specific entertainment field) ■ Gain one dot of Blood Sorcery(Tremere only) or Presence ■ Spend three dots between the Fame and Herd Backgrounds ■ Spend two dots between the Enemies and Mythic Flaws",
                         HuntingRole = "Manipulation + Subterfuge or Intimidation + Fame: You feed from your fans, church, or other adoring crowd. The Skill for which you’re famous may be Performance, Science, Craft, Academics, Politics, or something else.Display yourself, choose a victim, and flatter or bully them into feeding you.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Sandman",
                         Description = "You rely on your stealth or Disciplines to feed from sleeping victims. If they never wake during the feeding, they won’t know you exist. Perhaps you were very anti-social in life; you don’t feel cut out for the intense interpersonal nightlife or physical violence of more extroverted hunters. ■ Add a specialty: Medicine(Anesthetics) or Stealth (Break-in) ■ Gain one dot of Auspex or Obfuscate ■ Gain one dot of Resources",
                         HuntingRole = " Dexterity + Stealth: You feed from sleeping victims. Case a hotel or house, break in, feed silently and get out.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Scene Queen",
                         Description = "You rely on your familiarity with a certain subculture and a wellcrafted pose, feeding on an exclusive subculture that believes you to be one of them. Your victims adore you for your status in the scene, and the ones who understand what you are disbelieved. You may belong to the street or be literal upper -class, abusing the weak with false hope and promises of taking them to the next level.In life, you almost certainly belonged to a scene similar to the one you stalk now. ■ Add a specialty: Etiquette (specific scene), Leadership (specific scene), or Streetwise (specific scene) ■ Gain one dot of Dominate or Potence ■ Gain the Fame Advantage: (•) ■ Gain the Contact Advantage: (•) ■ Gain either the Influence Flaw: (•) Disliked(outside your subculture) or the Feeding Flaw: (•) Prey Exclusion(a different subculture from yours)",
                         HuntingRole = "Manipulation + Persuasion: You feed from a highor low-class subculture in which you enjoy high status.Make the scene, groom and isolate a victim from whom to feed, and gaslight or silence them to keep the scene cool. \"I’ll let the others see us together if you keep it together.\"",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Siren",
                         Description = "You feed almost exclusively during or while feigning sex, and you rely on your Disciplines, seduction skills, or the unquenchable appetites of others to conceal your carnivorous nature. You have mastered the art of the onenight stand or move through the sex - club scene like a dark star. You think of yourself as a sexy beast, but in your darkest moments, you fear that you're at best a problematic lover, at worst a habitual rapist.A former lover who escaped destruction might be your Touchstone or your stalker. (If so, add them to the Relationship Map.) Maybe in life you were a pick - up artist, movie producer, author, a glorious slutty kinkster– or a virgin who intends to make up for lost time post - mortem. ■ Add a specialty: Persuasion (Seduction) or Subterfuge (Seduction) ■ Gain one dot of Fortitude or Presence ■ Gain the Looks Merit: (••) Beautiful ■ Gain the Enemy Flaw: (•) A spurned lover or jealous partner ■",
                         HuntingRole = "Charisma + Subterfuge: You feed under the guise of sex. Pick up your victim, charm them, and take them somewhere alone to feed.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Extortionist",
                         Description = "The extortionist likes to force their victims to bleed for them. Ostensibly, the extortionist acquires blood in exchange for services such as security or surveillance, but as many times as the need for protection is real, it is just as often a fiction engineered to make the deal feel acceptable. • Add a specialty: Intimidation (Coercion) or Larceny (Security) • Gain one dot of Dominate or Potence • Spend three dots between the Contacts and Resources Backgrounds • Gain the Enemy Flaw: (••) The police or a victim who escaped your extortion and now wants revenge",
                         HuntingRole =  " Strength or Manipulation + Intimidation, you feed through coercion both subtle and painfully obvious.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Cult of the Blood Gods")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Graverobber",
                         Description = "Graverobbers often feed from fresh corpses, but despite their name, they prefer feeding from mourners in cemeteries and sad, frightened visitors and patients in hospitals. Melancholic Resonance in a victim’s blood appeals more than any other humour. This predator type often requires the vampire to hold a haven in or connections to a church, hospital, or morgue. • Add a specialty: Occult(Grave Rituals) or Medicine(Cadavers) • Gain one dot of Fortitude or Oblivion • Gain the Feeding Merit: (•••) Iron Gullet • Gain the Haven Advantage: (•) • Gain the Herd Flaw: (••) Obvious Predator(your cold nature makes you act in a deeply unsettling matter when hunting)",
                         HuntingRole =  "Resolve + Medicine, sifting through the quiet dead for a body bearing rancid blood.Moving among miserable mortals for a vulnerable bite uses Manipulation + Insight.A cold corpse can slake up to 3 Hunger, but suffers the same slake penalties as bagged blood.A body fed from soon before death, drained of blood afterwards, or missing parts will slake less.",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Cult of the Blood Gods")).Id
                     },
                     new PredatorType()
                     {
                         Name = "Importuanate Charmer",
                         Description = "The pushy person likes to convince their victims by nagging and persuade them to give up their blood. Often lying about what they get in return or just make it sound there is something in it for them. ■ Add a specialty: Persuasion(importunate) ■ Gain one dot of Dominate ■ Spend three dots between the Contacts and Resources Backgrounds. ■ Gain the Enemy Flaw: (••) The police or a victim who realises how bad the choice was afterwards now wants revenge.",
                         HuntingRole = "Manipulation + Persuasion",
                         BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("N/A")).Id
                     }
                };

                await _context.AddRangeAsync(predatorTypes);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding PredatorTypes.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedLoreSheets()
        {
            if (_context.LoreSheets.Any()) return;
            try
            {
                
                IList<LoreSheet> loreSheets = new List<LoreSheet>() {
                    new LoreSheet()
                    {
                        Name = "The Cobweb",
                        Description = "You’re never truly alone. Not anymore. Not even if you want to be.Not even if you try to be. The Cobweb catches so many thoughts in its sticky strands, sends them skittering further inside, or reverberating out to the far edges. You’re not always sure the thoughts you’re hearing are from now.Some feel like they’ve been stuck for years and have just shaken loose. Others taste like tomorrow. The Cobweb, also known as the Madness Network, is a psychic network to which all Malkavians are linked. No two clan members describe it exactly the same way.For some, it’s a constant low murmuring in their Blood.Others describe it as a kind of hivemind operating alongside their own cognition.Many — even most  — Malkavians are only reminded of its presence a few times a year, like a sudden burst of static on a forgotten radio. No one knows the Cobweb’s ultimate purpose, though it’s been used to summon a gathering of Oracles together with a subconscious imperative dubbed The Call.Some suspect Malkav himself uses the Network to view the world through his descendants’ eyes and ears, or that he simply is the Network.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Chicago by Night")).Id
                    },
                    new LoreSheet()
                    {
                        Name = "Descendant of Vasantasena",
                        Description = "Vasantasena and her Malkavian sire Unmada traveled the world throughout the Middle Ages, preaching against the Blood Bond. They rejected the vinculum, traditional Kindred hierarchy, and all loss of free will. Ultimately, they condemned the Antediluvians for their cruel tyranny through Jyhad, joining the Sabbat during its formation. As the Camarilla had, the Sabbat rejected her cause, instituting both hierarchy and ritual enslavement through vitae.She eventually rejected them in turn, assembling a faction of Malkavians to embrace freedom from the Sabbat. Vasantasena’s descendants are many and varied, and all of them followed her from the Sabbat before the sect’s recent, bestial devolution.Some joined the Camarilla, others the Anarchs.All possess the zeal and charm she wields like a knife.All fight for something.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    }
                };

                await _context.AddRangeAsync(loreSheets);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding LoreSheets.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedLoreSheetParts()
        {
            if (_context.LoreSheetParts.Any()) return;
            try
            {
                IList<LoreSheetPart> loreSheetParts = new List<LoreSheetPart>() {
                     new LoreSheetPart()
                     {
                         LoreSheetId = _context.LoreSheets.FirstOrDefault(l => l.Name.Equals("The Cobweb")).Id,
                         Name = "A Break in the Static",
                         Description = "The Cobweb is just barely perceptible to you. You catch sporadic snatches of conversation, often just a few distinct words or images.It’s enough to piece together an order or a call for aid, though you’re unable to respond.",
                         Level = 1
                     },
                     new LoreSheetPart()
                     {
                         LoreSheetId = _context.LoreSheets.FirstOrDefault(l => l.Name.Equals("The Cobweb")).Id,
                         Name = "Step into My Parlor",
                         Description = "You can communicate over the Network with other Malkavians nearby. These discussions are heavily abstract, limited to short phrases, simple images, and strong emotions.",
                         Level = 2
                     },
                     new LoreSheetPart()
                     {
                         LoreSheetId = _context.LoreSheets.FirstOrDefault(l => l.Name.Equals("The Cobweb")).Id,
                         Name = "Across the Web",
                         Description = "You can communicate over the Network with other Malkavians nearby. These discussions are heavily abstract, limited to short phrases, simple images, and strong emotions.",
                         Level = 3
                     },
                     new LoreSheetPart()
                     {
                         LoreSheetId = _context.LoreSheets.FirstOrDefault(l => l.Name.Equals("The Cobweb")).Id,
                         Name = "Pluch the Strands",
                         Description = "You can communicate over the Network with other Malkavians nearby. These discussions are heavily abstract, limited to short phrases, simple images, and strong emotions.",
                         Level = 4
                     },
                     new LoreSheetPart()
                     {
                         LoreSheetId = _context.LoreSheets.FirstOrDefault(l => l.Name.Equals("The Cobweb")).Id,
                         Name = "Malkav's Will",
                         Description = " The entity in the Cobweb is awake and aware. They know your name and tell you their secrets. They have a plan and want you to help carry it out. Malkav — or a consciousness pretending to be him — speaks to you directly via the Network. Once per story, you may ask the Storyteller to divulge a secret about another Malkavian or reveal what orders the mind in the Cobweb wants you to follow.",
                         Level = 5
                     },
                     new LoreSheetPart()
                     {
                         LoreSheetId = _context.LoreSheets.FirstOrDefault(l => l.Name.Equals("Descendant of Vasantasena")).Id,
                         Name = "Agent of Chaos",
                         Description = " You thrive while everything around you burns or spins into catastrophe. In turbulent situations, such as an unusually chaotic and unpredictable combat, a car chase through a crowded city, or when fleeing an exploding building, once per session, you may re-roll a single die without spending Willpower.",
                         Level = 1
                     },

                };

                await _context.AddRangeAsync(loreSheetParts);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding LoreSheetParts.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedRituals()
        {
            if (_context.Rituals.Any()) return;
            try
            {
                IList<Ritual> rituals = new List<Ritual>() {
                    new Ritual()
                    {
                        Name = "Wake with evening's freshness",
                        Description = "Performed before dawn, this ritual allows the caster to awake at any sign of danger, fully alert as if awake during the night.",
                        RitualLevel = 1,
                        Ingredients = "The burnt bones and feathers of a rooster.",
                        Process = "The caster mixes the ashes with their own Blood, drawing a circle with the mixture around their place of sleep.",
                        System = "Do not make a Ritual roll unless true danger appears. If the caster is threatened during the day, make the Ritual roll then, with the caster rousing on a win. For the duration of the scene the vampire ignores the daytime penalties for staying awake. On a critical win, the effects last until the following dawn.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Ritual()
                    {
                        Name = "Communicate with kindred Sire",
                        Description = "The caster uses the bond between sire and childe to open a bridge between minds, allowing the childe to create a telepathic link for the purpose of long-distance communication. As with some other Rituals, this one sees a resurgence in these nights of wiretaps and electronic surveillance.",
                        RitualLevel = 2,
                        Ingredients = "An object previously possessed by the sire and a silver bowl filled with clear water.",
                        Process = "The caster submerges the object in water and lets their Blood drip into the bowl, concentrating upon the last memory of their sire for up to 30 minutes.",
                        System = "Make the ritual roll after 15 minutes have passed. A win allows for ten minutes of two-way silent mental communication once 15 more minutes have passed. A critical win allows immediate communication.Any major disturbance on either end breaks the connection.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new Ritual()
                    {
                        Name = "Deflection of wooden doom",
                        Description = "By performing this ritual the vampire protects themselves from being staked. The first stake that would pierce their heart shatters before penetrating the skin.",
                        RitualLevel = 3,
                        Ingredients = "Wood splinters or shavings.",
                        Process = "Wood splinters or shavings.",
                        System = "Make no Ritual roll until the vampire is staked. If the Ritual roll is a win, the stake shatters as it touches the skin of the vampire. (A critical win blinds the attacker for two turns, showering their face with splinters.) This only works on genuine attempts at staking - merely holding the stake against the vampire does not trigger the effect.The protection lasts until the end of the night or until the splinter is removed from under the tongue of the vampire, whichever comes first.",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    }

                };

                await _context.AddRangeAsync(rituals);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Rituals.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

        private async Task SeedThinBloodAlchemy()
        {
            if (_context.ThinBloodAlchemies.Any()) return;

            try
            {
                IList<ThinBloodAlchemy> thinBloodAlachemies = new List<ThinBloodAlchemy>()
                {
                    new ThinBloodAlchemy(){
                        Name = "Far Reach",
                        Description = "This formula allows the alchemist to use their mind to grab, hold, and push objects or people without touching them. While few can employ enough mental force to actually cause direct harm, a clever applicant can find many ways to get an opponent into, or themselves out of, harm’s way.",
                        Ingredients = "The alchemist’s Blood, choleric human blood,melted nylon fibers or a grated refrigerator magnet or weird nootropics ordered off the internet.",
                        ActivationCost = "One Rouse Check.",
                        DicePools = "Resolve + Alchemy vs Strength + Athletics.",
                        System = " The alchemist can lift, push, or pull a physical object or person under 100 kg, within their sight and closer than 10 meters. The object moves swiftly, but not rapidly enough to injure a person with the blow; the object may break if it is fragile.The exception: knives or other small metal tools, which the alchemist can wield with a Resolve + Alchemy test, at a two - dice penalty because of the need for precision.A knife used this way does only one point of extra damage. Trying to move someone actively resisting requires a contest of Resolve + Alchemy vs Strength + Athletics.On a win, the alchemist can pull the victim within grabbing or clawing range, or throw them one meter for each point of margin on the contest, doing an equal amount of Superficial damage.They land prone.Keeping someone or something floating in mid - air requires a Resolve + Thin-Blood Alchemy(Difficulty 3) roll every turn.Fine manipulation(such as pulling the pin of a grenade) requires a Wits + Alchemy roll at a suitable Difficulty, as determined by the Storyteller.",
                        AlchemyLevel = 1,
                        Duration = "One turn unless held (see system).",
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new ThinBloodAlchemy(){
                        Name = "Haze",
                        Description = "This formula creates a field of mist that follows the user, rendering them more difficult to target with ranged weapons and concealing their identity.",
                        Ingredients = "In addition to the alchemist’s Blood and phlegmatic human blood, dry ice or cigar smoke or auto exhaust.",
                        ActivationCost = "One Rouse Check.",
                        Duration = "One scene or until voluntarily ended.",
                        System = "Upon activation a cloud of mist-like vapor surrounds the alchemist, masking their features and obscuring their silhouette. Anyone attempting to identify the user or hit them with ranged weapons suffers a two-dice penalty to their pool. The user can extend the cloud to encompass a group of up to five people by making another Rouse check",
                        AlchemyLevel = 1,
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },
                    new ThinBloodAlchemy(){
                        Name = "Envelop",
                        Description = "This formula creates a mist that clings to a victim, blinding it and (in the case of mortals) causing suffocation. ",
                        Ingredients = "The alchemist’s Blood, melancholic and phlegmatic human blood, potassium chlorate, smog or halon gas.",
                        ActivationCost = "One Rouse Check.",
                        DicePools = "Wits + Alchemy vs Stamina + Survival.",
                        System = "The alchemist activates the power and chooses a target within sight. A swirling mist envelops the target, obscuring their sight and penalizing them three dice from all sight-based detection and ranged attack dice pools. In addition, the alchemist can make the mist suffocate a mortal with a contest of Wits + Alchemy vs. Stamina + Survival. On a win, the target can take no action except coughing and choking; on a critical win, the target loses consciousness. The alchemist can only employ Envelop on single targets, and only on one at a time.",
                        Duration = " Until scene ends or the alchemist ends the effect voluntarily.",
                        AlchemyLevel = 2,
                        BookId = _context.Books.FirstOrDefault(b => b.Title.Equals("Core Rulebook")).Id
                    },


                };

                await _context.AddRangeAsync(thinBloodAlachemies);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Thin Blood Alchemies.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }
    }
}
