<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Declaration.aspx.cs" Inherits="VKATalk.Card.Declaration" %>

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
                    title: "Declaration",
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
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender3", "hdnfieldProfessionalNameid1");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function OpenHorsePopup(value) {
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            if (value === "RaceCard") {
                window.open('../Master/ProspectusGeneralRaceCardCondition.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                //window.open('../Master/MasterRaceCardCondition.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');

            }
            else if (value === "ShoeDescription") {
                window.open('../Popups/HorseShoeDescription.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
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
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Declaration</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
            <table>
                <tr>
                            <td>
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardDeclaration" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                                <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameid" />
                            </td>
                        </tr>
            </table>
                    <table>
                        
                        <tr>
                            <td>Data Entry Date:(*)</td>
                            <td>
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
                                    ValidationGroup="CardDeclaration" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDeclarationEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width:50px;"></td>
                            <td>Division Race Date:(*)</td>
                            <td>
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
                                    ValidationGroup="CardDeclaration" />
                                <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDivisionRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width:50px;"></td>
                            <td>Center:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterName" AutoPostBack="True" OnSelectedIndexChanged="drpdwnCenterName_SelectIndexChange">
                                    <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width:50px;"></td>
                            <td>Season:</td>
                            <td>
                                <b>
                                    <asp:Label ID="lblSeason" runat="server"></asp:Label></b>
                            </td>
                            <td style="width:50px;"></td>
                            <td>Year:</td>
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
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceNameID" Value='<%# Bind("GeneralRaceNameID") %>' />
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
                            <td style="width: 95px">Declaration: 
                            </td>
                            <td>
                                <asp:Label ID="lblEntryDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table border="1">
                        <tr>
                            <td>Declaration Race Name:
                            </td>
                            <td>
                                <asp:Label ID="lblGeneralRaceName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnRaceCardCondition" runat="server" Text="Race Card Condition Update" OnClientClick="OpenHorsePopup('RaceCard')" />
                            </td>
                            <td>
                                <asp:Button ID="btnShoe" runat="server" Text="Shoe (Shoe Description) Update" OnClientClick="OpenHorsePopup('ShoeDescription')" />
                            </td>
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
                <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GvShowALL_OnSelectedIndexChanged" 
                    OnRowDataBound="GvShowALL_RowDataBound">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo" ItemStyle-Width="2%" />
                        <%--<asp:BoundField DataField="HORSENO" ReadOnly="true" HeaderText="HNo(*)" ItemStyle-Width="2%" />--%>
                        <asp:TemplateField HeaderText="HNo" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:label ID="lblHorseNo" Width="15px" runat="server" Text='<%# Bind("HORSENO") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Horse Name(*)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:label ID="lblhorsename" runat="server" Text='<%# Bind("HorseName") %>'></asp:label>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielHorseID" Value='<%# Bind("HorseID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldShoeID" Value='<%# Bind("ShoeMID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldShoeMetalID" Value='<%# Bind("ShoeMetalMID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseBandageID" Value='<%# Bind("HorseBandageID") %>' />
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField DataField="HANDICAPRATING" ReadOnly="true" HeaderText="HR" ItemStyle-Width="2%" />
                         <asp:TemplateField HeaderText="AW GBC(*)" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:label ID="lblACCEPTANCEWEIGHTGBC" Width="25px" runat="server" Text='<%# Bind("ACCEPTANCEWEIGHTGBC") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>

                        <asp:TemplateField HeaderText="Jockey Name(*)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxJockeyNameG" Width="175px" runat="server" Text='<%# Bind("JockeyName") %>' AutoPostBack="true" OnTextChanged="txtbxJockeyNameG_OnTextChanged"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameid1"/>
                                       <asp:AutoCompleteExtender ServiceMethod="AddJockeyList"
                                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                TargetControlID="txtbxJockeyNameG" 
                                                OnClientItemSelected="GetOwnerID"
                                                ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false" UseContextKey="true">
                                            </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="DJA" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:label ID="lblDJA" Width="25px" runat="server" Text='<%# Bind("DJA") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>

                        <asp:TemplateField HeaderText="Declare Weight" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxDeclareWeightG" Width="25px" runat="server" Text='<%# Bind("DeclareWeight") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Draw No(*)" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxDrawNoG" Width="25px" runat="server" Text='<%# Bind("DrawNo") %>' MaxLength="2"></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shoe(*)" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:label ID="lblShoe" Width="15px" runat="server" Text='<%# Bind("Shoe") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="ShoeMetal(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:label ID="lblShoeMetal" Width="15px" runat="server" Text='<%# Bind("ShoeMetal") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>

                        <%--<asp:BoundField DataField="Shoe" HeaderText="Shoe" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="ShoeMetal" HeaderText="Shoe Metal" ItemStyle-Width="5%" />--%>
                        <asp:BoundField DataField="BandageType" HeaderText="Bandage" ItemStyle-Width="5%" />
                         <asp:TemplateField HeaderText="Equipment" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:label ID="lblEquipmentAlias" Width="55px" runat="server" Text='<%# Bind("EquipmentAlias") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Bit" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:label ID="lblBitAlias" Width="55px" runat="server" Text='<%# Bind("BitAlias") %>'></asp:label>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField DataField="Trainer" HeaderText="Trainer" ItemStyle-Width="15%" />
                        <%--<asp:BoundField DataField="Owner" HeaderText="Owner" ItemStyle-Width="75%" />--%>
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
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CardDeclaration" /></td>
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
    <br />
    <br />
    <div id="Div1" style="height: 400px; width: 99%; overflow: auto;" runat="server">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
      <asp:GridView ID="GrdVwDeclarationDisplay" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HORSENO" ReadOnly="true" HeaderText="HNo" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HorseName" ReadOnly="true" HeaderText="Horse Name" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="HANDICAPRATING" ReadOnly="true" HeaderText="HR" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="ACCEPTANCEWEIGHTGBC" ReadOnly="true" HeaderText="AW GB" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="JockeyName" ReadOnly="true" HeaderText="Jockey Name" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="DJA" ReadOnly="true" HeaderText="DJA" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="DeclareWeight" ReadOnly="true" HeaderText="Declare Weight" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="DrawNo" ReadOnly="true" HeaderText="Draw No" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Shoe" ReadOnly="true" HeaderText="Shoe" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="ShoeMetal" ReadOnly="true" HeaderText="Shoe Metal" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="BandageType" ReadOnly="true" HeaderText="Bandage" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="EquipmentAlias" ReadOnly="true" HeaderText="Equipment" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="BitAlias" ReadOnly="true" HeaderText="Bit" ItemStyle-Width="4%" />
                        <asp:BoundField DataField="Trainer" HeaderText="Trainer" ItemStyle-Width="15%" />
                        <%--<asp:BoundField DataField="Owner" HeaderText="Owner" ItemStyle-Width="75%" />--%>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
                </ContentTemplate>
             </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
