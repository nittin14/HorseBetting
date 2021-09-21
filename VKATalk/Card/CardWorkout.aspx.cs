using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;
using System.Data;

namespace VKATalk.Card
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.WebSockets;

    using OfficeOpenXml;

    public partial class CardWorkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();

                var dt = new CardsBL().GetCardWorkoutSourceName();
                if (dt.Rows.Count > 0)
                {
                    drpdwnSourceName.DataSource = dt;
                    drpdwnSourceName.DataTextField = "ProfessionalName";
                    drpdwnSourceName.DataValueField = "SourcePNameID";
                    drpdwnSourceName.DataBind();
                    drpdwnSourceName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    drpdwnSourceName.Focus();

                }
            }
        }

        protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            //ClearSelection();
            var dt = new CardsBL().GetRaceCenterName(txtbxRaceDate.Text);
            if (dt.Rows.Count > 0)
            {
                drpdwnCenterName.DataSource = dt;
                drpdwnCenterName.DataTextField = "CenterName";
                drpdwnCenterName.DataValueField = "ID";
                drpdwnCenterName.DataBind();
                drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                drpdwnCenterName.Focus();

            }
           


        }

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
                if (drpdwnCenterName.SelectedItem.Value.Equals("-1"))
                {
                    lblSeason.Text = string.Empty;
                    lblYear.Text = string.Empty;
                    drpdwnHorseName.ClearSelection();
                    drpdwnSourceName.ClearSelection();


                }
                else
                {
                    var dt1 = new CardsBL().GetWorkOutHorseInformation(txtbxRaceDate.Text);
                    if (dt1.Rows.Count > 0)
                    {
                        drpdwnHorseName.DataSource = dt1;
                        drpdwnHorseName.DataTextField = "HorseName";
                        drpdwnHorseName.DataValueField = "HorseID_FK";
                        drpdwnHorseName.DataBind();
                        drpdwnHorseName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        drpdwnHorseName.Focus();

                    }
                    var ds = new CardsBL().GetAcceptanceDivisionDetailMultipleReturn(
                         txtbxRaceDate.Text,
                         Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardWorkout");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblSeason.Text = ds.Tables[0].Rows[0][0].ToString();
                        lblYear.Text = ds.Tables[0].Rows[0][1].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void drpdwnHorseName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                if (drpdwnHorseName.SelectedItem.Value.Equals("-1"))
                {
                    GvShowALL.DataSource = new DataTable();
                    GvShowALL.DataBind();
                    drpdwnSourceName.ClearSelection();
                }
                else
                {
                    var ds = new CardsBL().GetCardTrackGridviewData(
                     txtbxRaceDate.Text,
                     Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), drpdwnSourceName.SelectedItem.Value, Convert.ToInt32(drpdwnHorseName.SelectedItem.Value));

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        GvShowALL.DataSource = ds.Tables[1];
                        GvShowALL.DataBind();
                    }
                    else
                    {
                        GvShowALL.DataSource = new DataTable();
                        GvShowALL.DataBind();
                    }
                }

                    


            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void drpdwnSourceName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                //GetRaceGeneralRaceDetail
                if (drpdwnSourceName.SelectedItem.Value.Equals("-1"))
                {
                    var ds = new CardsBL().GetCardTrackGridviewData(
                     txtbxRaceDate.Text,
                     Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), drpdwnSourceName.SelectedItem.Value, Convert.ToInt32(drpdwnHorseName.SelectedItem.Value));

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        GvShowALL.DataSource = ds.Tables[1];
                        GvShowALL.DataBind();
                    }
                    else
                    {
                        GvShowALL.DataSource = new DataTable();
                        GvShowALL.DataBind();
                    }
                }
                else
                {
                    var ds = new CardsBL().GetCardTrackGridviewDataWorkOutType(
                     txtbxRaceDate.Text,
                     Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), drpdwnSourceName.SelectedItem.Value, Convert.ToInt32(drpdwnHorseName.SelectedItem.Value));



                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        GvShowALL.DataSource = ds.Tables[1];
                        GvShowALL.DataBind();
                    }
                    else
                    {
                        GvShowALL.DataSource = new DataTable();
                        GvShowALL.DataBind();
                    }
                }
                    


            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
    }
}