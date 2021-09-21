<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardGatePractiseEntry.aspx.cs" Inherits="VKATalk.Card.CardGatePractiseEntry" %>

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
                    title: "Card Gate Practise",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };


        function GetSourceID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldProfessionalnameid.ClientID %>').value = HdnKey;
        }


        function GetSourceID3(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldSourceIndividualID.ClientID %>').value = HdnKey;
        }

        function GetSourceID4(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldSourceName2.ClientID %>').value = HdnKey;
        }

        
        function GetHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID.ClientID %>').value = HdnKey;
        }
        
        
        function GetHorseID2(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameIDIndividual.ClientID %>').value = HdnKey;
        }
        
        function GetRiderID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID.ClientID %>').value = HdnKey;
        }
        
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Gate Practise Entry</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="GatePractise" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GatePractise1" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GatePractise2" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                 <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="GatePractise3" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="GatePractise4" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%;">Data Entry Date:(*)</td>
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
                                    ValidationGroup="GatePractise" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            </tr>
                        <tr>
                            <td>Source Name:(*)</td>
                            <td>
                                    <asp:TextBox ID="txtbxSourceName" Width="455px" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalnameid" />
                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ServiceMethod="AddSourceNameList"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxSourceName" CompletionListElementID="listPlacement"
                                            OnClientItemSelected="GetSourceID"
                                            ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" 
                                    ValidationGroup="GatePractise" runat="server" ControlToValidate="txtbxSourceName"
                                    Text="*" ErrorMessage="Please enter Source Name."></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        <tr>
                            <td>Gate Practice Date:(*)</td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtbxRaceDate" runat="server" Width="75px"></asp:TextBox>
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
                                    ValidationGroup="GatePractise" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            </tr>
                        <tr>
                            <td style="width: 50px">Center:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterName">
                                </asp:DropDownList>
                            </td>
                            </tr>
                        <tr>
                            <td style="width: 50px">Gate Practice Lot No:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnLotNo">
                                    <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            </tr>
                        <tr>
                            <td>Distance:(*)</td>
                            <td>
                                    <asp:DropDownList ID="drpdwnDistance" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" InitialValue="-1" 
                                    ValidationGroup="GatePractise" runat="server" ControlToValidate="drpdwnDistance"
                                    Text="*" ErrorMessage="Please select distance."></asp:RequiredFieldValidator>
                            </td>
                            <tr>
                            <td>Track:(*)</td>
                            <td>
                                    <asp:DropDownList ID="drpdwnTrack" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpdwnDistance_SelectIndexChange"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" InitialValue="-1" 
                                    ValidationGroup="GatePractise" runat="server" ControlToValidate="drpdwnTrack"
                                    Text="*" ErrorMessage="Please select track."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                       <tr>
                           <td>
                               <asp:Button ID="btnMockRaceAdd" Text="Add" runat="server" OnClick="btnAdd_Click" ValidationGroup="GatePractise" />
                           </td>
                           <td>
                               <asp:Button ID="btnMockRaceEntryClear" Text="Clear" OnClick="btnMockRaceEntryClear_Click" runat="server" />
                           </td>
                           <td>
                               <asp:Button ID="btnMockRaceEntryDelete" Text="Delete" OnClick="btnMockRaceEntryDelete_Click" runat="server"  ValidationGroup="GatePractise"/>
                           </td>
                       </tr>
                   </table>
                    <div id="DvMockRaceEntry" style="width: 50%; overflow: auto;" runat="server">
                                <asp:GridView ID="GvMockRace" runat="server" Width="100%" AutoGenerateColumns="false"
                                    DataKeyNames="GatePracticeCID" EmptyDataText="No Record Found"
                                    OnRowCommand="GvMockRace_OnSelectedIndexChanged">
                                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                        HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="ProfessionalName" ReadOnly="true" HeaderText="Source Name" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="MockRaceDate" ReadOnly="true" HeaderText="Gate Practice Date" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="CenterName" ReadOnly="true" HeaderText="Center" ItemStyle-Width="2%" />
                                        <asp:TemplateField HeaderText="Gate Practice Lot No" ItemStyle-Width="12%">
                                         <ItemTemplate>
                                             <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameidG" Value='<%# Bind("ProfessionalNameID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldTrackMID" Value='<%# Bind("TrackMID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldGatePracticeLotNo" Value='<%# Bind("GatePracticeLotNo") %>' />
                                             <asp:LinkButton Text='<%# Bind("GatePracticeLotNo") %>' ID="lnkMockRaceNo" runat="server" CommandName="MockRaceNo" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distance" ItemStyle-Width="8%">
                                         <ItemTemplate>
                                             <asp:HiddenField runat="server" ID="hdnfieldDistance" Value='<%# Bind("Distance") %>' />
                                             <asp:LinkButton Text='<%# Bind("Distance") %>' ID="lnkDistance" runat="server" CommandName="Distance"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TrackName" ReadOnly="true" HeaderText="Track" ItemStyle-Width="2%" />
                                        <asp:BoundField DataField="GatePracticeWinner" ReadOnly="true" HeaderText="Gate Practice Winner" ItemStyle-Width="12%" />
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Left" />
                                </asp:GridView>
                </div>
                    <br />
                    <table>
                        <tr>
                            <td>Gate Practice Participating Horses Entry:</td>
                            <td><b>
                                <asp:Label ID="lblHorseParticipatingEntry" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnfieldMockRaceID" runat="server" />
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>Placing:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxPlacing" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" 
                                    ValidationGroup="GatePractise1" runat="server" ControlToValidate="txtbxPlacing"
                                    Text="*" ErrorMessage="Please enter Placing."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            ControlToValidate="txtbxPlacing"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="GatePractise1"
                                            runat="server">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>Horse Name:(*)</td>
                            <td>
                                    <asp:TextBox ID="txtbxHorseName" Width="455px" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" />
                                <div id="Div1" style="height: 300px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxHorseName" CompletionListElementID="Div1"
                                            OnClientItemSelected="GetHorseID"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" 
                                    ValidationGroup="GatePractise1" runat="server" ControlToValidate="txtbxHorseName"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        <tr>
                            <td>Rider Name:(*)</td>
                            <td>
                                    <asp:TextBox ID="txtbxRiderName" Width="455px" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldRiderNameID" />
                                <div id="Div2" style="height: 300px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxRiderName" CompletionListElementID="Div2"
                                            OnClientItemSelected="GetRiderID"
                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" 
                                    ValidationGroup="GatePractise1" runat="server" ControlToValidate="txtbxRiderName"
                                    Text="*" ErrorMessage="Please enter Rider Name."></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        <tr>
                            <td>Finish Time:</td>
                            <td>
                                <asp:TextBox ID="txtbxMM" Width="20px" MaxLength="2" runat="server"></asp:TextBox>:
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                            ControlToValidate="txtbxMM"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="GatePractise1"
                                            runat="server">*</asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtbxSS" Width="20px" MaxLength="2" runat="server"></asp:TextBox>:
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                            ControlToValidate="txtbxSS"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="GatePractise1"
                                            runat="server">*</asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtbxPPP" Width="20px" MaxLength="3" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                            ControlToValidate="txtbxPPP"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="GatePractise1"
                                            runat="server">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Verdict Margin:</td>
                            <td>
                               <asp:DropDownList ID="drpdwnVerdictMargin" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Common Comment:</td>
                            <td>
                               <asp:TextBox ID="txtbxComment" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            <div id="Div5" style="height:300px; overflow-y:scroll;" ></div>
                              <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                  CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtbxComment" CompletionListElementID="Div5"
                                ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                            </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                    </table>
                    <table>
                       <tr>
                           
                           <td>
                               <asp:Button ID="btnHorseAdd" Text="Add" runat="server" OnClick="btnHorseAdd_Click" ValidationGroup="GatePractise1" />
                           </td>
                           <td>
                               <asp:Button ID="btnClearHorse" runat="server" OnClick="btnClearHorse_Click" Text="Clear" />
                           </td>
                           <td>
                               <asp:Button ID="btnDeleteHorse" Text="Delete" OnClick="btnDeleteHorse_Click" runat="server"  ValidationGroup="GatePractise1"/>
                           </td>
                       </tr>
                   </table>
                    <div id="Div3" style="width: 50%; overflow: auto;" runat="server">
                                <asp:GridView ID="GrdViewHorseEntry" runat="server" Width="100%" AutoGenerateColumns="false"
                                    DataKeyNames="GPParticipatingHorsesCID" EmptyDataText="No Record Found"
                                    OnSelectedIndexChanged="GrdViewHorseEntry_OnSelectedIndexChanged">
                                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                        HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Placing" ItemStyle-Width="8%">
                                         <ItemTemplate>
                                             <asp:HiddenField runat="server" ID="hdnfieldHorseNameID2" Value='<%# Bind("HorseNameID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldRiderNameID" Value='<%# Bind("RiderPNameID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldVerdictMarginID" Value='<%# Bind("VardictMarginID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldPlacing" Value='<%# Bind("Placing") %>' />
                                             <asp:LinkButton Text='<%# Bind("Placing") %>' ID="lnkMockRaceNo" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="HorseName" ReadOnly="true" HeaderText="Horse Name" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="ProfessionalName" ReadOnly="true" HeaderText="Rider Name" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="FinishTime" ReadOnly="true" HeaderText="Finish Time" ItemStyle-Width="2%" />
                                        <asp:BoundField DataField="VerdictMargin" ReadOnly="true" HeaderText="Verdict Margin" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="CommonComment" ReadOnly="true" HeaderText="Common Comment" ItemStyle-Width="12%" />
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Left" />
                                </asp:GridView>
                </div>

                    <br />
                    <table>
                        <tr>
                            <td>Gate Practice Distance Break Up Entry:</td>
                            <td><b>
                                <asp:Label ID="lblDistanceBreakUpEntry" runat="server"></asp:Label>
                                <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>Source Name:(*)</td>
                            <td>
                                    <asp:TextBox ID="txtbxSourceName2" Width="455px" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldSourceName2" />
                                <div id="Div4" style="height: 300px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ServiceMethod="AddSourceNameList"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxSourceName2" CompletionListElementID="Div4"
                                            OnClientItemSelected="GetSourceID4"
                                            ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" 
                                    ValidationGroup="MockRaceEntry2" runat="server" ControlToValidate="txtbxSourceName2"
                                    Text="*" ErrorMessage="Please enter Source Name."></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        <tr>
                            <td>Fix:</td>
                            <td><asp:CheckBox ID="chkbxFix" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Distance Break Up:</td>
                            <td>
                               <asp:DropDownList ID="drpdwnDistaceBreakup" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Time Taken (In Distance Break Up):</td>
                            <td>
                                <asp:TextBox ID="txtbxMM1" Width="20px" MaxLength="2" runat="server"></asp:TextBox>:
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                            ControlToValidate="txtbxMM1"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="MockRaceEntry2"
                                            runat="server">*</asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtbxSS1" Width="20px" MaxLength="2" runat="server"></asp:TextBox>:
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9"
                                            ControlToValidate="txtbxSS1"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="MockRaceEntry2"
                                            runat="server">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Distance Break Up Comment:</td>
                            <td>
                               <asp:TextBox ID="txtbxComments2" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                              <asp:AutoCompleteExtender ServiceMethod="AddCommentsList1"
                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                  CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtbxComments2" CompletionListElementID="Div6"
                                ID="AutoCompleteExtender6" runat="server" FirstRowSelected="false">
                            </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                    </table>
                    <table>
                       <tr>
                           <td>
                               <asp:Button ID="btnDistanceBreakupAdd" Text="Add" runat="server" OnClick="btnDistanceBreakupAdd_Click" ValidationGroup="MockRaceEntry2" />
                           </td>
                            <td>
                               <asp:Button ID="btnBreakUpClear" runat="server" OnClick="btnBreakUpClear_Click" Text="Clear" />
                           </td>
                           <td>
                               <asp:Button ID="btnBreakUpDelete" Text="Delete" OnClick="btnBreakUpDelete_Click" runat="server"  ValidationGroup="MockRaceEntry2"/>
                           </td>
                        
                       </tr>
                   </table>
                    <div id="Div7" style="width: 50%; overflow: auto;" runat="server">
                                <asp:GridView ID="GvDistanceBreakUp" runat="server" Width="100%" AutoGenerateColumns="false"
                                    DataKeyNames="GPDistanceBreakUpCID" EmptyDataText="No Record Found"
                                    OnSelectedIndexChanged="GvDistanceBreakUp_OnSelectedIndexChanged">
                                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                        HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Source Name" ItemStyle-Width="18%">
                                         <ItemTemplate>
                                             <asp:HiddenField runat="server" ID="hdnfieldSourcePNameID" Value='<%# Bind("SourcePNameID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldDistanceBreakUpMID" Value='<%# Bind("DistanceBreakupMID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldSourceName" Value='<%# Bind("ProfessionalName") %>' />
                                             <asp:LinkButton Text='<%# Bind("ProfessionalName") %>' ID="lnkMockRaceNo" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DistanceBreakUp" ReadOnly="true" HeaderText="Distance Break Up" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="TimeTaken" ReadOnly="true" HeaderText="Time Taken" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="DistanceBreakUpComment" ReadOnly="true" HeaderText="Distance Break Up Comments" ItemStyle-Width="22%" />
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Left" />
                                </asp:GridView>
                </div>

                    <br />
                    <table>
                        <tr>
                            <td>Gate Practice Individual Horse Comment Entry:</td>
                            <td><b>
                                <asp:Label ID="lblIndividualHorse" runat="server"></asp:Label>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>Source Name:(*)</td>
                            <td>
                                    <asp:TextBox ID="txtbxIndividualSource" Width="455px" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldSourceIndividualID" />
                                <div id="Div9" style="height: 300px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ServiceMethod="AddSourceNameList"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxIndividualSource" CompletionListElementID="Div9"
                                            OnClientItemSelected="GetSourceID3"
                                            ID="AutoCompleteExtender8" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" 
                                    ValidationGroup="MockRaceEntry4" runat="server" ControlToValidate="txtbxIndividualSource"
                                    Text="*" ErrorMessage="Please enter Source Name."></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        <tr>
                            <td>Fix:</td>
                            <td><asp:CheckBox ID="chkbxIndividualFix" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Horse Name:(*)</td>
                            <td>
                                    <asp:TextBox ID="txtbxHorseNameIndividual" Width="455px" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameIDIndividual" />
                                <div id="Div14" style="height: 300px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList1"
                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                            TargetControlID="txtbxHorseNameIndividual" CompletionListElementID="Div14"
                                            OnClientItemSelected="GetHorseID2"
                                            ID="AutoCompleteExtender11" runat="server" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" 
                                    ValidationGroup="MockRaceEntry4" runat="server" ControlToValidate="txtbxHorseNameIndividual"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        <tr>
                            <td>Individual Horse Comment:(*)</td>
                            <td>
                               <asp:TextBox ID="txtbxIndividualcomment" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            <div id="Div12" style="height:300px; overflow-y:scroll;" ></div>
                              <asp:AutoCompleteExtender ServiceMethod="AddCommentsList1"
                                MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                  CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtbxIndividualcomment" CompletionListElementID="Div12"
                                ID="AutoCompleteExtender10" runat="server" FirstRowSelected="false">
                            </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" 
                                    ValidationGroup="MockRaceEntry4" runat="server" ControlToValidate="txtbxIndividualcomment"
                                    Text="*" ErrorMessage="Please enter Individual Horse Comment."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                       <tr>
                          
                           <td>
                               <asp:Button ID="btnAddIndividual" Text="Add" runat="server" OnClick="btnAddIndividual_Click" ValidationGroup="MockRaceEntry4" />
                           </td>
                          <td>
                               <asp:Button ID="btnIndividualClear" runat="server" OnClick="btnIndividualClear_Click" Text="Clear" />
                           </td>
                           <td>
                               <asp:Button ID="btnIndividualDelete" Text="Delete" OnClick="btnIndividualDelete_Click" runat="server"  ValidationGroup="MockRaceEntry4"/>
                           </td>
                       </tr>
                   </table>
                    <div id="Div13" style="width: 50%; overflow: auto;" runat="server">
                                <asp:GridView ID="GvIndividual" runat="server" Width="100%" AutoGenerateColumns="false"
                                    DataKeyNames="GPIHCommentCID" EmptyDataText="No Record Found"
                                    OnSelectedIndexChanged="GvIndividual_OnSelectedIndexChanged">
                                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                        HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Source Name" ItemStyle-Width="18%">
                                         <ItemTemplate>
                                             <asp:HiddenField runat="server" ID="hdnfieldIndividualSourcePNameIDG" Value='<%# Bind("SourcePNameID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldIndividualHorseNameIDG" Value='<%# Bind("HorseNameID") %>' />
                                             <asp:HiddenField runat="server" ID="hdnfieldIndividualSourceName" Value='<%# Bind("ProfessionalName") %>' />
                                             <asp:LinkButton Text='<%# Bind("ProfessionalName") %>' ID="lnkMockRaceNo" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="HorseName" ReadOnly="true" HeaderText="Horse Name" ItemStyle-Width="22%" />
                                        <asp:BoundField DataField="IndividualHorseComment" ReadOnly="true" HeaderText="Individual Horse Comment" ItemStyle-Width="22%" />
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
