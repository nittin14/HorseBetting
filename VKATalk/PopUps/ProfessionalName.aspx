<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfessionalName.aspx.cs" Inherits="VKATalk.PopUps.ProfessionalName" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title></title>
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <style type="text/css">
        .AutoExtender {
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

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            left: auto;
            margin: 0px;
            list-style-type: none;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
            margin: 0px;
            list-style-type: none;
        }

        .ui-dialog-titlebar-close {
            visibility: hidden;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Professional Name",
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Professional Name</h1>
    <div id="dialog" style="display: none">
    </div>
    <div>
        <table align="center">
            <tr>
                <td>
                    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="ProfessionalNamePopup" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                        Font-Size="12" />
                    <asp:HiddenField runat="server" ID="hdnfldProfessionalId" />
                    <asp:HiddenField runat="server" ID="hdnfieldProfessionalDateofNameChange" />
                    <asp:HiddenField runat="server" ID="hdnfieldprofessionalName" />
                    <asp:HiddenField runat="server" ID="hdnfieldProfileID" />
                    <asp:HiddenField runat="server" ID="hdnfieldBaseCenterID" />
                    <asp:HiddenField runat="server" ID="hdnfieldRegistrationNumber" />

                </td>
            </tr>
            <tr>
                <td>Registration Number:</td>
                <td>
                    <asp:Label ID="lblRegistrationNumber" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Professional Name:(*)

                </td>
                <td>
                    <asp:TextBox ID="txtbxProfessionalName" runat="server" Width="750" Height="50" AutoPostBack="True" TextMode="MultiLine" OnTextChanged="txtbxProfessionalName_OnTextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Professional name" ValidationGroup="ProfessionalNamePopup" ControlToValidate="txtbxProfessionalName">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Professional Name Without Solutation:(*)

                </td>
                <td>
                    <asp:TextBox ID="txtbxPNWithoutSolution" runat="server" Width="750"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Professional name WS" ValidationGroup="ProfessionalNamePopup" ControlToValidate="txtbxPNWithoutSolution">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Professional Short Name:(*)
                </td>
                <td>
                    <asp:TextBox ID="txtbxProfessionalShortName" runat="server" Width="750"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Short name" ValidationGroup="ProfessionalNamePopup" ControlToValidate="txtbxProfessionalShortName">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Professional Name Alias:(*)
                </td>
                <td>
                    <asp:TextBox ID="txtbxProfessionalAlias" runat="server" Width="750"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Alias name" ValidationGroup="ProfessionalNamePopup" ControlToValidate="txtbxProfessionalAlias">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Profile:(*)
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpdwnProfiler" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please select Profile" ValidationGroup="ProfessionalNamePopup" InitialValue="-1" ControlToValidate="drpdwnProfiler">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Base Center:(*)
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpdwnBaseCenter" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select BaseCenter" ValidationGroup="ProfessionalNamePopup" InitialValue="-1" ControlToValidate="drpdwnBaseCenter">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Date Of Birth (Age):(*)
                </td>
                <td>
                    <asp:TextBox ID="txtbxDateofBirth" runat="server" Width="75px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxDateofBirth"
                        Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender1"
                        ControlToValidate="txtbxDateofBirth"
                        EmptyValueMessage="Please enter correct Date of Birth."
                        InvalidValueMessage="Please enter correct Date of Birth."
                        Display="Dynamic"
                        IsValidEmpty="true"
                        InvalidValueBlurredMessage="*"
                        ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                        ValidationGroup="ProfessionalNamePopup" />
                    <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxDateofBirth"
                        Format="dd-MM-yyyy"></asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Professional Type:(*)</td>
                <td colspan="6">
                    <asp:DropDownList runat="server" ID="drpdwnProfessionalType" />
                     <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator6" Display="Dynamic" 
                                    ValidationGroup="ProfessionalNamePopup" runat="server" ControlToValidate="drpdwnProfessionalType"
                                    Text="*" ErrorMessage="Please select Professional Type"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>My Comments:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtbxComment" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    <div id="Div1" style="height: 300px; overflow-y: scroll;"></div>
                    <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtbxComment" CompletionListElementID="Div1"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>


                    <%-- <div id="listPlacement" style="height:300px; overflow-y:scroll;" ></div>
                    <asp:AutoCompleteExtender ServiceMethod="AddCommentList"
                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                        CompletionListItemCssClass=".AutoExtenderList"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtbxComment" CompletionListElementID="listPlacement"
                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>--%>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="ProfessionalNamePopup" />
                    <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                    <%--<asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="ProfessionalNamePopup"/>    --%>
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
        <div id="dvHorseName" style="height: 350px; width: 100%; overflow: auto;" runat="server">
            <asp:GridView ID="GvHorseName" runat="server" Width="100%"
                AutoGenerateColumns="False" DataKeyNames="ProfessionalNameID" OnSelectedIndexChanged="GvHorseName_OnSelectedIndexChanged">
                <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                    HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="Status" HeaderText="Current Status" ItemStyle-Width="10" />
                    <asp:TemplateField HeaderText="Name/ New Name" ItemStyle-Width="10">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("ProfessionalName") %>' />
                            <asp:LinkButton Text='<%# Bind("ProfessionalName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProfessionalNameWS" HeaderText="Name without solutation/ New Name without solutation" ItemStyle-Width="10" />
                    <asp:BoundField DataField="ProfessionalShortName" HeaderText="Short Name/ New Short Name" ItemStyle-Width="10" />
                    <asp:BoundField DataField="ProfessionalNameAlias" HeaderText="Alias Name/ New Alias Name" ItemStyle-Width="10" />
                    <asp:BoundField DataField="DateOfNameChange" HeaderText="Date of Name Change" ItemStyle-Width="10" />
                    <asp:BoundField DataField="ProfileType" HeaderText="Profile" ItemStyle-Width="5" />
                    <asp:BoundField DataField="CenterName" HeaderText="Base Center" ItemStyle-Width="5" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" ItemStyle-Width="5" />
                    <asp:BoundField DataField="ProfessionalType" HeaderText="Professional Type" ItemStyle-Width="5" />
                    <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="20" />
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
