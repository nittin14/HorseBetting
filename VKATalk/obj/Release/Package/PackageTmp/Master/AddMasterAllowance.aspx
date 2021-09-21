<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AddMasterAllowance.aspx.cs" Inherits="VKATalk.Master.AddMasterAllowance" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Add Master Allowance</title>
     <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
     <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    
     <script type="text/javascript">
         function ShowPopup(message) {
             $(function () {
                 $("#dialog").html(message);
                 $("#dialog").dialog({
                     title: "Allowance",
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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Allowance</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 370px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="AlowCondition" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Center Name:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnCenterName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator5" Display="Dynamic" 
                                    ValidationGroup="AlowCondition" runat="server" ControlToValidate="drpdwnCenterName"
                                    Text="*" ErrorMessage="Please select Center Name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>From Year:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator6" Display="Dynamic" 
                                    ValidationGroup="AlowCondition" runat="server" ControlToValidate="drpdwnFromYear"
                                    Text="*" ErrorMessage="Please select From Year"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Till Year:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>From Season:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator7" Display="Dynamic" 
                                    ValidationGroup="AlowCondition" runat="server" ControlToValidate="drpdwnFromSeason"
                                    Text="*" ErrorMessage="Please select From Season"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Till Season:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Fixed:</td>
                            <td>
                                <asp:CheckBox ID="chkFixed" runat="server" />
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter alias name"  ValidationGroup="Center" ControlToValidate="txtbxAlias">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>

                        <tr>
                            <td>Jockey Age:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxJockeyAge" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter jockey age" ValidationGroup="AlowCondition" ControlToValidate="txtbxJockeyAge">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Total Win From:(*)</td>
                            <td>
                                <asp:TextBox ID="txtTotalWinFrom" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Total Win From" ValidationGroup="AlowCondition" ControlToValidate="txtTotalWinFrom">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Total Win Till:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxTotalWinTo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Total Win To" ValidationGroup="AlowCondition" ControlToValidate="txtbxTotalWinTo">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Allowance (In Kg):(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxAllowance" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter Allowance" ValidationGroup="AlowCondition" ControlToValidate="txtbxAllowance">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add" OnClick="BtnSubmit_Click" ValidationGroup="AlowCondition" /></td>
                            <td><asp:Button ID="btnDownload" runat="server" Text="Download" /></td>
                            <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
                            <td><asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" /></td>
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
                                                <asp:GridView ID="gridvwAddAllowance" runat="server" Width="100%"
                                                    AutoGenerateColumns="False" DataKeyNames="AllowanceID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
                                                    
                                                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                        HorizontalAlign="Center" />
                                                    
                                                    <Columns>
                                                        <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="10" />
                                                        <asp:TemplateField HeaderText="Center" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("Center") %>' />
                                                                <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="JockeyAge" HeaderText="Jockey Age" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="TotalWinFrom" HeaderText="Total Win From" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="TotalWinTill" HeaderText="Total Win Till" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="Allowance" HeaderText="Allowance" ItemStyle-Width="30" />
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
