using System;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;

namespace VKATalk.Master
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.UI;

    using OfficeOpenXml;

    public partial class AddClassGroup : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
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
                BindDropDown(drpdwnAgeCondition, "AgeCondition", "AgeCondition", "AgeConditionID");
                drpdwnAgeCondition.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnClass, "ClassType", "ClassType", "ClassTypeID");
                drpdwnClass.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnClassGroup, "ClassGroupType", "ClassGroupType", "ClassGroupTypeID");
                drpdwnClassGroup.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindData();
            }
        }

        /// <summary>
        /// Bind Dropdown at the time of page load
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TableName_"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = Bl.GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            BtnSubmit.Text = "Add";
        }

        /// <summary>
        /// Submit Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int status;
                var million = string.Empty;
                    if(chkbxMillion.Checked)
                        million="Yes";

                var sweepstake = string.Empty;
                    if(chkbxSweepStake.Checked)
                        sweepstake="Yes";

                var classic = string.Empty;
                    if(chkbxClassic.Checked)
                        classic="Yes";

                    //var grade = string.Empty;
                    //if(chkbxGrade.Checked)
                    //    grade="Yes";

                if (BtnSubmit.Text.Equals("Add"))
                {
                    status = Bl.InsertClassGroup(Convert.ToInt32(DrpdwnCenter.SelectedItem.Value), Convert.ToInt32(drpdwnFromYear.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillYear.SelectedItem.Value), Convert.ToInt32(drpdwnFromSeason.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillSeason.SelectedItem.Value),
                        Convert.ToInt32(drpdwnClassGroup.SelectedItem.Value),
                        rdbtnRaceType.Text.Equals("") ? "" : rdbtnRaceType.SelectedItem.Text,
                        rdbtnRaceStatus.Text.Equals("") ? "" : rdbtnRaceStatus.SelectedItem.Text,
                        million,
                        sweepstake,
                        drpdwnGrade.SelectedItem.Text.Equals("-- Please Select --") ? "" : drpdwnGrade.SelectedItem.Text, 
                        "", 
                        Convert.ToInt32(drpdwnClass.SelectedItem.Value), 1,
                        classic, 
                        "Insert", 0,
                        0, Convert.ToInt32(drpdwnAgeCondition.SelectedItem.Value));
                }
                else
                {
                    status = Bl.InsertClassGroup(Convert.ToInt32(DrpdwnCenter.SelectedItem.Value), Convert.ToInt32(drpdwnFromYear.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillYear.SelectedItem.Value), Convert.ToInt32(drpdwnFromSeason.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnTillSeason.SelectedItem.Value), 
                        Convert.ToInt32(drpdwnClassGroup.SelectedItem.Value),
                        rdbtnRaceType.Text.Equals("") ? "" : rdbtnRaceType.SelectedItem.Text,
                        rdbtnRaceStatus.Text.Equals("") ? "" : rdbtnRaceStatus.SelectedItem.Text,
                         million,
                        sweepstake,
                        drpdwnGrade.SelectedItem.Text.Equals("-- Please Select --") ? "" : drpdwnGrade.SelectedItem.Text, 
                        "",
                        Convert.ToInt32(drpdwnClass.SelectedItem.Value), 1,
                        classic, 
                        "Update", Convert.ToInt32(ViewState["GridViewRowID"]),
                        0, Convert.ToInt32(drpdwnAgeCondition.SelectedItem.Value));
                    
                }
                if (status == 1)
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
                else if (status == 4)
                {
                    var message = "Record Already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BtnSubmit.Text = "Add";
                }
                else if (status == 5)
                {
                    var message = "Record activated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                    BindData();
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
                var message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        /// <summary>
        /// Show Gridview value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// Bind Gridview
        /// </summary>
        protected void BindData()
        {
            try
            {
                DataTable dt;
                dt = Bl.GetMasterData("ClassGroup");
                dvgridview.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    grdvwClassGroup.DataSource = dt;
                    grdvwClassGroup.DataBind();
                }
                else
                {
                    grdvwClassGroup.DataSource = new DataTable();
                    grdvwClassGroup.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Text = "Update";
                GridViewRow row = grdvwClassGroup.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = grdvwClassGroup.DataKeys[row.RowIndex];
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
                    if (!(row.Cells[6].Text.Equals("&nbsp;") || row.Cells[6].Text.Equals("")))
                    {
                        rdbtnRaceType.Items.FindByText(row.Cells[6].Text).Selected = true;
                    }
                    //if (!row.Cells[7].Text.Equals("&nbsp;"))
                    //{
                    //    drpdwnCategory.Items.FindByText(row.Cells[7].Text).Selected = true;
                    //}
                    if (!row.Cells[7].Text.Equals("&nbsp;"))
                    {
                        drpdwnClass.Items.FindByText(row.Cells[7].Text).Selected = true;
                    }
                    if (!row.Cells[8].Text.Equals("&nbsp;"))
                    {
                        //drpdwnAgeCondition.ClearSelection();
                        var stringvalue = row.Cells[8].Text;
                        drpdwnAgeCondition.Items.FindByText(stringvalue.Replace("&amp;", "&")).Selected = true;

                        //drpdwnAgeCondition.Items.FindByText(row.Cells[8].Text).Selected = true;
                    }
                    if (!(row.Cells[9].Text.Equals("&nbsp;") || row.Cells[9].Text.Equals("")))
                    {
                        rdbtnRaceStatus.Items.FindByText(row.Cells[9].Text).Selected = true;
                    }
                    if (!(row.Cells[10].Text.Equals("&nbsp;") || row.Cells[10].Text.Equals("")))
                    {
                        if(row.Cells[10].Text.Equals("Million"))
                          chkbxMillion.Checked=true;
                    }
                    if (!(row.Cells[11].Text.Equals("&nbsp;") || row.Cells[11].Text.Equals("")))
                    {
                        if (row.Cells[11].Text.Equals("Sweep Stake"))
                          chkbxSweepStake.Checked=true;
                    }
                    if (!(row.Cells[12].Text.Equals("&nbsp;") || row.Cells[12].Text.Equals("")))
                    {
                        if (row.Cells[12].Text.Equals("Classic"))
                          chkbxClassic.Checked=true;
                    }
                    if (!(row.Cells[13].Text.Equals("&nbsp;") || row.Cells[13].Text.Equals("")))
                    {
                        drpdwnGrade.Items.FindByText(row.Cells[13].Text).Selected = true;
                        //if (row.Cells[13].Text.Equals("Grade"))
                        //    chkbxGrade.Checked = true;
                        //rdbtnGraded.Items.FindByText(row.Cells[13].Text).Selected = true;
                    }
                    if (!row.Cells[14].Text.Equals("&nbsp;"))
                    {
                        //Terms 2 &amp; 3 years Big
                        var stringvalue = row.Cells[14].Text;
                        drpdwnClassGroup.Items.FindByText(stringvalue.Replace("&amp;","&")).Selected = true;
                    }
                    //if (!row.Cells[16].Text.Equals("&nbsp;"))
                    //{
                    //    drpdwnClassGroupAlias.Items.FindByText(row.Cells[16].Text).Selected = true;
                    //}
                }

            }
            catch (Exception ex)
            {
                BtnSubmit.Text = "Add";
                ErrorHandling.SendErrorToText(ex);
                var message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        public void ClearAllSelection(Control parent)
        {

            rdbtnRaceType.SelectedValue = "-1";
            rdbtnRaceStatus.SelectedValue = "-1";
            chkbxClassic.Checked = false;
            //chkbxGrade.Checked = false;
            chkbxMillion.Checked = false;
            chkbxSweepStake.Checked = false;
           
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    //var status = Bl.InsertClassGroup(Convert.ToInt32(ViewState["GridViewRowID"]), "Delete", "", "", "", "", "", "", "", "", "", "", "", "", "", 1, "", 0);
                    var status = Bl.InsertClassGroup(0, 0, 0, 0, 0,0, "", "", "", "", "", "", 0, 1, "", "Delete", Convert.ToInt32(ViewState["GridViewRowID"]),0,0);
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
                var message = "Issue found in record.";
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
                //  DataTable dt = Bl.GetMasterData("Year");
                using (DataTable dt = Bl.GetMasterData("ClassGroup"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("ClassGroupID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("ClassGroup");

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
                            Response.AddHeader("content-disposition", "attachment;filename=ClassGroup.xlsx");
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("ClassGroup"))
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
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "MasterClassGroup");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        //                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        BindData();
                        var message = "Issue in few record. Please check the XL sheet.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Master Class Group");
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
                            Response.AddHeader("content-disposition", "attachment;filename=ClassGroup.xlsx");
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