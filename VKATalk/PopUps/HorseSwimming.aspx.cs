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

    public partial class HorseSwimming : System.Web.UI.Page
    {
        int _horseId = 0;
        int _userId = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                if (!Request.QueryString["HorseNameID"].Equals(""))
                {
                    _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                    horseId.Value = Request.QueryString["HorseNameID"];
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
            //DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseName", prefixText);
            DataTable dt = new CardsBL().GetHorseNameAutoFiller("CardHorseListWithoutDate", prefixText, "");
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
        protected void btnClear_Click(object sender, EventArgs e)
		{
			ClearAllSelection(this);
			//ShowHorseName();
			btnSave.Text = "Add";
            drpdnWorkoutRating.Items.FindByText("0").Selected = true;
            chkbxIsShow.Checked = true;
        }
		public void horseinformation()
        {
            if (_horseId != 0)
            {
				//ShowHorseName();
			}
            else
            {
                hdnfieldGeneralRaceNameID.Value = Request.QueryString["GeneralRaceNameID"].ToString();
            }
            BindDropDown(this.drpdnWorkoutRating, "WorkoutRating", "Rating", "RatingCMID");
            //drpdwnCommon.Items.Insert(0, new ListItem("-- Please select --", "-1"));
            drpdnWorkoutRating.Items.FindByText("0").Selected = true;
            BindData();
            //if (Request.QueryString["PageName"].Equals("1"))
            //{
            //    txtbxHorseName.Enabled = false;
            //}
            //else
            //{
            //    txtbxHorseName.Enabled = true;
            //}
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
                DataTable dt = Bl.GetHorseDetail(hdnfieldHorseNameID.Value, "HorseSwimming");
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
        /// This button Save the HorseName popup data in database and bind with dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                if (hdnfieldHorseNameID.Value.Equals(""))
                {
                    string message = "Please select Horse name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                
                //hdnfieldHorseNameID
                else
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        status = new MasterHorseBL().HorseSwimming(
                            Convert.ToInt32(hdnfieldHorseNameID.Value)
                            , txtbxSwimmingDate.Text
                            , Convert.ToInt32(txtbxSwimminground.Text)
                            , Convert.ToInt32(drpdnWorkoutRating.SelectedItem.Value)
                            , ((chkbxIsShow.Checked.Equals(true)) ? "true" : "false")
                            , "Insert",string.Empty);
                    }
                    else if (btnSave.Text.Equals("Update"))
                    {
                        status = new MasterHorseBL().HorseSwimming(
                            Convert.ToInt32(hdnfieldHorseNameID.Value)
                            , txtbxSwimmingDate.Text
                            , Convert.ToInt32(txtbxSwimminground.Text)
                            , Convert.ToInt32(drpdnWorkoutRating.SelectedItem.Value)
                            , ((chkbxIsShow.Checked.Equals(true)) ? "true" : "false")
                            , "Update", ViewState["GlobalID"].ToString());

                        btnSave.Text = "Add";
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
                if (!ViewState["GlobalID"].Equals(""))
                {
                    new MasterHorseBL().HorseSwimming(
                            Convert.ToInt32(hdnfieldHorseNameID.Value)
                            , txtbxSwimmingDate.Text
                            , Convert.ToInt32(txtbxSwimminground.Text)
                            , Convert.ToInt32(drpdnWorkoutRating.SelectedItem.Value)
                            , ((chkbxIsShow.Checked.Equals(true)) ? "true" : "false")
                            , "Delete", ViewState["GlobalID"].ToString());
                    ClearAllSelection(this);
                    BindData();
					btnSave.Text = "Add";
                    drpdnWorkoutRating.Items.FindByText("0").Selected = true;
                    chkbxIsShow.Checked = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Record Deleted Successfully.');", true); 
                    ViewState["GlobalID"] = string.Empty;
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

        protected void GvGlobal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = this.GvGlobal.SelectedRow;
                //HiddenField hdnfieldHorseName = (HiddenField)row.FindControl("hdnfieldHorseName");
                var dataKey = GvGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                 //   ClearAllSelection(this);
                  //  HiddenField hdnfieldHorseNameIDG = (HiddenField)row.FindControl("hdnfieldHorseNameID");
                    HiddenField hdnfieldSwimmingDate = (HiddenField)row.FindControl("hdnfieldSwimmingDate");
                    ViewState["GlobalID"] = dataKey.Value;
                    //txtbxHorseName.Text = hdnfieldHorseName.Value;
                    //hdnfieldHorseNameID.Value = hdnfieldHorseNameIDG.Value;
                    txtbxSwimmingDate.Text = hdnfieldSwimmingDate.Value;
                    txtbxSwimminground.Text = row.Cells[1].Text;
                    drpdnWorkoutRating.ClearSelection();
                    this.drpdnWorkoutRating.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if(row.Cells[3].Text.Equals("Show"))
                    {
                        chkbxIsShow.Checked = true;
                    }
                    else
                    {
                        chkbxIsShow.Checked = false;
                    }
                }
			}
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }

        public void ClearAllSelection(Control parent)
        {
            if (chkbxfix.Checked.Equals(true))
            {
                txtbxSwimmingDate.Text = string.Empty;
                txtbxSwimminground.Text = string.Empty;
                drpdnWorkoutRating.ClearSelection();
                chkbxIsShow.Checked = true;
            }
            else
            {
                txtbxHorseName.Text = string.Empty;
                hdnfieldHorseNameID.Value = string.Empty;
                hdnfieldHorseName.Value = string.Empty;
                horseId.Value = string.Empty;

                txtbxSwimmingDate.Text = string.Empty;
                txtbxSwimminground.Text = string.Empty;
                drpdnWorkoutRating.ClearSelection();
                chkbxIsShow.Checked = true;
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_Swimming"))
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
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "Horse_Swimming");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse Swimming");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_Swimming.xlsx");
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
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "HorseSwimming"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //dt.Columns.Remove("VeterinaryProblemID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_Swimming");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_Swimming.xlsx");
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