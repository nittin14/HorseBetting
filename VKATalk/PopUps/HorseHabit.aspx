﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HorseHabit.aspx.cs" Inherits="VKATalk.PopUps.HorseHabit" %>

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
                   title: "Horse Habit",
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
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Horse Habit</h1>
        <div id="dialog" style="display: none">
        </div>
       <div>
            <table align="center">
                 <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HHabit" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
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
                    <td>Habit:(*)
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpdwnCommon1" runnat="server" AutoPostBack="true" OnSelectedIndexChanged="drpdwnCommon1_SelectIndexChange" />
                        <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="HHabit" runat="server" ControlToValidate="drpdwnCommon1"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Year."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Habit Alias:
                    </td>
                    <td>
                        <asp:Label ID="lblAlias" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Habit Type:
                    </td>
                    <td>
                        <asp:Label ID="lblHabitType" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>Details:
                    </td>
                    <td><asp:TextBox ID="txtbxSaddleNo" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Saddle No"  ValidationGroup="HHabit" 
                            ControlToValidate="txtbxSaddleNo">*</asp:RequiredFieldValidator>--%>
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
                            ValidationGroup="HHabit" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxFromDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Till Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxTillDate" runat="server"></asp:TextBox>
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
                            ValidationGroup="HHabit" />
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                        <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxComment" CompletionListElementID="Div6"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="HHabit" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" ValidationGroup="HHabit"/>
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="HHabit"/>    
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
                    AutoGenerateColumns="False" DataKeyNames="HorseHabitID" OnSelectedIndexChanged="GvGlobal_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Habit" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Habit") %>'/>
                                <asp:LinkButton Text='<%# Bind("Habit") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Details" HeaderText="Details" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="TillDate" HeaderText="Till Date" ItemStyle-Width="15%" />
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
    </script>
</asp:Content>
