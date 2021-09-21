using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VKATalkBusinessLayer;

namespace VKATalk.PopUps
{
    using System.Globalization;

    public partial class HorseRename : System.Web.UI.Page
    {
        MasterHorseBL Bl = new MasterHorseBL();
        int _userId = 1;
        private int _value = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Master != null) Page.Master.FindControl("NavigationMenu").Visible = false;
            if (!Request.QueryString["HorseNameID"].Equals(""))
            {
                _value = Convert.ToInt32(Request.QueryString["HorseNameID"]);
                hdnfldhorseId.Value = Convert.ToString(_value);
            }
            if (!IsPostBack)
            {
                try
                {
                    if (_value != 0)
                    {
                        GetGridviewData();
                        //btnSave.Text = "Update";
                    }
                    else
                    {
                        btnSave.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
                    object[] now = new object[] { "Page_Load (HorsePopup):", DateTime.Now, ", Issue Detail:", ex.Message + ex.StackTrace };
                    ErrorHandling.CheckEachSteps(string.Concat(now));
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Incorrect Information');", true);
                    string message = "Incorrect Information.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
        public static List<string> AddCommentList(string prefixText, int count)
        {
            DataTable dt = new MasterHorseBL().GetHorseNameAutoFiller("CommentList", prefixText);
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
                DataSet ds = Bl.GetHorseNameWithCombination(_value, "HorseName");

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
                this.btnSave.Text = ds.Tables[0].Rows.Count == 2 ? "Update" : "Add";
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        /// <summary>
        /// Bind Horse Name already exist in out Database
        /// </summary>
        private void BindData(DataTable dt)
        {
            try
            {
                DataSet ds = null;

                if (btnSave.Text.Equals("Add"))
                {
                    ds = Bl.GetHorseNameWithCombination(this._value, "HorseRenameInsert");
                }
                else
                {
                    ds = Bl.GetHorseNameWithCombination(this._value, "HorseRenameUpdate");
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
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                int newHorseId = 0;
                DataTable dt;
                int count = this.GvHorseName.Rows.Count;
                if (count <= 2)
                {
                    if (btnSave.Text.Equals("Add"))
                    {
                        dt = this.Bl.HorseNewName(
                            _value,
                            this.txtbxHorseRename.Text,
                            this.txtbxShortRename.Text,
                            this.txtbxHorseAliasRename.Text,
                            this.txtbxDateofNameChange.Text,
                            this.txtbxComment.Text,
                            this._userId,
                            "Insert");
                        //hdnfldhorseId.Value = Convert.ToString(dt.Rows[0][0]);
                    }
                    else
                    {
                        dt = this.Bl.HorseNewName(
                            (int)ViewState["GridViewRowID"],
                            this.txtbxHorseRename.Text,
                            this.txtbxShortRename.Text,
                            this.txtbxHorseAliasRename.Text,
                            this.txtbxDateofNameChange.Text,
                            this.txtbxComment.Text,
                            this._userId,
                            "Update");
                    }
                    newHorseId = Convert.ToInt32(dt.Rows[0][2]);
                    var message = string.Empty;
                    if (newHorseId == 2)
                    {
                        message = "Ex Name & Cr Name cannot be same.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);

                    }
                    else if (newHorseId == 3)
                    {
                        message = "This Name Already Taken By Another Horse With Same Date of Year.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else if (newHorseId == 4)
                    {
                        message = "This Name Already Taken By Another Horse With Same Date of Birth.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        
                    }
                    else
                    {
                        ClearAllSelection(this);

                        if (this.btnSave.Text.Equals("Update"))
                        {
                            message = "Record Updated Successfully.";
                            Page.ClientScript.RegisterStartupScript(
                                this.GetType(),
                                "Popup",
                                "ShowPopup('" + message + "');",
                                true);
                        }
                        else
                        {
                            hdnfldhorseId.Value = Convert.ToString(dt.Rows[0][0]);
                            message = "Record Added Successfully.";
                            Page.ClientScript.RegisterStartupScript(
                                this.GetType(),
                                "Popup",
                                "ShowPopup('" + message + "');",
                                true);
                        }
                        BindData(dt);

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(
                                this.GetType(),
                                "Popup",
                                "ShowPopup(You cannot add 2nd new name);",
                                true);
                }
            }
            catch (Exception ex)
            {
                // listPlacement.Visible = false;
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                if (hdnfldhorseId.Value != "")
                {
                    Session["HorseID"] = hdnfldhorseId.Value;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "refreshParentPage();", true);

            }
            catch (Exception ex)
            {
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
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
                    txtbxHorseRename.Text = hdnval.Value.Equals("&nbsp;") ? "" : hdnval.Value.Contains("&#39;") ? hdnval.Value.Replace("&#39;", "'") : hdnval.Value;
                    txtbxShortRename.Text = row.Cells[2].Text.Equals("&nbsp;") ? "" : row.Cells[2].Text.Contains("&#39;") ? row.Cells[2].Text.Replace("&#39;", "'") : row.Cells[2].Text;
                    txtbxHorseAliasRename.Text = row.Cells[3].Text.Equals("&nbsp;") ? "" : row.Cells[3].Text.Contains("&#39;") ? row.Cells[3].Text.Replace("&#39;", "'") : row.Cells[3].Text;
                    txtbxDateofNameChange.Text = GvHorseName.Rows[0].Cells[5].Text;
                    if (row.Cells[6].Text.Contains("&quot;"))
                    {
                        txtbxComment.Text = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&quot;") ? row.Cells[6].Text.Replace("&quot;", "\"") : row.Cells[6].Text;
                    }
                    else
                    {
                        txtbxComment.Text = row.Cells[6].Text.Equals("&nbsp;") ? "" : row.Cells[6].Text.Contains("&#39;") ? row.Cells[6].Text.Replace("&#39;", "'") : row.Cells[6].Text;
                    }

                    if (row.Cells[0].Text.Equals("Cr"))
                    {
                        if (GvHorseName.Rows.Count == 1)
                        {
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please add new name.');", true);
                            string message = "Please add new name.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            btnSave.Text = "Add";
                            ClearAllSelection(this);
                        }
                    }
                    else
                    {
                        string message = "Please select new name.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        btnSave.Text = "Add";
                        ClearAllSelection(this);

                        //if (GvHorseName.Rows.Count == 1)
                        //{
                        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please add new name.');", true);
                        //    string message = "Please add new name.";
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                        //    btnSave.Text = "Add";
                        //    ClearAllSelection(this);
                        //}
                        //else
                        //{
                        //    string message = "Please add new name.";
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);

                        //    btnSave.Text = "Update";
                        //    ClearAllSelection(this);    
                        //}
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.SendErrorToText(ex);
                string message = "Incorrect Information.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ViewState["GridViewRowID"].Equals(""))
                {
                    var dt = this.Bl.HorseNewName(
                            (int)ViewState["GridViewRowID"],
                            this.txtbxHorseRename.Text,
                            this.txtbxShortRename.Text,
                            this.txtbxHorseAliasRename.Text,
                            this.txtbxDateofNameChange.Text,
                            this.txtbxComment.Text,
                            this._userId,
                            "Delete");

                    ClearAllSelection(this);
                    BindData(dt);
                    btnSave.Text = "Add";
                    var message = "Record Deleted Successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    ViewState["GridViewRowID"] = "";
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
        protected void txtbxHorseRename_OnTextChanged(object sender, EventArgs e)
        {
            var txtvalue = sender as TextBox;
            if (txtvalue != null)
            {
                string value = txtvalue.Text;
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this.txtbxHorseRename.Text = textInfo.ToTitleCase(value.ToLower());
                this.txtbxShortRename.Text = this.txtbxHorseRename.Text.Replace(" ", "");
                this.txtbxHorseAliasRename.Text = this.txtbxShortRename.Text.Substring(0, Math.Min(this.txtbxShortRename.Text.Length, 10));
            }
        }
    }
}