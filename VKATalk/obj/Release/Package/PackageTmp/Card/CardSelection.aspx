<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardSelection.aspx.cs" Inherits="VKATalk.Card.CardSelection" %>
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
                    title: "Card Selection",
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
        

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Selection</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardSelection" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
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
                                    ValidationGroup="CardSelection" />
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
                    <table id="tblHorseEntryForm" style="width:126%;" runat="server" visible="False" border="1">
                        <tr>
                            <td><b>Selector(*)</b></td>
                            <td><b>DRNo-1</b></td>
                            <td><b>DRNo-2</b></td>
                            <td><b>DRNo-3</b></td>
                            <td><b>DRNo-4</b></td>
                            <td><b>DRNo-5</b></td>
                            <td><b>DRNo-6</b></td>
                            <td><b>DRNo-7</b></td>
                            <td><b>DRNo-8</b></td>
                            <td><b>DRNo-9</b></td>
                            <td><b>DRNo-10</b></td>
                            <td><b>DRNo-11</b></td>
                            <td><b>Day Best</b></td>
                            <td><b>Good Double {DRNo-HNo}</b></td>
                            <td><b>Good Place {DRNo-HNo}</b></td>
                            <td><b>Rolling {DRNo-HNo}</b></td>
                            <td><b>Upsetter {DRNo-HNo}</b></td>
                            <td><b>Eatable Bet{DRNo-HNo}</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField runat="server" ID="hdnfieldhotlinerid" />
                                <asp:TextBox ID="txtbxHotliner" runat="server" style="width:280px;" TextMode="MultiLine"></asp:TextBox>
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
                                    ValidationGroup="CardSelection" runat="server" ControlToValidate="txtbxHotliner"
                                    Text="*" ErrorMessage="Please enter Hotliner Name."></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx11" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx12" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx13" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx21" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx22" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx23" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx31" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx32" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx33" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx41" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx42" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx43" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx51" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx52" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx53" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx61" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx62" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx63" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx71" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx72" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx73" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx81" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx82" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx83" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx91" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx92" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx93" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx101" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx102" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx103" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbx111" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx112" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbx113" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbxDayBest" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbxGoodDouble" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbxgoodplace" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbxRoller" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbxUpsetter" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td style="width: 95px;">
                                <asp:TextBox ID="txtbxeatablebet" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                             <td>
                                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CardSelection" />

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
                            AutoGenerateColumns="False" DataKeyNames="SelectionCID" EmptyDataText="No Entry Found"
                            OnSelectedIndexChanged="dvgrdviewHorseDetail_OnSelectedIndexChanged">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="ProfessionalName" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldSelectorPID" Value='<%# Bind("SelectorPID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID_FK") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldPRofessionalName" Value='<%# Bind("ProfessionalName") %>' />
                                        <asp:LinkButton Text='<%# Bind("ProfessionalName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DivisionRaceName" HeaderText="Division Race Name" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="DRNo1" HeaderText="DRNo1" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo2" HeaderText="DRNo2" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo3" HeaderText="DRNo3" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo4" HeaderText="DRNo4" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo5" HeaderText="DRNo5" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo6" HeaderText="DRNo6" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo7" HeaderText="DRNo7" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo8" HeaderText="DRNo8" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo9" HeaderText="DRNo9" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo10" HeaderText="DRNo10" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DRNo11" HeaderText="DRNo11" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DayBest" HeaderText="Day Best" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="GoodDouble" HeaderText="Good Double" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="GoodPlace" HeaderText="Good Place" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Rolling" HeaderText="Rolling" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Upsetter" HeaderText="Upsetter" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="EatableBet" HeaderText="EatableBet" ItemStyle-Width="5%" />
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
