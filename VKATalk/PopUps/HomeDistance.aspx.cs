using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class HomeDistance : System.Web.UI.Page
    {
        int _userId = 1;
        int _horseId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["HorseNameID"].Equals(""))
            {
                _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                horseId.Value = Request.QueryString["HorseNameID"];
            }
            if (!IsPostBack)
            {
                try
                {

                    if (_horseId != 0)
                    {
                        var reqQueryValue = string.Empty;
                        if (!Request.QueryString["HorseName"].Equals(""))
                        {
                            reqQueryValue = Request.QueryString["HorseName"];
                        }
                        string[] horseName = reqQueryValue.Split(',');
                        lblHorseNameFirst.Text = horseName[1];
                        lblHorseNameSecond.Text = horseName[2];
                        
                        BindDropDown(drpdwnHomeDistance, "Distance", "Distance", "DistanceID");
                        BindDropDown(this.drpdwnSupportType, "SupportType", "SupportType", "SupportTypeID");
                        //BindDropDown(this.drpdwnSupportLevel, "SupportLevel", "SupportLevel", "SupportLevelID");
                        drpdwnHomeDistance.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        drpdwnSupportType.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        drpdwnSupportLevel.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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


        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new MasterHorseBL().GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
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
                    ds = new MasterHorseBL().GetHorseTillDateValidationMultiple(_horseId, "HomeDistance", txtbxFromDate.Text, btnSave.Text, drpdwnHomeDistance.SelectedItem.Value, drpdwnSupportType.SelectedItem.Value);
                }
                else
                {
                    ds = new MasterHorseBL().GetHorseTillDateValidationMultiple((int)ViewState["horseRecordID"], "HomeDistance", txtbxFromDate.Text, btnSave.Text, drpdwnHomeDistance.SelectedItem.Value, drpdwnSupportType.SelectedItem.Value);
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
                        status = new MasterHorseBL().HomeDistance(
                            _horseId,
                            Convert.ToInt32(this.drpdwnHomeDistance.SelectedItem.Value),
                            Convert.ToInt32(this.drpdwnSupportType.SelectedItem.Value),
                            this.drpdwnSupportLevel.SelectedItem.Value,
                            this.txtbxFromDate.Text,
                            this.txtbxTillDate.Text,
                            this.txtbxComment.Text,
                            this._userId,
                            "Insert");
                    }
                    else
                    {
                        status = new MasterHorseBL().HomeDistance(
                            (int)ViewState["horseRecordID"],
                            Convert.ToInt32(this.drpdwnHomeDistance.SelectedItem.Value),
                            Convert.ToInt32(this.drpdwnSupportType.SelectedItem.Value),
                            this.drpdwnSupportLevel.SelectedItem.Value,
                            this.txtbxFromDate.Text,
                            this.txtbxTillDate.Text,
                            this.txtbxComment.Text,
                            this._userId,
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
            }
            catch (Exception ex)
            {
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
        public static List<string> AddCommentsList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HomeDistanceComments", prefixText);
            List<string> commentList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                commentList.Add(dt.Rows[i][0].ToString());
            }
            return commentList;
        }


        public void ClearAllSelection(Control parent)
        {

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
         /// <summary>
        /// BindData with Gridview
        /// </summary>
        private void BindData()
        {
            try
            {
               DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "HomeDistance");
               if (ds.Tables[0].Rows.Count > 0)
                {
                    this.GvHomeDistance.DataSource = ds.Tables[0];
                    this.GvHomeDistance.DataBind();
                }
                else
                {
                    this.GvHomeDistance.DataSource = new DataTable();
                    this.GvHomeDistance.DataBind();
                }
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
            //this.DataBind();
            btnSave.Text = "Add";
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
                if (!ViewState["horseRecordID"].Equals(""))
                {
                    //var status = new MasterHorseBL().HorseProfile((int)ViewState["horseRecordID"], 0, "", "__-__-____", "__-__-____", "", _userId, "Delete");
                    var status = new MasterHorseBL().HomeDistance((int)ViewState["horseRecordID"],0,0,"","__-__-____","__-__-____","",this._userId,"Delete");
                    if (status > 0)
                    {
                        ClearAllSelection(this);
                        BindData();
                        btnSave.Text = "Add";
                        var message = "Record deleted successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ViewState["horseRecordID"] = "";
                    }
                    else
                    {
                        var message = "Incorrect Information.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                else
                {
                    var message = "No record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception exception)
            {
                string message = "Incorrect Information.";
                ErrorHandling.SendErrorToText(exception);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvHomeDistance_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvHomeDistance.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvHomeDistance.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["horseRecordID"] = dataKey.Value;
                    this.drpdwnHomeDistance.Items.FindByText(hdnval.Value).Selected = true;
                    this.drpdwnSupportType.Items.FindByText(row.Cells[1].Text).Selected = true;
					drpdwnSupportLevel.ClearSelection();
					var supportlevel = new MasterHorseBL().GetHorseName("SupportType", Convert.ToInt32(drpdwnSupportType.SelectedItem.Value));
					if (supportlevel.Rows.Count > 0)
					{
						drpdwnSupportLevel.DataSource = supportlevel;
						drpdwnSupportLevel.DataTextField = "RatingMarkFormatStyle";
						drpdwnSupportLevel.DataValueField = "RatingMarkFormatStyleID";
						drpdwnSupportLevel.DataBind();
					}
					this.drpdwnSupportLevel.Items.FindByText(row.Cells[2].Text).Selected = true;
                    txtbxFromDate.Text = row.Cells[3].Text;
					if (!row.Cells[4].Text.Contains("&nbsp;"))
					{
						txtbxTillDate.Text = row.Cells[4].Text;
					}
						
                    if (row.Cells[5].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[5].Text.Equals("&nbsp;") ? "" : row.Cells[5].Text.Contains("&quot;") ? row.Cells[5].Text.Replace("&quot;", "\"") : row.Cells[5].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[5].Text.Equals("&nbsp;") ? "" : row.Cells[5].Text.Contains("&#39;") ? row.Cells[5].Text.Replace("&#39;", "'") : row.Cells[5].Text;
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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
               
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void drpdwnSupportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
				drpdwnSupportLevel.ClearSelection();
				var supportlevel = new MasterHorseBL().GetHorseName("SupportType", Convert.ToInt32(drpdwnSupportType.SelectedItem.Value));
				if (supportlevel.Rows.Count > 0)
				{
					drpdwnSupportLevel.DataSource = supportlevel;
					drpdwnSupportLevel.DataTextField = "RatingMarkFormatStyle";
					drpdwnSupportLevel.DataValueField = "RatingMarkFormatStyleID";
					drpdwnSupportLevel.DataBind();
				}
				//drpdwnSupportLevel.Items.Insert(0, new ListItem("-- Please select --", "-1"));
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_HomeDistance"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information');", true);
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
                    //var dtErrorResult = new MasterHorseBL().UploadExcelRecordBulkMinimumColumns(dt, "HorseHomeDistance");
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "HorseHomeDistance");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_HomeDistance");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_HomeDistance.xlsx");
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
                //using (DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "HorseHomeDistance"))
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "HorseHomeDistance"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                  //      dt.Columns.Remove("HorseOwnerStudID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_HomeDistance");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_HomeDistance.xlsx");
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