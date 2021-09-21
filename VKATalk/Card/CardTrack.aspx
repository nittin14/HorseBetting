<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardTrack.aspx.cs" Inherits="VKATalk.Card.CardTrack" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <style type="text/css">
        .container {
            margin: 0px auto !important;
        }

        #main_container {
            width: 1700px;
            height: 800px;
            margin: 0px auto;
            background: none;
            font-size: 12px;
            color: #333333;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }

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
                    title: "Card Track",
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
        function GetSourceID1(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldsourcenamedisplay.ClientID %>').value = HdnKey;
        }

        function GetHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID.ClientID %>').value = HdnKey;
        }
        function GetHorseID1(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID1.ClientID %>').value = HdnKey;
        }
        function GetHorseID2(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID2.ClientID %>').value = HdnKey;
        }
        function GetHorseID3(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID3.ClientID %>').value = HdnKey;
        }
        function GetHorseID4(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID4.ClientID %>').value = HdnKey;
        }
        function GetHorseID5(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID5.ClientID %>').value = HdnKey;
        }
        function GetHorseID6(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID6.ClientID %>').value = HdnKey;
        }

        function GetHorseID7(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID7.ClientID %>').value = HdnKey;
        }
        function GetHorseID8(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID8.ClientID %>').value = HdnKey;
        }
        function GetHorseID9(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID9.ClientID %>').value = HdnKey;
        }
        function GetHorseID10(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID10.ClientID %>').value = HdnKey;
        }
        function GetHorseID11(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID11.ClientID %>').value = HdnKey;
        }
        function GetHorseID12(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID12.ClientID %>').value = HdnKey;
        }
        function GetHorseID13(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID13.ClientID %>').value = HdnKey;
        }



        function GetRiderID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID.ClientID %>').value = HdnKey;
        }

        function GetRiderID1(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID1.ClientID %>').value = HdnKey;
        }
        function GetRiderID2(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID2.ClientID %>').value = HdnKey;
        }
        function GetRiderID3(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID3.ClientID %>').value = HdnKey;
        }
        function GetRiderID4(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID4.ClientID %>').value = HdnKey;
        }
        function GetRiderID5(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID5.ClientID %>').value = HdnKey;
        }
        function GetRiderID6(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID6.ClientID %>').value = HdnKey;
        }

        function GetRiderID7(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID7.ClientID %>').value = HdnKey;
        }
        function GetRiderID8(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID8.ClientID %>').value = HdnKey;
        }
        function GetRiderID9(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID9.ClientID %>').value = HdnKey;
        }
        function GetRiderID10(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID10.ClientID %>').value = HdnKey;
        }
        function GetRiderID11(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID11.ClientID %>').value = HdnKey;
        }
        function GetRiderID12(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRiderNameID12.ClientID %>').value = HdnKey;
        }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Track</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 92%;margin: 0px;">
        <tr>
            <td>
                <fieldset style="width: 95%;">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardTrack" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%;">Data Entry Date:(*)</td>
                            <td style="width: 40%;">
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
                                    ValidationGroup="CardTrack" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Source Name:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxSourceName" Width="255px" runat="server"></asp:TextBox>
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
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxSourceName"
                                    Text="*" ErrorMessage="Please enter Source Name."></asp:RequiredFieldValidator>
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
                            <td>Fix:</td>
                            <td>
                                <asp:CheckBox ID="chkbxFix" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Track Date:(*)</td>
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
                                    ValidationGroup="CardTrack" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>Distance:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnDistance" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" InitialValue="-1"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="drpdwnDistance"
                                    Text="*" ErrorMessage="Please select distance."></asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>Track:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTrack" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" InitialValue="-1"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="drpdwnTrack"
                                    Text="*" ErrorMessage="Please select track."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Workout Type:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnWorkouttype" runat="server">
                                    <asp:ListItem Value="-1" Text="-- Please select --"></asp:ListItem>
                                    <asp:ListItem Value="TR" Text="TR"></asp:ListItem>
                                    <asp:ListItem Value="GP" Text="GP"></asp:ListItem>
                                    <asp:ListItem Value="MR" Text="MR"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" InitialValue="-1"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="drpdwnWorkouttype"
                                    Text="*" ErrorMessage="Please select Workout Type."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Fix:</td>
                            <td>
                                <asp:CheckBox ID="chkbxFix2" runat="server" /></td>
                        </tr>
                    </table>
                    <div style="overflow-y:scroll;max-height:260px;">
                    <table border="1">
                        <tr>
                            <td>Row No.</td>
                            <td>Horse Name</td>
                            <td>Rider Name</td>
                            <td>DR</td>
                            <td>CW</td>
                            <td>Distance Breakup</td>
                            <td>Time Taken</td>

                            <td>DBC</td>

                            <td>Verdict Margin</td>
                            <td>Comman Comment</td>
                            <td>Individual Horse Comment</td>

                            <td>IH CC</td>
                            <td>Workout Quality</td>
                            <td>WR</td>
                            <td>WIM</td>
                            <td>IsS</td>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName" Width="155px" runat="server"></asp:TextBox>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxHorseName"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName" Width="155px" runat="server"></asp:TextBox>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxRiderName"
                                    Text="*" ErrorMessage="Please enter Rider Name."></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxDraw1" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW1" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup" runat="server"></asp:DropDownList>
                            </td>
                            <td style="width: 70px;">
                                <div style="width: 50px;">
                                    <asp:TextBox ID="txtbxMM1" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                    <asp:TextBox ID="txtbxSS1" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                    ControlToValidate="txtbxMM1"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9"
                                        ControlToValidate="txtbxSS1"
                                        ValidationExpression="\d+"
                                        ErrorMessage="Please enter numbers only"
                                        ValidationGroup="CardTrack"
                                        runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                </div>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC1" runat="server" Checked="false" />
                            </td>
                            <td style="width:70px;">
                                <asp:DropDownList ID="drpdwnVerdictMargin" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment" runat="server"></asp:TextBox>
                                <div id="Div3" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment" CompletionListElementID="Div3"
                                    ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment" runat="server"></asp:TextBox>
                                <div id="Div4" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment" CompletionListElementID="Div4"
                                    ID="AutoCompleteExtender10" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>

                            <td>
                                <asp:CheckBox ID="chkbxIHCC1" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality1" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR1" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM1" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow1" runat="server" Checked="true" />
                            </td>
                        </tr>



                        <tr>
                            <td>2</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName1" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID1" />
                                <div id="Div7" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName1" CompletionListElementID="Div7"
                                        OnClientItemSelected="GetHorseID1"
                                        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxHorseName1"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName1" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID1" />
                                <div id="Div6" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName1" CompletionListElementID="Div6"
                                        OnClientItemSelected="GetRiderID1"
                                        ID="AutoCompleteExtender6" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxRiderName1"
                                    Text="*" ErrorMessage="Please enter Rider Name."></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxDraw2" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW2" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup1" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM2" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
               
                 <asp:TextBox ID="txtbxSS2" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    ControlToValidate="txtbxMM2"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                    ControlToValidate="txtbxSS2"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC2" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin1" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment1" runat="server"></asp:TextBox>
                                <div id="Div5" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment1" CompletionListElementID="Div5"
                                    ID="AutoCompleteExtender7" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment1" runat="server"></asp:TextBox>
                                <div id="Div12" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment1" CompletionListElementID="Div12"
                                    ID="AutoCompleteExtender9" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>

                             <td>
                                <asp:CheckBox ID="chkbxIHCC2" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality2" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR2" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM2" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow2" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName2" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID2" />
                                <div id="Div8" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName2" CompletionListElementID="Div8"
                                        OnClientItemSelected="GetHorseID2"
                                        ID="AutoCompleteExtender8" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxHorseName2"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName2" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID2" />
                                <div id="Div9" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName2" CompletionListElementID="Div9"
                                        OnClientItemSelected="GetRiderID2"
                                        ID="AutoCompleteExtender11" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxDraw3" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW3" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup2" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM3" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS3" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11"
                                    ControlToValidate="txtbxMM3"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12"
                                    ControlToValidate="txtbxSS3"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxDBC3" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin2" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment2" runat="server"></asp:TextBox>
                                <div id="Div11" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment2" CompletionListElementID="Div11"
                                    ID="AutoCompleteExtender12" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment2" runat="server"></asp:TextBox>
                                <div id="Div13" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment2" CompletionListElementID="Div13"
                                    ID="AutoCompleteExtender13" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            
                             <td>
                                <asp:CheckBox ID="chkbxIHCC3" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality3" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR3" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM3" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow3" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName3" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID3" />
                                <div id="Div15" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName3" CompletionListElementID="Div15"
                                        OnClientItemSelected="GetHorseID3"
                                        ID="AutoCompleteExtender14" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxHorseName3"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName3" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID3" />
                                <div id="Div14" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName3" CompletionListElementID="Div14"
                                        OnClientItemSelected="GetRiderID3"
                                        ID="AutoCompleteExtender15" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxRiderName3"
                                    Text="*" ErrorMessage="Please enter Rider Name."></asp:RequiredFieldValidator>--%>
                            </td>
                             <td>
                                <asp:TextBox ID="txtbxDraw4" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW4" Width="25px" runat="server"></asp:TextBox>
                            </td> 
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup3" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM4" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS4" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14"
                                    ControlToValidate="txtbxMM4"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15"
                                    ControlToValidate="txtbxSS4"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxDBC4" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin3" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment3" runat="server"></asp:TextBox>
                                <div id="Div16" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment3" CompletionListElementID="Div16"
                                    ID="AutoCompleteExtender16" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment3" runat="server"></asp:TextBox>
                                <div id="Div17" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment3" CompletionListElementID="Div17"
                                    ID="AutoCompleteExtender17" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                              <td>
                                <asp:CheckBox ID="chkbxIHCC4" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality4" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR4" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM4" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow4" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>5</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName4" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID4" />
                                <div id="Div18" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName4" CompletionListElementID="Div18"
                                        OnClientItemSelected="GetHorseID4"
                                        ID="AutoCompleteExtender18" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxHorseName4"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName4" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID4" />
                                <div id="Div19" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName4" CompletionListElementID="Div19"
                                        OnClientItemSelected="GetRiderID4"
                                        ID="AutoCompleteExtender19" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxRiderName4"
                                    Text="*" ErrorMessage="Please enter Rider Name."></asp:RequiredFieldValidator>--%>
                            </td>
                             <td>
                                <asp:TextBox ID="txtbxDraw5" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW5" Width="25px" runat="server"></asp:TextBox>
                            </td> 
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup4" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM5" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS5" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18"
                                    ControlToValidate="txtbxMM5"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator19"
                                    ControlToValidate="txtbxSS5"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                               <td>
                                <asp:CheckBox ID="chkbxDBC5" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin4" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment4" runat="server"></asp:TextBox>
                                <div id="Div20" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment4" CompletionListElementID="Div20"
                                    ID="AutoCompleteExtender20" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment4" runat="server"></asp:TextBox>
                                <div id="Div21" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment4" CompletionListElementID="Div21"
                                    ID="AutoCompleteExtender21" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIHCC5" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality5" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR5" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM5" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow5" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>6</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName5" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID5" />
                                <div id="Div22" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName5" CompletionListElementID="Div22"
                                        OnClientItemSelected="GetHorseID5"
                                        ID="AutoCompleteExtender22" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxHorseName5"
                                    Text="*" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName5" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID5" />
                                <div id="Div23" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName5" CompletionListElementID="Div23"
                                        OnClientItemSelected="GetRiderID5"
                                        ID="AutoCompleteExtender23" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator181" Display="Dynamic"
                                    ValidationGroup="CardTrack" runat="server" ControlToValidate="txtbxRiderName5"
                                    Text="*" ErrorMessage="Please enter Rider Name."></asp:RequiredFieldValidator>--%>
                            </td>
                             <td>
                                <asp:TextBox ID="txtbxDraw6" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW6" Width="25px" runat="server"></asp:TextBox>
                            </td> 
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup5" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM6" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS6" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator21"
                                    ControlToValidate="txtbxMM6"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator22"
                                    ControlToValidate="txtbxSS6"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>

                            <td>
                                <asp:CheckBox ID="chkbxDBC6" runat="server" Checked="false" />
                            </td>

                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin5" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment5" runat="server"></asp:TextBox>
                                <div id="Div24" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment5" CompletionListElementID="Div24"
                                    ID="AutoCompleteExtender24" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment5" runat="server"></asp:TextBox>
                                <div id="Div25" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment3" CompletionListElementID="Div25"
                                    ID="AutoCompleteExtender25" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            
                            <td>
                                <asp:CheckBox ID="chkbxIHCC6" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality6" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR6" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM6" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow6" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>7</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName6" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID6" />
                                <div id="Div26" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName6" CompletionListElementID="Div26"
                                        OnClientItemSelected="GetHorseID6"
                                        ID="AutoCompleteExtender26" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName6" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID6" />
                                <div id="Div27" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName6" CompletionListElementID="Div27"
                                        OnClientItemSelected="GetRiderID6"
                                        ID="AutoCompleteExtender27" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                              <td>
                                <asp:TextBox ID="txtbxDraw7" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW7" Width="25px" runat="server"></asp:TextBox>
                            </td>  
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup6" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM7" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS7" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator25"
                                    ControlToValidate="txtbxMM7"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator26"
                                    ControlToValidate="txtbxSS7"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC7" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin6" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment6" runat="server"></asp:TextBox>
                                <div id="Div28" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment6" CompletionListElementID="Div28"
                                    ID="AutoCompleteExtender28" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment6" runat="server"></asp:TextBox>
                                <div id="Div29" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment6" CompletionListElementID="Div29"
                                    ID="AutoCompleteExtender29" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            
                            <td>
                                <asp:CheckBox ID="chkbxIHCC7" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality7" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR7" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM7" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow7" runat="server" Checked="true" />
                            </td>
                        </tr>

                        <tr>
                            <td>8</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName7" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID7" />
                                <div id="Div30" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName7" CompletionListElementID="Div30"
                                        OnClientItemSelected="GetHorseID7"
                                        ID="AutoCompleteExtender30" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName7" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID7" />
                                <div id="Div31" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName7" CompletionListElementID="Div31"
                                        OnClientItemSelected="GetRiderID7"
                                        ID="AutoCompleteExtender31" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                               <td>
                                <asp:TextBox ID="txtbxDraw8" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW8" Width="25px" runat="server"></asp:TextBox>
                            </td>   
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup7" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM8" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS8" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                    ControlToValidate="txtbxMM8"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                    ControlToValidate="txtbxSS8"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC8" runat="server" Checked="false" />
                            </td>
							
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin7" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment7" runat="server"></asp:TextBox>
                                <div id="Div32" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment7" CompletionListElementID="Div32"
                                    ID="AutoCompleteExtender32" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment7" runat="server"></asp:TextBox>
                                <div id="Div33" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment7" CompletionListElementID="Div33"
                                    ID="AutoCompleteExtender33" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxIHCC8" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality8" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR8" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM8" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow8" runat="server" Checked="true" />
                            </td>
                        </tr>

                        <tr>
                            <td>9</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName8" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID8" />
                                <div id="Div34" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName8" CompletionListElementID="Div34"
                                        OnClientItemSelected="GetHorseID8"
                                        ID="AutoCompleteExtender34" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName8" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID8" />
                                <div id="Div42" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName8" CompletionListElementID="Div42"
                                        OnClientItemSelected="GetRiderID8"
                                        ID="AutoCompleteExtender35" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                                <td>
                                <asp:TextBox ID="txtbxDraw9" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW9" Width="25px" runat="server"></asp:TextBox>
                            </td>    
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup8" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM81" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS81" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                    ControlToValidate="txtbxMM81"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                    ControlToValidate="txtbxSS81"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC9" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin8" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment8" runat="server"></asp:TextBox>
                                <div id="Div35" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment8" CompletionListElementID="Div35"
                                    ID="AutoCompleteExtender36" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment8" runat="server"></asp:TextBox>
                                <div id="Div36" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment8" CompletionListElementID="Div36"
                                    ID="AutoCompleteExtender37" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxIHCC9" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality9" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR9" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM9" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow9" runat="server" Checked="true" />
                            </td>
                        </tr>
                     

                        <tr>
                            <td>10</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName9" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID9" />
                                <div id="Div37" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName9" CompletionListElementID="Div37"
                                        OnClientItemSelected="GetHorseID9"
                                        ID="AutoCompleteExtender38" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName9" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID9" />
                                <div id="Div38" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName9" CompletionListElementID="Div38"
                                        OnClientItemSelected="GetRiderID9"
                                        ID="AutoCompleteExtender39" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                                 <td>
                                <asp:TextBox ID="txtbxDraw10" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW10" Width="25px" runat="server"></asp:TextBox>
                            </td>     
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup9" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM9" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS9" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                    ControlToValidate="txtbxMM9"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10"
                                    ControlToValidate="txtbxSS9"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC10" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin9" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment9" runat="server"></asp:TextBox>
                                <div id="Div39" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment9" CompletionListElementID="Div39"
                                    ID="AutoCompleteExtender40" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment9" runat="server"></asp:TextBox>
                                <div id="Div40" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment9" CompletionListElementID="Div40"
                                    ID="AutoCompleteExtender41" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxIHCC10" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality10" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR10" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM10" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow10" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>11</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName10" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID10" />
                                <div id="Div41" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName10" CompletionListElementID="Div41"
                                        OnClientItemSelected="GetHorseID10"
                                        ID="AutoCompleteExtender42" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName10" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID10" />
                                <div id="Div43" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName10" CompletionListElementID="Div43"
                                        OnClientItemSelected="GetRiderID10"
                                        ID="AutoCompleteExtender43" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                             
                                     <td>
                                <asp:TextBox ID="txtbxDraw11" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW11" Width="25px" runat="server"></asp:TextBox>
                            </td>     
                                
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup10" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM10" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS10" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13"
                                    ControlToValidate="txtbxMM10"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16"
                                    ControlToValidate="txtbxSS10"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC11" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin10" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment10" runat="server"></asp:TextBox>
                                <div id="Div44" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment10" CompletionListElementID="Div44"
                                    ID="AutoCompleteExtender44" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment10" runat="server"></asp:TextBox>
                                <div id="Div45" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment10" CompletionListElementID="Div45"
                                    ID="AutoCompleteExtender45" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxIHCC11" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality11" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR11" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM11" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow11" runat="server" Checked="true" />
                            </td>
                        </tr>
                                               
                        <tr>
                            <td>12</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName11" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID11" />
                                <div id="Div46" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName11" CompletionListElementID="Div46"
                                        OnClientItemSelected="GetHorseID11"
                                        ID="AutoCompleteExtender46" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName11" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID11" />
                                <div id="Div47" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName11" CompletionListElementID="Div47"
                                        OnClientItemSelected="GetRiderID11"
                                        ID="AutoCompleteExtender47" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                              
                                     <td>
                                <asp:TextBox ID="txtbxDraw12" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW12" Width="25px" runat="server"></asp:TextBox>
                            </td>     
                                 
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup11" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM11" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS11" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17"
                                    ControlToValidate="txtbxMM11"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator20"
                                    ControlToValidate="txtbxSS11"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC12" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin11" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment11" runat="server"></asp:TextBox>
                                <div id="Div48" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment11" CompletionListElementID="Div48"
                                    ID="AutoCompleteExtender48" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment11" runat="server"></asp:TextBox>
                                <div id="Div49" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment11" CompletionListElementID="Div49"
                                    ID="AutoCompleteExtender49" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxIHCC12" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality12" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR12" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM12" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow12" runat="server" Checked="true" />
                            </td>
                        </tr>


                        <tr>
                            <td>13</td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName12" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID12" />
                                <div id="Div50" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName12" CompletionListElementID="Div50"
                                        OnClientItemSelected="GetHorseID12"
                                        ID="AutoCompleteExtender50" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRiderName12" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldRiderNameID12" />
                                <div id="Div51" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddRiderList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxRiderName12" CompletionListElementID="Div51"
                                        OnClientItemSelected="GetRiderID12"
                                        ID="AutoCompleteExtender51" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                               
                                     <td>
                                <asp:TextBox ID="txtbxDraw13" Width="25px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCW13" Width="25px" runat="server"></asp:TextBox>
                            </td>     
                                  
                            <td>
                                <asp:DropDownList ID="drpdwnDistaceBreakup12" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxMM12" Width="15px" MaxLength="2" runat="server"></asp:TextBox>:
                                
                                <asp:TextBox ID="txtbxSS12" Width="15px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator23"
                                    ControlToValidate="txtbxMM12"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator24"
                                    ControlToValidate="txtbxSS12"
                                    ValidationExpression="\d+"
                                    ErrorMessage="Please enter numbers only"
                                    ValidationGroup="CardTrack"
                                    runat="server"><span style="color:red">*</span></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxDBC13" runat="server" Checked="false" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnVerdictMargin12" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxComment12" runat="server"></asp:TextBox>
                                <div id="Div52" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddCommonComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxComment12" CompletionListElementID="Div52"
                                    ID="AutoCompleteExtender52" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIndividualcomment12" runat="server"></asp:TextBox>
                                <div id="Div53" style="height: 300px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddIndividualComment"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxIndividualcomment12" CompletionListElementID="Div53"
                                    ID="AutoCompleteExtender53" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxIHCC13" runat="server" Checked="false" />
                            </td>
                               <td>
                                <asp:DropDownList ID="drpdwnWorkoutQuality13" style="width:150px;" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdwnWR13" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:DropDownList ID="drpdwnWIM13" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:CheckBox ID="chkbxIsShow13" runat="server" Checked="true" />
                            </td>
                        </tr>


                    </table>
                        </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnMockRaceAdd" Text="Add" runat="server" OnClick="btnAdd_Click" ValidationGroup="CardTrack" />
                            </td>
                            <td>
                                <asp:Button ID="btnShow" Text="Show" runat="server" OnClick="btnShow_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnMockRaceEntryClear" Text="Clear" OnClick="btnMockRaceEntryClear_Click" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnMockRaceEntryDelete" Text="Delete" OnClick="btnMockRaceEntryDelete_Click" runat="server" />
                            </td>

                            <td>
                                <asp:Button ID="btnImport" runat="server" Text="Import" Enabled="false" />
                                <%--<asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                    CancelControlID="Button2" BackgroundCssClass="Background">
                                </asp:ModalPopupExtender>--%>
                            </td>
                            <td>
                            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" Enabled="false" /></td>
                        <td><asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>

                        </tr>
                    </table>
                    
                </fieldset>
                 <table>
                        <tr>
                            <td>Data Entry Date:</td>
                            <td>
                                <asp:TextBox ID="txtbxentrydatedisplay" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" CultureName="en-GB" runat="server" TargetControlID="txtbxentrydatedisplay"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" runat="server" TargetControlID="txtbxentrydatedisplay"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                          <td>Track Date:</td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtbxTrackDatedisplay" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender3" CultureName="en-GB" runat="server" TargetControlID="txtbxTrackDatedisplay"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4" runat="server" TargetControlID="txtbxTrackDatedisplay"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td>Horse Name: </td>
                            <td>
                                <asp:TextBox ID="txtbxHorseDisplay" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID13" />
                                <div id="Dvhorsedisplay" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseDisplay" CompletionListElementID="Dvhorsedisplay"
                                        OnClientItemSelected="GetHorseID13"
                                        ID="AutoCompleteExtender54" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                
                            </td>
                            <td>Source Name:</td>
                            <td>
                                <asp:TextBox ID="txtbxSourceNameDisplay" Width="155px" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldsourcenamedisplay" />
                                <div id="dvsourcenamedisplay" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddSourceNameList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxSourceNameDisplay" CompletionListElementID="dvsourcenamedisplay"
                                        OnClientItemSelected="GetSourceID1"
                                        ID="AutoCompleteExtender55" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                            <%--<td>Division Race Date:</td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtbxDivisionDisplay" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton5" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" CultureName="en-GB" runat="server" TargetControlID="txtbxDivisionDisplay"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton5" runat="server" TargetControlID="txtbxDivisionDisplay"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>--%>
                            <td>Center:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterDisplay">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnShowDisplay" Text="Show" runat="server" OnClick="btnShowDisplay_Click" />
                            </td>
                        </tr>
                    </table>
                <div id="DvMockRaceEntry" style="width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="GvMockRace" runat="server" Width="97%" AutoGenerateColumns="false"
                            DataKeyNames="TrackCID" EmptyDataText="No Record Found"
                            OnSelectedIndexChanged="GvMockRace_OnSelectedIndexChanged">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Bunch ID" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameidG" Value='<%# Bind("SourcePNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameG" Value='<%# Bind("ProfessionalName") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameIDG" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldRiderPNameIDG" Value='<%# Bind("RiderPNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldTrackBunchID" Value='<%# Bind("TrackBunchCID") %>' />
                                        <asp:LinkButton Text='<%# Bind("TrackBunchCID") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProfessionalName" ReadOnly="true" HeaderText="Source Name" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="CenterName" ReadOnly="true" HeaderText="Center" ItemStyle-Width="2%" />  <%--2--%>
                                <asp:BoundField DataField="TrackDate" ReadOnly="true" HeaderText="Track Date" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="TrackAlias" ReadOnly="true" HeaderText="Track" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="WorkoutType" ReadOnly="true" HeaderText="Workout Type" ItemStyle-Width="5%" /> <%--5--%>
                                <asp:BoundField DataField="HorseName" ReadOnly="true" HeaderText="Horse Name" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="RiderName" ReadOnly="true" HeaderText="Rider Name" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DrawNo" ReadOnly="true" HeaderText="DR" ItemStyle-Width="2%" />   <%--8--%>
                                <asp:BoundField DataField="MRCarriedWeight" ReadOnly="true" HeaderText="CW" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="DistanceBreakUp" ReadOnly="true" HeaderText="Distance BreakUp" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="TimeTaken" ReadOnly="true" HeaderText="Time Taken" ItemStyle-Width="2%" /><%--9--%>
                                <asp:BoundField DataField="DBCircle" ReadOnly="true" HeaderText="DBC" ItemStyle-Width="2%" /><%--9--%>

                                <asp:BoundField DataField="VerdictMargin" ReadOnly="true" HeaderText="Verdict Margin" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="CommonComment" ReadOnly="true" HeaderText="Common Comment" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="IndividualHorseComment" ReadOnly="true" HeaderText="Individual Horse Comment" ItemStyle-Width="5%" />

                                <asp:BoundField DataField="IHCCircle" ReadOnly="true" HeaderText="IHCC" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="WorkQuality" ReadOnly="true" HeaderText="Work Quality" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="Rating" ReadOnly="true" HeaderText="WR" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="Mark" ReadOnly="true" HeaderText="WIM" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="IsShow" ReadOnly="true" HeaderText="IsS" ItemStyle-Width="2%" />
                                
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
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
