<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="CardResult.aspx.cs" Inherits="VKATalk.Card.CardResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <style type="text/css">
        .AutoExtender {
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

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            left: auto;
            margin: 0px;
            list-style-type: none;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
            margin: 0px;
            list-style-type: none;
        }

        .ui-dialog-titlebar-close {
            visibility: hidden;
        }

        .modalBackground {
            background-color: #333333;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
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
                    title: "Card Result",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function GetOwnerID(source, eventArgs) {
            //alert("1");
            //alert(source);
            if (source) {
                // Get the HiddenField ID.
                //alert("3");
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender3", "hdnfieldEmergencyColorID");
                //alert("4");
              //  alert(hiddenfieldID);
                $get(hiddenfieldID).value = eventArgs.get_value();
                //alert("5");
                //alert($get(hiddenfieldID).value);
            }
        }

        function GetJockeyID(source, eventArgs) {
            if (source) {
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender4", "hdnfieldProfessionalnameid");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function GetHorseNameID(source, eventArgs) {
            if (source) {
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender5", "hdnfieldHorseNameIDG");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        
       

        function OpenHorsePopup(value) {
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            var generalraceid = document.getElementById('<%=hdnfieldGeneralRaceIDM.ClientID %>').value;
            var divisionraceid = document.getElementById('<%=hdnfielddivisionraceidpopup.ClientID %>').value;
            var centerid = document.getElementById('<%=drpdwnCenterName.ClientID %>').value;
            var divisionracedate = document.getElementById('<%=txtbxDivisionRaceDate.ClientID %>').value;
            var enterdate = document.getElementById('<%=txtbxDeclarationEnterDate.ClientID %>').value;
            var horsenameid = document.getElementById('<%=hdnfieldHorseNameIDM.ClientID %>').value;
            if (value === "totaldivident") {
                window.open('../Card/ResultToteDivident.aspx?GeneralRaceNameID=' + generalracenameid + '&GeneralRaceID=' + generalraceid + '&DivisionRaceID=0&CenterMID=' + centerid + '&DivisionRaceDate=' + divisionracedate + '&DataEntryDate=' + enterdate, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "RaceCommentary") {
                window.open('../Card/ResultRaceCommentary.aspx?GeneralRaceNameID=' + generalracenameid + '&GeneralRaceID=' + generalraceid + '&DivisionRaceID=' + divisionraceid + '&CenterMID=' + centerid + '&DivisionRaceDate=' + divisionracedate + '&DataEntryDate=' + enterdate + '&HorseNameID=' + horsenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "RaceDayIncidentUpdate") {
                window.open('../Card/RaceDayIncident.aspx?GeneralRaceNameID=' + generalracenameid + '&GeneralRaceID=' + generalraceid + '&DivisionRaceID=' + divisionraceid + '&CenterMID=' + centerid + '&DivisionRaceDate=' + divisionracedate + '&DataEntryDate=' + enterdate + '&HorseNameID=' + horsenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "SactionalTiming") {
                window.open('../Card/ResultSectionalTiming.aspx?GeneralRaceNameID=' + generalracenameid + '&GeneralRaceID=' + generalraceid + '&DivisionRaceID=0&CenterMID=' + centerid + '&DivisionRaceDate=' + divisionracedate + '&DataEntryDate=' + enterdate + '&HorseNameID=' + horsenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "LapTiming") {
                window.open('../Card/ResultLapTiming.aspx?GeneralRaceNameID=' + generalracenameid + '&GeneralRaceID=' + generalraceid + '&DivisionRaceID=' + divisionraceid + '&CenterMID=' + centerid + '&DivisionRaceDate=' + divisionracedate + '&DataEntryDate=' + enterdate + '&HorseNameID=' + horsenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "ShoeDescription") {
                window.open('../Popups/AddShoeDescription.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Equipment") {
                window.open('../Popups/HorseEquipment.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Bit") {
                window.open('../Popups/HorseBit.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Bandage") {
                window.open('../Popups/HorseBandage.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "OwnerRecord") {
                window.open('../Popups/OwnerRecord.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Permanent") {
                window.open('../Popups/ProspectusMasterPermanentCondition.aspx?ProspectusId=NULL&RaceName=NULL&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }

        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Result</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <%--<fieldset style="width: 100%;" class="Userlogin">--%>
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardResult" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                Font-Size="12" />
                            <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            <asp:HiddenField ID="hdnfieldGeneralRaceIDM" runat="server" />
                            <asp:HiddenField ID="hdnfielddivisionraceidpopup" runat="server" />
                            <asp:HiddenField ID="hdnfieldHorseNameIDM" runat="server" />
                            <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameidMain" />
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
                                ValidationGroup="CardResult" />
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
                                ValidationGroup="CardResult" />
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
                        <td>Division Race Name:
                        </td>
                        <td style="width: 150px;">
                            <asp:Label ID="lblGeneralRaceName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnTotalDivident" runat="server" Text="Total Divident" OnClientClick="OpenHorsePopup('totaldivident')" />
                        </td>
                        <td>
                            <asp:Button ID="btnRaceCommentary" runat="server" Text="Race Commentary" OnClientClick="OpenHorsePopup('RaceCommentary')" />
                        </td>
                        <td>
                            <asp:Button ID="btnRaceDayIncident" runat="server" Text="Race Day Incident Update" OnClientClick="OpenHorsePopup('RaceDayIncidentUpdate')" />
                        </td>
                        <td>
                            <asp:Button ID="btnTrainerUpdate" runat="server" Text="Trainer Update" OnClientClick="OpenHorsePopup('TrainerUpdate')" />
                        </td>
                        <td>
                            <asp:Button ID="btnEquipment" runat="server" Text="Equipment Update" OnClientClick="OpenHorsePopup('Equipment')" />
                        </td>
                        <td>
                            <asp:Button ID="btnRaceDaySituationUpdate" runat="server" Text="Race Day Situation Update" OnClientClick="OpenHorsePopup('RaceDaySituationUpdate')" />
                        </td>
                        <td>
                            <asp:Button ID="btnSactionalTiming" runat="server" Text="Sactional Timing" OnClientClick="OpenHorsePopup('SactionalTiming')" />
                        </td>
                        <td>
                            <asp:Button ID="btnLapTiming" runat="server" Text="Lap Timing" OnClientClick="OpenHorsePopup('LapTiming')" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnOddsUpdate" runat="server" Text="Odds Update" OnClientClick="OpenHorsePopup('OddsUpdate')" />
                        </td>
                        <td>
                            <asp:Button ID="btnOwnerUpdate" runat="server" Text="Owner Update" OnClientClick="OpenHorsePopup('OwnerUpdate')" />
                        </td>
                        <td>
                            <asp:Button ID="btnBit" runat="server" Text="Bit Update" OnClientClick="OpenHorsePopup('Bit')" />
                        </td>

                    </tr>
                </table>

                <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                                DataKeyNames="GlobalID" EmptyDataText="No Record Found" OnRowDataBound="GvShowALL_RowDataBound">
                                <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo(*)" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="HNo(*)" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHORSENO" Width="55px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Horse Name(*)" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID") %>' />
                                            <asp:TextBox ID="txtbxHorseNameG" Width="155px" runat="server" 
                                                        AutoPostBack="true" OnTextChanged="txtbxHorseNameG_OnTextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnfieldHorseNameIDG" Value='<%# Bind("HorseNameID") %>' />
                                            <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                TargetControlID="txtbxHorseNameG"
                                                OnClientItemSelected="GetHorseNameID"
                                                ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false" UseContextKey="true">
                                            </asp:AutoCompleteExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Placing" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%--<asp:HiddenField runat="server" ID="hdnfieldPlacing" Value='<%# Bind("Placing") %>' />
                                            <asp:DropDownList ID="drpdwnPlacing" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtbxPlacing" runat="server" Width="25px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AWGBC" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblACCEPTANCEWEIGHTGBC" Width="25px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jockey(*)" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbxJockeyNameG" Width="155px" runat="server"
                                                    AutoPostBack="true" OnTextChanged="txtbxJockeyNameG_OnTextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnfieldProfessionalnameid" />
                                            <asp:AutoCompleteExtender ServiceMethod="AddJockeyList"
                                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                TargetControlID="txtbxJockeyNameG"
                                                OnClientItemSelected="GetJockeyID"
                                                ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false" UseContextKey="true">
                                            </asp:AutoCompleteExtender>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JA" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJA" Width="25px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Declared Weight(*)" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeclareWeight" Width="25px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carried Weight(*)" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbxCarriedWeightG" MaxLength="4" Width="55px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JkyWgts" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJkyWgts" Width="25px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Finish Time(*)" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbxFinishTimeMMG" Width="25px" MaxLength="1" runat="server"></asp:TextBox>:
                                        <asp:TextBox ID="txtbxFinishTimeSSG" Width="25px" MaxLength="2" runat="server"></asp:TextBox>:
                                        <asp:TextBox ID="txtbxFinishTimeMSG" Width="25px" MaxLength="3" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verdict Margin(*)" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnfielVerdictMargin" Value='<%# Bind("VerdictMargin") %>' />
                                            <asp:DropDownList ID="drpdwnVerdictMarginG" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Horse Body Weight(*)" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbxHorseBodyWeightG" Width="25px" MaxLength="3" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RHR (Revised Handicap Rating)" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbxRevisedRatingG" Width="25px" runat="server" MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvShowALL" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>


                <%-- </fieldset>--%>
            </td>
        </tr>
    </table>

    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CardResult" /></td>
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


    <div id="dvCardResult" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvCardResult" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="GlobalID" OnSelectedIndexChanged="GvCardResult_OnSelectedIndexChanged"
                    OnRowDataBound="GvCardResult_RowDataBound">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HorseNo" ReadOnly="true" HeaderText="HNo" ItemStyle-Width="2%" />
                        <asp:TemplateField HeaderText="Horse Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceIDG1" Value='<%# Bind("DivisionRaceID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldProfessionalnameidG1" Value='<%# Bind("JockeyNameID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameIDG1" Value='<%# Bind("HorseNameID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfielVerdictMarginG1" Value='<%# Bind("VerdictMargin") %>' />
                                <asp:LinkButton Text='<%# Bind("HorseName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Placing" ReadOnly="true" HeaderText="Placing" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="ACCEPTANCEWEIGHTGBC" ReadOnly="true" HeaderText="AWGBC" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="JockeyName" ReadOnly="true" HeaderText="Jockey" ItemStyle-Width="12%" />
                        <asp:BoundField DataField="JA" ReadOnly="true" HeaderText="JA" ItemStyle-Width="2%" />
                        <%--<asp:BoundField DataField="DeclaredWeight" ReadOnly="true" HeaderText="Declared Weight" ItemStyle-Width="5%" />--%>
                         <asp:TemplateField HeaderText="Declared Weight" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeclaredWeight" Width="25px" runat="server" Text='<%# Bind("DeclaredWeight") %>'></asp:Label>
                                        </ItemTemplate>
                         </asp:TemplateField>
<%--                        <asp:BoundField DataField="CarriedWeight" ReadOnly="true" HeaderText="Carried Weight" ItemStyle-Width="5%" />--%>
                         <asp:TemplateField HeaderText="Carried Weight" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCarriedWeight" Width="25px" runat="server" Text='<%# Bind("CarriedWeight") %>'></asp:Label>
                                        </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField DataField="JkyWgts" ReadOnly="true" HeaderText="JkyWgts" ItemStyle-Width="2%" />
                        <asp:TemplateField HeaderText="Finish Time" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <%# Eval("MM")%>:<%# Eval("SS")%>:<%# Eval("MS")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:BoundField DataField="VerdictMargin" ReadOnly="true" HeaderText="Verdict Margin" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HorseBodyWeight" ReadOnly="true" HeaderText="Horse Body Weight" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="RevisedRating" ReadOnly="true" HeaderText="RHR (Revised Handicap Rating)" ItemStyle-Width="5%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
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
