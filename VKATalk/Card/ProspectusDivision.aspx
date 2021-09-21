<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProspectusDivision.aspx.cs" Inherits="VKATalk.Card.ProspectusDivision" %>

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
                    title: "Prospectus Division",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function OpenHorsePopup(value) {
            var dateofrace = document.getElementById('<%=txtbxRaceDate.ClientID %>').value;
            var CenterID = document.getElementById('<%=txtbxRaceDate.ClientID %>').value;
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            if (value === "BanName") {
                window.open('../Popups/HandicapBan.aspx?DOB=' + dateofrace, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <div style="padding-right:220px;">
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Prospectus Division</h1>
        </div>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="ProspecctusDivision" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Data Entry Date:(*)</td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="txtbxHandicapEnterDate" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="MaskedEditExtender1"
                                    ControlToValidate="txtbxHandicapEnterDate"
                                    EmptyValueMessage="Please enter correct Handicap date."
                                    InvalidValueMessage="Please enter correct Handicap date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="ProspecctusDivision" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="padding-left: 25px;">General Race Date:(*)</td>
                            <td style="width: 300px">
                                <asp:TextBox ID="txtbxRaceDate" runat="server" AutoPostBack="True" Width="75px" OnTextChanged="txtbxRaceDate_OnTextChanged"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxRaceDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxRaceDate"
                                    EmptyValueMessage="Please enter correct date."
                                    InvalidValueMessage="Please enter correct date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="ProspecctusDivision" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width: 100px">Center:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterName" AutoPostBack="True" OnSelectedIndexChanged="drpdwnCenterName_SelectIndexChange">
                                    <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding-left: 50px;">Season:</td>
                            <td>
                                <b>
                                    <asp:Label ID="lblSeason" runat="server"></asp:Label></b>
                            </td>
                            <td style="padding-left: 50px;">Year:</td>
                            <td>
                                <b>
                                    <asp:Label ID="lblYear" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                    <div id="dvgridview" style="height: 260px; width: 99%; overflow: auto;" runat="server">
                        <asp:GridView ID="grdvwRaceDetail" runat="server" Width="100%"
                            AutoGenerateColumns="False" DataKeyNames="GeneralRaceNameID" EmptyDataText="No Detail Found."
                            OnRowCommand="grdvwRaceDetail_OnRowCommand" ShowHeaderWhenEmpty="true">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkUpdate" runat="server" CommandName="RowUpdate" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SerialNumber" HeaderText="PRNo" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="General Race Name" ItemStyle-Width="27%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("GENERALRACENAME") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceID" Value='<%# Bind("GENERALRACEID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceNameID" Value='<%# Bind("GeneralRaceNameID") %>' />
                                        <asp:LinkButton Text='<%# Bind("GENERALRACENAME") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Of Division(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpdwnNoofDivision" runat="server">
                                            <asp:ListItem Selected="True" Value="-1" Text="--Please select--"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Div.II"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Div.III"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Div.IV"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddRaceName" runat="server" Text="Create Division Race" OnClick="btnAddRaceName_Click" />
                            </td>
                        </tr>
                    </table>

                    <div id="dvAcceptance" style="width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="GvAcceptance" runat="server" Width="99%"
                            AutoGenerateColumns="False" DataKeyNames="DivisionRaceID" EmptyDataText="No Detail Found."
                            OnSelectedIndexChanged="GvAcceptance_OnSelectedIndexChanged" OnRowDataBound="GvAcceptance_RowDataBound">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division Race Name(*)" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxDivisionRaceName" Width="200px" runat="server" Text='<%# Bind("DivisionRaceName") %>'></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" 
                                    ValidationGroup="ProspecctusDivision" runat="server" ControlToValidate="txtbxDivisionRaceName"
                                    Text="*" ErrorMessage="Please enter Division Race Name.">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division Race Date(*)" ItemStyle-Width ="15%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxGridviewRaceDate" runat="server" Width="75px" Text='<%# Bind("DivisionRaceDate") %>' ></asp:TextBox>
                                        <asp:ImageButton ID="ImageButtonG" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                        <asp:MaskedEditExtender ID="mskDateAvailableG" CultureName="en-GB" runat="server" TargetControlID="txtbxGridviewRaceDate"
                                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                            ControlExtender="mskDateAvailableG"
                                            ControlToValidate="txtbxGridviewRaceDate"
                                            EmptyValueMessage="Please enter correct date."
                                            InvalidValueMessage="Please enter correct date."
                                            Display="Dynamic"
                                            IsValidEmpty="true"
                                            InvalidValueBlurredMessage="*"
                                            ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                            ValidationGroup="ProspecctusDivision" />
                                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButtonG" runat="server" TargetControlID="txtbxGridviewRaceDate"
                                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="DR No(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxDayRaceNoG" runat="server" MaxLength="2" Width="25px" Text='<%# Bind("DayRaceNo") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            ControlToValidate="txtbxDayRaceNoG" runat="server"
                                            ErrorMessage="Only Numbers allowed"
                                            ValidationExpression="\d+" ValidationGroup="ProspecctusDivision">*
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="SR No(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxSeasonRaceNoG" runat="server" MaxLength="3" Width="25px" Text='<%# Bind("SeasonRaceNo") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                            ControlToValidate="txtbxSeasonRaceNoG" runat="server"
                                            ErrorMessage="Only Numbers allowed"
                                            ValidationExpression="\d+" ValidationGroup="ProspecctusDivision">*
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Race Time(*)" ItemStyle-Width="22%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldhh" Value='<%# Bind("hh") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldmm" Value='<%# Bind("mm") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldAMPM" Value='<%# Bind("ampm") %>' />
                                        <asp:DropDownList ID="drpdwnhhG" runat="server">
                                            <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:DropDownList ID="drpdwnmmG" runat="server">
                                            <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                             <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                             <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                             <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                             <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                             <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                             <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                             <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                             <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                             <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                             <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpdwnampmG" runat="server">
                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight Shuffle Type(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshuffletype" runat="server" Text='<%# Bind("WeightShuffleType") %>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="drpdwnWeightShuffleType" runat="server"></asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" InitialValue="-1" 
                                            ValidationGroup="ProspecctusDivision" runat="server" ControlToValidate="drpdwnWeightShuffleType"
                                            Text="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight Shuffle Value" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxWeightShuffleValue" MaxLength="4" runat="server" Width="25px" Text='<%# Bind("WeightShuffleValue") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtbxWeightShuffleValue" runat="server"
                                            ErrorMessage="Only Numbers allowed"
                                            ValidationExpression="^\d{1,3}(\.\d{0,1})?$" ValidationGroup="ProspecctusDivision">*
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:boundfield datafield="racefinaloutcome" readonly="true" headertext="race final outcome" itemstyle-width="20%" />--%>
                                <asp:TemplateField HeaderText="Race Final Outcome" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRaceFinalOutcome" runat="server" Text='<%# Bind("RaceFinalOutcome") %>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="drpdwnRaceFinalOutCome" Width="120px" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Race Final Outcome Reason" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxRaceFinalOutcomeReason" runat="server" Text='<%# Bind("RaceFinalOutcomeReason") %>' TextMode="MultiLine"></asp:TextBox>
                                         <asp:AutoCompleteExtender ServiceMethod="OutcomeReason"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                             CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtbxRaceFinalOutcomeReason"
                                            ID="ACComments1" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="My Comments" ItemStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxMyComments" runat="server" Text='<%# Bind("Comments") %>' TextMode="MultiLine"></asp:TextBox>
                                        <asp:AutoCompleteExtender ServiceMethod="Comments"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                             CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtbxMyComments"
                                            ID="ACComments" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="btnAddProvisionDivisionRace" runat="server" Text="Add" OnClick="btnAddProvisionDivisionRace_Click" />
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" /></td>
            <td>
                <asp:Button runat="server" ID="btnPdf" Text="PDF" /></td>
            <td>
                <asp:Button runat="server" ID="btnEdit" Text="Edit" /></td>

            <td>
                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
            </td>
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
                <td>
                <asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
            <td>
                <input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>
            <%--<td>
                <asp:Button runat="server" ID="btnHandicapShow" Text="Show Handicap" OnClick="btnHandicapShow_Click" /></td>
            <td>--%>
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
    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
