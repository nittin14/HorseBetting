
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceAllReportDetails.aspx.cs" Inherits="VKATalk.Card.RaceAllReportDetails" %>

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
                    title: "Race Card",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function GetOwnerID(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender3", "hdnfieldEmergencyColorID");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function GetJockeyID(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender4", "hdnfieldProfessionalnameid1");
                $get(hiddenfieldID).value = eventArgs.get_value();
            }
        }

        function OpenHorsePopup(value) {
            if (value === "RaceGuide") {

                var parm = document.getElementById('<%= drpdwnCenterName.ClientID %>');
                var value = parm.options[parm.selectedIndex].text;
                window.open('../Reports/RaceGuide.aspx?RaceDate=' + document.getElementById('<%= txtbxDivisionRaceDate.ClientID %>').value + '&CenterID=' + document.getElementById('<%= drpdwnCenterName.ClientID %>').value
                    + '&CenterName=' + value
                    + '&Season=' + document.getElementById('<%= lblSeason.ClientID %>').innerText
                    + '&Year=' + document.getElementById('<%= lblYear.ClientID %>').innerText
                    , '_blank', 'status=yes, menubar=no, location=center');
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Master Hyperlink Sheet</h1>
    <div id="dialog" style="display: none">
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="RaceGuide" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 25px;">Race Date:(*)</td>
                            <td style="width: 300px">
                                <asp:TextBox ID="txtbxDivisionRaceDate" runat="server" AutoPostBack="True" Width="75px" OnTextChanged="txtbxDivisionRaceDate_OnTextChanged"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton6" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxDivisionRaceDate"
                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator6" runat="server"
                                    ControlExtender="mskDateAvailable"
                                    ControlToValidate="txtbxDivisionRaceDate"
                                    EmptyValueMessage="Please enter correct date."
                                    InvalidValueMessage="Please enter correct date."
                                    Display="Dynamic"
                                    IsValidEmpty="true"
                                    InvalidValueBlurredMessage="*"
                                    ValidationExpression="^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$"
                                    ValidationGroup="RaceGuide" />
                                <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton6" runat="server" TargetControlID="txtbxDivisionRaceDate"
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
                    <br />
                    <br />

                  <table style="border:double; border-color:black; width:1250px;">
                     
                      <tr>
                          <td>
                              <asp:LinkButton runat="server" ID="LinkButton1" Text="Entry" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                       <tr>
                          <td>
                               <asp:LinkButton runat="server" ID="LinkButton2" Text="Handicap" OnClientClick="OpenHorsePopup('RaceGuide')" />
                              
                        </td>
                    </tr>
                       <tr>
                          <td>
                             <asp:LinkButton runat="server" ID="LinkButton3" Text="Acceptance" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                       <tr>
                          <td>
                              <asp:LinkButton runat="server" ID="LinkButton4" Text="Declaration" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                       <tr>
                          <td>
                               <asp:LinkButton runat="server" ID="LinkButton5" Text="Race Card" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                       <tr>
                          <td>
                             <asp:LinkButton runat="server" ID="LinkButton6" Text="Result" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                             <tr>
                          <td>
                               <asp:LinkButton runat="server" ID="LinkButton8" Text="Data Descripency" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                       <tr>
                          <td>
                             <asp:LinkButton runat="server" ID="LinkButton7" Text="Race Guide" OnClientClick="OpenHorsePopup('RaceGuide')" />
                        </td>
                    </tr>
                              
                              
                              
                             
                              
                            
                              
                              
                          
                  </table>
               
            </td>
        </tr>
    </table>

    
</asp:Content>
