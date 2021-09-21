using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;

namespace VKATalk.Reports
{
	public partial class RaceGuide : System.Web.UI.Page
	{
        DataSet ds;
        string racedate = string.Empty;
        int raceid = 1;
        int centerid = 1;
        string horseid = string.Empty;
        int count = 1;
        string racedatesecond = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
		{
            
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;

            try
            {
                if (!IsPostBack)
                {

                        racedate = Request.QueryString["RaceDate"];
                        centerid = Convert.ToInt32(Request.QueryString["CenterID"]);
                        lblCener.Text = Request.QueryString["CenterName"];
                        lblSeason.Text = Request.QueryString["Season"];
                        lblYear.Text = Request.QueryString["Year"];
                        lblRaceDate.Text = racedate + "(" + Convert.ToDateTime(racedate).DayOfWeek + ")";

                    var dt = new ReportBL().GetTotalRaceDetail(centerid.ToString(), racedate);
                    if (dt.Rows.Count > 0)
                    {
                        lblDivisionRaceID.Text = dt.Rows[0][1].ToString();
                        drpdwnRaceNumber.DataSource = dt;
                        drpdwnRaceNumber.DataTextField = "DayRaceNo";
                        drpdwnRaceNumber.DataValueField = "DayRaceNo";
                        drpdwnRaceNumber.DataBind();
                        drpdwnRaceNumber.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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

        protected void drpdwnRaceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(!drpdwnRaceNumber.SelectedItem.Value.Equals("-1"))
                {
                    

                    ds = new ReportBL().GetRaceGuide(Request.QueryString["RaceDate"], Convert.ToInt32(Request.QueryString["CenterID"]),Convert.ToInt32(drpdwnRaceNumber.SelectedItem.Value));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvCardRace.DataSource = ds.Tables[0];
                        lvCardRace.DataBind();
                        //Race1Show(ds.Tables[0]);
                    }
                }
                else
                {
                    lvCardRace.DataSource = new DataTable();
                    lvCardRace.DataBind();
                }
                

                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void lvCardRace_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hdnflDivisionRaceID = e.Item.FindControl("hdnfieldDivisionRaceID") as HiddenField;
            if (ds.Tables[1].Rows.Count > 0)
            {
                var dtHorse = new DataTable();
                dtHorse = ds.Tables[1].Clone();

                foreach (DataRow dr in ds.Tables[1].Select("DivisionRaceID=" + Convert.ToInt32(hdnflDivisionRaceID.Value.ToString())))
                {

                    dtHorse.ImportRow(dr);

                }



                var momenttotype = string.Empty;
                momenttotype = ds.Tables[0].Rows[0][18].ToString();
                var permanentconditionhighlight = ds.Tables[0].Rows[0][43].ToString();
                var seasonalconditionhighlight = ds.Tables[0].Rows[0][44].ToString();
                var racecardconditionhighlight = ds.Tables[0].Rows[0][45].ToString();
                var lblName = (Label)e.Item.FindControl("NameLabel");
                var Label22 = (Label)e.Item.FindControl("Label22");
                var Label24 = (Label)e.Item.FindControl("Label24");
                var Label36 = (Label)e.Item.FindControl("Label36");
                var Label37 = (Label)e.Item.FindControl("Label37");
                var Label33 = (Label)e.Item.FindControl("Label33");

                if (permanentconditionhighlight.Contains("1"))
                {
                    Label36.BackColor = Color.Orange;
                }
                else
                {
                    Label36.BackColor = Color.White;
                }
                if (seasonalconditionhighlight.Contains("1"))
                {
                    Label37.BackColor = Color.Orange;
                }
                else
                {
                    Label37.BackColor = Color.White;
                }
                if (racecardconditionhighlight.Contains("1"))
                {
                    Label33.BackColor = Color.Orange;
                }
                else
                {
                    Label33.BackColor = Color.White;
                }



                if (momenttotype.Equals("Trophy"))
                {
                    // panel.BackColor = Color.Orange;
                    lblName.BackColor = Color.Orange;
                    Label22.BackColor = Color.Orange;
                    Label24.BackColor = Color.Orange;
                }
                else
                {
                    // panel.BackColor = Color.White;
                    lblName.BackColor = Color.White;
                    Label22.BackColor = Color.White;
                    Label24.BackColor = Color.White;
                }

                ListView lvHorse = e.Item.FindControl("lvHorse") as ListView;
                lvHorse.DataSource = dtHorse;
                lvHorse.DataBind();

                

            }

        }


        protected void lvHorse_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hdnfieldHorseNameID = e.Item.FindControl("hdnfieldHorseNameID") as HiddenField;
            HiddenField hdnfieldHorseID = e.Item.FindControl("hdnfieldHorseID") as HiddenField;
            ViewState["HorseID"] = hdnfieldHorseID.Value;
            var ds1 = new ReportBL().GetHorseFamily(hdnfieldHorseNameID.Value, Request.QueryString["RaceDate"]);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ListView lvFamilyTree = e.Item.FindControl("lvFamilyTree") as ListView;
                lvFamilyTree.DataSource = ds1.Tables[0];
                lvFamilyTree.DataBind();
            }

           

            var ds = new ReportBL().GetHorseFamilyMoreDetail(hdnfieldHorseID.Value, Request.QueryString["RaceDate"], raceid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ListView lsviewhorseperformance = e.Item.FindControl("lsviewhorseperformance") as ListView;
                lsviewhorseperformance.DataSource = ds.Tables[0];
                lsviewhorseperformance.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {

                Repeater rpCardDeclaration = e.Item.FindControl("rpCardDeclaration") as Repeater;
                rpCardDeclaration.DataSource = ds.Tables[1];
                rpCardDeclaration.DataBind();
            }


            if (ds.Tables[2].Rows.Count > 0)
            {

                Repeater rpcardentry = e.Item.FindControl("rpcardentry") as Repeater;
                rpcardentry.DataSource = ds.Tables[2];
                rpcardentry.DataBind();
            }

            var ds2 = new ReportBL().GetHorsePerformance(Convert.ToInt32(ViewState["HorseID"]), Request.QueryString["RaceDate"]);
            Repeater rpBunchPerformance = e.Item.FindControl("rpBunchPerformance") as Repeater;
            Repeater rpBunchClusterPerformance = e.Item.FindControl("rpBunchClusterPerformance") as Repeater;
            Repeater rpBunchGroupAlias = e.Item.FindControl("rpBunchGroupAlias") as Repeater;
            Repeater rpHorsePerformanceCalculator = e.Item.FindControl("rpHorsePerformanceCalculator") as Repeater;
            Repeater rpDistancePerformanceCurrent = e.Item.FindControl("rpDistancePerformanceCurrent") as Repeater;
            

            if (ds2.Tables[0].Rows.Count > 0)
            {
                rpBunchPerformance.Visible = true;
                rpBunchPerformance.DataSource = ds2.Tables[0];
                rpBunchPerformance.DataBind();
            }
            else
            {
                rpBunchPerformance.Visible = false;
            }


            if (ds2.Tables[1].Rows.Count > 0)
            {
                rpBunchClusterPerformance.Visible = true;
                rpBunchClusterPerformance.DataSource = ds2.Tables[1];
                rpBunchClusterPerformance.DataBind();
            }
            else
            {
                rpBunchClusterPerformance.Visible = false;
            }
            if (ds2.Tables[2].Rows.Count > 0)
            {
                rpBunchGroupAlias.Visible = true;
                rpBunchGroupAlias.DataSource = ds2.Tables[2];
                rpBunchGroupAlias.DataBind();
            }
            else
            {
                rpBunchGroupAlias.Visible = false;
            }

            if (ds2.Tables[4].Rows.Count > 0)
            {
                rpHorsePerformanceCalculator.Visible = true;
                rpHorsePerformanceCalculator.DataSource = ds2.Tables[4];
                rpHorsePerformanceCalculator.DataBind();
            }
            else
            {
                rpHorsePerformanceCalculator.Visible = false;
            }

            if (ds2.Tables[5].Rows.Count > 0)
            {
                rpDistancePerformanceCurrent.Visible = true;
                rpDistancePerformanceCurrent.DataSource = ds2.Tables[5];
                rpDistancePerformanceCurrent.DataBind();
            }
            else
            {
                rpDistancePerformanceCurrent.Visible = false;
            }





            var Label58 = (Label)e.Item.FindControl("Label58");
            var Label14 = (Label)e.Item.FindControl("Label14");

            if (!Label14.Text.Equals(""))
            {
                Label58.BackColor = Color.Orange;
            }
            else
            {
                Label58.BackColor = Color.White;
            }
        }

        protected void lvMasterBunchAlias_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var ds1 = new ReportBL().GetHorsePerformance(Convert.ToInt32(ViewState["HorseID"]), Request.QueryString["RaceDate"]);
            Repeater rpBunchPerformance = e.Item.FindControl("rpBunchPerformance") as Repeater;
            Repeater rpBunchClusterPerformance = e.Item.FindControl("rpBunchClusterPerformance") as Repeater;
            Repeater rpBunchGroupAlias = e.Item.FindControl("rpBunchGroupAlias") as Repeater;

            if (ds1.Tables[0].Rows.Count > 0)
            {
                rpBunchPerformance.Visible = true;
                rpBunchPerformance.DataSource = ds1.Tables[0];
                rpBunchPerformance.DataBind();
            }
            else
            {
                rpBunchPerformance.Visible = false;
            }
            if (ds1.Tables[1].Rows.Count > 0)
            {
                rpBunchClusterPerformance.Visible = true;
                rpBunchClusterPerformance.DataSource = ds1.Tables[1];
                rpBunchClusterPerformance.DataBind();
            }
            else
            {
                rpBunchClusterPerformance.Visible = false;
            }
            if (ds1.Tables[2].Rows.Count > 0)
            {
                rpBunchGroupAlias.Visible = true;
                rpBunchGroupAlias.DataSource = ds1.Tables[2];
                rpBunchGroupAlias.DataBind();
            }
            else
            {
                rpBunchGroupAlias.Visible = false;
            }

        }

