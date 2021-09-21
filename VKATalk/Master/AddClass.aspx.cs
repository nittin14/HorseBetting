using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;

namespace VKATalk.Master
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Net;

    using OfficeOpenXml;

    public partial class AddClass : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        //CustomValidator err = new CustomValidator();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindDropDown(drpdwnRatingRange, "HandicapRatingRange", "HandicapRatingRange", "HandicapRatingRangeID");
                drpdwnRatingRange.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(DrpdwnCenter, "Center", "CenterName", "ID");
                DrpdwnCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTillYear, "Year", "YearName", "YearID");
                drpdwnTillYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnFromSeason, "Season", "SeasonName", "SeasonID");
                drpdwnFromSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTillSeason, "Season", "SeasonName", "SeasonID");
                drpdwnTillSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnClassType, "ClassType", "ClassType", "ClassTypeID");
                drpdwnClassType.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                //BindDropDown(drpdwnClassAlias, "ClassTypeAlias", "ClassTypeAlias", "ClassTypeID");
                //drpdwnClassAlias.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindData();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
        }

        /// <summary>
        /// Submit the Data of the Class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int status;
                if (BtnSubmit.Text.Equals("Add"))
                {
                    status = Bl.InserUpdatetMasterPagesDataClass(Convert.ToInt32(DrpdwnCenter.SelectedItem.Value), Convert.ToInt32(drpdwnFromYear.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillYear.SelectedItem.Value), Convert.ToInt32(drpdwnFromSeason.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillSeason.SelectedItem.Value), "", 
                        Convert.ToInt32(drpdwnClassType.SelectedItem.Value),
                        0, Convert.ToInt32(drpdwnRatingRange.SelectedItem.Value), "", 
                        "", 1, "Insert", 0);
                }
                else
                {
                    status = Bl.InserUpdatetMasterPagesDataClass(Convert.ToInt32(DrpdwnCenter.SelectedItem.Value), Convert.ToInt32(drpdwnFromYear.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillYear.SelectedItem.Value), Convert.ToInt32(drpdwnFromSeason.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillSeason.SelectedItem.Value), 
                        "",
                        Convert.ToInt32(drpdwnClassType.SelectedItem.Value), 0, 
                        Convert.ToInt32(drpdwnRatingRange.SelectedItem.Value), 
                        "","", 1, "Update", Convert.ToInt32(ViewState["GridViewRowID"]));
                }
                
                if (status ==1)
                {
                    string message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BindData();
                }
                else if (status == 2)
                {
                    var message = "Record updated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BindData();
                    BtnSubmit.Text = "Add";
                }
                else if (status == 5)
                {
                    var message = "Record activated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    BindData();
                    ClearAllSelection(this);
                    BtnSubmit.Text = "Add";
                }
                else if (status == 4)
                {
                    string message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    //BtnSubmit.Text = "Update";
                    ClearAllSelection(this);
                    BtnSubmit.Text = "Add";
                }
                else
                {
                    ErrorHandling.CheckEachSteps(Convert.ToString(status));
                    string message = "Issue in Record. (Status) : " + status;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Issue in record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void GvClass_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = GvClass.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvClass.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;

                    DrpdwnCenter.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnFromYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!(row.Cells[3].Text.Equals("&nbsp;") || row.Cells[3].Text.Equals("-1")))
                    {
                        drpdwnTillYear.Items.FindByText(row.Cells[3].Text).Selected = true;
                    }
                    drpdwnFromSeason.Items.FindByText(row.Cells[4].Text).Selected = true;
                    if (!(row.Cells[5].Text.Equals("&nbsp;") || row.Cells[5].Text.Equals("-1")))
                    {
                        drpdwnTillSeason.Items.FindByText(row.Cells[5].Text).Selected = true;
                    }
                    //if (!row.Cells[6].Text.Equals("&nbsp;"))
                    //{
                    //    drpdwnCategory.Items.FindByText(row.Cells[6].Text).Selected = true;
                    //}
                    if (!row.Cells[6].Text.Equals("&nbsp;"))
                    {
                        drpdwnRatingRange.Items.FindByText(row.Cells[6].Text).Selected = true;
                    }
                    //if (!row.Cells[8].Text.Equals("&nbsp;"))
                    //{
                    //    txtMinHandicapRating.Text = row.Cells[8].Text;
                    //}
                    //if (!row.Cells[9].Text.Equals("&nbsp;"))
                    //{
                    //    txtMaxHandicapRating.Text = row.Cells[9].Text;
                    //}
                    if (!row.Cells[7].Text.Equals("&nbsp;"))
                    {
                        drpdwnClassType.Items.FindByText(row.Cells[7].Text).Selected = true;
                    }
                    //if (!row.Cells[11].Text.Equals("&nbsp;"))
                    //{
                    //    drpdwnClassAlias.Items.FindByText(row.Cells[11].Text).Selected = true;
                    //}
                    //txtbxClassAlias.Text = row.Cells[11].Text;
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        public void ClearAllSelection(Control parent)
        {

            foreach (Control x in parent.Controls)
            {
                if ((x.GetType() == typeof(TextBox)))
                {

                    ((TextBox)(x)).Text = "";
                }
                if (!chkbxFix.Checked)
                {
                    if ((x.GetType() == typeof(DropDownList)))
                    {

                        ((DropDownList)(x)).ClearSelection();
                    }
                }
                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }


        private void BindData()
        {
            try
            {

                DataTable dt;
                dt = Bl.GetMasterData("Class");
                //tblGridview.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    GvClass.DataSource = dt;
                    GvClass.DataBind();
                }
                else
                {
                    GvClass.DataSource = new DataTable();
                    GvClass.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
            }
        }




        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var status = Bl.InserUpdatetMasterPagesDataClass(0,0,0,0, 0,"",0, 0, 0, "", "", 1, "Delete", (int)ViewState["GridViewRowID"]);
                    ClearAllSelection(this);
                    BindData();
                    BtnSubmit.Text = "Add";
                    var message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GridViewRowID"] = "";
                }
                else
                {
                    var message = "No record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
            }
        }
        
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = Bl.GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
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
                //  DataTable dt = Bl.GetMasterData("Year");
                using (DataTable dt = Bl.GetMasterData("Class"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("ClassID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Class");

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

                            //  }
                            Response.AddHeader("content-disposition", "attachment;filename=Class.xlsx");
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Class"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void Import_To_Grid(string FilePath, string Extension)
        {
            try
            {
                string strConn;
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                if (FilePath.Substring(FilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
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
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "MasterClass");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        //                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        BindData();
                        var message = "Issue in few record. Please check the XL sheet.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Master Class");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Class.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        BindData();
                        var message = "All Record has been added successfully.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                else
                {
                    var message = "No Record found.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
       
    }
}