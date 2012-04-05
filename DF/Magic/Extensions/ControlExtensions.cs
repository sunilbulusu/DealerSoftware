using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Magic.Extensions
{
    public static class ControlExtensions
    {
        public static int GetIdFromCommandArg(this System.Web.UI.WebControls.LinkButton c)
        {
            return c.CommandArgument.ToInt();
        }

        public static int GetIdFromCommandArg(this System.Web.UI.WebControls.Button c)
        {
            return c.CommandArgument.ToInt();
        }

        public static void SelectByText(this System.Web.UI.WebControls.DropDownList d, string text)
        {
            ListItem item = d.Items.FindByText(text);

            if (item == null)
                return;

            d.SelectedValue = item.Value;
        }
    
    }
}
