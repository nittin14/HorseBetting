<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardTip.aspx.cs" Inherits="VKATalk.Card.CardTip" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
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

        .modalBackground
        {
            background-color: #333333;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: #CCCCCC;
            padding: 1px;
            width: 300px;
            Height: 200px;
        }
    </style>
    <script type="text/javascript">

        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "CardTip",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function GetHotlinerID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldhotlinerid.ClientID %>').value = HdnKey;
        }

        function GetHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseID.ClientID %>').value = HdnKey;
        }

        function GetTipID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldTipID.ClientID %>').value = HdnKey;
        }

        function SetContextKey() {
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=txtbxRaceDate.ClientID %>").value);
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Tip</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardTip" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                             <td style="padding-left: 5px;">Data Entry Date:(*)</td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="txtbxEntryEnterDate" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxEntryEnterDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="MaskedEditExtender1"
                                    ControlToValidate="txtbxEntryEnterDate"
                                    EmptyValueMessage="Please enter correct Handicap date."
                                    InvalidValueMessage="Please enter correct Handicap date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="CardAcceptance" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxEntryEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width: 200px">Division Race Date:(*)</td>
                            <td style="width: 300px">
                                <asp:TextBox ID="txtbxRaceDate" runat="server" AutoPostBack="True" Width="75px" OnTextChanged="txtbxRaceDate_OnTextChanged"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxRaceDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxRaceDate"
                                    EmptyValueMessage="Please enter date."
                                    InvalidValueMessage="Invalid date format."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                                    ValidationGroup="CardTip" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width: 100px">Center:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterName" AutoPostBack="True" OnSelectedIndexChanged="drpdwnCenterName_SelectIndexChange">
                                    <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding-left:50px;">Season:</td>
                            <td>
                                <b><asp:Label ID="lblSeason" runat="server"></asp:Label></b>
                            </td>
                            <td style="padding-left:50px;">Year:</td>
                            <td>
                               <b> <asp:Label ID="lblYear" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                    <table id="tblHorseEntryForm" runat="server" visible="False" border="1">
                        <tr>
                            <td style="width: 180px;"><b>Tipster(*)</b></td>
                            <td style="width: 100px;"><b>DRNo</b></td>
                            <td style="width: 250px;"><b>HNo</b></td>
                            <td style="width: 250px;"><b>Horse Name(*)</b></td>
                            <td style="width: 250px;"><b>Tip(*)</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField runat="server" ID="hdnfieldhotlinerid" />
                                <asp:TextBox ID="txtbxHotliner" runat="server" style="width: 380px;"></asp:TextBox>
                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHotlinerList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHotliner" CompletionListElementID="listPlacement"
                                        OnClientItemSelected="GetHotlinerID"
                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" 
                                    ValidationGroup="CardTip" runat="server" ControlToValidate="txtbxHotliner"
                                    Text="*" ErrorMessage="Please enter Hotliner Name."></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px;">
                                <asp:DropDownList runat="server" ID="drpdwndayraceno"></asp:DropDownList>
                            </td>
                            <td style="width: 10px;">
                                <asp:DropDownList runat="server" ID="drpdwnHorseNo"></asp:DropDownList>
                            </td>

                            <td style="width: 210px;">
                                <asp:HiddenField runat="server" ID="hdnfieldHorseID" />
                                <asp:TextBox ID="txtbxHorseName" runat="server" style="width: 480px;" onkeyup="SetContextKey()"></asp:TextBox>
                                <div id="Div1" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                        CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName" CompletionListElementID="Div1" 
                                        OnClientItemSelected="GetHorseID"
                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" 
                                    ValidationGroup="CardTip" runat="server" ControlToValidate="txtbxHorseName"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 210px;">
                                <asp:TextBox ID="txtbxHotline" runat="server" style="width: 250px;"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldTipID" />
                                <div id="Div2" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHotlineList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHotline" CompletionListElementID="Div2"
                                        OnClientItemSelected="GetTipID"
                                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" 
                                    ValidationGroup="CardTip" runat="server" ControlToValidate="txtbxHotline"
                                    Text="*" ErrorMessage="Please enter Hotline"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                             <td>
                                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CardTip" />

                             </td>
                             <td>
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />

                             </td>
                            <td>
                                <asp:Button runat="server" ID="btnShow" Text="Show" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnPdf" Text="PDF" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" /></td>
                            <td>
                                <asp:Button ID="btnImport" runat="server" Text="Import" />
                                <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                    CancelControlID="Button2" BackgroundCssClass="Background">
                                </asp:ModalPopupExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>

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
                    <div id="dvgrdviewHorseDetail" style="height: 300px; width: 95%; overflow: auto;" runat="server">
                        <asp:GridView ID="grdvwHorseDetail" runat="server" Width="100%"
                            AutoGenerateColumns="False" DataKeyNames="TipCID" EmptyDataText="No Entry Found"
                            OnSelectedIndexChanged="dvgrdviewHorseDetail_OnSelectedIndexChanged">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="ProfessionalName" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseID" Value='<%# Bind("HorseID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldHotlinerID" Value='<%# Bind("TipsterPID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID_FK") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldPRofessionalName" Value='<%# Bind("ProfessionalName") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldtiptypeid" Value='<%# Bind("TipTypeMID") %>' />
                                        <asp:LinkButton Text='<%# Bind("ProfessionalName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DivisionRaceName" HeaderText="Division Race Name" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="DRNo" HeaderText="DRNo" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="HorseNo" HeaderText="HNo" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="HorseName" HeaderText="Horse Name" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="TipType" HeaderText="Tip" ItemStyle-Width="25%" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>


    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
