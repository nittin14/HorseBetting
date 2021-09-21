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

    public partial class ProspectusGeneralDates : System.Web.UI.Page
    {
        //MasterHorseBL Bl = new MasterHorseBL();
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["ProspectusGeneralID"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["ProspectusGeneralID"]);
                hdnfldprospectusid.Value = Convert.ToString(_value);
            }
            if (!IsPostBack)
            {
                try
                {
                    if (!Request.QueryString["GeneralRaceName"].Equals(""))
                    {
                        var masterracename = Session["ProspectusGeneralRaceName"].ToString();
                        string[] masterrcename = masterracename.Split('{');
                        lblGeneralRaceName.Text = masterrcename[0];
                        BindDropDown(drpdnTypeofDate, "GeneralDateType", "DateType", "GeneralDateTypeID");
						BindDropDown(this.drpdwnDateTerm, "GeneralDateTerm", "DateTerm", "DateTermMID");
						//drpdwnDateTerm.Items.Insert(0, new ListItem("-- Please select --", "-1"));

						PopulateHours();
                        //PopulateMinutes();
                    }

                    if (_value != 0)
                    {
                        GetGridviewData();
                    }
                }
                catch (Exception ex)
                {
                    object[] now = new object[] { "Page_Load (HorsePopup):", DateTime.Now, ", Issue Detail:", ex.Message + ex.StackTrace };
                    ErrorHandling.CheckEachSteps(string.Concat(now));
                    var message = "Incorrect Information.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
        }

        private void PopulateHours()
        {
            for (int i = 0; i <= 12; i++)
            {

                drpdwnhh.Items.Add(i.ToString("D2"));
            }
        }
        private void PopulateMinutes()
        {
            for (int i = 0; i <= 59; i++)
            {
                drpdwnMM.Items.Add(i.ToString("D2"));
            }
        }

        private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        {
            DataTable dt = new ProspectusBL().GetDropdownBind(tablename);
            ddl.DataSource = dt;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
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
        public static List<string> AddReasonOfChange(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("GeneralDateReasonOfChange", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
            }
            return horseList;
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
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("GeneralDateMyComments", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
            }
            return horseList;
        }

		/// <summary>
		/// Fill current Mission
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> AddAmountInWords(string prefixText, int count)
		{
			DataTable dt = new ProspectusBL().GetprospectusAutoFiller("GeneralDateAmountInWords", prefixText);
			List<string> horseList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				horseList.Add(dt.Rows[i][0].ToString());
			}
			return horseList;
		}


		private void GetGridviewData()
        {
            try
            {
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombinationGeneral(_value, "GeneralDates");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvProspectus.DataSource = ds.Tables[0];
                    GvProspectus.DataBind();
                }
                else
                {
                    GvProspectus.DataSource = new DataTable();
                    GvProspectus.DataBind();
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
        /// This button Save the HorseName popup data in database and bind with dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string message = string.Empty;
                int newprospectusId = 0;
                DataTable dt;
                int status = 0;
                string minutes = string.Empty;
               
                if (!(drpdwnhh.SelectedItem.Text.Equals("00") && drpdwnMM.SelectedItem.Text.Equals("00")))
                {
                   minutes= drpdwnhh.SelectedItem.Text + ":" + drpdwnMM.SelectedItem.Text + "(" + drpdwnAMPM.SelectedItem.Text + ")";
                }
                string notallowed = (chkboxNotAllowed.Checked) ? "Not Allowed" : string.Empty ;
                //var datetermselected = string.Empty;
                //datetermselected = drpdwnDateTerm.SelectedItem.Text;

                int? fees = (chkboxNotAllowed.Checked || txtbxfees.Text.Equals("")) ? (int?)null : Convert.ToInt32(txtbxfees.Text);




                if (btnSave.Text.Equals("Add"))
                {
                    status = new ProspectusBL().GeneralDates(
                        _value,
                        Convert.ToInt32(drpdnTypeofDate.SelectedItem.Value),
                        notallowed,
						drpdwnDateTerm.SelectedItem.Value,
                        txtbxFromDate.Text,
                        minutes,
                        1,
                        "Insert", fees,txtbxAmountPercentage.Text,txtbxInWords.Text, txtbxReasonOfChange.Text, txtbxMyComments.Text);

                }
                else
                {
                    status = new ProspectusBL().GeneralDates(
                        (int)ViewState["GridViewRowID"],
                        Convert.ToInt32(drpdnTypeofDate.SelectedItem.Value),
                        notallowed,
						drpdwnDateTerm.SelectedItem.Value,
                        txtbxFromDate.Text,
                        minutes,
                        1,
                        "Update", fees, txtbxAmountPercentage.Text, txtbxInWords.Text, txtbxReasonOfChange.Text, txtbxMyComments.Text);

                }
                //newprospectusId = Convert.ToInt32(dt.Rows[0][2]);
                if (status == 1 || status == 2)
                {
                    message = status == 1 ? "Record added successfully." : "Record update successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    GetGridviewData();
                    ClearAllSelection(this);
                    if (status == 2)
                    {
                        btnSave.Text = "Add";
                    }
                }
                else if (status == 5)
                {
                    message = "Record activated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    GetGridviewData();
                    ClearAllSelection(this);
                    btnSave.Text = "Add";
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
				if (!ViewState["GridViewRowID"].Equals(""))
				{
					var status = new ProspectusBL().GeneralDates(
							 (int)ViewState["GridViewRowID"],
							 0,
						string.Empty,
						string.Empty,
						"__-__-____",
						string.Empty,
						1,
						"Delete", 0,string.Empty,string.Empty, string.Empty, string.Empty);
					ClearAllSelection(this);
					GetGridviewData();
					btnSave.Text = "Add";
					var message = "Record Deleted Successfully.";
					ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					ViewState["GridViewRowID"] = "";
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
		/// Close the window and pass the value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnfldprospectusid.Value != "")
                {
                    Session["ProspectusGeneralID"] = hdnfldprospectusid.Value;
                 
                    if (!Request.QueryString["GeneralRaceName"].Equals(""))
                    {
                        Session["ProspectusGeneralName"] = Request.QueryString["GeneralRaceName"];
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }


            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnSave.Text = "Add";
            if (chkboxNotAllowed.Checked.Equals(true))
            {
                chkboxNotAllowed.Checked = false;
               // drpdwnDateTerm.Enabled = true;
                txtbxFromDate.Enabled = true;
                drpdwnhh.Enabled = true;
                drpdwnMM.Enabled = true;
                drpdwnAMPM.Enabled = true;
                txtbxfees.Enabled = true;
                txtbxReasonOfChange.Enabled = true;
                txtbxMyComments.Enabled = true;
				txtbxAmountPercentage.Enabled = true;
				txtbxInWords.Enabled = true;
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
                    ((RadioButtonList)(x)).SelectedValue = "-1";
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }

            if (chkboxNotAllowed.Checked.Equals(true))
            {
                chkboxNotAllowed.Checked = false;
               // drpdwnDateTerm.Enabled = true;
                txtbxFromDate.Enabled = true;
                drpdwnhh.Enabled = true;
                drpdwnMM.Enabled = true;
                drpdwnAMPM.Enabled = true;
                txtbxfees.Enabled = true;
                txtbxReasonOfChange.Enabled = true;
                txtbxMyComments.Enabled = true;
				txtbxAmountPercentage.Enabled = true;
				txtbxInWords.Enabled = true;
			}
        }

        /// <summary>
        /// on select index change
        /// </summary>
        /// <param name="sender">value</param>
        /// <param name="e">event</param>
        protected void GvProspectus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                GridViewRow row = GvProspectus.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldtypeofdate");
                var dataKey = GvProspectus.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    drpdnTypeofDate.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnDateTerm.Items.FindByText(row.Cells[1].Text).Selected = true;
                    if (!row.Cells[2].Text.Equals("Not Allowed"))
                    {
                        chkboxNotAllowed.Checked = false;
                    }
                    else
                    {
                        chkboxNotAllowed.Checked = true;
                    }
                    if (chkboxNotAllowed.Checked.Equals(false))
                    {
                        
                        if (row.Cells[3].Text.Contains("&quot;"))
                        {
                            txtbxFromDate.Text = "";
                        }
                        else
                        {
                            string strcomments = row.Cells[3].Text;
                            txtbxFromDate.Text = strcomments.Replace("&amp;", "&");
                        }
                        if (!row.Cells[4].Text.Contains("&nbsp;"))
                        {
                            string minutes = row.Cells[4].Text;
                            string[] minsplit = minutes.Split(':');
                            string[] time = minsplit[1].Split('(');
                            string[] time2 = time[1].Split(')');
                            drpdwnhh.Items.FindByText(minsplit[0]).Selected = true;
                            drpdwnMM.Items.FindByText(time[0]).Selected = true;
                            drpdwnAMPM.Items.FindByText(time2[0]).Selected = true;
                        }
                        
                        if (row.Cells[5].Text.Contains("&nbsp;"))
                        {
                            txtbxfees.Text = "";
                        }
                        else
                        {
                            string fees = row.Cells[5].Text;
                            txtbxfees.Text = fees.Replace("&amp;", "&");
                        }

						if (row.Cells[6].Text.Contains("&nbsp;"))
						{
							txtbxAmountPercentage.Text = "";
						}
						else
						{
							var reasonofchange = row.Cells[6].Text;
							txtbxAmountPercentage.Text = reasonofchange.Replace("&amp;", "&");
						}

						if (row.Cells[7].Text.Contains("&nbsp;"))
						{
							txtbxInWords.Text = "";
						}
						else
						{
							var reasonofchange = row.Cells[7].Text;
							txtbxInWords.Text = reasonofchange.Replace("&amp;", "&");
						}



						if (row.Cells[8].Text.Contains("&nbsp;"))
                        {
                            txtbxReasonOfChange.Text = "";
                        }
                        else
                        {
                            string reasonofchange = row.Cells[8].Text;
                            txtbxReasonOfChange.Text = reasonofchange.Replace("&amp;", "&");
                        }
                        if (row.Cells[9].Text.Contains("&nbsp;"))
                        {
                            txtbxMyComments.Text = "";
                        }
                        else
                        {
                            string mycomments = row.Cells[9].Text;
                            txtbxMyComments.Text = mycomments.Replace("&amp;", "&");
                        }
                    }
                     if (chkboxNotAllowed.Checked.Equals(true))
                     {
                         //chkboxNotAllowed.Checked = false;
                         //drpdwnDateTerm.Enabled = false;
                         txtbxFromDate.Enabled = false;
                         drpdwnhh.Enabled = false;
                         drpdwnMM.Enabled = false;
                         drpdwnAMPM.Enabled = false;
                         txtbxfees.Enabled = false;
                         txtbxReasonOfChange.Enabled = false;
                         txtbxMyComments.Enabled = false;
						txtbxAmountPercentage.Enabled = false;
						txtbxInWords.Enabled = false;
					}
                }
            }
            catch (Exception ex)
            {
                btnSave.Text = "Add";
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_General_Dates"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusGeneralDates", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("General Dates");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_Dates.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        GetGridviewData();
                        var message1 = "All Record has been added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }


        }

        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    ClearAllSelection(this);
        //    btnSave.Text = "Add";
        //}
        protected void chkboxNotAllowed_Change(object sender, EventArgs e)
        {
            try
            {

                if(chkboxNotAllowed.Checked.Equals(true))
                {
                   // drpdwnDateTerm.Enabled=false;
                    txtbxFromDate.Enabled=false;
                    drpdwnhh.Enabled=false;
                    drpdwnMM.Enabled=false;
                    drpdwnAMPM.Enabled=false;
                    txtbxfees.Enabled=false;
                    txtbxReasonOfChange.Enabled=false;
                    txtbxMyComments.Enabled=false;
					txtbxAmountPercentage.Enabled = false;
					txtbxInWords.Enabled = false;
				}
                else{
                    // drpdwnDateTerm.Enabled=true;
                    txtbxFromDate.Enabled=true;
                    drpdwnhh.Enabled=true;
                    drpdwnMM.Enabled=true;
                    drpdwnAMPM.Enabled=true;
                    txtbxfees.Enabled=true;
                    txtbxReasonOfChange.Enabled=true;
                    txtbxMyComments.Enabled=true;
					txtbxAmountPercentage.Enabled = true;
					txtbxInWords.Enabled = true;
				}
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + "');", true);
            }
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //using (DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(0, "MasterMillion"))
                using (DataSet ds = new ProspectusBL().GetExport(0, "ProspectusGeneralDates"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("General Dates");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_Dates.xlsx");
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
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + ex.StackTrace + "');", true);
            }
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

    }
}