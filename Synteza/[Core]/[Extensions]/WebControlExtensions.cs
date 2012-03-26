namespace Petroules.Synteza
{
    using System.Globalization;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Provides extensions to the ASP.NET <see cref="Control"/> class.
    /// </summary>
    public static class WebControlExtensions
    {
        /// <summary>
        /// Renders the control and its children to a string of HTML text.
        /// </summary>
        /// <param name="control">The control to render.</param>
        /// <returns>The HTML text of the control and its children.</returns>
        public static string RenderToHtml(this Control control)
        {
            using (StringWriter strWriter = new StringWriter(CultureInfo.InvariantCulture))
            using (HtmlTextWriter writer = new HtmlTextWriter(strWriter))
            {
                control.RenderControl(writer);
                return strWriter.ToString();
            }
        }

        /// <summary>
        /// Sets the inner HTML of a web control by removing all its child controls and adding an inner &lt;div&gt;.
        /// </summary>
        /// <param name="control">The control to set the inner HTML of.</param>
        /// <param name="html">The HTML to set.</param>
        public static void SetInnerHtml(this Control control, string html)
        {
            control.Controls.Clear();
            control.Controls.Add(new HtmlGenericControl() { InnerHtml = html });
        }

        /// <summary>
        /// Sets the inner text of a web control by removing all its child controls and adding an inner &lt;div&gt;.
        /// </summary>
        /// <param name="control">The control to set the inner text of.</param>
        /// <param name="text">The text to set.</param>
        public static void SetInnerText(this Control control, string text)
        {
            control.Controls.Clear();
            control.Controls.Add(new HtmlGenericControl() { InnerText = text });
        }

        /// <summary>
        /// Appends HTML to the first child control of the specified web control.
        /// </summary>
        /// <param name="control">The control to append to the inner HTML of.</param>
        /// <param name="html">The HTML to append.</param>
        public static void AppendInnerHtml(this Control control, string html)
        {
            if (control.Controls.Count == 0)
            {
                control.Controls.Add(new HtmlGenericControl() { InnerHtml = html });
            }
            else
            {
                (control.Controls[0] as HtmlGenericControl).InnerHtml += html;
            }
        }

        /// <summary>
        /// Appends text to the first child control of the specified web control.
        /// </summary>
        /// <param name="control">The control to append to the inner text of.</param>
        /// <param name="text">The text to append.</param>
        public static void AppendInnerText(this Control control, string text)
        {
            if (control.Controls.Count == 0)
            {
                control.Controls.Clear();
                control.Controls.Add(new HtmlGenericControl() { InnerText = text });
            }
            else
            {
                (control.Controls[0] as HtmlGenericControl).InnerText += text;
            }
        }
    }
}
