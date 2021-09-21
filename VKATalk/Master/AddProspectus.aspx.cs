using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Globalization;
using OfficeOpenXml;

namespace VKATalk.Master
{
    using System.Data;

    using VKATalkBusinessLayer;

    public partial class AddProspectus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
				lblGeneralRacecount.Text = Convert.ToString(new ProspectusBL().GetProspectusGeneralLastId("ProspectusMaster"));
				//if (!IsPostBack)
    //            {

    //                BindDropDown(drpdwnAbbriviation, "RaceAbbreviation", "RaceAbbreviation", "RaceAbbreviationMID");
    //                drpdwnAbbriviation.Items.Insert(0, new ListItem("-- Please select --", "-1"));
    //            }

                if (Session["ProspectusID"] != null)
                {
                    this.hdnfieldprospectusid.Value = Convert.ToString(Session["ProspectusID"]);
                    btnProspectusName.Text = "Update";
                   // BindDropDown(drpdwnAbbriviation, "RaceAbbreviation", "RaceAbbreviation", "RaceAbbreviationMID");
                   // drpdwnAbbriviation.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                    this.MainProspectusBind(Session["ProspectusID"].ToString());
                    this.Session["ProspectusID"] = null;
                }

               
             
            }
            catch (Exception ex)
            {
                var message = "Incorrect Information";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        private void BindDropDown(DropDownList ddl, String TableName_, string TextField, String ValueField)
        {
            DataTable dt;
            //dt = new MasterHorseBL().GetDropdownBind(TableName_);
            dt = new ProspectusBL().GetDropdownBind(TableName_);
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
        }

        protected void MainProspectusBind(string prospectusId)
        {
            try
            {
           
            DataSet ds = null;
                ds = new ProspectusBL().GetProspectusCompleteInformation(Convert.ToInt32(prospectusId), "MainProspectus");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Session["ProspectusID"] != null)
                    {
                        this.txtbxProspectusName.Text = ds.Tables[0].Rows[0][1].ToString();
                    }
                    //this.txtbxAbbreviation.Text = ds.Tables[0].Rows[0][2].ToString();
                    if (!ds.Tables[0].Rows[0][2].ToString().Equals(""))
                    {
                       // drpdwnAbbriviation.ClearSelection();
                        //BindDropDown(drpdwnAbbriviation, "RaceAbbreviation", "RaceAbbreviation", "RaceAbbreviationMID");
                        //drpdwnAbbriviation.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        string itemToCompare = string.Empty;
                       // string itemOrigin = "test";
                        //foreach (ListItem item in drpdwnAbbriviation.Items)
                        //{
                        //    itemToCompare = item.Text;
                        //    if (ds.Tables[0].Rows[0][2].ToString() == itemToCompare)
                        //    {
                        //        drpdwnAbbriviation.ClearSelection();
                        //        item.Selected = true;
                        //    }
                        //}

                       // drpdwnAbbriviation.Items.FindByText(ds.Tables[0].Rows[0][2].ToString()).Selected = true;
                    }
                    //if (ds.Tables[0].Rows[0][3].ToString() != null && ds.Tables[0].Rows[0][3].ToString() != "")
                    //rdbtnForeignHorsesTerm.Items.FindByText(ds.Tables[0].Rows[0][3].ToString()).Selected = true;
                    if (ds.Tables[0].Rows[0][3].ToString() != null && ds.Tables[0].Rows[0][3].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0][3].ToString().Equals("true"))
                        {
                            chkbxMaidenTerm.Checked = true;
                        }
                        else
                        {
                            chkbxMaidenTerm.Checked = false;
                        }
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    //this.lblProspectusCurrentName.Text = ds.Tables[1].Rows[0][0].ToString();
                    //this.lblProspectusExName.Text = ds.Tables[1].Rows.Count > 1 ? ds.Tables[1].Rows[1][0].ToString() : "";
                    dvGdvwMasterRaceName.Visible = true;
                    GvMasterRaceName.DataSource = ds.Tables[1];
                    GvMasterRaceName.DataBind();
                    var count = ds.Tables[1].Rows.Count;
                    var prospectusExName = ds.Tables[1].Rows.Count > 1 ? ds.Tables[1].Rows[count-1][0].ToString() : "";
                    this.hdnfieldRaceName.Value = Convert.ToString(ds.Tables[1].Rows[0][0] + "," + prospectusExName);
                    Session["ProspectusMasterRaceName"]=hdnfieldRaceName.Value;
                    this.lblCenterName.Text = (ds.Tables[1].Rows[0][1].ToString().Replace("(","")).Replace(")","").Trim();
                    this.lblSeasonName.Text = (ds.Tables[1].Rows[0][2].ToString().Replace("(", "")).Replace(")", "").Trim();
                }
                this.lblRaceInMemoryOf.Text = ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0][0].ToString() : "No Record found.";
                //this.lblSponceroftheRace.Text = ds.Tables[3].Rows.Count > 0 ? ds.Tables[3].Rows[0][0].ToString() : "No Record found.";
                dvsponcer.Visible = true;
                GvSponcer.DataSource = ds.Tables[3];
                GvSponcer.DataBind();

				//this.lblMomenttoPresenter.Text = ds.Tables[4].Rows.Count > 0 ? ds.Tables[4].Rows[0][0].ToString() : "No Record found.";
				if (ds.Tables[4].Rows.Count > 0)
				{
					dvmomenttopresenter.Visible = true;
					GvMomenttopresenter.DataSource = ds.Tables[4];
					GvMomenttopresenter.DataBind();
				}
				else
				{
					dvmomenttopresenter.Visible = false;
					GvMomenttopresenter.DataSource = new DataTable();
					GvMomenttopresenter.DataBind();
				}

				this.lblDistance.Text = ds.Tables[5].Rows.Count > 0 ? ds.Tables[5].Rows[0][0].ToString() : "No Record found.";
                this.lblRaceType.Text = ds.Tables[6].Rows.Count > 0 ? ds.Tables[6].Rows[0][0].ToString() : "No Record found.";
                this.lblHandicapRatingRange.Text = ds.Tables[7].Rows.Count > 0 ? ds.Tables[7].Rows[0][0].ToString() : "No Record found.";
                this.lblEligbleHandicapRatingRange.Text = ds.Tables[8].Rows.Count > 0 ? ds.Tables[8].Rows[0][0].ToString() : "No Record found.";
                this.lblAgeCondition.Text = ds.Tables[9].Rows.Count > 0 ? ds.Tables[9].Rows[0][0].ToString() : "No Record found.";
                this.lblRaceStatus.Text = ds.Tables[10].Rows.Count > 0 ? ds.Tables[10].Rows[0][0].ToString() : "No Record found.";
                this.lblMillion.Text = ds.Tables[11].Rows.Count > 0 ? ds.Tables[11].Rows[0][0].ToString() : "No Record found.";
                this.lblSweepStake.Text = ds.Tables[12].Rows.Count > 0 ? ds.Tables[12].Rows[0][0].ToString() : "No Record found.";
                this.lblClassic.Text = ds.Tables[13].Rows.Count > 0 ? ds.Tables[13].Rows[0][0].ToString() : "No Record found.";
                this.lblGraded.Text = ds.Tables[14].Rows.Count > 0 ? ds.Tables[14].Rows[0][0].ToString() : "No Record found.";
                this.lblGradedNo.Text = ds.Tables[15].Rows.Count > 0 ? ds.Tables[15].Rows[0][0].ToString() : "No Record found.";
                this.lblMomenttoType.Text = ds.Tables[16].Rows.Count > 0 ? ds.Tables[16].Rows[0][0].ToString() : "No Record found.";
               // this.lblMomenttoCost.Text = ds.Tables[17].Rows.Count > 0 ? ds.Tables[17].Rows[0][0].ToString() : "No Record found.";
                //this.lblProfessionalBackground.Text = ds.Tables[18].Rows.Count > 0 ? ds.Tables[18].Rows[0][0].ToString() : "No Record found.";
                divInterestedprofessionBackground.Visible = true;
                GvInterestedProfessionBackground.DataSource = ds.Tables[18];
                GvInterestedProfessionBackground.DataBind();

                //lblHWPCondition.Text = ds.Tables[19].Rows.Count > 0 ? ds.Tables[19].Rows[0][0].ToString() : "No Record found.";
                dvHWPCondition.Visible = true;
                GVHWPCondition.DataSource = ds.Tables[19];
                GVHWPCondition.DataBind();

                lblBunchCondition.Text = ds.Tables[20].Rows.Count > 0 ? ds.Tables[20].Rows[0][0].ToString() : "No Record found.";
				// lblPermanentCondition.Text = ds.Tables[21].Rows.Count > 0 ? ds.Tables[21].Rows[0][0].ToString() : "No Record found.";
				if (ds.Tables[21].Rows.Count > 0)
				{
					DvPermanentCondition.Visible = true;
					GvPermanentCondition.DataSource = ds.Tables[21];
					GvPermanentCondition.DataBind();
				}
				else
				{
					DvPermanentCondition.Visible = false;
					GvPermanentCondition.DataSource = new DataTable();
					GvPermanentCondition.DataBind();
				}

				//lblHandicapWeight.Text = ds.Tables[22].Rows.Count > 0 ? ds.Tables[22].Rows[0][0].ToString() : "No Record found.";
				dvhandicapweightaspergender.Visible = true;
                GvHandicapWeightAsperGender.DataSource = ds.Tables[22];
                GvHandicapWeightAsperGender.DataBind();

                if (ds.Tables[23].Rows.Count > 0)
                {
                    DvWeightAsPerAge.Visible = true;
                    GvWeightAsPerAge.DataSource = ds.Tables[23];
                    GvWeightAsPerAge.DataBind();
                }
                else
                {
                    DvWeightAsPerAge.Visible = false;
                    GvWeightAsPerAge.DataSource = new DataTable();
                    GvWeightAsPerAge.DataBind();
                }

                //lblRaceHistory.Text = ds.Tables[24].Rows.Count > 0 ? ds.Tables[24].Rows[0][0].ToString() : "No Record found.";

                if (ds.Tables[24].Rows.Count > 0)
                {
                    DvRaceHistory.Visible = true;
                    GvRaceHistory.DataSource = ds.Tables[24];
                    GvRaceHistory.DataBind();
                }
                else
                {
                    DvRaceHistory.Visible = false;
                    GvRaceHistory.DataSource = new DataTable();
                    GvRaceHistory.DataBind();
                }

				//lblOtherCondition.Text = ds.Tables[25].Rows.Count > 0 ? ds.Tables[25].Rows[0][0].ToString() : "No Record found.";

				if (ds.Tables[25].Rows.Count > 0)
				{
					DvotherCondition.Visible = true;
					GvOtherCondition.DataSource = ds.Tables[25];
					GvOtherCondition.DataBind();
				}
				else
				{
					DvotherCondition.Visible = false;
					GvOtherCondition.DataSource = new DataTable();
					GvOtherCondition.DataBind();
				}

				if (ds.Tables[26].Rows.Count > 0)
                {
                    DvStakeMoneyAddition.Visible = true;
                    GvStakeMoneyAddition.DataSource = ds.Tables[26];
                    GvStakeMoneyAddition.DataBind();
                }
                else
                {
                    DvStakeMoneyAddition.Visible = false;
                    GvStakeMoneyAddition.DataSource = new DataTable();
                    GvStakeMoneyAddition.DataBind();
                }

                if (ds.Tables[27].Rows.Count > 0)
                {
                    gdvwmasterbunch.DataSource = ds.Tables[27];
                    gdvwmasterbunch.DataBind();
                }
                else
                {
                    gdvwmasterbunch.DataSource = new DataTable();
                    gdvwmasterbunch.DataBind();
                }

                if (ds.Tables[28].Rows.Count > 0)
                {
                    dvGvRaceAbbriviation.Visible = true;
                    GvRaceAbbriviation.DataSource = ds.Tables[28];
                    GvRaceAbbriviation.DataBind();
                }
                else
                {
                    dvGvRaceAbbriviation.Visible = false;
                    GvRaceAbbriviation.DataSource = new DataTable();
                    GvRaceAbbriviation.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }

        //private void BindDropDown(DropDownList ddl, String tablename, string textfield, String valuefield)
        //{
        //    DataTable dt = new ProspectusBL().GetDropdownBind(tablename);
        //    ddl.DataSource = dt;
        //    ddl.DataTextField = textfield;
        //    ddl.DataValueField = valuefield;
        //    ddl.DataBind();
        //}


        /// <summary>
        /// Fill current Mission
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AddProspectusList(string prefixText, int count)
        {
            DataTable dt = new ProspectusBL().GetprospectusAutoFiller("ProspectusName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][1].ToString());
            }
            return horseList;
        }



        protected void btnShow_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtbxProspectusName.Text.Equals(""))
                {
                    var message = "Please select Prospectus Master Name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string[] prospectusName = txtbxProspectusName.Text.Split('{');

                    if (prospectusName.Length == 1)
                    {
                        var message = "Prospectus Name not found.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        //hdnfieldprospectusid.Value = Convert.ToString(new ProspectusBL().GetProspectusId(prospectusName[0], prospectusName[1].Remove(prospectusName[1].Length - 1), prospectusName[2].Remove(prospectusName[2].Length-1)));

                        DataTable dt = new ProspectusBL().GetProspectusId(prospectusName[0], prospectusName[1].Remove(prospectusName[1].Length - 1), prospectusName[2].Remove(prospectusName[2].Length - 1));
                        if (dt.Rows.Count > 0)
                        {
                            hdnfieldprospectusid.Value = dt.Rows[0][1].ToString();
                            lblMasterRaceNameID.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString();
                        }

                        btnProspectusName.Text = "Update";
                        MainProspectusBind(hdnfieldprospectusid.Value);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex.Message + ex.StackTrace + "');", true);
            }
        }

        protected void btnHorseShow_Click(object sender, EventArgs e)
        {
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
                btnProspectusName.Enabled = false;
                Button8.Enabled = false;
                btnRaceInMemoryOf.Enabled = false;
                Button2.Enabled = false;
                Button16.Enabled = false;
                //Button17.Enabled = false;
                Button3.Enabled = false;
                Button4.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button9.Enabled = false;
                Button10.Enabled = false;
                Button11.Enabled = false;
                Button12.Enabled = false;
                Button13.Enabled = false;
                Button14.Enabled = false;
                Button15.Enabled = false;
                btnAdd.Enabled = false;
                Button20.Enabled=false;
                Button21.Enabled=false;
                Button19.Enabled = false;
                Button22.Enabled = false;
                Button23.Enabled = false;
                Button24.Enabled = false;
                Button25.Enabled = false;
				Button26.Enabled = false;
				Button27.Enabled = false;
                Button28.Enabled = false;
                button17.Enabled = false;

			}
            else
            {
                btnProspectusName.Enabled = true;
                Button8.Enabled = true;
                btnRaceInMemoryOf.Enabled = true;
                Button2.Enabled = true;
                Button16.Enabled = true;
                //Button17.Enabled = true;
                Button3.Enabled = true;
                Button4.Enabled = true;
                Button5.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
                Button9.Enabled = true;
                Button10.Enabled = true;
                Button11.Enabled = true;
                Button12.Enabled = true;
                Button13.Enabled = true;
                Button14.Enabled = true;
                Button15.Enabled = true;
                btnAdd.Enabled = true;
                Button20.Enabled = true;
                Button21.Enabled = true;
                Button19.Enabled = true;
                Button22.Enabled = true;
                Button23.Enabled = true;
                Button24.Enabled = true;
                Button25.Enabled = true;
				Button26.Enabled = true;
				Button27.Enabled = true;
                Button28.Enabled = true;
                button17.Enabled = true;
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

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            lblMasterRaceNameID.Text = string.Empty;
            chkbxMaidenTerm.Checked = false;
            ClearAllSelection(this);
            dvGdvwMasterRaceName.Visible = false;
            txtbxProspectusName.Enabled = true;
            btnProspectusName.Enabled = true;
            Button8.Enabled = true;
            btnRaceInMemoryOf.Enabled = true;
            Button2.Enabled = true;
            Button16.Enabled = true;
            //Button17.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            Button5.Enabled = true;
            Button6.Enabled = true;
            Button7.Enabled = true;
            Button9.Enabled = true;
            Button10.Enabled = true;
            Button11.Enabled = true;
            Button12.Enabled = true;
            Button13.Enabled = true;
            Button14.Enabled = true;
            Button15.Enabled = true;
            btnAdd.Enabled = true;
            Button20.Enabled = true;
            Button21.Enabled = true;
            Button19.Enabled = true;
            Button22.Enabled = true;
            Button23.Enabled = true;
			Button26.Enabled = true;
			Button27.Enabled = true;
            Button28.Enabled = true;

            GvInterestedProfessionBackground.DataSource = new DataTable();
            GvInterestedProfessionBackground.DataBind();

            GVHWPCondition.DataSource = new DataTable();
            GVHWPCondition.DataBind();

            GvHandicapWeightAsperGender.DataSource = new DataTable();
            GvHandicapWeightAsperGender.DataBind();

			GvPermanentCondition.DataSource = new DataTable();
			GvPermanentCondition.DataBind();
		}

        public void ClearAllSelection(Control parent)
        {
            hdnfieldprospectusid.Value = "";
            //hdnfdHorseDetail.Value = "";
            //hdnfdHorseDoB.Value = "";
            //btnHorseName.Text = "Add";
            btnProspectusName.Text = "Add";
            GvSponcer.DataSource = new DataTable();
            GvSponcer.DataBind();

            GvWeightAsPerAge.DataSource = new DataTable();
            GvWeightAsPerAge.DataBind();

            GvMasterRaceName.DataSource = new DataTable();
            GvMasterRaceName.DataBind();

            GvRaceHistory.DataSource = new DataTable();
            GvRaceHistory.DataBind();

            GvStakeMoneyAddition.DataSource = new DataTable();
            GvStakeMoneyAddition.DataBind();

            GvRaceAbbriviation.DataSource = new DataTable();
            GvRaceAbbriviation.DataBind();

            GvMomenttopresenter.DataSource = new DataTable();
			GvMomenttopresenter.DataBind();

			GvOtherCondition.DataSource = new DataTable();
			GvOtherCondition.DataBind();

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

                    ((RadioButtonList)(x)).ClearSelection();
                }
                if ((x.GetType() == typeof(RadioButtonList)))
                {
                    //((RadioButtonList)(x)).SelectedValue = "-1";
                    ((RadioButtonList)(x)).ClearSelection();
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
				if (hdnfieldprospectusid.Value != null)
				{
					var status = new ProspectusBL().AddProspectus(
							Convert.ToInt32(hdnfieldprospectusid.Value),
							string.Empty,
							string.Empty,
							string.Empty,
							1,
							"Delete");
					var message = "Record Deleted Successfully.";
					Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					Session["ProspectusID"] = null;
					hdnfieldprospectusid.Value = string.Empty;
					chkbxMaidenTerm.Checked = false;
					ClearAllSelection(this);
					dvGdvwMasterRaceName.Visible = false;
					txtbxProspectusName.Enabled = true;
					btnProspectusName.Enabled = true;
					Button8.Enabled = true;
					btnRaceInMemoryOf.Enabled = true;
					Button2.Enabled = true;
					Button16.Enabled = true;
					//Button17.Enabled = true;
					Button3.Enabled = true;
					Button4.Enabled = true;
					Button5.Enabled = true;
					Button6.Enabled = true;
					Button7.Enabled = true;
					Button9.Enabled = true;
					Button10.Enabled = true;
					Button11.Enabled = true;
					Button12.Enabled = true;
					Button13.Enabled = true;
					Button14.Enabled = true;
					Button15.Enabled = true;
					btnAdd.Enabled = true;
					Button20.Enabled = true;
					Button21.Enabled = true;
					Button19.Enabled = true;
					Button22.Enabled = true;
					Button23.Enabled = true;
					Button26.Enabled = true;
					Button27.Enabled = true;
                    Button28.Enabled = true;

                    GvInterestedProfessionBackground.DataSource = new DataTable();
					GvInterestedProfessionBackground.DataBind();

					GVHWPCondition.DataSource = new DataTable();
					GVHWPCondition.DataBind();

					GvHandicapWeightAsperGender.DataSource = new DataTable();
					GvHandicapWeightAsperGender.DataBind();

					GvPermanentCondition.DataSource = new DataTable();
					GvPermanentCondition.DataBind();
				}
				else
				{
					var message = "Incorrect Information.";
					Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
				}

			}
			catch (Exception exception)
			{
				ErrorHandling.SendErrorToText(exception);
				var message = "Incorrect Information.";
				Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
			}

		}
		protected void BtnAddClick(object sender, EventArgs e)
        {
            SaveData();
            DisableAllField(this, "Disable");
            DisableButton("Disable");
        }

        protected void SaveData()
        {
            try
            {
                int status = 0;
                var foreignhorsesterm=string.Empty;
               
                var maidenhorseterm = string.Empty;
                if (chkbxMaidenTerm.Checked.Equals(true))
                {
                    maidenhorseterm = "true";

                }
                else
                {
                    maidenhorseterm = "false";
                }
                status = new ProspectusBL().AddProspectus(
                            Convert.ToInt32(hdnfieldprospectusid.Value),
                            "0",
                            foreignhorsesterm,
                            maidenhorseterm,
                            1,
                            "Insert");

                if (status == 2)
                {
                    string message = "Duplicate Record.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ClearAllSelection(this);
                }
                else if (status > 0)
                {
                    string message = "Record added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    DisableAllField(this, "Disable");
                    DisableButton("Disable");
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
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Prospectus_Master"))
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
                    var dtErrorResult = new ProspectusBL().Import30(dt, "ProspectusMaster", 0);
                    if (dtErrorResult.Rows.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {

                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Race Name");

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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Master.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }


        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //using (DataSet ds = new ProspectusBL().GetProspectusNameWithCombination(0, "MasterMillion"))
                using (DataSet ds = new ProspectusBL().GetExport(0, "ProspectusMaster"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("MasterRaceID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Prospectus Master");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Prospectus_Master.xlsx");
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

        protected void btnExportToday_Click(object sender, EventArgs e)
        {
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session.Remove("ProspectusMasterRaceName");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
    }
}