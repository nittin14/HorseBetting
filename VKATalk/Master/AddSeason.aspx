<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddSeason.aspx.cs" Inherits="VKATalk.Master.AddSeason" %>

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
                    title: "Season",
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Season</h1>
    <div id="dialog" style="display: none">
    </div>

    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 625px;" class="Userlogin">
                    <table>
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Season" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Season Name:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxSeasonName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvd" runat="server" ErrorMessage="Please enter Season Name" ValidationGroup="Season" ControlToValidate="txtbxSeasonName">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="Season"
                                    ControlToValidate="txtbxSeasonName" Text="*" ErrorMessage="Only alphabets are allowed" Display="Dynamic"
                                    ForeColor="Red" ValidationExpression="^[a-zA-Z_ ]*$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Season Alias:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxSeasonAlias" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Season Alias" ValidationGroup="Season" ControlToValidate="txtbxSeasonAlias">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Season"
                                    ControlToValidate="txtbxSeasonAlias" Text="*" ErrorMessage="Only alphabets are allowed" Display="Dynamic"
                                    ForeColor="Red" ValidationExpression="^[a-zA-Z_ ]*$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="Season" /></td>
                            <td><asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" /></td>
                            <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" /></td>
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
        <asp:GridView ID="Gv_Season" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="SeasonID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="10" />
                <asp:TemplateField HeaderText="SeasonName" ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("SeasonName") %>' />
                        <asp:LinkButton Text='<%# Bind("SeasonName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SeasonAlias" HeaderText="Season Alias" ItemStyle-Width="30" />
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
