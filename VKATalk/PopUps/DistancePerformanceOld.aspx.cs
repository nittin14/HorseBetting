using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.UI;

    using OfficeOpenXml;

    public partial class DistancePerformanceOld : System.Web.UI.Page
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
                    this._horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                    horseId.Value = Request.QueryString["HorseNameID"];
                    _horseId = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                }
                if (!IsPostBack)
                {
					//DateTime dateForButton = DateTime.Now.AddDays(-1);
					//txtbxFromDate.Text = Convert.ToString(dateForButton);
                    var reqQueryValue = string.Empty;
                    if (!Request.QueryString["HorseName"].Equals(""))
                    {
                        reqQueryValue = Request.QueryString["HorseName"];
                    }
                    string[] horseName = reqQueryValue.Split(',');
                    lblHorseNameFirst.Text = horseName[1];
                    lblHorseNameSecond.Text = horseName[2];
                    BindData();
                    DataTable dt = new MasterBL().GetDropdownBind("Distance");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ListItem item = new ListItem();
                            item.Text = dr["Distance"].ToString();
                            item.Value = dr["DistanceID"].ToString();
                            chkbxDistance.Items.Add(item);
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

        /// <summary>
        /// This is use for bind the gridview
        /// </summary>
        public void BindData()
        {
                MasterHorseBL Bl = new MasterHorseBL();
                DataTable dt = Bl.GetHorseDetail(Convert.ToString(_horseId), "DistanceOldPerformance");
                if (dt.Rows.Count > 0)
                {
                    GvDistanceOld.DataSource = dt;
                    GvDistanceOld.DataBind();
                }
                else
                {
                    GvDistanceOld.DataSource = new DataTable();
                    GvDistanceOld.DataBind();
                }
           
        }

        protected void btnAddDistance_Click(object sender, EventArgs e)
        {
            try
            {
                dvDistance.Visible = true;
                int rowcount = 0;
                int totolrows = 0;
                Dictionary<int, string> dic = new Dictionary<int, string>();
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("DistanceID", typeof(int)));
                dt.Columns.Add(new DataColumn("Distance", typeof(string)));
                DataRow dr;
                foreach (ListItem lst in this.chkbxDistance.Items)
                {
                    if (lst.Selected)
                    {
                        dr = dt.NewRow();
                        dr["DistanceID"] = Convert.ToInt32(lst.Value);
                        dr["Distance"] = lst.Text;
                        rowcount += 1;
                        totolrows += 1;
                        dt.Rows.Add(dr);
                    }
                }
                if (totolrows > 0)
                {
                    ViewState["Distance"] = dt;
                    gvDistance.DataSource = dt;
                    gvDistance.DataBind();
                }
                else
                {
                    var message = "Please select Distance.";
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
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
                if ((x.GetType() == typeof(CheckBoxList)))
                {

                    ((CheckBoxList)(x)).ClearSelection();
                }
                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
            txtbxFromDate.Text = "14-04-2019";
        }


      

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                var strresult = string.Empty;
                if (btnSave.Text == "Add")
                {
					if (Regex.IsMatch(
						this.txtbxFromDate.Text,
						@"(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"))
					{
						// Do something here

						DataTable dtDistance = (DataTable)ViewState["Distance"];
						DataTable dtdistanceOldPerformance = new DataTable();
						dtdistanceOldPerformance.Columns.Add(new DataColumn("HorseID_FK", typeof(int)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("DistanceID", typeof(int)));

						dtdistanceOldPerformance.Columns.Add(
							new DataColumn("PerformanceAddedTillDate", typeof(DateTime)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("I", typeof(int)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("II", typeof(int)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("III", typeof(int)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("IV", typeof(int)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("TotalRuns", typeof(int)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("IsShow", typeof(bool)));
						dtdistanceOldPerformance.Columns.Add(new DataColumn("Distance", typeof(int)));
						DataRow dr1;

						int rowIndex = 0;
						if (!(dtDistance == null)) { 
						for (int i = 0; i < dtDistance.Rows.Count; i++)
						{
							Label lblDistance = (Label)gvDistance.Rows[rowIndex].Cells[1].FindControl("lblDistance");
							TextBox txtbxper1 =
								(TextBox)gvDistance.Rows[rowIndex].Cells[2].FindControl("txtbxPerformance1");
							TextBox txtbxper2 =
								(TextBox)gvDistance.Rows[rowIndex].Cells[2].FindControl("txtbxPerformance2");
							TextBox txtbxper3 =
								(TextBox)gvDistance.Rows[rowIndex].Cells[2].FindControl("txtbxPerformance3");
							TextBox txtbxper4 =
								(TextBox)gvDistance.Rows[rowIndex].Cells[2].FindControl("txtbxPerformance4");
							TextBox txtbxper5 =
								(TextBox)gvDistance.Rows[rowIndex].Cells[2].FindControl("txtbxPerformance5");
							if(txtbxper1.Text.Equals("") || txtbxper2.Text.Equals("") || txtbxper3.Text.Equals("") || txtbxper4.Text.Equals("") || txtbxper5.Text.Equals(""))
								{
									ClientScript.RegisterStartupScript(
						   this.GetType(),
						   "Popup",
						   "ShowPopup('Please Enter Performance.');",
						   true);
									return;
								}
							var gvDistanceDataKey = this.gvDistance.DataKeys[rowIndex];
							var distanceId = 0;
							if (gvDistanceDataKey != null)
							{
								distanceId = Convert.ToInt32(gvDistanceDataKey.Values[0]);
							}
							dr1 = dtdistanceOldPerformance.NewRow();
							dr1["HorseID_FK"] = this._horseId;
							dr1["DistanceID"] = distanceId;
							dr1["PerformanceAddedTillDate"] = txtbxFromDate.Text;
							dr1["I"] = Convert.ToInt32(txtbxper1.Text);
							dr1["II"] = Convert.ToInt32(txtbxper2.Text);
							dr1["III"] = Convert.ToInt32(txtbxper3.Text);
							dr1["IV"] = Convert.ToInt32(txtbxper4.Text);
							dr1["TotalRuns"] = Convert.ToInt32(txtbxper5.Text);
							dr1["IsShow"] = true;
							dr1["Distance"] = lblDistance.Text;
							dtdistanceOldPerformance.Rows.Add(dr1);
							rowIndex += 1;
						}
					}
						else
						{
							ClientScript.RegisterStartupScript(
						   this.GetType(),
						   "Popup",
						   "ShowPopup('Please add Distance.');",
						   true);
							return;
						}
                        if (dtdistanceOldPerformance.Rows[0][3].Equals("") && dtdistanceOldPerformance.Rows[0][4].Equals("") && 
                            dtdistanceOldPerformance.Rows[0][5].Equals("") && dtdistanceOldPerformance.Rows[0][6].Equals("") &&
                            dtdistanceOldPerformance.Rows[0][7].Equals(""))
                        {
                            ClientScript.RegisterStartupScript(
                           this.GetType(),
                           "Popup",
                           "ShowPopup('Please add Distance.');",
                           true);
                          
                        }
                        else
                        {
                            strresult = new MasterHorseBL().DistanceOldPerformance(dtdistanceOldPerformance,1,"Insert");
                            string message = "Record added successfully.";
                            ClientScript.RegisterStartupScript(
                                this.GetType(),
                                "Popup",
                                "ShowPopup('" + message + "');",
                                true);
                            ClearAllSelection(this);
                            BindData();
                            dvDistance.Visible = false;
							DateTime dateForButton = DateTime.Now.AddDays(-1);
							txtbxFromDate.Text = Convert.ToString(dateForButton);
						}
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(
                            this.GetType(),
                            "Popup",
                            "ShowPopup('Invalid date.');",
                            true);
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    status = new MasterHorseBL().DistanceOldGroupEdit(Convert.ToInt32(ViewState["horseRecordID"]), Convert.ToInt32(this.drpdwnDistance.SelectedItem.Value), this.txtbxFromDate.Text, Convert.ToInt32(this.txtbx1.Text), Convert.ToInt32(this.txtbx2.Text), Convert.ToInt32(this.txtbx3.Text), Convert.ToInt32(this.txtbx4.Text), Convert.ToInt32(this.txtbx5.Text), this._userId, "Update", (chkboxShow.Checked==true) ? 1 : 0);
                    ClearAllSelection(this);
                    dvDistance.Visible = false;
                    BindData();
                    
                    if (status == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        dvDistance.Visible = false;
                        tblDistanceUpdate.Visible = false;
						DateTime dateForButton = DateTime.Now.AddDays(-1);
						txtbxFromDate.Text = Convert.ToString(dateForButton);
					}
                    else if (status == 5)
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        dvDistance.Visible = false;
                        tblDistanceUpdate.Visible = false;
                    }
                    else if (status == 4)
                    {
                        string message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        btnSave.Text = "Add";
                        dvDistance.Visible = false;
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
                    int status = 0;
                    status = new MasterHorseBL().DistanceOldGroupEdit(Convert.ToInt32(ViewState["horseRecordID"]), 0, "", 0, 0, 0, 0, 0, 1, "Delete",0);
                    ClearAllSelection(this);
                    dvDistance.Visible = false;
                    BindData();
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["horseRecordID"] = "";
					//DateTime dateForButton = DateTime.Now.AddDays(-1);
					//txtbxFromDate.Text = Convert.ToString(dateForButton);
				}
                else
                {
                    var message = "Incorrect Information.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception exception)
            {
                ErrorHandling.SendErrorToText(exception);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse_DistancePerformanceOld"))
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
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
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
                    //var dtErrorResult = Bl.UploadExcelRecordBulkMinimumColumns(dt, "HorseOwnerStud");
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "DistancePerformanceOld");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("DistancePerformanceOld");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_DistancePerformanceOld.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();

                        }
                    }
                    else
                    {
                        var message = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        BindData();
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('All Record has been added successfully');", true);
                    }
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Issue found in record');", true);
                    var message = "Issue found in record.";
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
        /// bind drop down 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TableName_"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new MasterHorseBL().GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }


        protected void GvDistanceOld_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvDistanceOld.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
				HiddenField hdnvaltilldate = (HiddenField)row.FindControl("hdnfieldTillDate");
				var dataKey = GvDistanceOld.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    this.tblDistanceUpdate.Visible = true;
                    BindDropDown(drpdwnDistance, "Distance", "Distance", "DistanceID");
                    drpdwnDistance.Items.FindByText(hdnval.Value).Selected = true;
                    ViewState["horseRecordID"] = dataKey.Value;
                    this.txtbx1.Text = row.Cells[3].Text;
                    this.txtbx2.Text = row.Cells[4].Text;
                    this.txtbx3.Text = row.Cells[5].Text;
                    this.txtbx4.Text = row.Cells[6].Text;
                    this.txtbx5.Text = row.Cells[7].Text;
					this.txtbxFromDate.Text = hdnvaltilldate.Value;
					if(row.Cells[1].Text.Equals("Yes"))
					{
						chkboxShow.Checked = true;
					}
					else
					{
						chkboxShow.Checked = false;
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
        /// Export Gridview Data in Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //using (DataSet ds = new MasterHorseBL().GetHorseNameWithCombination(_horseId, "DistancePerformanceOld"))
                using (DataSet ds = new MasterHorseBL().GetExport(_horseId, "DistancePerformanceOld"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //dt.Columns.Remove("DistancePerformanceOldID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse_DistancePerformanceOld");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse_DistancePerformanceOld.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        var message = "No Record Found.";
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
    }
}