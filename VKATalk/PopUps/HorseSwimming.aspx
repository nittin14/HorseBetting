<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorseSwimming.aspx.cs" Inherits="VKATalk.PopUps.HorseSwimming" %>

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
                   title: "Horse Swimming",
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

    function SetContextKey() {
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=hdnfieldGeneralRaceNameID.ClientID %>").value);
        }

        
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Horse Swimming</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                 <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HSwimmingValidation" ShowMessageBox="true" 
                            ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                        <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Horse Name:
                    </td>
                    <td>
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
                <%--<tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblHorseNameSecond"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        Fix:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkbxfix" runat="server" />
                    </td>
                </tr>
                <%--<tr>
                    <td>Veterinary Problem:(*)
                    </td>
                    <td>
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
                            ValidationGroup="HSwimmingValidation" runat="server" ControlToValidate="txtbxVietProblem"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Veterinary Problem."></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
                <tr>
                    <td>Swimming Date:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxSwimmingDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" 
                                TargetControlID="txtbxSwimmingDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxSwimmingDate"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                           ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="HSwimmingValidation" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxSwimmingDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Swimming Rounds:(*)</td>
                    <td>
                        <asp:TextBox ID="txtbxSwimminground" runat="server" MaxLength="2" Width="50px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbxSwimminground"
                            ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$" ValidationGroup="HSwimmingValidation"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator InitialValue="" ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="HSwimmingValidation" runat="server" ControlToValidate="txtbxSwimminground"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Swimming Rounds."></asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>Workout Rating:(*)</td>
                    <td>
                        <asp:DropDownList ID="drpdnWorkoutRating" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>IsShow:(*)</td>
                    <td><asp:CheckBox ID="chkbxIsShow" runat="server" Checked="true" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click" />
                         <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="HSwimmingValidation" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="HSwimmingValidation"/>    
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
                    AutoGenerateColumns="False" DataKeyNames="HorseSwimmingID" 
                    OnSelectedIndexChanged="GvGlobal_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Swimming Date" ItemStyle-Width="25%">
                            <ItemTemplate>
                               <%-- <asp:HiddenField runat="server" id="hdnfieldHorseName" Value='<%# Bind("HorseName") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldHorseNameID" Value='<%# Bind("HorseNameID") %>'/>--%>
                                <asp:HiddenField runat="server" id="hdnfieldSwimmingDate" Value='<%# Bind("SwimmingDate") %>'/>
                                <asp:LinkButton Text='<%# Bind("SwimmingDate") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="SwimmingDate" HeaderText="Swimming Date" ItemStyle-Width="5%" />--%>
                        <asp:BoundField DataField="SwimmingRounds" HeaderText="Swimming Rounds" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="WorkoutRating" HeaderText="Workout Rating" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="isshow" HeaderText="Is Show" ItemStyle-Width="5%" />
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

