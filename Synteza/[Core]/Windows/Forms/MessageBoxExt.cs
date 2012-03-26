namespace Petroules.Synteza.Windows.Forms
{
    using System.Windows.Forms;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Contains extended message box functions.
    /// </summary>
    public static class MessageBoxExt
    {
        /// <summary>
        /// Displays an RTL-sensitive information message box with the specified text.
        /// </summary>
        /// <remarks>
        /// The following additional options are set:
        /// <ul>
        /// <li>Caption is "Information" (culture-sensitive).</li>
        /// <li>Buttons are: <see cref="MessageBoxButtons.OK"/>.</li>
        /// <li>Icon is: <see cref="MessageBoxIcon.Information"/>.</li>
        /// <li>Default button is: <see cref="MessageBoxDefaultButton.Button1"/>.</li>
        /// </ul>
        /// </remarks>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowInformation(string text)
        {
            if (Form.ActiveForm != null)
            {
                return MessageBox.Show(Form.ActiveForm, text, Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, Form.ActiveForm.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
            else
            {
                return MessageBox.Show(text, Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }
        }

        /// <summary>
        /// Displays an RTL-sensitive error message box with the specified text.
        /// </summary>
        /// <remarks>
        /// The following additional options are set:
        /// <ul>
        /// <li>Caption is "Error" (culture-sensitive).</li>
        /// <li>Buttons are: <see cref="MessageBoxButtons.OK"/>.</li>
        /// <li>Icon is: <see cref="MessageBoxIcon.Error"/>.</li>
        /// <li>Default button is: <see cref="MessageBoxDefaultButton.Button1"/>.</li>
        /// </ul>
        /// </remarks>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowError(string text)
        {
            if (Form.ActiveForm != null)
            {
                return MessageBox.Show(Form.ActiveForm, text, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Form.ActiveForm.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
            else
            {
                return MessageBox.Show(text, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
            }
        }

        /// <summary>
        /// Displays an RTL-sensitive boolean question message box with the specified text.
        /// </summary>
        /// <remarks>
        /// The following additional options are set:
        /// <ul>
        /// <li>Caption is "Question" (culture-sensitive).</li>
        /// <li>Buttons are: <see cref="MessageBoxButtons.YesNo"/>.</li>
        /// <li>Icon is: <see cref="MessageBoxIcon.Question"/>.</li>
        /// <li>Default button is: <see cref="MessageBoxDefaultButton.Button1"/>.</li>
        /// </ul>
        /// </remarks>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowQuestion(string text)
        {
            if (Form.ActiveForm != null)
            {
                return MessageBox.Show(Form.ActiveForm, text, Resources.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Form.ActiveForm.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
            else
            {
                return MessageBox.Show(text, Resources.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
            }
        }

        /// <summary>
        /// Displays an RTL-sensitive cancellable boolean question message box with the specified text.
        /// </summary>
        /// <remarks>
        /// The following additional options are set:
        /// <ul>
        /// <li>Caption is "Question" (culture-sensitive).</li>
        /// <li>Buttons are: <see cref="MessageBoxButtons.YesNoCancel"/>.</li>
        /// <li>Icon is: <see cref="MessageBoxIcon.Question"/>.</li>
        /// <li>Default button is: <see cref="MessageBoxDefaultButton.Button1"/>.</li>
        /// </ul>
        /// </remarks>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowQuestionCancel(string text)
        {
            if (Form.ActiveForm != null)
            {
                return MessageBox.Show(Form.ActiveForm, text, Resources.Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Form.ActiveForm.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
            else
            {
                return MessageBox.Show(text, Resources.Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
            }
        }

        /// <summary>
        /// Displays an RTL-sensitive warning confirmation message box with the specified text.
        /// </summary>
        /// <remarks>
        /// The following additional options are set:
        /// <ul>
        /// <li>Caption is "Warning" (culture-sensitive).</li>
        /// <li>Buttons are: <see cref="MessageBoxButtons.YesNo"/>.</li>
        /// <li>Icon is: <see cref="MessageBoxIcon.Warning"/>.</li>
        /// <li>Default button is: <see cref="MessageBoxDefaultButton.Button2"/>.</li>
        /// </ul>
        /// </remarks>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowConfirmWarning(string text)
        {
            if (Form.ActiveForm != null)
            {
                return MessageBox.Show(Form.ActiveForm, text, Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, Form.ActiveForm.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
            else
            {
                return MessageBox.Show(text, Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0);
            }
        }

        /// <summary>
        /// Displays an RTL-sensitive cancellable warning confirmation message box with the specified text.
        /// </summary>
        /// <remarks>
        /// The following additional options are set:
        /// <ul>
        /// <li>Caption is "Warning" (culture-sensitive).</li>
        /// <li>Buttons are: <see cref="MessageBoxButtons.YesNoCancel"/>.</li>
        /// <li>Icon is: <see cref="MessageBoxIcon.Warning"/>.</li>
        /// <li>Default button is: <see cref="MessageBoxDefaultButton.Button3"/>.</li>
        /// </ul>
        /// </remarks>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the <see cref="System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowConfirmWarningCancel(string text)
        {
            if (Form.ActiveForm != null)
            {
                return MessageBox.Show(Form.ActiveForm, text, Resources.Warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, Form.ActiveForm.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
            else
            {
                return MessageBox.Show(text, Resources.Warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, 0);
            }
        }
    }
}
