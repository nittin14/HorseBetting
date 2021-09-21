<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorseName.aspx.cs" Inherits="VKATalk.PopUps.HorseName" %>
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
                    title: "Horse Name",
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
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Horse Name</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HorseNamePopup" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                         <asp:HiddenField runat="server" ID="hdnfldhorseId"/>
                        <asp:HiddenField runat="server" ID="hrseDateofNameChange"/>
                    </td>
                </tr>
                <tr>
                    <td>Name:(*)

                    </td>
                    <td>
                        <asp:TextBox ID="txtbxHorseName" runat="server" AutoPostBack="True" OnTextChanged="txtbxHorseName_OnTextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Horse name" ValidationGroup="HorseNamePopup" ControlToValidate="txtbxHorseName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Short Name:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxHorseShortName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Short name" ValidationGroup="HorseNamePopup" ControlToValidate="txtbxHorseShortName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Name Alias:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxHorseAlias" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Alias name" ValidationGroup="HorseNamePopup" ControlToValidate="txtbxHorseAlias">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                    <td>
                        Profile:(*)
                    </td>
                    <td>
                            <asp:DropDownList runat="server" ID="drpdwnProfile">
                           <%-- <asp:ListItem Selected="True" Text="Horse"></asp:ListItem>
                            <asp:ListItem Text="Sire"></asp:ListItem>
                            <asp:ListItem Text="Dam"></asp:ListItem>
                            <asp:ListItem Text="Broodmare Dam"></asp:ListItem>
                            <asp:ListItem Text="Broodmare Sire"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
              
                <tr>
                    <td>Date of Birth:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxDateofBirth" runat="server" ValidationGroup="HorseNamePopup" AutoPostBack="True" OnTextChanged="txtbxDateofBirth_OnTextChanged"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxDateofBirth" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxDateofBirth"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="HorseNamePopup" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDateofBirth"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                  <tr>
                    <td>Age:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxAge" runat="server" AutoPostBack="True" OnTextChanged="txtbxAge_OnTextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddCommentList"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxComment" CompletionListElementID="Div6"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="HorseNamePopup" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/>
                        <%--<asp:Button runat="server" id="btnDelete" text="Delete" ValidationGroup="HorseNamePopup"/>    --%>
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
                <asp:GridView ID="GvHorseName" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="HorseNameID" OnSelectedIndexChanged="GvHorseName_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="Current Status" ItemStyle-Width="10" />
                        <asp:TemplateField HeaderText="Name/ New Name" ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("HorseName") %>'/>
                                <asp:LinkButton Text='<%# Bind("HorseName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HorseShortName" HeaderText="Short Name/ New Short Name" ItemStyle-Width="20" />
                        <asp:BoundField DataField="HorseNameAlias" HeaderText="Alias Name/ New Alias Name" ItemStyle-Width="10" />
                        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" ItemStyle-Width="10" />
                        <asp:BoundField DataField="DateOfNameChange" HeaderText="Date of Name Change" ItemStyle-Width="10" />
                        <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="10" />
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