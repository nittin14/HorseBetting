<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddShoe.aspx.cs" Inherits="VKATalk.Master.AddShoe" %>
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
                     title: "Shoe",
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
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Shoe</h1>
    <div id="dialog" style="display: none">
</div>
<table align="center">
    <tr>
    <td>
        <fieldset style="width:900px;" class="Userlogin">
            <table align="center">
    <tr>
        <td colspan="2">
        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="ShoeDescription" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
         Font-Size="12" />
        </td>
    </tr>
<tr>
        <td>Shoe:(*)</td>
        <td>
            <asp:TextBox ID="txtbxShoe" runat="server"> </asp:TextBox>
            <asp:RequiredFieldValidator ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="ShoeDescription" runat="server" ControlToValidate="txtbxShoe"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Shoe Description."></asp:RequiredFieldValidator>
        </td>
        <td>Shoe In Detail(*):</td>
        <td><asp:TextBox ID="txtbxShoesInDetail" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="ShoeDescription" runat="server" ControlToValidate="txtbxShoesInDetail"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Shoe Description Alias."></asp:RequiredFieldValidator>
        </td>
        
</tr>

<tr>
        <td>Shoe Metal:(*)</td>
        <td>
            <asp:DropDownList ID="drpdwnShoeMetal" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" InitialValue="-1" 
                            ValidationGroup="ShoeDescription" runat="server" ControlToValidate="drpdwnShoeMetal"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Shoe Metal."></asp:RequiredFieldValidator>
        </td>
        <td></td>
    <td></td>
        
</tr>

<tr>
        <td>Left Fore Leg:</td>
        <td>
            <%--<asp:TextBox ID="txtbxFRLeg" runat="server"></asp:TextBox>--%>
            <asp:DropDownList ID="drpdwnleftforleg" runat="server"></asp:DropDownList>
        </td>
        <td>Right Fore Leg:</td>
        <td>
            <asp:DropDownList ID="drpdwnRightForeLeg" runat="server"></asp:DropDownList>
        </td>
       
</tr>
<tr>
        <td>Left Hind Leg:</td>
        <td>
            <asp:DropDownList ID="drpdwnLeftHindleg" runat="server"></asp:DropDownList>
        </td>
        <td>Right Hind Leg:</td>
        <td>
            <asp:DropDownList ID="drpdwnRightHindleg" runat="server"></asp:DropDownList>
        </td>
   
      
</tr>
</table>
            <table align="center">
<tr>
<td><asp:Button ID="BtnSubmit" runat="server" Text="Add" 
        onclick="BtnSubmit_Click" ValidationGroup="ShoeDescription" /></td>
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
             <asp:GridView ID="Gv_ShoeDescription" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="ShoeMID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="10" />
                <asp:TemplateField HeaderText="Shoe" ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("Shoe") %>' />
                        <asp:LinkButton Text='<%# Bind("Shoe") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ShoeInDetail" HeaderText="Shoe In Detail" ItemStyle-Width="30" />
                <asp:BoundField DataField="ShoeMetal" HeaderText="Shoe Metal" ItemStyle-Width="30" />
                <asp:BoundField DataField="LFLShoeBaseMID" HeaderText="Left Fore Leg" ItemStyle-Width="30" />
                <asp:BoundField DataField="RFLShoeBaseMID" HeaderText="Right Fore Leg" ItemStyle-Width="30" />
                <asp:BoundField DataField="LHLShoeBaseMID" HeaderText="Left Hind Leg" ItemStyle-Width="30" />
                <asp:BoundField DataField="RHLShoeBaseMID" HeaderText="Right Hind Leg" ItemStyle-Width="30" />
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
