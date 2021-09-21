<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HandicapBan.aspx.cs" Inherits="VKATalk.PopUps.HandicapBan" %>
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
                   title: "Handicap Ban",
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Handicap Ban</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HandicapBan" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                    </td>
                </tr>
                <tr>
                    <td>Horse Name:
                    </td>
                    <td>
                        <%--<asp:Label runat="server" ID="lblHorseNameFirst"></asp:Label>--%>
                        <asp:DropDownList ID="drpdwnHorseName" runat="server"></asp:DropDownList>
                        <asp:HiddenField runat="server" id="hdnfieldHorseName"/>
                        <asp:HiddenField runat="server" ID="horseId"/>
                        <asp:HiddenField runat="server" ID="hdnfieldhorsenameid"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-1" ErrorMessage="Please select Horse Name" 
                            ValidationGroup="HandicapBan" ControlToValidate="drpdwnHorseName">*</asp:RequiredFieldValidator>
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
                    <td>Type of Ban:(*)
                    </td>
                    <td>
                        <%--<asp:TextBox ID="txtbxTypeofBan" runat="server"></asp:TextBox>
                        <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                         <asp:AutoCompleteExtender ServiceMethod="TypeofBan"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxTypeofBan" CompletionListElementID="Div6"
                            ID="AutoCompleteTypeofBan" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>--%>
                        <asp:DropDownList ID="drpdwnTypeofBan" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" InitialValue="-1" ErrorMessage="Please select Type of Ban" ValidationGroup="HandicapBan" 
                            ControlToValidate="drpdwnTypeofBan">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Ban Details:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxBanDetails" runat="server"></asp:TextBox>
                        <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                         <asp:AutoCompleteExtender ServiceMethod="BanDetaiList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxBanDetails" CompletionListElementID="Div1"
                            ID="AcBanDetail" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>Ban Start Date:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxStartDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskStartDatae" CultureName="en-GB" runat="server" TargetControlID="txtbxStartDate" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskStartDatae"
                            ControlToValidate="txtbxStartDate"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="HandicapBan" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxStartDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Total Days Ban:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxTotalDayBan" runat="server" AutoPostBack="true" OnTextChanged="txtbxTotalDayBan_OnTextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxEndDate" runat="server" AutoPostBack="true" OnTextChanged="txtbxEndDate_OnTextChanged"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="msStardDate" CultureName="en-GB" runat="server" TargetControlID="txtbxEndDate" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                            ControlExtender="msStardDate"
                            ControlToValidate="txtbxEndDate"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="True"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="HandicapBan" />
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxEndDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                        <div id="Div2" style="height:300px; overflow-y:scroll;" ></div>
                         <asp:AutoCompleteExtender ServiceMethod="CommentList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxComment" CompletionListElementID="Div2"
                            ID="ACComments" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click" />
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="HandicapBan" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <%--<asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="HandicapBan"/>    
                        <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />--%>
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
            <div id="dvHorseBan" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvHorseBan" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="HorseBanID" OnSelectedIndexChanged="GvHorseBan_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Horse Name">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("HorseName") %>'/>
                                <asp:LinkButton Text='<%# Bind("HorseName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Ban" HeaderText="Type of Ban" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BanDetails" HeaderText="Ban Details" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="TotalDaysBan" HeaderText="Total Days Ban" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="ENDDate" HeaderText="END Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="MyComments" HeaderText="Comments" ItemStyle-Width="30%" />
                        </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    
    <script type="text/javascript">
        function refreshParentPage() {
            //window.opener.location.href = window.opener.location.href;
            //if (window.opener.progressWindow) {
            //    window.opener.progressWindow.close();
            //}
            window.close();
        }
    </script>
</asp:Content>
