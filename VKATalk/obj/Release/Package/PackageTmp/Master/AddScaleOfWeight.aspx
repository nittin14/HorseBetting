<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddScaleOfWeight.aspx.cs" Inherits="VKATalk.Master.AddScaleOfWeight" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
   <script type="text/javascript">
     
       <%--function ShowHideDiv() {
           var chkAge = document.getElementById('<%=rbWFA.ClientID%>').checked;
           var chkClass = document.getElementById('<%=rbWFC.ClientID%>').checked;
           var chkgender = document.getElementById('<%=rdvbtnGender.ClientID%>').checked;
           if (chkAge === true) {
               
               document.getElementById("DivWFC").style.display = "none";
               document.getElementById("DivGender").style.display = "none";
               document.getElementById("DivWFA").style.display = "block";

           }
           else if (chkClass === true) {
               document.getElementById("DivWFC").style.display = "block";
               document.getElementById("DivWFA").style.display = "none";
               document.getElementById("DivGender").style.display = "none";
           }
           else if (chkgender === true) {
               document.getElementById("DivWFC").style.display = "none";
               document.getElementById("DivWFA").style.display = "none";
               document.getElementById("DivGender").style.display = "block";
           }
       }--%>

       
         function ShowPopup(message) {
             $(function () {
                 $("#dialog").html(message);
                 $("#dialog").dialog({
                     title: "ScaleofWeight",
                     buttons: {
                         Close: function () {
                             $(this).dialog('close');
                         }
                     },
                     modal: true
                 });
             });
         };

      
</script>
    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager id="ScripManager1" runat="server"></asp:ScriptManager>
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Scale Of Weight</h1>
        <div id="dialog" style="display: none">
</div>
   
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    <table>
         <tr>
    <td colspan="2">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="scaleweight" ShowMessageBox="false" ShowSummary="true"  Font-Names="verdana" 
     Font-Size="12" />
        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
    </td>
    </tr>

        <tr>
            <td>Select Parameter:</td>
            <td><asp:DropDownList ID="drpdwnAddParameter" runat="server">
                <asp:ListItem Selected="True" Text="Age Parameter" Value="1"></asp:ListItem>
                <asp:ListItem Text="Distance Parameter" Value="2"></asp:ListItem>
                </asp:DropDownList>
              
            </td>
        </tr>
     
    </table>
    
</asp:Panel>
<!-- ModalPopupExtender -->   




<table align="center">
  <tr>
    <td>
      <fieldset style="width:370px;" class="Userlogin">
            <table>
    <tr>
    <td colspan="2">
    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="scaleweight" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
     Font-Size="12" />
    </td>
    </tr>
<tr>
<td>Center Name:(*)</td>
<td colspan="2"><asp:DropDownList ID="drpdwnCenter" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator2" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnCenter"
                            Text="*" ErrorMessage="Please select Center Name"></asp:RequiredFieldValidator>
</td>
</tr>
                <tr>
<td>From Year:(*)</td>
<td><asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnFromYear"
                            Text="*" ErrorMessage="Please select From Year"></asp:RequiredFieldValidator>
</td>
                    <td>Till Year:</td>
                    <td><asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList>
                        
                    </td>
</tr>
                <tr>
<td>From Season:(*)</td>
<td><asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator4" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnFromSeason"
                            Text="*" ErrorMessage="Please select From Season"></asp:RequiredFieldValidator>

</td>
                    <td>Till Season:</td>
                    <td><asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList></td>
</tr>
                <tr>
<td>Fix:</td>
<td colspan="3">
    <asp:CheckBox runat="server" id="chkbxFix" />
 </td>
</tr>
                <tr>
<td>Scale of Weight Type:(*)</td>
<td colspan="3">
  <%--  <asp:RadioButton ID="rbWFC" runat="server" Text="Weight for Class" GroupName="weight" Checked="true" onclick="ShowHideDiv()" />
    <asp:RadioButton ID="rbWFA" runat="server" Text="Weight for Age" GroupName="weight" onclick="ShowHideDiv()" /> 
    <asp:RadioButton ID="rdvbtnGender" runat="server" Text="Weight for Gender" GroupName="weight" onclick="ShowHideDiv()" /> --%>

   <%-- <asp:RadioButton ID="rbWFC" OnCheckedChanged="rbWFC_click" AutoPostBack="true" runat="server" Text="Weight for Class" GroupName="weight" Checked="true" />
    <asp:RadioButton ID="rbWFA" OnCheckedChanged="rbWFA_click" AutoPostBack="true" runat="server" Text="Weight for Age" GroupName="weight" /> 
    <asp:RadioButton ID="rdvbtnGender" OnCheckedChanged="rdvbtnGender_click" AutoPostBack="true" runat="server" Text="Weight for Gender" GroupName="weight" /> --%>

    <asp:RadioButtonList ID="rdbtnScaleofWeightFirst" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbtnScaleofWeightFirst_OnSelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="Weight for Class" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Weight for Age" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Weight for Gender" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
 </td>
