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

    public partial class Handicap : System.Web.UI.Page
    {
        private int rownumber = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                grdvwRaceDetail.DataSource = new DataTable();
                grdvwRaceDetail.DataBind();
            }

    
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

        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //BtnSubmit.Text = "Update";
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                HiddenField hdnvaluegeneralraceid = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");
                HiddenField hdnfieldhandicapratingrange = (HiddenField)row.FindControl("hdnfieldHandicapRatingRange");
				HiddenField hdnfieldClassTypeID = (HiddenField)row.FindControl("hdnfieldClassTypeID");
                HiddenField hdnfieldRaceType = (HiddenField)row.FindControl("hdnfieldRaceType");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["GridViewRowID"] = dataKey.Value; //generalraceid
                    ViewState["GeneralRaceID"] = hdnvaluegeneralraceid.Value; //generalraceid
                    ViewState["GridViewRaceName"] = hdnval.Value;//reneralracename
                    hdnfieldGeneralRaceNameID.Value = Convert.ToString(dataKey.Value);
                    Session["GeneralRaceNameID"] = hdnfieldGeneralRaceNameID.Value;
                    Session["CenterID"] = drpdwnCenterName.SelectedItem.Value;
                    Session["RaceDate"] = txtbxRaceDate.Text;
					ViewState["ClassTypeID"] = hdnfieldClassTypeID.Value;//reneralracename
                    ViewState["RaceType"] = hdnfieldRaceType.Value;//reneralracename
                    lblRaceNameShow.Text = hdnval.Value;
                }

              //  var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(dataKey.Value), "Handicap");
				var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(dataKey.Value), "Handicap", lblSeason.Text, lblYear.Text);
				if (dsGeneralDate.Tables[0].Rows.Count > 0)
                {
                    if (dsGeneralDate.Tables[0].Rows.Count > 0)
                    {
                        lblHandicapDate.Text = dsGeneralDate.Tables[0].Rows[0][0].ToString();
                    }
					else
					{
						lblHandicapDate.Text = string.Empty;
					}
                }
				else
				{
					lblHandicapDate.Text = string.Empty;
				}
				lblRecordTable.Text = "Entry";
                
                
                DataSet ds= new CardsBL().HandicapHorseInformationEntry(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), 
                    Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectHandicap", 0, 1, lblYear.Text, lblSeason.Text, 
                            hdnfieldhandicapratingrange.Value, "Nothing", Convert.ToDecimal(0),txtbxHandicapEnterDate.Text, ViewState["ClassTypeID"].ToString(),
                            hdnfieldRaceType.Value);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdvwHorseDetail.DataSource = ds.Tables[0];
                    grdvwHorseDetail.DataBind();
                    var localrownumber = 0;
                    for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                    {
                        localrownumber = Convert.ToInt32(ds.Tables[0].Rows[count][1]);
                    }
                }
                else
                {
                    grdvwHorseDetail.DataSource = new DataTable();
                    grdvwHorseDetail.DataBind();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    chkboxRaisedLowered.ClearSelection();
                    txtbxRaisedLoweredValue.Text = string.Empty;
                    //var loweredtext = ds.Tables[1].Rows[0]["HdWghRaisedLowered"];
                    if (!ds.Tables[1].Rows[0]["HdWghRaisedLowered"].ToString().Equals(""))
                    {
                        chkboxRaisedLowered.Items.FindByText(ds.Tables[1].Rows[0]["HdWghRaisedLowered"].ToString()).Selected = true;
                        txtbxRaisedLoweredValue.Text = ds.Tables[1].Rows[0]["HdWghRaisedLoweredValue"].ToString();
                    }
                    
                    //var loweredtextvalue = ds.Tables[1].Rows[0]["HdWghRaisedLoweredValue"];
                    DataSet ds1;
                    if (!ds.Tables[1].Rows[0]["HdWghRaisedLowered"].ToString().Equals(""))
                    {
                        ds1 = new CardsBL().HandicapHorseInformationEntry(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), 
                                Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectHandicap", 0, 1, 
                                lblYear.Text, lblSeason.Text, hdnfieldhandicapratingrange.Value, ds.Tables[1].Rows[0]["HdWghRaisedLowered"].ToString(), 
                                Convert.ToDecimal(txtbxRaisedLoweredValue.Text), txtbxHandicapEnterDate.Text, ViewState["ClassTypeID"].ToString(), 
                                hdnfieldRaceType.Value);
                    }
                    else
                    {
                        ds1 = new CardsBL().HandicapHorseInformationEntry(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), 
                            Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectHandicap", 0, 1, lblYear.Text, lblSeason.Text, 
                            hdnfieldhandicapratingrange.Value, "", Convert.ToDecimal(0.0), txtbxHandicapEnterDate.Text, 
                            ViewState["ClassTypeID"].ToString(), hdnfieldRaceType.Value);
                    }

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        grdvwHorseDetail.DataSource = ds1.Tables[0];
                        grdvwHorseDetail.DataBind();
                        var localrownumber = 0;
                        for (int count = 0; count < ds1.Tables[0].Rows.Count; count++)
                        {
                            localrownumber = Convert.ToInt32(ds1.Tables[0].Rows[count][1]);
                        }
                    }
                    else
                    {
                        grdvwHorseDetail.DataSource = new DataTable();
                        grdvwHorseDetail.DataBind();
                    }
                }
                else
                {
                    chkboxRaisedLowered.ClearSelection();
                    txtbxRaisedLoweredValue.Text = string.Empty;
                }
                HandicapShow();
                
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        private void HandicapShow()
        {
            try
            {
            var dt1 = new CardsBL().HandicapHorseInformation(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectHandicap", 0, 1, lblYear.Text, lblSeason.Text,"", "Nothing", Convert.ToDecimal(0));
            if (dt1.Rows.Count > 0)
            {
                GvHandicap.DataSource = dt1;
                GvHandicap.DataBind();
            }
            else
            {
                GvHandicap.DataSource = new DataTable();
                GvHandicap.DataBind();
            }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
        protected void btnAddWeight_Click(object sender, EventArgs e)
        {
            try
            {
                var lowerraised = string.Empty;
                for (int i = 0; i < chkboxRaisedLowered.Items.Count; i++)
                {
                    if (!(chkboxRaisedLowered.SelectedIndex == -1))
                    {
                        lowerraised = chkboxRaisedLowered.SelectedItem.Text;
                    }
                }
                if (lowerraised.Equals(""))
                {
                    string message = "Please select Handicap Weight Raised or Lowered.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    lblRecordTable.Text = "Entry";
                    DataSet ds = new CardsBL().HandicapHorseInformationEntry(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), 
                        Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectHandicap", 0, 1, lblYear.Text, lblSeason.Text, "", 
                        lowerraised, Convert.ToDecimal(txtbxRaisedLoweredValue.Text), txtbxHandicapEnterDate.Text, ViewState["ClassTypeID"].ToString(),
                        ViewState["RaceType"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdvwHorseDetail.DataSource = ds.Tables[0];
                        grdvwHorseDetail.DataBind();
                        var localrownumber = 0;
                        for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                        {
                            localrownumber = Convert.ToInt32(ds.Tables[0].Rows[count][1]);
                        }
                    }
                    else
                    {
                        grdvwHorseDetail.DataSource = new DataTable();
                        grdvwHorseDetail.DataBind();
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        chkboxRaisedLowered.Items.FindByText(ds.Tables[1].Rows[0]["HdWghRaisedLowered"].ToString()).Selected = true;
                        txtbxRaisedLoweredValue.Text = ds.Tables[1].Rows[0]["HdWghRaisedLoweredValue"].ToString();
                        
                    }
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
            dt = new ProspectusBL().GetDropdownBind(TableName_);
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


        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
				ClearSelection();
				var dt = new CardsBL().GetRaceGeneralRaceDetail(
                    txtbxRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    lblSeason.Text = dt.Rows[0][11].ToString();
                    lblYear.Text = dt.Rows[0][12].ToString();
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

		public void ClearSelection()
		{
			lblSeason.Text = "";
			lblYear.Text = "";
			grdvwRaceDetail.DataSource = new DataTable();
			grdvwRaceDetail.DataBind();
			grdvwHorseDetail.DataSource = new DataTable();
			grdvwHorseDetail.DataBind();
			GvHandicap.DataSource = new DataTable();
			GvHandicap.DataBind();
			lblRaceNameShow.Text = "";
			lblHandicapDate.Text = "";
			chkboxRaisedLowered.ClearSelection();
			txtbxRaisedLoweredValue.Text = string.Empty;
		}

		protected void btnClear_Click(object sender, EventArgs e)
        {
            //txtbxHorseName.Text = "";
            //drpdwnGender.ClearSelection();
            //txtbxTrainer.Text = "";
            //txtbxOwner.Text = "";
            //hdnfieldOwnerID.Value = "";
            //hdnfieldTrainerID.Value = "";
            //hdnfieldhorseid.Value = "";
        }

        protected void btnShuffle_Click(object sender, EventArgs e)
        {
			try
			{
				var generalraceid = 0;
				var generalracenameid = 0;
				var chkbxlowupper = string.Empty;
				var chkbxlowupperValue = string.Empty;

				generalraceid = Convert.ToInt32(ViewState["GeneralRaceID"]);
				generalracenameid = Convert.ToInt32(hdnfieldGeneralRaceNameID.Value);


				if (chkboxRaisedLowered.SelectedIndex == -1)
				{
					chkbxlowupper = string.Empty;
				}
				else
				{
					chkbxlowupper = chkboxRaisedLowered.SelectedItem.Text;
				}

				if (!(txtbxRaisedLoweredValue.Text.Equals("")))
				{
					chkbxlowupperValue = txtbxRaisedLoweredValue.Text;
				}
				else
				{
					chkbxlowupperValue = string.Empty;
				}
				new CardsBL().HandicapUpdate(generalraceid.ToString(), generalracenameid.ToString(), chkbxlowupper, chkbxlowupperValue);

				HandicapShow();
				chkboxRaisedLowered.ClearSelection();
				txtbxRaisedLoweredValue.Text = string.Empty;
				var message = "Record updated successfully.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);



				var lowerraised = string.Empty;
				for (int i = 0; i < chkboxRaisedLowered.Items.Count; i++)
				{
					if (!(chkboxRaisedLowered.SelectedIndex == -1))
					{
						lowerraised = chkboxRaisedLowered.SelectedItem.Text;
					}
				}
				if (lowerraised.Equals(""))
				{
					string message1 = "Please select Handicap Weight Raised or Lowered.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
				}
				else
				{
					lblRecordTable.Text = "Entry";
					DataSet ds = new CardsBL().HandicapHorseInformationEntry(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectHandicap", 0, 1, lblYear.Text, lblSeason.Text, "", lowerraised, Convert.ToDecimal(txtbxRaisedLoweredValue.Text), txtbxHandicapEnterDate.Text, ViewState["ClassTypeID"].ToString(), ViewState["RaceType"].ToString());
					if (ds.Tables[0].Rows.Count > 0)
					{
						grdvwHorseDetail.DataSource = ds.Tables[0];
						grdvwHorseDetail.DataBind();
						var localrownumber = 0;
						for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
						{
							localrownumber = Convert.ToInt32(ds.Tables[0].Rows[count][1]);
						}
					}
					else
					{
						grdvwHorseDetail.DataSource = new DataTable();
						grdvwHorseDetail.DataBind();
					}

					if (ds.Tables[1].Rows.Count > 0)
					{
						chkboxRaisedLowered.Items.FindByText(ds.Tables[1].Rows[0]["HdWghRaisedLowered"].ToString()).Selected = true;
						txtbxRaisedLoweredValue.Text = ds.Tables[1].Rows[0]["HdWghRaisedLoweredValue"].ToString();

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Create a new DataTable.
            DataTable dtHandicap = new DataTable("Handicap");
            try
            {

				//Add columns to DataTable.
				dtHandicap.Columns.Add("HandicapEnterDate", typeof(DateTime));//0
				dtHandicap.Columns.Add("RaceDate", typeof(DateTime));
				dtHandicap.Columns.Add("GeneralRaceID", typeof(int));
				dtHandicap.Columns.Add("GeneralRaceNameID", typeof(int));
				dtHandicap.Columns.Add("HrSrNo", typeof(int));//4
				dtHandicap.Columns.Add("HorseID", typeof(int));
				dtHandicap.Columns.Add("HorseNameID", typeof(int));//6
				dtHandicap.Columns.Add("Age", typeof(int));
				dtHandicap.Columns.Add("GenderID", typeof(int));
				dtHandicap.Columns.Add("HandicapRating", typeof(string));
				dtHandicap.Columns.Add("MyHandicapRating", typeof(string)); //11
				dtHandicap.Columns.Add("HdWghHRSOW", typeof(string));
				dtHandicap.Columns.Add("HdWghMyHRSOW", typeof(string));
				dtHandicap.Columns.Add("HdWghAPGender", typeof(string));//14

				dtHandicap.Columns.Add("HWAC", typeof(string));//15

				dtHandicap.Columns.Add("TotalHdWghPenalty", typeof(string));//16
				dtHandicap.Columns.Add("HdWghHWP", typeof(string));//17
				dtHandicap.Columns.Add("MyHdWghHWP", typeof(string));
				dtHandicap.Columns.Add("HdWghAPGenderHWP", typeof(string));
				dtHandicap.Columns.Add("HWACHWP", typeof(string));//20
				dtHandicap.Columns.Add("HdWghRaisedLowered", typeof(string));
				dtHandicap.Columns.Add("HdWghRaisedLoweredValue", typeof(string));
				dtHandicap.Columns.Add("FinalHdWghAWRL", typeof(string));//23
				dtHandicap.Columns.Add("FinalMyHdWghAWRL", typeof(string));
				dtHandicap.Columns.Add("FinalHdWghAPGenderAWRL", typeof(string));
				dtHandicap.Columns.Add("FHWACAWRL", typeof(string));//26
				dtHandicap.Columns.Add("HdWghGBC", typeof(string));
				dtHandicap.Columns.Add("CreatedDate", typeof(DateTime));//28
				dtHandicap.Columns.Add("CreatedUserID", typeof(int));
				dtHandicap.Columns.Add("IsActive", typeof(int));
				dtHandicap.Columns.Add("CenterID", typeof(int));//31
				dtHandicap.Columns.Add("BanDetail", typeof(string));//32
				var breakgbc = 0;
                var rows = 0;
                foreach (GridViewRow row in grdvwHorseDetail.Rows)
				{
                    var duplicatecheck = new CardsBL().HandicapDuplicateCheck(Convert.ToInt32(ViewState["GeneralRaceID"]), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value),
                            Convert.ToInt32((row.FindControl("hdnfieldhorseid") as HiddenField).Value));
                    if (duplicatecheck == 0)
                    {
                        dtHandicap.Rows.Add();
                        
                        if (txtbxHandicapEnterDate.Text.Equals("__-__-____"))
                        {
                            dtHandicap.Rows[rows][0] = DBNull.Value;
                        }
                        else
                        {
                            string[] dateString = txtbxHandicapEnterDate.Text.Split('-');
                            DateTime enterDate =
                                Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                            dtHandicap.Rows[rows][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                        }
                        dtHandicap.Rows[rows][1] = txtbxRaceDate.Text;
                        dtHandicap.Rows[rows][2] = Convert.ToInt32(ViewState["GeneralRaceID"]);
                        dtHandicap.Rows[rows][3] = Convert.ToInt32(hdnfieldGeneralRaceNameID.Value);
                        if (row.Cells[1].Text.Equals("") || row.Cells[1].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][4] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][4] = row.Cells[1].Text;
                        }
                        dtHandicap.Rows[rows][5] = Convert.ToInt32((row.FindControl("hdnfieldhorseid") as HiddenField).Value);
                        dtHandicap.Rows[rows][6] = Convert.ToInt32((row.FindControl("hdnfieldhorsenameid") as HiddenField).Value);
                        if (row.Cells[3].Text.Equals("") || row.Cells[3].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][7] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][7] = row.Cells[3].Text;
                        }
                        if ((row.FindControl("hdnfieldhorsesexid") as HiddenField).Value.Equals(""))
                        {
                            dtHandicap.Rows[rows][8] = Convert.ToInt32(0);
                        }
                        else
                        {
                            dtHandicap.Rows[rows][8] = Convert.ToInt32((row.FindControl("hdnfieldhorsesexid") as HiddenField).Value);
                        }



                        if (!(row.FindControl("txtbxGvHandicapRating") as TextBox).Text.Equals(""))
                        {
                            dtHandicap.Rows[rows][9] = (row.FindControl("txtbxGvHandicapRating") as TextBox).Text;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][9] = string.Empty;
                        }

                        if (row.Cells[6].Text.Equals("") || row.Cells[6].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][10] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][10] = row.Cells[6].Text;
                        }
                        if (row.Cells[7].Text.Equals("") || row.Cells[7].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][11] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][11] = row.Cells[7].Text;
                        }
                        if (row.Cells[8].Text.Equals("") || row.Cells[8].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][12] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][12] = row.Cells[8].Text;
                        }
                        if (row.Cells[9].Text.Equals("") || row.Cells[9].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][13] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][13] = row.Cells[9].Text;
                        }
                        //dtHandicap.Columns.Add("HWAC", typeof(string));//14
                        if (row.Cells[10].Text.Equals("") || row.Cells[10].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][14] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][14] = row.Cells[10].Text;
                        }
                        //Total HWP, ("TotalHdWghPenalty", 
                        if (row.Cells[11].Text.Equals("") || row.Cells[11].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][15] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][15] = row.Cells[11].Text;
                        }
                        //HWHWP, HdWghHWP", 
                        if (row.Cells[12].Text.Equals("") || row.Cells[12].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][16] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][16] = row.Cells[12].Text;
                        }

                        if (row.Cells[13].Text.Equals("") || row.Cells[13].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][17] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][17] = row.Cells[13].Text;
                        }

                        if (row.Cells[14].Text.Equals("") || row.Cells[14].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][18] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][18] = row.Cells[14].Text;
                        }

                        if (row.Cells[15].Text.Equals("") || row.Cells[15].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][19] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][19] = row.Cells[15].Text;
                        }

                        if (chkboxRaisedLowered.SelectedIndex == -1)
                        {
                            dtHandicap.Rows[rows][20] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][20] = chkboxRaisedLowered.SelectedItem.Text;
                        }

                        if (!(txtbxRaisedLoweredValue.Text.Equals("")))
                        {
                            dtHandicap.Rows[rows][21] = txtbxRaisedLoweredValue.Text;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][21] = string.Empty;
                        }



                        if (row.Cells[16].Text.Equals("") || row.Cells[16].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][22] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][22] = row.Cells[16].Text;
                        }


                        if (row.Cells[17].Text.Equals("") || row.Cells[17].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][23] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][23] = row.Cells[17].Text;
                        }


                        if (row.Cells[18].Text.Equals("") || row.Cells[18].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][24] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][24] = row.Cells[18].Text;
                        }

                        if (row.Cells[19].Text.Equals("") || row.Cells[19].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][25] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][25] = row.Cells[19].Text;
                        }
                        if ((row.FindControl("txtbxhandiapratinggivebyclub") as TextBox).Text.Equals(""))
                        {
                            breakgbc = 1;
                            break;

                        }
                        else
                        {
                            breakgbc = 0;
                            dtHandicap.Rows[rows][26] = (row.FindControl("txtbxhandiapratinggivebyclub") as TextBox).Text;
                        }

                        dtHandicap.Rows[rows][27] = DateTime.Now;
                        dtHandicap.Rows[rows][28] = 1;
                        dtHandicap.Rows[rows][29] = 1;
                        dtHandicap.Rows[rows][30] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                        //dtHandicap.Rows[rows][31] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);


                        if (row.Cells[21].Text.Equals("") || row.Cells[21].Text.Equals("&nbsp;"))
                        {
                            dtHandicap.Rows[rows][31] = string.Empty;
                        }
                        else
                        {
                            dtHandicap.Rows[rows][31] = row.Cells[21].Text;
                        }

                        rows += 1;
                    }

				}
				if(breakgbc==1)
				{
					var message = "Please enter HWGBC in all records.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}
				else
				{
					var result = new CardsBL().AddHandicap(dtHandicap);
					if (result == 1)
					{
						HandicapShow();
						var message = "Record added successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					}
					else
					{
						var message = "Issue in Record.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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

        
        protected void dvgrdviewHorseDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int status = 0;
            try
            {
                GridViewRow row = grdvwHorseDetail.SelectedRow;
                
                var dataKey = grdvwHorseDetail.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                if (dataKey != null)
                {
                    var serialnumber = grdvwHorseDetail.SelectedRow.Cells[1].Text;
                    var horseage = grdvwHorseDetail.SelectedRow.Cells[3].Text;
                    HiddenField hdnfieldhorseid = (HiddenField)row.FindControl("hdnfieldhorseid");
                    HiddenField hdnfieldhorsenameid = (HiddenField)row.FindControl("hdnfieldhorsenameid");
                    HiddenField hdnfieldsexid = (HiddenField)row.FindControl("hdnfieldhorsesexid");
                    var handicaprating = string.Empty;
                    if (!(row.FindControl("txtbxGvHandicapRating") as TextBox).Text.Equals(""))
                    {
                        TextBox txtbxhandingrating = (TextBox)row.FindControl("txtbxGvHandicapRating");
                        handicaprating = txtbxhandingrating.Text;
                    }
                    else
                    {
                        handicaprating = "";
                    }
                    var MyHandicapRatingRange = grdvwHorseDetail.SelectedRow.Cells[6].Text;
                    var HandicapWeight = grdvwHorseDetail.SelectedRow.Cells[7].Text;
                    var MyHandicapWeight = grdvwHorseDetail.SelectedRow.Cells[8].Text;
                    var HandicapWeightAsPerGender = grdvwHorseDetail.SelectedRow.Cells[9].Text;
					var HandicapWeightAsPerAgeCondition = grdvwHorseDetail.SelectedRow.Cells[10].Text;
					var TotalHandicapWeightAsperGender = grdvwHorseDetail.SelectedRow.Cells[11].Text;
                    var HWHWP = grdvwHorseDetail.SelectedRow.Cells[12].Text;
                    var MyHWHWP = grdvwHorseDetail.SelectedRow.Cells[13].Text;
                    var HWGHWP = grdvwHorseDetail.SelectedRow.Cells[14].Text;
					var HWACHWP = grdvwHorseDetail.SelectedRow.Cells[15].Text;


					var FHWAWRL = grdvwHorseDetail.SelectedRow.Cells[16].Text;
                    var FMyHWAWRL = grdvwHorseDetail.SelectedRow.Cells[17].Text;
                    var FHWGAWRL = grdvwHorseDetail.SelectedRow.Cells[18].Text;
					var FHWACAWRL = grdvwHorseDetail.SelectedRow.Cells[19].Text;

					var handicapratinggivenbyclub = string.Empty;
                    if (!(row.FindControl("txtbxhandiapratinggivebyclub") as TextBox).Text.Equals(""))
                    {
                        TextBox txtbxhandiapratinggivebyclub = (TextBox)row.FindControl("txtbxhandiapratinggivebyclub");
                        handicapratinggivenbyclub = txtbxhandiapratinggivebyclub.Text;
                    }
                    else
                    {
                        handicapratinggivenbyclub = "";
                    }
                    
                   var BanDetail = grdvwHorseDetail.SelectedRow.Cells[20].Text;
                    status = new CardsBL().UpdateHandicapHorseInformation(serialnumber,Convert.ToInt32(horseage), Convert.ToInt32(hdnfieldhorseid.Value),
                         Convert.ToInt32(hdnfieldhorsenameid.Value), Convert.ToInt32(hdnfieldsexid.Value),
                         (handicaprating.Equals("&nbsp;"))? string.Empty: handicaprating,
						 (MyHandicapRatingRange.Equals("&nbsp;")) ? string.Empty : MyHandicapRatingRange,
						 (HandicapWeight.Equals("&nbsp;")) ? string.Empty : HandicapWeight,
						 (MyHandicapWeight.Equals("&nbsp;")) ? string.Empty : MyHandicapWeight,
						 (HandicapWeightAsPerGender.Equals("&nbsp;")) ? string.Empty : HandicapWeightAsPerGender,
						 (TotalHandicapWeightAsperGender.Equals("&nbsp;")) ? string.Empty : TotalHandicapWeightAsperGender,
						 (HWHWP.Equals("&nbsp;")) ? string.Empty : HWHWP,
						 (MyHWHWP.Equals("&nbsp;")) ? string.Empty : MyHWHWP,
						 (HWGHWP.Equals("&nbsp;")) ? string.Empty : HWGHWP,
                          (FHWAWRL.Equals("&nbsp;")) ? string.Empty : FHWAWRL,
						 (FMyHWAWRL.Equals("&nbsp;")) ? string.Empty : FMyHWAWRL,
						  (FHWGAWRL.Equals("&nbsp;")) ? string.Empty : FHWGAWRL,
						  (handicapratinggivenbyclub.Equals("&nbsp;")) ? string.Empty : handicapratinggivenbyclub,
						 Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), txtbxRaceDate.Text,
						  (HandicapWeightAsPerAgeCondition.Equals("&nbsp;")) ? string.Empty : HandicapWeightAsPerAgeCondition,
						 (HWACHWP.Equals("&nbsp;")) ? string.Empty : HWACHWP,
						  (FHWACAWRL.Equals("&nbsp;")) ? string.Empty : FHWACAWRL);
                }

                if (status == 1)
                {
                    HandicapShow();
                    string message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string message = "Issue in Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


		protected void btnFileUpload_Click(object sender, EventArgs e)
		{
			try
			{
				if (flupload.HasFile)
				{
					string FileName = Path.GetFileName(flupload.PostedFile.FileName);
					if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Handicap"))
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
                var dtresult = new DataTable();
                var id = string.Empty;
                var rowcount = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        rowcount = rowcount + 1;
                        id = dt.Rows[count][0].ToString();
                        if (!(string.IsNullOrEmpty(id)))
                        {


                            dtresult = new CardsBL().ImportCardFiles(dt.Rows[count][1].ToString(), dt.Rows[count][2].ToString(), dt.Rows[count][3].ToString(), dt.Rows[count][4].ToString(),
                                 dt.Rows[count][5].ToString(), dt.Rows[count][6].ToString(), dt.Rows[count][7].ToString(), dt.Rows[count][8].ToString(), dt.Rows[count][9].ToString(),
                                 dt.Rows[count][10].ToString(), dt.Rows[count][11].ToString(), dt.Rows[count][12].ToString(), dt.Rows[count][13].ToString(), dt.Rows[count][14].ToString(),
                                 dt.Rows[count][15].ToString(), dt.Rows[count][16].ToString(), dt.Rows[count][17].ToString(), dt.Rows[count][18].ToString(), dt.Rows[count][19].ToString(),
                                 dt.Rows[count][20].ToString(), dt.Rows[count][21].ToString(), dt.Rows[count][22].ToString(), dt.Rows[count][23].ToString(), dt.Rows[count][24].ToString(),
                                 dt.Rows[count][25].ToString(), dt.Rows[count][26].ToString(), dt.Rows[count][27].ToString(), dt.Rows[count][28].ToString(), dt.Rows[count][29].ToString(),
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "Card_Handicap");


                            if (dtresult.Rows.Count > 0)
                            {
                                if (dtresult.Rows[0][0].Equals("9"))
                                {
                                    var message = "Issue in Row No: " + id + "<br />" + "Column Detail: " + dtresult.Rows[0][1]
                                        + "<br />" + "Table Name: " + dtresult.Rows[0][2];
                                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                                    break;
                                }
                            }
                        }
                    }

                    if (rowcount.Equals(dt.Rows.Count))
                    {
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

		protected void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_Handicap"))
				{
					DataTable dt = ds.Tables[0];
					if (dt.Rows.Count > 0)
					{
						//  dt.Columns.Remove("ProfessionalCurrentStatusID");
						using (ExcelPackage xp = new ExcelPackage())
						{
							ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Handicap");
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
							Response.AddHeader("content-disposition", "attachment;filename=Card_Handicap.xlsx");
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