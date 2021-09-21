<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddIncident.aspx.cs" Inherits="VKATalk.Master.AddIncident" %>
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
                    title: "Incident",
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
        }

        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            left: auto;
            margin: 0px;
        }

        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
            margin: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager id="ScripManager2" runat="server"></asp:ScriptManager>
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Incident</h1>
<div id="dialog" style="display: none">
</div>
<table align="center">
    <tr>
    <td>
        <fieldset style="width:800px;" class="Userlogin">
            <table>
                <tr>
                    <td colspan="6">
                    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Incident" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
                     Font-Size="12" />
                    </td>
                </tr>
                <tr>
                    <td>Incident:(*)</td>
                    <td><asp:TextBox ID="txtbxIncidentName" runat="server" Width="450px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Incident"  ValidationGroup="Incident" ControlToValidate="txtbxIncidentName">*</asp:RequiredFieldValidator>

                    </td>
 </tr>
                <tr>
                    <td>Incident Alias:(*)</td>
                    <td><asp:TextBox ID="txtbxAlias" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqv1" runat="server" ErrorMessage="Please enter Alias"  ValidationGroup="Incident" ControlToValidate="txtbxAlias">*</asp:RequiredFieldValidator>

                    </td>
                     </tr>
                <tr>
                    <td>Incident Impact:(*)</td>
                    <td><asp:TextBox ID="txtbxImpact" runat="server" MaxLength="1"></asp:TextBox>
                        <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                         <asp:AutoCompleteExtender ServiceMethod="IncidentImpactList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                             CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxImpact" CompletionListElementID="Div6"
                            ID="ACComments" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Incident Impact"  ValidationGroup="Incident" ControlToValidate="txtbxImpact">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rvDigits" runat="server" ControlToValidate="txtbxImpact"
                                ErrorMessage="Enter numbers only" ValidationGroup="Incident" ForeColor="Red"
                                ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                </td>
   
                </tr>
</table>
            <table>
<tr>
<td><asp:Button ID="BtnSubmit" runat="server" Text="Add" 
        onclick="BtnSubmit_Click" ValidationGroup="Incident" /></td>
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
             <asp:GridView ID="Gv_Incident" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="IncidentID" OnSelectedIndexChanged="Gv_Incident_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="5" />
                  <asp:TemplateField HeaderText="Incident" ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Incident") %>'/>
                        <asp:LinkButton Text='<%# Bind("Incident") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Alias" HeaderText="Alias" ItemStyle-Width="30" />
               <asp:BoundField DataField="Impact" HeaderText="Impact" ItemStyle-Width="30" />
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


