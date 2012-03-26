namespace MMO3D.Client
{
    /// <summary>
    /// Represents a Windows button control modified so that it does not receive focus.
    /// </summary>
    public class NoFocusButton : System.Windows.Forms.Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoFocusButton"/> class.
        /// </summary>
        public NoFocusButton()
            : base()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, false);
        }
    }
}
