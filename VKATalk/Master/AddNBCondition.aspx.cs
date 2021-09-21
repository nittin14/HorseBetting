using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using OfficeOpenXml;
using System.Data.OleDb;
using System.Text.RegularExpressions;


namespace VKATalk.Master
{
    public partial class AddNBCondition : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        DataTable dtdropdownBind;
        DataTable dtYear;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDropDown(drpdwnCenterName, "Center", "CenterName", "ID");
                    drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                    drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    BindDropDown(drpdwnTillYear, "Year", "YearName", "YearID");
                    drpdwnTillYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                   
                    BindData();


                }
            }
            catch (Exception ex)
            {
                var message = "Issue in record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
				if (txtTopWeight.Text.Equals("") && txtbxlowerweight.Text.Equals(""))
				{
					var message = "Please enter Top Weight or Bottom Weight.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}
				else
				{
					int status = 0;
					int checkcondition = 0;
					if (BtnSubmit.Text.Equals("Add"))
					{
						status = Bl.InsertMasterPagesDataCondition("NBCondition", drpdwnCenterName.SelectedItem.Value, drpdwnFromYear.SelectedItem.Value, drpdwnTillYear.SelectedItem.Value, rdbtnRaceType.SelectedItem.Text, txtTopWeight.Text, txtbxlowerweight.Text, 1);
						checkcondition = 1;
					}
					else
					{
						status = Bl.UpdateMasterPagesDataCondition("NBCondition", (int)ViewState["GridViewRowID"], drpdwnCenterName.SelectedItem.Value, drpdwnFromYear.SelectedItem.Value, drpdwnTillYear.SelectedItem.Value, rdbtnRaceType.SelectedItem.Text, txtTopWeight.Text, txtbxlowerweight.Text, 1, "Update");
						checkcondition = 2;
					}
					if (status == 1 || status == 2)
					{
						var message = checkcondition == 1 ? "Record added successfully." : "Record update successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						BindData();
						ClearAllSelection(this);
						if (checkcondition == 2)
						{
							BtnSubmit.Text = "Add";
						}
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
						//BtnSubmit.Text = "Update";
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
                var message = "Issue in record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }

        }

        
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                var message = "Issue in record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }

        }

        private void BindData()
        {

            DataTable dt;
            dt = Bl.GetMasterData("NBCondition");
            dvgridview.Visible = true;
            if (dt.Rows.Count > 0)
            {

                gridvwNBCondition.DataSource = dt;
                gridvwNBCondition.DataBind();

            }
            else
            {
                DataTable dt1 = new DataTable();
                gridvwNBCondition.DataSource = dt1;
                gridvwNBCondition.DataBind();
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

                if ((x.GetType() == typeof(RadioButtonList)))
                {

                    ((RadioButtonList)(x)).SelectedIndex = 0;
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }

        protected void Gridview_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = gridvwNBCondition.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = gridvwNBCondition.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    drpdwnCenterName.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnFromYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!(row.Cells[3].Text.Equals("&nbsp;") || row.Cells[3].Text.Equals("-1")))
                    {
                        drpdwnTillYear.Items.FindByText(row.Cells[3].Text).Selected = true;
                    }
                    rdbtnRaceType.Items.FindByText(row.Cells[4].Text).Selected = true;
                    if (!(row.Cells[5].Text.Equals("&nbsp;")))
                    {
                        txtTopWeight.Text = row.Cells[5].Text;
                    }
                    if (!(row.Cells[6].Text.Equals("&nbsp;")))
                    {
                        txtbxlowerweight.Text = row.Cells[6].Text;
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var status = Bl.UpdateMasterPagesDataCondition("NBCondition", (int)ViewState["GridViewRowID"], "", "", "", "", "", "", 1, "Delete");
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
            catch (Exception exception)
            {
                string message = "Issue in Record.";
                ErrorHandling.SendErrorToText(exception);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        /// <summary>
        /// Export in Excel file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = Bl.GetMasterData("NBCondition"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("NBConditionID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("NBCondition");

                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            // int colend = colstart + dt.Columns.Count;
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
                            Response.AddHeader("content-disposition", "attachment;filename=NBCondition.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message+ "');", true);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("NBCondition"))
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
                bool hasHeaders = false;
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
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count > 0)
                {
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "NBCondition");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        //                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        BindData();
                        var message = "Issue in few record. Please check the XL sheet.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("NBCondition");
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
                            Response.AddHeader("content-disposition", "attachment;filename=NBCondition.xlsx");
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
            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }

        }

       
    }
}