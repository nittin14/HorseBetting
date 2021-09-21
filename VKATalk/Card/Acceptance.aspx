<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Acceptance.aspx.cs" Inherits="VKATalk.Card.Acceptance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <style type="text/css">
        div.c {
            text-decoration-line: underline;
            text-decoration-style: double;
        }

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

        .modalBackground {
            background-color: #333333;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
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
                    title: "Acceptance",
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
            var generalracenameid = document.getElementById('<%=hdnfieldGeneralRaceNameID.ClientID %>').value;
            if (value === "Veterinery") {
                window.open('../Popups/HorseVeterinaryProblem.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "Equipment") {
                window.open('../Popups/HorseEquipment.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "OwnerRecord") {
                window.open('../Popups/OwnerRecord.aspx?HorseNameID=0&HorseName=NULL&HorseDOB=00-00-0000&PageName=2&GeneralRaceNameID=' + generalracenameid, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
        }

        function GetHorseID(source, eventArgs) {
            alert("1");
            alert(source);
            alert(eventArgs.get_value());
            if (source) {
                // Get the HiddenField ID.
                alert("3");
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender4", "hdnfieldHorseNameidG");
                alert("4");
                alert(hiddenfieldID);
                $get(hiddenfieldID).value = eventArgs.get_value();
                      alert("5");
                alert(hiddenfieldID.value);
            }
            alert("2");

        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Acceptance</h1>
    <%--  <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline; text-decoration-style:double">Acceptance</h1>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Acceptance</h1>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; border: 2px solid red; padding:10px; border-radius: 25px;">Acceptance</h1>
    <span style="text-align: center; font-size: xx-large; font-weight: bold; border: 2px solid red; padding:10px; border-radius: 25px;">Acceptance</span>
   <span style="text-decoration: underline;font-size:xx-large; font-weight: bold;"> <span style="text-align: center; font-size: xx-large; font-weight: bold; border: 2px solid red; padding:10px; border-radius: 25px;">Acceptance</span></span>--%>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="10">
                            <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardAcceptance" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                Font-Size="12" />
                            <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                        </td>
                    </tr>
                </table>
                <table>

                    <tr>
                        <td>Acceptance Enter Date:(*)</td>
                        <td>
                            <asp:TextBox ID="txtbxHandicapEnterDate" runat="server" Width="75px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                ControlExtender="MaskedEditExtender1"
                                ControlToValidate="txtbxHandicapEnterDate"
                                EmptyValueMessage="Please enter correct Handicap date."
                                InvalidValueMessage="Please enter correct Handicap date."
                                Display="Dynamic"
                                IsValidEmpty="true"
                                InvalidValueBlurredMessage="*"
                                ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                ValidationGroup="CardAcceptance" />
                            <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxHandicapEnterDate"
                                Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                        </td>
                        <td style="width: 50px;"></td>
                        <td>General Race Date:(*)</td>
                        <td>
                            <asp:TextBox ID="txtbxRaceDate" runat="server" AutoPostBack="True" Width="75px" OnTextChanged="txtbxRaceDate_OnTextChanged"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                            <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxRaceDate"
                                Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                ControlExtender="mskDateAvailable"
                                ControlToValidate="txtbxRaceDate"
                                EmptyValueMessage="Please enter correct date."
                                InvalidValueMessage="Please enter correct date."
                                Display="Dynamic"
                                IsValidEmpty="true"
                                InvalidValueBlurredMessage="*"
                                ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                ValidationGroup="CardAcceptance" />
                            <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxRaceDate"
                                Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                        </td>
                        <td style="width: 50px;"></td>
                        <td>Center:(*)</td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpdwnCenterName" AutoPostBack="True" OnSelectedIndexChanged="drpdwnCenterName_SelectIndexChange">
                                <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 50px;"></td>
                        <td>Season:</td>
                        <td>
                            <b>
                                <asp:Label ID="lblSeason" runat="server"></asp:Label></b>
                        </td>
                        <td style="width: 50px;"></td>
                        <td>Year:</td>
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
                        <emptydatarowstyle font-names="Calibri" font-size="Medium" forecolor="Red"
                            horizontalalign="Center" />
                        <columns>
                                <asp:BoundField DataField="SerialNumber" HeaderText="PSNo" ItemStyle-Width="2%">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="General Race Name(*)" ItemStyle-Width="23%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("GENERALRACENAME") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceID" Value='<%# Bind("GeneralRaceID") %>' />
                                        <asp:LinkButton Text='<%# Bind("GENERALRACENAME") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="PermanentCondition" HeaderText="Pamanent Condition" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="SeasonalCondition" HeaderText="Seasonal Condition" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="RaceCardCondition" HeaderText="Race Card Condition" ItemStyle-Width="20%" />
                            </columns>
                        <pagerstyle horizontalalign="Left" />
                    </asp:GridView>
                </div>

                <table border="1">
                    <tr>
                        <td style="width: 250px">Acceptance: 
                        </td>
                        <td>
                            <asp:Label ID="lblEntryDate" runat="server"></asp:Label>
                        </td>

                    </tr>
                </table>

                <table border="1">
                    <tr>
                        <td>General Race Name:
                        </td>
                        <td>
                            <asp:Label ID="lblGeneralRaceName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnVeterinery" runat="server" Text="Veterinery Update" OnClientClick="OpenHorsePopup('Veterinery')" />
                        </td>
                        <td>
                            <asp:Button ID="btnEquipment" runat="server" Text="Equipment Update" OnClientClick="OpenHorsePopup('Equipment')" />
                        </td>
                        <td>
                            <asp:Button ID="btnOwnerRecord" runat="server" Text="Owner Record Update" OnClientClick="OpenHorsePopup('OwnerRecord')" />
                        </td>
                    </tr>
                </table>

                <div id="dvgrdviewHorseDetail" style="height: 300px; width: 99%; overflow: auto;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                        <contenttemplate>
                        <asp:GridView ID="grdvwHorseDetail" runat="server" Width="100%" AutoGenerateColumns="false"
                            DataKeyNames="AcceptanceID" EmptyDataText="No Entry Found" OnRowDataBound="grdvwHorseDetail_RowDataBound">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                               
                                <asp:BoundField DataField="HrSrNo" HeaderText="HNo(*)" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="HORSENAME" HeaderText="Horse Name(*)" ItemStyle-Width="20%" />
                                <asp:TemplateField HeaderText="Bifurcation(*)" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldAcceptanceID" Value='<%# Bind("AcceptanceID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldhorseid" Value='<%# Bind("horseid") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldhorsenameid" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:DropDownList runat="server" ID="drpdwnBifurcation" AutoPostBack="true" OnSelectedIndexChanged="drpdwnBifurcation_SelectIndexChange">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HNo(**)" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxHno" runat="server" Width="65px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="HandicapRating" HeaderText="HR" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="HdWghGBC" HeaderText="HW GBC" ItemStyle-Width="5%" />
                                 <asp:TemplateField HeaderText="AWAWRL" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:label ID="lblAWAWRL" runat="server" Width="25px"></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AW GBC(**)" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxAWGBC" runat="server" Width="65px" Text='<%# Bind("HdWghGBC") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Struck Out Type(**)  " ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="drpdwnStruckOutType">
                                            <asp:ListItem Selected="True" Text="Scratch"></asp:ListItem>
                                            <asp:ListItem Text="Ballot Out"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                 </contenttemplate>
                        <triggers>
                <asp:AsyncPostBackTrigger ControlID="GvShowALL" />
            </triggers>
                    </asp:UpdatePanel>
                </div>

            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CardAcceptance" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnShow" Text="Show" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnPdf" Text="PDF" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnEdit" Text="Edit" />
            </td>

            <td>
                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnDelete" Text="Delete" />
            </td>
            <td>
                <asp:Button ID="btnImport" runat="server" Text="Import" />
                <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                    CancelControlID="Button2" BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
            </td>
            <td>
                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
            </td>
            <td>
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
            </td>
            <%--<td>
                <asp:Button runat="server" ID="btnHandicapShow" Text="Show Handicap"/></td>
            <td>--%>
        </tr>
    </table>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
        <table>
            <tr>
                <td>File Upload:</td>
                <td>
                    <asp:FileUpload ID="flupload" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnFileUpload" runat="server" Text="Upload" OnClick="btnFileUpload_Click" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Close" />
                </td>
            </tr>
        </table>

    </asp:Panel>
    <table>
        <tr>
            <td>Race Name:</td>
            <td>
                <asp:DropDownList runat="server" ID="drpdwnRaceNameS"></asp:DropDownList></td>
            <td>Display:</td>
            <td>
                <asp:DropDownList runat="server" ID="drpdwnDisplay" AutoPostBack="true" OnSelectedIndexChanged="drpdwnDisplay_SelectIndexChange">
                    <asp:ListItem Selected="True" Text="---ALL---" Value="1"></asp:ListItem>
                    <asp:ListItem Text="---Struck Out---" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Without Division" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Div. I" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Div. II" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Div. III" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Div. IV" Value="6"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
    </table>
    <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <contenttemplate>
                <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="GlobalID" EmptyDataText="No Record Found"
                    AllowPaging="false" OnPageIndexChanging="OnPaging" OnRowEditing="RowEdit"
                    OnRowUpdating="RowUpdate" OnRowCancelingEdit="CancelEdit" OnRowDataBound="GvShowALL_RowDataBound"
                    PageSize="10">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                         <asp:TemplateField HeaderText="General Race Name" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnfieldGeneralRacenameid" Value='<%# Eval("GeneralRaceNameID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceid" Value='<%# Eval("GeneralRaceID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldGeneralRaceDate" Value='<%# Eval("GeneralRaceDate") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceID" Value='<%# Eval("DivisionRaceID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldAcceptanceID" Value='<%# Eval("AcceptanceID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldAcceptanceStruckOutID" Value='<%# Bind("AcceptanceStruckOutID") %>' />
                                <asp:HiddenField runat="server" ID="hdnfieldHorseID_FK" Value='<%# Bind("HorseID_FK") %>' />
                                <asp:Label ID="lblGeneralRaceName" runat="server" Text='<%# Eval("GeneralRaceName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bifurcation(*)" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblDivisionDetailS" runat="server" Text='<%# Eval("DivisionRaceName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="drpdwnBifurcationS" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="150px" HeaderText="HNo">
                            <ItemTemplate>
                                <asp:Label ID="lblHNoS" runat="server"
                                    Text='<%# Eval("HNo")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHNoS" runat="server"
                                    Text='<%# Eval("HNo")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HORSENAME" ReadOnly="true" HeaderText="Horse Name" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="AcceptanceWeightAWRL" ReadOnly="true" HeaderText="AW AWRL" ItemStyle-Width="5%" />
                        <asp:TemplateField ItemStyle-Width="150px" HeaderText="AW GBC">
                            <ItemTemplate>
                                <asp:Label ID="lblAWGBCS" runat="server"
                                    Text='<%# Eval("AcceptanceWeightGBC")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAWGBCS" runat="server"
                                    Text='<%# Eval("AcceptanceWeightGBC")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="150px" HeaderText="Struck Out Type">
                            <ItemTemplate>
                                <asp:Label ID="lblStruckOutTypeS" runat="server"
                                    Text='<%# Eval("StruckOutType")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="drpdwnStruckOuttypeS">
                                    <asp:ListItem Selected="True" Text="---Please select--"></asp:ListItem>
                                    <asp:ListItem Text="Scratch"></asp:ListItem>
                                    <asp:ListItem Text="Ballot Out"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </contenttemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="GvShowALL" />
            </triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
