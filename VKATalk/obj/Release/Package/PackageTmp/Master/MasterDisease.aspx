<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterDisease.aspx.cs" Inherits="VKATalk.Master.MasterDisease" %>
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

        .modalBackground { 
            background-color:#333333; 
            filter:alpha(opacity=70); 
            opacity:0.7; 
        } 
        .modalPopup { 
            background-color:#FFFFFF; 
            border-width:1px; 
            border-style:solid; 
            border-color:#CCCCCC; 
            padding:1px; 
            width:300px; 
            Height:200px; 
        }  
    </style>

        <script type="text/javascript">
            function ShowPopup(message) {
                $(function () {
                    $("#dialog").html(message);
                    $("#dialog").dialog({
                        title: "Disease",
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
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Disease</h1>
     <div id="dialog" style="display: none">
</div>
<table align="center">
  <tr>
    <td>
      <fieldset style="width:600px;" class="Userlogin">
   <table>
    <tr>
    <td colspan="2">
    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Disease" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
     Font-Size="12" />
    </td>
    </tr>
<tr>
<td>Disease:(*)</td>
<td><asp:TextBox ID="txtbxDisease" runat="server" style="width:350px;"></asp:TextBox>
<asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Disease name"  ValidationGroup="Disease" ControlToValidate="txtbxDisease">*</asp:RequiredFieldValidator>
    
</td>
</tr>
<tr>
<td>Disease Alias:(*)</td>
<td><asp:TextBox ID="txtbxAlias" runat="server" ></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter alias name"  ValidationGroup="Disease" ControlToValidate="txtbxAlias">*</asp:RequiredFieldValidator>
</td>
</tr>
                <tr>
<td>Disease In Detail:</td>
<td><asp:TextBox ID="txtbxDiseaseInDetail" runat="server" style="width: 350px; height: 50px;" TextMode="MultiLine"></asp:TextBox>
   
</td>
</tr>
       <tr>
<td>Medical Name:</td>
<td><asp:TextBox ID="txtbxmedicalname" runat="server" style="width: 350px;"></asp:TextBox>
   
</td>
</tr>
                <tr>
<td>Impact On Performance:</td>
<td><asp:TextBox ID="txtbxPerformanceImpact" runat="server" style="width: 350px; height: 100px;" TextMode="MultiLine"></asp:TextBox>
    
</td>
</tr>
         <tr>
<td>Treatment:</td>
<td><asp:TextBox ID="txtbxtreatment" runat="server" style="width: 350px; height: 50px;" TextMode="MultiLine"></asp:TextBox>
    
</td>
</tr>
        <tr>
<td>Precautions:</td>
<td><asp:TextBox ID="txtbxPrecautions" runat="server" style="width: 350px; height: 50px;" TextMode="MultiLine"></asp:TextBox>
    <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
    <asp:AutoCompleteExtender ServiceMethod="PrecautionsList"
                             MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                             CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                             CompletionListItemCssClass=".AutoExtenderList"
                             CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                             TargetControlID="txtbxPrecautions" CompletionListElementID="Div6"
                             ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender> 
    
</td>
</tr>
         <tr>
<td>My Comments:</td>
<td><asp:TextBox ID="txtbxComments" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
    <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
   <asp:AutoCompleteExtender ServiceMethod="DiseaseCommentList"
                             MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                             CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                             CompletionListItemCssClass=".AutoExtenderList"
                             CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                             TargetControlID="txtbxComments" CompletionListElementID="Div1" 
                             ID="ACComments" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender> 
</td>
</tr>
              
</table>
            <table>
<tr>
<td><asp:Button ID="BtnSubmit" runat="server" Text="Add" 
        onclick="BtnSubmit_Click" ValidationGroup="Disease" /></td>
<td><asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" /></td>
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
    <div id="dvgridview" style="height:300px; width:100%; overflow:auto;" runat="server">
             <asp:GridView ID="GvDisease" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="DiseaseID" OnSelectedIndexChanged="GvDisease_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="5%" />
                <asp:TemplateField HeaderText="Disease" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Disease") %>'/>
                        <asp:LinkButton Text='<%# Bind("Disease") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DiseaseAlias" HeaderText="Disease Alias" ItemStyle-Width="10%" />
                <asp:BoundField DataField="DiseaseInDetails" HeaderText="Disease In Details" ItemStyle-Width="10%" />
                <asp:BoundField DataField="MedicalName" HeaderText="Medical Name" ItemStyle-Width="10%" />
                 <asp:BoundField DataField="ImpactonPerformance" HeaderText="Impact on Performance" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="Treatment" HeaderText="Treatment" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="Precautions" HeaderText="Precautions" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="MyComments" HeaderText="Comments" ItemStyle-Width="20%" />
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
