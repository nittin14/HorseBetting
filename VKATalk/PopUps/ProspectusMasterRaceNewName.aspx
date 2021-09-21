<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProspectusMasterRaceNewName.aspx.cs" Inherits="VKATalk.PopUps.ProspectusMasterRaceNewName" %>
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

        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Prospect Master Race New Name",
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
    <asp:ScriptManager ID="ToolkitScriptManager5" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Master Race New Name</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="PropRaceNewName" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                         <asp:HiddenField runat="server" ID="hdnfldprospectusid"/>
                        <%--<asp:HiddenField runat="server" ID="hrseDateofNameChange"/>--%>
                    </td>
                </tr>
                <tr>
                    <td>Master Race New Name:(*)

                    </td>
                    <td>
                        <asp:TextBox ID="txtbxRaceName" runat="server" Width="620px" AutoPostBack="true" OnTextChanged="txtbxRaceName_OnTextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Prospectus Race Name" ValidationGroup="PropRaceNewName" ControlToValidate="txtbxRaceName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Master Race New Name Alias:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxRaceNameAlias" runat="server" Width="620px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Alias name" ValidationGroup="PropRaceNewName" ControlToValidate="txtbxRaceNameAlias">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>Date of Name Change:(*)
                    </td>
                    <td>
                       <asp:TextBox ID="txtbxDateofNameChange" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxDateofNameChange" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                         <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxDateofNameChange"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="PropRaceNewName" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDateofNameChange"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
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
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="PropRaceNewName" />
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="PropRaceNewName" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/>
                        <asp:Button ID="btnClose" runat="server" Text="Close"  OnClick="btnClose_Click" />
                    </td>
                </tr>
            </table>
            
            
             <div id="dvProspectus" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvProspectus" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="MasterRaceNameID" OnSelectedIndexChanged="GvProspectus_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="Current Status" ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="Name/ New Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("MasterRaceName") %>'/>
                                 <asp:HiddenField runat="server" ID="hdnfieldParentID" Value='<%# Bind("MasterRaceNameParentID") %>' />
                                <asp:LinkButton Text='<%# Bind("MasterRaceName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MasterRaceNameAlias" HeaderText="Alias" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="DateOfNameChange" HeaderText="DateofNameChange" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="15%" />
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