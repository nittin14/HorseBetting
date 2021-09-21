<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClassGroup.aspx.cs" Inherits="VKATalk.Master.AddClassGroup" %>

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

        .modalBackground
        {
            background-color: #333333;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: #CCCCCC;
            padding: 1px;
            width: 300px;
            Height: 200px;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Class Group",
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
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Class Group</h1>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 1070px;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="4">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="ClassGroup" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td colspan="3">
                                <asp:DropDownList ID="DrpdwnCenter" runat="server"></asp:DropDownList></td>
                            <asp:RequiredFieldValidator InitialValue="-1" ID="reqFieldValidatior" Display="Dynamic"
                                ValidationGroup="ClassGroup" runat="server" ControlToValidate="DrpdwnCenter"
                                Text="*" ErrorMessage="Please select Center"></asp:RequiredFieldValidator>
                        </tr>
                        <tr>
                            <td>From Year:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic"
                                    ValidationGroup="ClassGroup" runat="server" ControlToValidate="drpdwnFromYear"
                                    Text="*" ErrorMessage="Please select From Year"></asp:RequiredFieldValidator>
                            </td>
                            <td>Till Year:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillYear" runat="server"></asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td>From Season:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator2" Display="Dynamic"
                                    ValidationGroup="ClassGroup" runat="server" ControlToValidate="drpdwnFromSeason"
                                    Text="*" ErrorMessage="Please select From Season"></asp:RequiredFieldValidator>
                            </td>
                            <td>Till Season:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnTillSeason" runat="server"></asp:DropDownList></td>
                        </tr>


                        <tr>
                            <td>Race Type:(*)</td>
                            <td style="width: 150px;">
                                <asp:RadioButtonList ID="rdbtnRaceType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="-1" Text="Handicap" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Terms"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td></td>
                            <td></td>
                            
                        </tr>
                        <tr>
                            <td>Class:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnClass" runat="server"></asp:DropDownList></td>
                            <td>Age Condition:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnAgeCondition" runat="server"></asp:DropDownList></td>

                        </tr>

                        <tr>
                            <td>Race Status:(*)</td>
                            <td style="width: 200px;">
                                <asp:RadioButtonList ID="rdbtnRaceStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Normal" Value="-1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Big" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Classic" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>

                            <td>Million:</td>
                            <td>
                                <asp:CheckBox ID="chkbxMillion" runat="server" />
                               <%-- <asp:RadioButtonList ID="rdbtnMillion" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                            </td>
                        </tr>

                        <tr>
                            <td>Sweep Stake:</td>
                            <td style="width: 150px;">
                                 <asp:CheckBox ID="chkbxSweepStake" runat="server" />
                                <%--<asp:RadioButtonList ID="rdbtnSweepStake" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                </asp:RadioButtonList>--%>

                            </td>
                            <td>Classic</td>
                            <td>
                                 <asp:CheckBox ID="chkbxClassic" runat="server" />
                               <%-- <asp:RadioButtonList ID="rdbtnYesNo" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                </asp:RadioButtonList>--%>

                            </td>

                        </tr>
                        <tr>
                            <td>Grade:</td>
                            <td>
                                <asp:DropDownList ID="drpdwnGrade" runat="server">
                                    <asp:ListItem Selected="True" Value="-1" Text="-- Please Select --"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Grade 1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Grade 2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Grade 3"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:CheckBox ID="chkbxGrade" runat="server" />--%>
                               <%-- <asp:RadioButtonList ID="rdbtnGraded" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Class Group:(*)</td>
                            <td>
                                <asp:DropDownList ID="drpdwnClassGroup" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" InitialValue="-1"
                                    ValidationGroup="ClassGroup" runat="server" ControlToValidate="drpdwnClassGroup"
                                    Text="*" ErrorMessage="Please enter Class Group"></asp:RequiredFieldValidator>
                            </td>
                        </tr>


                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Add"
                                    OnClick="BtnSubmit_Click" ValidationGroup="ClassGroup" /></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" /></td>
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
    <div id="dvgridview" style="height: 360px; width: 100%; overflow: auto;" runat="server" visible="false">
        <asp:GridView ID="grdvwClassGroup" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="ClassGroupID" OnSelectedIndexChanged="GridView_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="5%" />
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("Center") %>' />
                        <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FromYear" HeaderText="From Year" ItemStyle-Width="5%" />
                <asp:BoundField DataField="TillYear" HeaderText="Till Year" ItemStyle-Width="5%" />
                <asp:BoundField DataField="FromSeason" HeaderText="From Season" ItemStyle-Width="5%" />
                <asp:BoundField DataField="TillSeason" HeaderText="Till Season" ItemStyle-Width="5%" />
                <asp:BoundField DataField="RaceType" HeaderText="Race Type" ItemStyle-Width="5%" />
                <%--<asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="5%" />--%>
                <asp:BoundField DataField="Class" HeaderText="Class" ItemStyle-Width="5%" />
                <asp:BoundField DataField="AgeCondition" HeaderText="Age Condition" ItemStyle-Width="5%" />
                <asp:BoundField DataField="RaceStatus" HeaderText="Race Status" ItemStyle-Width="4%" />
                <asp:BoundField DataField="Million" HeaderText="Million" ItemStyle-Width="4%" />
                <asp:BoundField DataField="SweepStake" HeaderText="SweepStake" ItemStyle-Width="4%" />
                <asp:BoundField DataField="Classic" HeaderText="Classic" ItemStyle-Width="4%" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" ItemStyle-Width="4%" />
                <asp:BoundField DataField="ClassGroupType" HeaderText="Class Group" ItemStyle-Width="5%" />
            </Columns>
            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
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
