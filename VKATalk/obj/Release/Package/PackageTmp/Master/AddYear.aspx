<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddYear.aspx.cs" Inherits="VKATalk.Master.AddYear" %>

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
                    title: "Year",
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Year</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 370px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Year" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Year:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxYearName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Year name" ValidationGroup="Year" ControlToValidate="txtbxYearName">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>Year Alias:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxYearAlias" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter alias name" ValidationGroup="Year" ControlToValidate="txtbxYearAlias">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <%--<tr>
                            <td>Year Start Date:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxStartdate" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateStart" CultureName="en-GB" runat="server" TargetControlID="txtbxStartdate" Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateStart"
                                    ControlToValidate="txtbxStartdate"
                                    EmptyValueMessage="Please enter start date."
                                    InvalidValueMessage="Invalid start date format."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                                    ValidationGroup="Year" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxStartdate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>

                            </td>
                        </tr>
                        <tr>
                            <td>Year End Date:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxEndDate" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskEndDate" CultureName="en-GB" runat="server" TargetControlID="txtbxEndDate" Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="mskEndDate"
                                    ControlToValidate="txtbxEndDate"
                                    EmptyValueMessage="Please enter end date."
                                    InvalidValueMessage="Invalid end date format."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                                    ValidationGroup="Year" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxEndDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>--%>
                    </table>
                    <table>
                        <tr>
                            <td><asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="Year" /></td>
                            <td><asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" /></td>
                            <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
                            <td><asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" /></td>
                            <td><asp:Button ID="btnImport" runat="server" Text="Import" />
                                <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                    CancelControlID="Button2" BackgroundCssClass="Background">
                                </asp:ModalPopupExtender>
                            </td>
                            <td><asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
                            <td><asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
                            <td><asp:Button ID="btnPrint" runat="server" Text="Print" /></td>
                            <td><input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>
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

    <div id="dvgridview" style="height: 360px; width: 100%; overflow: auto;" runat="server">
        <asp:GridView ID="Gv_year" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="YearID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="2%" />
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="40%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("YearName") %>' />
                        <asp:LinkButton Text='<%# Bind("YearName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="YearAlias" HeaderText="Alias" ItemStyle-Width="40%" />
               <%-- <asp:BoundField DataField="YEARSTARTDATE" HeaderText="Year start date" ItemStyle-Width="30" />
                <asp:BoundField DataField="YEARENDDATE" HeaderText="Year end date" ItemStyle-Width="30" />--%>
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
