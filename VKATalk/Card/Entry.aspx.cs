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

    public partial class Entry : System.Web.UI.Page
    {
        private int rownumber = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				txtbxEntryEnterDate.Text = CommonMethods.CurrentDate();
                BindDropDown(drpdwnGender, "HorseSex", "Alias", "HorseSexID");
                drpdwnGender.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnEntryStruckOutStage, "ProspectusGeneralDateType", "DateType", "GeneralDateTypeID");
                drpdwnEntryStruckOutStage.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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
            lblEntryDate.Text = "";
            lbl1stSupplimentry.Text = "";
            lbl2ndSupplimentry.Text = "";
            lblFinalEntry.Text = "";
            txtbxHorseName.Text = "";
           // lblGender.Text = "";
            //lblTrainer.Text = "";
            txtbxTrainer.Text = "";
            txtbxOwner.Text = "";
            txtbxRowNumber.Text = "";
            tblHorseEntryForm.Visible = false;
            grdvwHorseDetail.DataSource = new DataTable();
            grdvwHorseDetail.DataBind();
            lblRaceNameShow.Text = "";
            drpdwnGender.ClearSelection();
        }

      
        protected void txtbxRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtbxRaceDate_OnTextChanged
                
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
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdvwRaceDetail.SelectedRow;
               // DropDownList drpdwnEntry = (DropDownList)row.FindControl("drpdwnEntryType");
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    tblHorseEntryForm.Visible = true;
                    ViewState["GridViewRowID"] = dataKey.Value; //generalraceid
                    ViewState["GridViewRaceName"] = hdnval.Value;//reneralracename
                    lblRaceNameShow.Text = hdnval.Value;
                    ViewState["SerialNumber"] = row.Cells[0].Text;
                    hdnfieldGeneralRaceNameID.Value = Convert.ToString(dataKey.Value);
                    //Session["GeneralRaceName"] = hdnfieldGeneralRaceNameID.Value 
                   // ViewState["EntryType"] = drpdwnEntry.SelectedItem.Value;
                }
                var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(dataKey.Value),"Entry", lblSeason.Text,lblYear.Text);
                if (dsGeneralDate.Tables[0].Rows.Count > 0)
                {
                    if (dsGeneralDate.Tables[0].Rows.Count > 0)
                    {
                        lblEntryDate.Text = dsGeneralDate.Tables[0].Rows[0][0].ToString();
                    }
                    if (dsGeneralDate.Tables[1].Rows.Count > 0)
                    {
                        lbl1stSupplimentry.Text = dsGeneralDate.Tables[1].Rows[0][0].ToString();
                    }
                    if (dsGeneralDate.Tables[2].Rows.Count > 0)
                    {
                        lbl2ndSupplimentry.Text = dsGeneralDate.Tables[2].Rows[0][0].ToString();
                    }
                    if (dsGeneralDate.Tables[3].Rows.Count > 0)
                    {
                        lblFinalEntry.Text = dsGeneralDate.Tables[3].Rows[0][0].ToString();
                    }
                }
				else
				{
					lblEntryDate.Text = string.Empty;
					lbl1stSupplimentry.Text = string.Empty;
					lbl2ndSupplimentry.Text = string.Empty;
					lblFinalEntry.Text = string.Empty;
				}

                var dt = new CardsBL().HorseInformation(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0, 0, 0, 0, 0, 1, "Select", 0, 1,txtbxEntryEnterDate.Text,false,false,0);
                if (dt.Rows.Count > 0)
                {
                    grdvwHorseDetail.DataSource = dt;
                    grdvwHorseDetail.DataBind();
                    var localrownumber = 0;
                    //for (int count = 0; count < dt.Rows.Count; count++)
                    //{
                    //    localrownumber = Convert.ToInt32(dt.Rows[count][1]);
                    //}
                    localrownumber = Convert.ToInt32(dt.Rows[0][1]);
                    txtbxRowNumber.Text = Convert.ToString(localrownumber + 1);
                }
                else
                {
                    grdvwHorseDetail.DataSource = new DataTable();
                    grdvwHorseDetail.DataBind();
                    txtbxRowNumber.Text = Convert.ToString(rownumber);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
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
        public static List<string> AddTrainerList(string prefixText, int count, string contextKey)
        {
            DataTable dt = new CardsBL().GetHorseNameAutoFiller("CardTrainerList", prefixText, contextKey);

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

        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddOwnerList(string prefixText, int count, string contextKey)
        {
            DataTable dt = new CardsBL().GetHorseNameAutoFiller("CardOwnerList", prefixText, contextKey);

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


        protected void dvgrdviewHorseDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdvwHorseDetail.SelectedRow;
                var dataKey = grdvwHorseDetail.DataKeys[row.RowIndex];
               // var rownumber = string.Empty;
                if (dataKey != null)
                {
                    HiddenField hdnvalhorseName = (HiddenField)row.FindControl("hdnfieldStatus");
                    HiddenField hdnvalhorseNameID = (HiddenField)row.FindControl("hdnfieldhorsenameid");
                    HiddenField hdnvalTrainerNameID = (HiddenField)row.FindControl("hdnfieldTrainernameid");
                    HiddenField hdnvalOwnerNameID = (HiddenField)row.FindControl("hdnfieldOwnernameid");

                    hdnfieldhorseid.Value = hdnvalhorseNameID.Value;
                    hdnfieldTrainerID.Value = hdnvalTrainerNameID.Value;
                    hdnfieldOwnerID.Value = hdnvalOwnerNameID.Value;

                    Label lblHorseSerialNumber = (Label)row.FindControl("lblHorseSerialNumber");
                    txtbxRowNumber.Text = lblHorseSerialNumber.Text;

                    chkbxSweepStakeEntry.Checked = false;
                    
                    if(row.Cells[0].Text.Equals("Yes"))
                        chkbxSweepStakeEntry.Checked = true;
                    else
                        chkbxSweepStakeEntry.Checked = false;

                    drpdwnEntryType.ClearSelection();
                    drpdwnEntryType.Items.FindByText(row.Cells[1].Text).Selected = true;
                    tblHorseEntryForm.Visible = true;
                    ViewState["GridViewCardID"] = dataKey.Value; //generalraceid
                    //rownumber = row.Cells[0].Text;
                    txtbxHorseName.Text = hdnvalhorseName.Value;
                    drpdwnGender.ClearSelection();
                    drpdwnGender.Items.FindByText(row.Cells[4].Text).Selected = true;
                    Label lblTrainerName = (Label)row.FindControl("lblTrainer");
                    txtbxTrainer.Text = lblTrainerName.Text;
                    Label lblOwnerName = (Label)row.FindControl("lblOwner");
                    txtbxOwner.Text = lblOwnerName.Text;

                    chkbxEntrystruckout.Checked = false;

                    if (row.Cells[7].Text.Equals("Yes"))
                        chkbxEntrystruckout.Checked = true;
                    else
                        chkbxEntrystruckout.Checked = false;
                    drpdwnEntryStruckOutStage.ClearSelection();
                    if(!(row.Cells[8].Text.Equals("") || row.Cells[8].Text.Equals("&nbsp;")))
                            drpdwnEntryStruckOutStage.Items.FindByText(row.Cells[8].Text).Selected = true;
                    btnAdd.Text = "Update";
                }

               
            }
            catch (Exception ex)
            {
                btnAdd.Text = "Add";
                hdnfieldhorseid.Value = "";
                txtbxHorseName.Text = "";
                txtbxTrainer.Text = "";
                txtbxOwner.Text = "";
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
        public static List<string> AddHorseList(string prefixText, int count ,string contextKey)
        {
            DataTable dt = new CardsBL().GetHorseNameAutoFiller("CardHorseList", prefixText, contextKey);
           
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
                var dt = new CardsBL().GetRaceGeneralRaceDetail(
                    txtbxRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                  //  dvgridview.Visible = true;
                    lblSeason.Text = dt.Rows[0][11].ToString();
                    lblYear.Text = dt.Rows[0][12].ToString();
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
                else
                {
                  //  dvgridview.Visible = false;
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();
                    ClearAll("Other");
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


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (!ViewState["GridViewCardID"].Equals(""))
				{
					//new MasterHorseBL().HorseSex((int)ViewState["horseStatusID"], Convert.ToInt32(this.drpdwnGender.SelectedItem.Value), txtbxFromDate.Text, txtbxTillDate.Text,
					//txtbxComment.Text, 1, "Delete");
					//ClearAllSelection(this);
					//BindData();
					DataTable dt = new CardsBL().HorseInformation(txtbxRaceDate.Text, 0,
					Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 0, 0,
					0, 0,
					0, 0, 1, "EntryDelete",
					Convert.ToInt32(ViewState["GridViewCardID"]), 1, txtbxEntryEnterDate.Text,false,false,0);
					var message = "Record Deleted Successfully.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					btnAdd.Text = "Add";
					if (dt.Rows.Count > 0)
					{
						grdvwHorseDetail.DataSource = dt;
						grdvwHorseDetail.DataBind();
						var localrownumber = 0;
                        //for (int count = 0; count < dt.Rows.Count; count++)
                        //{
                        //	localrownumber = Convert.ToInt32(dt.Rows[count][1]);
                        //}
                        localrownumber = Convert.ToInt32(dt.Rows[0][1]);
                        txtbxRowNumber.Text = Convert.ToString(localrownumber + 1);
					}
					else
					{
						grdvwHorseDetail.DataSource = new DataTable();
						grdvwHorseDetail.DataBind();
					}
					ClearAll("HorseAdd");
					ViewState["GridViewCardID"] = "";
				}
				else
				{
					string message = "No Record Found.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}

			}
			catch (Exception ex)
			{
				ErrorHandling.SendErrorToText(ex);
				string message = "Incorrect Information.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
			}

		}
		protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                DataTable dt;
                if (btnAdd.Text.Equals("Add"))
                {
                    dt = new CardsBL().HorseInformation(txtbxRaceDate.Text,Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 
                                Convert.ToInt32(drpdwnEntryType.SelectedItem.Value), Convert.ToInt32(ViewState["SerialNumber"]), Convert.ToInt32(hdnfieldhorseid.Value), 
                                    Convert.ToInt32(drpdwnGender.SelectedItem.Value), Convert.ToInt32(hdnfieldTrainerID.Value), Convert.ToInt32(hdnfieldOwnerID.Value), 1, "Insert", 
                                    Convert.ToInt32(ViewState["GridViewCardID"]), 1,txtbxEntryEnterDate.Text, Convert.ToBoolean(chkbxSweepStakeEntry.Checked),
                                    Convert.ToBoolean(chkbxEntrystruckout.Checked), Convert.ToInt32(drpdwnEntryStruckOutStage.SelectedItem.Value));
                }
                else
                {
                    dt = new CardsBL().HorseInformation(txtbxRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldGeneralRaceNameID.Value), 
                                        Convert.ToInt32(drpdwnEntryType.SelectedItem.Value), Convert.ToInt32(txtbxRowNumber.Text), Convert.ToInt32(hdnfieldhorseid.Value), 
                                                Convert.ToInt32(drpdwnGender.SelectedItem.Value), Convert.ToInt32(hdnfieldTrainerID.Value), Convert.ToInt32(hdnfieldOwnerID.Value), 1, 
                                                    "EntryUpdate", Convert.ToInt32(ViewState["GridViewCardID"]), 1, txtbxEntryEnterDate.Text, Convert.ToBoolean(chkbxSweepStakeEntry.Checked),
                                    Convert.ToBoolean(chkbxEntrystruckout.Checked), Convert.ToInt32(drpdwnEntryStruckOutStage.SelectedItem.Value));
                    btnAdd.Text = "Add";
                }
                if (dt.Rows.Count > 0)
                {
					if (Convert.ToInt32(dt.Rows[0][0]) == 4) {
						var message = "Record already exists.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					}
					else if (Convert.ToInt32(dt.Rows[0][0]) == 5)
					{
						var message = "Record activated successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					}
					else
					{
						grdvwHorseDetail.DataSource = dt;
						grdvwHorseDetail.DataBind();
						var localrownumber = 0;
                        localrownumber = Convert.ToInt32(dt.Rows[0][1]);
                        txtbxRowNumber.Text = Convert.ToString(localrownumber + 1);
					}
                }
                else
                {
                    grdvwHorseDetail.DataSource = new DataTable();
                    grdvwHorseDetail.DataBind();
                }
               
                ClearAll("HorseAdd");
             }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void ClearAll(string methodname)
        {
			if(methodname.Equals("HorseAdd"))
			{
				txtbxHorseName.Text = string.Empty;
				txtbxHorseName.Focus();
				hdnfieldTrainerID.Value = "";
				txtbxTrainer.Text = "";

				hdnfieldOwnerID.Value = "";
				txtbxOwner.Text = "";
				drpdwnGender.ClearSelection();
                drpdwnEntryStruckOutStage.ClearSelection();
                drpdwnEntryType.ClearSelection();
                chkbxEntrystruckout.Checked = false;

            }
			else
			{
				lblSeason.Text = "";
				lblYear.Text = "";
				txtbxHorseName.Text = string.Empty;
				txtbxHorseName.Focus();
				hdnfieldTrainerID.Value = "";
				txtbxTrainer.Text = "";

				hdnfieldOwnerID.Value = "";
				txtbxOwner.Text = "";
				if (Convert.ToInt32(drpdwnCenterName.SelectedItem.Value).Equals(-1))
				{
					lblEntryDate.Text = "";
					lbl1stSupplimentry.Text = "";
					lbl2ndSupplimentry.Text = "";
					lblFinalEntry.Text = "";
					tblHorseEntryForm.Visible = false;
					grdvwHorseDetail.DataSource = new DataTable();
					grdvwHorseDetail.DataBind();
				}
				drpdwnGender.ClearSelection();
                drpdwnEntryStruckOutStage.ClearSelection();
                //chkbxSweepStakeEntry.Checked = false;
                chkbxEntrystruckout.Checked = false;
            }


            if (!chkbxFix.Checked.Equals(true))
            {
                chkbxSweepStakeEntry.Checked = false;
                drpdwnEntryType.ClearSelection();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
                txtbxHorseName.Text = "";
                drpdwnGender.ClearSelection();
                txtbxTrainer.Text = "";
                txtbxOwner.Text = "";
                hdnfieldOwnerID.Value = "";
                hdnfieldTrainerID.Value = "";
                hdnfieldhorseid.Value = "";
				btnAdd.Text = "Add";
                drpdwnEntryStruckOutStage.ClearSelection();
                chkbxSweepStakeEntry.Checked = false;
                chkbxEntrystruckout.Checked = false;
                chkbxFix.Checked = false;
                drpdwnEntryType.ClearSelection();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearAll("Close");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }

       

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Entry"))
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
                    for(int count=0; count < dt.Rows.Count; count++)
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
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "Card_Entry");


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
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_Entry"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Entry");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Entry.xlsx");
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