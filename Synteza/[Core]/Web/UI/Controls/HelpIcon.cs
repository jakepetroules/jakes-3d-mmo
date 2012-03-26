namespace Petroules.Synteza.Web.UI.Controls
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Represents a help icon used to display context sensitive help information.
    /// </summary>
    [DefaultProperty("HelpText")]
    [ToolboxData("<{0}:HelpIcon runat=server></{0}:HelpIcon>")]
    public class HelpIcon : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpIcon"/> class.
        /// </summary>
        public HelpIcon()
            : base(HtmlTextWriterTag.Img)
        {
        }

        /// <summary>
        /// Gets or sets the help text that the icon should display.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string HelpText
        {
            get { return (string)this.ViewState["HelpText"] ?? string.Empty; }
            set { this.ViewState["HelpText"] = value; }
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            this.Attributes["src"] = "/style/images/help.png";
            this.Attributes["alt"] = "?";
            this.Attributes["onmouseover"] = string.Format(CultureInfo.InvariantCulture, "return overlib(\"{0}\");", HttpUtility.HtmlEncode(this.HelpText));
            this.Attributes["onmouseout"] = "return nd();";
        }
    }
}
