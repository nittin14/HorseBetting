<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceDaySituation.aspx.cs" Inherits="VKATalk.Card.RaceDaySituation" %>

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
                    title: "Card Race Day Situation",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        

        function GetReport(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender2", "hdnfieldReport");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function GetCommentator(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender1", "hdnfieldCommentator");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        
        function GetHorseList(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender3", "hdnfieldHorseID");
                $get(hiddenfieldID).value = eventArgs.get_value();

            }
        }


        function OpenHorsePopup(value) {
            var centerid = document.getElementById('<%=drpdwnCenterName.ClientID %>').value;
            var dataentrydate = document.getElementById('<%=txtbxDivisionRaceDate.ClientID %>').value;
            var divisionraceid = document.getElementById('<%=hdnfieldDivisionRaceMID.ClientID %>').value;

            if (value === "IncidentUpdate") {
                window.open('../Card/RaceDayIncident.aspx?DivisionRaceID=' + divisionraceid + '&CenterID=' + centerid + '&DataEntryDate=' + dataentrydate, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            //else if (value === "RaceCommentary") {
            //    window.open('../Card/ResultRaceCommentary.aspx?GeneralRaceNameID=' + generalracenameid + '&GeneralRaceID=' + generalraceid + '&DivisionRaceID=' + divisionraceid + '&CenterMID=' + centerid + '&DivisionRaceDate=' + divisionracedate + '&DataEntryDate=' + enterdate + '&HorseNameID=' + horsenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            //}
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Ray Day Situation</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RaceDaySituation" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldDivisionRaceMID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Data Entry Date:(*)</td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="txtbxDeclarationEnterDate" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender5" CultureName="en-GB" runat="server" TargetControlID="txtbxDeclarationEnterDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="MaskedEditExtender5"
                                    ControlToValidate="txtbxDeclarationEnterDate"
                                    EmptyValueMessage="Please enter correct Handicap date."
                                    InvalidValueMessage="Please enter correct Handicap date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="RaceDaySituation" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDeclarationEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="padding-left: 25px;">Division Race Date:(*)</td>
                            <td style="width: 300px">
                                <asp:TextBox ID="txtbxDivisionRaceDate" runat="server" AutoPostBack="True" Width="75px" OnTextChanged="txtbxDivisionRaceDate_OnTextChanged"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton6" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxDivisionRaceDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator6" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxDivisionRaceDate"
                                    EmptyValueMessage="Please enter correct date."
                                    InvalidValueMessage="Please enter correct date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="RaceDaySituation" />
                                <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDivisionRaceDate"
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
                    <br />
                    <br />

                    <table>
                        <tr>
                            <td>
                                False Rails:(*)
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxFalse" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Measurment of False Rails:(**)
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMeasurement" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator2" Display="Dynamic" 
                            ValidationGroup="RaceDaySituation" runat="server" ControlToValidate="txtbxMeasurement"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Measurment of False Rails."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                         <tr>
                            <td>
                               Penetrometer Reading:(*)
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxPenetrometer" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator3" Display="Dynamic" 
                            ValidationGroup="RaceDaySituation" runat="server" ControlToValidate="txtbxPenetrometer"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Penetrometer Reading."></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtbxPenetrometer"
                        ErrorMessage="Only numeric allowed." ForeColor="Red"
                        ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="RaceDaySituation">*
                    </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Going:(*)
                            </td>
                            <td>
                               <asp:DropDownList ID="drpdwnGoing" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="RaceDaySituation" runat="server" ControlToValidate="drpdwnGoing"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Race Day Reporter."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                Weather:(*)
                            </td>
                            <td>
                               <asp:DropDownList ID="drpdwnWeather" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator4" Display="Dynamic" 
                            ValidationGroup="RaceDaySituation" runat="server" ControlToValidate="drpdwnWeather"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Weather."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>

    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="RaceDaySituation" /></td>
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" /></td>
            <td>
                <asp:Button runat="server" ID="btnPdf" Text="PDF" /></td>
            
            <td>
                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" /></td>
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
            <td>
                <asp:Button runat="server" ID="btnHandicapShow" Text="Show Handicap" OnClick="btnHandicapShow_Click" /></td>
            <td>
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
    <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
                <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GvShowALL_OnSelectedIndexChanged"
                            OnRowDataBound="GvShowALL_RowDataBound">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" ValidationGroup="RaceDaySituationG" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo(*)" ItemStyle-Width="2%" />
                        <asp:TemplateField HeaderText="Division Race Name(*)" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:label ID="lblDivisionRaceName" runat="server" Text='<%# Bind("DivisionRaceName") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Real Race Time(*)" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldRealRaceTime" Value='<%# Bind("RealRaceTime") %>' />
                                        <asp:TextBox ID="txtbxFinishTimeMMG" Width="25px" MaxLength="2" runat="server"> </asp:TextBox>:
                                        <asp:TextBox ID="txtbxFinishTimeSSG" Width="25px" MaxLength="2" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                           </asp:TemplateField>
                         <asp:BoundField DataField="FalseRails" ReadOnly="true" HeaderText="False Rails" ItemStyle-Width="2%" />
                         <asp:BoundField DataField="FRMeasurement" ReadOnly="true" HeaderText="Measurment Of False Rails" ItemStyle-Width="5%" />
                         <asp:TemplateField HeaderText="Penetrometer Reading" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxPenetrometerG" Width="55px" MaxLength="5" runat="server" Text='<%# Bind("PenetrometerReading") %>'></asp:TextBox>
                                         <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator3" Display="Dynamic" 
                                                ValidationGroup="RaceDaySituationG" runat="server" ControlToValidate="txtbxPenetrometerG"
                                                Text="*" ForeColor="Red" ErrorMessage="Please enter Penetrometer Reading."></asp:RequiredFieldValidator>
                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtbxPenetrometerG"
                                            ErrorMessage="Only numeric allowed." ForeColor="Red"
                                            ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="RaceDaySituationG">*
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Going" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdfieldGoing" Value='<%# Bind("Going") %>' />
                                         <asp:DropDownList ID="drpdwnGoingG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="Weather" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdfieldWeather" Value='<%# Bind("Weather") %>' />
                                         <asp:DropDownList ID="drpdwnWeatherG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Temperature(in C) " ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxtemperatureG" Width="55px" MaxLength="3" runat="server" Text='<%# Bind("Temperature") %>'></asp:TextBox>
                                    </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Rain In Morning" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldRainInMorning" Value='<%# Bind("RainInMorning") %>' />
                                        <asp:CheckBox ID="chkbxRainInMorningG" runat="server" />
                                    </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Rain Before Race" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldRainBeforeRace" Value='<%# Bind("RainBeforeRace") %>' />
                                        <asp:CheckBox ID="chkbxRainBeforeRaceG" runat="server" />
                                    </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Rain During Race" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldRainDuringRace" Value='<%# Bind("RainDuringRace") %>' />
                                        <asp:CheckBox ID="chkbxRainDuringRaceG" runat="server" />
                                    </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Other Factor" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxOtherFactor" Width="155px" runat="server" Text='<%# Bind("OtherFactor") %>'></asp:TextBox>
                                        <asp:AutoCompleteExtender ServiceMethod="AddOtherFactorList"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxOtherFactor"
                                            ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                       </asp:TemplateField>
                    </Columns>
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
