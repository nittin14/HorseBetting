using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using VKATalkBusinessLayer;

namespace VKATalk.Card
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.WebSockets;

    using OfficeOpenXml;
    using VKATalk.Common;

    public partial class RaceCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxDeclarationEnterDate.Text = CommonMethods.CurrentDate();
            }
        }


        protected void txtbxJockeyNameG_OnTextChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = (GridViewRow)(sender as TextBox).NamingContainer;
                TextBox jkname = row.FindControl("txtbxJockeyNameG") as TextBox;
                Label lblDJA = row.FindControl("lblDJA") as Label;
                Label lblACCEPTANCEWEIGHTGBC = row.FindControl("lblACCEPTANCEWEIGHTGBC") as Label;
                TextBox txtbxDeclareWeightG = row.FindControl("txtbxCDWG") as TextBox;

                if (!jkname.Text.Equals(""))
                {
                    HiddenField hdnfieldProfessionalNameid = row.FindControl("hdnfieldProfessionalNameid1") as HiddenField;
                    HiddenField hdnfieldDivisionRaceID = row.FindControl("hdnfieldDivisionRaceID") as HiddenField;
                    if (hdnfieldProfessionalNameid != null)
                    {
                        DataTable dt = new CardsBL().GetCardDJA(Convert.ToInt32(hdnfieldProfessionalNameid.Value),
                                           txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldDivisionRaceID.Value));
                        if (dt.Rows.Count > 0)
                        {
                            lblDJA.Text = dt.Rows[0][0].ToString();
                            txtbxDeclareWeightG.Text = Convert.ToString(Convert.ToDouble(lblACCEPTANCEWEIGHTGBC.Text) - Convert.ToDouble(dt.Rows[0][0]));
                        }
                        else
                        {
                            lblDJA.Text = string.Empty;
                            txtbxDeclareWeightG.Text = lblACCEPTANCEWEIGHTGBC.Text;
                        }
                    }
                }
                else
                {
                    lblDJA.Text = string.Empty;
                    txtbxDeclareWeightG.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void txtbxDivisionRaceDate_OnTextChanged(object sender, EventArgs e)
        {
            var dt = new CardsBL().GetRaceCenterName(txtbxDivisionRaceDate.Text);
            if (dt.Rows.Count > 0)
            {
                drpdwnCenterName.DataSource = dt;
                drpdwnCenterName.DataTextField = "CenterName";
                drpdwnCenterName.DataValueField = "ID";
                drpdwnCenterName.DataBind();
                drpdwnCenterName.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                drpdwnCenterName.Focus();
            }
        }

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "RaceCard");
                if (dt.Rows.Count > 0)
                {
                    lblSeason.Text = dt.Rows[0][10].ToString();
                    lblYear.Text = dt.Rows[0][11].ToString();
                    dvgridview.Visible = true;
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();
                }
                else
                {
                    dvgridview.Visible = false;
                    grdvwRaceDetail.DataSource = new DataTable();
                    grdvwRaceDetail.DataBind();

                    GvShowALL.DataSource = new DataTable();
                    GvShowALL.DataBind();
                }

                lblEntryDate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //BtnSubmit.Text = "Update";
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnfielddivisionracename = (HiddenField)row.FindControl("hdnfielddivisionracename");
                HiddenField hdnfieldGeneralRaceNameIDG = (HiddenField)row.FindControl("hdnfieldGeneralRaceNameIDG");
                hdnfieldGeneralRaceNameID.Value = hdnfieldGeneralRaceNameIDG.Value;
                HiddenField hdnfieldGeneralRaceID = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["DivisionRaceID"] = dataKey.Value; //generalraceid
                    ViewState["DivisionRaceName"] = hdnfielddivisionracename.Value;//reneralracename
                    ViewState["GeneralRaceNameID"] = hdnfieldGeneralRaceNameIDG.Value;
                    ViewState["GeneralRaceID"] = hdnfieldGeneralRaceID.Value;
                    lblGeneralRaceName.Text = ViewState["DivisionRaceName"].ToString() + " (" + ViewState["DivisionRaceID"].ToString() + ")";
                }

                var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(dataKey.Value), "RaceCard", lblSeason.Text, lblYear.Text);
                if (dsGeneralDate.Tables[0].Rows.Count > 0)
                {
                    if (dsGeneralDate.Tables.Count == 1)
                    {
                        if (dsGeneralDate.Tables[0].Rows.Count > 0)
                        {
                            lblEntryDate.Text = dsGeneralDate.Tables[0].Rows[0][0].ToString();
                        }
                        else
                        {
                            lblEntryDate.Text = string.Empty;
                        }
                    }
                    else
                    {
                        lblEntryDate.Text = string.Empty;
                    }
                }
                else
                {
                    lblEntryDate.Text = string.Empty;
                }
                ShowDeclaration();

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        private void ShowDeclaration()
        {
            var dt = new CardsBL().GetDeclaration(Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32(ViewState["GeneralRaceNameID"]), ViewState["DivisionRaceName"].ToString(), "RaceCard", txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
            if (dt.Tables[0].Rows.Count > 0)
            {
                GvShowALL.DataSource = dt;
                GvShowALL.DataBind();
            }
            else
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }
        }

        protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpdwnOwnerColorCap = (e.Row.FindControl("drpdwnOwnerColorCap") as DropDownList);
                BindDropDown(drpdwnOwnerColorCap, "RaceCardOwnerColorCap", "CapColor", "OwnerColorID");
                drpdwnOwnerColorCap.Items.Insert(0, new ListItem("-- Please select --", "-1"));


                DropDownList drpdwnJockeyChangeReasonG = (e.Row.FindControl("drpdwnJockeyChangeReason") as DropDownList);
                BindDropDown(drpdwnJockeyChangeReasonG, "JockeyChangeReason", "JockeyChangeReason", "JockeyChangeReasonCMID");
                drpdwnJockeyChangeReasonG.Items.Insert(0, new ListItem("-- Please select --", "-1"));


                DropDownList drpdwnHorseRunningStatusG = (e.Row.FindControl("drpdwnHorseRunningStatus") as DropDownList);
                BindDropDown(drpdwnHorseRunningStatusG, "Master_CardHorseRunningStatus", "HorseRunningStatus", "HorseRunningStatusCMID");
                // drpdwnHorseRunningStatusG.Items.Insert(0, new ListItem("-- Please select --", "-1"));


                DataRowView dr = e.Row.DataItem as DataRowView;
                if (!(dr["OwnerCapColor"].ToString().Equals("") || dr["OwnerCapColor"].ToString().Equals("-1")))
                {
                    drpdwnOwnerColorCap.ClearSelection();
                    drpdwnOwnerColorCap.Items.FindByText(dr["OwnerCapColor"].ToString()).Selected = true;
                }


                if (!dr["JockeyChangeReason"].ToString().Equals(""))
                {
                    drpdwnJockeyChangeReasonG.ClearSelection();
                    drpdwnJockeyChangeReasonG.Items.FindByText(dr["JockeyChangeReason"].ToString()).Selected = true;
                }

                if (!dr["HorseRunningStatus"].ToString().Equals(""))
                {
                    drpdwnHorseRunningStatusG.ClearSelection();
                    drpdwnHorseRunningStatusG.Items.FindByText(dr["HorseRunningStatus"].ToString()).Selected = true;
                }
                else
                {
                    drpdwnHorseRunningStatusG.ClearSelection();
                    drpdwnHorseRunningStatusG.Items.FindByText("Running.").Selected = true;
                }

            }

        }
        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            dt = new ProspectusBL().GetDropdownBind(TableName_);
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
        public static List<string> AddJockeyList(string prefixText, int count)
        {
            DataTable dt = new CardsBL().GetCardAutoFiller("CardDeclarationJockeyList", prefixText);

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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dtDeclaration = new DataTable("Declaration");
            try
            {
                //DataTable status = new CardsBL().GetDeclareOldRecordStatus(Convert.ToInt32(ViewState["GeneralRaceNameID"]), txtbxDivisionRaceDate.Text, 
                //    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), lblSeason.Text, lblYear.Text, "RaceCard", Convert.ToInt32(ViewState["DivisionRaceID"]));
                //if (status.Rows.Count > 0)
                //{
                //    var message = "Records already exists.";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //}
                //else
                //{
                dtDeclaration.Columns.Add("DataEntryDate", typeof(string));
                dtDeclaration.Columns.Add("DivisionRaceDate", typeof(string));
                dtDeclaration.Columns.Add("CenterMID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceNameID", typeof(int));
                dtDeclaration.Columns.Add("DivisionRaceID", typeof(int));
                dtDeclaration.Columns.Add("HorseNo", typeof(int));//6
                dtDeclaration.Columns.Add("HorseID", typeof(int));
                dtDeclaration.Columns.Add("HorseNameID", typeof(int));
                dtDeclaration.Columns.Add("OwnerCapColorID", typeof(int));
                dtDeclaration.Columns.Add("EmergencyOwnerColor", typeof(string));
                dtDeclaration.Columns.Add("ChangedJockeyID", typeof(int));//11
                dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));
                dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                dtDeclaration.Columns.Add("IsActive", typeof(int));//14
                dtDeclaration.Columns.Add("ChangedJockeyNamePID", typeof(int));//15
                dtDeclaration.Columns.Add("ChangedDeclareJockeyAllowance", typeof(string));
                dtDeclaration.Columns.Add("ChangedDeclareWeight", typeof(string));
                dtDeclaration.Columns.Add("JockeyChangeReasonCMID", typeof(int));
                dtDeclaration.Columns.Add("HorseRunningStatusCMID", typeof(int));


                int rowcount = 0;
                foreach (GridViewRow row in GvShowALL.Rows)
                {
                    DataTable status = new CardsBL().GetDeclareOldRecordStatus(Convert.ToInt32(ViewState["GeneralRaceNameID"]), txtbxDivisionRaceDate.Text,
                                           Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), lblSeason.Text, lblYear.Text, "RaceCard",
                                           Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32((row.FindControl("hdnfieldHorseNameID") as HiddenField).Value));
                    if (status.Rows.Count <= 0)
                    {
                        dtDeclaration.Rows.Add();
                        if (txtbxDeclarationEnterDate.Text.Equals("__-__-____"))
                        {
                            dtDeclaration.Rows[rowcount][0] = DBNull.Value;
                        }
                        else
                        {
                            string[] dateString = txtbxDeclarationEnterDate.Text.Split('-');
                            DateTime enterDate =
                                Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                            dtDeclaration.Rows[rowcount][0] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                        }

                        if (txtbxDivisionRaceDate.Text.Equals("__-__-____"))
                        {
                            dtDeclaration.Rows[rowcount][1] = DBNull.Value;
                        }
                        else
                        {
                            string[] dateString = txtbxDivisionRaceDate.Text.Split('-');
                            DateTime enterDate =
                                Convert.ToDateTime(dateString[2].Substring(0, 4) + "-" + dateString[1] + "-" + dateString[0]);
                            dtDeclaration.Rows[rowcount][1] = enterDate.ToString("yyyy-MM-dd 00:00:00");
                        }

                        dtDeclaration.Rows[rowcount][2] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                        dtDeclaration.Rows[rowcount][3] = Convert.ToInt32(ViewState["GeneralRaceID"]);
                        dtDeclaration.Rows[rowcount][4] = Convert.ToInt32(ViewState["GeneralRaceNameID"]);
                        dtDeclaration.Rows[rowcount][5] = Convert.ToInt32(ViewState["DivisionRaceID"]);
                        Label lblHorseNo = (Label)row.FindControl("lblHorseNo");
                        dtDeclaration.Rows[rowcount][6] = Convert.ToInt32(lblHorseNo.Text);
                        dtDeclaration.Rows[rowcount][7] = Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value);
                        dtDeclaration.Rows[rowcount][8] = Convert.ToInt32((row.FindControl("hdnfieldHorseNameID") as HiddenField).Value);
                        dtDeclaration.Rows[rowcount][9] = Convert.ToInt32((row.FindControl("drpdwnOwnerColorCap") as DropDownList).SelectedItem.Value);
                        dtDeclaration.Rows[rowcount][10] = (row.FindControl("txtbxEmergencyOwnerColorG") as TextBox).Text;
                        if ((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][11] = DBNull.Value;
                            dtDeclaration.Rows[rowcount][15] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][11] = new CardsBL().GetProfessionalID(Convert.ToInt32((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value));
                            dtDeclaration.Rows[rowcount][15] = Convert.ToInt32((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value);
                        }
                        dtDeclaration.Rows[rowcount][12] = DateTime.Now;
                        dtDeclaration.Rows[rowcount][13] = 1;
                        dtDeclaration.Rows[rowcount][14] = 1;
                        var lbldja = (row.FindControl("lblDJA") as Label);
                        if (lbldja == null)
                        {
                            dtDeclaration.Rows[rowcount][16] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][16] = (row.FindControl("lblDJA") as Label).Text;
                        }

                        if ((row.FindControl("txtbxCDWG") as TextBox).Text.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][17] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][17] = (row.FindControl("txtbxCDWG") as TextBox).Text;
                        }

                        dtDeclaration.Rows[rowcount][18] = Convert.ToInt32((row.FindControl("drpdwnJockeyChangeReason") as DropDownList).SelectedItem.Value);
                        dtDeclaration.Rows[rowcount][19] = Convert.ToInt32((row.FindControl("drpdwnHorseRunningStatus") as DropDownList).SelectedItem.Value);
                        rowcount++;
                    }
                }

                var result = new CardsBL().AddCardRace(dtDeclaration);
                if (result == 1)
                {
                    var message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var message = "Issue in Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                //  }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void GvShowALL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = GvShowALL.SelectedRow;
                var dataKey = GvShowALL.DataKeys[row.RowIndex];
                var rownumber = string.Empty;
                var professionalnameid = 0;
                var drawno = string.Empty;
                int status = 0;
                var emgercencycolor = string.Empty;
                var professionalnameidG = 0;

                if (dataKey != null)
                {

                    DropDownList drpdwnOwnerColorCap = (DropDownList)row.FindControl("drpdwnOwnerColorCap");
                    HiddenField hdnfieldProfessionalnameid = (HiddenField)row.FindControl("hdnfieldProfessionalnameid1");
                    TextBox txtbxEmergencyOwnerColorG = (TextBox)row.FindControl("txtbxEmergencyOwnerColorG");
                    TextBox txtbxCDWG = (TextBox)row.FindControl("txtbxCDWG");
                    TextBox jkname = row.FindControl("txtbxJockeyNameG") as TextBox;
                    DropDownList drpdwnJockeyChangeReason = (DropDownList)row.FindControl("drpdwnJockeyChangeReason");
                    DropDownList drpdwnHorseRunningStatus = (DropDownList)row.FindControl("drpdwnHorseRunningStatus");


                    emgercencycolor = txtbxEmergencyOwnerColorG.Text;

                    if (!hdnfieldProfessionalnameid.Value.Equals(""))
                    {
                        if (jkname.Text.Equals(""))
                        {
                            professionalnameidG = 0;
                        }
                        else
                        {
                            professionalnameidG = Convert.ToInt32(hdnfieldProfessionalnameid.Value);
                        }
                    }

                    Label lblHorseNo = (Label)row.FindControl("lblHorseNo");
                    HiddenField hdnfieldhorseid = (HiddenField)row.FindControl("hdnfielHorseID");
                    HiddenField hdnfieldhorsenameid = (HiddenField)row.FindControl("hdnfieldHorseNameID");

                    status = new CardsBL().RaceCardUpdate(Convert.ToInt32(dataKey.Value), Convert.ToInt32(drpdwnOwnerColorCap.SelectedItem.Value),
                            emgercencycolor, Convert.ToInt32(professionalnameidG), "RaceCard", txtbxCDWG.Text
                            , Convert.ToInt32(drpdwnJockeyChangeReason.SelectedItem.Value)
                            , Convert.ToInt32(drpdwnHorseRunningStatus.SelectedItem.Value), string.Empty,
                            Convert.ToInt32(lblHorseNo.Text),
                            Convert.ToInt32(hdnfieldhorseid.Value), Convert.ToInt32(hdnfieldhorsenameid.Value));
                }

                if (status == 1)
                {
                    ShowDeclaration();
                    var message = "Record updated successfully.";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string message = "Issue in Record.";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_RaceCard"))
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


                            dtresult = new CardsBL().ImportCardFiles(dt.Rows[count][1].ToString(), dt.Rows[count][2].ToString(), dt.Rows[count][3].ToString(), dt.Rows[count][4].ToString(),
                                 dt.Rows[count][5].ToString(), dt.Rows[count][6].ToString(), dt.Rows[count][7].ToString(), dt.Rows[count][8].ToString(), dt.Rows[count][9].ToString(),
                                 dt.Rows[count][10].ToString(), dt.Rows[count][11].ToString(), dt.Rows[count][12].ToString(), dt.Rows[count][13].ToString(), dt.Rows[count][14].ToString(),
                                 dt.Rows[count][15].ToString(), dt.Rows[count][16].ToString(), dt.Rows[count][17].ToString(), dt.Rows[count][18].ToString(), dt.Rows[count][19].ToString(),
                                 dt.Rows[count][20].ToString(), dt.Rows[count][21].ToString(), dt.Rows[count][22].ToString(), dt.Rows[count][23].ToString(), dt.Rows[count][24].ToString(),
                                 dt.Rows[count][25].ToString(), dt.Rows[count][26].ToString(), dt.Rows[count][27].ToString(), dt.Rows[count][28].ToString(), dt.Rows[count][29].ToString(),
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "Card_RaceCard");


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
                else
                {
                    var message = "No Record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

                //if (dt.Rows.Count > 0)
                //{
                //    var dtErrorResult = new CardsBL().Import30(dt, "Card_RaceCard", 0);
                //    if (dtErrorResult.Rows.Count > 0)
                //    {
                //        using (ExcelPackage xp = new ExcelPackage())
                //        {

                //            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceCard");

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
                //            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceCard.xlsx");
                //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //            Response.BinaryWrite(xp.GetAsByteArray());
                //            Response.End();
                //        }
                //    }
                //    else
                //    {
                //        //BindData();
                //        var message1 = "All Record has been added successfully.";
                //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message1 + "');", true);
                //    }
                //}
                //else
                //{
                //    var message = "No Record Found.";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //}

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
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_RaceCard"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_RaceCard");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_RaceCard.xlsx");
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

        protected void btnHandicapShow_Click(object sender, EventArgs e)
        {
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ClearAll();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
    }
}