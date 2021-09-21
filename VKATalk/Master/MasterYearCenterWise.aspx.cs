using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace VKATalk.Master
{
    public partial class MasterYearCenterWise : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDropDown(this.drpdwnCenter, "Center", "CenterName", "ID");
                    drpdwnCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    BindDropDown(this.drpdwnYear, "Year", "YearName", "YearID");
                    drpdwnYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    BindData();
                }
            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int status;
                int checkcondition = 0;
                    if (BtnSubmit.Text.Equals("Add"))
                    {
                        status = Bl.MasterCenterYearWise(0, Convert.ToInt32(drpdwnCenter.SelectedItem.Value), Convert.ToInt32(drpdwnYear.SelectedItem.Value), txtbxStartDate.Text, txtbxEndDate.Text, 1, "Insert");
                    }
                    else
                    {
                        status = Bl.MasterCenterYearWise((int)ViewState["GridViewRowID"], Convert.ToInt32(drpdwnCenter.SelectedItem.Value), Convert.ToInt32(drpdwnYear.SelectedItem.Value), txtbxStartDate.Text, txtbxEndDate.Text, 1, "Update");
                    }

                    if (status == 1 || status == 2)
                    {
                        var message = status == 1 ? "Record added successfully." : "Record update successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
         
        }

        private void BindData()
        {
            DataTable dt;
            dt = Bl.GetMasterData("MasterYearCenterWise");
            if (dt.Rows.Count > 0)
            {
                GvGlobal.DataSource = dt;
                GvGlobal.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                GvGlobal.DataSource = dt1;
                GvGlobal.DataBind();
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

        protected void GvGlobal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = GvGlobal.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvGlobal.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    drpdwnCenter.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!row.Cells[3].Text.Equals("&nbsp;"))
                    {
                        txtbxStartDate.Text = row.Cells[3].Text;
                    }
                    if (!row.Cells[4].Text.Equals("&nbsp;"))
                    {
                        txtbxEndDate.Text = row.Cells[4].Text;
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
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
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var status = Bl.MasterCenterYearWise((int)ViewState["GridViewRowID"], 0, 0, "__-__-____", "__-__-____", 1, "Delete");
                    ClearAllSelection(this);
                    BindData();
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
            catch (Exception exception)
            {
                string message = "Issue in Record.";
                ErrorHandling.SendErrorToText(exception);
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
                using (DataTable dt = Bl.GetMasterData("MasterYearCenterWise"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("YearCWID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("YearCenterWise");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Master_YearCenterWise.xlsx");
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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Master_YearCenterWise"))
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


                            dtresult = new MasterBL().ImportMasterFiles(dt.Rows[count][1].ToString(), dt.Rows[count][2].ToString(), dt.Rows[count][3].ToString(), dt.Rows[count][4].ToString(),
                                 dt.Rows[count][5].ToString(), dt.Rows[count][6].ToString(), dt.Rows[count][7].ToString(), dt.Rows[count][8].ToString(), dt.Rows[count][9].ToString(),
                                 dt.Rows[count][10].ToString(), dt.Rows[count][11].ToString(), dt.Rows[count][12].ToString(), dt.Rows[count][13].ToString(), dt.Rows[count][14].ToString(),
                                 dt.Rows[count][15].ToString(), dt.Rows[count][16].ToString(), dt.Rows[count][17].ToString(), dt.Rows[count][18].ToString(), dt.Rows[count][19].ToString(),
                                 dt.Rows[count][20].ToString(), dt.Rows[count][21].ToString(), dt.Rows[count][22].ToString(), dt.Rows[count][23].ToString(), dt.Rows[count][24].ToString(),
                                 dt.Rows[count][25].ToString(), dt.Rows[count][26].ToString(), dt.Rows[count][27].ToString(), dt.Rows[count][28].ToString(), dt.Rows[count][29].ToString(),
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "MasterYearCenterWise");


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
                //if (dt.Rows.Count > 0)
                //{
                //    var dtErrorResult = new MasterBL().ImportExcel30(dt, "MasterYearCenterWise");
                //    if (dtErrorResult.Rows.Count > 0)
                //    {
                //        //                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                //        BindData();
                //        var message = "Issue in few record. Please check the XL sheet.";
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //        using (ExcelPackage xp = new ExcelPackage())
                //        {
                //            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Master_YearCenterWise");
                //            int rowstart = 1;
                //            int colstart = 1;
                //            int rowend = rowstart;
                //            int colend = colstart + (dtErrorResult.Columns.Count - 1);
                //            //  int colend = colstart;
                //            rowend = rowstart + dtErrorResult.Rows.Count;
                //            ws.Cells[rowstart, colstart].LoadFromDataTable(dtErrorResult, true);
                //            int i = 1;
                //            foreach (DataColumn dc in dtErrorResult.Columns)
                //            {
                //                i++;
                //                if (dc.DataType == typeof(decimal)) ws.Column(i).Style.Numberformat.Format = "#0.00";
                //            }
                //            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                //            ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                //                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                //                    ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                //                        ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style =
                //                            OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                //            Response.AddHeader("content-disposition", "attachment;filename=Master_YearCenterWise.xlsx");
                //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //            Response.BinaryWrite(xp.GetAsByteArray());
                //            Response.End();
                //        }
                //    }
                //    else
                //    {
                //        BindData();
                //        var message = "All Record has been added successfully.";
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //    }
                //}
                else
                {
                    var message = "No Record found.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }
        }

    }
}