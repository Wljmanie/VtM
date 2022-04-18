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
        }

        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any()) return;
            foreach(var role in Enum.GetNames(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            if(_context.Users.Any()) return;

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
            if(_context.Books.Any()) return;
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
            if(_context.Clans.Any()) return;

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
    }
}
