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
    using System.Diagnostics;
    using System.Configuration;
    public partial class AddHorse : System.Web.UI.Page
    {
        private MasterHorseBL Bl = new MasterHorseBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
				lblHorsePortfolio.Text = Convert.ToString(Bl.GetHorseCount());
                
				AjaxControlToolkit.TabPanel activeTab = TabContainer1.ActiveTab;
                    if (!IsPostBack)
                    {
                        BindDropDown(drpdwnColor, "HorseColor", "HorseColor", "HorseColorID");
                        //drpdwnColor.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        BindDropDown(drpdwnBirthNation, "MasterNation", "Nation", "NationID");
                        drpdwnBirthNation.Items.Insert(0, new ListItem("-- Please select --", "-1"));

						/**** Tab Panel -> Family **************************/
						BindDropDown(drpdwnFAgeCondition, "AgeCondition", "AgeCondition", "AgeConditionID");
						drpdwnFAgeCondition.Items.Insert(0, new ListItem("-- Please select --", "-1"));
						BindDropDown(drpdwnFGender, "HorseSex", "HorseSex", "HorseSexID");
						drpdwnFGender.Items.Insert(0, new ListItem("-- Please select --", "-1"));
						BindDropDown(drpdwnFBaseCenter, "Center", "CenterName", "ID");
						drpdwnFBaseCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
						BindDropDown(drpdwnFProfile, "MasterHorseProfile", "HorseProfile", "ProfileHMID");
						drpdwnFProfile.Items.Insert(0, new ListItem("-- Please select --", "-1"));
						/************** END **************************************/


						/**** Tab Panel -> Achivements **************************/
						//BindDropDown(drpdwnCenter, "Center", "CenterName", "ID");
      //                  drpdwnCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
      //                  BindDropDown(drpdwnYear, "Year", "YearName", "YearID");
      //                  drpdwnYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
      //                  BindDropDown(drpdwnSeason, "Season", "SeasonName", "SeasonID");
      //                  drpdwnSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        /************** END **************************************/
                        BindData();
                    

                    }
                    if (Session["HorseID"] != null)
                    {
                        BindDropDown(drpdwnColor, "HorseColor", "HorseColor", "HorseColorID");
                        drpdwnColor.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        BindDropDown(drpdwnBirthNation, "MasterNation", "Nation", "NationID");
                        drpdwnBirthNation.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                        drpdwnBirthNation.ClearSelection();
                        if (drpdwnBirthNation.Items.Count > 1)
                        {
                            drpdwnBirthNation.Items.FindByText("India").Selected = true;
                        }
                        if (Session["HorseID"] != null)
                        {
                            this.hdnHorseNameId.Value = Convert.ToString(Session["HorseID"]);
                        }
                        this.btnHorseName.Text = "Update";
                        this.MainHorseBind(Session["HorseID"].ToString());
                        this.Session["HorseID"] = null;
                    }
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void GvHorseFamily_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (e.Row.RowIndex == 0)
					e.Row.Style.Add("height", "150px");
                //Label lblDistance = (e.Row.FindControl("lblDistance") as Label);
                //var test = lblDistance.Text;

            }
		}
		protected void Tabs_ActiveTabChanged(object sender, EventArgs e)
		{

            if (TabContainer1.ActiveTab.TabIndex == 2)
            {
                if (btnProfile.Enabled.Equals(true))
                {
                    ViewState["Value"] = 1;
                    DisableAllField(this, "Enable");
                    DisableButton("Enable");
                }
                else
                {
                    ViewState["Value"] = 2;
                    DisableAllField(this, "Disable");
                    DisableButton("Disable");
                }
                
                var dt = new MasterHorseBL().GetHorseNameDamBasis(hdnHorseNameId.Value);
                if (dt.Rows.Count > 0)
                {
                    GvHorseFamily.DataSource = dt;
                    GvHorseFamily.DataBind();
                    lbltotalfamilyMember.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    GvHorseFamily.DataSource = new DataTable();
                    GvHorseFamily.DataBind();
                }

            }
            else if (TabContainer1.ActiveTab.TabIndex == 1)
            {
                if (ViewState["Value"].ToString() == "1")
                {
                    DisableAllField(this, "Enable");
                    DisableButton("Enable");
                }
                else
                {
                    DisableAllField(this, "Disable");
                    DisableButton("Disable");
                }

            }
            else if (TabContainer1.ActiveTab.TabIndex == 4)
            {
                if (btnProfile.Enabled.Equals(true))
                {
                    ViewState["Value"] = 1;
                 //   DisableAllField(this, "Enable");
                 //   DisableButton("Enable");
                }
                else
                {
                    ViewState["Value"] = 2;
                   // DisableAllField(this, "Disable");
                   // DisableButton("Disable");
                }

                DisableAllField(this, "Enable");
                DisableButton("Enable");
                txtbxAchieveHorseName.Enabled = false;
                AchivementBindData();
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
            GvProfile.DataSource = new DataTable();
            GvProfile.DataBind();
            ClearAllSelection(this);
            txtbxHorseName.Enabled = true;
            rdbtnDobType.ClearSelection();
            btnAdd.Enabled = true;
            btnFamilyTree.Enabled = true;
            btnHorseName.Enabled = true;
            Button8.Enabled = true;
            btnProfile.Enabled = true;
            btnStatus.Enabled = true;
            Button2.Enabled = true;
            Button12.Enabled = true;
            Button13.Enabled = true;
            Button14.Enabled = true;
            Button15.Enabled = true;
            Button18.Enabled = true;
            Button16.Enabled = true;
            Button23.Enabled = true;
            Button24.Enabled = true;
            Button25.Enabled = true;
            Button1.Enabled = true;
            Button9.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            Button5.Enabled = true;
            Button6.Enabled = true;
            Button7.Enabled = true;
            Button10.Enabled = true;
            Button48.Enabled = true;
            Button26.Enabled = true;
            Button27.Enabled = true;
            Button28.Enabled = true;
            Button29.Enabled = true;
            Button17.Enabled = true;
            Button30.Enabled = true;
            Button31.Enabled = true;
            Button32.Enabled = true;
            Button33.Enabled = true;
            Button34.Enabled = true;
            Button35.Enabled = true;
            Button36.Enabled = true;
            Button37.Enabled = true;
            Button38.Enabled = true;
            Button39.Enabled = true;
            Button40.Enabled = true;
            Button41.Enabled = true;
            btnCurrentMission.Enabled = true;
            btnHorseBan.Enabled = true;
            btnHomeDistance.Enabled = true;
            btnMyHomeDistance.Enabled = true;
            btnExpectedDistance.Enabled = true;
            Button19.Enabled = true;
            lblHorseIDNameID.Text = string.Empty;
            DisableAllField(this, "Enable");
            DisableButton("Enable");
            Button11.Enabled = true;
            Button20.Enabled = true;
        }

        protected void btnFamilyTree_OnClick(object sender, EventArgs e)
        {
            try
            {
                //string myComputerPath = Environment.GetFolderPath(E:\FamilyTree);
                string myComputerPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string downloadArea = Path.Combine(myComputerPath, @"\FamilyTree\");
                System.Diagnostics.Process.Start("explorer", myComputerPath);

          
                
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                ErrorHandling.SendErrorToText(ex);
                
                //var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Incorrect Information.');", true);
            }
        }

        private void openExplorer()
        {
            Process.Start("explorer");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            DisableAllField(this,"Enable");
            DisableButton("Enable");
        }

        public void DisableButton(string action)
        {
            if (action.Equals("Disable"))
            {
               // btnShow.Enabled = false;
               // btnClear.Enabled = false;
                btnAdd.Enabled = false;
                btnFamilyTree.Enabled = false;
                btnHorseName.Enabled = false;
                Button8.Enabled = false;
                btnProfile.Enabled = false;
                btnStatus.Enabled = false;
                Button2.Enabled = false;
                Button12.Enabled = false;
                Button13.Enabled = false;
                Button14.Enabled = false;
                Button15.Enabled = false;
                Button18.Enabled = false;
                Button16.Enabled = false;
                Button23.Enabled = false;
                Button24.Enabled = false;
                Button25.Enabled = false;
                Button1.Enabled = false;
                Button9.Enabled = false;
                Button3.Enabled = false;
                Button4.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button10.Enabled = false;
                Button48.Enabled = false;
                Button26.Enabled = false;
                Button27.Enabled = false;
                Button28.Enabled = false;
                Button29.Enabled = false;
                Button17.Enabled = false;
                Button30.Enabled = false;
                Button31.Enabled = false;
                Button32.Enabled = false;
                Button33.Enabled = false;
                Button34.Enabled = false;
                Button35.Enabled = false;
                Button36.Enabled = false;
                Button37.Enabled = false;
                Button38.Enabled = false;
                Button39.Enabled = false;
                Button40.Enabled = false;
                Button41.Enabled = false;
                btnCurrentMission.Enabled = false;
                btnHorseBan.Enabled = false;
                btnHomeDistance.Enabled = false;
                btnMyHomeDistance.Enabled = false;
                btnExpectedDistance.Enabled = false;
                Button19.Enabled = false;
                Button11.Enabled = false;
                Button20.Enabled = false;
            }
            else
            {
               // btnShow.Enabled = true;
                //btnClear.Enabled = true;
                btnAdd.Enabled = true;
                btnFamilyTree.Enabled = true;
                btnHorseName.Enabled = true;
                Button8.Enabled = true;
                btnProfile.Enabled = true;
                btnStatus.Enabled = true;
                Button2.Enabled = true;
                Button12.Enabled = true;
                Button13.Enabled = true;
                Button14.Enabled = true;
                Button15.Enabled = true;
                Button18.Enabled = true;
                Button16.Enabled = true;
                Button23.Enabled = true;
                Button24.Enabled = true;
                Button25.Enabled = true;
                Button1.Enabled = true;
                Button9.Enabled = true;
                Button3.Enabled = true;
                Button4.Enabled = true;
                Button5.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
                Button10.Enabled = true;
                Button48.Enabled = true;
                Button26.Enabled = true;
                Button27.Enabled = true;
                Button28.Enabled = true;
                Button29.Enabled = true;
                Button17.Enabled = true;
                Button30.Enabled = true;
                Button31.Enabled = true;
                Button32.Enabled = true;
                Button33.Enabled = true;
                Button34.Enabled = true;
                Button35.Enabled = true;
                Button36.Enabled = true;
                Button37.Enabled = true;
                Button38.Enabled = true;
                Button39.Enabled = true;
                Button40.Enabled = true;
                Button41.Enabled = true;
                btnCurrentMission.Enabled = true;
                btnHorseBan.Enabled = true;
                btnHomeDistance.Enabled = true;
                btnMyHomeDistance.Enabled = true;
                btnExpectedDistance.Enabled = true;
                Button19.Enabled = true;
                Button11.Enabled = true;
                Button20.Enabled = true;
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

                        ((Label)(x)).Enabled=false;
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
                        DisableAllField(x,"Disable");
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
                        DisableAllField(x,"Enable");
                    }
                }
            }
        }

        public void ClearAllSelection(Control parent)
        {
            hdnHorseNameId.Value = "";
            hdnfdHorseDetail.Value = "";
            hdnfdHorseDoB.Value = "";
            btnHorseName.Text = "Add";

			GvRunningCenter.DataSource = new DataTable();
			GvRunningCenter.DataBind();

			GvOwnerRecord.DataSource = new DataTable();
            GvOwnerRecord.DataBind();

            GvOwnerActual.DataSource = new DataTable();
            GvOwnerActual.DataBind();

            GvTrainerRecord.DataSource = new DataTable();
            GvTrainerRecord.DataBind();

            GvTrainerActual.DataSource = new DataTable();
            GvTrainerActual.DataBind();

            GvGender.DataSource = new DataTable();
            GvGender.DataBind();

            GvOwnerStud.DataSource = new DataTable();
            GvOwnerStud.DataBind();

			GvDistancePerformanceOld.DataSource = new DataTable();
			GvDistancePerformanceOld.DataBind();

			GvClassPerformanceOld.DataSource = new DataTable();
			GvClassPerformanceOld.DataBind();

			GvObservation.DataSource = new DataTable();
            GvObservation.DataBind();

            GvHabit.DataSource = new DataTable();
            GvHabit.DataBind();

            GvTargetRace.DataSource = new DataTable();
            GvTargetRace.DataBind();

            GvEquiupment.DataSource = new DataTable();
            GvEquiupment.DataBind();

            GvBit.DataSource = new DataTable();
            GvBit.DataBind();

            GvVietProblem.DataSource = new DataTable();
            GvVietProblem.DataBind();


            GdviewHorseSwimming.DataSource = new DataTable();
            GdviewHorseSwimming.DataBind();

            GdvTreadmill.DataSource = new DataTable();
            GdvTreadmill.DataBind();

            GvProfile.DataSource = new DataTable();
            GvProfile.DataBind();

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

                    ((CheckBox)(x)).Checked = false;
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

            rdbtnPhysicType.ClearSelection();
            rdbtnBodyShape.ClearSelection();
            drpdwnBirthNation.ClearSelection();
            drpdwnBirthNation.Items.FindByText("India").Selected = true;
        }

        protected void btnShow_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtbxHorseName.Text.Equals(""))
                {
                    var message = "Please select Horse Name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string[] horseName = txtbxHorseName.Text.Split('{');
                    
                    if (horseName.Length == 1)
                    {
                        var message = "Horse Name not found.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        // hdnHorseId.Value = Convert.ToString(Bl.GetHorseId(horseName[0], horseName[1].Substring(0, horseName[1].Length - 1)));



                        DataTable dt = Bl.GetHorseId(horseName[0], horseName[1].Substring(0, horseName[1].Length - 1));
                        if (dt.Rows.Count > 0)
                        {
                            hdnHorseNameId.Value = dt.Rows[0][1].ToString();
                            hdnAchieveHorseId.Value = dt.Rows[0][1].ToString();
                            lblHorseIDNameID.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString(); 
                        }

                        //hdnAchieveHorseId.Value = Convert.ToString(Bl.GetHorseId(horseName[0], horseName[1].Substring(0, horseName[1].Length - 1)));
                        btnHorseName.Text = "Update";
                        txtbxAchieveHorseName.Text = txtbxHorseName.Text;
                        MainHorseBind(hdnHorseNameId.Value);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
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
        public static List<string> AddHorseList(string prefixText, int count)
    {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][1].ToString());
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
		public static List<string> AddFamilyHorseList(string prefixText, int count)
		{
			DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseDamList", prefixText);
			List<string> horseList = new List<string>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				horseList.Add(dt.Rows[i][1].ToString());
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
        public static List<string> GetDivisionRaceName(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("DivisionRaceName", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
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
        public static List<string> AddLineageList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseLineage", prefixText);
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
        public static List<string> PhysicType(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("PhysicType", prefixText);
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
        public static List<string> BirthDefectList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("BirthDefectList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(dt.Rows[i][0].ToString());
            }
            return horseList;
        }


        protected void txtbxSireName_OnTextChanged(object sender, EventArgs e)
        {
            if (txtbxSireName.Text != "")
            {
                DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("SireProfileCompleteStatus", txtbxSireName.Text);
                if (dt.Rows.Count > 0)
                {
                    if (!(dt.Rows[0][0].ToString().Equals("NULL") || dt.Rows[0][0].ToString().Equals("")))
                    {
                    lblSireNameStage.Text = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        lblSireNameStage.Text = "Not Done";
                    }
                    
                }
                else
                {
                    lblSireNameStage.Text = "Not Done";
                }
                
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
        public static List<string> AddSireList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseSireList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }

        protected void txtbxDamName_OnTextChanged(object sender, EventArgs e)
        {
            if (txtbxDamName.Text != "")
            {
                DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("DamProfileCompleteStatus", txtbxDamName.Text);
                if (dt.Rows.Count > 0)
                {
                    if(!(dt.Rows[0][0].ToString().Equals("NULL") || dt.Rows[0][0].ToString().Equals("")))
                    {
                    lblDamNameStage.Text = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        lblDamNameStage.Text = "Not Done";
                    }
                }
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
        public static List<string> AddDamList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseDamList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
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
        public static List<string> AddBirthStudList(string prefixText, int count)
        {
            //using(SqlCommand )
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseStudList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
             //   horseList.Add(dt.Rows[i][1].ToString());
                string studlist =
                    AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0]));
                horseList.Add(studlist);
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
        public static List<string> AddBreederList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("HorseBreederList", prefixText);
            List<string> horseList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                horseList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        dt.Rows[i][1].ToString(),
                        Convert.ToString(dt.Rows[i][0])));
            }
            return horseList;
        }


        protected void MainHorseBind(string horseId)
        {
            try
            {
                DataSet ds = null;
                ds = Bl.GetHorseCompleteInformation(Convert.ToInt32(horseId), "MainHorse");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.hdnfdHorseDetail.Value = ds.Tables[0].Rows[0][1].ToString();
                    if (Session["HorseID"] != null)
                    {
                        this.txtbxHorseName.Text = ds.Tables[0].Rows[0][1].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DOBType"].ToString() != "NULL"
                        && ds.Tables[0].Rows[0]["DOBType"].ToString() != "")
                    {
                        rdbtnDobType.Items.FindByText(ds.Tables[0].Rows[0]["DOBType"].ToString()).Selected = true;
                    }
                    else
                    {
                        rdbtnDobType.ClearSelection();
                    }
                    if (ds.Tables[0].Rows[0]["LateFoal"].ToString() != "NULL" && ds.Tables[0].Rows[0]["LateFoal"].ToString() != "")
                    {
                        //rdbtnLateFoal.Items.FindByText(ds.Tables[0].Rows[0]["LateFoal"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["LateFoal"].ToString().Equals("Yes"))
                            chkbxLateFoal.Checked=true;
                    }
                    if (ds.Tables[0].Rows[0]["HopeAgainstLateFoal"].ToString() != "NULL" && ds.Tables[0].Rows[0]["HopeAgainstLateFoal"].ToString() != "")
                    {
                        //rdbtnHopAgainstLFoal.Items.FindByText(ds.Tables[0].Rows[0]["HopeAgainstLateFoal"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["HopeAgainstLateFoal"].ToString().Equals("Yes"))
                            chkbxHopeLateFoal.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["DateofDeath"].ToString() != "NULL" && ds.Tables[0].Rows[0]["DateofDeath"].ToString() != "")
                    {
                        txtbxDateofDeath.Text = ds.Tables[0].Rows[0]["DateofDeath"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HorseColor"].ToString() != "NULL" && ds.Tables[0].Rows[0]["HorseColor"].ToString() != "")
                    {
                        drpdwnColor.ClearSelection();
                        drpdwnColor.Items.FindByText(ds.Tables[0].Rows[0]["HorseColor"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["Nation"].ToString() != "NULL" && ds.Tables[0].Rows[0]["Nation"].ToString() != "")
                    {
                        drpdwnBirthNation.ClearSelection();
                        drpdwnBirthNation.Items.FindByText(ds.Tables[0].Rows[0]["Nation"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["SireName"].ToString() != "NULL" && ds.Tables[0].Rows[0]["SireName"].ToString() != "")
                    {
                        txtbxSireName.Text = ds.Tables[0].Rows[0]["SireName"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["SireNameID"].ToString() != "NULL" && ds.Tables[0].Rows[0]["SireNameID"].ToString() != "")
                    {
                        hdnfieldSireNameID.Value = ds.Tables[0].Rows[0]["SireNameID"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["DamName"].ToString() != "NULL" && ds.Tables[0].Rows[0]["DamName"].ToString() != "")
                    {
                        txtbxDamName.Text = ds.Tables[0].Rows[0]["DamName"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["DamNameID"].ToString() != "NULL" && ds.Tables[0].Rows[0]["DamNameID"].ToString() != "")
                    {
                        hdnfieldDamNameID.Value = ds.Tables[0].Rows[0]["DamNameID"].ToString();
                    }



                    if (ds.Tables[0].Rows[0]["GotAbroad"].ToString() != "NULL" && ds.Tables[0].Rows[0]["GotAbroad"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["GotAbroad"].ToString().Equals("Yes"))
                            chkbxAbroad.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["StudNameAlias"].ToString() != "NULL" && ds.Tables[0].Rows[0]["StudNameAlias"].ToString() != "")
                    {
                        txtbxBirthStud.Text = ds.Tables[0].Rows[0]["StudNameAlias"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["StudNamePID"].ToString() != "NULL" && ds.Tables[0].Rows[0]["StudNamePID"].ToString() != "")
                    {
                        hdnfieldbirthstudNameID.Value = ds.Tables[0].Rows[0]["StudNamePID"].ToString();
                    }



                    if (ds.Tables[0].Rows[0]["BreederNameID"].ToString() != "NULL" && ds.Tables[0].Rows[0]["BreederNameID"].ToString() != "")
                    {
                        hdnbreederNameID.Value = ds.Tables[0].Rows[0]["BreederNameID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Breeder"].ToString() != "NULL" && ds.Tables[0].Rows[0]["Breeder"].ToString() != "")
                    {
                        txtbxBreeder.Text = ds.Tables[0].Rows[0]["Breeder"].ToString();
                    }



                    if (ds.Tables[0].Rows[0]["ClassicCantender"].ToString() != "NULL" && ds.Tables[0].Rows[0]["ClassicCantender"].ToString() != "")
                    {
                        //rdbtnClassicCantender.Items.FindByText(ds.Tables[0].Rows[0]["ClassicCantender"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["ClassicCantender"].ToString().Equals("Yes"))
                            chkbxCantender.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["ClassicMaterial"].ToString() != "NULL" && ds.Tables[0].Rows[0]["ClassicMaterial"].ToString() != "")
                    {
                        //rdbtnClassicMaterial.Items.FindByText(ds.Tables[0].Rows[0]["GotAbroad"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["ClassicMaterial"].ToString().Equals("Yes"))
                            chkbxProvedClassicMaterial.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Lineage"].ToString() != "NULL" && ds.Tables[0].Rows[0]["Lineage"].ToString() != "")
                    {
                        txtbxLineage.Text = ds.Tables[0].Rows[0]["Lineage"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["UnderValuedHorse"].ToString() != "NULL" && ds.Tables[0].Rows[0]["UnderValuedHorse"].ToString() != "") 
                    {
                        //rdbtnUnderValuedHorse.Items.FindByText(ds.Tables[0].Rows[0]["UnderValuedHorse"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["UnderValuedHorse"].ToString().Equals("Yes"))
                            chkbxUndervalueHorse.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["BodyShape"].ToString() != "NULL" && ds.Tables[0].Rows[0]["BodyShape"].ToString() != "")
                    {
                        rdbtnBodyShape.Items.FindByText(ds.Tables[0].Rows[0]["BodyShape"].ToString()).Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["PhysicType"].ToString() != "NULL" && ds.Tables[0].Rows[0]["PhysicType"].ToString() != "")
                    {
                        //txtbxPhysicType.Text = ds.Tables[0].Rows[0]["PhysicType"].ToString();
                        rdbtnPhysicType.Items.FindByText(ds.Tables[0].Rows[0]["PhysicType"].ToString()).Selected = true;
                        //if (ds.Tables[0].Rows[0]["PhysicType"].ToString().Equals("true") || ds.Tables[0].Rows[0]["PhysicType"].ToString().Equals("True"))
                        //    chkbxPhysicType.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["BirthDefect"].ToString() != "NULL" && ds.Tables[0].Rows[0]["BirthDefect"].ToString() != "")
                    {
                        txtbxBirthDefect.Text = ds.Tables[0].Rows[0]["BirthDefect"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DosageIndex"].ToString() != "NULL" && ds.Tables[0].Rows[0]["DosageIndex"].ToString() != "")
                    {
                        txtbxDosageIndex.Text = ds.Tables[0].Rows[0]["DosageIndex"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DosageProfile"].ToString() != "NULL" && ds.Tables[0].Rows[0]["DosageProfile"].ToString() != "")
                    {
                        txtbxDosageProfile.Text = ds.Tables[0].Rows[0]["DosageProfile"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CenterOfDistribution"].ToString() != "NULL" && ds.Tables[0].Rows[0]["CenterOfDistribution"].ToString() != "")
                    {
                        txtbxCenterofDistribution.Text = ds.Tables[0].Rows[0]["CenterOfDistribution"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["RainyDayPerformer"].ToString() != "NULL" && ds.Tables[0].Rows[0]["RainyDayPerformer"].ToString() != "")
                    {
                       // rdbtnRainyDayPerformer.Items.FindByText(ds.Tables[0].Rows[0]["RainyDayPerformer"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["RainyDayPerformer"].ToString().Equals("Yes"))
                            chkbxRainyDayPerformer.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["WhipMustRequired"].ToString() != "NULL" && ds.Tables[0].Rows[0]["WhipMustRequired"].ToString() != "")
                    {
                        //rdbtnWhipMustRequired.Items.FindByText(ds.Tables[0].Rows[0]["WhipMustRequired"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["WhipMustRequired"].ToString().Equals("Yes"))
                            chkbxWhipMustRequired.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["ProfileComplete"].ToString() != "NULL" && ds.Tables[0].Rows[0]["ProfileComplete"].ToString() != "")
                    {
                        rdbtnProfile.Items.FindByText(ds.Tables[0].Rows[0]["ProfileComplete"].ToString()).Selected = true;
                       // lblSireNameStage.Text = ds.Tables[0].Rows[0]["ProfileComplete"].ToString();
                       // lblDamNameStage.Text = ds.Tables[0].Rows[0]["ProfileComplete"].ToString();

                    }
                    if (ds.Tables[0].Rows[0]["SireStatus"].ToString() != "NULL" && ds.Tables[0].Rows[0]["SireStatus"].ToString() != "")
                    {
                        lblSireNameStage.Text = ds.Tables[0].Rows[0]["SireStatus"].ToString();
                    }
                    else
                    {
                        lblSireNameStage.Text = "";
                    }
                    if (ds.Tables[0].Rows[0]["DamStatus"].ToString() != "NULL" && ds.Tables[0].Rows[0]["DamStatus"].ToString() != "")
                    {
                        lblDamNameStage.Text = ds.Tables[0].Rows[0]["DamStatus"].ToString();
                    }
                    else
                    {
                        lblDamNameStage.Text = "";
                    }
                    if (ds.Tables[0].Rows[0]["CarryTopWeight"].ToString() != "NULL" && ds.Tables[0].Rows[0]["CarryTopWeight"].ToString() != "")
                    {
                     //   rdbtnCarryTopWeight.Items.FindByText(ds.Tables[0].Rows[0]["CarryTopWeight"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["CarryTopWeight"].ToString().Equals("Yes"))
                            chkbxCarryTopWeight.Checked = true;
                    }

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.lblHorseCurrentName.Text = ds.Tables[1].Rows[0][0].ToString();
                    this.lblHorseExName.Text = ds.Tables[1].Rows.Count > 1 ? ds.Tables[1].Rows[1][0].ToString() : "";
                    this.hdnfdHorseDetail.Value = hdnfdHorseDetail.Value + "," + ds.Tables[1].Rows[0][0].ToString() + "," +
                                             lblHorseExName.Text;
                    this.lblDOB.Text = ds.Tables[1].Rows[0][1].ToString();
                    this.hdnfdHorseDoB.Value = this.lblDOB.Text;
                }

                this.lblHorseStatus.Text = ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0][0].ToString() : "No Record found.";
                this.lblCurrentStatus.Text = ds.Tables[3].Rows.Count > 0 ? ds.Tables[3].Rows[0][0].ToString() : "No Record found.";
                this.lblCurrentMission.Text = ds.Tables[4].Rows.Count > 0 ? ds.Tables[4].Rows[0][0].ToString() : "No Record found.";
                this.lblBan.Text = ds.Tables[5].Rows.Count > 0 ? ds.Tables[5].Rows[0][0].ToString() : "No Record found.";
                //this.lblHorseProfile.Text = ds.Tables[6].Rows.Count > 0 ? ds.Tables[6].Rows[0][0].ToString() : "No Record found.";
                
                dvProfile.Visible = true;
                GvProfile.DataSource = ds.Tables[6];
                GvProfile.DataBind();

                this.lblHomeDistance.Text = ds.Tables[7].Rows.Count > 0 ? ds.Tables[7].Rows[0][0].ToString() : "No Record found.";
                this.lblMyHomeDistance.Text = ds.Tables[8].Rows.Count > 0 ? ds.Tables[8].Rows[0][0].ToString() : "No Record found.";
                this.lblExpectedDistance.Text = ds.Tables[9].Rows.Count > 0 ? ds.Tables[9].Rows[0][0].ToString() : "No Record found.";
                this.lblFavDistanceGroup.Text = ds.Tables[10].Rows.Count > 0 ? ds.Tables[10].Rows[0][0].ToString() : "No Record found.";
                this.lblHomeClass.Text = ds.Tables[11].Rows.Count > 0 ? ds.Tables[11].Rows[0][0].ToString() : "No Record found.";
                this.lblMyHomeClass.Text = ds.Tables[12].Rows.Count > 0 ? ds.Tables[12].Rows[0][0].ToString() : "No Record found.";
                this.lblExpectedClass.Text = ds.Tables[13].Rows.Count > 0 ? ds.Tables[13].Rows[0][0].ToString() : "No Record found.";
                this.lblFavClassGroup.Text = ds.Tables[14].Rows.Count > 0 ? ds.Tables[14].Rows[0][0].ToString() : "No Record found.";
                
               // this.lblGender.Text = ds.Tables[15].Rows.Count > 0 ? ds.Tables[15].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[15].Rows.Count > 0)
                {
                    dvGender.Visible = true;
                    GvGender.DataSource = ds.Tables[15];
                    GvGender.DataBind();
                }
                else
                {
                    dvGender.Visible = false;
                    GvGender.DataSource = new DataTable();
                    GvGender.DataBind();
                }

                this.lblStandingNation.Text = ds.Tables[16].Rows.Count > 0 ? ds.Tables[16].Rows[0][0].ToString() : "No Record found.";
                this.lblBaseCenter.Text = ds.Tables[17].Rows.Count > 0 ? ds.Tables[17].Rows[0][0].ToString() : "No Record found.";
                //this.lblStationCenter.Text = ds.Tables[18].Rows.Count > 0 ? ds.Tables[18].Rows[0][0].ToString() : "No Record found.";
				if (ds.Tables[18].Rows.Count > 0)
				{
					dvRunningCenter.Visible = true;
					GvRunningCenter.DataSource = ds.Tables[18];
					GvRunningCenter.DataBind();
				}
				else
				{
					dvRunningCenter.Visible = false;
					GvRunningCenter.DataSource = new DataTable();
					GvRunningCenter.DataBind();
				}
				// this.lblOwnerRecord.Text = ds.Tables[19].Rows.Count > 0 ? ds.Tables[19].Rows[0][0].ToString() : "No Record found.";
				if (ds.Tables[19].Rows.Count > 0)
                {
                    dvOwnerRecord.Visible = true;
                    GvOwnerRecord.DataSource = ds.Tables[19];
                    GvOwnerRecord.DataBind();
                }
                else
                {
                    dvOwnerRecord.Visible = false;
                    GvOwnerRecord.DataSource = new DataTable();
                    GvOwnerRecord.DataBind();
                }

                
                //this.lblOwnerActual.Text = ds.Tables[20].Rows.Count > 0 ? ds.Tables[20].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[20].Rows.Count > 0)
                {
                    dvOwnerActual.Visible = true;
                    GvOwnerActual.DataSource = ds.Tables[20];
                    GvOwnerActual.DataBind();
                }
                else
                {
                    dvOwnerActual.Visible = false;
                    GvOwnerActual.DataSource = new DataTable();
                    GvOwnerActual.DataBind();
                }

                //this.lblTrainerRecord.Text = ds.Tables[21].Rows.Count > 0 ? ds.Tables[21].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[21].Rows.Count > 0)
                {
                    dvtrainerrecord.Visible = true;
                    GvTrainerRecord.DataSource = ds.Tables[21];
                    GvTrainerRecord.DataBind();
                }
                else
                {
                    dvtrainerrecord.Visible = false;
                    GvTrainerRecord.DataSource = new DataTable();
                    GvTrainerRecord.DataBind();
                }


                //this.lbltrainerActual.Text = ds.Tables[22].Rows.Count > 0 ? ds.Tables[22].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[22].Rows.Count > 0)
                {
                    dvTrainerActual.Visible = true;
                    GvTrainerActual.DataSource = ds.Tables[22];
                    GvTrainerActual.DataBind();
                }
                else
                {
                    dvTrainerActual.Visible = false;
                    GvTrainerActual.DataSource = new DataTable();
                    GvTrainerActual.DataBind();
                }
                
                //this.lblTargetRace.Text = ds.Tables[23].Rows.Count > 0 ? ds.Tables[23].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[23].Rows.Count > 0)
                {
                    dvTargetRace.Visible = true;
                    GvTargetRace.DataSource = ds.Tables[23];
                    GvTargetRace.DataBind();
                }
                else
                {
                    dvTargetRace.Visible = false;
                    GvTargetRace.DataSource = new DataTable();
                    GvTargetRace.DataBind();
                }

                this.lblBodyWeight.Text = ds.Tables[24].Rows.Count > 0 ? ds.Tables[24].Rows[0][0].ToString() : "No Record found.";
                this.lblHandicap.Text = ds.Tables[25].Rows.Count > 0 ? ds.Tables[25].Rows[0][0].ToString() : "No Record found.";
                this.lblMyHandicap.Text = ds.Tables[26].Rows.Count > 0 ? ds.Tables[26].Rows[0][0].ToString() : "No Record found.";
                this.lblShoe.Text = ds.Tables[27].Rows.Count > 0 ? ds.Tables[27].Rows[0][0].ToString() : "No Record found.";
                this.lblShoeDescription.Text = ds.Tables[28].Rows.Count > 0 ? ds.Tables[28].Rows[0][0].ToString() : "No Record found.";
                //this.lblEquipment.Text = ds.Tables[29].Rows.Count > 0 ? ds.Tables[29].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[29].Rows.Count > 0)
                {
                    DvEquipment.Visible = true;
                    GvEquiupment.DataSource = ds.Tables[29];
                    GvEquiupment.DataBind();
                }
                else
                {
                    DvEquipment.Visible = false;
                    GvEquiupment.DataSource = new DataTable();
                    GvEquiupment.DataBind();
                }
               // this.lblBit.Text = ds.Tables[30].Rows.Count > 0 ? ds.Tables[30].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[30].Rows.Count > 0)
                {
                    DvBit.Visible = true;
                    GvBit.DataSource = ds.Tables[30];
                    GvBit.DataBind();
                }
                else
                {
                    DvBit.Visible = false;
                    GvBit.DataSource = new DataTable();
                    GvBit.DataBind();
                }

                this.lblTrackStar.Text = ds.Tables[31].Rows.Count > 0 ? ds.Tables[31].Rows[0][0].ToString() : "No Record found.";
                this.lblDirectGate.Text = ds.Tables[32].Rows.Count > 0 ? ds.Tables[32].Rows[0][0].ToString() : "No Record found.";
                this.lblSaddleNo.Text = ds.Tables[33].Rows.Count > 0 ? ds.Tables[33].Rows[0][0].ToString() : "No Record found.";
                this.lblLiking.Text = ds.Tables[34].Rows.Count > 0 ? ds.Tables[34].Rows[0][0].ToString() : "No Record found.";
                this.lblRunningStyle.Text = ds.Tables[35].Rows.Count > 0 ? ds.Tables[35].Rows[0][0].ToString() : "No Record found.";
                //this.lblGoodHabit.Text = ds.Tables[36].Rows.Count > 0 ? ds.Tables[36].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[36].Rows.Count > 0)
                {
                    dvHabit.Visible = true;
                    GvHabit.DataSource = ds.Tables[36];
                    GvHabit.DataBind();
                }
                else
                {
                    dvHabit.Visible = false;
                    GvHabit.DataSource = new DataTable();
                    GvHabit.DataBind();
                }

                //this.lblBadHabit.Text = ds.Tables[37].Rows.Count > 0 ? ds.Tables[37].Rows[0][0].ToString() : "No Record found.";
                //this.lblOtherHabit.Text = ds.Tables[38].Rows.Count > 0 ? ds.Tables[38].Rows[0][0].ToString() : "No Record found.";
                //this.lblVet.Text = ds.Tables[39].Rows.Count > 0 ? ds.Tables[39].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[39].Rows.Count > 0)
                {
                    dvVietProblem.Visible = true;
                    GvVietProblem.DataSource = ds.Tables[39];
                    GvVietProblem.DataBind();
                }
                else
                {
                    dvVietProblem.Visible = false;
                    GvVietProblem.DataSource = new DataTable();
                    GvVietProblem.DataBind();
                }

                //this.lblMyObservationShort.Text = ds.Tables[40].Rows.Count > 0 ? ds.Tables[40].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[40].Rows.Count > 0)
                {
                    dvObservation.Visible = true;
                    GvObservation.DataSource = ds.Tables[40];
                    GvObservation.DataBind();
                }
                else
                {
                    dvObservation.Visible = false;
                    GvObservation.DataSource = new DataTable();
                    GvObservation.DataBind();
                }

                //this.lblMyObservationLong.Text = ds.Tables[41].Rows.Count > 0 ? ds.Tables[41].Rows[0][0].ToString() : "No Record found.";
                this.lblHorseBandage.Text = ds.Tables[42].Rows.Count > 0 ? ds.Tables[42].Rows[0][0].ToString() : "No Record found.";
                
               // this.lblOwnerStud.Text = ds.Tables[43].Rows.Count > 0 ? ds.Tables[43].Rows[0][0].ToString() : "No Record found.";
                if (ds.Tables[43].Rows.Count > 0)
                {
                    dvOwnerStud.Visible = true;
                    GvOwnerStud.DataSource = ds.Tables[43];
                    GvOwnerStud.DataBind();
                }
                else
                {
                    dvOwnerStud.Visible = false;
                    GvOwnerStud.DataSource = new DataTable();
                    GvOwnerStud.DataBind();
                }

                this.lblCurrentForm.Text = ds.Tables[44].Rows.Count > 0 ? ds.Tables[44].Rows[0][0].ToString() : "No Record found.";

				if (ds.Tables[45].Rows.Count > 0)
				{
					dvDistancePerformanceOld.Visible = true;
					GvDistancePerformanceOld.DataSource = ds.Tables[45];
					GvDistancePerformanceOld.DataBind();
				}
				else
				{
					dvDistancePerformanceOld.Visible = false;
					GvDistancePerformanceOld.DataSource = new DataTable();
					GvDistancePerformanceOld.DataBind();
				}

				if (ds.Tables[46].Rows.Count > 0)
				{
					dvclssperold.Visible = true;
					GvClassPerformanceOld.DataSource = ds.Tables[46];
					GvClassPerformanceOld.DataBind();
				}
				else
				{
					dvclssperold.Visible = false;
					GvClassPerformanceOld.DataSource = new DataTable();
					GvClassPerformanceOld.DataBind();
				}

                if (ds.Tables[47].Rows.Count > 0)
                {
                    Div6.Visible = true;
                    GdviewHorseSwimming.DataSource = ds.Tables[47];
                    GdviewHorseSwimming.DataBind();
                }
                else
                {
                    Div6.Visible = false;
                    GdviewHorseSwimming.DataSource = new DataTable();
                    GdviewHorseSwimming.DataBind();
                }

                if (ds.Tables[48].Rows.Count > 0)
                {
                    Div8.Visible = true;
                    GdvTreadmill.DataSource = ds.Tables[48];
                    GdvTreadmill.DataBind();
                }
                else
                {
                    Div6.Visible = false;
                    GdvTreadmill.DataSource = new DataTable();
                    GdvTreadmill.DataBind();
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "dialogCurrentMission('" + message + "');", true);
        }


        protected void BtnAddClick(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void SaveData()
        {
            try
            {
                if (txtbxHorseName.Text == "")
                {
                    string message = "Please select Horse Name";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    int status = 0;
                    var chkatfoal = string.Empty;
                    if (chkbxLateFoal.Checked)
                        chkatfoal = "Yes";
                    else
                        chkatfoal = "No";

                    var hopechkatfoal = string.Empty;
                    if (chkbxHopeLateFoal.Checked)
                        hopechkatfoal = "Yes";
                    else
                        hopechkatfoal = "No";

                    var chkboard = string.Empty;
                    if (chkbxAbroad.Checked)
                        chkboard = "Yes";
                    else
                        chkboard = "No";


                    var carrytopweight = string.Empty;
                    if (chkbxCarryTopWeight.Checked)
                        carrytopweight = "Yes";
                    else
                        carrytopweight = "No";

                    var cantender = string.Empty;
                    if (chkbxCantender.Checked)
                        cantender = "Yes";
                    else
                        cantender = "No";

                    var classicmaterial = string.Empty;
                    if (chkbxProvedClassicMaterial.Checked)
                        classicmaterial = "Yes";
                    else
                        classicmaterial = "No";

                    var rainydayperformer = string.Empty;
                    if (chkbxRainyDayPerformer.Checked)
                        rainydayperformer = "Yes";
                    else
                        rainydayperformer = "No";


                    var whipmusrequired = string.Empty;
                    if (chkbxWhipMustRequired.Checked)
                        whipmusrequired = "Yes";
                    else
                        whipmusrequired = "No";


                    //var physictype = string.Empty;
                    //if (chkbxPhysicType.Checked)
                    //    physictype = "Yes";
                    //else
                    //    physictype = "No";

                    var udnervaluehorse = string.Empty;
                    if (chkbxUndervalueHorse.Checked)
                        udnervaluehorse = "Yes";
                    else
                        udnervaluehorse = "No";

                    var physictype = string.Empty;
                    if (!(rdbtnPhysicType.SelectedItem == null))
                        physictype = rdbtnPhysicType.SelectedItem.Text;

                    var bodyshape = string.Empty;
                    if (!(rdbtnBodyShape.SelectedItem == null))
                        bodyshape = rdbtnBodyShape.SelectedItem.Text;

                    status = new MasterHorseBL().HorseCompleteInformation(
                                Convert.ToInt32(hdnHorseNameId.Value),
                                (rdbtnDobType.SelectedValue == "") ? string.Empty : rdbtnDobType.SelectedItem.Text,
                                chkatfoal,
                                hopechkatfoal,
                                txtbxDateofDeath.Text,
                                Convert.ToInt32(drpdwnColor.SelectedItem.Value),
                                Convert.ToInt32(drpdwnBirthNation.SelectedItem.Value),
								(txtbxSireName.Text.Equals(""))? string.Empty : (hdnfieldSireNameID.Value == null) ? string.Empty : hdnfieldSireNameID.Value,
                                (txtbxDamName.Text.Equals("")) ? string.Empty : (hdnfieldDamNameID.Value == null) ? string.Empty : hdnfieldDamNameID.Value,
                                chkboard,
                                (txtbxBirthStud.Text.Equals("")) ? string.Empty : (hdnfieldbirthstudNameID.Value == null) ? string.Empty : hdnfieldbirthstudNameID.Value,
                                (txtbxBreeder.Text.Equals("")) ? string.Empty : (hdnbreederNameID.Value == null) ? string.Empty : hdnbreederNameID.Value,
                                cantender,
                                classicmaterial,
                                txtbxLineage.Text,
                                udnervaluehorse,
                                physictype, //txtbxPhysicType.Text, 
                                txtbxBirthDefect.Text, 
                                txtbxDosageIndex.Text, txtbxDosageProfile.Text, txtbxCenterofDistribution.Text,
                                rainydayperformer,
                                whipmusrequired,
                                rdbtnProfile.SelectedItem.Text,
                                1,
                                "Insert",
                                carrytopweight,
                                bodyshape);

                    if (status == 2)
                    {
                        if (txtbxSireName.Text.Equals(""))
                            hdnfieldSireNameID.Value = string.Empty;
                        if (txtbxDamName.Text.Equals(""))
                            hdnfieldDamNameID.Value = string.Empty;
                        if (txtbxBirthStud.Text.Equals(""))
                            hdnfieldbirthstudNameID.Value = string.Empty;
                        if (txtbxBreeder.Text.Equals(""))
                            hdnbreederNameID.Value = string.Empty;
                        string msg = "Duplicate Record.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msg + "');", true);
                        //ClearAllSelection(this);
                        //BindData();
                    }
                    else if (status > 0)
                    {
                        if (txtbxSireName.Text.Equals(""))
                            hdnfieldSireNameID.Value = string.Empty;
                        if (txtbxDamName.Text.Equals(""))
                            hdnfieldDamNameID.Value = string.Empty;
                        if (txtbxBirthStud.Text.Equals(""))
                            hdnfieldbirthstudNameID.Value = string.Empty;
                        if (txtbxBreeder.Text.Equals(""))
                            hdnbreederNameID.Value = string.Empty;

                        string msg = "Record added successfully.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
                        this.MainHorseBind(hdnHorseNameId.Value);
                        DisableAllField(this, "Disable");
                        DisableButton("Disable");
                     
                    }
                    else
                    {
                        if (txtbxSireName.Text.Equals(""))
                            hdnfieldSireNameID.Value = string.Empty;
                        if (txtbxDamName.Text.Equals(""))
                            hdnfieldDamNameID.Value = string.Empty;
                        if (txtbxBirthStud.Text.Equals(""))
                            hdnfieldbirthstudNameID.Value = string.Empty;
                        if (txtbxBreeder.Text.Equals(""))
                            hdnbreederNameID.Value = string.Empty;
                        string msg1 = "Incorrect Information.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg1 + "');", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msg1 + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
               // Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                var msg = "Incorrect Information";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
            }
        }

        protected void btnHorseShow_Click(object sender, EventArgs e)
        {

            try
            {
                AchivementBindData();
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                var msg = "Incorrect Information";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
            }
        }


        private void AchivementBindData()
        {
            DataTable dt = new CardsBL().GetHorseAchivementData(0, Convert.ToInt32(hdnHorseNameId.Value));
            if (dt.Rows.Count > 0)
            {
                GvHorseAchivements.DataSource = dt;
                GvHorseAchivements.DataBind();
            }
            else
            {
                GvHorseAchivements.DataSource = new DataTable();
                GvHorseAchivements.DataBind();
            }
        }
        private void BindData()
        {
            DataSet ds=null;
            if (hdnAchieveHorseId.Value != "")
            {
               ds = Bl.GetHorseNameWithCombination(Convert.ToInt32(hdnAchieveHorseId.Value), "HorseAchivements");
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvHorseAchivements.DataSource = ds.Tables[0];
                    GvHorseAchivements.DataBind();
                }
                else
                {
                    GvHorseAchivements.DataSource = new DataTable();
                    GvHorseAchivements.DataBind();
                }
            }
            else
            {
                    GvHorseAchivements.DataSource = new DataTable();
                    GvHorseAchivements.DataBind();
            }
        }


        protected void GvHorseAchivements_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnAcheiveAdd.Text = "Update";
                GridViewRow row = GvHorseAchivements.SelectedRow;
                HiddenField hdnfieldHorseNameAchivement = (HiddenField)row.FindControl("hdnfieldHorseNameAchivement");
                HiddenField hdnfieldHorseNameIDAchievment = (HiddenField)row.FindControl("hdnfieldHorseNameIDAchievment");
                HiddenField hdnfieldDivisionRaceIDAchvment = (HiddenField)row.FindControl("hdnfieldDivisionRaceIDAchvment");
                var dataKey = GvHorseAchivements.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    //ClearAllSelection(this);
                    hdnHorseNameId.Value = hdnfieldHorseNameIDAchievment.Value;
                    txtbxAchieveHorseName.Text = hdnfieldHorseNameAchivement.Value;
                    ViewState["horseAchivementID"] = dataKey.Value;
                    hdnfieldDivisionRaceNameAchivement.Value = hdnfieldDivisionRaceIDAchvment.Value;
                    if (!row.Cells[1].Text.Contains("&nbsp;"))
                    {
                        txtbxPosition.Text = row.Cells[1].Text;
                    }
                    if (!row.Cells[2].Text.Contains("&nbsp;"))
                    {
                        txtbxRaceName.Text = row.Cells[2].Text;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }


        protected void btnAcheiveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                if (btnAcheiveAdd.Text.Equals("Add"))
                    {
                        status = Bl.InsertAchivements(
                            0
                            ,Convert.ToInt32(hdnAchieveHorseId.Value)
                            ,0
                            ,0,0
                            ,"__-__-____"
                            ,String.Empty
                            , txtbxPosition.Text
                            , Convert.ToInt32(hdnfieldDivisionRaceNameAchivement.Value)
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , 1
                            ,"Insert");
                    }
                else if (btnAcheiveAdd.Text.Equals("Update"))
                    {
                        status = Bl.InsertAchivements(
                            (int)ViewState["horseAchivementID"]
                            , Convert.ToInt32(hdnAchieveHorseId.Value)
                            ,0,0,0
                            , "__-__-____"
                            , String.Empty
                            , txtbxPosition.Text
                            ,Convert.ToInt32(hdnfieldDivisionRaceNameAchivement.Value)
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , String.Empty
                            , 1
                            , "Update");
                        //btnSave.Text = "Add";
                    }
                    if (status == 1)
                    {
                        var message = "Record added successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        AchivementBindData();
                        ClearAchivementTab();

                    }
                    else if (status == 2)
                    {
                        var message = "Record updated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        AchivementBindData();
                        ClearAchivementTab();
                        btnAcheiveAdd.Text = "Add";
                    }
                    else if (status == 5)
                    {
                        var message = "Record activated successfully.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        ClearAchivementTab();
                        btnAcheiveAdd.Text = "Add";
                        AchivementBindData();
                    }
                    else if (status == 4)
                    {
                        string message = "Record already exist.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        btnAcheiveAdd.Text = "Add";
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        protected void btnDeleteAchivement_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                status = Bl.InsertAchivements(
                        (int)ViewState["horseAchivementID"]
                        , 0
                        , 0, 0, 0
                        , "__-__-____"
                        , String.Empty
                        , string.Empty
                        , 0
                        , String.Empty
                        , String.Empty
                        , String.Empty
                        , String.Empty
                        , String.Empty
                        , String.Empty
                        , String.Empty
                        , 1
                        ,"Delete");
                if (status == 3)
                {
                    var message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    AchivementBindData();
                    ClearAchivementTab();
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
                var message = "Incorrect Information.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }


        private void ClearAchivementTab()
        {
            txtbxPosition.Text = string.Empty;
            txtbxRaceName.Text = string.Empty;
            hdnfieldDivisionRaceNameAchivement.Value = string.Empty;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                status = new MasterHorseBL().HorseCompleteInformation(
                            Convert.ToInt32(hdnHorseNameId.Value),
                            "",
                            "",
                            "",
                            "__-__-____",
                            0,
                            0,
                            "",//Convert.ToInt32(drpdwnSireName.SelectedItem.Value)
                            "",//Convert.ToInt32(drpdwnDamName.SelectedItem.Value)
                            "",
                            "",//Convert.ToInt32(drpdwnStud.SelectedItem.Value)
                            "",//dropdownbreeder
                            "",
                            "",
                            "",
                            "",
                            "", "", "", "", "",
                            "",
                            "",
                            "",
                            1,
                            "Delete",
                            "","");

                if (status == 2)
                {
                    string msg = "Duplicate Record.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
                }
                else if (status > 0)
                {
                    string msg = "Record deleted successfully.";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msg + "');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
                    ClearAllSelection(this);
                    //BindData();
                }
                else
                {
                    string msg1 = "Incorrect Information.";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msg1 + "');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg1 + "');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information.');", true);
                var msg = "Incorrect Information";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
            }

        }


        protected void btnAchieveShow_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtbxAchieveHorseName.Equals(""))
                {
                    var message = "Please select Horse Name.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }
                else
                {
                    string[] horseName = txtbxAchieveHorseName.Text.Split('{');

                    if (horseName.Length == 1)
                    {
                        var message = "Horse Name not found.";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        //hdnAchieveHorseId.Value = Convert.ToString(Bl.GetHorseId(horseName[0], horseName[1].Substring(0, horseName[1].Length - 1)));
                        DataTable dt = Bl.GetHorseId(horseName[0], horseName[1].Substring(0, horseName[1].Length - 1));
                        if (dt.Rows.Count > 0)
                        {
                          //  hdnHorseId.Value = dt.Rows[0][1].ToString();
                            hdnAchieveHorseId.Value = dt.Rows[0][1].ToString();
                            lblHorseIDNameID.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][1].ToString();
                        }
                        BindData();
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

        protected void btnAchieveClear_OnClick(object sender, EventArgs e)
        {
            ClearAchivementTab();
        }


        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("Horse"))
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
                    var dtErrorResult = new MasterHorseBL().Import30(dt, "MainHorse");
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
                using (DataSet ds = new MasterHorseBL().GetExport(0, "MainHorse"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("HorseID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse.xlsx");
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


        protected void btnExportAchivement_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = new MasterHorseBL().GetExport(0, "MainHorseAchivement"))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("HorseID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("Horse");
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
                            Response.AddHeader("content-disposition", "attachment;filename=Horse.xlsx");
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
          //  Session.Remove("ProfessionalName");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeMe()", true);
        }
     
    }
}