using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Magic
{
    public class ControlManager
    {
        public static Control FindControlRecursive(Control control, string id)
        {
            if (control == null || string.IsNullOrWhiteSpace(id))
                return null;

            if (!string.IsNullOrWhiteSpace(control.ID) && control.ID.StartsWith(id))
                return control;

            if (control.Controls.Count > 0)
            {
                foreach (Control ctrl in control.Controls)
                {
                    Control foundCtrl = FindControlRecursive(ctrl, id);

                    if (foundCtrl != null)
                        return foundCtrl;
                }
            }

            return null;
        }

        public static void ClearControls(ref Control control)
        {
            if (control == null)
                return;

            if (control is TextBox)
            {
                ((TextBox)control).Text = string.Empty;
                return;
            }

            else if (control is DropDownList)
            {
                ((DropDownList)control).SelectedIndex = 0;
                return;
            }

            if (control.HasControls())
            {
                foreach (Control c in control.Controls)
                {
                    Control ctrl = c;
                    ClearControls(ref ctrl);
                }
            }
        }

        public static List<ListItem> GetItemsFromDropDown(DropDownList ddl)
        {
            List<ListItem> items = new List<ListItem>();
            if (ddl == null)
                return items;

            return items;
        }

        public static void SetViewState(Control container)
        {
            container.ViewStateMode= ViewStateMode.Disabled;
            if (container.HasControls())
            {
                foreach(Control ctrl in container.Controls)
                {
                    Control control = ctrl;
                    SetViewState(control);
                }
            }
                
        }
    }
}