        protected void lvFamilyTree_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var Label50 = (Label)e.Item.FindControl("Label50");
            var Label56 = (Label)e.Item.FindControl("Label56");
            var hdnfielddisperformatstyle = (HiddenField)e.Item.FindControl("hdnfielddisperformatstyle");
            var hdnfieldBunchGroupPerformanceStyleFormat = (HiddenField)e.Item.FindControl("hdnfieldBunchGroupPerformanceStyleFormat");
            Label50.Text = textFormatStyle(Label50.Text, hdnfielddisperformatstyle.Value);
            Label56.Text = textFormatStyle(Label56.Text, hdnfieldBunchGroupPerformanceStyleFormat.Value);


        }

        protected void lsviewhorseperformance_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hdnfieldDivisionRaceDateSeason = e.Item.FindControl("hdnfieldDivisionRaceDateSeason") as HiddenField;
            if (!horseid.Equals(ViewState["HorseID"].ToString()))
            {
                count = 1;
            }

            if (count == 1)
            {
                racedatesecond = hdnfieldDivisionRaceDateSeason.Value;
                horseid = ViewState["HorseID"].ToString();
            }
           


            var ds = new ReportBL().GetReportSwimmingTrckViet(horseid.ToString(), racedatesecond,hdnfieldDivisionRaceDateSeason.Value);
            if(ds.Tables[0].Rows.Count>0)
            {
                Repeater repeater1 = e.Item.FindControl("Repeater1") as Repeater;
                repeater1.DataSource = ds.Tables[0];
                repeater1.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {

                Repeater rpHorseHabitCurrentMissionForm = e.Item.FindControl("rpHorseHabitCurrentMissionForm") as Repeater;
                rpHorseHabitCurrentMissionForm.DataSource = ds.Tables[1];
                rpHorseHabitCurrentMissionForm.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {

                Repeater rpracecardconnectioncluster = e.Item.FindControl("rpracecardconnectioncluster") as Repeater;
                rpracecardconnectioncluster.DataSource = ds.Tables[2];
                rpracecardconnectioncluster.DataBind();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {

                Repeater rpFactFinding = e.Item.FindControl("rpFactFinding") as Repeater;
                rpFactFinding.DataSource = ds.Tables[3];
                rpFactFinding.DataBind();
            }

            if (ds.Tables[4].Rows.Count > 0)
            {

                Repeater rpCardTrack = e.Item.FindControl("rpCardTrack") as Repeater;
                rpCardTrack.DataSource = ds.Tables[4];
                rpCardTrack.DataBind();
            }

            if (ds.Tables[5].Rows.Count > 0)
            {

                Repeater rptHorseVeterinaryProblems = e.Item.FindControl("rptHorseVeterinaryProblems") as Repeater;
                rptHorseVeterinaryProblems.DataSource = ds.Tables[5];
                rptHorseVeterinaryProblems.DataBind();


            }

            if (ds.Tables[6].Rows.Count > 0)
            {

                Repeater rpTreadmill = e.Item.FindControl("rpTreadmill") as Repeater;
                rpTreadmill.DataSource = ds.Tables[6];
                rpTreadmill.DataBind();
            }

            if (ds.Tables[7].Rows.Count > 0)
            {

                Repeater rpStruckOut = e.Item.FindControl("RpStruckOut") as Repeater;
                rpStruckOut.DataSource = ds.Tables[7];
                rpStruckOut.DataBind();
            }
            if (count == 2 || count == 3 || count == 4 || count == 5 || count == 6 || count == 7 || count == 8 || count == 9 || count == 10
                || count == 11 || count == 12 || count == 13 || count == 14 || count == 15 || count == 16 || count == 17 
                    || count == 18 || count == 19)
            {
                racedatesecond = hdnfieldDivisionRaceDateSeason.Value;
            }

            count++;
        }

            protected string textFormatStyle(string value, string formatdetail)
        {
            var finaltext = string.Empty;
            StringBuilder str = new StringBuilder();
            try
            {
                string[] formatstyle = formatdetail.Split(',');
                string[] str1 = value.Split(']');
                if (str1.Length > 1)
                {
                    for (int count = 0; count < formatstyle.Length; count++)
                    {
                        if (str1.Length > 1)
                        {
                            if (!formatstyle[count].ToString().Equals(""))
                            {
                                if (formatstyle[count].ToString().TrimEnd().Contains("Cross Mark"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        //string explanation = "x";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "x");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Question Mark"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "?");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Exclamation Mark"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "!");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Tick Mark"))
                                {

                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {

                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='color:red;font-size:13px; font-weight: bold;'>{0}</span>", "\u2714");

                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Green Single Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:green;'>{0}</span>", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Green Double Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:green; text-decoration-style:double;'>{0}</span>", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Green Circle"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='border: 2px solid green; border-radius: 25px;padding:3px;'>{0}</span>", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Green Circle with Green Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:green; '> <span style='text-align: center; border: 2px solid green; padding:5px; border-radius: 25px;'>{0}</span></span >", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Green Circle with Red Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:red; '> <span style='text-align: center; border: 2px solid green; padding:5px; border-radius: 25px;'>{0}</span></span >", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Red Single Under Line") && str1[count].ToString().Contains("Red Single Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext += string.Format("<span style='text-decoration: underline; text-decoration-color:red;'>{0}</span>", str1[count].ToString() + "]");
                                    }
                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Red Double Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:red; text-decoration-style:double;'>{0}</span>", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Red Circle"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='border: 2px solid red; border-radius: 25px;padding:3px;'>{0}</span>", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Equals("Red Circle with Red Under Line"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:red; '> <span style='text-align: center; border: 2px solid red; padding:5px; border-radius: 25px;'>{0}</span></span >", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Red Thick Underline"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + string.Format("<span style='text-decoration: underline; text-decoration-color:red; border-bottom: 10px solid red;'>{0}</span>", str1[count].ToString() + "]");
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Blue Dot"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = ".";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='font-size:50px; font-weight: bold; color: blue'>{0}</span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Blue Dot with Red Thick Underline"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = ".";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='font-size: xx-large; font-weight: bold; text-decoration:underline; text-decoration-color:red;'><span style='font-size:50px; font-weight: bold; color: blue'>{0}</span></span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Green Dot"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = ".";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='font-size:50px; font-weight: bold; color: green'>{0}</span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Green Dot with Red Thick Underline"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = ".";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='font-size: xx-large; font-weight: bold; text-decoration:underline; text-decoration-color:red;'><span style='font-size:50px; font-weight: bold; color: green'>{0}</span></span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Red Dot"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = ".";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='font-size:50px; font-weight: bold; color: red'>{0}</span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Red Dot with Red Thick Underline"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = ".";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='font-size: xx-large; font-weight: bold; text-decoration:underline; text-decoration-color:red;'><span style='font-size:50px; font-weight: bold; color: red'>{0}</span></span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Positive"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = "+";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='color:red;'>{0}</span>", explanation);
                                    }

                                }
                                else if (formatstyle[count].ToString().TrimEnd().Contains("Negative"))
                                {
                                    string[] str2 = formatstyle[count].Split('=');
                                    for (int count1 = 0; count1 < str1.Length; count1++)
                                    {
                                        string explanation = "-";
                                        if (str1[count1].Contains(str2[0].ToString().TrimEnd()))
                                            finaltext = finaltext + str1[count].ToString() + "]" + string.Format("<span style='color:red;'>{0}</span>", explanation);
                                    }

                                }
                            }
                            else
                            {
                                finaltext = value;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

            return finaltext;
        }
    }
}