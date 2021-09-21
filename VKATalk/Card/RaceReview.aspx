<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceReview.aspx.cs" Inherits="VKATalk.Card.RaceReview" %>

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
                    title: "Race Review",
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
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            if (value === "Veterinery") {
                window.open('../Popups/HorseVeterinaryProblem.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Equipment") {
                window.open('../Popups/HorseEquipment.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "OwnerRecord") {
                window.open('../Popups/OwnerRecord.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Race Review</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RaceReview" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Acceptance Enter Date:(*)</td>
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
                                    ValidationGroup="RaceReview" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="padding-left: 25px;">Division Race Date:(*)</td>
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
                                    ValidationGroup="RaceReview" />
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
                    <div id="dvgridview" style="width: 100%; overflow: auto;" runat="server" visible="False">
                        <asp:GridView ID="grdvwRaceDetail" runat="server" Width="100%"
                            AutoGenerateColumns="False" DataKeyNames="DivisionRaceID" EmptyDataText="No Detail Found."
                            OnSelectedIndexChanged="grdvwRaceDetail_OnSelectedIndexChanged">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="DayRaceNo" HeaderText="DRNo" ItemStyle-Width="3%" />
                                <asp:BoundField DataField="SeasonRaceNo" HeaderText="SRNo" ItemStyle-Width="3%" />
                                <asp:BoundField DataField="RaceTime" HeaderText="Race Time" ItemStyle-Width="5%" />
                                <asp:TemplateField HeaderText="Division Race Name" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfielddivisionracename" Value='<%# Bind("DivisionRaceName") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceNameIDG" Value='<%# Bind("GeneralRaceNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceID" Value='<%# Bind("GeneralRaceID") %>' />
                                        <asp:LinkButton Text='<%# Bind("DivisionRaceName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    


                    

                    <table border="1">
                        <tr>
                            <td>General Race Name:
                            </td>
                            <td>
                                <asp:Label ID="lblGeneralRaceName" runat="server"></asp:Label>
                            </td>
                             <td>
                                <asp:Button ID="btnRaceDaySituationUpdate" runat="server" Text="Race Day Situation Update" OnClientClick="OpenHorsePopup('RaceDaySituationUpdate')" />
                            </td>
                           <td>
                                <asp:Button ID="btnDirectGate" runat="server" Text="Direct Gate" OnClientClick="OpenHorsePopup('directgate')" />
                            </td>
                            <td>
                                <asp:Button ID="btnCurrentMissionUpdate" runat="server" Text="Current Mission Update" OnClientClick="OpenHorsePopup('CurrentMissionUpdate')" />
                            </td>
                            <td>
                                <asp:Button ID="btnObservationShortTerm" runat="server" Text="My Observation Short Term Update" OnClientClick="OpenHorsePopup('Observationshortterm')" />
                            </td>
                            <td>
                                <asp:Button ID="btnBandage" runat="server" Text="Bandage Update" OnClientClick="OpenHorsePopup('Bandage')" />
                            </td>
                             <td>
                                <asp:Button ID="btnEquipment" runat="server" Text="Equipment Update" OnClientClick="OpenHorsePopup('Equipment')" />
                            </td>
                            </tr>
                        <tr>
                            <td></td>
                            <td></td>
                             <td>
                                <asp:Button ID="btnTrainerUpdate" runat="server" Text="Trainer Update" OnClientClick="OpenHorsePopup('TrainerUpdate')" />
                            </td>
                            <td>
                                <asp:Button ID="btnRaceDayIncident" runat="server" Text="Race Day Incident Update" OnClientClick="OpenHorsePopup('RaceDayIncidentUpdate')" />
                            </td>
                           <td>
                                <asp:Button ID="btnJockey" runat="server" Text="Jockey Weight Update" OnClientClick="OpenHorsePopup('JockeyUpdate')" />
                            </td>
                            <td>
                                <asp:Button ID="btnCurentForm" runat="server" Text="Current Form Update" OnClientClick="OpenHorsePopup('CurrentForm')" />
                            </td>
                            <td>
                                <asp:Button ID="btnObservationLongTerm" runat="server" Text="My Observation Long Term Update" OnClientClick="OpenHorsePopup('Observationlongterm')" />
                            </td>
                            <td>
                                <asp:Button ID="btnOdds" runat="server" Text="Odds Update" OnClientClick="OpenHorsePopup('OddsUpdate')" />
                            </td>
                            </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnBit" runat="server" Text="Bit Update" OnClientClick="OpenHorsePopup('BitUpdate')" />
                            </td>
                             <td>
                                <asp:Button ID="btnOwner" runat="server" Text="Owner Update" OnClientClick="OpenHorsePopup('OwnerUpdate')" />
                            </td>
                        </tr>
                    </table>

                     <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
                <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GvShowALL_OnSelectedIndexChanged" 
                     OnRowDataBound ="GvShowALL_RowDataBound">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                       <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo(*)" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HORSENO" ReadOnly="true" HeaderText="HNo(*)" ItemStyle-Width="2%" />
                        <asp:TemplateField HeaderText="Horse Name(*)" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:label ID="lblhorsename" runat="server" Text='<%# Bind("HorseName") %>'></asp:label>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseName" Value='<%# Bind("HorseName") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielHorseID" Value='<%# Bind("HorseID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldJockeyID" Value='<%# Bind("JockeyID") %>' />
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="Placing" ReadOnly="true" HeaderText="Placing(*)" ItemStyle-Width="2%" />
                          <asp:BoundField DataField="JockeyName" ReadOnly="true" HeaderText="Jockey(*)" ItemStyle-Width="12%" />
                        <asp:BoundField DataField="DeclaredWeight" ReadOnly="true" HeaderText="Declared Weight(*)" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="CarriedWeight" ReadOnly="true" HeaderText="Carried Weight(*)" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="HorseBodyWeight" ReadOnly="true" HeaderText="Horse Body Weight {Last/Current}" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="FinishTime" ReadOnly="true" HeaderText="Finish Time(*)" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="VerdictMargin" ReadOnly="true" HeaderText="Verdict Margin(*)" ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="Paddock Condition" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <asp:Label ID="lblPeddockConditionG" runat="server" Text='<%# Eval("PaddockCondition") %>' Visible = "false" />
                                        <asp:DropDownList ID="drpdwnPeddockConditionG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Early Gategoer" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <%--<asp:CheckBox ID="chkbxGategoer" runat="server" Checked='<%# Bind("EarlyGategoer") %>' />--%>
                                        <asp:CheckBox ID="chkbxGategoer" runat="server" Checked='<%# Convert.ToBoolean(Eval("EarlyGategoer")) %>' />
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="1st Sectional Position" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxSectionalPositionG" Width="55px" runat="server" MaxLength="2" Text='<%# Bind("1stSectionalPosition") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Band Position" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxBandPosition" Width="55px" runat="server" MaxLength="2" Text='<%# Bind("BandPosition") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Last Sectional Position" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxLastSectionalPosition" Width="55px" runat="server" MaxLength="2" Text='<%# Bind("LastSectionalPosition") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Run Quality" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRunQualityG" runat="server" Text='<%# Eval("RunQuality") %>' Visible = "false" />
                                        <asp:DropDownList ID="drpdwnRunQualityG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Trainer On Board Effort" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrainerOnBoardEffectG" runat="server" Text='<%# Eval("TrainerOnBoardEffort") %>' Visible = "false" />
                                        <asp:DropDownList ID="drpdwnTrainerOnBoardEffectG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jockey On Board Effort" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJockeyOnBoardEffectG" runat="server" Text='<%# Eval("JockeyOnBoardEffort") %>' Visible = "false" />
                                        <asp:DropDownList ID="drpdwnJockeyOnBoardEffectG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Untry Due To Any Mate" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUntryDueG" runat="server" Text='<%# Eval("UntryDuetoAnyMate") %>' Visible = "false" />
                                        <asp:DropDownList ID="drpdwnUntryDuetoAnyMateG" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Betting Order" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <asp:Label ID="lblInBettingOrderG" runat="server" Text='<%# Eval("BettingOrder") %>' Visible = "false" />
                                        <asp:DropDownList ID="drpdwnInBettingOrderG" runat="server"></asp:DropDownList>
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
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="RaceReview" /></td>
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" /></td>
            <td>
                <asp:Button runat="server" ID="btnPdf" Text="PDF" /></td>
           <%-- <td>
                <asp:Button runat="server" ID="btnEdit" Text="Edit" /></td>--%>

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
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
          <%--  <td>
                <asp:Button runat="server" ID="btnHandicapShow" Text="Show Handicap" OnClick="btnHandicapShow_Click" /></td>
            <td>--%>
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
    
    
    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
