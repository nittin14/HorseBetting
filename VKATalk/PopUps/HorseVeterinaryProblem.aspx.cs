using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class HorseVeterinaryProblem : System.Web.UI.Page
    {
        int _horseId = 0;
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                if (!Request.QueryString["HorseNameID"].Equals(""))
                {
                    _value = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                    horseId.Value = Request.QueryString["HorseNameID"];
                    _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                }
                if (!IsPostBack)
                {
                    horseinformation();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

		protected void btnClear_Click(object sender, EventArgs e)
		{
			ClearAllSelection(this);
			ShowHorseName();
			btnSave.Text = "Add";
		}
		public void horseinformation()
        {
            if (_horseId != 0)
            {
				ShowHorseName();
			}
            else
            {
                hdnfieldGeneralRaceNameID.Value = Request.QueryString["GeneralRaceNameID"].ToString();
            }
           // BindDropDown(this.drpdwnCommon, "HorseVet", "Disease", "DiseaseID");
           // drpdwnCommon.Items.Insert(0, new ListItem("-- Please select --", "-1"));
            BindData();
            if (Request.QueryString["PageName"].Equals("1"))
            {
                txtbxHorseName.Enabled = false;
            }
            else
            {
                txtbxHorseName.Enabled = true;
            }
        }

		public void ShowHorseName()
		{
			var reqQueryValue = string.Empty;
			if (!(Request.QueryString["HorseName"].Equals("") || Request.QueryString["HorseName"].Equals("NULL")))
			{
				reqQueryValue = Request.QueryString["HorseName"];
                string[] horseName = reqQueryValue.Split(',');
                txtbxHorseName.Text = horseName[1];
                if (Request.QueryString["PageName"].Equals("1"))
                {
                    txtbxHorseName.Text = horseName[1];
                    lblHorseNameSecond.Text = horseName[2];
                }
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
        public static List<string> AddHorseList(string prefixText, int count, string contextKey)
        {
            //DataTable dt = new MasterHorseBL().GetHorseNameAutoFillAcceptanceCard("HorseAcceptanceList", prefixText, contextKey);
            DataTable dt = new CardsBL().GetHorseNameAutoFiller("CardHorseListWithoutDate", prefixText, "");
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
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
        public static List<string> AddVietProblemList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("AddVietProblemList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }

        /// <summary>
        /// bind drop down 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TableName_"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new MasterHorseBL().GetHorseName(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }

        /// <summary>
        /// This is use for bind the gridview
        /// </summary>
        public void BindData()
        {
                MasterHorseBL Bl = new MasterHorseBL();
                DataTable dt = Bl.GetHorseDetail(Convert.ToString(_horseId), "HorseVet");
                if (dt.Rows.Count > 0)
                {
                    this.GvGlobal.DataSource = dt;
                    this.GvGlobal.DataBind();
                }
                else
                {
                    this.GvGlobal.DataSource = new DataTable();
                    this.GvGlobal.DataBind();
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
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseVet", prefixText);
            List<string> currentMissionComments = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentMissionComments.Add(dt.Rows[i][0].ToString());
            }
            return currentMissionComments;
        }

        /// <summary>
        /// This button Save the HorseName popup data in database and bind with dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["PageName"].Equals("2"))
                {
                    if (!hdnfieldHorseNameID.Value.Equals(""))
                    {
                        _horseId = Convert.ToInt32(hdnfieldHorseNameID.Value);
                    }
                }
               
                int status = 0;
                //DataSet ds = null;
                //if (btnSave.Text.Equals("Add"))
                //{
                //    ds = new MasterHorseBL().GetHorseTillDateValidation(_horseId, "HorseVet", txtbxFromDate.Text, btnSave.Text);
                //}
                //else
                //{
                //    ds = new MasterHorseBL().GetHorseTillDateValidation((int)ViewState["horseStatusID"], "HorseVet", txtbxFromDate.Text, btnSave.Text);
                //}

                //Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);

                if (_horseId == 0)
                {
                    string message = "Please select Horse name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                //else if (checkTillDate == 2)
                //{
                //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Check the existing record TillDate.');", true);
                //    string message = "Please Check the existing record TillDate.";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //}
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new MasterHorseBL().HorseVet(
                            _horseId,
                            Convert.ToInt32(this.hdnfieldVieProblemID.Value),
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxStartDate.Text,
                            txtbxEndDate.Text,
                            txtbxComment.Text,
                            1,
                            "Insert",
                            (lblDayProblem.Text.Equals("")) ? 0 : Convert.ToInt32(lblDayProblem.Text),
                            (lblTreatment.Text.Equals("")) ? 0 : Convert.ToInt32(lblTreatment.Text));
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = new MasterHorseBL().HorseVet(
                            (int)ViewState["horseStatusID"],
                            Convert.ToInt32(this.hdnfieldVieProblemID.Value),
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxStartDate.Text,
                            txtbxEndDate.Text,   
                            txtbxComment.Text,
                            1,
                            "Update",
                            (lblDayProblem.Text.Equals("")) ? 0 : Convert.ToInt32(lblDayProblem.Text),
                            (lblTreatment.Text.Equals("")) ? 0 : Convert.ToInt32(lblTreatment.Text));
                       // btnSave.Text = "Add";
                    }
                    if (status == 1)
                    {
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        horseinformation();
                    }
                    else if (status == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        horseinformation();
                    }
                    else if (status == 5)
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        horseinformation();
                    }
                    else if (status == 4)
                    {
                        string message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        horseinformation();
                    }
                    else
                    {
                        ErrorHandling.CheckEachSteps(Convert.ToString(status));
                        string message = "Issue in Record. (Status) : " + status;
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
				ShowHorseName();
			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        /// <summary>
        /// Close the window and pass the value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Request.QueryString["PageName"].Equals("1"))
                {
                    if (horseId.Value != "")
                    {
                        Session["HorseID"] = horseId.Value;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "CloseWindow();", true);
                    Session["HorseID"] = null;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["horseStatusID"].Equals(""))
                {
                    new MasterHorseBL().HorseVet((int)ViewState["horseStatusID"],
                        Convert.ToInt32(this.hdnfieldVieProblemID.Value),txtbxFromDate.Text, txtbxTillDate.Text,
                        txtbxStartDate.Text,txtbxEndDate.Text,txtbxComment.Text, 1, "Delete",0,0);
                    ClearAllSelection(this);
                    BindData();
					ShowHorseName();
					btnSave.Text = "Add";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Record Deleted Successfully.');", true); 
                    ViewState["horseStatusID"] = "";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('No Record Found.');", true); 
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }

        }

        protected void txtbxTillDate_TextChanged(object sender, EventArgs e)
        {
            try{
                if(!txtbxTillDate.Text.Equals("__-__-____"))
                {
                CalculateDay();
                }
                else{
                    lblDayProblem.Text=string.Empty;
                }
                //CalculateTreatmentDay();
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }

        protected void txtbxEndDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(!txtbxEndDate.Text.Equals("__-__-____"))
                {
                CalculateTreatmentDay();
                }
                else{
                    lblTreatment.Text=string.Empty;
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }

        public void CalculateDay()
        {
            if (!txtbxTillDate.Text.Equals("__-__-____"))
            {
                var days = (Convert.ToDateTime(txtbxTillDate.Text) - Convert.ToDateTime(txtbxFromDate.Text)).TotalDays + 1;
                lblDayProblem.Text = days.ToString();
            }
        }

        public void CalculateTreatmentDay()
        {
            if (!txtbxEndDate.Text.Equals("__-__-____"))
            {
                var days = (Convert.ToDateTime(txtbxEndDate.Text) - Convert.ToDateTime(txtbxStartDate.Text)).TotalDays;
                lblTreatment.Text = days.ToString();
            }
        }
        protected void GvGlobal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = this.GvGlobal.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    HiddenField hdnfieldDiseaseID = (HiddenField)row.FindControl("hdnfieldDiseaseID");
                    HiddenField hdnfieldHorseNameG = (HiddenField)row.FindControl("hdnfieldHorseNameG");
                    hdnfieldVieProblemID.Value = hdnfieldDiseaseID.Value;
                   // ClearAllSelection(this);
                    ViewState["horseStatusID"] = dataKey.Value;
                   // this.drpdwnCommon.Items.FindByText(hdnval.Value).Selected = true;
                    txtbxVietProblem.Text = hdnval.Value;
                    txtbxFromDate.Text = row.Cells[1].Text;
                    txtbxHorseName.Text = hdnfieldHorseNameG.Value;
                    //_horseId = Convert.ToInt32(hdnfieldHorseNameID.Value);
                    if (!row.Cells[2].Text.Contains("&nbsp;"))
                    {
                        txtbxTillDate.Text = row.Cells[2].Text;
                       
                    }
                    if (!row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        lblDayProblem.Text = row.Cells[3].Text;
                    }
                    if (!row.Cells[4].Text.Contains("&nbsp;"))
                    {
                        txtbxStartDate.Text = row.Cells[4].Text;
                    }
                    if (!row.Cells[5].Text.Contains("&nbsp;"))
                    {
                        txtbxEndDate.Text = row.Cells[5].Text;
                    }
                    if (!row.Cells[6].Text.Contains("&nbsp;"))
                    {
                        lblTreatment.Text = row.Cells[6].Text;
                    }
                    //if(!(txtbxTillDate.Text.Equals("") && txtbxEndDate.Text.Equals("")))
                    if (row.Cells[7].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[7].Text.Equals("&nbsp;") ? "" : row.Cells[7].Text.Contains("&quot;") ? row.Cells[7].Text.Replace("&quot;", "\"") : row.Cells[7].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[7].Text.Equals("&nbsp;") ? "" : row.Cells[7].Text.Contains("&#39;") ? row.Cells[7].Text.Replace("&#39;", "'") : row.Cells[7].Text;
                    }
                }
				ShowHorseName();
			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }


        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    ClearAllSelection(this);
        //    horseinformation();
        //    btnSave.Text = "Add";
        //}
        
        public void ClearAllSelection(Control parent)
        {
            if (chkbxfix.Checked.Equals(true))
            {

                txtbxVietProblem.Text = string.Empty;
                hdnfieldVieProblemID.Value = string.Empty;
                txtbxFromDate.Text = string.Empty;
                txtbxTillDate.Text = string.Empty;
                lblDayProblem.Text = string.Empty;
                txtbxStartDate.Text = string.Empty;
                txtbxEndDate.Text = string.Empty;
                lblTreatment.Text = string.Empty;
                txtbxComment.Text = string.Empty;
            }
            else
            {
                txtbxHorseName.Text = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                hdnfieldHorseName.Value = string.Empty;
                horseId.Value = string.Empty;
                lblHorseNameSecond.Text = string.Empty;

                txtbxVietProblem.Text = string.Empty;
                hdnfieldVieProblemID.Value = string.Empty;
                txtbxFromDate.Text = string.Empty;
                txtbxTillDate.Text = string.Empty;
                lblDayProblem.Text = string.Empty;
                txtbxStartDate.Text = string.Empty;
                txtbxEndDate.Text = string.Empty;
                lblTreatment.Text = string.Empty;
                txtbxComment.Text = string.Empty;
            }
            GvGlobal.DataSource = new DataTable();
            GvGlobal.DataBind();
        }


        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_VeterinaryProblem"))
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
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "HorseVet");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse Vet");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_VeterinaryProblem.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        BindData();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('All Record has been added successfully.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('No Record Found.');", true);
                }
				ShowHorseName();
			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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
                //using (DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "HorseVet"))
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "HorseVet"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //dt.Columns.Remove("VeterinaryProblemID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_VeterinaryProblem");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_VeterinaryProblem.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                }
				ShowHorseName();
			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnfieldHorseNameID.ValidateRequestMode.Equals(""))
                {
                    string message = "Please select Horse Name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    hdnfieldHorseName.Value = txtbxHorseName.Text;
                    _horseId = Convert.ToInt32(hdnfieldHorseNameID.Value);
                    BindData();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
    }
}