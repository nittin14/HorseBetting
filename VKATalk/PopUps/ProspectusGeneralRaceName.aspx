<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProspectusGeneralRaceName.aspx.cs" Inherits="VKATalk.PopUps.ProspectusGeneralRaceName" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <title></title>
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
        function GetMasterProspectusID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldMasterProsectusId.ClientID %>').value = HdnKey;
        }
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Prospect General Race Name",
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
    <asp:ScriptManager ID="ToolkitScriptManager2" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">General Race Name</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="PropGObservation" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                         <asp:HiddenField runat="server" ID="hdnfldprospectusid"/>
                        <asp:HiddenField runat="server" ID="hdnfieldGridviewGeneralRaceID"/>
                        <asp:HiddenField runat="server" ID="hdfieldCurrentEx"/>
                    </td>
                </tr>
                <tr>
                            <td>Master Race:(*)</td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="txtbxProspectusMasterName" Width="700px" AutoPostBack="true" 
                                    OnTextChanged="txtbxProspectusMasterName_OnTextChanged"></asp:TextBox>
                                <asp:HiddenField ID="hdnfieldMasterProsectusId" runat="server" />
                                <div id="Div1" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddProspectusList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                        CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxProspectusMasterName" CompletionListElementID="Div1"
                                        OnClientItemSelected="GetMasterProspectusID"
                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                    
                                </div>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Prospectus Master Race Name" 
                                   ValidationGroup="PropGObservation" ControlToValidate="txtbxProspectusMasterName">*</asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                <tr>
                    <td>General Race Name:(*)

                    </td>
                    <td>
                        <asp:TextBox ID="txtbxRaceName" runat="server" Width="500px" AutoPostBack="true" OnTextChanged="txtbxRaceName_OnTextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Prospectus Race Name" ValidationGroup="PropGObservation" ControlToValidate="txtbxRaceName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>General Race Name Alias:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxRaceNameAlias" runat="server" Width="500px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Alias name" ValidationGroup="PropGObservation" ControlToValidate="txtbxRaceNameAlias">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Center:(*)
                    </td>
                    <td>
                        <asp:label ID="lblCenter" runat="server"></asp:label>
                    </td>
                </tr>
                 <tr>
                    <td>Season:(*)
                    </td>
                    <td>
                        <asp:label ID="lblSeason" runat="server"></asp:label>
                    </td>
                </tr>
                 <tr>
                    <td>Year:(*)
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpdwnYear"/>
                        <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter Year." ValidationGroup="PropGObservation" ControlToValidate="drpdwnYear">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>ID:
                    </td>
                    <td>
                        <asp:Label ID="lblgeneralraceidnamegridviewvalue" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Parameters:
                    </td>
                    <td>
                        <asp:Label ID="lblParameters" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="listPlacement" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddCommentList"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxComment" CompletionListElementID="listPlacement"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="1"></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="PropGObservation" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"  />
                        <asp:Button ID="btnClose" runat="server" Text="Close"  OnClick="btnClose_Click" />
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
             <div id="dvProspectus" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvProspectus" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="GeneralRaceNameID" OnSelectedIndexChanged="GvProspectus_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="Current Status" ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="Master Race Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblMasterRaceName" runat="server" Text='<%# Bind("MasterRaceName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name/ New Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("MasterRaceName") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldGeneralRaceName" Value='<%# Bind("GeneralRaceName") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldMasterRaceNameID" Value='<%# Bind("MasterRaceNameID") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldGeneralRaceNameID" Value='<%# Bind("GeneralRaceNameID") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldCENTERNAME" Value='<%# Bind("CENTERNAME") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldSEASONNAME" Value='<%# Bind("SEASONNAME") %>'/>
                                <asp:LinkButton Text='<%# Bind("GeneralRaceName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GeneralRaceNameAlias" HeaderText="Alias" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="YearName" HeaderText="Year" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="DateOfNameChange" HeaderText="DateofNameChange" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="20%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    
    <script type="text/javascript">
      
        function refreshParentPage() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }
    </script>
</asp:Content>