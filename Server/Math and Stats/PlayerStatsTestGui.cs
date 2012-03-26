namespace MMO3D.PlayerStatsTest
{
    using System;
    using System.Windows.Forms;
    using MMO3D.CommonCode;

    /// <summary>
    /// The player stats test main GUI form.
    /// </summary>
    public partial class PlayerStatsTestGui : Form
    {
        /// <summary>
        /// The player stats instance used for testing.
        /// </summary>
        private PlayerStats playerStats;

        /// <summary>
        /// The character class instance used for testing.
        /// Do not set this directly on the PlayerStats class;
        /// clone it. You're not supposed to be able to edit
        /// the modifiers of a CharacterClass, it is (or will
        /// be) an immutable type.
        /// </summary>
        private CharacterClass characterClass;

        /// <summary>
        /// Initializes a new instance of the PlayerStatsTestGui class.
        /// </summary>
        public PlayerStatsTestGui()
        {
            this.InitializeComponent();
            this.InitializeData();
        }

        /// <summary>
        /// Initializes or resets all data in the program.
        /// </summary>
        private void InitializeData()
        {
            this.characterClass = new CharacterClass(1, 1, 1, 1, 1, 1, 1);
            this.playerStats = new PlayerStats(this.characterClass.Clone() as CharacterClass);

            this.ResetPropertyGrids();
        }

        /// <summary>
        /// Refreshes the property grids.
        /// </summary>
        private void ResetPropertyGrids()
        {
            this.playerStats.Class = this.characterClass.Clone() as CharacterClass;

            this.propertyGridPlayerStats.SelectedObject = this.playerStats;
            this.propertyGridCharacterClass.SelectedObject = this.characterClass;
        }

        /// <summary>
        /// Resets all the data in the test program.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            this.InitializeData();
        }

        /// <summary>
        /// Increases the player's level.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonIncreaseLevel_Click(object sender, EventArgs e)
        {
            int xp = this.playerStats.ExperiencePoints;
            this.playerStats.ExperiencePoints = PlayerStats.ExperiencePointsForLevel;
            this.playerStats.IncreaseLevel();
            this.playerStats.ExperiencePoints = xp;

            this.ResetPropertyGrids();
        }

        /// <summary>
        /// Refreshes both property grids whenever a property in the character class has been changed.
        /// </summary>
        /// <param name="s">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PropertyGridCharacterClass_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.ResetPropertyGrids();
        }
    }
}
