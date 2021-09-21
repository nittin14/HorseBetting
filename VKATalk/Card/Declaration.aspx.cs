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

    public partial class Declaration : System.Web.UI.Page
    {
        private int rownumber = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtbxDeclarationEnterDate.Text = CommonMethods.CurrentDate();
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

        protected void txtbxJockeyNameG_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as TextBox).NamingContainer;
                HiddenField hdnfieldProfessionalNameid = row.FindControl("hdnfieldProfessionalNameid1") as HiddenField;
                HiddenField hdnfieldDivisionRaceID = row.FindControl("hdnfieldDivisionRaceID") as HiddenField;
                if (hdnfieldProfessionalNameid != null)
                {
                    Label lblDJA = row.FindControl("lblDJA") as Label;
                    Label lblACCEPTANCEWEIGHTGBC = row.FindControl("lblACCEPTANCEWEIGHTGBC") as Label;
                    TextBox txtbxDeclareWeightG = row.FindControl("txtbxDeclareWeightG") as TextBox;

                    DataTable dt = new CardsBL().GetCardDJA(Convert.ToInt32(hdnfieldProfessionalNameid.Value),
                                       txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), Convert.ToInt32(hdnfieldDivisionRaceID.Value));
                    if (dt.Rows.Count > 0 && !dt.Rows[0][0].ToString().Equals(""))
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
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void drpdwnCenterName_SelectIndexChange(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                var dt = new CardsBL().GetAcceptanceDivisionDetail(
                    txtbxDivisionRaceDate.Text,
                    Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), "Declaration");
                if (dt.Rows.Count > 0)
                {
                    lblSeason.Text = dt.Rows[0][10].ToString();
                    lblYear.Text = dt.Rows[0][11].ToString();
                    dvgridview.Visible = true;
                    grdvwRaceDetail.DataSource = dt;
                    grdvwRaceDetail.DataBind();

                    lblEntryDate.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }



        protected void ClearAll()
        {
            lblSeason.Text = string.Empty;
            lblYear.Text = string.Empty;
            lblEntryDate.Text = string.Empty;
            lblGeneralRaceName.Text = string.Empty;
            GvShowALL.DataSource = new DataTable();
            GvShowALL.DataBind();
            if (Convert.ToInt32(drpdwnCenterName.SelectedItem.Value).Equals(-1))
            {
                grdvwRaceDetail.DataSource = new DataTable();
                grdvwRaceDetail.DataBind();

                GrdVwDeclarationDisplay.DataSource = new DataTable();
                GrdVwDeclarationDisplay.DataBind();

            }
        }


        protected void grdvwRaceDetail_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //BtnSubmit.Text = "Update";
                GridViewRow row = grdvwRaceDetail.SelectedRow;
                HiddenField hdnfielddivisionracename = (HiddenField)row.FindControl("hdnfielddivisionracename");
                HiddenField hdnfieldGeneralRaceNameID = (HiddenField)row.FindControl("hdnfieldGeneralRaceNameID");
                HiddenField hdnfieldGeneralRaceID = (HiddenField)row.FindControl("hdnfieldGeneralRaceID");
                var dataKey = grdvwRaceDetail.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ViewState["DivisionRaceID"] = dataKey.Value; //generalraceid
                    ViewState["DivisionRaceName"] = hdnfielddivisionracename.Value;//reneralracename
                    ViewState["GeneralRaceNameID"] = hdnfieldGeneralRaceNameID.Value;
                    ViewState["GeneralRaceID"] = hdnfieldGeneralRaceID.Value;
                    //hdnfieldGeneralRaceNameID.Value = Convert.ToString(dataKey.Value);
                    lblGeneralRaceName.Text = ViewState["DivisionRaceName"].ToString() + " (" + ViewState["DivisionRaceID"].ToString() + ")";

                }

                var dsGeneralDate = new CardsBL().GetEntryDateInformation(Convert.ToInt32(ViewState["GeneralRaceNameID"]), "Declaration", lblSeason.Text, lblYear.Text);
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
                }
                else
                {
                    lblEntryDate.Text = string.Empty;
                }

                ShowDeclaration();

                foreach (GridViewRow gvRow in GvShowALL.Rows)
                {

                    AjaxControlToolkit.AutoCompleteExtender AutoCompleteExtender1
                     = (AjaxControlToolkit.AutoCompleteExtender)gvRow.FindControl("AutoCompleteExtender3");

                    AutoCompleteExtender1.ContextKey = 0 + ',' + 0 + ',' + 0 + ',' + 0 + ',' + 0 + ','
                        + txtbxDivisionRaceDate.Text;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        protected void GvShowALL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Cells[10].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");

                    //DropDownList DropDownList1 = (e.Row.FindControl("drpdwnShoe") as DropDownList);
                    //DropDownList1.DataSource = new DataTable();
                    //DropDownList1.DataBind();

                    //BindDropDown(DropDownList1, "MasterShoe", "Shoe", "ShoeMID");
                    //DropDownList1.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    //DropDownList drpdwnShoeMetal = (e.Row.FindControl("drpdwnShoeMetal") as DropDownList);
                    //BindDropDown(drpdwnShoeMetal, "MasterShoeMetal", "ShoeMetal", "ShoeMetalMID");
                    //drpdwnShoeMetal.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                    HiddenField hdnfieldProfessionalNameid1 = (e.Row.FindControl("hdnfieldProfessionalNameid1") as HiddenField);


                    DataRowView dr = e.Row.DataItem as DataRowView;

                    if (!dr["JockeyNameID"].ToString().Equals(""))
                        hdnfieldProfessionalNameid1.Value = dr["JockeyNameID"].ToString();

                    //if (!dr["Shoe"].ToString().Equals(""))
                    //    DropDownList1.Items.FindByText(dr["Shoe"].ToString()).Selected = true;

                    //if (!dr["ShoeMetal"].ToString().Equals(""))
                    //    drpdwnShoeMetal.Items.FindByText(dr["ShoeMetal"].ToString()).Selected = true;

                }

            }
            catch (Exception ex)
            {
                //listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        private void ShowDeclaration()
        {
            var dt = new CardsBL().GetDeclaration(Convert.ToInt32(ViewState["DivisionRaceID"]), Convert.ToInt32(ViewState["GeneralRaceNameID"]), ViewState["DivisionRaceName"].ToString(), "Declaration", txtbxDivisionRaceDate.Text, Convert.ToInt32(drpdwnCenterName.SelectedItem.Value));
            if (dt.Tables[0].Rows.Count > 0)
            {
                GvShowALL.DataSource = dt.Tables[0];
                GvShowALL.DataBind();
            }
            else
            {
                GvShowALL.DataSource = new DataTable();
                GvShowALL.DataBind();
            }

            if (dt.Tables[1].Rows.Count > 0)
            {
                GrdVwDeclarationDisplay.DataSource = dt.Tables[1];
                GrdVwDeclarationDisplay.DataBind();
            }
            else
            {
                GrdVwDeclarationDisplay.DataSource = new DataTable();
                GrdVwDeclarationDisplay.DataBind();
            }
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
                //                        Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), lblSeason.Text, lblYear.Text, "Declaration", Convert.ToInt32(ViewState["DivisionRaceID"]));
                //if (status.Rows.Count > 0)
                //{
                //	var message = "Records already exists.";
                //	ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                //}
                //else
                //{
                dtDeclaration.Columns.Add("DataEntryDate", typeof(string));
                dtDeclaration.Columns.Add("DivisionRaceDate", typeof(string));
                dtDeclaration.Columns.Add("CenterMID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceID", typeof(int));
                dtDeclaration.Columns.Add("GeneralRaceNameID", typeof(int));
                dtDeclaration.Columns.Add("DivisionRaceID", typeof(int));
                dtDeclaration.Columns.Add("HorseNo", typeof(int));
                dtDeclaration.Columns.Add("HorseID", typeof(int));
                dtDeclaration.Columns.Add("HorseNameID", typeof(int));
                dtDeclaration.Columns.Add("JockeyID", typeof(int));//9
                dtDeclaration.Columns.Add("DrawNo", typeof(int));
                dtDeclaration.Columns.Add("CreatedDate", typeof(DateTime));
                dtDeclaration.Columns.Add("CreatedUserID", typeof(int));
                dtDeclaration.Columns.Add("IsActive", typeof(int));
                dtDeclaration.Columns.Add("ShoeMID", typeof(int));//14
                dtDeclaration.Columns.Add("ShoeMetalMID", typeof(int));
                dtDeclaration.Columns.Add("JockeyNamePID", typeof(int));
                dtDeclaration.Columns.Add("DeclareJockeyAllowance", typeof(string));//17
                dtDeclaration.Columns.Add("DeclareWeight", typeof(string));
                dtDeclaration.Columns.Add("HorseBandageID", typeof(int));
                var breakgbc = 0;
                int rowcount = 0;
                foreach (GridViewRow row in GvShowALL.Rows)
                {
                    DataTable status = new CardsBL().GetDeclareOldRecordStatus(Convert.ToInt32(ViewState["GeneralRaceNameID"]), txtbxDivisionRaceDate.Text,
                                            Convert.ToInt32(drpdwnCenterName.SelectedItem.Value), lblSeason.Text, lblYear.Text, "Declaration", 
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
                            //dtDeclaration.Rows[rowcount][1] = Convert.ToDateTime(dtformat);
                        }

                        dtDeclaration.Rows[rowcount][2] = Convert.ToInt32(drpdwnCenterName.SelectedItem.Value);
                        dtDeclaration.Rows[rowcount][3] = Convert.ToInt32(ViewState["GeneralRaceID"]);
                        dtDeclaration.Rows[rowcount][4] = Convert.ToInt32(ViewState["GeneralRaceNameID"]);
                        dtDeclaration.Rows[rowcount][5] = Convert.ToInt32(ViewState["DivisionRaceID"]);
                        Label lblHorseNo = (Label)row.FindControl("lblHorseNo");
                        dtDeclaration.Rows[rowcount][6] = Convert.ToInt32(lblHorseNo.Text);
                        dtDeclaration.Rows[rowcount][7] = Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value);
                        dtDeclaration.Rows[rowcount][8] = Convert.ToInt32((row.FindControl("hdnfieldHorseNameID") as HiddenField).Value);
                        if ((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value.Equals(""))
                        {
                            breakgbc = 1;
                            break;
                            //dtDeclaration.Rows[rowcount][9] = DBNull.Value;
                        }
                        else
                        {
                            breakgbc = 0;
                            dtDeclaration.Rows[rowcount][9] = new CardsBL().GetProfessionalID(Convert.ToInt32((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value));
                        }
                        if ((row.FindControl("txtbxDrawNoG") as TextBox).Text.Equals(""))
                        {
                            breakgbc = 1;
                            break;
                        }
                        else
                        {
                            breakgbc = 0;
                            dtDeclaration.Rows[rowcount][10] = Convert.ToInt32((row.FindControl("txtbxDrawNoG") as TextBox).Text);
                        }

                        dtDeclaration.Rows[rowcount][11] = DateTime.Now;
                        dtDeclaration.Rows[rowcount][12] = 1;
                        dtDeclaration.Rows[rowcount][13] = 1;
                        if ((row.FindControl("hdnfieldShoeID") as HiddenField).Value.Equals(""))
                        {
                            breakgbc = 1;
                            break;
                        }
                        else
                        {
                            breakgbc = 0;
                            dtDeclaration.Rows[rowcount][14] = Convert.ToInt32((row.FindControl("hdnfieldShoeID") as HiddenField).Value);
                        }
                        if ((row.FindControl("hdnfieldShoeMetalID") as HiddenField).Value.Equals(""))
                        {
                            breakgbc = 1;
                            break;
                        }
                        else
                        {
                            breakgbc = 0;
                            dtDeclaration.Rows[rowcount][15] = Convert.ToInt32((row.FindControl("hdnfieldShoeMetalID") as HiddenField).Value);
                        }


                        if ((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value.Equals(""))
                        {
                            breakgbc = 1;
                            break;
                        }
                        else
                        {
                            breakgbc = 0;
                            dtDeclaration.Rows[rowcount][16] = Convert.ToInt32((row.FindControl("hdnfieldProfessionalNameid1") as HiddenField).Value);
                        }

                        var lbldja = (row.FindControl("lblDJA") as Label);
                        if (lbldja == null)
                        {
                            dtDeclaration.Rows[rowcount][17] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][17] = (row.FindControl("lblDJA") as Label).Text;
                        }

                        if ((row.FindControl("txtbxDeclareWeightG") as TextBox).Text.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][18] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][18] = (row.FindControl("txtbxDeclareWeightG") as TextBox).Text;
                        }
                        if ((row.FindControl("hdnfieldHorseBandageID") as HiddenField).Value.Equals(""))
                        {
                            dtDeclaration.Rows[rowcount][19] = DBNull.Value;
                        }
                        else
                        {
                            dtDeclaration.Rows[rowcount][19] = Convert.ToInt32((row.FindControl("hdnfieldHorseBandageID") as HiddenField).Value);
                        }

                        

                        rowcount++;

                        new CardsBL().InsertDeclarationBitEquipemnt(Convert.ToInt32(ViewState["DivisionRaceID"]),
                             Convert.ToInt32((row.FindControl("hdnfielHorseID") as HiddenField).Value),
                              Convert.ToInt32((row.FindControl("hdnfieldHorseNameID") as HiddenField).Value),
                              (row.FindControl("lblBitAlias") as Label).Text,
                              (row.FindControl("lblEquipmentAlias") as Label).Text,"Declaration"
                              );
                    }

                   
                }


                if (breakgbc == 1)
                {
                    dtDeclaration.Dispose();
                    var message = "Please fill all compulsary fields.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    var result = new CardsBL().AddDeclaration(dtDeclaration);
                    if (result == 1)
                    {
                        dtDeclaration.Dispose();
                        GvShowALL.DataSource = new DataTable();
                        GvShowALL.DataBind();
                        ShowDeclaration();
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        dtDeclaration.Dispose();
                        var message = "Issue in Record.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                //}
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
                var horsebandageid = 0;
                //var shoeid = string.Empty;
                int status = 0;
                if (dataKey != null)
                {
                    HiddenField hdnfieldProfessionalNameID = (HiddenField)row.FindControl("hdnfieldProfessionalNameid1");
                    professionalnameid = Convert.ToInt32(hdnfieldProfessionalNameID.Value);


                    var declareweight = string.Empty;
                    if (!(row.FindControl("txtbxDeclareWeightG") as TextBox).Text.Equals(""))
                    {
                        TextBox txtbxdeclareweight = (TextBox)row.FindControl("txtbxDeclareWeightG");
                        declareweight = txtbxdeclareweight.Text;
                    }
                    else
                    {
                        declareweight = "";
                    }


                    var drawno = string.Empty;
                    if (!(row.FindControl("txtbxDrawNoG") as TextBox).Text.Equals(""))
                    {
                        TextBox txtbxDrawNoG = (TextBox)row.FindControl("txtbxDrawNoG");
                        drawno = txtbxDrawNoG.Text;
                    }
                    else
                    {
                        drawno = "";
                    }

                    var dja = string.Empty;
                    if (!(row.FindControl("lblDJA") as Label).Text.Equals(""))
                    {
                        Label lbldjaG = (Label)row.FindControl("lblDJA");
                        dja = lbldjaG.Text;
                    }
                    else
                    {
                        dja = "";
                    }

                    HiddenField hdnfieldShoeID = (HiddenField)row.FindControl("hdnfieldShoeID");
                    HiddenField hdnfieldShoeMetalID = (HiddenField)row.FindControl("hdnfieldShoeMetalID");
                    Label lblHorseNo = (Label)row.FindControl("lblHorseNo");
                    HiddenField hdnfieldhorseid = (HiddenField)row.FindControl("hdnfielHorseID");
                    HiddenField hdnfieldhorsenameid = (HiddenField)row.FindControl("hdnfieldHorseNameID");

                    Label lblEquipmentAlias = (Label)row.FindControl("lblEquipmentAlias");
                    Label lblBitAlias = (Label)row.FindControl("lblBitAlias");
                    HiddenField hdnfieldHorseBandageID = (HiddenField)row.FindControl("hdnfieldHorseBandageID");
                    if (!hdnfieldHorseBandageID.Value.Equals(""))
                    {
                        horsebandageid = Convert.ToInt32(hdnfieldHorseBandageID.Value);
                    }

                    if (hdnfieldShoeID.Value.Equals("") || hdnfieldShoeMetalID.Value.Equals(""))
                    {
                        status = 2;
                    }
                    else
                    {
                        status = new CardsBL().DeclarationUpdate(Convert.ToInt32(dataKey.Value), Convert.ToInt32(professionalnameid),
                        Convert.ToInt32(drawno), Convert.ToInt32(hdnfieldShoeID.Value), Convert.ToInt32(hdnfieldShoeMetalID.Value), declareweight, dja, Convert.ToInt32(lblHorseNo.Text),
                        Convert.ToInt32(hdnfieldhorseid.Value), Convert.ToInt32(hdnfieldhorsenameid.Value),
                        lblBitAlias.Text, lblEquipmentAlias.Text, horsebandageid
                        );
                    }
                    

                }

                if (status == 1)
                {
                    ShowDeclaration();
                    string message = "Record updated successfully.";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else if (status == 2)
                {
                    string message = "Shoe & ShoeMetal should not be blank.";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string message = "Issue in Record.";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Card_Declaration"))
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
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "Card_Declaration");


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
                using (DataSet ds = new CardsBL().GetExport("__-__-____", 0, "Card_Declaration"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //  dt.Columns.Remove("ProfessionalCurrentStatusID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Card_Declaration");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Card_Declaration.xlsx");
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