using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VKATalk
{
    public partial class Styles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Red Single Under Line
            //lblText.Text = string.Format("<span style='text-decoration: underline; text-decoration-color:red;'>{0}</span>", lblText.Text);

            //Exclamation Mark
            string explanation = ".";
            //lblText.Text = lblText.Text + string.Format("<span style='color:red;'>{0}</span>", explanation);

            lblText.Text = lblText.Text + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "x");
            lblText.Text = lblText.Text + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "?");
            lblText.Text = lblText.Text + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "\u2714");
            lblText.Text = lblText.Text + string.Format("<span style='font-size: xx-large; font-weight: bold; text-decoration:underline; text-decoration-color:red;'><span style='font-size:50px; font-weight: bold; color: blue'>{0}</span></span>", ".");
            //lblText.Text = lblText.Text + string.Format("<span style='text-decoration: underline; font-size:xx-large; font-weight: bold; text-decoration-color:green; '> <span style='text-align: center; font-size: xx-large; font-weight: bold; border: 2px solid green; padding:5px; border-radius: 25px;'>{0}</span></span >", "testing2222");

            //lblText.Text = lblText.Text + string.Format("<span style='text-decoration: underline; text-decoration-color:green; '> <span style='text-align: center; border: 2px solid green; padding:5px; border-radius: 25px;'>{0}</span></span >", "testing2222");
        }
    }
}