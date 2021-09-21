<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardWorkout.aspx.cs" Inherits="VKATalk.Card.CardWorkout" %>

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
                    title: "Card Workout",
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
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Card Workout</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="CardWorkout" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField ID="hdnfieldGeneralRaceNameID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td >Division Race Date:(*)</td>
                            <td style="width: 150px">
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
                                    ValidationGroup="CardWorkout" />
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
                   
                    <table>
                         <tr>
                            <td>Horse Name:</td>
                            <td><asp:DropDownList ID="drpdwnHorseName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpdwnHorseName_SelectIndexChange">
                                <asp:ListItem Text="--Please select--" Value="-1"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Source:</td>
                            <td><asp:DropDownList ID="drpdwnSourceName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpdwnSourceName_SelectIndexChange"></asp:DropDownList> </td>
                        </tr>
                       
                    </table>

                    <div id="DvAcceptanceShow" style="height: 300px; width: 99%; overflow: auto;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <asp:GridView ID="GvShowALL" runat="server" Width="100%" AutoGenerateColumns="false"
                    EmptyDataText="No Record Found">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="TrackDate" ReadOnly="true" HeaderText="TrackDate" ItemStyle-Width="9%" />
                        <asp:BoundField DataField="SR" ReadOnly="true" HeaderText="Sr" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="CenterName" ReadOnly="true" HeaderText="Cn" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="WorkoutType" ReadOnly="true" HeaderText="WT" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="AGHR" ReadOnly="true" HeaderText="A G HR" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="DRHRHName" ReadOnly="true" HeaderText="Horse Name (DRNo-HNo)" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Rider" ReadOnly="true" HeaderText="Rider" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="DrawNo" ReadOnly="true" HeaderText="Draw" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="MRCarriedWeight" ReadOnly="true" HeaderText="CW" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="DistanceBreakUp" ReadOnly="true" HeaderText="Distance Break up" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="TimeTaken" ReadOnly="true" HeaderText="Time Taken" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="TrackAlias" ReadOnly="true" HeaderText="Track" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="VerdictMarginAlias" ReadOnly="true" HeaderText="Verdict Margin" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="CommonComment" ReadOnly="true" HeaderText="Common Comment" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="IndividualHorseComment" ReadOnly="true" HeaderText="Individual Horse Comment" ItemStyle-Width="15%" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvShowALL" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
                </fieldset>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
