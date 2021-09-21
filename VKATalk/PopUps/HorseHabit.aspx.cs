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

    public partial class HorseHabit : System.Web.UI.Page
    {
        int _horseId = 0;
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
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
                    if (_horseId != 0)
                    {
						horseinformation();
						//var reqQueryValue = string.Empty;
      //                  if (!Request.QueryString["HorseName"].Equals(""))
      //                  {
      //                      reqQueryValue = Request.QueryString["HorseName"];
      //                  }
      //                  string[] horseName = reqQueryValue.Split(',');
      //                  lblHorseNameFirst.Text = horseName[1];
      //                  lblHorseNameSecond.Text = horseName[2];
      //                  BindDropDown(this.drpdwnCommon1, "HorseHabitType", "Habit", "HabitID");
      //                  drpdwnCommon1.Items.Insert(0, new ListItem("-- Please select --", "-1"));
      //                  BindData();
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
			BindDropDown(this.drpdwnCommon1, "HorseHabitType", "Habit", "HabitID");
			drpdwnCommon1.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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
			if (!Request.QueryString["HorseName"].Equals(""))
			{
				reqQueryValue = Request.QueryString["HorseName"];
			}
			string[] horseName = reqQueryValue.Split(',');
			txtbxHorseName.Text = horseName[1];
			if (Request.QueryString["PageName"].Equals("1"))
			{
				txtbxHorseName.Text = horseName[1];
				lblHorseNameSecond.Text = horseName[2];
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
			DataTable dt = new MasterHorseBL().GetHorseNameAutoFillAcceptance("HorseAcceptanceList", prefixText, contextKey);
			List<string> horseList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
						dt.Rows[i][1].ToString(),
						Convert.ToString(dt.Rows[i][0])));
			}
			return horseList;
		}

		protected void drpdwnCommon1_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {

                GetHabitInformation();
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        public void GetHabitInformation()
        {
            DataTable dt = null;
            int entrytype = 0;
            dt = new MasterHorseBL().GetHorseHabitInformation(Convert.ToInt32(drpdwnCommon1.SelectedItem.Value));

            if (dt.Rows.Count > 0)
            {
                lblAlias.Text = dt.Rows[0][0].ToString();
                lblHabitType.Text = dt.Rows[0][1].ToString();

            }
            else
            {
                lblAlias.Text = "";
                lblHabitType.Text = string.Empty;
            }
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
                DataTable dt = Bl.GetHorseDetail(Convert.ToString(_horseId), "HorseHabit");
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
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseHabit", prefixText);
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
                int status = 0;
                
                DataSet ds = null;
                if (btnSave.Text.Equals("Add"))
                {
                    ds = new MasterHorseBL().GetHorseTillDateValidationMultiple(_horseId, "HorseHabit", txtbxFromDate.Text, btnSave.Text, drpdwnCommon1.SelectedItem.Value, string.Empty);
                }
                else
                {
                    ds = new MasterHorseBL().GetHorseTillDateValidationMultiple((int)ViewState["horseStatusID"], "HorseHabit", txtbxFromDate.Text, btnSave.Text, drpdwnCommon1.SelectedItem.Value, string.Empty);
                }
                Int64 checkTillDate = Convert.ToInt64(ds.Tables[0].Rows[0][0]);

                
                if (_horseId == 0)
                {
                    string message = "Please select Horse name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (checkTillDate == 2)
                {
                    string message = "Please Check the existing record TillDate.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new MasterHorseBL().HorseHabits(
                            _horseId,
                            Convert.ToInt32(this.drpdwnCommon1.SelectedItem.Value),
                            0,
                            txtbxSaddleNo.Text,
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxComment.Text,
                            1,
                            "Insert");
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = new MasterHorseBL().HorseHabits(
                            (int)ViewState["horseStatusID"],
                            Convert.ToInt32(this.drpdwnCommon1.SelectedItem.Value),
                            0,
                            txtbxSaddleNo.Text,
                            txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxComment.Text,
                            1,
                            "Update");
                    }
                    if (status == 1)
                    {
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);

                    }
                    else if (status == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                    }
                    else if (status == 5)
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                    }
                    else if (status == 4)
                    {
                        string message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
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
                if (horseId.Value != "")
                {
                    Session["HorseID"] = horseId.Value;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    new MasterHorseBL().HorseHabits((int)ViewState["horseStatusID"],
                        0,0, 
                        txtbxSaddleNo.Text,txtbxFromDate.Text, txtbxTillDate.Text,
                        txtbxComment.Text, 1, "Delete");
                    ClearAllSelection(this);
                    BindData();
					ShowHorseName();
					btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["horseStatusID"] = "";
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    ClearAllSelection(this);
                    ViewState["horseStatusID"] = dataKey.Value;
                    drpdwnCommon1.Items.FindByText(hdnval.Value).Selected = true;
                    GetHabitInformation();
                    if (!row.Cells[1].Text.Contains("&nbsp;"))
                    {
                        txtbxSaddleNo.Text = row.Cells[1].Text;
                    }
                    txtbxFromDate.Text = row.Cells[2].Text;
                    if (!row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        txtbxTillDate.Text = row.Cells[3].Text;
                    }
                    if (row.Cells[4].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&quot;") ? row.Cells[4].Text.Replace("&quot;", "\"") : row.Cells[4].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
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


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
			ShowHorseName();
			btnSave.Text = "Add";
        }
        
        public void ClearAllSelection(Control parent)
        {
            drpdwnCommon1.ClearSelection();
            lblAlias.Text = string.Empty;
            lblHabitType.Text = string.Empty;

            foreach (Control x in parent.Controls)
            {
                if ((x.GetType() == typeof(TextBox)))
                {

                    ((TextBox)(x)).Text = "";
                }

                if ((x.GetType() == typeof(DropDownList)))
                {

                    ((DropDownList)(x)).ClearSelection();
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }

            
        }


        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_Habit"))
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
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "HorseHabit");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_Habit");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_Habit.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        BindData();
                        var message = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                else
                {
                    var message = "No Record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
        /// Export Gridview Data in Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //using (DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "HorseHabit"))
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "HorseHabit"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                       // dt.Columns.Remove("HorseHabitID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_Habit");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_Habit.xlsx");
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
    }
}