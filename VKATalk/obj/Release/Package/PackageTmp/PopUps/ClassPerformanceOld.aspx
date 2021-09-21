<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ClassPerformanceOld.aspx.cs" Inherits="VKATalk.PopUps.ClassPerformanceOld" %>

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
                    title: "Class Performance Old",
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Horse Bunch Group Performance Old</h1>
    <div id="dialog" style="display: none">
    </div>
    <div>
        <table style="width: 100%;">
            <tr>
                <td></td>
                <td>
                    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="classperformance" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                        Font-Size="12" />
                </td>
            </tr>

            <tr>
                <td style="width:15%">Horse Name:
                </td>
                <td>
                    <asp:Label runat="server" ID="lblHorseNameFirst"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdnfieldHorseName" />
                    <asp:HiddenField runat="server" ID="horseId" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lblHorseNameSecond"></asp:Label>
                </td>
            </tr>
      <%--  </table>
        <table style="width: 100%;">--%>
            <tr>
                <td style="width:15%">Bunch Group:(*)
                    </td>
                <td><asp:Button runat="server" id="btnClass" runnat="server" Text="Add Bunch Group" OnClick="btnClass_Click"/></td>
                <td style="padding-left:75px;">
                     <div style="height: 50px; overflow: auto; width:75%">
                        <asp:CheckBoxList runat="server" ID="chkbxDistance" RepeatDirection="Horizontal"></asp:CheckBoxList>
                    </div>
                </td>
            </tr>
                    
            <tr>
                <td style="width:15%">Performance Added Till Date:(*)</td>
                 <td>
                    <asp:TextBox ID="txtbxFromDate" runat="server" Width="75px" Text="14-04-2019"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                    <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxFromDate"
                        Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                        ControlExtender="mskDateAvailable"
                        ControlToValidate="txtbxFromDate"
                        EmptyValueMessage="Please enter date."
                        InvalidValueMessage="Invalid date format."
                        Display="Dynamic"
                        IsValidEmpty="true"
                        InvalidValueBlurredMessage="*"
                        ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                        ValidationGroup="classperformance" />
                    <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxFromDate"
                        Format="dd-MM-yyyy"></asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Performance will Add:(*)</td>
                <td>
                    <asp:CheckBox runat="server" ID="chkboxShow" Checked="True" /></td>
            </tr>

        </table>

    </div>
    <br />
    <div id="dvClassperformance" runat="server" Visible="False">
        <asp:GridView ID="gvDistance" runat="server" AutoGenerateColumns="false" CellPadding="5" DataKeyNames="ClassGroupTypeID">
            <Columns>
                <asp:TemplateField HeaderText="SL No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class Group(*)">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDistance" Text='<%# Bind("ClassGroupType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Performance (l / ll / lll / lV / Total Runs)(*)">
                    <ItemTemplate>
                        <asp:TextBox ID="txtbxPerformance1" runat="server" Width="2%" MaxLength="2"> </asp:TextBox>/
                 <asp:TextBox ID="txtbxPerformance2" runat="server" Width="2%" MaxLength="2"></asp:TextBox>/
                <asp:TextBox ID="txtbxPerformance3" runat="server" Width="2%" MaxLength="2"></asp:TextBox>/
                <asp:TextBox ID="txtbxPerformance4" runat="server" Width="2%" MaxLength="2"></asp:TextBox>/
                <asp:TextBox ID="txtbxPerformance5" runat="server" Width="3%" MaxLength="2"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <table id="tblDistanceUpdate" runat="server" visible="False">
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="classperformanceDistanceupdate" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                    Font-Size="12" />
            </td>
        </tr>
        <tr>
            <td>Bunch Group:(*)</td>
            <td>
                <asp:DropDownList runat="server" ID="drpdwnDistance" />
            </td>
        </tr>
        <tr>
            <td>I/II/III/IV/Total Runs:</td>
            <td>
                <asp:TextBox runat="server" ID="txtbx1" Width="2%" MaxLength="2"></asp:TextBox>/
                    <asp:TextBox runat="server" ID="txtbx2" Width="2%" MaxLength="2"></asp:TextBox>/
                    <asp:TextBox runat="server" ID="txtbx3" Width="2%" MaxLength="2"></asp:TextBox>/
                    <asp:TextBox runat="server" ID="txtbx4" Width="2%" MaxLength="2"></asp:TextBox>/
                    <asp:TextBox runat="server" ID="txtbx5" Width="4%" MaxLength="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Performance(I)." ValidationGroup="classperformanceDistanceupdate" ControlToValidate="txtbx1">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Performance(II)." ValidationGroup="classperformanceDistanceupdate" ControlToValidate="txtbx2">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Performance(III)." ValidationGroup="classperformanceDistanceupdate" ControlToValidate="txtbx3">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter Performance(IV)." ValidationGroup="classperformanceDistanceupdate" ControlToValidate="txtbx4">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Performance(Total Runs)." ValidationGroup="classperformanceDistanceupdate" ControlToValidate="txtbx5">*</asp:RequiredFieldValidator>
            </td>
        </tr>
       
    </table>
    <table>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="classperformance" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="classperformance"/>    
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
   
    <div id="dvDistanceOld" style="height: 350px; width: 100%; overflow: auto;" runat="server">
        <asp:GridView ID="GvDistanceOld" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="ClassGroupPerformanceOldID" OnSelectedIndexChanged="GvDistanceOld_OnSelectedIndexChanged">
            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="Till Date">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("TillDate") %>' />
                        <asp:LinkButton Text='<%# Bind("TillDate") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsShow" HeaderText="Is Show" ItemStyle-Width="10%" />
                <asp:BoundField DataField="BunchGroup" HeaderText="Class Group" ItemStyle-Width="10%" />
                <asp:BoundField DataField="I" HeaderText="I" ItemStyle-Width="10%" />
                <asp:BoundField DataField="II" HeaderText="II" ItemStyle-Width="10%" />
                <asp:BoundField DataField="III" HeaderText="III" ItemStyle-Width="10%" />
                <asp:BoundField DataField="IV" HeaderText="IV" ItemStyle-Width="10%" />
                <asp:BoundField DataField="TotalRuns" HeaderText="Total Runs" ItemStyle-Width="10%" />
            </Columns>
            <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
            <PagerStyle HorizontalAlign="Left" />
        </asp:GridView>
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
