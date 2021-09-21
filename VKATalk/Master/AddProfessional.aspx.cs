using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKATalkBusinessLayer;
using System.Globalization;
using System.Data.OleDb;
using System.Data;
using OfficeOpenXml;
using System.IO;
using System.Configuration;

namespace VKATalk.Master
{
    public partial class AddProfessional : System.Web.UI.Page
    {
        private MasterHorseBL Bl = new MasterHorseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["ProfessionalID"] == null)
                        BindDropDown(drpdwnProfessionalType, "ProfessionalType", "ProfessionalType", "ProfessionalTypeID");
                }

                if (Session["ProfessionalID"] != null)
                {
                    this.hdnProfessionalId.Value = Convert.ToString(Session["ProfessionalID"]);
                    this.btnProfessionalName.Text = "Update";
                    BindDropDown(drpdwnProfessionalType, "ProfessionalType", "ProfessionalType", "ProfessionalTypeID");
                    this.MainHorseBind(Session["ProfessionalID"].ToString());
                    this.Session["ProfessionalID"] = null;
                    
                }
               
				lblProfCount.Text = new ProfessionalBL().ProfessionalCount().ToString();
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        {
            DataTable dt = new MasterHorseBL().GetDropdownBind(tablename);
            ddl.DataSource = dt;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            lblProfessionalIDNameID.Text = string.Empty;
            ClearAllSelection(this);
            txtbxProfessionalName.Enabled = true;
            btnProfessionalName.Enabled = true;
            Button8.Enabled = true;
            btnProfile.Enabled = true;
            Button14.Enabled = true;
            Button18.Enabled = true;
            Button3.Enabled = true;
            Button19.Enabled = true;
            Button20.Enabled = true;
            Button2.Enabled = true;
            Button4.Enabled = true;
            Button1.Enabled = true;
            Button5.Enabled = true;
            Button6.Enabled = true;
            Button7.Enabled = true;
            Button9.Enabled = true;
            Button10.Enabled = true;
            Button11.Enabled = true;
            Button12.Enabled = true;
            Button13.Enabled = true;
            Button15.Enabled = true;
            Button23.Enabled = true;
            Button24.Enabled = true;
            btnAdd.Enabled = true;
            Button16.Enabled = true;
            Button17.Enabled = true;
            Button21.Enabled = true;
            DisableAllField(this, "Enable");
            DisableButton("Enable");
            Session["ProfessionalName"] = null;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            DisableAllField(this, "Enable");
            DisableButton("Enable");
        }

        public void DisableButton(string action)
        {
            if (action.Equals("Disable"))
            {
                // btnShow.Enabled = false;
                // btnClear.Enabled = false;
                btnProfessionalName.Enabled = false;
                Button8.Enabled = false;
                btnProfile.Enabled = false;
                Button14.Enabled = false;
                Button18.Enabled = false;
                Button3.Enabled = false;
                Button19.Enabled = false;
                Button20.Enabled = false;
                Button2.Enabled = false;
                Button4.Enabled = false;
                Button1.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button9.Enabled = false;
                Button10.Enabled = false;
                Button11.Enabled = false;
                Button12.Enabled = false;
                Button13.Enabled = false;
                Button15.Enabled = false;
                Button23.Enabled = false;
                Button24.Enabled = false;
                btnAdd.Enabled = false;
                Button16.Enabled = false;
                Button17.Enabled = false;
                Button21.Enabled = false;
            }
            else
            {
                btnProfessionalName.Enabled = true;
                Button8.Enabled = true;
                btnProfile.Enabled = true;
                Button14.Enabled = true;
                Button18.Enabled = true;
                Button3.Enabled = true;
                Button19.Enabled = true;
                Button20.Enabled = true;
                Button2.Enabled = true;
                Button4.Enabled = true;
                Button1.Enabled = true;
                Button5.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
                Button9.Enabled = true;
                Button10.Enabled = true;
                Button11.Enabled = true;
                Button12.Enabled = true;
                Button13.Enabled = true;
                Button15.Enabled = true;
                Button23.Enabled = true;
                Button24.Enabled = true;
                btnAdd.Enabled = true;
                Button16.Enabled = true;
                Button17.Enabled = true;
                Button21.Enabled = true;
            }
        }

        public void DisableAllField(Control parent, string action)
        {
            if (action.Equals("Disable"))
            {
                foreach (Control x in parent.Controls)
                {
                    if ((x.GetType() == typeof(TextBox)))
                    {

                        ((TextBox)(x)).Enabled = false;
                    }

                    if ((x.GetType() == typeof(DropDownList)))
                    {

                        ((DropDownList)(x)).Enabled = false;
                    }

                    if ((x.GetType() == typeof(Label)))
                    {

                        ((Label)(x)).Enabled = false;
                    }
                    if ((x.GetType() == typeof(CheckBox)))
                    {

                        ((CheckBox)(x)).Enabled = false;
                    }
                    if ((x.GetType() == typeof(CheckBoxList)))
                    {

                        ((CheckBoxList)(x)).Enabled = false;
                    }
                    if ((x.GetType() == typeof(RadioButtonList)))
                    {

                        ((RadioButtonList)(x)).Enabled = false;
                    }

                    if (x.HasControls())
                    {
                        DisableAllField(x, "Disable");
                    }
                }
            }
            else
            {
                foreach (Control x in parent.Controls)
                {
                    if ((x.GetType() == typeof(TextBox)))
                    {

                        ((TextBox)(x)).Enabled = true;
                    }

                    if ((x.GetType() == typeof(DropDownList)))
                    {

                        ((DropDownList)(x)).Enabled = true;
                    }

                    if ((x.GetType() == typeof(Label)))
                    {

                        ((Label)(x)).Enabled = true;
                    }
                    if ((x.GetType() == typeof(CheckBox)))
                    {

                        ((CheckBox)(x)).Enabled = true;
                    }
                    if ((x.GetType() == typeof(CheckBoxList)))
                    {

                        ((CheckBoxList)(x)).Enabled = true;
                    }
                    if ((x.GetType() == typeof(RadioButtonList)))
                    {

                        ((RadioButtonList)(x)).Enabled = true;
                    }

                    if (x.HasControls())
                    {
                        DisableAllField(x, "Enable");
                    }
                }
            }
        }

        public void ClearAllSelection(Control parent)
        {
            hdnProfessionalId.Value = "";
            hdnfdProfessionalDetail.Value = "";
            hdnfdProfessionalDoB.Value = "";
            hdnfieldRegNum.Value = "";
            btnProfessionalName.Text = "Add";
            GvProfile.DataSource = new DataTable();
            GvProfile.DataBind();

            GvCenter.DataSource = new DataTable();
            GvCenter.DataBind();

            GvPartner.DataSource = new DataTable();
            GvPartner.DataBind();

            GvJockeyWeight.DataSource = new DataTable();
            GvJockeyWeight.DataBind();

			GvMyObservation.DataSource = new DataTable();
			GvMyObservation.DataBind();

            GvJockeyAllowanceStage.DataSource = new DataTable();
            GvJockeyAllowanceStage.DataBind();

            GvAntiPerson.DataSource = new DataTable();
            GvAntiPerson.DataBind();

            GvProfessionalBackground.DataSource = new DataTable();
            GvProfessionalBackground.DataBind();

            GvHabit.DataSource = new DataTable();
            GvHabit.DataBind();

            GvImportantDates.DataSource = new DataTable();
            GvImportantDates.DataBind();

            GvHomeDistance.DataSource = new DataTable();
            GvHomeDistance.DataBind();


            GvFavDistanceGroup.DataSource = new DataTable();
            GvFavDistanceGroup.DataBind();


            GvHomeClass.DataSource = new DataTable();
            GvHomeClass.DataBind();
            GvFavClassGroup.DataSource = new DataTable();
            GvFavClassGroup.DataBind();

            GvPenalty.DataSource = new DataTable();
            GvPenalty.DataBind();

            GvRelation.DataSource = new DataTable();
            GvRelation.DataBind();

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

                if ((x.GetType() == typeof(Label)))
                {

                    ((Label)(x)).Text = "";
                }
                if ((x.GetType() == typeof(CheckBox)))
                {

                    ((CheckBox)(x)).Text = "";
                }
                if ((x.GetType() == typeof(CheckBoxList)))
                {

                    ((CheckBoxList)(x)).Text = "";
                }
                if ((x.GetType() == typeof(RadioButtonList)))
                {

                    ((RadioButtonList)(x)).SelectedValue = "1";
                }

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }
        }

        protected void btnShow_OnClick(object sender, EventArgs e)
        {
            try
            {

                if (txtbxProfessionalName.Text.Equals(""))
                {
                    var message = "Please select Professional Name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string[] professionalName = txtbxProfessionalName.Text.Split('{');
                    Session["ProfessionalName"] = txtbxProfessionalName.Text;
                    if (professionalName.Length == 1)
                    {
                        var message = "Professional Name not found.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else if (professionalName.Length == 2)
                    {
                        DataTable dt = new ProfessionalBL().GetProfessionalId(professionalName[0], "", "");
                        if (dt.Rows.Count > 0)
                        {
                            hdnProfessionalId.Value = dt.Rows[0][1].ToString();
                            lblProfessionalIDNameID.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString();
                        }


                        btnProfessionalName.Text = "Update";
                        MainHorseBind(hdnProfessionalId.Value);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                    }
                    else
                    {
                        //hdnProfessionalId.Value = Convert.ToString(new ProfessionalBL().GetProfessionalId(professionalName[0], professionalName[1].Substring(0, professionalName[1].Length - 1),professionalName[2].Substring(0, professionalName[2].Length - 1)));

                        DataTable dt = new ProfessionalBL().GetProfessionalId(professionalName[0], professionalName[1].Substring(0, professionalName[1].Length - 1), professionalName[2].Substring(0, professionalName[2].Length - 1));
                        if (dt.Rows.Count > 0)
                        {
                            hdnProfessionalId.Value = dt.Rows[0][1].ToString();
                            lblProfessionalIDNameID.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString();
                        }
                        btnProfessionalName.Text = "Update";
                        MainHorseBind(hdnProfessionalId.Value);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                       // Session["ProfessionalName"] = txtbxProfessionalName.Text;
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
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddProfessionalList(string prefixText, int count)
       {
            DataTable dt = new ProfessionalBL().GetProfessionalNameAutoFiller("ProfessionalName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][1].ToString());
            }
            return horseList;
        }

        protected void MainHorseBind(string horseId)
        {
            try
            {
                DataSet ds = null;
                ds = new ProfessionalBL().GetProfessionalCompleteInformation(Convert.ToInt32(horseId), "MainProfessional");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.hdnfdProfessionalDetail.Value = ds.Tables[0].Rows[0][1].ToString();
                    Session["ProfessionalName"] = hdnfdProfessionalDetail.Value;
                    if (Session["ProfessionalID"] != null)
                    {
                        this.txtbxProfessionalName.Text = ds.Tables[0].Rows[0][1].ToString();
                        lblRegistrationNumber.Text = ds.Tables[0].Rows[0][2].ToString();
                        hdnfieldRegNum.Value = lblRegistrationNumber.Text;
                    }
                    if(!ds.Tables[0].Rows[0][3].ToString().Equals(""))
                    txtbxdob.Text = ds.Tables[0].Rows[0][3].ToString();
                    if (!ds.Tables[0].Rows[0][4].ToString().Equals(""))
                    {
                       // lblprofessionaltype.Text = ds.Tables[0].Rows[0][4].ToString();
                        drpdwnProfessionalType.ClearSelection();
                        drpdwnProfessionalType.Items.FindByText(ds.Tables[0].Rows[0][4].ToString()).Selected = true;
                    }
                    //if (!ds.Tables[0].Rows[0][5].ToString().Equals(""))
                    //txtbxLicenseDate.Text = ds.Tables[0].Rows[0][5].ToString();
                    //if (!ds.Tables[0].Rows[0][6].ToString().Equals(""))
                    //txtbxtrLicenseDate.Text = ds.Tables[0].Rows[0][6].ToString();
                    //if (!ds.Tables[0].Rows[0][7].ToString().Equals(""))
                    //txtbxbodyWeight.Text = ds.Tables[0].Rows[0][7].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.lblProfessionalCurrentName.Text = ds.Tables[1].Rows[0][0].ToString();
                    this.lblProfessionalExName.Text = ds.Tables[1].Rows.Count > 1 ? ds.Tables[1].Rows[1][0].ToString() : "";
                    //this.hdnfdProfessionalDetail.Value = hdnfdProfessionalDetail.Value + "," + ds.Tables[1].Rows[0][0].ToString() + "," +
                    //                         lblProfessionalExName.Text;
                    lblRegistrationNumber.Text = ds.Tables[0].Rows[0][2].ToString();
                    hdnfieldRegNum.Value = lblRegistrationNumber.Text;
                    Session["ProfessionalName"] = hdnfdProfessionalDetail.Value;
                }
                lblOwnerColor.Text = ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0][0].ToString() : "No Record found.";
                lblStatus.Text = ds.Tables[3].Rows.Count > 0 ? ds.Tables[3].Rows[0][0].ToString() : "No Record found.";
                lblCurrentStatus.Text = ds.Tables[4].Rows.Count > 0 ? ds.Tables[4].Rows[0][0].ToString() : "No Record found.";
                lblAprenticeOf.Text = ds.Tables[5].Rows.Count > 0 ? ds.Tables[5].Rows[0][0].ToString() : "No Record found.";
                lblOtherName.Text = ds.Tables[6].Rows.Count > 0 ? ds.Tables[6].Rows[0][0].ToString() : "No Record found.";
                //lblBodyWeight.Text = ds.Tables[7].Rows.Count > 0 ? ds.Tables[7].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[7].Rows.Count > 0)
                {
                    DvJockeyWeight.Visible = true;
                    GvJockeyWeight.DataSource = ds.Tables[7];
                    GvJockeyWeight.DataBind();
                }
                else
                {
                    DvJockeyWeight.Visible = false;
                    GvJockeyWeight.DataSource = new DataTable();
                    GvJockeyWeight.DataBind();
                }

                
                lblAssistanceOf.Text = ds.Tables[8].Rows.Count > 0 ? ds.Tables[8].Rows[0][0].ToString() : "No Record found.";
                //lblPartner.Text = ds.Tables[9].Rows.Count > 0 ? ds.Tables[9].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[9].Rows.Count > 0)
                {
                    DvPartner.Visible = true;
                    GvPartner.DataSource = ds.Tables[9];
                    GvPartner.DataBind();
                }
                else
                {
                    DvPartner.Visible = false;
                    GvPartner.DataSource = new DataTable();
                    GvPartner.DataBind();
                }
                //lblRelation.Text = ds.Tables[10].Rows.Count > 0 ? ds.Tables[10].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[10].Rows.Count > 0)
                {
                    dvRelation.Visible = true;
                    GvRelation.DataSource = ds.Tables[10];
                    GvRelation.DataBind();
                }
                else
                {
                    dvRelation.Visible = false;
                    GvRelation.DataSource = new DataTable();
                    GvRelation.DataBind();
                }

                if (ds.Tables[11].Rows.Count > 0)
                {
                    dvHomeDistance.Visible = true;
                    GvHomeDistance.DataSource = ds.Tables[11];
                    GvHomeDistance.DataBind();
                }
                else
                {
                    dvHomeDistance.Visible = false;
                    GvHomeDistance.DataSource = new DataTable();
                    GvHomeDistance.DataBind();
                }

                //lblHomeDistance.Text = ds.Tables[11].Rows.Count > 0 ? ds.Tables[11].Rows[0][0].ToString() : "No Record found.";
                
                //lblFavDistanceGroup.Text = ds.Tables[12].Rows.Count > 0 ? ds.Tables[12].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[12].Rows.Count > 0)
                {
                    DvFavDistanceGroup.Visible = true;
                    GvFavDistanceGroup.DataSource = ds.Tables[12];
                    GvFavDistanceGroup.DataBind();
                }
                else
                {
                    DvFavDistanceGroup.Visible = false;
                    GvFavDistanceGroup.DataSource = new DataTable();
                    GvFavDistanceGroup.DataBind();
                }

                //lblHomeClass.Text = ds.Tables[13].Rows.Count > 0 ? ds.Tables[13].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[13].Rows.Count > 0)
                {
                    DvHomeClass.Visible = true;
                    GvHomeClass.DataSource = ds.Tables[13];
                    GvHomeClass.DataBind();
                }
                else
                {
                    DvHomeClass.Visible = false;
                    GvHomeClass.DataSource = new DataTable();
                    GvHomeClass.DataBind();
                }
                
                
               // lblFavClassGroup.Text = ds.Tables[14].Rows.Count > 0 ? ds.Tables[14].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[14].Rows.Count > 0)
                {
                    DvFavClassGroup.Visible = true;
                    GvFavClassGroup.DataSource = ds.Tables[14];
                    GvFavClassGroup.DataBind();
                }
                else
                {
                    DvFavClassGroup.Visible = false;
                    GvFavClassGroup.DataSource = new DataTable();
                    GvFavClassGroup.DataBind();
                }
                
                //lblHabit.Text = ds.Tables[15].Rows.Count > 0 ? ds.Tables[15].Rows[0][0].ToString() : "No Record found.";

                if (ds.Tables[15].Rows.Count > 0)
                {
                    dvHabit.Visible = true;
                    GvHabit.DataSource = ds.Tables[15];
                    GvHabit.DataBind();
                }
                else
                {
                    dvHabit.Visible = false;
                    GvHabit.DataSource = new DataTable();
                    GvHabit.DataBind();
                }

                //lblImportantDates.Text = ds.Tables[16].Rows.Count > 0 ? ds.Tables[16].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[16].Rows.Count > 0)
                {
                    dvImportantDates.Visible = true;
                    GvImportantDates.DataSource = ds.Tables[16];
                    GvImportantDates.DataBind();
                }
                else
                {
                    dvImportantDates.Visible = false;
                    GvImportantDates.DataSource = new DataTable();
                    GvImportantDates.DataBind();
                }

				if (ds.Tables[17].Rows.Count > 0)
				{
					dvMyObservation.Visible = true;
					GvMyObservation.DataSource = ds.Tables[17];
					GvMyObservation.DataBind();
				}
				else
				{
					dvMyObservation.Visible = false;
					GvMyObservation.DataSource = new DataTable();
					GvMyObservation.DataBind();
				}

				//lblMyObservations.Text = ds.Tables[17].Rows.Count > 0 ? ds.Tables[17].Rows[0][0].ToString() : "No Record found.";
                
                //lblProfessionalProfile.Text = ds.Tables[18].Rows.Count > 0 ? ds.Tables[18].Rows[0][0].ToString() : "No Record found.";
                dvProfile.Visible = true;
                GvProfile.DataSource = ds.Tables[18];
                GvProfile.DataBind();

                //lblBaseCenter.Text = ds.Tables[19].Rows.Count > 0 ? ds.Tables[19].Rows[0][0].ToString() : "No Record found.";
                divCenter.Visible = true;
                GvCenter.DataSource = ds.Tables[19];
                GvCenter.DataBind();

                lblReligion.Text = ds.Tables[20].Rows.Count > 0 ? ds.Tables[20].Rows[0][0].ToString() : "No Record found.";
                
                //lblAntiPerson.Text = ds.Tables[21].Rows.Count > 0 ? ds.Tables[21].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[21].Rows.Count > 0)
                {
                    dvAntiPerson.Visible = true;
                    GvAntiPerson.DataSource = ds.Tables[21];
                    GvAntiPerson.DataBind();
                }
                else
                {
                    dvAntiPerson.Visible = false;
                    GvAntiPerson.DataSource = new DataTable();
                    GvAntiPerson.DataBind();
                }

                //lblProfessionalBackground.Text = ds.Tables[22].Rows.Count > 0 ? ds.Tables[22].Rows[0][0].ToString() : "No Record found.";


                if (ds.Tables[22].Rows.Count > 0)
                {
                    dvProfessionalBackground.Visible = true;
                    GvProfessionalBackground.DataSource = ds.Tables[22];
                    GvProfessionalBackground.DataBind();
                }
                else
                {
                    dvProfessionalBackground.Visible = false;
                    GvProfessionalBackground.DataSource = new DataTable();
                    GvProfessionalBackground.DataBind();
                }

                if (ds.Tables[23].Rows.Count > 0)
                {
                    dvJockeyAllowanceStage.Visible = true;
                    GvJockeyAllowanceStage.DataSource = ds.Tables[23];
                    GvJockeyAllowanceStage.DataBind();
                }
                else
                {
                    dvJockeyAllowanceStage.Visible = false;
                    GvJockeyAllowanceStage.DataSource = new DataTable();
                    GvJockeyAllowanceStage.DataBind();
                }

                if (ds.Tables[24].Rows.Count > 0)
                {
                    dvpenalty.Visible = true;
                    GvPenalty.DataSource = ds.Tables[24];
                    GvPenalty.DataBind();
                }
                else
                {
                    dvpenalty.Visible = false;
                    GvPenalty.DataSource = new DataTable();
                    GvPenalty.DataBind();
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        /// <summary>
        ///  Current Mission
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void btnCurrentMission_Click(object sender, EventArgs e)
        {
            string message = "CurrentMission";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "dialogCurrentMission('" + message + "');", true);
        }


        protected void BtnAddClick(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void SaveData()
        {
            try
            {
                if (txtbxProfessionalName.Text == "")
                {
                    string message = "Please select Professional Name";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    int status = 0;
                    status = new ProfessionalBL().ProfessionalCompleteInformation(
                                Convert.ToInt32(hdnProfessionalId.Value),
                                (txtbxdob.Text.Equals("")) ? "__-__-____" : txtbxdob.Text,
                                "__-__-____",
                               "__-__-____",
                               string.Empty,
                                1,
                                "Insert",
                                drpdwnProfessionalType.SelectedItem.Value);

                    if (status == 2)
                    {
                        string message = "Duplicate Record.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAllSelection(this);
                        //BindData();
                    }
                    else if (status > 0)
                    {
                        string message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                        //ClearAllSelection(this);
                        //BindData();
                    }
                    else
                    {
                        string message = "Incorrect Information.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnHorseShow_Click(object sender, EventArgs e)
        {
        }

       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;

                status = new ProfessionalBL().ProfessionalCompleteInformation(
                            Convert.ToInt32(hdnProfessionalId.Value),
                            (txtbxdob.Text.Equals("")) ? "__-__-____" : txtbxdob.Text,
                            "__-__-____",
                           "__-__-____",
                           string.Empty,
                            1,
                            "Delete",
                            drpdwnProfessionalType.SelectedItem.Value);

                if (status == 2)
                {
                    string message = "Duplicate Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);

                    ClearAllSelection(this);
                }
                else if (status == 3)
                {
                    string message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);

                    ClearAllSelection(this);
                }
                else
                {
                    string message = "Incorrect Information.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Professional"))
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
                    var dtErrorResult = new ProfessionalBL().Import30(dt, "MainProfessional");
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Issue in few record. Please check the XL sheet');", true);
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = new ProfessionalBL().GetExport(0, "MainProfessional"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //dt.Columns.Remove("ProfessionalNameID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Professional");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Professional.xlsx");
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

        protected void btnExportToday_Click(object sender, EventArgs e)
        {
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //Session["ProfessionalName"] = txtbxProfessionalName.Text;
           // Session.Abandon("ProfessionalName");
           // Session.Remove("ProfessionalName");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
     
    }
}