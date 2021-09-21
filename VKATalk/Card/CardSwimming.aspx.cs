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
    using VKATalk.Common;

    public partial class CardSwimming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
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
                //GetRaceGeneralRaceDetail
                var ds = new CardsBL().GetAcceptanceDivisionDetailMultipleReturn(
                     txtbxRaceDate.Text,
                     Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardSwimming");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblSeason.Text = ds.Tables[0].Rows[0][0].ToString();
                    lblYear.Text = ds.Tables[0].Rows[0][1].ToString();
                    //tblHorseEntryForm.Visible = true;
                }

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


                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    //lblSeason.Text = dt.Rows[0][2].ToString();
                //    //lblYear.Text = dt.Rows[0][3].ToString();
                //    //GvShowALL.DataSource = dt;
                //    //GvShowALL.DataBind();


                //}
                //else
                //{
                //   // dvgridview.Visible = false;
                //    GvShowALL.DataSource = new DataTable();
                //    GvShowALL.DataBind();
                //}

                // AcceptanceShow();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

    }
}