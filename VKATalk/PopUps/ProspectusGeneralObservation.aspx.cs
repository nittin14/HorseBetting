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

    public partial class ProspectusGeneralObservation : System.Web.UI.Page
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
					BindDropDown(drpdwnRelatedType, "ObservationRelatedType", "ObservationRelatedType", "PGORelatedTypeID");
					drpdwnRelatedType.Items.Insert(0, new ListItem("-- Please select --", "-1"));
					if (!Request.QueryString["GeneralRaceName"].Equals(""))
                    {
                        var masterracename = Session["ProspectusGeneralRaceName"].ToString();
                        string[] masterrcename = masterracename.Split('{');
                        lblGeneralRaceName.Text = masterrcename[0];
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


		private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
		{
			ddl.DataSource = new ProspectusBL().GetDropdownBind(tablename);
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
        public static List<string> AddCommentList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusGeneralObservationCommentAutofill", prefixText);
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
		public static List<string> AddHorseList(string prefixText, int count, string contextKey)
		{
			DataTable dt = new ProspectusBL().GetprospectusAutoFillerWithParameters("ObservationRelatedDetail", prefixText, contextKey);

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
		/// <summary>
		/// Fill current Mission
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> ReasonAutoFiller(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusGeneralObservationReasonAutofill", prefixText);
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
		public static List<string> ObservationAutoFiller(string prefixText, int count)
	{
			DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusGeneralObservationfill", prefixText);
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
                DataSet ds = new ProspectusBL().GetProspectusNameWithCombinationGeneral(_value, "Observation");

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
                if (btnSave.Text.Equals("Add"))
                {
                    status = new ProspectusBL().Observation(
                        _value,
                        txtbxObservation.Text,
                        rdbtnaimedduration.SelectedItem.Text,
                         txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxReason.Text,
                        txtbxComment.Text,
                        1,
                        "Insert",drpdwnRelatedType.SelectedItem.Value,hdnfieldRelatedNameID.Value);
                    //hdnfldprospectusid.Value = Convert.ToString(dt.Rows[0][1]);
                    
                }
                else
                {
                    status = new ProspectusBL().Observation(
                        (int)ViewState["GridViewRowID"],
                        txtbxObservation.Text,
                        rdbtnaimedduration.SelectedItem.Text,
                         txtbxFromDate.Text,
                            txtbxTillDate.Text,
                            txtbxReason.Text,
                        txtbxComment.Text,
                        _userId,
                        "Update", drpdwnRelatedType.SelectedItem.Value, hdnfieldRelatedNameID.Value);

                }
                //newprospectusId = Convert.ToInt32(dt.Rows[0][2]);
                if (status == 5)
                {
					GetGridviewData();
					ClearAllSelection(this);
					message = "Record activated successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (status == 4)
                {
                    message = "Record already exist.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    ClearAllSelection(this);
                    this.Page.ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "Popup",
                        this.btnSave.Text.Equals("Update")
                            ? "ShowPopup('Record Updated Successfully');"
                            : "ShowPopup('Record Added Successfully.');",
                        true);
                    GetGridviewData();
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
					var status = new ProspectusBL().Observation(
							(int)ViewState["GridViewRowID"],
							string.Empty, string.Empty,
							"__-__-____",
							"__-__-____",
							string.Empty, string.Empty,
							1,
							"Delete",string.Empty, string.Empty);
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
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldObservation");
				HiddenField hdnfieldObservationRelatedToNameID = (HiddenField)row.FindControl("hdnfieldRelatedNameID");
				//hdnfieldRelatedNameID.Value
				var dataKey = GvProspectus.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
					if (!(hdnfieldObservationRelatedToNameID==null))
						hdnfieldRelatedNameID.Value = hdnfieldObservationRelatedToNameID.Value;
					//string strobservation = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
					//txtbxObservation.Text = strobservation.Replace("&amp;", "&");
					//rdbtnaimedduration.ClearSelection();
					//rdbtnaimedduration.Items.FindByText(row.Cells[1].Text).Selected = true;


					string strobservation = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    rdbtnaimedduration.ClearSelection();
                    rdbtnaimedduration.Items.FindByText(strobservation).Selected = true;

                    if (!row.Cells[1].Text.Contains("&nbsp;"))
                    {
                        string strobservation1 = row.Cells[1].Text.Equals("&nbsp;") ? "" : row.Cells[1].Text.Contains("&#39;") ? row.Cells[1].Text.Replace("&#39;", "'") : row.Cells[1].Text;
                        txtbxObservation.Text = strobservation1.Replace("&amp;", "&");
                    }
					if (!row.Cells[2].Text.Contains("&nbsp;"))
					{
						drpdwnRelatedType.Items.FindByText(row.Cells[2].Text).Selected = true;
					}
						
                    if (row.Cells[3].Text.Contains("&quot;"))
                    {
                        string strreason = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&quot;") ? row.Cells[3].Text.Replace("&quot;", "\"") : row.Cells[3].Text;
                        txtbxObservationRelatedName.Text = strreason.Replace("&amp;", "&");
                    }
                    else
                    {
						string strreason1 = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&quot;") ? row.Cells[3].Text.Replace("&quot;", "\"") : row.Cells[3].Text;
						txtbxObservationRelatedName.Text = strreason1.Replace("&amp;", "&");
                    }

                    txtbxFromDate.Text = row.Cells[7].Text;
					if (!row.Cells[8].Text.Contains("&nbsp;"))
					{
						txtbxTillDate.Text = row.Cells[8].Text;
					}
					
                    
                    if (row.Cells[9].Text.Contains("&quot;"))
                    {
                        string strcomments = row.Cells[9].Text.Equals("&nbsp;") ? "" : row.Cells[9].Text.Contains("&quot;") ? row.Cells[9].Text.Replace("&quot;", "\"") : row.Cells[9].Text;
                        txtbxComment.Text = strcomments.Replace("&amp;", "&");
                    }
                    else
                    {
                        string strcomments1 = row.Cells[9].Text.Equals("&nbsp;") ? "" : row.Cells[9].Text.Contains("&#39;") ? row.Cells[9].Text.Replace("&#39;", "'") : row.Cells[9].Text;
                        txtbxComment.Text = strcomments1.Replace("&amp;", "&");
                    }
					if (row.Cells[6].Text.Contains("&quot;"))
					{
						string strreason = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&quot;") ? row.Cells[6].Text.Replace("&quot;", "\"") : row.Cells[6].Text;
						txtbxReason.Text = strreason.Replace("&amp;", "&");
					}
					else
					{
						string strreason1 = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&quot;") ? row.Cells[6].Text.Replace("&quot;", "\"") : row.Cells[6].Text;
						txtbxReason.Text = strreason1.Replace("&amp;", "&");
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_General_Observation"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusGeneralObservation", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Observation");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_Observation.xlsx");
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
				var message = "Incorrect Information.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }

        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    ClearAllSelection(this);
        //    btnSave.Text = "Add";
        //}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //using (DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(0, "MasterMillion"))
                using (DataSet ds = new ProspectusBL().GetExport(0, "ProspectusGeneralObservation"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
						dt.Columns.Remove("GeneralObservationID");

						using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Observation");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_General_Observation.xlsx");
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