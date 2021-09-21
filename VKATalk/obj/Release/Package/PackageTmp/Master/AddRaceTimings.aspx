<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRaceTimings.aspx.cs" Inherits="VKATalk.Master.AddRaceTimings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Race Timing",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function GetHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldhorseid.ClientID %>').value = HdnKey;
        }
    </script>
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

        .modalBackground { 
            background-color:#333333; 
            filter:alpha(opacity=70); 
            opacity:0.7; 
        } 
        .modalPopup { 
            background-color:#FFFFFF; 
            border-width:1px; 
            border-style:solid; 
            border-color:#CCCCCC; 
            padding:1px; 
            width:300px; 
            Height:200px; 
        }  

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Race Timings</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 870px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="4">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RT" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Race Timing Type:(*)</td>
                            <td style="width: 200px">
                                <asp:DropDownList ID="drpdwnRaceTimings" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator3" Display="Dynamic"
                                    ValidationGroup="RT" runat="server" ControlToValidate="drpdwnRaceTimings"
                                    Text="*" ErrorMessage="Please select Race Timing Types"></asp:RequiredFieldValidator>
                            </td>
                            <td>Center:(*)</td>
                            <td>
                                <asp:DropDownList ID="DrpdwnCenter" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic"
                                    ValidationGroup="RT" runat="server" ControlToValidate="DrpdwnCenter"
                                    Text="*" ErrorMessage="Please select Center."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>From Year:(**)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                            </td>
                            <td>Till Year:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td>From Season:(**)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                            </td>
                            <td>Till Season:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Track:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTrack" runat="server"></asp:DropDownList>
                              
                                <asp:RequiredFieldValidator ID="rqv" runat="server" InitialValue="-1" ErrorMessage="Please select Track." ValidationGroup="RT" ControlToValidate="drpdwnTrack">*</asp:RequiredFieldValidator>
                            </td>
                            <td>Distance:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnDistance" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator4" Display="Dynamic"
                                    ValidationGroup="RT" runat="server" ControlToValidate="drpdwnDistance"
                                    Text="*" ErrorMessage="Please select Distance."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Race Type:</td>
                            <td style="width: 150px;">
                                <asp:RadioButtonList ID="rdbtnRaceType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Handicap" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Terms" Value="2"></asp:ListItem>
                                </asp:RadioButtonList></td>

                            <%--<td>Class:(*)</td>--%>
                            <td>Class Group:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnClass" runat="server"></asp:DropDownList>
                             
                            </td>
                        </tr>
                        <tr>
                            <td>Race Status:(**)</td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnRaceStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Normal" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Big" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Classic" Value="3"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>Race Date:(**)</td>
                            <td>
                                <asp:TextBox ID="txtbxRaceDate" runat="server" ValidationGroup="RT" Width="90px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxRaceDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxRaceDate"
                                    EmptyValueMessage="Please enter date."
                                    InvalidValueMessage="Invalid date format."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                                    ValidationGroup="RT" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Horse Name:(**)</td>
                            <td colspan="3">
                                <%--<asp:DropDownList runat="server" ID="drpdwnHorseName" Width="520px" />--%>
                                 <asp:TextBox runat="server" ID="txtbxHorseName" Width="700px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldhorseid" />
                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxHorseName" CompletionListElementID="listPlacement"
                                        OnClientItemSelected="GetHorseID"
                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </td>
                           
                        </tr>
                        <tr>
                            
                            <td>Carried Weight in Kg:(**)</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtbxCarriedWeight"></asp:TextBox></td>
                             <td>Penetro Meter Reading:(**)</td>
                            <td>
                                <asp:TextBox ID="txtbxPReading" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                           
                            <td>False Rails:(**)</td>
                            <td>
                                <asp:RadioButtonList ID="rdbtnFalseRails" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>Timing (Min : Sec : Pulse):(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxMin" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbxSec" runat="server" Width="20px" MaxLength="2"></asp:TextBox>:
                                <asp:TextBox ID="txtbxPulse" runat="server" Width="25px" MaxLength="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" ErrorMessage="Please enter Minute." ValidationGroup="RT" ControlToValidate="txtbxMin"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="*" ErrorMessage="Please enter Second." ValidationGroup="RT" ControlToValidate="txtbxSec"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Text="*" ErrorMessage="Please enter Pulse." ValidationGroup="RT" ControlToValidate="txtbxPulse">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="RT" /></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="RT" /></td>
                            <td>
                                <asp:Button ID="btnImport" runat="server" Text="Import" />
                                <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                    CancelControlID="Button2" BackgroundCssClass="Background">
                                </asp:ModalPopupExtender>
                            </td>

                            <td>
                                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
                            <td>
                                <asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" /></td>
                            <td>
                                <input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>

                        </tr>
                    </table>
                </fieldset>
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

    <div id="dvgridview" style="height: 300px; width: 100%; overflow: auto;" runat="server">
        <asp:GridView ID="grdviewRaceTiming" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="RaceTimingsID" EmptyDataText="No Race timing record Found" 
            OnSelectedIndexChanged="GridView_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="5%" />
                <asp:TemplateField HeaderText="Race Timing Type" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("RaceTimingType") %>' />
                        <asp:LinkButton Text='<%# Bind("RaceTimingType") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CenterName" HeaderText="Center" ItemStyle-Width="5%" />
                <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="5%" />
                <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="5%" />
                <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="5%" />
                <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="5%" />
                <asp:BoundField DataField="TrackName" HeaderText="Track" ItemStyle-Width="5%" />
                <asp:BoundField DataField="Distance" HeaderText="Distance" ItemStyle-Width="5%" />
                <asp:BoundField DataField="RaceType" HeaderText="Race Type" ItemStyle-Width="5%" />
                <asp:BoundField DataField="ClassGroup" HeaderText="Class Group" ItemStyle-Width="5%" />
                <asp:BoundField DataField="RaceStatus" HeaderText="Race Status" ItemStyle-Width="5%" />
                <asp:BoundField DataField="RaceDate" HeaderText="Race Date" ItemStyle-Width="5%" />
                <asp:BoundField DataField="HorseName" HeaderText="Horse Name" ItemStyle-Width="5%" />
                <asp:BoundField DataField="CarriedWeight" HeaderText="Carried Weight" ItemStyle-Width="5%" />
                <asp:BoundField DataField="PenetrometerReading" HeaderText="Penetrometer Reading" ItemStyle-Width="5%" />
                <asp:BoundField DataField="FalseRails" HeaderText="False Rails" ItemStyle-Width="5%" />
                <asp:BoundField DataField="Timing" HeaderText="Timing" ItemStyle-Width="5%" />
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
