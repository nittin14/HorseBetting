using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;
using System.Drawing;
using VKATalkClassLayer;
using VKATalk.Common;
using System.Collections.Specialized;
using System.Text;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

using OfficeOpenXml;

namespace VKATalk.Master
{
    public partial class AddStakeMoney : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindData();
                    BindDropDown(drpdwnCenter, "Center", "CenterName", "ID");
                    drpdwnCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    BindDropDown(drpdwnHandicapRatingRange, "HandicapRatingRange", "HandicapRatingRange", "HandicapRatingRangeID");
                    drpdwnHandicapRatingRange.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    BindDropDown(drpdwnAgeCondition, "AgeCondition", "AgeCondition", "AgeConditionID");
                    drpdwnAgeCondition.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                    drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    BindDropDown(drpdwnFromSeason, "Season", "SeasonName", "SeasonID");
                    drpdwnFromSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    BindDropDown(drpdwnStakeMoneyEarner, "ProfessionalProfile", "ProfileType", "ProfileTypeID");
                    drpdwnStakeMoneyEarner.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record ";
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


        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddMasterRaceNameList(string prefixText, int count)
        {
            DataTable dt = new MasterBL().GetprospectusAutoFiller("ProspectusName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }

        protected void txtbxAmount_OnTextChange(object sender, EventArgs e)
        {
            var stakemoneytotalamount = txtbxStakeMoney.Text;
            var amount = txtbxAmount.Text;
            Double percentageDifference;
            //var percentage = (stakemoneytotalamount / amount) * 100;
            double tempNumber = Convert.ToDouble((Convert.ToDouble(amount) * 100) / Convert.ToDouble(stakemoneytotalamount));
            txtbxPercentage.Text = tempNumber.ToString();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            VKATalkClassLayer.StakeMoney clsType = new VKATalkClassLayer.StakeMoney();

            try
            {

                clsType.Center = drpdwnCenter.SelectedItem.Value;
                clsType.RaceStatus = string.Empty;
                clsType.MasterRaceNameID = string.Empty;
                clsType.RaceType = rdbtnRaceType.SelectedItem.Text;
                clsType.HandicapRatingRange= drpdwnHandicapRatingRange.SelectedItem.Value;
                clsType.AgeCondition = drpdwnAgeCondition.SelectedItem.Value;
                clsType.StakeMoneyTableNo=drpdwnTableNo.SelectedItem.Text;
                clsType.StakeMoneyVar = txtbxStakeMoney.Text;
                clsType.MomenttoCost = txtbxMomenttoCost.Text;
                clsType.FromYearId = drpdwnFromYear.SelectedItem.Value;
                clsType.TillDate = txtbxTillDate.Text;
                clsType.TillYearID= string.Empty;
                clsType.FromSeason = drpdwnFromSeason.SelectedItem.Value;
                clsType.TillSeason = string.Empty;
                clsType.Placing = drpdwnPlacing.SelectedItem.Text;
                clsType.SMEProfileTypeID = drpdwnStakeMoneyEarner.SelectedItem.Value;
                clsType.Percentage = txtbxPercentage.Text;
                clsType.Amount = txtbxAmount.Text;

                DataTable dt;
                if (btnSave.Text.Equals("Add"))
                {
					if(hdnfieldStakeMoneyID.Value.ToString().Equals(""))
					{
						dt = Bl.InsertStakeMoney(clsType, 1, "Insert", 0, 0);
					}
					else
					{

						dt = Bl.InsertStakeMoney(clsType, 1, "Insert", Convert.ToInt32(hdnfieldStakeMoneyID.Value), Convert.ToInt32(ViewState["StakeMoneyEarnerID"]));
					}
                    
                }
                else
                {
                    dt = Bl.InsertStakeMoney(clsType, 1, "Update", (int)ViewState["StakeMoneyID"], Convert.ToInt32(ViewState["StakeMoneyEarnerID"]));
                }

				if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        if (chkbxstatus.Checked.Equals(true))
                        {
                            hdnfieldStakeMoneyID.Value = dt.Rows[0][1].ToString();
                        }
                        else
                        {
                            hdnfieldStakeMoneyID.Value = string.Empty;
                        }
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                    }
                    else if (dt.Rows[0][0].ToString() == "2")
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                    }
                    else if (dt.Rows[0][0].ToString() == "5")
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                    }
                    else if (dt.Rows[0][0].ToString() == "4")
                    {
                        string message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                    }
                    else
                    {
                        ErrorHandling.CheckEachSteps(Convert.ToString(dt.Rows[0][0]));
                        string message = "Issue in Record. (Status) : " + dt.Rows[0][0].ToString();
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


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
        }


        private void BindData()
        {
            DataTable dt;
            dt = Bl.GetMasterData("MasterStakeMoney");
            if (dt.Rows.Count > 0)
            {
                GvStakeMoney.DataSource = dt;
                GvStakeMoney.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                GvStakeMoney.DataSource = dt1;
                GvStakeMoney.DataBind();
            }
        }

        public void ClearAllSelection(Control parent)
        {

            if (chkbxstatus.Checked.Equals(true))
            {
                drpdwnPlacing.ClearSelection();
               // drpdwnStakeMoneyEarner.ClearSelection();
                txtbxPercentage.Text = string.Empty;
                txtbxAmount.Text = string.Empty;
            }
            else
            {
                hdnfieldStakeMoneyID.Value = string.Empty;
                chkbxstatus.Checked = false;
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

                        ((RadioButtonList)(x)).SelectedIndex=0;
                    }
                    
                    if (x.HasControls())
                    {
                        ClearAllSelection(x);
                    }
                }


            }
        }

        protected void GvStakeMoney_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                var row = GvStakeMoney.SelectedRow;
                var hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvStakeMoney.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["StakeMoneyID"] = dataKey.Value;
                    var hdnfieldstakemoneyearnerid = (HiddenField)row.FindControl("hdnfieldstakemoneyearnerid");
                    ViewState["StakeMoneyEarnerID"] = hdnfieldstakemoneyearnerid.Value;
                    drpdwnCenter.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnFromSeason.Items.FindByText(row.Cells[1].Text).Selected = true;
                    drpdwnFromYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!row.Cells[3].Text.Contains("&nbsp;"))
                    {
                        txtbxTillDate.Text = row.Cells[3].Text;
                    }
                    rdbtnRaceType.Items.FindByText(row.Cells[4].Text).Selected = true;
                    if (!row.Cells[5].Text.Contains("&nbsp;"))
                    {
						var stringvalue = row.Cells[5].Text;
						drpdwnHandicapRatingRange.Items.FindByText(stringvalue.Replace("&amp;", "&")).Selected = true;
						//drpdwnHandicapRatingRange.Items.FindByText(row.Cells[5].Text).Selected = true;
                    }
                    if (!row.Cells[6].Text.Contains("&nbsp;"))
                    {
						var stringvalue = row.Cells[6].Text;
						drpdwnAgeCondition.Items.FindByText(stringvalue.Replace("&amp;", "&")).Selected = true;

						//drpdwnAgeCondition.Items.FindByText(row.Cells[6].Text).Selected = true;
                    }
                    if (!row.Cells[7].Text.Contains("&nbsp;"))
                    {
                        drpdwnTableNo.Items.FindByText(row.Cells[7].Text).Selected = true;
                    }
                    if (!row.Cells[8].Text.Contains("&nbsp;"))
                    {
                        txtbxStakeMoney.Text = row.Cells[8].Text;
                    }
                    if (!row.Cells[9].Text.Contains("&nbsp;"))
                    {
                        txtbxMomenttoCost.Text = row.Cells[9].Text;
                    }
                    if (!row.Cells[10].Text.Contains("&nbsp;"))
                    {
                        drpdwnStakeMoneyEarner.Items.FindByText(row.Cells[10].Text).Selected = true;
                    }
                    if (!row.Cells[11].Text.Contains("&nbsp;"))
                    {
                        drpdwnPlacing.Items.FindByText(row.Cells[11].Text).Selected = true;
                    }

                    if (!row.Cells[12].Text.Contains("&nbsp;"))
                    {
                        txtbxPercentage.Text = row.Cells[12].Text;
                    }
                    if (!row.Cells[13].Text.Contains("&nbsp;"))
                    {
                        txtbxAmount.Text = row.Cells[13].Text;
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

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Master_StakeMoney"))
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
                    //var dtErrorResult = new MasterHorseBL().UploadExcelRecordBulkMinimumColumns(dt, "HorseBodyWeight");
                    var dtErrorResult = new MasterBL().ImportExcel30(dt, "Master_StakeMoney");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("StakeMoney");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Master_StakeMoney.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        string message = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                    }
                }
                else
                {
                    string message = "No Record found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void btnShow_Click(object sender, EventArgs e)
        {
            try{
                VKATalkClassLayer.StakeMoney clsType = new VKATalkClassLayer.StakeMoney();
                clsType.Center = drpdwnCenter.SelectedItem.Value;
                clsType.RaceStatus = string.Empty;
                clsType.MasterRaceNameID = string.Empty;
                clsType.RaceType = rdbtnRaceType.SelectedItem.Text;
                clsType.HandicapRatingRange = drpdwnHandicapRatingRange.SelectedItem.Value;
                clsType.AgeCondition = drpdwnAgeCondition.SelectedItem.Value;
                clsType.StakeMoneyTableNo = drpdwnTableNo.SelectedItem.Text;
                clsType.StakeMoneyVar = txtbxStakeMoney.Text;
                clsType.MomenttoCost = txtbxMomenttoCost.Text;
                clsType.FromYearId = drpdwnFromYear.SelectedItem.Value;
                clsType.TillDate = txtbxTillDate.Text;
                clsType.TillYearID = string.Empty;
                clsType.FromSeason = drpdwnFromSeason.SelectedItem.Value;
                clsType.TillSeason = string.Empty;
                clsType.Placing = drpdwnPlacing.SelectedItem.Text;
                clsType.SMEProfileTypeID = drpdwnStakeMoneyEarner.SelectedItem.Value;
                clsType.Percentage = txtbxPercentage.Text;
                clsType.Amount = txtbxAmount.Text;

                DataTable dt = Bl.GetStakeMoney(clsType);
                if (dt.Rows.Count > 0)
                {
                    GvStakeMoney.DataSource = dt;
                    GvStakeMoney.DataBind();
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    GvStakeMoney.DataSource = dt1;
                    GvStakeMoney.DataBind();
                }
             }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            VKATalkClassLayer.StakeMoney clsType = new VKATalkClassLayer.StakeMoney();

            try
            {

                clsType.Center = drpdwnCenter.SelectedItem.Value;
                clsType.RaceStatus = string.Empty;
                clsType.MasterRaceNameID = string.Empty;
                clsType.RaceType = rdbtnRaceType.SelectedItem.Text;
                clsType.HandicapRatingRange = drpdwnHandicapRatingRange.SelectedItem.Value;
                clsType.AgeCondition = drpdwnAgeCondition.SelectedItem.Value;
                clsType.StakeMoneyTableNo = drpdwnTableNo.SelectedItem.Text;
                clsType.StakeMoneyVar = txtbxStakeMoney.Text;
                clsType.MomenttoCost = txtbxMomenttoCost.Text;
                clsType.FromYearId = drpdwnFromYear.SelectedItem.Value;
                clsType.TillDate = txtbxTillDate.Text;
                clsType.TillYearID = string.Empty;
                clsType.FromSeason = drpdwnFromSeason.SelectedItem.Value;
                clsType.TillSeason = string.Empty;
                clsType.Placing = drpdwnPlacing.SelectedItem.Text;
                clsType.SMEProfileTypeID = drpdwnStakeMoneyEarner.SelectedItem.Value;
                clsType.Percentage = txtbxPercentage.Text;
                clsType.Amount = txtbxAmount.Text;

                DataTable dt;
                dt = Bl.InsertStakeMoney(clsType, 1, "Delete", (int)ViewState["StakeMoneyID"], Convert.ToInt32(ViewState["StakeMoneyEarnerID"]));


                if (dt.Rows.Count > 0)
                {
                    var message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    BindData();
                    ClearAllSelection(this);
                    btnSave.Text = "Add";
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
                using (DataTable dt = Bl.GetMasterData("MasterStakeMoney"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("StakeMoneyID");
                        dt.Columns.Remove("StakeMoneyEarnerID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("StakeMoney");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Master_StakeMoney.xlsx");
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


        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
    }
}