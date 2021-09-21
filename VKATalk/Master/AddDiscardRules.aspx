<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDiscardRules.aspx.cs" Inherits="VKATalk.Master.AddDiscardRules" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
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
                    title: "Discard Rules",
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
    <asp:ScriptManager ID="ScripManager2" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Discard Rules</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 900px;" class="Userlogin">
                    <table align="center">
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="DiscardRules" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnCenterName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvd" runat="server" InitialValue="-1" ErrorMessage="Please select Center" ValidationGroup="DiscardRules" ControlToValidate="drpdwnCenterName">*</asp:RequiredFieldValidator>

                            </td>
                            <td>From Year:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqv" InitialValue="-1" runat="server" ErrorMessage="Please select From Year" ValidationGroup="DiscardRules" ControlToValidate="drpdwnFromYear">*</asp:RequiredFieldValidator>

                            </td>
                            <td>Till Year:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>

                            <td>From Season:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="fseason" InitialValue="-1" runat="server" ErrorMessage="Please select FromSeason" ValidationGroup="DiscardRules" ControlToValidate="drpdwnFromSeason">*</asp:RequiredFieldValidator>

                            </td>
                            <td>Till Season:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="tSeason" runat="server" ErrorMessage="Please select TillSeason" ValidationGroup="DiscardRules" ControlToValidate="drpdwnTillSeason">*</asp:RequiredFieldValidator>

                            </td>
                            <td>RuleApplyHorseAge:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxRuleApplyHorseAge" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rrrrr" runat="server" ErrorMessage="Please enter RuleApplyHorseAge" ValidationGroup="DiscardRules" ControlToValidate="txtbxRuleApplyHorseAge">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>MaxHandicapRating:</td>
                            <td>
                                <asp:TextBox ID="txtbxMaxHandicapRating" runat="server"></asp:TextBox>
                            </td>
                            <td>MinHandicapRating:</td>
                            <td>
                                <asp:TextBox ID="txtbxMinHandicapRating" runat="server"></asp:TextBox>
                            </td>
                            <td>RuleApplyDate:
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxRuleApply" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxRuleApply" Mask="99-99-9999"
                                    ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxRuleApply"
                                    EmptyValueMessage="Please enter date."
                                    InvalidValueMessage="Invalid date format."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                                    ValidationGroup="DiscardRules" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRuleApply"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>

                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="DiscardRules" /></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" /></td>
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
                                <asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" /></td>
                            <td>
                                <input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
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
    <div id="dvgridview" style="height: 360px; width: 100%; overflow: auto;" runat="server" visible="false">
        <asp:GridView ID="Gv_DiscardRules" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="DiscardRulesID" OnSelectedIndexChanged="Gv_DiscardRules_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="5" />
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("Center") %>' />
                        <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="10" />
                <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="10" />
                <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="5" />
                <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="5" />
                <asp:BoundField DataField="RuleApplyHorseAge" HeaderText="Horse Age" ItemStyle-Width="10" />
                <asp:BoundField DataField="MaxHandicapRating" HeaderText="Max Rating" ItemStyle-Width="10" />
                <asp:BoundField DataField="MinHandicapRating" HeaderText="Min Rating" ItemStyle-Width="5" />
                <asp:BoundField DataField="RuleApplyDate" HeaderText="RuleApplyDate" ItemStyle-Width="5" />
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
