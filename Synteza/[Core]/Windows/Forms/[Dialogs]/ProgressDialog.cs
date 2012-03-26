namespace Petroules.Synteza.Windows.Forms
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Represents a progress dialog.
    /// </summary>
    public partial class ProgressDialog : Form
    {
        /// <summary>
        /// The informational text displayed on the dialog.
        /// </summary>
        private volatile string informationText;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressDialog"/> class.
        /// </summary>
        public ProgressDialog()
        {
            this.InitializeComponent();
            this.InformationText = string.Empty;
        }

        /// <summary>
        /// Raised when the cancel button is clicked to stop the operation.
        /// </summary>
        public event EventHandler OperationCanceled
        {
            add { this.buttonCancel.Click += value; }
            remove { this.buttonCancel.Click -= value; }
        }

        /// <summary>
        /// Gets or sets the progress of the operation. This property is thread-safe.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value is less than zero or greater than 100.</exception>
        public int Progress
        {
            get
            {
                return this.progressBar.GetValue();
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("value", value, Resources.ValueMustBeInRange);
                }

                this.progressBar.SetValue(value);
                this.UpdateDialogText(value, this.InformationText);
            }
        }

        /// <summary>
        /// Gets or sets the informational text displayed on the dialog. This property is thread-safe.
        /// </summary>
        public string InformationText
        {
            get { return this.informationText; }
            set { this.UpdateDialogText(this.Progress, this.informationText = value); }
        }

        /// <summary>
        /// Updates the dialog text with the current progress and status message. This method is thread-safe.
        /// </summary>
        /// <param name="progress">The progress of the operation.</param>
        /// <param name="text">The informational text displayed on the dialog.</param>
        private void UpdateDialogText(int progress, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                this.labelText.SetText(string.Format(CultureInfo.InvariantCulture, "{0}% - {1}", progress, text));
            }
            else
            {
                this.labelText.SetText(string.Format(CultureInfo.InvariantCulture, "{0}%", progress));
            }
        }
    }
}