</tr>
</table>
<div id="DivWFC" runat="server" visible="false">
    <table>
       
        <tr>
            <td>Class I Handicap Rating:</td>
            <td><asp:TextBox ID="txtbxClass1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Class II Handicap Rating:</td>
            <td><asp:TextBox ID="txtbxClass2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Class III Handicap Rating:</td>
            <td><asp:TextBox ID="txtbxClass3" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Class IV Handicap Rating:</td>
            <td><asp:TextBox ID="txtbxClass4" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Class V Handicap Rating:</td>
            <td><asp:TextBox ID="txtbxClass5" runat="server"></asp:TextBox></td>
        </tr>
         <tr>
        <td>Handicap Weight:(*)</td>
        <td><asp:TextBox ID="txtbxHandicapWeight" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator InitialValue="" ID="rqweightforclasshandicapweight" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="txtbxHandicapWeight"
                            Text="*" ErrorMessage="Please enter Handicap Weight"></asp:RequiredFieldValidator>
        </td>
            </tr>
    </table>
</div>

<div id="DivWFA" runat="server" visible="false">
    <table>
        <tr>
            <td>Weight System Type:</td>
            <td>
                <asp:RadioButtonList ID="rdbtnWeightSystemType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbtnWeightSystemType_OnSelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="Handicap Weight" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Weight Addition" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Age Condition" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        </table>
    <div id="dvHandicapWeight" runat="server" visible="true">
            <table>
                <tr>
            <td>Month:(*)</td>
            <td><asp:DropDownList ID="drpdwnMonth" runat="server">
                <asp:ListItem Selected="True" Text="--Please select--" Value="-1"></asp:ListItem>
                <asp:ListItem Value="1" Text="Janurary"></asp:ListItem>
                <asp:ListItem Value="2" Text="Feburary"></asp:ListItem>
                <asp:ListItem Value="3" Text="March"></asp:ListItem>
                <asp:ListItem Value="4" Text="April"></asp:ListItem>
                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                <asp:ListItem Value="6" Text="June"></asp:ListItem>
                <asp:ListItem Value="7" Text="July"></asp:ListItem>
                <asp:ListItem Value="8" Text="August"></asp:ListItem>
                <asp:ListItem Value="9" Text="September"></asp:ListItem>
                <asp:ListItem Value="10" Text="October"></asp:ListItem>
                <asp:ListItem Value="11" Text="November"></asp:ListItem>
                <asp:ListItem Value="12" Text="December"></asp:ListItem>
            </asp:DropDownList>
              <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator5" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnMonth"
                            Text="*" ErrorMessage="Please select Month."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Distance Parameter:(*)</td>
            <td><asp:DropDownList ID="drpdwnDistanceParameter" runat="server">
                <asp:ListItem Selected="True" Text="--Please select--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="< 1200" Value="1"></asp:ListItem>
                <asp:ListItem Text="1201-1599" Value="2"></asp:ListItem>
                <asp:ListItem Text="1600-2399" Value="3"></asp:ListItem>
                <asp:ListItem Text="2400-3199" Value="4"></asp:ListItem>
                <asp:ListItem Text="3200 Above" Value="5"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator6" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnDistanceParameter"
                            Text="*" ErrorMessage="Please select Distance Parameter."></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td>Nation:(*)</td>
            <td> <asp:DropDownList runat="server" ID="drpdwnNation"/>
                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator7" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnNation"
                            Text="*" ErrorMessage="Please select Nation."></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td>Fix:</td>
            <td> <asp:CheckBox runat="server" ID="chkbxAgeParameterfix"/></td>
        </tr> 
         <tr>
            <td>Age Parameter:(*)</td>
            <td><asp:DropDownList ID="drpdwnAgeParameter" runat="server">
                <asp:ListItem Selected="True" Text="--Please select--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="2" Value="1"></asp:ListItem>
                <asp:ListItem Text="3" Value="2"></asp:ListItem>
                <asp:ListItem Text="4" Value="3"></asp:ListItem>
                <asp:ListItem Text="5" Value="4"></asp:ListItem>
                <asp:ListItem Text="6>=" Value="5"></asp:ListItem>
                </asp:DropDownList>
                  <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator8" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnAgeParameter"
                            Text="*" ErrorMessage="Please select Age Parameter."></asp:RequiredFieldValidator>
            </td>
             <td>Handicap Weight:(*)</td>
            <td>
                <asp:TextBox runat="server" ID="txtbxHandicapWeightWeightforAge"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="txtbxHandicapWeightWeightforAge"
                            Text="*" ErrorMessage="Please enter Handicap Weight."></asp:RequiredFieldValidator>
            </td>
        </tr>
            </table>
        </div>
    <div id="dvAgeCondition" runat="server" visible="false">
            <table>
                <tr>
            <td>Age Condition:</td>
            <td><asp:DropDownList ID="drpdwnAgeCondition" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator11" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnAgeCondition"
                            Text="*" ErrorMessage="Please select Age Condition."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Handicap Weight:(*)</td>
            <td>
                <asp:TextBox runat="server" ID="txtbxAgeHandicapWeight"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="txtbxAgeHandicapWeight"
                            Text="*" ErrorMessage="Please enter Handicap Weight."></asp:RequiredFieldValidator>
            </td>
        </tr>
                </table>
             </div>
