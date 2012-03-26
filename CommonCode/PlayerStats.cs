namespace MMO3D.CommonCode
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Encapsulates a player's level and stats, and provides access to related constants.
    /// </summary>
    public sealed class PlayerStats
    {
        /// <summary>
        /// The minimum level possible.
        /// </summary>
        public const int MinLevel = 1;

        /// <summary>
        /// The maximum level possible.
        /// </summary>
        public const int MaxLevel = 30;

        /// <summary>
        /// The number of experience points needed to gain a level.
        /// </summary>
        public const int ExperiencePointsForLevel = 100000;

        /// <summary>
        /// The minimum base stat possible.
        /// </summary>
        public const int MinBaseStat = 36;

        /// <summary>
        /// The maximum base stat possible.
        /// </summary>
        public const int MaxBaseStat = 255;

        /// <summary>
        /// The minimum number of points gained in each stat when the player gains a level.
        /// </summary>
        public const int MinStatsGained = 4;

        /// <summary>
        /// The maximum number of points gained in each stat when the player gains a level.
        /// </summary>
        public const int MaxStatsGained = 10;

        /// <summary>
        /// The minimum percentage bonus possible.
        /// </summary>
        public const float MinPercentageBonus = 0;

        /// <summary>
        /// The maximum percentage bonus possible.
        /// </summary>
        public const float MaxPercentageBonus = 3;

        /// <summary>
        /// The minimum actual stat possible.
        /// </summary>
        public const int MinActualStat = 0;

        /// <summary>
        /// The maximum actual stat possible.
        /// </summary>
        public const int MaxActualStat = 999;

        /// <summary>
        /// The maximum actual hitpoints possible.
        /// </summary>
        public const int MaxActualHitPoints = 4000;

        /// <summary>
        /// The minimum point boost possible.
        /// </summary>
        public const int MinPointBoost = -PlayerStats.MaxActualHitPoints;

        /// <summary>
        /// The maximum point boost possible.
        /// </summary>
        public const int MaxPointBoost = PlayerStats.MaxActualHitPoints;

        /// <summary>
        /// The minimum number of hitpoints a player can have <strong>before</strong> dying.
        /// A number of hitpoints lesser than this value indicates that the player has died.
        /// </summary>
        public const int MinHitPoints = 0;

        /// <summary>
        /// The player's character class.
        /// </summary>
        private CharacterClass characterClass;

        /// <summary>
        /// The player's level.
        /// </summary>
        private int level;

        /// <summary>
        /// The number of experience points the player has.
        /// </summary>
        private int experiencePoints;

        /// <summary>
        /// The player's current hitpoints level.
        /// </summary>
        private int currentHitPoints;

        /// <summary>
        /// The player's base hitpoints stat.
        /// </summary>
        private int baseHitPoints;

        /// <summary>
        /// The player's base strength stat.
        /// </summary>
        private int baseStrength;

        /// <summary>
        /// The player's base skill stat.
        /// </summary>
        private int baseSkill;

        /// <summary>
        /// The player's base evade stat.
        /// </summary>
        private int baseEvade;

        /// <summary>
        /// The player's base magic stat.
        /// </summary>
        private int baseMagic;

        /// <summary>
        /// The player's base resistance stat.
        /// </summary>
        private int baseResistance;

        /// <summary>
        /// The player's base speed stat.
        /// </summary>
        private int baseSpeed;

        /// <summary>
        /// The bonus the player's hitpoints is multiplied by after the class modifier.
        /// </summary>
        private float hitPointsPercentageBonus;

        /// <summary>
        /// The bonus the player's strength is multiplied by after the class modifier.
        /// </summary>
        private float strengthPercentageBonus;

        /// <summary>
        /// The bonus the player's skill is multiplied by after the class modifier.
        /// </summary>
        private float skillPercentageBonus;

        /// <summary>
        /// The bonus the player's evade is multiplied by after the class modifier.
        /// </summary>
        private float evadePercentageBonus;

        /// <summary>
        /// The bonus the player's magic is multiplied by after the class modifier.
        /// </summary>
        private float magicPercentageBonus;

        /// <summary>
        /// The bonus the player's resistance is multiplied by after the class modifier.
        /// </summary>
        private float resistancePercentageBonus;

        /// <summary>
        /// The bonus the player's speed is multiplied by after the class modifier.
        /// </summary>
        private float speedPercentageBonus;

        /// <summary>
        /// The bonus added to the player's hitpoints after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int hitPointsPointBonus;

        /// <summary>
        /// The bonus added to the player's strength after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int strengthPointBonus;

        /// <summary>
        /// The bonus added to the player's skill after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int skillPointBonus;

        /// <summary>
        /// The bonus added to the player's evade after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int evadePointBonus;

        /// <summary>
        /// The bonus added to the player's magic after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int magicPointBonus;

        /// <summary>
        /// The bonus added to the player's resistance after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int resistancePointBonus;

        /// <summary>
        /// The bonus added to the player's speed after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        private int speedPointBonus;

        /// <summary>
        /// Initializes a new instance of the PlayerStats class.
        /// </summary>
        /// <param name="characterClass">The character class associated with these stats.</param>
        public PlayerStats(CharacterClass characterClass)
        {
            this.Class = characterClass;
            this.Level = PlayerStats.MinLevel;

            this.BaseHitPoints = PlayerStats.MinBaseStat;
            this.BaseStrength = PlayerStats.MinBaseStat;
            this.BaseSkill = PlayerStats.MinBaseStat;
            this.BaseEvade = PlayerStats.MinBaseStat;
            this.BaseMagic = PlayerStats.MinBaseStat;
            this.BaseResistance = PlayerStats.MinBaseStat;
            this.BaseSpeed = PlayerStats.MinBaseStat;

            this.HitPointsPercentageBonus = 1;
            this.StrengthPercentageBonus = 1;
            this.SkillPercentageBonus = 1;
            this.EvadePercentageBonus = 1;
            this.MagicPercentageBonus = 1;
            this.ResistancePercentageBonus = 1;
            this.SpeedPercentageBonus = 1;

            this.CurrentHitPoints = this.ActualHitPoints;
        }

        /// <summary>
        /// Gets or sets the player's character class.
        /// </summary>
        /// <value>See summary.</value>
        [Description("The player's character class.")]
        public CharacterClass Class
        {
            get
            {
                return this.characterClass;
            }

            set
            {
                // If we're setting this for the first time, don't throw a null ref exception
                if (this.characterClass == null)
                {
                    this.characterClass = value;
                }
                else
                {
                    this.SetHitPointsSafe<CharacterClass>(ref this.characterClass, value);
                }
            }
        }

        /// <summary>
        /// Gets the player's level.
        /// </summary>
        /// <value>See summary.</value>
        [Description("The player's level.")]
        public int Level
        {
            get { return this.level; }
            private set { this.level = (int)MathHelper.Clamp(value, PlayerStats.MinLevel, PlayerStats.MaxLevel); }
        }

        /// <summary>
        /// Gets or sets the number of experience points the player has.
        /// </summary>
        /// <value>See summary.</value>
        [Description("The number of experience points the player has.")]
        public int ExperiencePoints
        {
            get { return this.experiencePoints; }
            set { this.experiencePoints = (int)MathHelper.Clamp(value, 0, PlayerStats.ExperiencePointsForLevel); }
        }

        /// <summary>
        /// Gets or sets the player's base hitpoints stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base hitpoints stat.")]
        public int BaseHitPoints
        {
            get { return this.baseHitPoints; }
            set { this.SetHitPointsSafe<int>(ref this.baseHitPoints, (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level))); }
        }

        /// <summary>
        /// Gets or sets the player's base strength stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base strength stat.")]
        public int BaseStrength
        {
            get { return this.baseStrength; }
            set { this.baseStrength = (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level)); }
        }

        /// <summary>
        /// Gets or sets the player's base skill stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base skill stat.")]
        public int BaseSkill
        {
            get { return this.baseSkill; }
            set { this.baseSkill = (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level)); }
        }

        /// <summary>
        /// Gets or sets the player's base evade stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base evade stat.")]
        public int BaseEvade
        {
            get { return this.baseEvade; }
            set { this.baseEvade = (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level)); }
        }

        /// <summary>
        /// Gets or sets the player's base magic stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base magic stat.")]
        public int BaseMagic
        {
            get { return this.baseMagic; }
            set { this.baseMagic = (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level)); }
        }

        /// <summary>
        /// Gets or sets the player's base resistance stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base resistance stat.")]
        public int BaseResistance
        {
            get { return this.baseResistance; }
            set { this.baseResistance = (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level)); }
        }

        /// <summary>
        /// Gets or sets the player's base speed stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stats")]
        [Description("The player's base speed stat.")]
        public int BaseSpeed
        {
            get { return this.baseSpeed; }
            set { this.baseSpeed = (int)MathHelper.Clamp(value, PlayerStats.GetMinimumStat(this.Level), PlayerStats.GetMaximumStat(this.Level)); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's hitpoints is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's hitpoints is multiplied by after the class modifier.")]
        public float HitPointsPercentageBonus
        {
            get { return this.hitPointsPercentageBonus; }
            set { this.SetHitPointsSafe<float>(ref this.hitPointsPercentageBonus, MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus)); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's strength is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's strength is multiplied by after the class modifier.")]
        public float StrengthPercentageBonus
        {
            get { return this.strengthPercentageBonus; }
            set { this.strengthPercentageBonus = MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's skill is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's skill is multiplied by after the class modifier.")]
        public float SkillPercentageBonus
        {
            get { return this.skillPercentageBonus; }
            set { this.skillPercentageBonus = MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's evade is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's evade is multiplied by after the class modifier.")]
        public float EvadePercentageBonus
        {
            get { return this.evadePercentageBonus; }
            set { this.evadePercentageBonus = MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's magic is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's magic is multiplied by after the class modifier.")]
        public float MagicPercentageBonus
        {
            get { return this.magicPercentageBonus; }
            set { this.magicPercentageBonus = MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's resistance is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's resistance is multiplied by after the class modifier.")]
        public float ResistancePercentageBonus
        {
            get { return this.resistancePercentageBonus; }
            set { this.resistancePercentageBonus = MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus); }
        }

        /// <summary>
        /// Gets or sets the bonus the player's speed is multiplied by after the class modifier.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Percentage Bonuses")]
        [Description("The bonus the player's speed is multiplied by after the class modifier.")]
        public float SpeedPercentageBonus
        {
            get { return this.speedPercentageBonus; }
            set { this.speedPercentageBonus = MathHelper.Clamp(value, PlayerStats.MinPercentageBonus, PlayerStats.MaxPercentageBonus); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's hitpoints after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's hitpoints after it is multiplied by the class modifier and percentage bonus.")]
        public int HitPointsPointBonus
        {
            get { return this.hitPointsPointBonus; }
            set { this.SetHitPointsSafe<int>(ref this.hitPointsPointBonus, (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost)); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's strength after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's strength after it is multiplied by the class modifier and percentage bonus.")]
        public int StrengthPointBonus
        {
            get { return this.strengthPointBonus; }
            set { this.strengthPointBonus = (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's skill after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's skill after it is multiplied by the class modifier and percentage bonus.")]
        public int SkillPointBonus
        {
            get { return this.skillPointBonus; }
            set { this.skillPointBonus = (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's evade after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's evade after it is multiplied by the class modifier and percentage bonus.")]
        public int EvadePointBonus
        {
            get { return this.evadePointBonus; }
            set { this.evadePointBonus = (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's magic after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's magic after it is multiplied by the class modifier and percentage bonus.")]
        public int MagicPointBonus
        {
            get { return this.magicPointBonus; }
            set { this.magicPointBonus = (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's resistance after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's resistance after it is multiplied by the class modifier and percentage bonus.")]
        public int ResistancePointBonus
        {
            get { return this.resistancePointBonus; }
            set { this.resistancePointBonus = (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost); }
        }

        /// <summary>
        /// Gets or sets the bonus added to the player's speed after it is multiplied by the class modifier and percentage bonus.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Point Bonuses")]
        [Description("The bonus added to the player's speed after it is multiplied by the class modifier and percentage bonus.")]
        public int SpeedPointBonus
        {
            get { return this.speedPointBonus; }
            set { this.speedPointBonus = (int)MathHelper.Clamp(value, PlayerStats.MinPointBoost, PlayerStats.MaxPointBoost); }
        }

        /// <summary>
        /// Gets the player's actual hitpoints stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual hitpoints stat.")]
        public int ActualHitPoints
        {
            get { return (int)MathHelper.Clamp((this.BaseHitPoints * this.Class.HitPointsModifier * 6 * this.HitPointsPercentageBonus) + this.HitPointsPointBonus, PlayerStats.MinHitPoints, PlayerStats.MaxActualHitPoints); }
        }

        /// <summary>
        /// Gets the player's actual strength stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual strength stat.")]
        public int ActualStrength
        {
            get { return (int)MathHelper.Clamp((this.BaseStrength * this.Class.StrengthModifier * this.StrengthPercentageBonus) + this.StrengthPointBonus, PlayerStats.MinActualStat, PlayerStats.MaxActualStat); }
        }

        /// <summary>
        /// Gets the player's actual skill stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual skill stat.")]
        public int ActualSkill
        {
            get { return (int)MathHelper.Clamp((this.BaseSkill * this.Class.SkillModifier * this.SkillPercentageBonus) + this.SkillPointBonus, PlayerStats.MinActualStat, PlayerStats.MaxActualStat); }
        }

        /// <summary>
        /// Gets the player's actual evade stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual evade stat.")]
        public int ActualEvade
        {
            get { return (int)MathHelper.Clamp((this.BaseEvade * this.Class.EvadeModifier * this.EvadePercentageBonus) + this.EvadePointBonus, PlayerStats.MinActualStat, PlayerStats.MaxActualStat); }
        }

        /// <summary>
        /// Gets the player's actual magic stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual magic stat.")]
        public int ActualMagic
        {
            get { return (int)MathHelper.Clamp((this.BaseMagic * this.Class.MagicModifier * this.MagicPercentageBonus) + this.MagicPointBonus, PlayerStats.MinActualStat, PlayerStats.MaxActualStat); }
        }

        /// <summary>
        /// Gets the player's actual resistance stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual resistance stat.")]
        public int ActualResistance
        {
            get { return (int)MathHelper.Clamp((this.BaseResistance * this.Class.ResistanceModifier * this.ResistancePercentageBonus) + this.ResistancePointBonus, PlayerStats.MinActualStat, PlayerStats.MaxActualStat); }
        }

        /// <summary>
        /// Gets the player's actual speed stat.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Actual Stats")]
        [Description("The player's actual speed stat.")]
        public int ActualSpeed
        {
            get { return (int)MathHelper.Clamp((this.BaseSpeed * this.Class.SpeedModifier * this.SpeedPercentageBonus) + this.SpeedPointBonus, PlayerStats.MinActualStat, PlayerStats.MaxActualStat); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base hitpoints can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base hitpoints stat any further, at their current level.")]
        public bool CanRaiseHitPoints
        {
            get { return this.BaseHitPoints < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base strength can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base strength stat any further, at their current level.")]
        public bool CanRaiseStrength
        {
            get { return this.BaseStrength < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base skill can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base skill stat any further, at their current level.")]
        public bool CanRaiseSkill
        {
            get { return this.BaseSkill < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base evade can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base evade stat any further, at their current level.")]
        public bool CanRaiseEvade
        {
            get { return this.BaseEvade < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base magic can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base magic stat any further, at their current level.")]
        public bool CanRaiseMagic
        {
            get { return this.BaseMagic < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base resistance can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base resistance stat any further, at their current level.")]
        public bool CanRaiseResistance
        {
            get { return this.BaseResistance < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets a value indicating whether the player's base speed can can be raised any further at their current level.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Base Stat Limits")]
        [Description("Whether the player can raise their base speed stat any further, at their current level.")]
        public bool CanRaiseSpeed
        {
            get { return this.BaseSpeed < PlayerStats.GetMaximumStat(this.Level); }
        }

        /// <summary>
        /// Gets or sets the player's current hitpoints.
        /// </summary>
        /// <value>See summary.</value>
        [Description("The player's current hitpoints.")]
        public int CurrentHitPoints
        {
            get { return this.currentHitPoints; }
            set { this.currentHitPoints = (int)MathHelper.Clamp(value, int.MinValue, this.ActualHitPoints); }
        }

        /// <summary>
        /// Gets a value indicating whether the player is alive.
        /// </summary>
        /// <value>See summary.</value>
        [Description("Whether the player is alive.")]
        public bool Alive
        {
            get { return this.CurrentHitPoints >= PlayerStats.MinHitPoints; }
        }

        /// <summary>
        /// Gets the minimum points in any base stat, at a particular level.
        /// </summary>
        /// <param name="level">The player's level.</param>
        /// <returns>See summary.</returns>
        /// <remarks>
        /// If a level below the minimum level or above the maximum level is given,
        /// the minimum stats for the minimum and maximum levels, respectively,
        /// will be returned.
        /// </remarks>
        public static int GetMinimumStat(int level)
        {
            if (level < PlayerStats.MinLevel)
            {
                level = PlayerStats.MinLevel;
            }
            else if (level > PlayerStats.MaxLevel)
            {
                level = PlayerStats.MaxLevel;
            }

            // return (int)MathHelper.Clamp(PlayerStats.MinBaseStat + (PlayerStats.MinStatsGained * (level - 1)), PlayerStats.MinBaseStat, PlayerStats.MaxBaseStat);*/
            return 0 / level;
        }

        /// <summary>
        /// Gets the maximum points in any base stat, at a particular level.
        /// </summary>
        /// <param name="level">The player's level.</param>
        /// <returns>See summary.</returns>
        /// <remarks>
        /// If a level below the minimum level or above the maximum level is given,
        /// the minimum stats for the minimum and maximum levels, respectively,
        /// will be returned.
        /// </remarks>
        public static int GetMaximumStat(int level)
        {
            if (level < PlayerStats.MinLevel)
            {
                level = PlayerStats.MinLevel;
            }
            else if (level > PlayerStats.MaxLevel)
            {
                level = PlayerStats.MaxLevel;
            }

            return (int)MathHelper.Clamp(PlayerStats.MinBaseStat + ((PlayerStats.MaxStatsGained - 1) * (level - 1)), PlayerStats.MinBaseStat, PlayerStats.MaxBaseStat);
        }

        /// <summary>
        /// Increases the player's level.
        /// </summary>
        public void IncreaseLevel()
        {
            if (this.Level < PlayerStats.MaxLevel && this.ExperiencePoints >= PlayerStats.ExperiencePointsForLevel)
            {
                this.Level++;
                this.ExperiencePoints = 0;

                this.BaseHitPoints += PlayerStats.GenerateStatGain();
                this.BaseStrength += PlayerStats.GenerateStatGain();
                this.BaseSkill += PlayerStats.GenerateStatGain();
                this.BaseEvade += PlayerStats.GenerateStatGain();
                this.BaseMagic += PlayerStats.GenerateStatGain();
                this.BaseResistance += PlayerStats.GenerateStatGain();
                this.BaseSpeed += PlayerStats.GenerateStatGain();
            }
        }

        /// <summary>
        /// Generates a pseudo-random number between <see cref="PlayerStats.MinStatsGained"/> and <see cref="PlayerStats.MaxStatsGained"/>, inclusive.
        /// </summary>
        /// <returns>See summary.</returns>
        private static int GenerateStatGain()
        {
            // We subtract one from the minimum stats to make the MinStatsGained value slightly more common
            // We add 1 to the maximum stats gained because the upper bound is exclusive
            return (int)MathHelper.Clamp(GameRandom.Next(PlayerStats.MinStatsGained - 1, PlayerStats.MaxStatsGained + 1), PlayerStats.MinStatsGained, PlayerStats.MaxStatsGained);
        }

        /// <summary>
        /// Sets the player's hitpoints in a safe way, when the class, base stat,
        /// stat percentage bonus or stat point bonuses are changed. This ensures
        /// the player can only die when the CurrentHitpoints property is set explicitly.
        /// </summary>
        /// <typeparam name="T">The type of the variable and value being set.</typeparam>
        /// <param name="variable">The variable to set the value of.</param>
        /// <param name="value">The value to set.</param>
        private void SetHitPointsSafe<T>(ref T variable, T value)
        {
            // Get the percentage of full HP
            float percentage = (float)this.CurrentHitPoints / (float)this.ActualHitPoints;

            // Make sure the HP doesn't get set to a weird value like int.MinValue
            // when we divide zero by zero
            if (float.IsNaN(percentage))
            {
                percentage = 0;
            }

            // Set the variable, whose value will contribute to the recalculation of the actual HP
            variable = value;

            // Ensure players are never killed by these effects by making the minimum zero...
            // Players should only be able to die by directly setting CurrentHitpoints...
            // This only takes effect is the player isn't already dead, though
            if (this.Alive)
            {
                this.CurrentHitPoints = (int)MathHelper.Clamp(percentage * this.ActualHitPoints, PlayerStats.MinHitPoints, this.ActualHitPoints);
            }
        }
    }
}
