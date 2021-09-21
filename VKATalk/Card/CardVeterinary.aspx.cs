using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;

namespace VKATalk.Card
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.WebSockets;

    using OfficeOpenXml;
    using VKATalk.Common;

    public partial class CardVeterinary : System.Web.UI.Page
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

        protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

        } 
        //private void AcceptanceShow()
        //{
        //    try
        //    {
        //       // var dt1 = new CardsBL().GetCardAcceptanceDivisionRace(Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text, Convert.ToInt32(ViewState["GeneralRaceNameID"]), "Acceptance");
        //        var dt1 = new CardsBL().GetDeclaration(Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32(ViewState["GeneralRaceNameID"]), ViewState["DivisionRaceName"].ToString(), "RaceReview", txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
        //        dtdivisioncount = dt1;
        //        if (dt1.Rows.Count > 0)
        //        {

        //            GvShowALL.DataSource = dt1;
        //            GvShowALL.DataBind();
        //        }
        //        else
        //        {
        //            GvShowALL.DataSource = new DataTable();
        //            GvShowALL.DataBind();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandling.SendErrorToText(ex);
        //        string message = "Issue in Record.";
        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        //    }
        //}

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                //GetRaceGeneralRaceDetail
                var ds = new CardsBL().GetCardVeterinary(
                     txtbxRaceDate.Text,
                     Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "CardVeterinary");
                if (ds.Tables[0].Rows.Count > 0)
                {
                   // dvgridview.Visible = true;
                    lblSeason.Text = ds.Tables[1].Rows[0][0].ToString();
                    lblYear.Text = ds.Tables[1].Rows[0][1].ToString();
                    GvShowALL.DataSource = ds.Tables[0];
                    GvShowALL.DataBind();
                }
                else
                {
                   // dvgridview.Visible = false;
                    GvShowALL.DataSource = new DataTable();
                    GvShowALL.DataBind();
                }

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


        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GridViewRow row = GvShowALL.SelectedRow;
                //var dataKey = GvShowALL.DataKeys[row.RowIndex];

                //if (dataKey != null)
                //{
                //    ViewState["GlobalID"] = dataKey.Value;
                //    DropDownList drpdwnPeddockConditionG = (DropDownList)row.FindControl("drpdwnPeddockConditionG");
                //    CheckBox chkbxGategoer = (CheckBox)row.FindControl("chkbxGategoer");
                //    TextBox txtbxSectionalPositionG = (TextBox)row.FindControl("txtbxSectionalPositionG");
                //    TextBox txtbxBandPosition = (TextBox)row.FindControl("txtbxBandPosition");
                //    TextBox txtbxLastSectionalPosition = (TextBox)row.FindControl("txtbxLastSectionalPosition");

                //    DropDownList drpdwnRunQualityG = (DropDownList)row.FindControl("drpdwnRunQualityG");
                //    DropDownList drpdwnTrainerOnBoardEffectG = (DropDownList)row.FindControl("drpdwnTrainerOnBoardEffectG");
                //    DropDownList drpdwnJockeyOnBoardEffectG = (DropDownList)row.FindControl("drpdwnJockeyOnBoardEffectG");
                //    DropDownList drpdwnUntryDuetoAnyMateG = (DropDownList)row.FindControl("drpdwnUntryDuetoAnyMateG");
                //    DropDownList drpdwnInBettingOrderG = (DropDownList)row.FindControl("drpdwnInBettingOrderG");

                //    var result = new CardsBL().CardRaceReview(Convert.ToInt32(ViewState["GlobalID"]), Convert.ToInt32(drpdwnPeddockConditionG.SelectedItem.Value), Convert.ToBoolean(chkbxGategoer.Checked),
                //        txtbxSectionalPositionG.Text, txtbxBandPosition.Text, txtbxLastSectionalPosition.Text, Convert.ToInt32(drpdwnRunQualityG.SelectedItem.Value),
                //        Convert.ToInt32(drpdwnTrainerOnBoardEffectG.SelectedItem.Value), Convert.ToInt32(drpdwnJockeyOnBoardEffectG.SelectedItem.Value), Convert.ToInt32(drpdwnUntryDuetoAnyMateG.SelectedItem.Value),
                //        Convert.ToInt32(drpdwnInBettingOrderG.SelectedItem.Value), 1, "Update");

                //    if (result == 2)
                //    {
                //        var message = "Record updated successfully.";
                //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //    }
                //    else
                //    {
                //        var message = "Issue in Record.";
                //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //    }
                //}

                //btnAdd.Text = "Update";
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

    }
}