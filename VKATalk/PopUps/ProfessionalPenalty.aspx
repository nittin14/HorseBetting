<%@ Page AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProfessionalPenalty.aspx.cs" Inherits="VKATalk.PopUps.ProfessionalPenalty" Language="C#" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <title></title>
      <script src="../Scripts/jquery1.7.2.min.js"></script>
      <script src="../Scripts/jquery-ui-1.8.9.js"></script>
      <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
      <style type="text/css">
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            margin: 0px;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 0px;
            background-color: White;
            list-style-type: none;
            height: 100px;
        }

        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            left: auto;
            margin: 0px;
            list-style-type: none;
        }

        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
            margin: 0px;
            list-style-type: none;
        }

        .ui-dialog-titlebar-close
        {
            visibility: hidden;
        }

     </style>
     <script type="text/javascript">
         function ShowPopup(message) {
             $(function () {
                 $("#dialog").html(message);
                 $("#dialog").dialog({
                     title: "ProfessionalPenalty",
                     buttons: {
                         Close: function () {
                             $(this).dialog('close');
                         }
                     },
                     modal: true
                 });
             });
         };


          function GetMasterProspectusID(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           document.getElementById('<%=hdnfieldAutoSelectionProfessionalNameID.ClientID %>').value = HdnKey;
        }
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Professional Penalty</h1>
      <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="ProfessionalPenalty" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                          <asp:HiddenField runat="server" id="hdnfieldProfessionalName"/>
                          <asp:HiddenField runat="server" ID="hdnfieldProfessionalID"/>
                          <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameID"/>
                    </td>
                </tr>
               <tr>
                    <td>Profesional Name:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxProfessionalName" runat="server" Width="550px"></asp:TextBox>
                        <asp:HiddenField ID="hdnfieldAutoSelectionProfessionalNameID" runat="server" />
                        <div id="Div11" style="height:300px; overflow-y:scroll;" >
                          <asp:AutoCompleteExtender ServiceMethod="ProfessionalName"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                             CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxProfessionalName" CompletionListElementID="Div11"
                              OnClientItemSelected="GetMasterProspectusID"
                            ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                            </div>
                        <asp:RequiredFieldValidator ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="ProfessionalPenalty" runat="server" ControlToValidate="txtbxProfessionalName"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter person name."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Penalty:(*)
                    </td>
                    <td colspan="3">
                       <asp:DropDownList ID="drpdwnPenalty" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-1" ErrorMessage="Please select Penalty" ValidationGroup="ProfessionalPenalty" ControlToValidate="drpdwnPenalty">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>Penalty Reason:(*)
                    </td>
                    <td colspan="3">
                       <asp:DropDownList ID="drpdwnPenaltyReason" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-1" ErrorMessage="Please select Penalty Reason" ValidationGroup="ProfessionalPenalty" ControlToValidate="drpdwnPenaltyReason">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Penalty Detail:
                    </td>
                    <td colspan="3">
                       <asp:TextBox ID="txtbxPenaltyDetail" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="divPenaltyDetail" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddPenaltyDetail"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                              CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxPenaltyDetail" CompletionListElementID="divPenaltyDetail"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>From Date:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxFromDate" runat="server" Width="75px"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxFromDate" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                         <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxFromDate"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="ProfessionalPenalty" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxFromDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                 <tr>
                    <td>Till Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxTillDate" runat="server" Width="75px"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxTillDate" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                         <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                            ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtbxTillDate"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="True"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="ProfessionalPenalty" />
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Working on Apeal:</td>
                    <td>
                        <asp:CheckBox ID="chkbxWorkingonApeal" runat="server" Checked="false" />
                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                       <asp:TextBox ID="txtbxCommentNew" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="listPlacement1" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                              CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxCommentNew" CompletionListElementID="listPlacement1"
                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="ProfessionalPenalty" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="ProfessionalPenalty"/>    
                        <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
            <table>
                <tr>
                    <td>File Upload:</td>
                    <td><asp:FileUpload ID="flupload" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnFileUpload" runat="server" Text="Upload" OnClick="btnFileUpload_Click" /></td>
                    <td><asp:Button ID="Button2" runat="server" Text="Close" /></td>
                </tr>
            </table>
            </asp:Panel>
            <div id="dvHomeDistance" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvHomeDistance" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="PenaltyPID" OnSelectedIndexChanged="GvHomeDistance_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Penalty" ItemStyle-Width="10%">
                            <ItemTemplate>
                                 <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Penalty") %>'/>
                                 <asp:LinkButton Text='<%# Bind("Penalty") %>' ID="lnkSelect" runat="server" CommandName="Select" /> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PenaltyReason" HeaderText="Penalty Reason" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="PenaltyDetail" HeaderText="Penalty Detail" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="TillDate" HeaderText="Till Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="WorkingOnApeal" HeaderText="Working On Apeal" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="40%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    
    <script type="text/javascript">

        function refreshParentPage() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }
    </script>
</asp:Content>