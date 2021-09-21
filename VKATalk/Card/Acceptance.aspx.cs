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
    using System.Linq;
    using System.Web.WebSockets;

    using OfficeOpenXml;
    using VKATalk.Common;

    public partial class Acceptance : System.Web.UI.Page
    {
        public DataTable dtdivisioncount;
        public DataTable dtbiturificaitoncount;
        public int addweight = 0;
        public static int horsenodiv1 = 1;
        public static int horsenodiv2 = 1;
        public static int horsenodiv3 = 1;
        public static int horsenodiv4 = 1;
        public static int[] divisionraceidcount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxHandicapEnterDate.Text = CommonMethods.CurrentDate();
                BindDropDown(drpdwnRaceNameS, "CardAcceptance", "GeneralRaceName", "GeneralRaceNameID");
				drpdwnRaceNameS.Items.Insert(0, new ListItem("-- Please select --", "-1"));
				// BindData();
				grdvwRaceDetail.DataSource = new DataTable();
                grdvwRaceDetail.DataBind();

                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }

        public void ClearSelection()
        {
            horsenodiv1 = 1;
            horsenodiv2 = 1;
            horsenodiv3 = 1;
            horsenodiv4 = 1;
            drpdwnCenterName.ClearSelection();
            lblSeason.Text = "";
            lblYear.Text = "";
            grdvwRaceDetail.DataSource = new DataTable();
            grdvwRaceDetail.DataBind();
            lblGeneralRaceName.Text = "";
			GvShowALL.DataSource = new DataTable();
			GvShowALL.DataBind();
			lblEntryDate.Text = "";
		}


		protected void ClearAll(string methodname)
		{
            horsenodiv1 = 1;
            horsenodiv2 = 1;
            horsenodiv3 = 1;
            horsenodiv4 = 1;
            if (methodname.Equals("HorseAdd"))
			{
				drpdwnRaceNameS.ClearSelection();
				if (Convert.ToInt32(drpdwnCenterName.SelectedItem.Value).Equals(-1))
				{
					lblEntryDate.Text = "";
					grdvwHorseDetail.DataSource = new DataTable();
					grdvwHorseDetail.DataBind();
					GvShowALL.DataSource = new DataTable();
					GvShowALL.DataBind();
				}
			}
			else
			{
				lblSeason.Text = "";
				lblYear.Text = "";
				drpdwnRaceNameS.ClearSelection();
				if (Convert.ToInt32(drpdwnCenterName.SelectedItem.Value).Equals(-1))
				{
					lblEntryDate.Text = "";
					grdvwHorseDetail.DataSource = new DataTable();
					grdvwHorseDetail.DataBind();
					GvShowALL.DataSource = new DataTable();
					GvShowALL.DataBind();
				}
			}

		}
		protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            ClearAll("Other");
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

        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            if (!drpdwnCenterName.SelectedItem.Value.Equals("-1"))
            {
                DataTable dt;
                dt = new ProspectusBL().GetDropdownBindMultipleValues(TableName_, txtbxRaceDate.Text, drpdwnCenterName.SelectedItem.Value);
                ddl.DataSource = dt;
                ddl.DataTextField = TextField;
                ddl.DataValueField = ValueField;
                ddl.DataBind();
            }
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
                horsenodiv1 = 1;
                horsenodiv2 = 1;
                horsenodiv3 = 1;
                horsenodiv4 = 1;
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                HiddenField hdnfieldgeneralraceid = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["GeneralRaceNameID"] = dataKey.Value; //generalraceid
                    ViewState["GeneralRaceID"] = hdnfieldgeneralraceid.Value; //generalraceid
                    ViewState["GridViewRaceName"] = hdnval.Value;//reneralracename
                    ViewState["SerialNumber"] = row.Cells[0].Text;
                    hdnfieldGeneralRaceNameID.Value = Convert.ToString(dataKey.Value);
					lblGeneralRaceName.Text = ViewState["GridViewRaceName"].ToString();

				}
                var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(dataKey.Value), "Acceptance", lblSeason.Text, lblYear.Text);
                if (dsGeneralDate.Tables[0].Rows.Count > 0)
                {
                    //  if(dsGeneralDate.Tables[0].
                    if (dsGeneralDate.Tables.Count == 1)
                    {
                        if (dsGeneralDate.Tables[0].Rows.Count > 0)
                        {
                            lblEntryDate.Text = dsGeneralDate.Tables[0].Rows[0][0].ToString();
                        }
						else
						{
							lblEntryDate.Text = string.Empty;
						}
                    }
                }
				else
				{
					lblEntryDate.Text = string.Empty;
				}
				//                AcceptanceShow();
				var dt = new CardsBL().HorseInformation(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectAcceptance", 0, 1, txtbxHandicapEnterDate.Text,false,false,0);
                if (dt.Rows.Count > 0)
                {
                    grdvwHorseDetail.DataSource = dt;
                    grdvwHorseDetail.DataBind();
                }
                else
                {
                    grdvwHorseDetail.DataSource = new DataTable();
                    grdvwHorseDetail.DataBind();
                }

				GvShowALL.DataSource = new DataTable();
				GvShowALL.DataBind();

			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

		protected void grdvwHorseDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList DropDownList1 = (e.Row.FindControl("drpdwnBifurcation") as DropDownList);
					var dt1 = new CardsBL().GetCardAcceptanceDivisionRace(Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text, Convert.ToInt32(ViewState["GeneralRaceNameID"]), "Acceptance");
                    dtbiturificaitoncount = dt1;
                   // var count = dt1.AsEnumerable().Select(r => r.Field<string>("DivisionRaceID")).ToArray();
                    divisionraceidcount = dt1.AsEnumerable().Select(r => r.Field<int>("DivisionRaceID")).ToArray();
                    DropDownList1.DataSource = dt1;
					DropDownList1.DataTextField = "DivisionRaceName";
					DropDownList1.DataValueField = "DivisionRaceID";
					DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("---Struck Out---", "-1"));

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


		protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					if ((e.Row.RowState & DataControlRowState.Edit) > 0)
					{
						DropDownList ddList = (DropDownList)e.Row.FindControl("drpdwnBifurcationS");
						//bind dropdown-list
						DataTable dt = new CardsBL().GetCardAcceptanceDivisionRace(Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), txtbxRaceDate.Text, Convert.ToInt32(ViewState["GeneralRaceNameID"]), "Acceptance");
						ddList.DataSource = dt;
						ddList.DataTextField = "DivisionRaceName";
						ddList.DataValueField = "DivisionRaceID";
						ddList.DataBind();
						ddList.Items.Insert(0, new ListItem("---Struck Out---", "-1"));

						DataRowView dr = e.Row.DataItem as DataRowView;
						//ddList.SelectedItem.Text = dr["category_name"].ToString();
						ddList.SelectedValue = dr["DivisionRaceName"].ToString();
					}
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

		protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
            
                var dt = new CardsBL().GetRaceGeneralRaceDetailAcceptance(
                    txtbxRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    BindDropDown(drpdwnRaceNameS, "CardAcceptance", "GeneralRaceName", "GeneralRaceNameID");
                    lblEntryDate.Text = string.Empty;
					lblSeason.Text = dt.Rows[0][7].ToString();
                    lblYear.Text = dt.Rows[0][8].ToString();
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
					GvShowALL.DataSource = new DataTable();
					GvShowALL.DataBind();
					grdvwHorseDetail.DataSource = new DataTable();
					grdvwHorseDetail.DataBind();
                    horsenodiv1 = 1;
                    horsenodiv2 = 1;
                    horsenodiv3 = 1;
                    horsenodiv4 = 1;
                }
                else
                {
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();
					ClearAll("Other");
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

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddJockeyList(string prefixText, int count)
       {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardHorseName", prefixText);

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

		protected void drpdwnBifurcation_SelectIndexChange(object sender, EventArgs e)
		{
			try
			{
                //if (Session["horseno"] != null)
                //{
                //    Session["horseno"] =  Convert.ToInt32(Session["horseno"]) +  1;
                //}
                //else
                //{
                //    Session["horseno"] = 1;
                //}
				GridViewRow row = (GridViewRow)(sender as DropDownList).NamingContainer;
				TextBox txtbxAWGBC = row.FindControl("txtbxAWGBC") as TextBox;
                TextBox txtbxHno = row.FindControl("txtbxHno") as TextBox;
                Label lblAWAWRL = row.FindControl("lblAWAWRL") as Label;
				HiddenField hdnfieldAcceptanceID = row.FindControl("hdnfieldAcceptanceID") as HiddenField;
				DropDownList drpdwnBifurcation = row.FindControl("drpdwnBifurcation") as DropDownList;

				DataTable dt = new CardsBL().GetAcceptanceWeight(Convert.ToInt32(drpdwnBifurcation.SelectedItem.Value), Convert.ToInt32(hdnfieldAcceptanceID.Value));
				if (dt.Rows.Count > 0)
				{
					lblAWAWRL.Text = dt.Rows[0][0].ToString();
					txtbxAWGBC.Text = dt.Rows[0][1].ToString();
                    

                }
                if(drpdwnBifurcation.SelectedIndex == 1)
                {
                    txtbxHno.Text = horsenodiv1.ToString();
                    horsenodiv1 = horsenodiv1 + 1;
                }
                else if (drpdwnBifurcation.SelectedIndex == 2)
                {
                    txtbxHno.Text = horsenodiv2.ToString();
                    horsenodiv2 = horsenodiv2 + 1;
                }
                else if (drpdwnBifurcation.SelectedIndex == 3)
                {
                    txtbxHno.Text = horsenodiv3.ToString();
                    horsenodiv3 = horsenodiv3 + 1;
                }
                else if (drpdwnBifurcation.SelectedIndex == 4)
                {
                    txtbxHno.Text = horsenodiv4.ToString();
                    horsenodiv4 = horsenodiv4 + 1;
                }
                else if (drpdwnBifurcation.SelectedIndex == 0)
                {
                    txtbxHno.Text = string.Empty;
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

		protected void btnAdd_Click(object sender, EventArgs e)
        {
			

			DataTable dtHandicap = new DataTable("Acceptance");
            DataTable dtAcceptanceStuckOut = new DataTable("Acceptance");
            try
            {
				DataTable status = new CardsBL().GetAcceptanceOldRecordStatus(Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), lblSeason.Text, lblYear.Text);
				if (status.Rows.Count > 0)
				{
					var message = "Records already exists.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}
				else
				{
					//Add columns to DataTable.
					dtHandicap.Columns.Add("AcceptanceEnterDate", typeof(string));//0
					dtHandicap.Columns.Add("GeneralRaceDate", typeof(string));//1
					dtHandicap.Columns.Add("CenterMID", typeof(int));//2
					dtHandicap.Columns.Add("GeneralRaceID", typeof(int));//3
					dtHandicap.Columns.Add("GeneralRaceNameID", typeof(int));//4
					dtHandicap.Columns.Add("DivisionedRaceID", typeof(int));//5
					dtHandicap.Columns.Add("HorseNo", typeof(int));//6
					dtHandicap.Columns.Add("HorseID", typeof(int));//7
					dtHandicap.Columns.Add("HorseNameID", typeof(int));//8
					dtHandicap.Columns.Add("AcptWghRaisedLowered", typeof(string));//9
					dtHandicap.Columns.Add("AcptWghRaisedLoweredValue", typeof(Double));//10
					dtHandicap.Columns.Add("AcceptanceWeightAWRL", typeof(string));//11
					dtHandicap.Columns.Add("AcceptanceWeightGBC", typeof(string));//12
					dtHandicap.Columns.Add("CreatedDate", typeof(DateTime));//13
					dtHandicap.Columns.Add("CreatedUserID", typeof(int));//14
					dtHandicap.Columns.Add("IsActive", typeof(int));//15


					dtAcceptanceStuckOut.Columns.Add("AcceptanceEnterDate", typeof(string));//0
					dtAcceptanceStuckOut.Columns.Add("GeneralRaceDate", typeof(string));//1
					dtAcceptanceStuckOut.Columns.Add("CenterID", typeof(int));//2
					dtAcceptanceStuckOut.Columns.Add("GeneralRaceID", typeof(int));//3
					dtAcceptanceStuckOut.Columns.Add("GeneralRaceNameID", typeof(int));//4
					dtAcceptanceStuckOut.Columns.Add("StruckOutHorseNo", typeof(int));//5
					dtAcceptanceStuckOut.Columns.Add("HorseID", typeof(int));//6
					dtAcceptanceStuckOut.Columns.Add("HorseNameID", typeof(int));//7
					dtAcceptanceStuckOut.Columns.Add("AcptWghRaisedLowered", typeof(string));//8
					dtAcceptanceStuckOut.Columns.Add("AcptWghRaisedLoweredValue", typeof(Double));//9
					dtAcceptanceStuckOut.Columns.Add("AcceptanceWeightAWRL", typeof(string));//10
					dtAcceptanceStuckOut.Columns.Add("AcceptanceWeightGBC", typeof(string));//11
					dtAcceptanceStuckOut.Columns.Add("StruckOutType", typeof(string));//12
					dtAcceptanceStuckOut.Columns.Add("CreatedDate", typeof(DateTime));//13
					dtAcceptanceStuckOut.Columns.Add("CreatedUserID", typeof(int));//14
					dtAcceptanceStuckOut.Columns.Add("IsActive", typeof(int));//15

					var lowerraised = string.Empty;
					int rowcount = 0;
					int rowcountacceptance = 0;
					foreach (GridViewRow row in grdvwHorseDetail.Rows)
					{
						DropDownList ddl = (DropDownList)row.FindControl("drpdwnBifurcation");
						if (ddl.SelectedItem.Text.Trim().Equals("---Struck Out---"))
						{
							rowcount = dtAcceptanceStuckOut.Rows.Count;
							dtAcceptanceStuckOut.Rows.Add();
							if (txtbxHandicapEnterDate.Text.Equals("__-__-____"))
							{
								dtAcceptanceStuckOut.Rows[rowcount][0] = DBNull.Value;
							}
							else
							{
								string[] dateString = txtbxHandicapEnterDate.Text.Split('-');
								DateTime enterDate =
									Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
								dtAcceptanceStuckOut.Rows[rowcount][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
							}
							dtAcceptanceStuckOut.Rows[rowcount][1] = txtbxRaceDate.Text;
							dtAcceptanceStuckOut.Rows[rowcount][2] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
							dtAcceptanceStuckOut.Rows[rowcount][3] = Convert.ToInt32(ViewState["GeneralRaceID"]);
							dtAcceptanceStuckOut.Rows[rowcount][4] = Convert.ToInt32(hdnfieldGeneralRaceNameID.Value);
							dtAcceptanceStuckOut.Rows[rowcount][5] = DBNull.Value;
							dtAcceptanceStuckOut.Rows[rowcount][6] = Convert.ToInt32((row.FindControl("hdnfieldhorseid") as HiddenField).Value);
							dtAcceptanceStuckOut.Rows[rowcount][7] = Convert.ToInt32((row.FindControl("hdnfieldhorsenameid") as HiddenField).Value);
							dtAcceptanceStuckOut.Rows[rowcount][8] = string.Empty;
							dtAcceptanceStuckOut.Rows[rowcount][9] = DBNull.Value;
							dtAcceptanceStuckOut.Rows[rowcount][10] = DBNull.Value;
							dtAcceptanceStuckOut.Rows[rowcount][11] =
								((row.FindControl("txtbxAWGBC") as TextBox).Text.Equals("&nbsp;") || (row.FindControl("txtbxAWGBC") as TextBox).Text.Equals("")) ? string.Empty : (row.FindControl("txtbxAWGBC") as TextBox).Text;
							DropDownList drpdwnStruckType = (DropDownList)row.FindControl("drpdwnStruckOutType");
							dtAcceptanceStuckOut.Rows[rowcount][12] = drpdwnStruckType.SelectedItem.Text;
							dtAcceptanceStuckOut.Rows[rowcount][13] = DateTime.Now;
							dtAcceptanceStuckOut.Rows[rowcount][14] = 1;
							dtAcceptanceStuckOut.Rows[rowcount][15] = 1;
							rowcount++;
						}
					}



					foreach (GridViewRow row in grdvwHorseDetail.Rows)
					{

						DropDownList ddl = (DropDownList)row.FindControl("drpdwnBifurcation");
                        //var check = ddl.Items.Count;

                        if (!ddl.SelectedItem.Text.Trim().Equals("---Struck Out---"))
						{
							rowcountacceptance = dtHandicap.Rows.Count;
							dtHandicap.Rows.Add();

							if (txtbxHandicapEnterDate.Text.Equals("__-__-____"))
							{
								dtHandicap.Rows[rowcountacceptance][0] = DBNull.Value;
							}
							else
							{
								string[] dateString = txtbxHandicapEnterDate.Text.Split('-');
								DateTime enterDate =
									Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
								dtHandicap.Rows[rowcountacceptance][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
							}
							dtHandicap.Rows[rowcountacceptance][1] = txtbxRaceDate.Text;
							dtHandicap.Rows[rowcountacceptance][2] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
							dtHandicap.Rows[rowcountacceptance][3] = Convert.ToInt32(ViewState["GeneralRaceID"]);
							dtHandicap.Rows[rowcountacceptance][4] = Convert.ToInt32(hdnfieldGeneralRaceNameID.Value);
							dtHandicap.Rows[rowcountacceptance][5] = Convert.ToInt32((row.FindControl("drpdwnBifurcation") as DropDownList).SelectedItem.Value);
							dtHandicap.Rows[rowcountacceptance][6] = Convert.ToInt32((row.FindControl("txtbxHno") as TextBox).Text);
							dtHandicap.Rows[rowcountacceptance][7] = Convert.ToInt32((row.FindControl("hdnfieldhorseid") as HiddenField).Value);
							dtHandicap.Rows[rowcountacceptance][8] = Convert.ToInt32((row.FindControl("hdnfieldhorsenameid") as HiddenField).Value);
							dtHandicap.Rows[rowcountacceptance][9] = string.Empty;
							dtHandicap.Rows[rowcountacceptance][10] = DBNull.Value;
							dtHandicap.Rows[rowcountacceptance][11] =
								((row.FindControl("lblAWAWRL") as Label).Text.Equals("&nbsp;") || (row.FindControl("lblAWAWRL") as Label).Text.Equals("")) ? string.Empty : (row.FindControl("lblAWAWRL") as Label).Text;
							dtHandicap.Rows[rowcountacceptance][12] =
								((row.FindControl("txtbxAWGBC") as TextBox).Text.Equals("&nbsp;") || (row.FindControl("txtbxAWGBC") as TextBox).Text.Equals("")) ? string.Empty : (row.FindControl("txtbxAWGBC") as TextBox).Text;
							dtHandicap.Rows[rowcountacceptance][13] = DateTime.Now;
							dtHandicap.Rows[rowcountacceptance][14] = 1;
							dtHandicap.Rows[rowcountacceptance][15] = 1;
							rowcountacceptance++;
						}

					}

                    //var value = Convert.ToInt32((row.FindControl("drpdwnBifurcation") as DropDownList).SelectedItem.Value);
                    //divisionraceidcount
                    var length = divisionraceidcount.Length;
                    DataRow[] result1;
                    DataRow[] result2;
                    DataRow[] result3;
                    DataRow[] result4;
                    bool checkstatus = true;
                    if (length == 1)
                    {
                        result1 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[0].ToString());
                        if (result1.Length > 0)
                            checkstatus = true;
                        else
                            checkstatus = false;
                    }
                    if (length == 2)
                    {
                        result1 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[0].ToString());
                        result2 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[1].ToString());
                        if (result1.Length > 0 && result2.Length > 0)
                            checkstatus = true;
                        else
                            checkstatus = false;
                    }
                    if (length == 3)
                    {
                        result1 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[0].ToString());
                        result2 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[1].ToString());
                        result3 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[2].ToString());

                        if (result1.Length > 0 && result2.Length > 0 && result3.Length > 0)
                            checkstatus = true;
                        else
                            checkstatus = false;
                    }
                    if (length == 4)
                    {
                        result1 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[0].ToString());
                        result2 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[1].ToString());
                        result3 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[2].ToString());
                        result4 = dtHandicap.Select("DivisionedRaceID=" + divisionraceidcount[3].ToString());
                        if (result1.Length > 0 && result2.Length > 0 && result3.Length > 0 && result4.Length > 0)
                            checkstatus = true;
                        else
                            checkstatus = false;

                    }

                    if (checkstatus.Equals(false))
                    {
                        var message = "Please enter all divisioned race entry.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                         var result = new CardsBL().AddAcceptance(dtHandicap, dtAcceptanceStuckOut);
                       // var result = 0;
                        if (result == 1)
                        {
                            ClearAll("HorseAdd");
                            BindDropDown(drpdwnRaceNameS, "CardAcceptance", "GeneralRaceName", "GeneralRaceNameID");
                            var dt = new CardsBL().HorseInformation(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "SelectAcceptance", 0, 1, txtbxHandicapEnterDate.Text, false, false, 0);
                            if (dt.Rows.Count > 0)
                            {
                                grdvwHorseDetail.DataSource = dt;
                                grdvwHorseDetail.DataBind();
                            }
                            else
                            {
                                grdvwHorseDetail.DataSource = new DataTable();
                                grdvwHorseDetail.DataBind();
                            }

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
                lblEntryDate.Text = "";
                grdvwHorseDetail.DataSource = new DataTable();
                grdvwHorseDetail.DataBind();
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
            ds = new CardsBL().GetDisplayGridviewData(txtbxRaceDate.Text, Convert.ToInt32(drpdwnRaceNameS.SelectedItem.Value), drpdwnDisplay.SelectedItem.Text, "Acceptance");
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

        protected void txtbxJockeyNameG_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as TextBox).NamingContainer;
                HiddenField hdnfieldProfessionalNameid = row.FindControl("hdnfieldHorseNameidG") as HiddenField;
                var test = hdnfieldProfessionalNameid.Value;
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
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

                HiddenField hdnfieldacceptanceid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldAcceptanceID") as HiddenField;
                int acceptanceid = Convert.ToInt32(hdnfieldacceptanceid.Value);

                HiddenField hdnfieldacceptancestruckoutid = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldAcceptanceStruckOutID") as HiddenField;
                int acceptancestruckoutid = Convert.ToInt32(hdnfieldacceptancestruckoutid.Value);

                HiddenField hdnfieldHorseID_FK = GvShowALL.Rows[e.RowIndex].FindControl("hdnfieldHorseID_FK") as HiddenField;
                int horseid = Convert.ToInt32(hdnfieldHorseID_FK.Value);



                string generalracename = ((Label)GvShowALL.Rows[e.RowIndex].FindControl("lblGeneralRaceName")).Text;
                string divisionracename = ((DropDownList)GvShowALL.Rows[e.RowIndex].FindControl("drpdwnBifurcationS")).SelectedItem.Text;
				int divisionraceid = Convert.ToInt32(((DropDownList)GvShowALL.Rows[e.RowIndex].FindControl("drpdwnBifurcationS")).SelectedItem.Value);
				string hno = ((TextBox)GvShowALL.Rows[e.RowIndex].FindControl("txtHNoS")).Text;
                string awgbcs = ((TextBox)GvShowALL.Rows[e.RowIndex].FindControl("txtAWGBCS")).Text;
                string strucouttype = ((DropDownList)GvShowALL.Rows[e.RowIndex].FindControl("drpdwnStruckOuttypeS")).SelectedItem.Text;
                int result = new CardsBL()
                        .AcceptanceUpdate(id,generalracename,generalracenameid,generalracedate,divisionraceid,divisionracename,
                        Convert.ToInt32(hno),awgbcs,strucouttype,acceptanceid,acceptancestruckoutid, horseid, 0);
                if (result >= 1)
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
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "Card_Acceptance");


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