<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="VKATalk.Master.AddClass" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
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
                     title: "Class",
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
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Class</h1>
     <div id="dialog" style="display: none">
</div>
<table align="center">
    <tr>
    <td>
        <fieldset style="width:100%" class="Userlogin">
            <table align="center">
                <tr>
                    <td colspan="5">
                    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Class" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana"  Font-Size="12" />
                    </td>
                </tr>
                    <tr>
                    <td>Center:(*)</td>  
                        <td colspan="3"><asp:DropDownList ID="DrpdwnCenter" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator2" Display="Dynamic" 
                            ValidationGroup="Class" runat="server" ControlToValidate="DrpdwnCenter"
                            Text="*" ErrorMessage="Please select center"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                                    <tr>
                        <td>From Year:(*)</td>
                    <td><asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                         <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="Class" runat="server" ControlToValidate="drpdwnFromYear"
                            Text="*" ErrorMessage="Please select From Year"></asp:RequiredFieldValidator>
                    </td>
                    <td>Till Year:(*)</td>
                        <td><asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator4" Display="Dynamic" 
                            ValidationGroup="Class" runat="server" ControlToValidate="drpdwnTillYear"
                            Text="*" ErrorMessage="Please select Till Year"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                                    <tr>
                    <td>From Season:(*)</td>
                    <td><asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                         <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator3" Display="Dynamic" 
                            ValidationGroup="Class" runat="server" ControlToValidate="drpdwnFromSeason"
                            Text="*" ErrorMessage="Please select From Season"></asp:RequiredFieldValidator>
                    </td>
                    <td>Till Season:</td>
                    <td><asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList></td>
                                        <td>
                                            Fix:
                                        </td>
                                        <td><asp:CheckBox runat="server" id="chkbxFix"/> </td>
                    </tr>
                
                <tr>
                   <%-- <td>Category :</td>
                     <td>
                         <asp:DropDownList ID="drpdwnCategory" runat="server">
                             <asp:ListItem Selected="True" Text="--Please select--" Value="-1"></asp:ListItem>
                             <asp:ListItem Text="Category I" Value="1"></asp:ListItem>
                             <asp:ListItem Text="Category II" Value="2"></asp:ListItem>
                             <asp:ListItem Text="Category III" Value="3"></asp:ListItem>
                         </asp:DropDownList>
                    </td>--%>
                    
                    <td>Handicap Rating Range :(*)</td>
                    <td colspan="1">
                        <asp:DropDownList ID="drpdwnRatingRange" runat="server"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtbxHandicapRatingRange" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="rvc3" runat="server" ErrorMessage="Please select HandicapRatingRange"  ValidationGroup="Class" 
                            ControlToValidate="drpdwnRatingRange" InitialValue="-1">*</asp:RequiredFieldValidator>
                    </td>
                     <td>Class :(*)</td>
                    <td>
                        <asp:DropDownList ID="drpdwnClassType" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please select Class"  ValidationGroup="Class" ControlToValidate="drpdwnClassType"
                             InitialValue="-1">*</asp:RequiredFieldValidator>

                    </td>
                  
                    </tr>
                
     </table>
              <table align="center">
                     <tr>
                        <td><asp:Button ID="BtnSubmit" runat="server" Text="Add" onclick="BtnSubmit_Click" ValidationGroup="Class" /></td>
                         <td><asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" /></td>
                         <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
                        <td><asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" /></td>   
                         <td><asp:Button ID="btnImport" runat="server" Text="Import" />
                                <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                    CancelControlID="Button2" BackgroundCssClass="Background">
                                </asp:ModalPopupExtender>
                            </td>
                            <td><asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
                         <td><asp:Button ID="btnExportToday" runat="server" Text="Export Today"/></td>
                        <td><asp:Button ID="btnPrint" runat="server" Text="Print"/></td>
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
<div id="dvgridview" style="height:360px; width:100%; overflow:auto;" runat="server">
              <asp:GridView ID="GvClass" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="ClassID" EmptyDataText="No Class Found" OnSelectedIndexChanged="GvClass_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                 <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="5" />
                  <asp:TemplateField HeaderText="Center" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Center") %>'/>
                        <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="10" />
                <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="10" />
                <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="5" />
                <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="5" />
                <%--<asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="5" />--%>
                <asp:BoundField DataField="HandicapRatingRange" HeaderText="RatingRange" ItemStyle-Width="5" />
                <%--<asp:BoundField DataField="MinHandicapRating" HeaderText="MinRating" ItemStyle-Width="5" />
                <asp:BoundField DataField="MaxHandicapRating" HeaderText="MaxRating" ItemStyle-Width="5" />--%>
                <asp:BoundField DataField="Class" HeaderText="Class" ItemStyle-Width="5" />
                <%--<asp:BoundField DataField="ClassTypeAlias" HeaderText="ClassAlias" ItemStyle-Width="5" />--%>
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


