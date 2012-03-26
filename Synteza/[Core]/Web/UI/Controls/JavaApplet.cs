namespace Petroules.Synteza.Web.UI.Controls
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Represents a Java applet.
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:JavaApplet runat=server></{0}:JavaApplet>")]
    public class JavaApplet : WebControl
    {
        /// <summary>
        /// Gets or sets the fully-qualified classname of the main class (example: mycompany.myproduct.MyApplet).
        /// </summary>
        public string ClassName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path to the JAR archive on the web server.
        /// </summary>
        public string JarPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the applet.
        /// </summary>
        public int AppletWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the applet.
        /// </summary>
        public int AppletHeight
        {
            get;
            set;
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            HtmlGenericControl objectTag = new HtmlGenericControl("object");
            objectTag.Attributes["classid"] = string.Format(CultureInfo.InvariantCulture, "java:{0}.class", this.ClassName);
            objectTag.Attributes["type"] = "application/x-java-applet";
            objectTag.Attributes["width"] = this.AppletWidth.ToString(CultureInfo.InvariantCulture);
            objectTag.Attributes["height"] = this.AppletHeight.ToString(CultureInfo.InvariantCulture);
            objectTag.Attributes["archive"] = this.JarPath;

            // Konqueror browser needs the following param
            HtmlGenericControl konquerorParam = new HtmlGenericControl("param");
            konquerorParam.Attributes["name"] = "archive";
            konquerorParam.Attributes["value"] = this.JarPath;
            // under this we need an: <!--<![endif]-->

            HtmlGenericControl innerObject = new HtmlGenericControl("object");
            innerObject.Attributes["classid"] = "clsid:8AD9C840-044E-11D1-B3E9-00805F499D93";
            innerObject.Attributes["width"] = this.AppletWidth.ToString(CultureInfo.InvariantCulture);
            innerObject.Attributes["height"] = this.AppletHeight.ToString(CultureInfo.InvariantCulture);
            innerObject.Attributes["codebase"] = "http://java.sun.com/update/1.5.0/jinstall-1_5_0-windows-i586.cab";

            HtmlGenericControl innerCodeParam = new HtmlGenericControl("param");
            innerCodeParam.Attributes["name"] = "code";
            innerCodeParam.Attributes["value"] = this.ClassName;

            HtmlGenericControl innerArchiveParam = new HtmlGenericControl("param");
            innerArchiveParam.Attributes["name"] = "archive";
            innerArchiveParam.Attributes["value"] = this.JarPath;

            innerObject.InnerHtml = @"<div runat='server' id='appletFailureCode'>
            <table class='nojava'>
                <tr>
                    <td>
                        You do not have Java installed or enabled.<br />
                        <a href='http://java.sun.com/products/plugin/downloads/index.html'>Get Java Now!</a>
                    </td>
                </tr>
            </table>
        </div>";

            // need: <!--[if !IE]>--> after inner obj and before closing outer obj

            objectTag.Controls.Add(konquerorParam);
            objectTag.Controls.Add(innerObject);
            innerObject.Controls.Add(innerCodeParam);
            innerObject.Controls.Add(innerArchiveParam);

            output.Write("<!-- XHTML-compliant, cross-browser Java applet code - http://ww2.cs.fsu.edu/~steele/XHTML/appletObject.html -->");
            output.Write("<!--[if !IE]>-->");

            objectTag.RenderControl(output);

            output.Write("<!--<![endif]-->");
        }
    }
}
