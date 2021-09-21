<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceCard.aspx.cs" Inherits="VKATalk.Card.RaceCard" %>

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
                    title: "Race Card",
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
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender3", "hdnfieldEmergencyColorID");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function GetJockeyID(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender4", "hdnfieldProfessionalnameid1");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function OpenHorsePopup(value) {
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            if (value === "RaceCard") {
                window.open('../Popups/ProspectusGeneralRaceCardCondition.aspx?ProspectusGeneralId=NULL&MasterID=0&MasterRaceName=NULL&GeneralRaceName=NULL&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Seasonal") {
                window.open('../Popups/ProspectusGeneralSeasonalCondition.aspx?ProspectusGeneralId=NULL&MasterID=0&MasterRaceName=NULL&GeneralRaceName=NULL&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
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
            else if (value === "Show") {
               
                var parm = document.getElementById('<%= drpdwnCenterName.ClientID %>');
                var value = parm.options[parm.selectedIndex].text;
                window.open('../Reports/RaceCardReport.aspx?RaceDate=' + document.getElementById('<%= txtbxDivisionRaceDate.ClientID %>').value + '&CenterID=' + document.getElementById('<%= drpdwnCenterName.ClientID %>').value
                    + '&CenterName=' + value
                    + '&Season=' + document.getElementById('<%= lblSeason.ClientID %>').innerText
                    + '&Year=' + document.getElementById('<%= lblYear.ClientID %>').innerText
                    , '_blank', 'status=yes, menubar=no, location=center');
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Race Card</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RaceCard" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                                <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameid" />
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
                                    ValidationGroup="RaceCard" />
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
                                    ValidationGroup="RaceCard" />
                                <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton6" runat="server" TargetControlID="txtbxDivisionRaceDate"
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

                    <div id="dvgridview" style="width: 99%; overflow: auto;" runat="server" visible="False">
                        <asp:GridView ID="grdvwRaceDetail" runat="server" Width="95%"
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
                                <asp:BoundField DataField="PermanentCondition" HeaderText="Pamanent Condition" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="SeasonalCondition" HeaderText="Seasonal Condition" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="RaceCardCondition" HeaderText="Race Card Condition" ItemStyle-Width="20%" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>

                    <table border="1">
                        <tr>
                            <td style="width: 95px">Race: 
                            </td>
                            <td>
                                <asp:Label ID="lblEntryDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table border="1">
                        <tr>
                            <td>Division Race Name:
                            </td>
                            <td>
                                <asp:Label ID="lblGeneralRaceName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnPermanentCondition" runat="server" Text="Permanent Condition Update" OnClientClick="OpenHorsePopup('Permanent')" />
                            </td>
                            <td>
                                <asp:Button ID="btnSeasonalCondition" runat="server" Text="Seasonal Condition Update" OnClientClick="OpenHorsePopup('Seasonal')" />
                            </td>
                            <td>
                                <asp:Button ID="btnRaceCardCondition" runat="server" Text="Race Card Condition Update" OnClientClick="OpenHorsePopup('RaceCard')" />
                            </td>
                            <td>
                                <asp:Button ID="btnShoe" runat="server" Text="Shoe (Shoe Description) Update" OnClientClick="OpenHorsePopup('ShoeDescription')" />
                            </td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnEquipment" runat="server" Text="Equipment Update" OnClientClick="OpenHorsePopup('Equipment')" />
                            </td>
                            <td>
                                <asp:Button ID="btnBit" runat="server" Text="Bit Update" OnClientClick="OpenHorsePopup('Bit')" />
                            </td>
                            <td>
                                <asp:Button ID="btnBandage" runat="server" Text="Bandage Update" OnClientClick="OpenHorsePopup('Bandage')" />
                            </td>
                            <td>
                                <asp:Button ID="btnOwnerRecord" runat="server" Text="Owner Record Update" OnClientClick="OpenHorsePopup('OwnerRecord')" />
                            </td>
                        </tr>
                    </table>

                    <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <asp:GridView ID="GvShowALL" runat="server" Width="95%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found" 
                    OnSelectedIndexChanged="GvShowALL_OnSelectedIndexChanged" onrowdatabound="GvShowALL_RowDataBound">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo(*)" ItemStyle-Width="2%" />
                        <%--<asp:BoundField DataField="HORSENO" ReadOnly="true" HeaderText="HNo(*)" ItemStyle-Width="2%" />--%>
                         <asp:TemplateField HeaderText="HNo(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:label ID="lblHorseNo" Width="15px" runat="server" Text='<%# Bind("HORSENO") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <%--<asp:BoundField DataField="HORSENAME" ReadOnly="true" HeaderText="Horse Name(*)" ItemStyle-Width="30%" />--%>
                        <asp:TemplateField HeaderText="Horse Name(*)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:label ID="lblhorsename" runat="server" Text='<%# Bind("HorseName") %>'></asp:label>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielHorseID" Value='<%# Bind("HorseID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID") %>' />
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="AWGBC" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:label ID="lblACCEPTANCEWEIGHTGBC" Width="25px" runat="server" Text='<%# Bind("ACCEPTANCEWEIGHTGBC") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField DataField="Owner" HeaderText="Owner(*)" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="OwnerColor" HeaderText="Owner Color(*)" ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="Owner Cap Color(*)" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpdwnOwnerColorCap" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Emergency Owner Color" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxEmergencyOwnerColorG" Width="75px" runat="server" Text='<%# Bind("EmergencyOwnerColor") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Changed Jockey " ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxJockeyNameG" Width="155px" runat="server" Text='<%# Bind("JockeyName") %>' AutoPostBack="true" OnTextChanged="txtbxJockeyNameG_OnTextChanged"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalnameid1" Value='<%# Bind("ProfessionalNameID") %>' />
                                       <asp:AutoCompleteExtender ServiceMethod="AddJockeyList"
                                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                TargetControlID="txtbxJockeyNameG" 
                                                OnClientItemSelected="GetJockeyID"
                                                ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                            </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="CJA" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:label ID="lblDJA" Width="55px" runat="server" Text='<%# Bind("CJA") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="CDW" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxCDWG" Width="75px" runat="server" Text='<%# Bind("CDW") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jockey Change Reason" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpdwnJockeyChangeReason" style="width: 125px;" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Horse Running Status" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpdwnHorseRunningStatus" style="width: 125px;" runat="server"></asp:DropDownList>
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
               
            </td>
        </tr>
    </table>

    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="RaceCard" /></td>
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" OnClientClick="OpenHorsePopup('Show')" /></td>
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

    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
