<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddHorseRetirementAge.aspx.cs" Inherits="VKATalk.Master.AddHorseRetirementAge" %>

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
                    title: "Horse Retirement Age",
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
    <asp:ScriptManager ID="ScripManager2" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Horse Retirement Age</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 820px;" class="Userlogin">
                    <table align="center">
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="HorseRetirementAge" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnCenterName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvd" InitialValue="-1" runat="server" ErrorMessage="Please select Center" ValidationGroup="HorseRetirementAge" ControlToValidate="drpdwnCenterName">*</asp:RequiredFieldValidator>

                            </td>
                            <td>From Year:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" InitialValue="-1" ErrorMessage="Please select FromYear" ValidationGroup="HorseRetirementAge" ControlToValidate="drpdwnFromYear">*</asp:RequiredFieldValidator>

                            </td>
                            <td>Till Year:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>From Season:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="uuui" InitialValue="-1" runat="server" ErrorMessage="Please enter FromSeason" ValidationGroup="HorseRetirementAge" ControlToValidate="drpdwnFromSeason">*</asp:RequiredFieldValidator>

                            </td>
                            <td>Till Season:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList>

                            </td>
                            <td>HorseRetirementAge:(*)
                            </td>
                            <td>
                                <asp:TextBox ID="txtbxHorseRetirementAge" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rwee" runat="server" ErrorMessage="Please enter HorseRetirementAge" ValidationGroup="HorseRetirementAge" ControlToValidate="txtbxHorseRetirementAge">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="HorseRetirementAge" /></td>
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
    <div id="dvgridview" style="height: 360px; width: 100%; overflow: auto;" runat="server">
        <asp:GridView ID="Gv_HorseRetirementAge" runat="server" Width="100%" GridLines="Both"
            AutoGenerateColumns="False" DataKeyNames="HorseRetirementAgeID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="10" />
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("Center") %>' />
                        <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="30" />
                <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="30" />
                <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="30" />
                <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="30" />
                <asp:BoundField DataField="HorseRetirementAge" HeaderText="HorseRetirementAge" ItemStyle-Width="30" />
            </Columns>
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
