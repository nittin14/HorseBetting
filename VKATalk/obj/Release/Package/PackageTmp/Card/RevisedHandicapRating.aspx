<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevisedHandicapRating.aspx.cs" Inherits="VKATalk.Card.RevisedHandicapRating" %>

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

       
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Revised Handicap Rating</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RevisedHandicapRating" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
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
                                    ValidationGroup="RevisedHandicapRating" />
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
                                    ValidationGroup="RevisedHandicapRating" />
                                <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton6" runat="server" TargetControlID="txtbxDivisionRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width: 100px">Center:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterName" AutoPostBack="True" OnSelectedIndexChanged="drpdwnCenterName_SelectIndexChange">
                                    <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <%--<td style="width: 100px">Base Center:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnBasecenter" AutoPostBack="True" OnSelectedIndexChanged="drpdwnBasecenter_SelectIndexChange">
                                    <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>--%>
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

                    <%--<div id="dvgridview" style="width: 100%; overflow: auto;" runat="server" visible="False">
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
                                <asp:BoundField DataField="PamanentCondition" HeaderText="Pamanent Condition" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="SeasonalCondition" HeaderText="Seasonal Condition" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="RaceCardCondition" HeaderText="Race Card Condition" ItemStyle-Width="20%" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>--%>

                    <table border="1">
                        <tr>
                            <td style="width: 95px">Revised Rating: 
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblEntryDate" runat="server"></asp:Label>
                            </td>
                            <td style="width: 175px">1st Revised:
                            </td>
                             <td style="width: 105px">
                                <asp:Label ID="lbl1stRevised" runat="server"></asp:Label>
                            </td>
                            <td style="width: 175px">2nd Revised:
                            </td>
                             <td style="width: 105px">
                                <asp:Label ID="lbl2ndRevise" runat="server"></asp:Label>
                            </td>
                            <td style="width: 175px">3rd Revised:
                            </td>
                             <td style="width: 105px">
                                <asp:Label ID="lbl3rdRevise" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                   <%-- <table border="1">
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
                    </table>--%>

                    <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found" >
                    <%--OnSelectedIndexChanged="GvShowALL_OnSelectedIndexChanged"> --%>
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                         <%--<asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="Horse Name(*)" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:label ID="lblhorsename" runat="server" Text='<%# Bind("HorseName") %>'></asp:label>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielHorseID" Value='<%# Bind("HorseID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Bind("DivisionRaceID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielGeneralRaceNameID" Value='<%# Bind("GeneralRaceNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielGeneralRaceID" Value='<%# Bind("GeneralRaceID") %>' />
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField DataField="A C G" HeaderText="A C G" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="DayRaceNo" ReadOnly="true" HeaderText="DRNo" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HORSENO" ReadOnly="true" HeaderText="HNo" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="HandicapRating" HeaderText="CHR" ItemStyle-Width="5%" />
                        <%--<asp:BoundField DataField="MyHandicapRating" HeaderText="CMyHR" ItemStyle-Width="5%" />--%>
                        <asp:BoundField DataField="Placing" HeaderText="Placing" ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="RHR" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxRevisedHandicapRating" Width="55px" MaxLength="3" runat="server" 
                                            Text='<%# Bind("RevisedHandicapRating") %>'></asp:TextBox>
                                    </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="RMyHR" ItemStyle-Width="5%" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxRevisedMyHandicapRating" Width="55px" MaxLength="3" runat="server" 
                                            Text='<%# Bind("RevisedMyHandicapRating") %>'></asp:TextBox>
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

                    
                </fieldset>
            </td>
        </tr>
    </table>

    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="RevisedHandicapRating" /></td>
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click" /></td>
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
                    <div id="DvRevisedRating" style="height: 300px; width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="GvRevisedRating" runat="server" Width="99%" AutoGenerateColumns="false"
                            DataKeyNames="RevisedHandicapRatingCID" EmptyDataText="No Entry Found"
                            OnRowEditing="RowEdit" OnRowDeleting="GvRevisedRating_RowDeleting"
                            OnRowUpdating="RowUpdate" OnRowCancelingEdit="CancelEdit" OnRowDataBound="GvRevisedRating_RowDataBound">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                 <asp:CommandField ShowEditButton="True" ItemStyle-Width="5%" />
                                <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="5%" />
                                <asp:TemplateField HeaderText="Horse Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblhorsename" runat="server" Text='<%# Eval("HorseName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="A C G" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblACG" runat="server" Text='<%# Eval("A C G")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="DRNo" ItemStyle-Width="2%">
                            <ItemTemplate>
                                <asp:Label ID="lblDayRaceNo" runat="server" Text='<%# Eval("DayRaceNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="HNo" ItemStyle-Width="2%">
                            <ItemTemplate>
                                <asp:Label ID="lblHorseNo" runat="server" Text='<%# Eval("HorseNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="CHR" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblHandicapRating" runat="server" Text='<%# Eval("HandicapRating")%>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Placing" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblPlacing" runat="server" Text='<%# Eval("Placing")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="RHR" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRevisedHandicapRatingG" runat="server" Text='<%# Eval("RevisedHandicapRating")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtbxRevisedHandicapRatingG" runat="server"
                                    Text='<%# Eval("RevisedHandicapRating")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="RMyHR" ItemStyle-Width="5%" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRevisedMyHandicapRatingG" runat="server" Text='<%# Eval("RevisedMyHandicapRating")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtbxRevisedMyHandicapRatingG" runat="server"
                                    Text='<%# Eval("RevisedMyHandicapRating")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                            
                        </asp:GridView>
                    </div>

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
