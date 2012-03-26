namespace Petroules.Synteza.Web.UI.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class BooleanField : BoundField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanField"/> class.
        /// </summary>
        public BooleanField()
        {
        }

        /// <summary>
        /// Adds text or controls to a cell's controls collection.
        /// </summary>
        /// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell"/> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField"/>.</param>
        /// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType"/> values.</param>
        /// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState"/> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell"/>.</param>
        /// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell"/> is contained in.</param>
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            switch (cellType)
            {
                case DataControlCellType.Header:
                    this.InitializeHeaderCell(cell, rowState);
                    break;
                case DataControlCellType.Footer:
                    this.InitializeFooterCell(cell, rowState);
                    break;
                case DataControlCellType.DataCell:
                    this.InitializeDataCell(cell, rowState);
                    break;
            }
        }

        /// <summary>
        /// When overridden in a derived class, creates an empty <see cref="T:System.Web.UI.WebControls.DataControlField"/>-derived object.
        /// </summary>
        /// <returns>
        /// An empty <see cref="T:System.Web.UI.WebControls.DataControlField"/>-derived object.
        /// </returns>
        protected override DataControlField CreateField()
        {
            return new BooleanField();
        }

        protected void InitializeHeaderCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            cell.Controls.Add(new Label() { Text = this.DataField });
        }

        protected void InitializeFooterCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            cell.Controls.Add(new CheckBox());
        }

        /// <summary>
        /// Determines which control to bind to data. In this a hyperlink control is bound regardless
        /// of the row state. The hyperlink control is then attached to a DataBinding event handler
        /// to actually retrieve and display data.
        /// <para/>
        /// Note: This control was built with the assumption that it will not be used in a GridView
        /// control that uses inline editing. If you are building a custom data control field and
        /// using this code for reference purposes key in mind that if your control needs to support
        /// inline editing you must determine which control to bind to data based on the row state.
        /// </summary>
        /// <param name="cell">A reference to the cell</param>
        /// <param name="rowState">State of the row being rendered</param>
        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            switch (rowState)
            {
                case DataControlRowState.Normal:
                    Label labelText = new Label();
                    labelText.DataBinding += this.LabelText_DataBinding;
                    cell.Controls.Add(labelText);
                    break;
                case DataControlRowState.Edit:
                    if (this.ReadOnly)
                    {
                        goto case DataControlRowState.Normal;
                    }

                    TextBox textBox = new TextBox();
                    textBox.Columns = 5;
                    textBox.ID = Guid.NewGuid().ToString();
                    textBox.DataBinding += this.TextBox_DataBinding;
                    cell.Controls.Add(textBox);
                    break;
            }
        }

        private void LabelText_DataBinding(object sender, EventArgs e)
        {
            // get a reference to the control that raised the event
            Label target = (Label)sender;
            Control container = target.NamingContainer;

            // get a reference to the row object
            object dataItem = DataBinder.GetDataItem(container);

            // get the row's value for the named data field only use Eval when it is neccessary
            // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
            // is faster because it does not use reflection
            object dataFieldValue = null;

            if (this.DataField.Contains("."))
            {
                dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
            }
            else
            {
                dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
            }

            // set the table cell's text. check for null values to prevent ToString errors
            if (dataFieldValue != null)
            {
                target.Text = dataFieldValue.ToString();
            }
        }

        private void TextBox_DataBinding(object sender, EventArgs e)
        {
            // get a reference to the control that raised the event
            TextBox target = (TextBox)sender;
            Control container = target.NamingContainer;

            // get a reference to the row object
            object dataItem = DataBinder.GetDataItem(container);

            // get the row's value for the named data field only use Eval when it is neccessary
            // to access child object values, otherwise use GetPropertyValue. GetPropertyValue
            // is faster because it does not use reflection
            object dataFieldValue = null;

            if (this.DataField.Contains("."))
            {
                dataFieldValue = DataBinder.Eval(dataItem, this.DataField);
            }
            else
            {
                dataFieldValue = DataBinder.GetPropertyValue(dataItem, this.DataField);
            }

            // set the table cell's text. check for null values to prevent ToString errors
            if (dataFieldValue != null)
            {
                target.Text = dataFieldValue.ToString();
            }
        }
    }
}