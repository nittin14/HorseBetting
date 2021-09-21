<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProspectus.aspx.cs" Inherits="VKATalk.Master.AddProspectus" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Add Master Prospectus</title>
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Master Race",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };


        var popupWindow = null;

        function OpenHorsePopup(value) {
            var RaceID = document.getElementById('<%=hdnfieldprospectusid.ClientID %>').value;
            var RaceName = document.getElementById('<%=hdnfieldRaceName.ClientID %>').value;
            var CenterName = document.getElementById('<%=lblCenterName.ClientID %>').innerText;
            if (value === "ProspectusName") {
                window.open('../Popups/ProspectusMasterRaceName.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "ProspectusNameNew") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterRaceNewName.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RaceInMemoryOf") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusRaceMemoryOf.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "SponceroftheRace") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusSponcer.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MomenttoPresenter") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMomenttoPresenter.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Distance") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasDistance.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RaceType") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterRaceType.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName + '&CenterName=' + CenterName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HandicapRatingRange") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterHanRatingRange.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "EligbleHandicapRatingRange") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterEligbleHandRatingRange.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Agecondition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterAgeCondition.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RaceStatus") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterRaceStatus.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Million") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterMillion.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "masterbunch") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterBunch.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "SweepStake") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterSweepStake.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Classic") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterClassic.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Graded") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterGraded.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "GradedNo") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterGradedNo.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MomenttoType") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterMomenttoType.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MomenttoCost") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterMomenttoCost.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "ProfessionalBackground") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterInterestedProfessionalBackground.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HWPCondition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterHWPCondition.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "BunchCondition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterBunchCondition.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "PermanentCondition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterPermanentCondition.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "OtherCondition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterOtherCondition.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HandicapWeight") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterHandicapWeight.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HandicapWeightAsPerAge") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterHandicapWeightAsPerAge.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RaceHistory") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterRaceHistory.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "StakeMoneyAddition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterStakeMoneyAddition.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Abbriviation") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusMasterRaceAbbriviation.aspx?ProspectusId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
        }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <div style="padding-right:220px;">
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Prospectus Master</h1>
        </div>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="8">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="prospectus" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField runat="server" ID="hdnfieldprospectusid" />
                                <asp:HiddenField runat="server" ID="hdnfieldRaceName" />
                            </td>
                        </tr>

                        <tr>
                            <td>Master Race:(*)</td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="txtbxProspectusName" Width="700px"></asp:TextBox>
                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                    <asp:AutoCompleteExtender ServiceMethod="AddProspectusList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxProspectusName" CompletionListElementID="listPlacement"
                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_OnClick" />
                                <asp:Button runat="server" ID="Button1" Text="Clear" OnClick="btnClear_OnClick" />
                            </td>
                            <td></td>
                        </tr>
                        <tr></tr>
                        <tr>
                            <td>Master Race Name:(*) & New Name(*)</td>
                            <td colspan="6">
                                <asp:Button ID="btnProspectusName" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ProspectusName')" />
                                <asp:Button ID="Button8" runat="server" Text="New Name" OnClientClick="OpenHorsePopup('ProspectusNameNew')" />


                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvGdvwMasterRaceName" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvMasterRaceName" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Master Race Name Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="MasterRaceName" HeaderText="Master Race Name" ItemStyle-Width="80%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                         <tr>
                            <td>Master Race ID - Name ID:(*)</td>
                            <td colspan="6">

                                <span style="padding-left: 80px">
                                    <asp:Label ID="lblMasterRaceNameID" runat="server"></asp:Label>
                                </span>
                            </td>

                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td colspan="6">

                                <span style="padding-left: 80px">
                                    <asp:Label ID="lblCenterName" runat="server"></asp:Label>
                                </span>
                            </td>

                        </tr>
                        <tr>
                            <td>Season:(*)</td>
                            <td colspan="6">

                                <span style="padding-left: 80px">
                                    <asp:Label ID="lblSeasonName" runat="server"></asp:Label>
                                </span>
                            </td>

                        </tr>
                        <tr>
                            <td>Race In Memory:</td>
                            <td colspan="6">
                                <asp:Button ID="btnRaceInMemoryOf" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RaceInMemoryOf')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblRaceInMemoryOf" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Sponcer of the Race:</td>
                            <td colspan="6">
                                <asp:Button ID="Button2" runat="server" Text="Add" OnClientClick="OpenHorsePopup('SponceroftheRace')" />
                                <%-- <span style="padding-left: 30px">
                                    <asp:Label ID="lblSponceroftheRace" runat="server"></asp:Label>
                                </span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvsponcer" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvSponcer" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="70%" />
                                            <asp:BoundField DataField="Profile" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Momentto:</td>
                            <td colspan="6">
                                <asp:Button ID="Button16" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MomenttoType')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblMomenttoType" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>Momentto Cost:</td>
                            <td colspan="6">
                                <asp:Button ID="Button17" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MomenttoCost')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblMomenttoCost" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>Momentto Presenter:</td>
                            <td colspan="6">
                                <asp:Button ID="Button3" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MomenttoPresenter')" />
                                <%--<span style="padding-left: 30px">
                                    <asp:Label ID="lblMomenttoPresenter" runat="server"></asp:Label>
                                </span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvmomenttopresenter" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvMomenttopresenter" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="80%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <%--<asp:BoundField DataField="Presenter" ItemStyle-Width="10%" />--%>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Interested Profession:</td>
                            <td colspan="6">
                                <asp:Button ID="Button19" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ProfessionalBackground')" />
                                <%--<span style="padding-left: 30px">
                                    <asp:Label ID="lblProfessionalBackground" runat="server"></asp:Label>
                                </span>--%>
                            </td>
                        </tr>
                        
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="divInterestedprofessionBackground" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvInterestedProfessionBackground" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="ProfessionalBackground" ItemStyle-Width="80%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillYearProfessionalBackground" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Distance:(*)</td>
                            <td colspan="6">
                                <asp:Button ID="Button4" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Distance')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblDistance" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>RaceType:(*)</td>
                            <td colspan="6">
                                <asp:Button ID="Button5" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RaceType')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblRaceType" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Handicap Rating Range (Class):</td>
                            <td colspan="6">
                                <asp:Button ID="Button6" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HandicapRatingRange')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblHandicapRatingRange" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Eligble Handicap Rating Range (Class):</td>
                            <td colspan="6">
                                <asp:Button ID="Button7" runat="server" Text="Add" OnClientClick="OpenHorsePopup('EligbleHandicapRatingRange')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblEligbleHandicapRatingRange" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>

                        <tr>
                            <td>Bunch Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button21" runat="server" Text="Add" OnClientClick="OpenHorsePopup('BunchCondition')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblBunchCondition" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Maiden Horse Term:</td>
                            <td colspan="6">
                                <asp:CheckBox ID="chkbxMaidenTerm" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Age Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button9" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Agecondition')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblAgeCondition" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Permanent Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button22" runat="server" Text="Add" OnClientClick="OpenHorsePopup('PermanentCondition')" />
                               <%-- <span style="padding-left: 30px">
                                    <asp:Label ID="lblPermanentCondition" runat="server"></asp:Label>
                                </span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvPermanentCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvPermanentCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PermanentCondition" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="FromYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                         <tr>
                            <td>Other Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button26" runat="server" Text="Add" OnClientClick="OpenHorsePopup('OtherCondition')" />
                               <%-- <span style="padding-left: 30px">
                                    <asp:Label ID="lblOtherCondition" runat="server"></asp:Label>
                                </span>--%>
                            </td>
                        </tr>
                         <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvotherCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvOtherCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="OtherCondition" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="FromYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Handicap Weight As Per Gender:</td>
                            <td colspan="6">
                                <asp:Button ID="Button23" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HandicapWeight')" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvhandicapweightaspergender" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvHandicapWeightAsperGender" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="HorseSex" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="HandicapWeight" ItemStyle-Width="70%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="HWAPGender" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Handicap Weight As Per Age Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button24" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HandicapWeightAsPerAge')" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvWeightAsPerAge" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvWeightAsPerAge" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="HorseSex" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="AgeCondition" ItemStyle-Width="50%" />
                                            <asp:BoundField DataField="HandicapWeight" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="FromYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Race Status:(*)</td>
                            <td colspan="6">
                                <asp:Button ID="Button10" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RaceStatus')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblRaceStatus" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Million:</td>
                            <td colspan="6">
                                <asp:Button ID="Button11" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Million')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblMillion" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Sweep Stake:</td>
                            <td colspan="6">
                                <asp:Button ID="Button12" runat="server" Text="Add" OnClientClick="OpenHorsePopup('SweepStake')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblSweepStake" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Classic:</td>
                            <td colspan="6">
                                <asp:Button ID="Button13" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Classic')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblClassic" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Grade:</td>
                            <td colspan="6">
                                <asp:Button ID="Button14" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Graded')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblGraded" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Grade No:</td>
                            <td colspan="6">
                                <asp:Button ID="Button15" runat="server" Text="Add" OnClientClick="OpenHorsePopup('GradedNo')" />
                                <span style="padding-left: 30px">
                                    <asp:Label ID="lblGradedNo" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>HWP Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button20" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HWPCondition')" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvHWPCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GVHWPCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="SrNo" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="PartNo" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="SecNo" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="Condition" ItemStyle-Width="200" ItemStyle-Wrap="true" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="HWPConditioin" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>HWP Value:</td>
                            <td colspan="6">
                               
                            </td>
                        </tr>
                        <tr>
                            <td>Race Abbriviation:</td>
                            <td colspan="6">
                                <asp:Button ID="Button28" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Abbriviation')" />
                                <%--<asp:DropDownList ID="drpdwnAbbriviation" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic"
                                    ValidationGroup="prospectus" runat="server" ControlToValidate="drpdwnAbbriviation"
                                    Text="*" ErrorMessage="Please select Race Abbriviation"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                         <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvGvRaceAbbriviation" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvRaceAbbriviation" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="RaceAbbreviation" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="FromYear" ItemStyle-Width="5%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Stake Money Addition:</td>
                            <td colspan="6">
                                <asp:Button ID="Button27" runat="server" Text="Add" OnClientClick="OpenHorsePopup('StakeMoneyAddition')" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvStakeMoneyAddition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvStakeMoneyAddition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="StakeMoneyAddition" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="Amount" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="FromYear" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="5%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        
                        <tr>
                            <td>Race History:</td>
                            <td colspan="6">
                                <asp:Button ID="Button25" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RaceHistory')" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvRaceHistory" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvRaceHistory" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="RaceHistory" ItemStyle-Width="50%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Bunch:</td>
                            <td colspan="6">
                                <asp:button id="button17" runat="server" Text="Add" OnClientClick="OpenHorsePopup('masterbunch')" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="div1" style="width: 80%; overflow: auto;" runat="server">
                                    <asp:gridview id="gdvwmasterbunch" runat="server" width="100%"
                                        autogeneratecolumns="false" showheader="false" emptydatatext="no record found">
                                        <emptydatarowstyle font-names="calibri" font-size="medium" forecolor="red"
                                            horizontalalign="center" />
                                        <columns>
                                            <asp:boundfield datafield="bunch" itemstyle-width="80%" />
                                            <asp:boundfield datafield="tillyear" itemstyle-width="20%" />
                                        </columns>
                                        <pagerstyle horizontalalign="left" />
                                    </asp:gridview>
                                </div>
                            </td>

                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                Total Master Race Created: <asp:Label ID="lblGeneralRacecount" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="BtnAddClick" ValidationGroup="prospectus" /></td>
                            <td>
                                <asp:Button ID="btnFooterShow" runat="server" Text="Show" ValidationGroup="prospectus" /></td>
                            <td>
                                <asp:Button ID="btnPdf" runat="server" Text="PDF" /></td>
                            <td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" /></td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
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
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
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
                    <asp:Button ID="Button18" runat="server" Text="Close" /></td>
            </tr>
        </table>

    </asp:Panel>
    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
