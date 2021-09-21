using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.Data;

namespace VKATalk.Reports
{
	public partial class RaceCardReport : System.Web.UI.Page
	{
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
		{
            var racedate = string.Empty;
            var centerid = 0;
            var centername = string.Empty;
            var season = string.Empty;
            var year = string.Empty;
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;

            try
            {
                if (!Request.QueryString["RaceDate"].Equals(""))
                {

                    racedate = Request.QueryString["RaceDate"];
                    centerid = Convert.ToInt32(Request.QueryString["CenterID"]);

                    lblCener.Text = Request.QueryString["CenterName"];
                    lblSeason.Text = Request.QueryString["Season"];
                    lblYear.Text = Request.QueryString["Year"];
                    lblRaceDate.Text = racedate + "(" + Convert.ToDateTime(racedate).DayOfWeek +")";



                    ds = new ReportBL().GetRaceCardReport(racedate, centerid);
                    if (ds.Tables[0].Rows.Count > 0) {
                         lvCardRace.DataSource = ds.Tables[0];
                        
                        lvCardRace.DataBind();
                        Label18.Text = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
           


        }


        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
       {
            //Label lblID = e.Item.FindControl("NameLabel") as Label;//finding control in Listview1 control
            HiddenField hdnflDivisionRaceID = e.Item.FindControl("hdnfieldDivisionRaceID") as HiddenField;
            if (ds.Tables[1].Rows.Count > 0)
            {
                var dtHorse = new DataTable();
                dtHorse = ds.Tables[1].Clone();

                foreach (DataRow dr in ds.Tables[1].Select("DivisionRaceID=" + Convert.ToInt32(hdnflDivisionRaceID.Value.ToString())))
                {

                    dtHorse.ImportRow(dr);

                }
                //ListViewDataItem currentItem = (ListViewDataItem)e.Item;
                //ListView lvHorse = (ListView)currentItem.FindControl("lvHorse");

                ListView lvHorse = e.Item.FindControl("lvHorse") as ListView;
                lvHorse.DataSource = dtHorse;
                lvHorse.DataBind();
            }

        }

    }
}