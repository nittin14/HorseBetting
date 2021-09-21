<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCenter.aspx.cs" Inherits="VKATalk.Master.AddCenter" %>

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
                    title: "Center Popup",
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

    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Center</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 600px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Center" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxCenter" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter center name" ValidationGroup="Center" ControlToValidate="txtbxCenter">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="Center"
                                    ControlToValidate="txtbxCenter" Text="*" ErrorMessage="Only alphabets are allowed" Display="Dynamic"
                                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Center Alias:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxAlias" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter alias name" ValidationGroup="Center" ControlToValidate="txtbxAlias">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Center"
                                    ControlToValidate="txtbxAlias" Text="*" ErrorMessage="Only alphabets are allowed" Display="Dynamic"
                                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Center New Name:(**)</td>
                            <td>
                                <asp:TextBox ID="txtbxCenterOldName" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Center"
                                    ControlToValidate="txtbxCenterOldName" Text="*" ErrorMessage="Only alphabets are allowed" Display="Dynamic"
                                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Center Old Name"  ValidationGroup="Center" ControlToValidate="txtbxCenterOldName">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>Center New Name Alias:(**)</td>
                            <td>
                                <asp:TextBox ID="txtbxCenterOldNameAlias" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="Center"
                                    ControlToValidate="txtbxCenterOldNameAlias" Text="*" ErrorMessage="Only alphabets are allowed" Display="Dynamic"
                                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>

                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Center Old Name Alias"  ValidationGroup="Center" ControlToValidate="txtbxCenterOldNameAlias">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>Date of Name Change (Center):(**)</td>
                            <td>
                                <asp:TextBox ID="txtbxNameChangedate" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxNameChangedate" Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxNameChangedate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>

                            </td>

                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="Center" /></td>
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
        <asp:GridView ID="Gv_center" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanged="Gv_center_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="RowCount" ItemStyle-Width="10">
                    <ItemTemplate>
                        <asp:Label ID="lblRowCount" runat="server" Text='<%# Bind("RowCount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("CenterName") %>' />
                        <asp:LinkButton Text='<%# Bind("CenterName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CenterAlias" HeaderText="Center Alias" ItemStyle-Width="30" />
                <asp:BoundField DataField="CenterOldName" HeaderText="Center old Name" ItemStyle-Width="100" />
                <asp:BoundField DataField="CenterOldNameAlias" HeaderText="Center old Alias" ItemStyle-Width="30" />
                <asp:BoundField DataField="DateofNameChange" HeaderText="Date of Name Change(Center)" ItemStyle-Width="30" />
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
