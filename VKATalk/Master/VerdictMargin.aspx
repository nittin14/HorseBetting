<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="VerdictMargin.aspx.cs" Inherits="VKATalk.Master.VerdictMargin" %>
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
                title: "Verdict Margin",
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
<asp:ScriptManager id="ScripManager2" runat="server"></asp:ScriptManager>
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Verdict Margin</h1>
<div id="dialog" style="display: none">
</div>
<table align="center">
    <tr>
    <td>
        <fieldset style="width:370px;" class="Userlogin">
            <table>
    <tr>
        <td colspan="2">
        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="VardictMargin" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
         Font-Size="12" />
        </td>
    </tr>
<tr>
<td>Verdict Margin:(*)</td>
<td><asp:TextBox ID="txtbxVerdictMargin" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Verdict Margin."  ValidationGroup="VardictMargin" ControlToValidate="txtbxVerdictMargin">*</asp:RequiredFieldValidator>

</td>
</tr>
<tr>
<td>Verdict Alias:(*)</td>
<td><asp:TextBox ID="txtbxAlias" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Verdict Margin Alias."  ValidationGroup="VardictMargin" ControlToValidate="txtbxAlias">*</asp:RequiredFieldValidator>

</td>
</tr>
<tr>
<td>Measurement:(*)</td>
<td><asp:TextBox ID="txtbxMeasurement" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Measurement."  ValidationGroup="VardictMargin" ControlToValidate="txtbxMeasurement">*</asp:RequiredFieldValidator>

</td>
</tr>
</table>
            <table>
<tr>
<td><asp:Button ID="BtnSubmit" runat="server" Text="Add" 
        onclick="BtnSubmit_Click" ValidationGroup="VardictMargin" /></td>
<td><asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" /></td>
    <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
<td><asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" /></td>
<td><asp:Button ID="btnImport" runat="server" Text="Import"/>
    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
    CancelControlID="Button2" BackgroundCssClass="Background">
</asp:ModalPopupExtender>

</td>
<td><asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"/></td>
<td><asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
<td><asp:Button ID="btnPrint" runat="server" Text="Print"/></td>
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
            <div id="dvgridview" style="height:360px; width:100%; overflow:auto;" runat="server">
             <asp:GridView ID="Gv_Distance" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="VerdictMarginID" EmptyDataText="No Distance Found" OnSelectedIndexChanged="Gv_Distance_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="2%" />
                <asp:TemplateField HeaderText="Verdict Margin" ItemStyle-Width="33%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("VerdictMargin") %>'/>
                        <asp:LinkButton Text='<%# Bind("VerdictMargin") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="VerdictMarginAlias" HeaderText="Verdict Margin Alias" ItemStyle-Width="33%" />
               <asp:BoundField DataField="Measurement" HeaderText="Measurement" ItemStyle-Width="33%" />
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
