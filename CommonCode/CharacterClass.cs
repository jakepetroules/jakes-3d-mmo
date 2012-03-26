namespace MMO3D.CommonCode
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a character class with various stat modifiers.
    /// </summary>
    public sealed class CharacterClass : ICloneable
    {
        /// <summary>
        /// The minimum stat modifier, in hundreds of percent (0 would be 0%, 10 would be 1000%).
        /// </summary>
        public const float MinModifier = 0;

        /// <summary>
        /// The maximum stat modifier, in hundreds of percent (0 would be 0%, 10 would be 1000%).
        /// </summary>
        public const float MaxModifier = 3;

        /// <summary>
        /// The class modifier the player's base hitpoints is multiplied by.
        /// </summary>
        private float hitPointsModifier;

        /// <summary>
        /// The class modifier the player's base strength is multiplied by.
        /// </summary>
        private float strengthModifier;

        /// <summary>
        /// The class modifier the player's base skill is multiplied by.
        /// </summary>
        private float skillModifier;

        /// <summary>
        /// The class modifier the player's base evade is multiplied by.
        /// </summary>
        private float evadeModifier;

        /// <summary>
        /// The class modifier the player's base magic is multiplied by.
        /// </summary>
        private float magicModifier;

        /// <summary>
        /// The class modifier the player's base resistance is multiplied by.
        /// </summary>
        private float resistanceModifier;

        /// <summary>
        /// The class modifier the player's base speed is multiplied by.
        /// </summary>
        private float speedModifier;

        /// <summary>
        /// Initializes a new instance of the CharacterClass class.
        /// </summary>
        /// <param name="hitPointsModifier">The class modifier the player's base hitpoints is multiplied by.</param>
        /// <param name="strengthModifier">The class modifier the player's base strength is multiplied by.</param>
        /// <param name="skillModifier">The class modifier the player's base skill is multiplied by.</param>
        /// <param name="evadeModifier">The class modifier the player's base evade is multiplied by.</param>
        /// <param name="magicModifier">The class modifier the player's base magic is multiplied by.</param>
        /// <param name="resistanceModifier">The class modifier the player's base resistance is multiplied by.</param>
        /// <param name="speedModifier">The class modifier the player's base speed is multiplied by.</param>
        public CharacterClass(float hitPointsModifier, float strengthModifier, float skillModifier, float evadeModifier, float magicModifier, float resistanceModifier, float speedModifier)
        {
            this.HitPointsModifier = hitPointsModifier;
            this.StrengthModifier = strengthModifier;
            this.SkillModifier = skillModifier;
            this.EvadeModifier = evadeModifier;
            this.MagicModifier = magicModifier;
            this.ResistanceModifier = resistanceModifier;
            this.SpeedModifier = speedModifier;
        }

        /// <summary>
        /// Prevents a default instance of the CharacterClass class from being created.
        /// </summary>
        private CharacterClass()
        {
        }

        /// <summary>
        /// Gets the class modifier the player's base hitpoints is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base hitpoints is multiplied by.")]
        public float HitPointsModifier
        {
            get { return this.hitPointsModifier; }
            private set { this.hitPointsModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Gets the class modifier the player's base strength is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base strength is multiplied by.")]
        public float StrengthModifier
        {
            get { return this.strengthModifier; }
            private set { this.strengthModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Gets the class modifier the player's base skill is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base skill is multiplied by.")]
        public float SkillModifier
        {
            get { return this.skillModifier; }
            private set { this.skillModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Gets the class modifier the player's base evade is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base evade is multiplied by.")]
        public float EvadeModifier
        {
            get { return this.evadeModifier; }
            private set { this.evadeModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Gets the class modifier the player's base magic is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base magic is multiplied by.")]
        public float MagicModifier
        {
            get { return this.magicModifier; }
            private set { this.magicModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Gets the class modifier the player's base resistance is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base resistance is multiplied by.")]
        public float ResistanceModifier
        {
            get { return this.resistanceModifier; }
            private set { this.resistanceModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Gets the class modifier the player's base speed is multiplied by.
        /// </summary>
        /// <value>See summary.</value>
        [Category("Stat Modifiers")]
        [Description("The class modifier the player's base speed is multiplied by.")]
        public float SpeedModifier
        {
            get { return this.speedModifier; }
            private set { this.speedModifier = MathHelper.Clamp(value, CharacterClass.MinModifier, CharacterClass.MaxModifier); }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            CharacterClass characterClass = new CharacterClass();
            characterClass.EvadeModifier = this.EvadeModifier;
            characterClass.HitPointsModifier = this.HitPointsModifier;
            characterClass.MagicModifier = this.MagicModifier;
            characterClass.ResistanceModifier = this.ResistanceModifier;
            characterClass.SkillModifier = this.SkillModifier;
            characterClass.SpeedModifier = this.SpeedModifier;
            characterClass.StrengthModifier = this.StrengthModifier;
            return characterClass;
        }
    }
}
