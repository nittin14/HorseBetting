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

    public partial class RaceReview : System.Web.UI.Page
    {
        private int rownumber = 1;
        public DataTable dtdivisioncount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                grdvwRaceDetail.DataSource = new DataTable();
                grdvwRaceDetail.DataBind();
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }

        public void ClearSelection()
        {
            drpdwnCenterName.ClearSelection();
            lblSeason.Text = "";
            lblYear.Text = "";
            grdvwRaceDetail.DataSource = new DataTable();
            grdvwRaceDetail.DataBind();
            GvShowALL.DataSource = new DataTable();
            GvShowALL.DataBind();
            lblGeneralRaceName.Text = "";
        }

        protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            ClearSelection();
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

                DropDownList drpdwnPeddockConditionG = (e.Row.FindControl("drpdwnPeddockConditionG") as DropDownList);
                BindDropDown(drpdwnPeddockConditionG, "PeddockCondition", "PeddockCondition", "PeddockConditionMID");
                drpdwnPeddockConditionG.Items.Insert(0, new ListItem("Please select","0"));
                string peddockcondition = (e.Row.FindControl("lblPeddockConditionG") as Label).Text;
                if(!peddockcondition.Equals(""))
                drpdwnPeddockConditionG.Items.FindByValue(peddockcondition).Selected = true;


                DropDownList drpdwnRunQualityG = (e.Row.FindControl("drpdwnRunQualityG") as DropDownList);
                BindDropDown(drpdwnRunQualityG, "RunQuality", "RunQuality", "RunQualityMID");
                drpdwnRunQualityG.Items.Insert(0, new ListItem("Please select", "0"));
                string runquality = (e.Row.FindControl("lblRunQualityG") as Label).Text;
                if (!runquality.Equals(""))
                    drpdwnRunQualityG.Items.FindByValue(runquality).Selected = true;

                DropDownList drpdwnTrainerOnBoardEffectG = (e.Row.FindControl("drpdwnTrainerOnBoardEffectG") as DropDownList);
                BindDropDown(drpdwnTrainerOnBoardEffectG, "TrainerOnBoardEffort", "OnBoardEffort", "OnBoardEffortMID");
                drpdwnTrainerOnBoardEffectG.Items.Insert(0, new ListItem("Please select", "0"));
                string traineronboard = (e.Row.FindControl("lblTrainerOnBoardEffectG") as Label).Text;
                if (!traineronboard.Equals(""))
                    drpdwnTrainerOnBoardEffectG.Items.FindByValue(traineronboard).Selected = true;

                DropDownList drpdwnJockeyOnBoardEffectG = (e.Row.FindControl("drpdwnJockeyOnBoardEffectG") as DropDownList);
                BindDropDown(drpdwnJockeyOnBoardEffectG, "JockeyOnBoardEffort", "OnBoardEffort", "OnBoardEffortMID");
                drpdwnJockeyOnBoardEffectG.Items.Insert(0, new ListItem("Please select", "0"));
                string jockeyonboard = (e.Row.FindControl("lblJockeyOnBoardEffectG") as Label).Text;
                if (!jockeyonboard.Equals(""))
                    drpdwnJockeyOnBoardEffectG.Items.FindByValue(jockeyonboard).Selected = true;


                DropDownList drpdwnUntryDuetoAnyMateG = (e.Row.FindControl("drpdwnUntryDuetoAnyMateG") as DropDownList);
                BindDropDown(drpdwnUntryDuetoAnyMateG, "UntryDuetoAnyMate", "MateType", "MateTypeMID");
                drpdwnUntryDuetoAnyMateG.Items.Insert(0, new ListItem("Please select", "0"));
                string untrydue = (e.Row.FindControl("lblUntryDueG") as Label).Text;
                if (!untrydue.Equals(""))
                    drpdwnUntryDuetoAnyMateG.Items.FindByValue(untrydue).Selected = true;


                DropDownList drpdwnInBettingOrderG = (e.Row.FindControl("drpdwnInBettingOrderG") as DropDownList);
                BindDropDown(drpdwnInBettingOrderG, "InBettingOrder", "InBettingOrder", "InBettingOrderMID");
                drpdwnInBettingOrderG.Items.Insert(0, new ListItem("Please select", "0"));
                string bettingorder = (e.Row.FindControl("lblInBettingOrderG") as Label).Text;
                if (!bettingorder.Equals(""))
                    drpdwnInBettingOrderG.Items.FindByValue(bettingorder).Selected = true;

               
            }

        } 
        private void AcceptanceShow()
        {
            try
            {
               // var dt1 = new CardsBL().GetCardAcceptanceDivisionRace(Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text, Convert.ToInt32(ViewState["GeneralRaceNameID"]), "Acceptance");
                var dt1 = new CardsBL().GetDeclaration(Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32(ViewState["GeneralRaceNameID"]), ViewState["DivisionRaceName"].ToString(), "RaceReview", txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                dtdivisioncount = dt1.Tables[0];
                if (dt1.Tables[0].Rows.Count > 0)
                {

                    GvShowALL.DataSource = dt1;
                    GvShowALL.DataBind();
                }
                else
                {
                    GvShowALL.DataSource = new DataTable();
                    GvShowALL.DataBind();
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new CardsBL().GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }


        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddHorseList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HandicapHorseName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //  horseList.Add(dt.Rows[i][1].ToString());
                horseList.Add(
                    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }


        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                HiddenField hdnfieldGeneralRaceNameIDG = (HiddenField)row.FindControl("hdnfieldGeneralRaceNameIDG");
                HiddenField hdnfieldgeneralraceid = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");
                HiddenField hdnfielddivisionracename = (HiddenField)row.FindControl("hdnfielddivisionracename");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["DivisionRaceID"] = dataKey.Value; //generalraceid
                    ViewState["GeneralRaceNameID"] = hdnfieldGeneralRaceNameIDG.Value; //generalraceid
                    ViewState["GeneralRaceID"] = hdnfieldgeneralraceid.Value; //generalraceid
                    ViewState["DivisionRaceName"] = hdnfielddivisionracename.Value;//reneralracename
                    ViewState["SerialNumber"] = row.Cells[0].Text;
                    hdnfieldGeneralRaceNameID.Value = ViewState["GeneralRaceNameID"].ToString();
                }
                AcceptanceShow();
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                //GetRaceGeneralRaceDetail
                var dt = new CardsBL().GetAcceptanceDivisionDetail(
                     txtbxRaceDate.Text,
                     Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "RaceReview");
                if (dt.Rows.Count > 0)
                {
                    dvgridview.Visible = true;
                    lblSeason.Text = dt.Rows[0][7].ToString();
                    lblYear.Text = dt.Rows[0][8].ToString();
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
                else
                {
                    dvgridview.Visible = false;
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();
                    if (Convert.ToInt32(drpdwnCenterName.SelectedItem.Value).Equals(-1))
                    {
                        GvShowALL.DataSource = new DataTable();
                        GvShowALL.DataBind();
                    }
                    ClearSelection();
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


        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtbxRaceDate.Text = "";
            drpdwnCenterName.ClearSelection();
            grdvwRaceDetail.DataSource = new DataTable();
            grdvwRaceDetail.DataBind();

            GvShowALL.DataSource = new DataTable();
            GvShowALL.DataBind();

        }

        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GvShowALL.SelectedRow;
                var dataKey = GvShowALL.DataKeys[row.RowIndex];

                if (dataKey != null)
                {
                    ViewState["GlobalID"] = dataKey.Value;
                    DropDownList drpdwnPeddockConditionG = (DropDownList)row.FindControl("drpdwnPeddockConditionG");
                    CheckBox chkbxGategoer = (CheckBox)row.FindControl("chkbxGategoer");
                    TextBox txtbxSectionalPositionG = (TextBox)row.FindControl("txtbxSectionalPositionG");
                    TextBox txtbxBandPosition = (TextBox)row.FindControl("txtbxBandPosition");
                    TextBox txtbxLastSectionalPosition = (TextBox)row.FindControl("txtbxLastSectionalPosition");

                    DropDownList drpdwnRunQualityG = (DropDownList)row.FindControl("drpdwnRunQualityG");
                    DropDownList drpdwnTrainerOnBoardEffectG = (DropDownList)row.FindControl("drpdwnTrainerOnBoardEffectG");
                    DropDownList drpdwnJockeyOnBoardEffectG = (DropDownList)row.FindControl("drpdwnJockeyOnBoardEffectG");
                    DropDownList drpdwnUntryDuetoAnyMateG = (DropDownList)row.FindControl("drpdwnUntryDuetoAnyMateG");
                    DropDownList drpdwnInBettingOrderG = (DropDownList)row.FindControl("drpdwnInBettingOrderG");

                    var result = new CardsBL().CardRaceReview(Convert.ToInt32(ViewState["GlobalID"]), Convert.ToInt32(drpdwnPeddockConditionG.SelectedItem.Value), Convert.ToBoolean(chkbxGategoer.Checked),
                        txtbxSectionalPositionG.Text, txtbxBandPosition.Text, txtbxLastSectionalPosition.Text, Convert.ToInt32(drpdwnRunQualityG.SelectedItem.Value),
                        Convert.ToInt32(drpdwnTrainerOnBoardEffectG.SelectedItem.Value), Convert.ToInt32(drpdwnJockeyOnBoardEffectG.SelectedItem.Value), Convert.ToInt32(drpdwnUntryDuetoAnyMateG.SelectedItem.Value),
                        Convert.ToInt32(drpdwnInBettingOrderG.SelectedItem.Value), 1, "Update");

                    if (result == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        var message = "Issue in Record.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }

                //btnAdd.Text = "Update";
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dtHandicap = new DataTable("Acceptance");
            try
            {

                //Add columns to DataTable.
                dtHandicap.Columns.Add("DataEntryDate", typeof(string));
                dtHandicap.Columns.Add("DivisionRaceID_Fk", typeof(int));
                dtHandicap.Columns.Add("HorseID", typeof(int));
                dtHandicap.Columns.Add("PaddockConditionMID", typeof(int));
                dtHandicap.Columns.Add("EarlyGategoer", typeof(Boolean));
                dtHandicap.Columns.Add("FirstSectionalPosition", typeof(string));
                dtHandicap.Columns.Add("BandPosition", typeof(string));
                dtHandicap.Columns.Add("LastSectionalPosition", typeof(string));
                dtHandicap.Columns.Add("RunQualityMID", typeof(int));
                dtHandicap.Columns.Add("TrainerOnBoardEffortMID", typeof(int));
                dtHandicap.Columns.Add("JockeyOnBoardEffortMID", typeof(int));
                dtHandicap.Columns.Add("MateTypeMID", typeof(int));
                dtHandicap.Columns.Add("InBettingOrderMID", typeof(int));
                dtHandicap.Columns.Add("PerformanceUpdated", typeof(string));
                dtHandicap.Columns.Add("CreatedDate", typeof(DateTime));
                dtHandicap.Columns.Add("CreatedUserID", typeof(int));
                dtHandicap.Columns.Add("IsActive", typeof(int));
                
                int count = 0;
                decimal gbccount = 0;
                var lowerraised = string.Empty;
                decimal lowerraisedvalue = 0;
                int rowcount = 0;
                int rowcountacceptance = 0;
                foreach (GridViewRow row in GvShowALL.Rows)
                {
                        dtHandicap.Rows.Add();
                        if (txtbxRaceDate.Text.Equals("__-__-____"))
                        {
                            dtHandicap.Rows[rowcount][0] = DBNull.Value;
                        }
                        else
                        {
                            string[] dateString = txtbxRaceDate.Text.Split('-');
                            DateTime enterDate =
                                Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                            dtHandicap.Rows[rowcount][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                        }
                        dtHandicap.Rows[rowcount][1] = Convert.ToInt32(ViewState["DivisionRaceID"]);
                        dtHandicap.Rows[rowcount][2] = Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value);
                        DropDownList drpdwnPeddockConditionG = (DropDownList)row.FindControl("drpdwnPeddockConditionG");
                        dtHandicap.Rows[rowcount][3] = Convert.ToInt32(drpdwnPeddockConditionG.SelectedItem.Value);
                        CheckBox chkbxGategoer = (CheckBox)row.FindControl("chkbxGategoer");
                        dtHandicap.Rows[rowcount][4] = Convert.ToInt32(chkbxGategoer.Checked);
                        dtHandicap.Rows[rowcount][5] = (row.FindControl("txtbxSectionalPositionG") as TextBox).Text;
                        dtHandicap.Rows[rowcount][6] = (row.FindControl("txtbxBandPosition") as TextBox).Text;
                        dtHandicap.Rows[rowcount][7] = (row.FindControl("txtbxLastSectionalPosition") as TextBox).Text;
                        DropDownList drpdwnRunQualityG = (DropDownList)row.FindControl("drpdwnRunQualityG");
                        dtHandicap.Rows[rowcount][8] = Convert.ToInt32(drpdwnRunQualityG.SelectedItem.Value);
                        DropDownList drpdwnTrainerOnBoardEffectG = (DropDownList)row.FindControl("drpdwnTrainerOnBoardEffectG");
                        dtHandicap.Rows[rowcount][9] = Convert.ToInt32(drpdwnTrainerOnBoardEffectG.SelectedItem.Value);
                        DropDownList drpdwnJockeyOnBoardEffectG = (DropDownList)row.FindControl("drpdwnJockeyOnBoardEffectG");
                        dtHandicap.Rows[rowcount][10] = Convert.ToInt32(drpdwnJockeyOnBoardEffectG.SelectedItem.Value);
                        DropDownList drpdwnUntryDuetoAnyMateG = (DropDownList)row.FindControl("drpdwnUntryDuetoAnyMateG");
                        dtHandicap.Rows[rowcount][11] = Convert.ToInt32(drpdwnUntryDuetoAnyMateG.SelectedItem.Value);
                        DropDownList drpdwnInBettingOrderG = (DropDownList)row.FindControl("drpdwnInBettingOrderG");
                        dtHandicap.Rows[rowcount][12] = Convert.ToInt32(drpdwnInBettingOrderG.SelectedItem.Value);
                        dtHandicap.Rows[rowcount][13] = string.Empty;
                        dtHandicap.Rows[rowcount][14] = DateTime.Now;
                        dtHandicap.Rows[rowcount][15] = 1;
                        dtHandicap.Rows[rowcount][16] = 1;
                        rowcount++;
                }





                var result = new CardsBL().AddRaceReview(dtHandicap, Convert.ToInt32(ViewState["DivisionRaceID"]));
                if (result == 1)
                {
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (result == 4)
                {
                    var message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var message = "Issue in Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnHandicapShow_Click(object sender, EventArgs e)
        {

            try
            {

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearAll();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }


        protected void ClearAll()
        {
            lblSeason.Text = "";
            lblYear.Text = "";
            if (Convert.ToInt32(drpdwnCenterName.SelectedItem.Value).Equals(-1))
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }


        protected void drpdwnDisplay_SelectIndexChange(object sender, EventArgs e)
        {
            try{

                BindData();
             }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            GvShowALL.PageIndex = e.NewPageIndex;
            GvShowALL.DataBind();

        }

        private void BindData()
        {
            DataSet ds = null;
            ds = new CardsBL().GetDisplayGridviewData(txtbxRaceDate.Text, Convert.ToInt32(0), "", "Acceptance");
            GvShowALL.DataSource = ds.Tables[0];
            GvShowALL.DataBind();

        }

        protected void RowEdit(object sender, GridViewEditEventArgs e)
        {
            GvShowALL.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvShowALL.EditIndex = -1;
            BindData();
        }

        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                int id = (int)GvShowALL.DataKeys[e.RowIndex].Value;
                
                HiddenField hdnfieldgeneralracenameid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldGeneralRacenameid") as HiddenField;
                int generalracenameid = Convert.ToInt32(hdnfieldgeneralracenameid.Value);

                HiddenField hdnfieldgeneralraceid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldGeneralRaceid") as HiddenField;
                int generalraceid = Convert.ToInt32(hdnfieldgeneralraceid.Value);

                HiddenField hdnfieldgeneralracedate = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldGeneralRaceDate") as HiddenField;
                string generalracedate = hdnfieldgeneralracedate.Value;

                HiddenField hdnfielddivisionraceid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldDivisionRaceID") as HiddenField;
                int divisionraceid = Convert.ToInt32(hdnfielddivisionraceid.Value);

                HiddenField hdnfieldacceptanceid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldAcceptanceID") as HiddenField;
                int acceptanceid = Convert.ToInt32(hdnfieldacceptanceid.Value);

                HiddenField hdnfieldacceptancestruckoutid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldAcceptanceStruckOutID") as HiddenField;
                int acceptancestruckoutid = Convert.ToInt32(hdnfieldacceptancestruckoutid.Value);

                string generalracename = ((Label)GvShowALL.Rows[e.RowIndex].FindControl("lblGeneralRaceName")).Text;
                string divisionracename = ((DropDownList)GvShowALL.Rows[e.RowIndex].FindControl("drpdwnBifurcationS")).SelectedItem.Text;
                string hno = ((TextBox)GvShowALL.Rows[e.RowIndex].FindControl("txtHNoS")).Text;
                string awgbcs = ((TextBox)GvShowALL.Rows[e.RowIndex].FindControl("txtAWGBCS")).Text;
                string strucouttype = ((DropDownList)GvShowALL.Rows[e.RowIndex].FindControl("drpdwnStruckOuttypeS")).SelectedItem.Text;
                int result = new CardsBL().AcceptanceUpdate(id,generalracename,generalracenameid,generalracedate,divisionraceid,divisionracename,Convert.ToInt32(hno),awgbcs,strucouttype,acceptanceid,acceptancestruckoutid,0,0);
                if (result == 1)
                {
                    GvShowALL.EditIndex = -1;
                    BindData();
                    var message = "Record Added Successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    
                }
                else
                {
                    var message = "Issue in Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
            //string CustomerID = ((Label)GridView1.Rows[e.RowIndex]

            //                    .FindControl("lblCustomerID")).Text;

            //string Name = ((TextBox)GridView1.Rows[e.RowIndex]

            //                    .FindControl("txtContactName")).Text;

            //string Company = ((TextBox)GridView1.Rows[e.RowIndex]

            //                    .FindControl("txtCompany")).Text;

            //SqlConnection con = new SqlConnection(strConnString);

            //SqlCommand cmd = new SqlCommand();

            //cmd.CommandType = CommandType.Text;

            //cmd.CommandText = "update customers set ContactName=@ContactName," +

            // "CompanyName=@CompanyName where CustomerID=@CustomerID;" +

            // "select CustomerID,ContactName,CompanyName from customers";

            //cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar).Value = CustomerID;

            //cmd.Parameters.Add("@ContactName", SqlDbType.VarChar).Value = Name;

            //cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Company;

            //GridView1.EditIndex = -1;

            //GridView1.DataSource = GetData(cmd);

            //GridView1.DataBind();

        }

        
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Acceptance"))
                    {
                        string Extension = Path.GetExtension(flupload.PostedFile.FileName);
                        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                        bool exists = System.IO.Directory.Exists(Server.MapPath(FolderPath));
                        if (!exists) System.IO.Directory.CreateDirectory(Server.MapPath(FolderPath));

                        string FilePath = Server.MapPath(FolderPath + FileName);
                        flupload.SaveAs(FilePath);
                        Import_To_Grid(FilePath, Extension);
                    }
                    else
                    {
                        var message = "Please select the correct File.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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

        private void Import_To_Grid(string FilePath, string Extension)
        {
            try
            {
                string strConn;
                bool hasHeaders = true;
                string HDR = hasHeaders ? "Yes" : "No";
                if (FilePath.Substring(FilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                OleDbConnection connExcel = new OleDbConnection(strConn);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = string.Empty;
                if (dtExcelSchema.Rows[0]["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                {
                    SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();

                }
                else
                {
                    SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                }
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count > 0)
                {
                    var dtErrorResult = new CardsBL().Import30(dt, "Card_Acceptance", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Acceptance");

                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dtErrorResult.Columns.Count - 1);
                            //  int colend = colstart;
                            rowend = rowstart + dtErrorResult.Rows.Count;
                            ws.Cells[rowstart, colstart].LoadFromDataTable(dtErrorResult, true);
                            int i = 1;
                            foreach (DataColumn dc in dtErrorResult.Columns)
                            {
                                i++;
                                if (dc.DataType == typeof(decimal)) ws.Column(i).Style.Numberformat.Format = "#0.00";
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                    ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                        ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style =
                                            OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Acceptance.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        //BindData();
                        var message1 = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
                    }
                }
                else
                {
                    var message = "No Record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }


        }


        /// <summary>
        /// Export Gridview Data in Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_Acceptance"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Acceptance");
                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dt.Columns.Count - 1);
                            rowend = rowstart + dt.Rows.Count;
                            ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                            int i = 1;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                i++;
                                if (dc.DataType == typeof(decimal))
                                    ws.Column(i).Style.Numberformat.Format = "#0.00";
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                               ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                               ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                               ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Acceptance.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
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