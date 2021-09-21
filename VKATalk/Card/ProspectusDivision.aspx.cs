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

    public partial class ProspectusDivision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                grdvwRaceDetail.DataSource = new DataTable();
                grdvwRaceDetail.DataBind();
            }
        }

        public void ClearSelection()
        {
            drpdwnCenterName.ClearSelection();
            lblSeason.Text = "";
            lblYear.Text = "";
            grdvwRaceDetail.DataSource = new DataTable();
            grdvwRaceDetail.DataBind();
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

        protected void grdvwRaceDetail_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
				//BtnSubmit.Text = "Update";
				if (e.CommandName == "RowUpdate")
				{
					int index = Convert.ToInt32(e.CommandArgument.ToString());
					var generaleacenameid = (HiddenField)grdvwRaceDetail.Rows[index].FindControl("hdnfieldGeneralRaceNameID");
					var generalraceid = (HiddenField)grdvwRaceDetail.Rows[index].FindControl("hdnfieldGeneralRaceID");
					var generalracename = (HiddenField)grdvwRaceDetail.Rows[index].FindControl("hdnfieldStatus");
					var divisioncount = (DropDownList)grdvwRaceDetail.Rows[index].FindControl("drpdwnNoofDivision");

					var totalcount = new CardsBL().DivisionRaceCount(Convert.ToInt32(generaleacenameid.Value), Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text);
					var result = 0;
					int DivisionedCount = Convert.ToInt32(divisioncount.SelectedItem.Value);

					if(totalcount> DivisionedCount) {
						//Delete
						var divisionracename = generalracename.Value;
						var tasktype = "DeActivate";
						var serialno = 0;
						result = new CardsBL().UpdateAcceptanceGenrealRaceInformation(
							Convert.ToInt32(generaleacenameid.Value), Convert.ToInt32(generalraceid.Value), generalracename.Value, 
							Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), 
							txtbxHandicapEnterDate.Text, txtbxRaceDate.Text, Convert.ToInt32(divisioncount.SelectedItem.Value), divisionracename, txtbxRaceDate.Text, tasktype, serialno);

					}
					else
					{

						//Add
						var divisionracename = generalracename.Value + " (Div. I)";
						var tasktype = "Activate";
						var serialno = 0;
						result = new CardsBL().UpdateAcceptanceGenrealRaceInformation(Convert.ToInt32(generaleacenameid.Value), Convert.ToInt32(generalraceid.Value), generalracename.Value, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value),
							txtbxHandicapEnterDate.Text, txtbxRaceDate.Text, Convert.ToInt32(divisioncount.SelectedItem.Value), divisionracename, txtbxRaceDate.Text, tasktype, serialno);
					}
					
					//Delete

					AcceptanceShow();
					var message1 = "Record updated successfully.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);

				}
			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        
        private void AcceptanceShow()
        {
            try
            {
                var dt1 = new CardsBL().GetCardAcceptance(Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text);
                
                if (dt1.Rows.Count > 0)
                {
                    grdvwRaceDetail.Columns[0].Visible = true;
                    GvAcceptance.DataSource = dt1;
                    GvAcceptance.DataBind();
                }
                else
                {
                    grdvwRaceDetail.Columns[0].Visible = false;
                    GvAcceptance.DataSource = new DataTable();
                    GvAcceptance.DataBind();
                }

                foreach (GridViewRow row in GvAcceptance.Rows)
                {
                    var hour = (row.FindControl("hdnfieldhh") as HiddenField).Value;
                    var min = (row.FindControl("hdnfieldmm") as HiddenField).Value;
                    var ampm = (row.FindControl("hdnfieldAMPM") as HiddenField).Value;
                    
                    //int DivisionedCount = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);

                    if (!hour.Equals(""))
                    {
                        DropDownList drpdwnhhG = (row.FindControl("drpdwnhhG") as DropDownList);
                        drpdwnhhG.Items.FindByValue(Convert.ToString(hour)).Selected = true;
                    }
                    if (!min.Equals(""))
                    {
                        DropDownList drpdwnmmG = (row.FindControl("drpdwnmmG") as DropDownList);
                        drpdwnmmG.Items.FindByValue(Convert.ToString(min)).Selected = true;
                    }
                    if (!ampm.Equals(""))
                    {
                        DropDownList drpdwnampmG = (row.FindControl("drpdwnampmG") as DropDownList);
                        drpdwnampmG.Items.FindByValue(Convert.ToString(ampm)).Selected = true;
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


        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                //GetRaceGeneralRaceDetail
                var dt = new CardsBL().GetRaceGeneralRaceDetail(
                    txtbxRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    // dvgridview.Visible = true;
                    lblSeason.Text = dt.Rows[0][11].ToString();
                    lblYear.Text = dt.Rows[0][12].ToString();
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
                else
                {
                    //dvgridview.Visible = false;
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();
                 
                    ClearSelection();
                }

                AcceptanceShow();
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
            hdnfieldGeneralRaceNameID.Value = "";
            //txtbxHandicapEnterDate.Text = string.Empty;
            txtbxRaceDate.Text = string.Empty;
            drpdwnCenterName.ClearSelection();
            lblSeason.Text = "";
            lblYear.Text = "";
            grdvwRaceDetail.DataSource = new DataTable();
            grdvwRaceDetail.DataBind();

            GvAcceptance.DataSource = new DataTable();
            GvAcceptance.DataBind();
           
        }

        protected void btnBanUpdate_Click(object sender, EventArgs e)
        {

        }


		protected void GvAcceptance_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					DropDownList DropDownList1 = (e.Row.FindControl("drpdwnWeightShuffleType") as DropDownList);
					BindDropDown(DropDownList1, "MasterWeightShuffle", "WeightShuffleType", "WeightShuffleTypeMID");
					DropDownList1.Items.Insert(0, new ListItem("-- Please select --", "-1"));

					DropDownList drpdwnRaceFinalOutCome = (e.Row.FindControl("drpdwnRaceFinalOutCome") as DropDownList);
					BindDropDown(drpdwnRaceFinalOutCome, "MasterRaceFinalOutCome", "RaceFinalOutCome", "RaceFinalOutComeDRID");
					drpdwnRaceFinalOutCome.Items.Insert(0, new ListItem("-- Please select --", "-1"));


					string hire = (e.Row.FindControl("lblshuffletype") as Label).Text;
					DropDownList1.Items.FindByText(hire).Selected = true;


					string racefinaloutcome = (e.Row.FindControl("lblRaceFinalOutcome") as Label).Text;
					drpdwnRaceFinalOutCome.Items.FindByText(racefinaloutcome).Selected = true;
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


		/// <summary>
		/// Fill MyComments
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> OutcomeReason(string prefixText, int count)
	{
			DataTable dt = new CardsBL().GetCardAutoFiller("DivisionOutcomeReason", prefixText);
			List<string> ImpactList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				ImpactList.Add(dt.Rows[i][0].ToString());
			}
			return ImpactList;
		}



		/// <summary>
		/// Fill MyComments
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> Comments(string prefixText, int count)
		{
			DataTable dt = new CardsBL().GetCardAutoFiller("DivisionComments", prefixText);
			List<string> ImpactList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				ImpactList.Add(dt.Rows[i][0].ToString());
			}
			return ImpactList;
		}
		protected void btnAddRaceName_Click(object sender, EventArgs e)
        {
            DataTable dtHandicap = new DataTable("Acceptance");
            DataTable dtAcceptanceracename = new DataTable("Acceptance");
            try
            {

                var dt1 = new CardsBL().GetCardAcceptance(Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text);
                if (dt1.Rows.Count > 0)
                {
                    string message = "Record already exists.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {

                    //Loop through the GridView and copy rows.
                    dtHandicap.Clear();
                    dtAcceptanceracename.Clear();
                    int count = 0;
                    DataSet ds = null;
                    dtHandicap.Columns.Add("GeneralRaceID", typeof(int));//0
                    dtHandicap.Columns.Add("GeneralRaceNameID", typeof(int));//1
                    dtHandicap.Columns.Add("NoofDivisions", typeof(int));//2
                    dtHandicap.Columns.Add("DivisionRaceName", typeof(string));//3
                    dtHandicap.Columns.Add("DivisionRaceDate", typeof(DateTime));//4
                    dtHandicap.Columns.Add("CreatedDate", typeof(DateTime));//5
                    dtHandicap.Columns.Add("CreatedUserID", typeof(int));//6
                    dtHandicap.Columns.Add("IsActive", typeof(int));//7
                    dtHandicap.Columns.Add("GeneralRaceDate", typeof(DateTime));//8
                    dtHandicap.Columns.Add("CenterID", typeof(int));//9
                    dtHandicap.Columns.Add("EntryDate", typeof(DateTime));//10
					dtHandicap.Columns.Add("SerialNo", typeof(string));//11
					dtHandicap.Columns.Add("WeightShuffleTypeMID", typeof(int));//12
					dtHandicap.Columns.Add("WeightShuffleValue", typeof(string));//13
					dtHandicap.Columns.Add("RaceFinalOutcomeDRID", typeof(int));//14
					dtHandicap.Columns.Add("RaceFinalOutcomeReason", typeof(string));//15
					dtHandicap.Columns.Add("MyComments", typeof(string));//16

					dtAcceptanceracename = dtHandicap.Clone();

                    foreach (GridViewRow row in grdvwRaceDetail.Rows)
                    {
                        dtHandicap.Rows.Add();
                        int DivisionedCount = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                        if (DivisionedCount == -1)
                        {
                            int dtcount = dtHandicap.Rows.Count - 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            dtHandicap.Rows[dtcount][3] = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 0;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;
						}
                        else if (DivisionedCount == 2)
                        {
                            int dtcount = dtHandicap.Rows.Count - 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname = divionname + " (Div. I)";
                            dtHandicap.Rows[dtcount][3] = divionname;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 1;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;

							dtHandicap.Rows.Add();
                            dtcount += 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname1 = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname1 = divionname1 + " (Div. II)";
                            dtHandicap.Rows[dtcount][3] = divionname1;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 2;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;
						}
                        else if (DivisionedCount == 3)
                        {
                            int dtcount = dtHandicap.Rows.Count - 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname = divionname + " (Div. I)";
                            dtHandicap.Rows[dtcount][3] = divionname;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 1;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;

							dtHandicap.Rows.Add();
                            dtcount += 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname1 = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname1 = divionname1 + " (Div. II)";
                            dtHandicap.Rows[dtcount][3] = divionname1;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 2;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;

							dtHandicap.Rows.Add();
                            dtcount += 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname2 = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname2 = divionname2 + " (Div. III)";
                            dtHandicap.Rows[dtcount][3] = divionname2;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 3;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;
						}
                        else if (DivisionedCount == 4)
                        {
                            int dtcount = dtHandicap.Rows.Count - 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname = divionname + " (Div. I)";
                            dtHandicap.Rows[dtcount][3] = divionname;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 1;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;

							dtHandicap.Rows.Add();
                            dtcount += 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname1 = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname1 = divionname1 + " (Div. II)";
                            dtHandicap.Rows[dtcount][3] = divionname1;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 2;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;

							dtHandicap.Rows.Add();
                            dtcount += 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname2 = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname2 = divionname2 + " (Div. III)";
                            dtHandicap.Rows[dtcount][3] = divionname2;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 3;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;


							dtHandicap.Rows.Add();
                            dtcount += 1;
                            dtHandicap.Rows[dtcount][0] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][1] = Convert.ToInt32((row.FindControl("hdnfieldGeneralRaceNameID") as HiddenField).Value);
                            dtHandicap.Rows[dtcount][2] = Convert.ToInt32((row.FindControl("drpdwnNoofDivision") as DropDownList).SelectedItem.Value);
                            string divionname3 = (row.FindControl("hdnfieldStatus") as HiddenField).Value;
                            divionname3 = divionname3 + " (Div. IV)";
                            dtHandicap.Rows[dtcount][3] = divionname3;
                            dtHandicap.Rows[dtcount][4] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][5] = DateTime.Now;
                            dtHandicap.Rows[dtcount][6] = 1;
                            dtHandicap.Rows[dtcount][7] = 1;
                            dtHandicap.Rows[dtcount][8] = txtbxRaceDate.Text;
                            dtHandicap.Rows[dtcount][9] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                            dtHandicap.Rows[dtcount][10] = txtbxHandicapEnterDate.Text;
							dtHandicap.Rows[dtcount][11] = 4;
							dtHandicap.Rows[dtcount][12] = DBNull.Value;
							dtHandicap.Rows[dtcount][13] = string.Empty;
							dtHandicap.Rows[dtcount][14] = DBNull.Value;
							dtHandicap.Rows[dtcount][15] = string.Empty;
							dtHandicap.Rows[dtcount][16] = string.Empty;
                            						
						}
                    }

                    var result = new CardsBL().AddAcceptanceDivision(dtHandicap);
                    if (result == 1)
                    {
                        //HandicapShow();
                        AcceptanceShow();
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        var message = "Issue in Record.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    dtHandicap.Dispose();
                    dtAcceptanceracename.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void btnAddProvisionDivisionRace_Click(object sender, EventArgs e)
        {
            try
            {
                var checkvalidvalue = 0;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[12] { new DataColumn("DivisionRaceID", typeof(int)),
                new DataColumn("DivisionRaceDate", typeof(DateTime)),
                new DataColumn("DayRaceNo", typeof(int)),
                new DataColumn("SeasonRaceNo",typeof(int)),
                new DataColumn("hh",typeof(string)), 
                new DataColumn("mm",typeof(string)), 
                new DataColumn("AMPM",typeof(string)),
				new DataColumn("WeightShuffleTypeMID",typeof(int)),
				new DataColumn("WeightShuffleValue",typeof(string)),
				new DataColumn("RaceFinalOutcomeDRID",typeof(int)),
				new DataColumn("RaceFinalOutcomeReason",typeof(string)),
				new DataColumn("MyComments",typeof(string))
				});


                foreach (GridViewRow row in GvAcceptance.Rows)
                {
                    int divisionraceid = Convert.ToInt32((row.FindControl("hdnfieldDivisionRaceID") as HiddenField).Value);
                    string divisionracedate = (row.FindControl("txtbxGridviewRaceDate") as TextBox).Text;
                    int dayraceno = Convert.ToInt32((row.FindControl("txtbxDayRaceNoG") as TextBox).Text);
                    int seasonraceno = Convert.ToInt32((row.FindControl("txtbxSeasonRaceNoG") as TextBox).Text);
                    string hh = (row.FindControl("drpdwnhhG") as DropDownList).SelectedItem.Text;
                    string min = (row.FindControl("drpdwnmmG") as DropDownList).SelectedItem.Text;
                    string ampm = (row.FindControl("drpdwnampmG") as DropDownList).SelectedItem.Text;
					int weightshuffleid = Convert.ToInt32((row.FindControl("drpdwnWeightShuffleType") as DropDownList).SelectedItem.Value);
					string weightshufflevalue = 
						((row.FindControl("txtbxWeightShuffleValue") as TextBox).Text.Equals(""))? String.Empty : (row.FindControl("txtbxWeightShuffleValue") as TextBox).Text;
					int racefinaloutcomeid = Convert.ToInt32((row.FindControl("drpdwnRaceFinalOutCome") as DropDownList).SelectedItem.Value); 
					string reason = 
					((row.FindControl("txtbxRaceFinalOutcomeReason") as TextBox).Text.Equals("")) ? String.Empty : (row.FindControl("txtbxRaceFinalOutcomeReason") as TextBox).Text;
					string comments = 
								((row.FindControl("txtbxMyComments") as TextBox).Text.Equals("")) ? String.Empty : (row.FindControl("txtbxMyComments") as TextBox).Text;
                    if(weightshuffleid == -1)
                    {
                        checkvalidvalue = 1;
                        break;
                    }
                    else
                    {
                        checkvalidvalue = 0;
                        dt.Rows.Add(divisionraceid, Convert.ToDateTime(divisionracedate), dayraceno, seasonraceno, hh, min, ampm, weightshuffleid, weightshufflevalue, racefinaloutcomeid, reason, comments);
                    }
					
                }
                if(checkvalidvalue == 1)
                {
                    var message = "Please select the Weight Shuffle Type.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else {
                    int value = new CardsBL().UpdateProvisionRaceDetail(dt);

                    if (value == 1)
                    {
                        //HandicapShow();
                        //AcceptanceShow();
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


        protected void GvAcceptance_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[12] { new DataColumn("DivisionRaceID", typeof(int)),
                new DataColumn("DivisionRaceDate", typeof(DateTime)),
                new DataColumn("DayRaceNo", typeof(int)),
                new DataColumn("SeasonRaceNo",typeof(int)),
                new DataColumn("hh",typeof(string)), 
                new DataColumn("mm",typeof(string)), 
                new DataColumn("AMPM",typeof(string)),
				new DataColumn("WeightShuffleTypeMID",typeof(int)),
				new DataColumn("WeightShuffleValue",typeof(string)),
				new DataColumn("RaceFinalOutcomeDRID",typeof(int)),
				new DataColumn("RaceFinalOutcomeReason",typeof(string)),
				new DataColumn("MyComments",typeof(string))
				});


                GridViewRow row = GvAcceptance.SelectedRow;

                var dataKey = GvAcceptance.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                if (dataKey != null)
                {
                    string divisionracename = ((row.FindControl("txtbxDivisionRaceName") as TextBox).Text.Equals("")) ? String.Empty : (row.FindControl("txtbxDivisionRaceName") as TextBox).Text;
                    if (divisionracename.Equals(""))
                    {
                        string message = "Division Race Name should not be blank.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        int divisionraceid = Convert.ToInt32(dataKey.Value);
                        string divisionracedate = (row.FindControl("txtbxGridviewRaceDate") as TextBox).Text;
                        int dayraceno = Convert.ToInt32((row.FindControl("txtbxDayRaceNoG") as TextBox).Text);
                        int seasonraceno = Convert.ToInt32((row.FindControl("txtbxSeasonRaceNoG") as TextBox).Text);
                        string hh = (row.FindControl("drpdwnhhG") as DropDownList).SelectedItem.Text;
                        string min = (row.FindControl("drpdwnmmG") as DropDownList).SelectedItem.Text;
                        string ampm = (row.FindControl("drpdwnampmG") as DropDownList).SelectedItem.Text;
                        int weightshuffleid = Convert.ToInt32((row.FindControl("drpdwnWeightShuffleType") as DropDownList).SelectedItem.Value);
                        string weightshufflevalue =
                            ((row.FindControl("txtbxWeightShuffleValue") as TextBox).Text.Equals("")) ? String.Empty : (row.FindControl("txtbxWeightShuffleValue") as TextBox).Text;
                        int racefinaloutcomeid = Convert.ToInt32((row.FindControl("drpdwnRaceFinalOutCome") as DropDownList).SelectedItem.Value);
                        string reason =
                        ((row.FindControl("txtbxRaceFinalOutcomeReason") as TextBox).Text.Equals("")) ? String.Empty : (row.FindControl("txtbxRaceFinalOutcomeReason") as TextBox).Text;
                        string comments =
                                    ((row.FindControl("txtbxMyComments") as TextBox).Text.Equals("")) ? String.Empty : (row.FindControl("txtbxMyComments") as TextBox).Text;
                        dt.Rows.Add(divisionraceid, Convert.ToDateTime(divisionracedate), dayraceno, seasonraceno, hh, min, ampm, weightshuffleid, weightshufflevalue, racefinaloutcomeid, reason, comments);
                        int value = new CardsBL().UpdateProvisionRaceDetailSingleRow(dt, divisionraceid,
                            divisionracename);
                        if (value == 1)
                        {
                            //HandicapShow();
                            string message = "Record updated successfully.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        }
                        else
                        {
                            string message = "Issue in Record.";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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



        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_Division"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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
                    var dtErrorResult = new CardsBL().Import30(dt, "Prospectus_Division", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Prospectus_Division");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Division.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        //BindData();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('All Record has been added successfully.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('No Record Found.');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                // var ds = new ProfessionalBL().GetProfessionalNameWithCombination(_professionalId, "ProfessionalBaseCenter");
                // using (DataSet ds = new ProfessionalBL().GetProfessionalNameWithCombination(_professionalId, "ProfessionalCStatus"))
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Prospectus_Division"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Prospectus_Division");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Division.xlsx");
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