<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorseVeterinaryProblem.aspx.cs" Inherits="VKATalk.PopUps.HorseVeterinaryProblem" %>

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
                   title: "Horse VeterinaryProblem",
                   buttons: {
                       Close: function () {
                           $(this).dialog('close');
                       }
                   },
                   modal: true
               });
           });
       };

       function GetSireID(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           document.getElementById('<%=hdnfieldHorseNameID.ClientID %>').value = HdnKey;
       }

        function GetVietProblmID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldVieProblemID.ClientID %>').value = HdnKey;
       }
        function SetContextKey() {
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=hdnfieldGeneralRaceNameID.ClientID %>").value);
        }

        
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Horse Veterinary Problem</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                 <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HVet" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                        <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Horse Name:
                    </td>
                    <td>
                        <%--<asp:Label runat="server" ID="lblHorseNameFirst"></asp:Label>--%>
                         <asp:TextBox runat="server" ID="txtbxHorseName" Width="700px" onkeyup="SetContextKey()"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" />
                                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxHorseName" CompletionListElementID="listPlacement"
                                                        OnClientItemSelected="GetSireID"
                                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                        <asp:HiddenField runat="server" id="hdnfieldHorseName"/>
                        <asp:HiddenField runat="server" ID="horseId"/>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblHorseNameSecond"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fix:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkbxfix" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Veterinary Problem:(*)
                    </td>
                    <td>
                       <%-- <asp:DropDownList runat="server" ID="drpdwnCommon" Width="455px" />--%>
                         <asp:TextBox runat="server" ID="txtbxVietProblem" Width="700px"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnfieldVieProblemID" />
                                                <div id="Div1" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddVietProblemList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxVietProblem" CompletionListElementID="Div1"
                                                        OnClientItemSelected="GetVietProblmID"
                                                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                        
                                                    </asp:AutoCompleteExtender>
                                                </div>
                        <asp:RequiredFieldValidator InitialValue="" ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="HVet" runat="server" ControlToValidate="txtbxVietProblem"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Veterinary Problem."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>From Date:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxFromDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxFromDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxFromDate"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                           ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="HVet" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxFromDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Till Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxTillDate" runat="server" AutoPostBack="true" OnTextChanged="txtbxTillDate_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxTillDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                            ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtbxTillDate"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="True"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="HVet" />
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Days Under Problem:</td>
                    <td>
                        <asp:Label ID="lblDayProblem" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Treatment Start Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxStartDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" CultureName="en-GB" runat="server" TargetControlID="txtbxStartDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                            ControlExtender="MaskedEditExtender2"
                            ControlToValidate="txtbxStartDate"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="True"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="HVet" />
                        <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Treatment End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxEndDate" runat="server" AutoPostBack="true" OnTextChanged="txtbxEndDate_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender3" CultureName="en-GB" runat="server" TargetControlID="txtbxEndDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                            ControlExtender="MaskedEditExtender3"
                            ControlToValidate="txtbxEndDate"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="True"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="HVet" />
                        <asp:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Days Under Treatment</td>
                    <td>
                        <asp:Label ID="lblTreatment" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                        <div id="dvCommentList" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxComment" CompletionListElementID="dvCommentList"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click" />
                         <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="HVet" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="HVet"/>    
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
            <div id="dvGlobal" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvGlobal" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="VeterinaryProblemID" OnSelectedIndexChanged="GvGlobal_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Problem" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Disease") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldDiseaseID" Value='<%# Bind("DiseaseID") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldHorseNameG" Value='<%# Bind("HorseName") %>'/>
                                <%--<asp:HiddenField runat="server" id="HiddenField2" Value='<%# Bind("DiseaseID") %>'/>--%>
                                <asp:LinkButton Text='<%# Bind("Disease") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="TillDate" HeaderText="Till Date" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="DaysUP" HeaderText="Under Problem" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="StartDate" HeaderText="Treatment Start Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="EndDate" HeaderText="Treatment End Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="DaysUT" HeaderText="Under Treatment" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="MyComments" HeaderText="Comments" ItemStyle-Width="20%" />
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

        function CloseWindow() {
            window.close();
        }
    </script>
</asp:Content>

