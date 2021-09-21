<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNBCondition.aspx.cs" Inherits="VKATalk.Master.AddNBCondition" %>
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
                    title: "NB Condition",
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
<asp:ScriptManager id="ScripManager1" runat="server"></asp:ScriptManager>
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">NB Condition</h1>
    <div id="dialog" style="display: none">
</div>
<table align="center">
  <tr>
    <td>
     <fieldset style="width:100%" class="Userlogin">
            <table align="center">
    <tr>
    <td colspan="5">
    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="NBCondition" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
     Font-Size="12" />
    </td>
    </tr>
<tr>
<td>Center Name:(*)</td>
<td colspan="3"><asp:DropDownList ID="drpdwnCenterName" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="NBCondition" runat="server" ControlToValidate="drpdwnCenterName"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Center name."></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td>From Year:(*)</td>
<td><asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="NBCondition" runat="server" ControlToValidate="drpdwnFromYear"
                            Text="*" ForeColor="Red" ErrorMessage="Please select From Year."></asp:RequiredFieldValidator>

</td>
    </tr>
                <tr>
    <td>Till Year:</td>
<td><asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList>
   
</td>
</tr>
                <tr>
<td>Race Type(*):</td>
<td colspan="3"><asp:RadioButtonList ID="rdbtnRaceType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="Handicap" Value="Handicap"></asp:ListItem>
                        <asp:ListItem Text="Terms" Value="Terms"></asp:ListItem>
                        </asp:RadioButtonList></td>

</tr>
                <tr>
<td>Minimum Top Weight:(**)</td>
<td colspan="3">
    <asp:TextBox ID="txtTopWeight" runat="server"></asp:TextBox>
</td>
</tr>
                                <tr>
<td>Minimum Bottom Weight:(**)</td>
<td colspan="3"><asp:TextBox ID="txtbxlowerweight" runat="server"></asp:TextBox>

</td>
</tr>
</table>
            <table>
<tr>
<td><asp:Button ID="BtnSubmit" runat="server" Text="Add" 
        onclick="BtnSubmit_Click" ValidationGroup="NBCondition" /></td>
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

<div id="dvgridview" style="height: 280px; width: 100%; overflow: auto;" runat="server">
             <asp:GridView ID="gridvwNBCondition" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="NBConditionID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                 <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="10" />
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Center") %>'/>
                        <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="30" />
                <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="30" />
               <asp:BoundField DataField="RaceType" HeaderText="Race Type" ItemStyle-Width="30" />
                    <asp:BoundField DataField="TopWeight" HeaderText="Min Top Weight" ItemStyle-Width="30" />
                    <asp:BoundField DataField="LowerWeight" HeaderText="Min Bottom Weight" ItemStyle-Width="30" />
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
