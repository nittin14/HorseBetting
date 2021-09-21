<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Handicap.aspx.cs" Inherits="VKATalk.Card.Handicap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                    title: "Handicap",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function OpenHorsePopup(value) {
            var dateofrace = document.getElementById('<%=txtbxRaceDate.ClientID %>').value;
            var CenterID = document.getElementById('<%=txtbxRaceDate.ClientID %>').value;
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            if (value === "BanName") {
                window.open('../Popups/HandicapBan.aspx?DOB=' + dateofrace, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Handicap</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardHandicap" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Handicap Enter Date:(*)</td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="txtbxHandicapEnterDate" runat="server" Width="75px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="MaskedEditExtender1"
                                    ControlToValidate="txtbxHandicapEnterDate"
                                    EmptyValueMessage="Please enter correct Handicap date."
                                    InvalidValueMessage="Please enter correct Handicap date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="CardHandicap" />
                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="padding-left: 25px;">Race Date:(*)</td>
                            <td style="width: 300px">
                                <asp:TextBox ID="txtbxRaceDate" runat="server" AutoPostBack="True" Width="75px" OnTextChanged="txtbxRaceDate_OnTextChanged"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxRaceDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxRaceDate"
                                    EmptyValueMessage="Please enter correct date."
                                    InvalidValueMessage="Please enter correct date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="CardHandicap" />
                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td style="width: 100px">Center:(*)</td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpdwnCenterName" AutoPostBack="True" OnSelectedIndexChanged="drpdwnCenterName_SelectIndexChange">
                                    <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding-left: 50px;">Season:</td>
                            <td>
                                <b>
                                    <asp:Label ID="lblSeason" runat="server"></asp:Label></b>
                            </td>
                            <td style="padding-left: 50px;">Year:</td>
                            <td>
                                <b>
                                    <asp:Label ID="lblYear" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                    <div id="dvgridview" style="height: 260px; width: 99%; overflow: auto;" runat="server">
                        <asp:GridView ID="grdvwRaceDetail" runat="server" Width="100%"
                            AutoGenerateColumns="False" DataKeyNames="GeneralRaceNameID" EmptyDataText="No Detail Found."
                            OnSelectedIndexChanged="grdvwRaceDetail_OnSelectedIndexChanged" ShowHeaderWhenEmpty="true">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="SerialNumber" HeaderText="Pr.Sr.No" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Race Name" ItemStyle-Width="27%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("GENERALRACENAME") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceID" Value='<%# Bind("GENERALRACEID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldClassTypeID" Value='<%# Bind("ClassTypeID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldRaceType" Value='<%# Bind("RaceType") %>' />
                                        <asp:LinkButton Text='<%# Bind("GENERALRACENAME") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Distance" HeaderText="Distance" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="RaceType" HeaderText="Race Type" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Hd.Rt.Rg(Class)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldHandicapRatingRange" Value='<%# Bind("HandicapRatingRange") %>' />
                                        <asp:Label ID="lblratingrange" runat="server" Text='<%# Bind("HandicapRatingRange") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EligibleHandicapRatingRange" HeaderText="El.Hd.Rt.Rg(Class)" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="AgeCondition" HeaderText="Age Condition" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="RaceStatus" HeaderText="Race Status" ItemStyle-Width="15%" />

                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    <table border="1">
                        <tr>
                            <td>Handicap Date:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtboxHandicapDate" runat="server"></asp:TextBox>--%>
                                <asp:Label ID="lblHandicapDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="1">
                        <tr>
                            <td>Handicap Weight Raised or Lowered:
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkboxRaisedLowered" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Raised"></asp:ListItem>
                                    <asp:ListItem Text="Lowered"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>Handicap Weight Raised or Lowered Value:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtbxRaisedLoweredValue" runat="server" Width="50px" AutoPostBack="true" OnTextChanged="txtbxRaisedLoweredValue_OnTextChanged"></asp:TextBox>--%>
                                <asp:TextBox ID="txtbxRaisedLoweredValue" runat="server" Width="50px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ErrorMessage="Plese enter numeric value only" ControlToValidate="txtbxRaisedLoweredValue"
                                    ValidationExpression="^(\d+)?([.]?\d{0,1})?$" ValidationGroup="CardHandicap">*</asp:RegularExpressionValidator>
                                <asp:Button ID="btnAddWeight" runat="server" Text="Add Weight" OnClick="btnAddWeight_Click"/>
                                <asp:Button ID="btnShuffle" runat="server" Text="Update HWShuffle" OnClick="btnShuffle_Click"/>
                            </td>
                            <td>
                                Ban Update:
                            </td>
                            <td>
                                <asp:Button ID="btnBanUpdate" runat="server" Text="Ban Update" OnClientClick="OpenHorsePopup('BanName')"  />
                            </td>
                          
                        </tr>
                    </table>

                    <table border="1" style="text-decoration: solid">
                        <tr>
                            <td><b>Race Name:</b></td>
                            <td><b>
                                <asp:Label ID="lblRaceNameShow" runat="server"></asp:Label></b> </td>
                        </tr>
                        <tr>
                            <td><b>Handicap Record Table:</b></td>
                            <td><b>
                                <asp:Label ID="lblRecordTable" runat="server"></asp:Label></b> </td>
                        </tr>
                    </table>
                    <div id="dvgrdviewHorseDetail" style="height: 300px; width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="grdvwHorseDetail" runat="server" Width="99%" AutoGenerateColumns="false"
                            DataKeyNames="EntryID" EmptyDataText="No Entry Found"
                            OnSelectedIndexChanged="dvgrdviewHorseDetail_OnSelectedIndexChanged">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton Text='Update' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="HSerialNumber" HeaderText="Hr.Sr.No.(*)" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="HORSENAME" HeaderText="Horse(*) {Horse Ex Name & From Date}" ItemStyle-Width="50%" />
                                <asp:BoundField DataField="HorseAge" HeaderText="Age(*)" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HorseSex" HeaderText="Gender(*)" ItemStyle-Width="3%" />
                                <asp:TemplateField HeaderText="HR" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldhorseid" Value='<%# Bind("horseid") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldhorsenameid" Value='<%# Bind("horsenameid") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldhorsesexid" Value='<%# Bind("HorseSexID") %>' />
                                        <asp:TextBox ID="txtbxGvHandicapRating" Width="25px" runat="server" Text='<%# Bind("HandicapRatingRange") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MyHandicapRatingRange" HeaderText="MyHR" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HandicapWeight" HeaderText="HW" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="MyHandicapWeight" HeaderText="MyHW" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HandicapWeightAsPerGender" HeaderText="HWG" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HandicapWeightAsPerAgeCondition" HeaderText="HWAC" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="TotalHandicapWeightAsperGender" HeaderText="Total HWP" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="HWHWP" HeaderText="HW HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="MyHWHWP" HeaderText="MyHW HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HWGHWP" HeaderText="HWG HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HWACHWP" HeaderText="HWAC HWP" ItemStyle-Width="2%" />
                                
                                <asp:BoundField DataField="FHWAWRL" HeaderText="FHW AWRL" ItemStyle-Width="2%" />
                               <%-- <asp:TemplateField HeaderText="FHW AWRL" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFHWAWRL" runat="server" Text='<%# Bind("FHWAWRL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                
                                <asp:BoundField DataField="FMyHWAWRL" HeaderText="FMyHW AWRL" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="FHWGAWRL" HeaderText="FHWG AWRL" ItemStyle-Width="2%" />
                                
                                <asp:BoundField DataField="FHWACAWRL" HeaderText="FHWAC AWRL" ItemStyle-Width="2%" />
                                <%--<asp:TemplateField HeaderText="FHWAC AWRL" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFHWACAWRL" runat="server" Text='<%# Bind("FHWACAWRL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                
                                <asp:TemplateField HeaderText="HW GBC(*)" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxhandiapratinggivebyclub" runat="server" Width="25px" Text='<%# Bind("HWGBC") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Ban" ItemStyle-Width="12%" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" /></td>
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" /></td>
            <td>
                <asp:Button runat="server" ID="btnPdf" Text="PDF" /></td>
            <td>
                <asp:Button runat="server" ID="btnEdit" Text="Edit" /></td>

            <td>
                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnDelete" Text="Delete" /></td>
            <td>
                <asp:Button ID="btnImport" runat="server" Text="Import" />
                <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnImport"
                    CancelControlID="Button2" BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
            </td>
            <td>
                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
            <td>
                <input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>
            <td>
                <asp:Button runat="server" ID="btnHandicapShow" Text="Show Handicap" OnClick="btnHandicapShow_Click" /></td>
            <td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="display: none">
                        <table>
                            <tr>
                                <td>File Upload:</td>
                                <td>
                                    <asp:FileUpload ID="flupload" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="btnFileUpload_Click" /></td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" Text="Close" /></td>
                            </tr>
                        </table>

                    </asp:Panel>
    <div id="DvHandicap" style="height: 300px; width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="GvHandicap" runat="server" Width="99%" AutoGenerateColumns="false"
                            DataKeyNames="HandicapID" EmptyDataText="No Entry Found">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="HSerialNumber" HeaderText="Hr.Sr.No.(*)" ItemStyle-Width="3%" />
                                <asp:BoundField DataField="HORSENAME" HeaderText="Horse(*) {Horse Ex Name & From Date}" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="HorseAge" HeaderText="Age(*)" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HorseSex" HeaderText="Gender(*)" ItemStyle-Width="3%" />
                                <asp:BoundField DataField="HandicapRatingRange" HeaderText="HR" ItemStyle-Width="3%" />
                                <asp:BoundField DataField="MyHandicapRatingRange" HeaderText="MyHR" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HandicapWeight" HeaderText="HW" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="MyHandicapWeight" HeaderText="MyHW" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HandicapWeightAsPerGender" HeaderText="HWG" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HandicapWeightAsPerAgeCondition" HeaderText="HWAC" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="TotalHandicapWeightAsperGender" HeaderText="Total HWP" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="HWHWP" HeaderText="HW HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="MyHWHWP" HeaderText="MyHW HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HWGHWP" HeaderText="HWG HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HWACHWP" HeaderText="HWAC HWP" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="FHWAWRL" HeaderText="FHW AWRL" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="FMyHWAWRL" HeaderText="FMyHW AWRL" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="FHWGAWRL" HeaderText="FHWG AWRL" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="FHWACAWRL" HeaderText="FHWAC AWRL" ItemStyle-Width="2%" />
                               <asp:BoundField DataField="HdWghGBC" HeaderText="HW GBC(*)" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="BanDetail" HeaderText="Ban" ItemStyle-Width="7%" />
                                
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