</div>

   <div id="DivGender" runat="server" visible="false">
    <table>
       
        <tr>
            <td>Gender:(*)</td>
            <td>
                <asp:DropDownList runat="server" ID="drpdwnGender"/>
                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator3" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="drpdwnGender"
                            Text="*" ErrorMessage="Please select Gender"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Handicap Weight:(*)</td>
            <td><asp:TextBox ID="txtbxHandicapWeightGender" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator InitialValue="" ID="rqweightforGenderhandicapweight" Display="Dynamic" 
                            ValidationGroup="scaleweight" runat="server" ControlToValidate="txtbxHandicapWeightGender"
                            Text="*" ErrorMessage="Please enter Handicap Weight"></asp:RequiredFieldValidator>
            </td>
        </tr>
       
    </table>
</div>
<table>
<tr>
<td>
    <asp:button id="btnsubmit" runat="server" text="Add" 
         onclick="btnsubmit_click" UseSubmitBehavior="false" ValidationGroup="scaleweight" /></td>
<td><asp:button id="btnDownload" runat="server" text="Download" /></td>
<td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
<td><asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" /></td>
<td><asp:button id="btnImport" runat="server" text="Import" />
     <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
    CancelControlID="Button2" BackgroundCssClass="Background">
</cc1:ModalPopupExtender>
</td>
<td><asp:button id="btnexport" runat="server" text="Export" OnClick="btnExport_Click"/></td>
<td><asp:button id="btnexporttoday" runat="server" text="Export today" /></td>
<td><asp:button id="btnprint" runat="server" text="Print" /></td>
<td><input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>
</tr>
</table>
    </fieldset>
   
    </td>
 </tr>
</table>
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
        <table>
            <tr>
                <td>File Upload:</td>
                <td>
                    <asp:FileUpload ID="flupload" runat="server" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnFileUpload" runat="server" Text="Upload" OnClick="btnFileUpload_Click" /></td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Close" /></td>
            </tr>
        </table>

    </asp:Panel>
<div id="dvgridview" style="height:300px; width:100%; overflow:auto;" runat="server">
             <asp:GridView ID="GvCommon" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="SOWID" OnSelectedIndexChanged="GvCommon_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="RowCount" HeaderText="Count" ItemStyle-Width="1%" />
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("CenterName") %>'/>
                        <asp:LinkButton Text='<%# Bind("CenterName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="4%" />
                        <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="ScaleOfWeightType" HeaderText="Type of Scale Weight" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="CIHandicapRating" HeaderText="I" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="CIIHandicapRating" HeaderText="II" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="CIIIHandicapRating" HeaderText="III" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="CIVHandicapRating" HeaderText="IV" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="CVHandicapRating" HeaderText="V" ItemStyle-Width="2%" />
                        <%--<asp:BoundField DataField="ScaleofWeightType" HeaderText="Weight Type" ItemStyle-Width="2%" />--%>
                        <asp:BoundField DataField="WeightSystemType" HeaderText="Weight System Type" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="DistanceParameter" HeaderText="Distance Parameter" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Nation" HeaderText="Nation" ItemStyle-Width="3%" />
                        <asp:BoundField DataField="AgeParameter" HeaderText="Age Parameter" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="AgeCondition" HeaderText="Age Condition" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="HorseGender" HeaderText="Gender" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HandicapWeight" HeaderText="Handicap Weight" ItemStyle-Width="5%" />
                        
                        <%--<asp:BoundField DataField="HandicapWeightGender" HeaderText="Handicap Weight" ItemStyle-Width="2%" />--%>
                </Columns>
                 <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                 <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
 </div>
        

    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>   
</asp:Content>
