<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterYearCenterWise.aspx.cs" Inherits="VKATalk.Master.MasterYearCenterWise" %>

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
                    title: "Year Center Wise",
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Master Year Center Wise</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 600px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="YearCenterWise" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnCenter" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" InitialValue="-1" ErrorMessage="Please select center name" ValidationGroup="YearCenterWise" ControlToValidate="drpdwnCenter">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Year:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnYear" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-1" ErrorMessage="Please select year" ValidationGroup="YearCenterWise" ControlToValidate="drpdwnYear">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>Year Start Date:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxStartDate" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxStartDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxStartDate"
                                    EmptyValueMessage="Please enter correct year start date."
                                    InvalidValueMessage="Please enter correct year start date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="YearCenterWise" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxStartDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Year End Date:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxEndDate" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxEndDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxEndDate"
                                    EmptyValueMessage="Please enter correct year end date."
                                    InvalidValueMessage="Please enter correct year end date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="YearCenterWise" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxEndDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="YearCenterWise" /></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" /></td>
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
    <div id="dvgridview" style="height: 300px; width: 100%; overflow: auto;" runat="server">
        <asp:GridView ID="GvGlobal" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="YearCWID" OnSelectedIndexChanged="GvGlobal_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="RowCount" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblRowCount" runat="server" Text='<%# Bind("RowCount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("CenterName") %>' />
                        <asp:LinkButton Text='<%# Bind("CenterName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="YearName" HeaderText="Year Name" ItemStyle-Width="15%" />
                <asp:BoundField DataField="YearStartDate" HeaderText="Start Date" ItemStyle-Width="30%" />
                <asp:BoundField DataField="YearEndDate" HeaderText="End Date" ItemStyle-Width="30%" />
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
