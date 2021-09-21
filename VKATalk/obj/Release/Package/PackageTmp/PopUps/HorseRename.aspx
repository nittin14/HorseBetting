﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HorseRename.aspx.cs" Inherits="VKATalk.PopUps.HorseRename" %>

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
                    title: "Horse New Name",
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
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Horse New Name</h1>
        <div id="dialog" style="display: none">
        </div>
       <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HorseNamePopup1" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                         <asp:HiddenField runat="server" ID="hdnfldhorseId"/>
                        <asp:HiddenField runat="server" ID="hrseDateofNameChange"/>
                    </td>
                </tr>
                <tr>
                    <td>New Name:(*)

                    </td>
                    <td>
                       <asp:TextBox ID="txtbxHorseRename" runat="server" Font-Size="14px" AutoPostBack="True" OnTextChanged="txtbxHorseRename_OnTextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Horse name" ValidationGroup="HorseNamePopup1" ControlToValidate="txtbxHorseRename">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>New Short Name:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxShortRename" runat="server" Font-Size="14px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Short name" ValidationGroup="HorseNamePopup1" ControlToValidate="txtbxShortRename">*</asp:RequiredFieldValidator>
                    </td>

                </tr>
                <tr>
                    <td>New Name Alias:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxHorseAliasRename" runat="server" Font-Size="14px" MaxLength="10"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Alias name" ValidationGroup="HorseNamePopup1" ControlToValidate="txtbxHorseAliasRename">*</asp:RequiredFieldValidator>
                    </td>

                </tr>

                <tr>
                    <td>Date of Name Change:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxDateofNameChange" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxDateofNameChange" Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                            ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtbxDateofNameChange"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="True"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="HorseNamePopup1" />
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxDateofNameChange"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>

                    </td>
                </tr>
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddCommentList"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxComment" CompletionListElementID="Div6"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                           <%--</div>                                 --%>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="HorseNamePopup1" />
                         <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/>
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="HorseNamePopup1" />
                        <asp:Button ID="btnClose" runat="server" Text="Close"  OnClick="btnClose_Click" />
                    </td>
                </tr>
            </table>
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