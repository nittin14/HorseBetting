<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceDayReport.aspx.cs" Inherits="VKATalk.Card.RaceDayReport" %>

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
                    title: "Card Race Day Report",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

         function GetIncident(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           document.getElementById('<%=hdnfieldIncidentID.ClientID %>').value = HdnKey;
       }

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
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender3", "hdnfieldHorseNameID");
                $get(hiddenfieldID).value = eventArgs.get_value();

            }
        }

        function SetContextKey() {
            $find('<%=AutoCompleteExtender3.ClientID%>').set_contextKey($get("<%=hdnfieldDivisionRaceMID.ClientID %>").value);
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Ray Day Report</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RaceDayReport" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
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
                                    ValidationGroup="RaceDayReport" />
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
                                    ValidationGroup="RaceDayReport" />
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

                   
                     <div id="divEntry" style="width: 100%; overflow: auto;" runat="server" visible="False">
                     <table border="1">
                        <tr>
                            <td>Division Race Name:
                            </td>
                            <td style="width:150px;">
                                <asp:Label ID="lblGeneralRaceName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnRaceDayIncidentUpdate" runat="server" Text="Race Day Incident Update" OnClientClick="OpenHorsePopup('IncidentUpdate')" />
                            </td>
                            <td>
                                <asp:Button ID="btnPenaltyUpdate" runat="server" Text="Penlty Updatd" OnClientClick="OpenHorsePopup('PenaltyUpdate')" />
                            </td>

                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                Race Day Reporter:(*)
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxCommentator" runat="server" Width="1000"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnfieldCommentator" />
                                                       <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddCommentatorList"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxCommentator" CompletionListElementID="Div1"
                                                                OnClientItemSelected="GetCommentator"
                                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                            </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator2" Display="Dynamic" 
                            ValidationGroup="RaceDayReport" runat="server" ControlToValidate="txtbxCommentator"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Race Day Reporter."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fix:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxFix" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Report:(*)
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxReport" runat="server" Width="1000" Height="100" TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:HiddenField runat="server" ID="hdnfieldReport" />--%>
                                                       <div id="Div2" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddReportList"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxReport" CompletionListElementID="Div2"
                                                                ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                            </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="RaceDayReport" runat="server" ControlToValidate="txtbxReport"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Race Day Reporter."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fix:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxAferReport" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Highlight:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkbxHighlight" runat="server" />
                            </td>
                        </tr>
                         <tr>
                            <td>
                                Horse Name:(*)
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxHorseName" runat="server" Width="1000" onkeyup="SetContextKey()"></asp:TextBox>
                                                 <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" />
                                                       <div id="Div3" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxHorseName" CompletionListElementID="Div3"
                                                                OnClientItemSelected="GetHorseList"
                                                                ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                            </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator3" Display="Dynamic" 
                            ValidationGroup="RaceDayReport" runat="server" ControlToValidate="txtbxHorseName"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Horse Name."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Incident:
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxIncident" runat="server" Width="1000"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnfieldIncidentID" />
                                                 <div id="divincident" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddIncidentList"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxIncident" CompletionListElementID="divincident"
                                                                OnClientItemSelected="GetIncident"
                                                                ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                                                </asp:AutoCompleteExtender>
                                
                                <%--<asp:TextBox ID="TextBox1" runat="server" Width="1000"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                                       <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddCommentatorList"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxCommentator" CompletionListElementID="Div1"
                                                                OnClientItemSelected="GetCommentator"
                                                                ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                                            </asp:AutoCompleteExtender>--%>
                            </td>
                        </tr>
                    </table>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>

    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="RaceDayReport" /></td>
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
                <asp:Button ID="btnImport" runat="server" Text="Import" Enabled="false" />
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
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GvShowALL_OnSelectedIndexChanged"> 
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField HeaderText="Race Day Reporter(*)" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldProfessionalName" Value='<%# Bind("ProfessionalName") %>'/>
                                <asp:LinkButton Text='<%# Bind("ProfessionalName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Report" ReadOnly="true" HeaderText="Report(*)" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="Highlight" ReadOnly="true" HeaderText="Highlight" ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="Horse Name(*)" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:label ID="lblhorsename" runat="server" Text='<%# Bind("HorseName") %>'></asp:label>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseName" Value='<%# Bind("HorseName") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfielHorseNameID" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameID" Value='<%# Bind("RaceDayReporterNamePID") %>' />
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
