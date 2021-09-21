<%@ Page AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ResultRaceCommentary.aspx.cs" Inherits="VKATalk.PopUps.ResultRaceCommentary" Language="C#" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <title></title>
      <script src="../Scripts/jquery1.7.2.min.js"></script>
      <script src="../Scripts/jquery-ui-1.8.9.js"></script>
      <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
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
            height: 100px;
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

     </style>
   <script type="text/javascript">
       function GetOwnerStudID(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           document.getElementById('<%=hdnfieldOwnerStudID.ClientID %>').value = HdnKey;
       }
       function GetCommentator(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           document.getElementById('<%=hdnfieldProfessionalNameCommentator.ClientID %>').value = HdnKey;
       }
       
       


       function ShowPopup(message) {
           $(function () {
               $("#dialog").html(message);
               $("#dialog").dialog({
                   title: "Result RaceCommentary",
                   buttons: {
                       Close: function () {
                           $(this).dialog('close');
                       }
                   },
                   modal: true
               });
           });
       };

       function SetContextKey() {
           $find('<%=AutoCompleteExtender8.ClientID%>').set_contextKey($get("<%=hdnfieldDivisionRaceDate.ClientID %>").value + ','
               + $get("<%=hdnfieldCenterID.ClientID %>").value);
        }
       </script>
  </asp:Content>
   <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Result RaceCommentary</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RaceCommentary" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                        <asp:HiddenField ID="hdnfieldDivisionRaceDate" runat="server" />
                        <asp:HiddenField ID="hdnfieldCenterID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Division Race Name:(*)
                    </td>
                    <td>
                         <asp:TextBox ID="txtbxOwnerStud" runat="server" Width="1000" onkeyup="SetContextKey()" AutoPostBack="true" 
                             OnTextChanged="txtbxOwnerStud_TextChanged"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnfieldOwnerStudID" />
                                                       <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddStudOwnerList"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxOwnerStud" CompletionListElementID="Div6"
                                                                OnClientItemSelected="GetOwnerStudID"
                                                                ID="AutoCompleteExtender8" runat="server" FirstRowSelected="false">
                                                            </asp:AutoCompleteExtender>
                         <asp:RequiredFieldValidator InitialValue="" ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="RaceCommentary" runat="server" ControlToValidate="txtbxOwnerStud"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Owner Stud."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Fix:
                    </td>
                    <td>
                       <asp:CheckBox ID="chkboxfix1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Commentator:(*)
                    </td>
                    <td>
                       <asp:TextBox ID="txtbxCommentator" runat="server" Width="500"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnfieldProfessionalNameCommentator" />
                                                       <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                                                            <asp:AutoCompleteExtender ServiceMethod="AddProfessionalName"
                                                                MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                                CompletionListItemCssClass=".AutoExtenderList"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                                TargetControlID="txtbxCommentator" CompletionListElementID="Div1"
                                                                OnClientItemSelected="GetCommentator"
                                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                            </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator2" Display="Dynamic" 
                            ValidationGroup="RaceCommentary" runat="server" ControlToValidate="txtbxCommentator"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Commentator."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Fix:
                    </td>
                    <td>
                       <asp:CheckBox ID="CheckBox2" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>Horse Name:(*)
                    </td>
                    <td>
                       <asp:DropDownList ID="drpdwnHorseName" runat="server" Width="500">
                       </asp:DropDownList>
                         <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="RaceCommentary" runat="server" ControlToValidate="drpdwnHorseName"
                            Text="*" ForeColor="Red" ErrorMessage="Please select Variant."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr>
                    <td>Commentary:(*)
                    </td>
                    <td>
                       <asp:TextBox ID="txtbxCommentary" runat="server" TextMode="MultiLine" Width="500" Height="100"></asp:TextBox>
                                                       <div id="Div2" style="height:300px; overflow-y:scroll;" ></div>
                         <asp:AutoCompleteExtender ServiceMethod="AddCommentaryCommentsList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                              CompletionListItemCssClass=".AutoExtenderList" CompletionListElementID="Div2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxCommentary"
                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator3" Display="Dynamic" 
                            ValidationGroup="RaceCommentary" runat="server" ControlToValidate="txtbxCommentary"
                            Text="*" ForeColor="Red" ErrorMessage="Please enter Commentary."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>Highlight:
                    </td>
                    <td>
                       <asp:CheckBox ID="chkbxHighlight" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click"/>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="RaceCommentary" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="RaceCommentary"/>    
                        <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
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
            <div id="dvHorseName" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvHorseGlobal" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="ResultRaceCommentaryCID" OnSelectedIndexChanged="GvHorseGlobal_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Division Race Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("DivisionRaceID") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldDivisionRaceName" Value='<%# Bind("DivisionRaceName") %>'/>
                                <asp:HiddenField runat="server" id="hdnfdProfessionalNameID" Value='<%# Bind("ProfessionalNameID") %>'/>
                                <asp:HiddenField runat="server" id="hdnflHorseNameID" Value='<%# Bind("HorseNameID") %>'/>
                                <asp:LinkButton Text='<%# Bind("DivisionRaceName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Commentator" HeaderText="Commentator" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="HorseName" HeaderText="Horse Name" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="Commentary" HeaderText="Commentary" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="HighLight" HeaderText="HighLight" ItemStyle-Width="10%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    
    <script type="text/javascript">

        function refreshParentPage() {
            window.close();
        }
    
    </script>
</asp:Content>