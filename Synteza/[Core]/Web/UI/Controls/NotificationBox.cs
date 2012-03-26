namespace Petroules.Synteza.Web.UI.Controls
{
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Represents a notification box used to show errors, warnings or information at the top of a page in a very visible manner.
    /// </summary>
    [ParseChildren(true, "InnerHtml")]
    [PersistChildren(true)]
    [ToolboxData("<{0}:NotificationBox runat=server></{0}:NotificationBox>")]
    public class NotificationBox : WebControl
    {
        /// <summary>
        /// The display mode of the notification box.
        /// </summary>
        private Mode mode = Mode.Hidden;

        /// <summary>
        /// The CSS class used for the selected display mode.
        /// </summary>
        private string modeClass = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationBox"/> class.
        /// </summary>
        public NotificationBox()
            : base(HtmlTextWriterTag.Div)
        {
        }

        /// <summary>
        /// Enumerates the possible display modes for the notification box.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// The notification box is not shown.
            /// </summary>
            Hidden,

            /// <summary>
            /// The notification box is shown with an error icon.
            /// </summary>
            Error,

            /// <summary>
            /// The notification box is shown with an information icon.
            /// </summary>
            Information,

            /// <summary>
            /// The notification box is shown with a warning icon.
            /// </summary>
            Warning
        }

        /// <summary>
        /// Gets or sets the display mode of the notification box. The default is <see cref="Mode.Hidden"/>.
        /// </summary>
        public Mode DisplayMode
        {
            get
            {
                return this.mode;
            }

            set
            {
                this.mode = value;

                switch (value)
                {
                    case Mode.Hidden:
                        this.Visible = false;
                        this.modeClass = string.Empty;
                        break;
                    case Mode.Error:
                        this.Visible = true;
                        this.modeClass = "errorbox";
                        break;
                    case Mode.Information:
                        this.Visible = true;
                        this.modeClass = "infobox";
                        break;
                    case Mode.Warning:
                        this.Visible = true;
                        this.modeClass = "warningbox";
                        break;
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the content found between the opening and closing tags of the notification box control.
        /// </summary>
        public string InnerHtml
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text between the opening and closing tags of the notification box control.
        /// </summary>
        public string InnerText
        {
            get { return HttpUtility.HtmlDecode(this.InnerHtml); }
            set { this.InnerHtml = HttpUtility.HtmlEncode(value); }
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            this.Attributes["class"] = "msgbox";

            HtmlGenericControl innerDiv = new HtmlGenericControl("div");
            innerDiv.Attributes["class"] = this.modeClass;
            innerDiv.InnerHtml = this.InnerHtml;
            innerDiv.RenderControl(output);
        }
    }
}
