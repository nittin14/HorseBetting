<%@ Page Title="Master Horse Sex" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AddMasterHorseSex.aspx.cs" Inherits="VKATalk.Master.AddMasterHorseSex" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>

    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "HorseSex",
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Horse Sex</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 370px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="MasterHorseValidation" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Horse Sex:(*)</td>
                            <td>
                                <asp:TextBox ID="txtHorseSex" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter horse sex" ValidationGroup="MasterHorseValidation" ControlToValidate="txtHorseSex">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Alias:(*)</td>
                            <td>
                                <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter alias" ValidationGroup="MasterHorseValidation" ControlToValidate="txtAlias">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="MasterHorseValidation" /></td>
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
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
    <table>
        <tr>
            <td>File Upload:</td>
            <td><asp:FileUpload ID="flupload" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnFileUpload" runat="server" Text="Upload" OnClick="btnFileUpload_Click" /></td>
            <td><asp:Button ID="Button2" runat="server" Text="Close" /></td>
        </tr>
    </table>
    
</asp:Panel>

    <div id="dvgridview" style="height: 360px; width: 100%; overflow: auto;" runat="server">
        <asp:GridView ID="gvMasterHorseSex" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="HorseSexID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="2%" />
                <asp:TemplateField HeaderText="Horse Sex" ItemStyle-Width="45%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("HorseSex") %>' />
                        <asp:LinkButton Text='<%# Bind("HorseSex") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Alias" HeaderText="Alias" ItemStyle-Width="45%" />
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
