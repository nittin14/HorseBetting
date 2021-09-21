using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.Globalization;
using System.Data;

namespace VKATalk.PopUps
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class ProfessionalName : System.Web.UI.Page
    {
        ProfessionalBL Bl = new ProfessionalBL();
        int _userId = 1;
        private int _value = 0;
        //private var _regnum = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
                if (!Request.QueryString["ProfessionalValue"].Equals(""))
                {
                    _value = Convert.ToInt32(Request.QueryString["ProfessionalValue"]);
                    hdnfldProfessionalId.Value = Convert.ToString(_value);
                }
               
                if (!IsPostBack)
                {
                    
                        if (_value != 0)
                        {
                            
                            GetGridviewData();
                            //btnSave.Text = "Update";
                        }
                        else
                        {
                            //GetRegistrationNumber();
                            //btnSave.Text = "Add";
                        }
                        BindDropDown(this.drpdwnProfiler, "ProfessionalProfileType", "ProfileType", "ProfileTypeID");
                        drpdwnProfiler.Items.FindByText("Owner").Selected = true;
                        BindDropDown(drpdwnProfessionalType, "ProfessionalType", "ProfessionalType", "ProfessionalTypeID");
                        drpdwnProfessionalType.Items.FindByText("Individual").Selected = true;
                        BindDropDown(this.drpdwnBaseCenter, "MasterCenter", "CenterName", "CenterID");
                        if(drpdwnBaseCenter.Items.FindByText("Unknown")!=null)
                        {
                            drpdwnBaseCenter.Items.FindByText("Unknown").Selected = true;
                        }
                        //drpdwnBaseCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                }
                if (!Request.QueryString["RegNum"].Equals(""))
                {
                    hdnfieldRegistrationNumber.Value = Request.QueryString["RegNum"];
                    lblRegistrationNumber.Text = hdnfieldRegistrationNumber.Value;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
            }
        }


        /// <summary>
        /// bind drop down 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="tableName_"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        private void BindDropDown(DropDownList ddl, String tableName_, string textField, String valueField)
        {
            DataTable dt;
            dt = new ProfessionalBL().GetProfessionalName(tableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
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
		public static List<string> AddCommentsList(string prefixText, int count)
		{
			DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("CommentList", prefixText);
			List<string> commentList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				commentList.Add(dt.Rows[i][0].ToString());
			}
			return commentList;
		}


		private void GetGridviewData()
        {
                DataSet ds = Bl.GetProfessionalNameWithCombination(_value, "ProfessionalName");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvHorseName.DataSource = ds.Tables[0];
                    GvHorseName.DataBind();
                }
                else
                {
                    GvHorseName.DataSource = new DataTable();
                    GvHorseName.DataBind();
                }
        }

        /// <summary>
        /// Bind Horse Name already exist in out Database
        /// </summary>
        private void BindData(DataTable dt)
        {
                DataSet ds = null;

                if (btnSave.Text.Equals("Add"))
                {
                    ds = Bl.GetProfessionalNameWithCombination(Convert.ToInt32(dt.Rows[0][1]), "ProfessionalNameCurrentInsert");
                }
                else
                {
                    ds = Bl.GetProfessionalNameWithCombination(Convert.ToInt32(dt.Rows[0][0]), "ProfessionalNameCurrentUpdate");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvHorseName.DataSource = ds.Tables[0];
                    GvHorseName.DataBind();
                }
                else
                {
                    GvHorseName.DataSource = new DataTable();
                    GvHorseName.DataBind();
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
                //string message = string.Empty;
                int status = 0;
                DataTable dt;
				if (btnSave.Text.Equals("Add") && GvHorseName.Rows.Count > 0)
				{
					var message = "Professional Name Already Created.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}
				else
				{
					if (btnSave.Text.Equals("Add"))
					{
						dt = this.Bl.ProfessionalName(
							0,
							this.txtbxProfessionalName.Text,
							txtbxPNWithoutSolution.Text,
							this.txtbxProfessionalShortName.Text,
							this.txtbxProfessionalAlias.Text,
							this.txtbxComment.Text,
							this._userId,
							"Insert",
							Convert.ToInt32(drpdwnProfiler.SelectedItem.Value),
							Convert.ToInt32(drpdwnBaseCenter.SelectedItem.Value),
							0, 0, txtbxDateofBirth.Text,drpdwnProfessionalType.SelectedItem.Value);

					}
					else
					{
						dt = this.Bl.ProfessionalName(
						   (int)ViewState["GridViewRowID"],
							this.txtbxProfessionalName.Text,
							txtbxPNWithoutSolution.Text,
							this.txtbxProfessionalShortName.Text,
							this.txtbxProfessionalAlias.Text,
						   this.txtbxComment.Text,
						   this._userId,
						   "Update",
							Convert.ToInt32(drpdwnProfiler.SelectedItem.Value),
							Convert.ToInt32(drpdwnBaseCenter.SelectedItem.Value),
							0,
							0, txtbxDateofBirth.Text, drpdwnProfessionalType.SelectedItem.Value);


					}
					status = Convert.ToInt32(dt.Rows[0][2]);
					var message = string.Empty;

					if (status == 1)
					{
						message = "Record added successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						if (btnSave.Text.Equals("Add"))
						{
							hdnfldProfessionalId.Value = Convert.ToString(dt.Rows[0][1]);
							hdnfieldprofessionalName.Value = txtbxProfessionalName.Text;
						}
						else
						{
							hdnfieldprofessionalName.Value = txtbxProfessionalName.Text;
						}
						BindData(dt);
						ClearAllSelection(this);
						drpdwnProfiler.Items.FindByText("Owner").Selected = true;
						drpdwnBaseCenter.Items.FindByText("Unknown").Selected = true;
					}
					else if (status == 2)
					{
						if (btnSave.Text.Equals("Add"))
						{
							hdnfldProfessionalId.Value = Convert.ToString(dt.Rows[0][1]);
							hdnfieldprofessionalName.Value = txtbxProfessionalName.Text;
						}
						else
						{
							hdnfieldprofessionalName.Value = txtbxProfessionalName.Text;
						}
						message = "Record updated successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						BindData(dt);
						ClearAllSelection(this);
						btnSave.Text = "Add";
						drpdwnProfiler.Items.FindByText("Owner").Selected = true;
						drpdwnBaseCenter.Items.FindByText("Unknown").Selected = true;
					}
					else if (status == 5)
					{
						if (btnSave.Text.Equals("Add"))
						{
							hdnfldProfessionalId.Value = Convert.ToString(dt.Rows[0][1]);
							hdnfieldprofessionalName.Value = txtbxProfessionalName.Text;
						}
						else
						{
							hdnfieldprofessionalName.Value = txtbxProfessionalName.Text;
						}
						message = "Record activated successfully.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						BindData(dt);
						ClearAllSelection(this);
						btnSave.Text = "Add";
						drpdwnProfiler.Items.FindByText("Owner").Selected = true;
						drpdwnBaseCenter.Items.FindByText("Unknown").Selected = true;
					}
					else if (status == 4)
					{
						message = "Record already exist.";
						ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						ClearAllSelection(this);
						btnSave.Text = "Add";
					}
					else
					{
						ErrorHandling.CheckEachSteps(Convert.ToString(status));
						message = "Issue in Record. (Status) : " + status;
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
                if (hdnfldProfessionalId.Value != "")
                {
                    Session["ProfessionalID"] = hdnfldProfessionalId.Value;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }
                

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
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

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }

        /// <summary>
        /// on select index change
        /// </summary>
        /// <param name="sender">value</param>
        /// <param name="e">event</param>
        protected void GvHorseName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvHorseName.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvHorseName.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    string strprofessionalname= hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    txtbxProfessionalName.Text = strprofessionalname.Replace("&amp;", "&");
                    string strsolutation= row.Cells[2].Text.Equals("&nbsp;") ? "" : row.Cells[2].Text.Contains("&#39;") ? row.Cells[2].Text.Replace("&#39;", "'") : row.Cells[2].Text;
                    txtbxPNWithoutSolution.Text = strsolutation.Replace("&amp;", "&");
                     string strprofessionalshortname= row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                     txtbxProfessionalShortName.Text = strprofessionalshortname.Replace("&amp;", "&");
                    string strprofessionalshortnamealias= row.Cells[4].Text.Equals("&nbsp;") ? "" : row.Cells[4].Text.Contains("&#39;") ? row.Cells[4].Text.Replace("&#39;", "'") : row.Cells[4].Text;
                    txtbxProfessionalAlias.Text = strprofessionalshortnamealias.Replace("&amp;", "&");
                    drpdwnProfiler.Items.FindByText(row.Cells[6].Text).Selected = true;
                    drpdwnBaseCenter.Items.FindByText(row.Cells[7].Text).Selected = true;
                    if (row.Cells[8].Text != "" || row.Cells[8].Text != null)
                        txtbxDateofBirth.Text = row.Cells[8].Text;
                    if (row.Cells[9].Text != "" || row.Cells[9].Text != null)
                        drpdwnProfessionalType.Items.FindByText(row.Cells[9].Text).Selected = true;

                    txtbxComment.Text = row.Cells[10].Text.Equals("&nbsp;") ? "" : row.Cells[10].Text.Contains("&#39;") ? row.Cells[10].Text.Replace("&#39;", "'") : row.Cells[10].Text;
                    if (row.Cells[0].Text.Equals("Cr"))
                    {
                        if (GvHorseName.Rows.Count == 2)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please select Old Name');", true);
                            ClearAllSelection(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information');", true);
            }
        }

        protected void txtbxProfessionalName_OnTextChanged(object sender, EventArgs e)
        {
            var txtvalue = sender as TextBox;
            if (txtvalue != null)
            {
                string value = txtvalue.Text;
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this.txtbxProfessionalName.Text = textInfo.ToTitleCase(value.ToLower());
                this.txtbxProfessionalShortName.Text = this.txtbxProfessionalName.Text.Replace(" ", "");
                txtbxProfessionalAlias.Text = this.txtbxProfessionalName.Text;
				txtbxPNWithoutSolution.Text = this.txtbxProfessionalName.Text;
			}

            //var txtvalue = sender as TextBox;
            //if (txtvalue != null)
            //{
            //    //string value = txtvalue.Text;
            //    //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            //    //this.txtbxProfessionalName.Text = textInfo.ToTitleCase(value.ToLower());
            //    this.txtbxProfessionalShortName.Text = this.txtbxProfessionalName.Text.Replace(" ", "");
            //    txtbxProfessionalAlias.Text = this.txtbxProfessionalName.Text;
            //}
        }


        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                   string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Professional_Name"))
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
                    var dtErrorResult = new ProfessionalBL().Import30(dt, "ProfessionalName");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional_Name");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional_Name.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {

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
                using (DataSet ds = new ProfessionalBL().GetExport(0, "ProfessionalName"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("ProfessionalID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional Name");
                            int rowstart = 1;
                            int colstart = 1;
                            int rowend = rowstart;
                            int colend = colstart + (dt.Columns.Count - 1);
                            //  int colend = colstart;
                            rowend = rowstart + dt.Rows.Count;
                            ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                            int i = 1;
                            foreach (DataColumn dc in dt.Columns)
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional_Name.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();

                        }
                    }
                    else
                    {
                        var message = "No Record found.";
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
        
    }
}