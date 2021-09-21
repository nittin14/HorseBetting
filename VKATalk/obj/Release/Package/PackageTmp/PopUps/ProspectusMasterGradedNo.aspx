<%@ Page AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProspectusMasterGradedNo.aspx.cs" Inherits="VKATalk.PopUps.ProspectusMasterGradedNo" Language="C#" %>

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
         function ShowPopup(message) {
             $(function () {
                 $("#dialog").html(message);
                 $("#dialog").dialog({
                     title: "Master Race Graded No",
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Master Race Grade No</h1>
      <div id="dialog" style="display: none">
        </div>
    <div>
        <table align="center">
            <tr>
                <td>
                    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="MasterGradedNo" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                        Font-Size="12" />
                </td>
            </tr>
            <tr>
                <td>Master Race Name:
                </td>
                <td>
                    <asp:Label runat="server" ID="lblMasterRaceNameFirst"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdnfieldRaceName" />
                    <asp:HiddenField runat="server" ID="hdnfieldRaceId" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lblMasterRaceNameSecond"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Grade No:(*)
                </td>
                <td>
                   
                    <asp:DropDownList ID="drpdwnGradeNo" runat="server">
                        <asp:ListItem Selected="True" Text="--Please Select--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Grade 1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Grade 2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Grade 3" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="Req_ID" Display="Dynamic" InitialValue="-1"
                        ValidationGroup="MasterGradedNo" runat="server" ControlToValidate="drpdwnGradeNo"
                        Text="*" ForeColor="Red" ErrorMessage="Please select Grade No."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>From Year:(*)
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpdwnFromYear" />
                    <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic"
                        ValidationGroup="MasterGradedNo" runat="server" ControlToValidate="drpdwnFromYear"
                        Text="*" ForeColor="Red" ErrorMessage="Please select From Year."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Till Year:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpdwnTillYear" />
                </td>
            </tr>
            <tr>
                <td>My Comments:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtbxComment" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    <div id="listPlacement" style="height:300px; overflow-y:scroll;" ></div>
                    <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtbxComment" CompletionListElementID="listPlacement"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="MasterGradedNo" />
                    <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="MasterGradedNo" />
                    
                    <asp:Button ID="btnImport" runat="server" Text="Import" />
                    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
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
        <div id="dvHProspectus" style="height: 350px; width: 100%; overflow: auto;" runat="server">
            <asp:GridView ID="GvProspectus" runat="server" Width="100%"
                AutoGenerateColumns="False" DataKeyNames="MasterGradeNoID" OnSelectedIndexChanged="GvHorseStatus_OnSelectedIndexChanged">
                <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                    HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Graded No" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("GradeNo") %>' />
                            <asp:LinkButton Text='<%# Bind("GradeNo") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="10%" />
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
