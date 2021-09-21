using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;
using System.Net;


namespace VKATalk.Master
{
    using System.Configuration;
    using System.Data.OleDb;
    using System.IO;

    using OfficeOpenXml;

    public partial class AddScaleOfWeight : System.Web.UI.Page
    {
        MasterBL Bl = new MasterBL();
        static int LastIndex = 0;
        static int UserID_FK = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				//rbWFC.Checked = true;
				DivWFC.Visible = true;
				BindDropDown(drpdwnCenter, "Center", "CenterName", "ID");
                drpdwnCenter.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnFromYear, "Year", "YearName", "YearID");
                drpdwnFromYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTillYear, "Year", "YearName", "YearID");
                drpdwnTillYear.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnFromSeason, "Season", "SeasonName", "SeasonID");
                drpdwnFromSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));
                BindDropDown(drpdwnTillSeason, "Season", "SeasonName", "SeasonID");
                drpdwnTillSeason.Items.Insert(0, new ListItem("-- Please select --", "-1"));

                BindDropDown(drpdwnNation, "MasterNation", "Nation", "NationID");
                drpdwnNation.Items.Insert(0, new ListItem("-- Please select --", "-1"));

				BindDropDown(drpdwnGender, "HorseSex", "HorseSex", "HorseSexID");
				drpdwnGender.Items.Insert(0, new ListItem("-- Please select --", "-1"));

				BindDropDown(drpdwnAgeCondition, "AgeCondition", "AgeCondition", "AgeConditionID");
				drpdwnAgeCondition.Items.Insert(0, new ListItem("-- Please select --", "-1"));

				
            }
			BindData();
		}

        private void BindData()
        {
            DataTable dt;
            dt = Bl.GetMasterData("ScaleofWeight");
            if (dt.Rows.Count > 0)
            {
                GvCommon.DataSource = dt;
                GvCommon.DataBind();
            }
            else
            {
                GvCommon.DataSource = new DataTable();
                GvCommon.DataBind();
            }
        }


		//protected void rbWFC_click(object sender, EventArgs e)
		//{
		//	DivWFC.Visible = true;
		//	DivWFA.Visible = false;
		//	DivGender.Visible = false;
		//}
		//protected void rbWFA_click(object sender, EventArgs e)
		//{
		//	DivWFC.Visible = false;
		//	DivWFA.Visible = true;
		//	DivGender.Visible = false;
		//}

		//protected void rdvbtnGender_click(object sender, EventArgs e)
		//{
		//	DivWFC.Visible = false;
		//	DivWFA.Visible = false;
		//	DivGender.Visible = true;
		//}

		/// <summary>
		/// Bind Dropdown at the time of page load
		/// </summary>
		/// <param name="ddl"></param>
		/// <param name="TableName_"></param>
		/// <param name="TextField"></param>
		/// <param name="ValueField"></param>
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
        /// Submit the Form Request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_click(object sender, EventArgs e)
        {
            
            VKATalkClassLayer.ScaleofWeight clsType = new VKATalkClassLayer.ScaleofWeight();
            int status = 0;
            int process = 0;
			try
			{
				if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Class"))
				{
					process = 1;
					//if (txtbxHandicapWeight.Text == "")
					//{
					//	string message = "Please enter Handicap Weight.";
					//	ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
					//	process = 0;

					//}
					//else
					//{
					//	process = 1;
					//}
				}
				if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Gender"))
				{
					process = 1;
				}
				if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Age"))
				{
					if(rdbtnWeightSystemType.SelectedItem.Text.Equals("Handicap Weight"))
					{

						if (drpdwnMonth.SelectedItem.Value == "-1")
						{
							string message = "Please select Month.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							process = 0;
						}
						else if (drpdwnDistanceParameter.SelectedItem.Value == "-1")
						{
							string message = "Please select Distance Parameter.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							process = 0;
						}
						else if (drpdwnNation.SelectedItem.Value == "-1")
						{
							string message = "Please select Nation.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							process = 0;
						}
						else if (drpdwnAgeParameter.SelectedItem.Value == "-1")
						{
							string message = "Please select Age Parameter.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							process = 0;
						}
						else
						{
							process = 1;
						}
					}
					else if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Weight Addition"))
					{
						process = 1;
					}
					else if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Age Condition"))
					{
						if (txtbxAgeHandicapWeight.Text == "")
						{
							string message = "Please select Handicap Weight.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							process = 0;
						}
						else
						{
							process = 1;
						}

					}

					
				}



				if (process == 1)
				{
					clsType.CenterID = Convert.ToInt32(drpdwnCenter.SelectedItem.Value);
					clsType.FromYearID = Convert.ToInt32(drpdwnFromYear.SelectedItem.Value);
					clsType.TillYearID = Convert.ToInt32(drpdwnTillYear.SelectedItem.Value);
					clsType.FromSeasonID = Convert.ToInt32(drpdwnFromSeason.SelectedItem.Value);
					clsType.TillSeasonID = Convert.ToInt32(drpdwnTillSeason.SelectedItem.Value);
					//clsType.ScaleofWeightType = rbWFC.Checked.Equals(false) ? rbWFA.Text : rbWFC.Text;
					if(rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Class"))
					{
						clsType.ScaleofWeightType = rdbtnScaleofWeightFirst.SelectedItem.Text;
						clsType.CIHandicapRating = txtbxClass1.Text;
						clsType.CIIHandicapRating = txtbxClass2.Text;
						clsType.CIIIHandicapRating = txtbxClass3.Text;
						clsType.CIVHandicapRating = txtbxClass4.Text;
						clsType.CVHandicapRating = txtbxClass5.Text;
						clsType.HandicapWeight = txtbxHandicapWeight.Text;
						clsType.WeightSystemType = string.Empty;
						clsType.Month = string.Empty;
						clsType.DistanceParameter = string.Empty;
						clsType.NationID = -1;
						clsType.AgeParameter = string.Empty;
						clsType.AgeHandicapWeight = string.Empty;
						clsType.HorseGender = string.Empty;
						clsType.HorseHandicapWeight = string.Empty;
						clsType.AgeCondition = "-1";
					}
					else if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Age"))
					{
						if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Handicap Weight"))
						{

							clsType.ScaleofWeightType = rdbtnScaleofWeightFirst.SelectedItem.Text;
							clsType.CIHandicapRating = string.Empty;
							clsType.CIIHandicapRating = string.Empty;
							clsType.CIIIHandicapRating = string.Empty;
							clsType.CIVHandicapRating = string.Empty;
							clsType.CVHandicapRating = string.Empty;
							clsType.HandicapWeight = string.Empty;
							clsType.WeightSystemType = rdbtnWeightSystemType.SelectedItem.Text;
							clsType.Month = drpdwnMonth.SelectedItem.Text;
							clsType.DistanceParameter = drpdwnDistanceParameter.SelectedItem.Text;
							clsType.NationID = Convert.ToInt32(drpdwnNation.SelectedItem.Value);
							clsType.AgeParameter = drpdwnAgeParameter.SelectedItem.Text;
							clsType.AgeHandicapWeight = string.Empty;
							clsType.HorseGender = string.Empty;
							clsType.HorseHandicapWeight = txtbxHandicapWeightWeightforAge.Text;
							clsType.AgeCondition = "-1";
						}
						else if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Weight Addition"))
						{
							clsType.ScaleofWeightType = rdbtnScaleofWeightFirst.SelectedItem.Text;
							clsType.CIHandicapRating = string.Empty;
							clsType.CIIHandicapRating = string.Empty;
							clsType.CIIIHandicapRating = string.Empty;
							clsType.CIVHandicapRating = string.Empty;
							clsType.CVHandicapRating = string.Empty;
							clsType.HandicapWeight = string.Empty;
							clsType.WeightSystemType = rdbtnWeightSystemType.SelectedItem.Text;
							clsType.Month = string.Empty;
							clsType.DistanceParameter = string.Empty;
							clsType.NationID = -1;
							clsType.AgeParameter = string.Empty;
							clsType.AgeHandicapWeight = string.Empty;
							clsType.HorseGender = string.Empty;
							clsType.HorseHandicapWeight = string.Empty;
							clsType.AgeCondition = "-1";
						}
						else if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Age Condition"))
						{
							clsType.ScaleofWeightType = rdbtnScaleofWeightFirst.SelectedItem.Text;
							clsType.CIHandicapRating = string.Empty;
							clsType.CIIHandicapRating = string.Empty;
							clsType.CIIIHandicapRating = string.Empty;
							clsType.CIVHandicapRating = string.Empty;
							clsType.CVHandicapRating = string.Empty;
							clsType.HandicapWeight = string.Empty;
							clsType.WeightSystemType = rdbtnWeightSystemType.SelectedItem.Text;
							clsType.Month = string.Empty;
							clsType.DistanceParameter = string.Empty;
							clsType.NationID = -1;
							clsType.AgeParameter = string.Empty;
							clsType.AgeHandicapWeight = txtbxAgeHandicapWeight.Text;
							clsType.HorseGender = string.Empty;
							clsType.HorseHandicapWeight = string.Empty;
							clsType.AgeCondition = drpdwnAgeCondition.SelectedItem.Value;
						}
						
					}
					else if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Gender"))
					{
						clsType.ScaleofWeightType = rdbtnScaleofWeightFirst.SelectedItem.Text;
						clsType.CIHandicapRating = string.Empty;
						clsType.CIIHandicapRating = string.Empty;
						clsType.CIIIHandicapRating = string.Empty;
						clsType.CIVHandicapRating = string.Empty;
						clsType.CVHandicapRating = string.Empty;
						clsType.HandicapWeight = string.Empty;
						clsType.WeightSystemType = string.Empty;
						clsType.Month = string.Empty;
						clsType.DistanceParameter = string.Empty;
						clsType.NationID = -1;
						clsType.AgeParameter = string.Empty;
						clsType.AgeHandicapWeight = string.Empty;
						clsType.HorseGender = drpdwnGender.SelectedItem.Value;
						clsType.HorseHandicapWeight = txtbxHandicapWeightGender.Text;
						clsType.AgeCondition = "-1";
					}

					if (btnsubmit.Text.Equals("Add"))
					{
						status = Bl.InsertUpdateMasterScaleofWeight(clsType, UserID_FK, "Insert", 0);
					}
					else
					{
						status = Bl.InsertUpdateMasterScaleofWeight(clsType, UserID_FK, "Update", Convert.ToInt32(ViewState["GridViewRowID"]));
					}
						if (status == 1)
						{
							string message = "Record added successfully.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							BindData();
							ClearAllSelection(this);
						}
						else if (status == 2)
						{
							string message = "Record updated successfully.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							BindData();
							ClearAllSelection(this);
							btnsubmit.Text = "Add";
							ViewState["GridViewRowID"] = string.Empty;
						}
						else if (status == 4)
						{
							string message = "Record already exist.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							ClearAllSelection(this);
							btnsubmit.Text = "Add";
						}
						else if (status == 5)
						{
							var message = "Record activated successfully.";
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
							BindData();
							ClearAllSelection(this);
							btnsubmit.Text = "Add";
						}
						else
						{
							ErrorHandling.CheckEachSteps(Convert.ToString(status));
							string message = "Issue in Record. (Status) : " + status;
							ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
						}
						if (chkbxFix.Checked)
						{
							drpdwnCenter.Items.FindByValue(Convert.ToString(clsType.CenterID)).Selected = true;
							drpdwnFromYear.Items.FindByValue(Convert.ToString(clsType.FromYearID)).Selected = true;
							drpdwnTillYear.Items.FindByValue(Convert.ToString(clsType.TillYearID)).Selected = true;
							drpdwnFromSeason.Items.FindByValue(Convert.ToString(clsType.FromSeasonID)).Selected = true;
							drpdwnTillSeason.Items.FindByValue(Convert.ToString(clsType.TillSeasonID)).Selected = true;
							if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Age"))
							{
								if (chkbxAgeParameterfix.Checked)
								{
									drpdwnMonth.Items.FindByText(Convert.ToString(clsType.Month)).Selected = true;
									drpdwnDistanceParameter.Items.FindByText(Convert.ToString(clsType.DistanceParameter)).Selected = true;
									drpdwnNation.Items.FindByValue(Convert.ToString(clsType.NationID)).Selected = true;
									rdbtnWeightSystemType.Items.FindByText(clsType.WeightSystemType).Selected = true;
								}
							}
						}
					//}
				}
			}
			catch (Exception ex)
			{
				ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowHideDiv()", true);
				ErrorHandling.SendErrorToText(ex);
				string message = "Issue in Record.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
			}

        }


		protected void rdbtnWeightSystemType_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Age Condition"))
			{
				dvHandicapWeight.Visible = false;
				dvAgeCondition.Visible = true;
			}
			else if (rdbtnWeightSystemType.SelectedItem.Text.Equals("Weight Addition"))
			{
				dvHandicapWeight.Visible = false;
				dvAgeCondition.Visible = false;
			}
			else
			{
				dvHandicapWeight.Visible = true;
				dvAgeCondition.Visible = false;
			}
		}


		protected void rdbtnScaleofWeightFirst_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Class"))
			{
				DivWFC.Visible = true;
				DivWFA.Visible = false;
				DivGender.Visible = false;
			}
			else if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Age"))
			{
				DivWFC.Visible = false;
				DivWFA.Visible = true;
				DivGender.Visible = false;
			}
			else if (rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Gender"))
			{
				DivWFC.Visible = false;
				DivWFA.Visible = false;
				DivGender.Visible = true;
			}
		}

		protected void GvCommon_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnsubmit.Text = "Update";
                GridViewRow row = GvCommon.SelectedRow;
                HiddenField hdnval = (HiddenField)row.FindControl("hdnfieldStatus");
                var dataKey = GvCommon.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    ClearAllSelection(this);
                    ViewState["GridViewRowID"] = dataKey.Value;
                    drpdwnCenter.Items.FindByText(hdnval.Value).Selected = true;
                    drpdwnFromYear.Items.FindByText(row.Cells[2].Text).Selected = true;
                    if (!(row.Cells[3].Text.Equals("&nbsp;") || row.Cells[3].Text.Equals("-1")))
                    {
                        drpdwnTillYear.Items.FindByText(row.Cells[3].Text).Selected = true;
                    }
                    drpdwnFromSeason.Items.FindByText(row.Cells[4].Text).Selected = true;
                    if (!(row.Cells[5].Text.Equals("&nbsp;") || row.Cells[5].Text.Equals("-1")))
                    {
                        drpdwnTillSeason.Items.FindByText(row.Cells[5].Text).Selected = true;
                    }
           
                    if (row.Cells[6].Text.Equals("Weight for Class"))
                    {
						//                  rbWFC.Checked = true;
						//                  rbWFA.Checked = false;
						//rdvbtnGender.Checked = false;
						rdbtnScaleofWeightFirst.Items.FindByText("Weight for Class").Selected = true;
						DivWFC.Visible = true;
						DivWFA.Visible = false;
						DivGender.Visible = false;
					}
					else if (row.Cells[6].Text.Equals("Weight for Gender"))
					{
						rdbtnScaleofWeightFirst.Items.FindByText("Weight for Gender").Selected = true;

						DivWFC.Visible = false;
						DivWFA.Visible = false;
						DivGender.Visible = true;
					}
                    else
                    {
						rdbtnScaleofWeightFirst.Items.FindByText("Weight for Age").Selected = true;

						DivWFC.Visible = false;
						DivWFA.Visible = true;
						DivGender.Visible = false;
					}


					// ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowHideDiv()", true);

					if (!row.Cells[7].Text.Equals("&nbsp;"))
                    {
                        if (row.Cells[6].Text.Equals("Weight for Class"))
                        {
                            txtbxClass1.Text = row.Cells[7].Text;
                        }

                    }
                    if (!row.Cells[8].Text.Equals("&nbsp;"))
                    {
                        if (row.Cells[6].Text.Equals("Weight for Class"))
                        {
                            txtbxClass2.Text = row.Cells[8].Text;
                        }

                    }
                    if (!row.Cells[9].Text.Equals("&nbsp;"))
                    {
                        if (row.Cells[6].Text.Equals("Weight for Class"))
                        {
                            txtbxClass3.Text = row.Cells[9].Text;
                        }

                    }
                    if (!row.Cells[10].Text.Equals("&nbsp;"))
                    {
                        if (row.Cells[6].Text.Equals("Weight for Class"))
                        {
                            txtbxClass4.Text = row.Cells[10].Text;
                        }

                    }
                    if (!row.Cells[11].Text.Equals("&nbsp;"))
                    {
                        if (row.Cells[6].Text.Equals("Weight for Class"))
                        {
                            txtbxClass5.Text = row.Cells[11].Text;
                        }

                    }



					if (!row.Cells[12].Text.Equals("&nbsp;"))
					{
						if (row.Cells[12].Text.Equals("Handicap Weight"))
						{
							//rdbtnWeightSystemType.Items.FindByText(row.Cells[12].Text).Selected = true;
							rdbtnWeightSystemType.Items.FindByText(row.Cells[12].Text).Selected = true;
							dvAgeCondition.Visible = false;
							dvHandicapWeight.Visible = true;
						}
						else if (row.Cells[12].Text.Equals("Weight Addition"))
						{
							rdbtnWeightSystemType.Items.FindByText(row.Cells[12].Text).Selected = true;
							dvAgeCondition.Visible = false;
							dvHandicapWeight.Visible = false;

						}
						else if (row.Cells[12].Text.Equals("Age Condition"))
						{
							rdbtnWeightSystemType.Items.FindByText(row.Cells[12].Text).Selected = true;
							dvAgeCondition.Visible = true;
							dvHandicapWeight.Visible = false;
						}
						


					}

					if (!row.Cells[13].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Age"))
						{
							drpdwnMonth.Items.FindByText(row.Cells[13].Text).Selected = true;
						}

					}

					if (!row.Cells[14].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Age"))
						{
							drpdwnDistanceParameter.Items.FindByText(WebUtility.HtmlDecode(row.Cells[14].Text)).Selected = true;
						}

					}

					if (!row.Cells[15].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Age"))
						{
							drpdwnNation.Items.FindByText(row.Cells[15].Text).Selected = true;
						}

					}

					if (!row.Cells[16].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Age"))
						{
							drpdwnAgeParameter.Items.FindByText(row.Cells[16].Text).Selected = true;
						}
						
					}
					if (!row.Cells[17].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Age"))
						{
							drpdwnAgeCondition.Items.FindByText(row.Cells[17].Text).Selected = true;
						}

					}

					if (!row.Cells[19].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Class"))
						{
							txtbxHandicapWeight.Text = row.Cells[19].Text;
						}
						else if (row.Cells[6].Text.Equals("Weight for Age"))
						{
							if (row.Cells[12].Text.Equals("Handicap Weight"))
							{
								txtbxHandicapWeightWeightforAge.Text = row.Cells[19].Text;
							}
							else if (row.Cells[12].Text.Equals("Age Condition"))
							{
								txtbxAgeHandicapWeight.Text = row.Cells[19].Text;
							}
							
						}
						else if (row.Cells[6].Text.Equals("Weight for Class"))
						{
							txtbxHandicapWeightGender.Text = row.Cells[19].Text;
						}
						
					}

					if (!row.Cells[18].Text.Equals("&nbsp;"))
					{
						if (row.Cells[6].Text.Equals("Weight for Gender"))
						{
							drpdwnGender.Items.FindByText(row.Cells[18].Text).Selected = true;
						}
						
					}
				}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowHideDiv()", true);
                ErrorHandling.SendErrorToText(ex);
                var message = "Issue in Record.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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

                if (x.HasControls())
                {
                    ClearAllSelection(x);
                }
            }

			if (!rdbtnScaleofWeightFirst.SelectedItem.Text.Equals("Weight for Class"))
			{
				rdbtnScaleofWeightFirst.ClearSelection();
				rdbtnScaleofWeightFirst.Items.FindByText("Weight for Class").Selected = true;
				DivWFC.Visible = true;
				DivWFA.Visible = false;
				DivGender.Visible = false;
			}

			if (!rdbtnWeightSystemType.SelectedItem.Text.Equals("Handicap Weight"))
			{
				rdbtnWeightSystemType.ClearSelection();
				rdbtnWeightSystemType.Items.FindByText("Handicap Weight").Selected = true;
				dvHandicapWeight.Visible = true;
				dvAgeCondition.Visible = false;
			}
		}

		protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                VKATalkClassLayer.ScaleofWeight clsType = new VKATalkClassLayer.ScaleofWeight();
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var status = Bl.InsertUpdateMasterScaleofWeight(clsType, UserID_FK, "Delete", Convert.ToInt32(ViewState["GridViewRowID"]));
                    ClearAllSelection(this);
                    BindData();
                    btnsubmit.Text = "Add";
                    var message = "Record deleted successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GridViewRowID"] = "";
                }
                else
                {
                    var message = "No record Found.";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowHideDiv()", true);
                ErrorHandling.SendErrorToText(ex);
                var message = "Issue found in record.";
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
                //  DataTable dt = Bl.GetMasterData("Year");
                using (DataTable dt = Bl.GetMasterData("ScaleofWeight"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("RowCount");
                        dt.Columns.Remove("SOWID");
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            ExcelWorksheet ws = xp.Workbook.Worksheets.Add("ScaleOfWeight");
                            
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
                            Response.AddHeader("content-disposition", "attachment;filename=ScaleOfWeight.xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        var message = "No Record found.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flupload.HasFile)
                {
                    string FileName = Path.GetFileName(flupload.PostedFile.FileName);
                    if (Path.GetFileNameWithoutExtension(flupload.PostedFile.FileName).Equals("ScaleOfWeight"))
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllSelection(this);
            btnsubmit.Text = "Add";
            //rbWFC.Checked = true;
            //rbWFA.Checked = false;
        }

        private void Import_To_Grid(string FilePath, string Extension)
        {
            try
            {
                string strConn;
                bool hasHeaders = false;
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


                            dtresult = new MasterBL().ImportMasterFiles(dt.Rows[count][1].ToString(), dt.Rows[count][2].ToString(), dt.Rows[count][3].ToString(), dt.Rows[count][4].ToString(),
                                 dt.Rows[count][5].ToString(), dt.Rows[count][6].ToString(), dt.Rows[count][7].ToString(), dt.Rows[count][8].ToString(), dt.Rows[count][9].ToString(),
                                 dt.Rows[count][10].ToString(), dt.Rows[count][11].ToString(), dt.Rows[count][12].ToString(), dt.Rows[count][13].ToString(), dt.Rows[count][14].ToString(),
                                 dt.Rows[count][15].ToString(), dt.Rows[count][16].ToString(), dt.Rows[count][17].ToString(), dt.Rows[count][18].ToString(), dt.Rows[count][19].ToString(),
                                 dt.Rows[count][20].ToString(), dt.Rows[count][21].ToString(), dt.Rows[count][22].ToString(), dt.Rows[count][23].ToString(), dt.Rows[count][24].ToString(),
                                 dt.Rows[count][25].ToString(), dt.Rows[count][26].ToString(), dt.Rows[count][27].ToString(), dt.Rows[count][28].ToString(), dt.Rows[count][29].ToString(),
                                 dt.Rows[count][30].ToString(), dt.Rows[count][31].ToString(), "MasterScaleOfWeight");


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
                    var message = "No Record found.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                var message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
    }
}