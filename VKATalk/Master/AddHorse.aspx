<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddHorse.aspx.cs" Inherits="VKATalk.Master.AddHorse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />

    <style type="text/css">
         
         .FixedHeader {
            position: absolute;
            font-weight: bold;
            
        } 

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

        function GetSireID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldSireNameID.ClientID %>').value = HdnKey;
        }


        function GetDamID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldDamNameID.ClientID %>').value = HdnKey;
        }

        function GetBirthStudID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldbirthstudNameID.ClientID %>').value = HdnKey;
        }

        function GetBreederID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnbreederNameID.ClientID %>').value = HdnKey;
        }

        function GetHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldHorseNameID.ClientID %>').value = HdnKey;
        }

        function GetFamilyHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldFamilyHorseid.ClientID %>').value = HdnKey;
        }


        function GetDivisionRaceNameID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldDivisionRaceNameAchivement.ClientID %>').value = HdnKey;
        }

        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Horse",
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
            var drpdwnvalue = document.getElementById('<%=hdnHorseNameId.ClientID %>').value;
            var horseName = document.getElementById('<%=hdnfdHorseDetail.ClientID %>').value;
            var horseDOB = document.getElementById('<%=hdnfdHorseDoB.ClientID %>').value;

            if (value === "HorseName") {
                window.open('../Popups/HorseName.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "HorseNewName") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseRename.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseStatus") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/AddHorseStatus.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "CurrentStatus") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/CurrentStatus.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "CurrentMission") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/CurrentMission.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseBan") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseBan.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseProfile") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseProfile.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HomeDistance") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HomeDistance.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MyHomeDistance") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/MyHomeDistance.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "ExpectedDistance") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/ExpectedDistance.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "FavDistanceGroup") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/FavourableDistanceGroup.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HomeClass") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HomeClass.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MyHomeClass") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/MyHomeClass.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "ExpectedClass") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/ExpectedClass.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "FavClassGroup") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/FavourableClassGroup.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "DistancePerformanceOld") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/DistancePerformanceOld.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "DistancePerformance") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/DistancePerformance.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }

            else if (value === "ClassPerformanceOld") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/ClassPerformanceOld.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "ClassPerformance") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/ClassPerformance.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Gender") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/Gender.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "StandingNation") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/StandingNation.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "BaseCenter") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseBaseCenter.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "StationCenter") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseRunningCenter.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "OwnerRecord") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/OwnerRecord.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "OwnerActual") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/OwnerActual.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "TrainerRecord") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/TrainerRecord.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "TrainerActual") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/TrainerActual.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "TargetRace") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/TargetRace.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "BodyWeight") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/BodyWeight.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Handicap") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HandicapRating.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MyHandicap") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/MyHandicapRating.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Shoe") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseShoe.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "ShoeDescription") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseShoeDescription.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseEquipment") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseEquipment.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB +'&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseBit") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseBit.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseTrackStar") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseTrackStar.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "DirectGate") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/DirectGate.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "SaddleNo") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseSaddleNo.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "LikingEnvironment") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseLikingEnviornment.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RunningStyle") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseRunningStyle.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseHabit") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseHabit.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "VeterinaryProblems") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseVeterinaryProblem.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "MyObservation") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                     window.open('../Popups/HorseMyObservations.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseBandage") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseBandage.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "OwnerStud") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/AddHorseOwnerStud.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "CurrentForm") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseCurrentForm.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseSwimming") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseSwimming.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "HorseTreadmill") {
                if (drpdwnvalue === "") {
                    alert("Please select Horse name.");
                } else {
                    window.open('../Popups/HorseTreadmill.aspx?HorseNameID=' + drpdwnvalue + '&HorseName=' + horseName + '&HorseDOB=' + horseDOB + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }

        }

        function parent_disable() {
            if (popupWindow && !popupWindow.closed)
                popupWindow.focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager2" runat="server"></asp:ScriptManager>
    <div style="padding-right:220px;">
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Horse</h1>
        </div>
    <div id="dialog" style="display: none">
    </div>

    <div style="text-align: left;">
        <asp:TabContainer ID="TabContainer1" runat="server" TabIndex="0" Width="100%" AutoPostBack="true" OnActiveTabChanged="Tabs_ActiveTabChanged">
            <asp:TabPanel ID="tbHorse" runat="server" HeaderText="Portfolio" Height="100px" ScrollBars="Both" Width="100%" TabIndex="1" >
                <ContentTemplate>
                    <table style="margin: 0 auto;" align="left">
                        <tr>
                            <td>
                                <%--<fieldset style="width: 100%" class="Userlogin">--%>
                                    <table>
                                        <tr>
                                            <td colspan="8">
                                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Horse" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana" Font-Size="12" />
                                                <asp:HiddenField runat="server" ID="hdnHorseNameId" />
                                                <asp:HiddenField runat="server" ID="hdnfdHorseDetail" />
                                                <asp:HiddenField runat="server" ID="hdnfdHorseDoB" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Horse:</td>
                                            <td colspan="5">
                                                <asp:Button runat="server" ID="btnFamilyTree" Text="FamilyTree" OnClick="btnFamilyTree_OnClick" />
                                                <asp:TextBox runat="server" ID="txtbxHorseName" Width="650px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldHorseNameID" />
                                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxHorseName" CompletionListElementID="listPlacement"
                                                        OnClientItemSelected="GetHorseID"
                                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_OnClick" />
                                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        </br>
                                        <tr>
                                            <td colspan="6">
                                               <span style="color:red;"><b>Not Done Stage-Information (Basic)</b></span> 
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Horse Name:(*) & New Name:(*)</td>
                                            <td colspan="2">
                                                <asp:Button ID="btnHorseName" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseName')" />
                                                <asp:Button ID="Button8" runat="server" Text="New Name" OnClientClick="OpenHorsePopup('HorseNewName')" />
                                                <span style="padding-left: 140px">
                                                    <asp:Label ID="lblHorseCurrentName" runat="server"></asp:Label><br />
                                                    
                                                </span>
                                            </td>
                                            <td colspan="5"></td>
                                        </tr>
                                        <tr>
                                            <td>Horse ID - NameID</td>
                                            <td colspan="6">
                                                <asp:Label ID="lblHorseIDNameID" runat="server"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <span style="padding-left: 300px">
                                                    <asp:Label ID="lblHorseExName" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Date of Birth:(*) (Age)</td>
                                            <td colspan="6">
                                                <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                               <span style="color:red;"><b>1st Stage-Information</b></span> 
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Date Of Birth Type(*) {Birth Date Type(*)}</td>
                                            <td colspan="6">
                                                <asp:RadioButtonList runat="server" ID="rdbtnDobType" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Actual" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Assumed" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <%--<asp:RequiredFieldValidator
                                                ID="ReqiredFieldValidator1"
                                                ValidationGroup="Horse"
                                                runat="server"
                                                ControlToValidate="rdbtnDobType"
                                                ErrorMessage="Please select Date of Birth type.">
                                            </asp:RequiredFieldValidator>--%>
                                        </tr>
                                        <tr>
                                            <td>Late Foal:</td>
                                            <td colspan="6">
                                                <asp:CheckBox ID="chkbxLateFoal" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Hope against Late Foal:</td>
                                            <td colspan="6">
                                                <asp:CheckBox ID="chkbxHopeLateFoal" runat="server" />
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Profile:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnProfile" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseProfile')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvProfile" style="width: 100%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvProfile" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Profile" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="10%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Permanent Status:(*)</td>
                                            <td>
                                                <asp:Button ID="btnStatus" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseStatus')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHorseStatus" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td>Current Status:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button2" runat="server" Text="Add" OnClientClick="OpenHorsePopup('CurrentStatus')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblCurrentStatus" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td>Color:(*)</td>
                                            <td colspan="6">
                                                <asp:DropDownList runat="server" ID="drpdwnColor" />
                                             <%--   <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic"
                                                    ValidationGroup="Horse" runat="server" ControlToValidate="drpdwnColor"
                                                    Text="*" ForeColor="Red" ErrorMessage="Please select Color."></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Gender:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button12" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Gender')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvGender" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvGender" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="HorseSex" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sire Name(*) with Sire Portfolio Status as Lable</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxSireName" runat="server" Width="650px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldSireNameID" />
                                                <div id="Div1" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddSireList"
                                                        MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                        CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxSireName" CompletionListElementID="Div1"
                                                        OnClientItemSelected="GetSireID"
                                                        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <%--<asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator2" Display="Dynamic"
                                                    ValidationGroup="Horse" runat="server" ControlToValidate="txtbxSireName"
                                                    Text="*" ForeColor="Red" ErrorMessage="Please select Sire Name."></asp:RequiredFieldValidator>--%>
                                                <span style="padding-left: 15px">
                                                    <asp:Label ID="lblSireNameStage" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Dam Name(*) with Sire Portfolio Status as Lable</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxDamName" runat="server" Width="650px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldDamNameID" />
                                                <div id="Div2" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddDamList"
                                                        MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                        CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxDamName" CompletionListElementID="Div2"
                                                        OnClientItemSelected="GetDamID"
                                                        ID="AutoCompleteExtender6" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic"
                                                    ValidationGroup="Horse" runat="server" ControlToValidate="txtbxDamName"
                                                    Text="*" ForeColor="Red" ErrorMessage="Please select Dam Name."></asp:RequiredFieldValidator>--%>
                                                <span style="padding-left: 15px">
                                                    <asp:Label ID="lblDamNameStage" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Got Abroad:</td>
                                            <td colspan="6">
                                                <asp:CheckBox ID="chkbxAbroad" runat="server" />
                                           
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Breeder:(*)</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxBreeder" runat="server" Width="500px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnbreederNameID" />
                                                <div id="Div4" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddBreederList"
                                                        MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                        CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxBreeder" CompletionListElementID="Div4"
                                                        OnClientItemSelected="GetBreederID"
                                                        ID="AutoCompleteExtender8" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <%--<asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator5" Display="Dynamic"
                                                    ValidationGroup="Horse" runat="server" ControlToValidate="txtbxBreeder"
                                                    Text="*" ForeColor="Red" ErrorMessage="Please enter Breeder."></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Birth Stud:(*)</td>
                                            <td colspan="6">
                                                <%-- <asp:DropDownList runat="server" id="drpdwnbirthStud" Width="280px"/>--%>
                                                <asp:TextBox ID="txtbxBirthStud" Width="455px" runat="server"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldbirthstudNameID" />
                                                <div id="Div3" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddBirthStudList"
                                                        MinimumPrefixLength="0" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                        CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxBirthStud" CompletionListElementID="Div3"
                                                        OnClientItemSelected="GetBirthStudID"
                                                        ID="AutoCompleteExtender7" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <%--<asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator4" Display="Dynamic"
                                                    ValidationGroup="Horse" runat="server" ControlToValidate="txtbxBirthStud"
                                                    Text="*" ForeColor="Red" ErrorMessage="Please enter Birth Stud."></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Owner Stud <span style="color:red"> (Entry must If Profile is Sire or Dam)</span>:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button18" runat="server" Text="Add" OnClientClick="OpenHorsePopup('OwnerStud')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvOwnerStud" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvOwnerStud" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="OwnerStud" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="OwnershipEngagement" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>Birth Nation:(*)</td>
                                            <td colspan="6">
                                                <asp:DropDownList runat="server" ID="drpdwnBirthNation" />
                                                <%--<asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic"
                                                    ValidationGroup="Horse" runat="server" ControlToValidate="drpdwnBirthNation"
                                                    Text="*" ForeColor="Red" ErrorMessage="Please select Birth Nation."></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Standing Nation:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button13" runat="server" Text="Add" OnClientClick="OpenHorsePopup('StandingNation')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblStandingNation" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="6">
                                               <span style="color:red;"><b>2nd Stage-Information</b></span> 
                                            </td>

                                        </tr>
                                         <tr>
                                            <td>Base Center:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button14" runat="server" Text="Add" OnClientClick="OpenHorsePopup('BaseCenter')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblBaseCenter" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td>Running Center:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button15" runat="server" Text="Add" OnClientClick="OpenHorsePopup('StationCenter')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvRunningCenter" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvRunningCenter" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CenterName" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="RunningCenter" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>Owner:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button16" runat="server" Text="Add" OnClientClick="OpenHorsePopup('OwnerRecord')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvOwnerRecord" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvOwnerRecord" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProfessionalName" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="ChangeStatus" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="ReasonOfChange" ItemStyle-Width="15%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Owner Color:</td>
                                            <td colspan="6">
                                                <asp:Label ID="lblOwnerColor" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Owner Hidden: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button23" runat="server" Text="Add" OnClientClick="OpenHorsePopup('OwnerActual')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvOwnerActual" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvOwnerActual" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProfessionalName" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="ChangeStatus" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="ReasonOfChange" ItemStyle-Width="15%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Trainer :(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button24" runat="server" Text="Add" OnClientClick="OpenHorsePopup('TrainerRecord')" />
                                               
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvtrainerrecord" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvTrainerRecord" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProfessionalName" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="ChangeStatus" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="ReasonOfChange" ItemStyle-Width="15%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Trainer Hidden: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button25" runat="server" Text="Add" OnClientClick="OpenHorsePopup('TrainerActual')" />
                                               
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvTrainerActual" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvTrainerActual" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProfessionalName" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="ChangeStatus" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="ReasonOfChange" ItemStyle-Width="15%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Home Distance:</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnHomeDistance" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HomeDistance')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHomeDistance" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>My Home Distance:</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnMyHomeDistance" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MyHomeDistance')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblMyHomeDistance" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Expected Distance:</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnExpectedDistance" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ExpectedDistance')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblExpectedDistance" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Distance Length:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button1" runat="server" Text="Add" OnClientClick="OpenHorsePopup('FavDistanceGroup')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblFavDistanceGroup" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                     <tr>
                                            <td>Distance Performance Old:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button48" runat="server" Text="Add" OnClientClick="OpenHorsePopup('DistancePerformanceOld')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvDistancePerformanceOld" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvDistancePerformanceOld" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PerformanceAddedTillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="IsShow" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="Distance" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="Performance" ItemStyle-Width="15%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Distance:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button3" runat="server" Text="Add" OnClientClick="OpenHorsePopup('DistancePerformance')" />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Home Class:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button4" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HomeClass')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHomeClass" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>My Home Class:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button5" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MyHomeClass')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblMyHomeClass" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Expected Class:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button6" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ExpectedClass')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblExpectedClass" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bunch:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button7" runat="server" Text="Add" OnClientClick="OpenHorsePopup('FavClassGroup')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblFavClassGroup" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bunch Group Performance Old:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button9" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ClassPerformanceOld')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvclssperold" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvClassPerformanceOld" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PerformanceAddedTillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="IsShow" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="BunchGroup" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="Performance" ItemStyle-Width="15%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bunch Group:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button10" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ClassPerformance')" />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Carry Top Weight:</td>
                                            <td colspan="6">
                                                <asp:CheckBox runat="server" ID="chkbxCarryTopWeight"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Classic Cantender:</td>
                                            <td colspan="6">
                                                <asp:CheckBox runat="server" ID="chkbxCantender"/>
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Proved Classic Material:</td>
                                            <td colspan="6">
                                                <asp:CheckBox runat="server" ID="chkbxProvedClassicMaterial"/>
                                             
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Under Valued Horse:</td>
                                            <td colspan="6">
                                                <asp:CheckBox runat="server" ID="chkbxUndervalueHorse"/>
                                                
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Lineage, <span style="color:red;">Profile is Sire or Dam, then ONLY</span>:</td>
                                            <td colspan="6">
                                                <asp:TextBox runat="server" ID="txtbxLineage" Width="500px"></asp:TextBox>
                                                <div id="dvOverflowGlobal" style="height: 100px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ServiceMethod="AddLineageList"
                                                    MinimumPrefixLength="0" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="txtbxLineage" CompletionListElementID="dvOverflowGlobal"
                                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                               <span style="color:red;"><b>3rd Stage-Information</b></span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Handicap Rating:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button28" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Handicap')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHandicap" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>MyHandicap Rating:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button29" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MyHandicap')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblMyHandicap" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Body Weight:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button27" runat="server" Text="Add" OnClientClick="OpenHorsePopup('BodyWeight')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblBodyWeight" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Bandage:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button17" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseBandage')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHorseBandage" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Veterinary Problems:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button40" runat="server" Text="Add" OnClientClick="OpenHorsePopup('VeterinaryProblems')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvVietProblem" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvVietProblem" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Disease" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="DaysUP" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TreatmentStartDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TreatmentEndDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="DaysUT" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ban:</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnHorseBan" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseBan')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblBan" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Habit:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button39" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseHabit')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvHabit" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvHabit" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="HabitType" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="Habit" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="Details" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Liking Environment:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button37" runat="server" Text="Add" OnClientClick="OpenHorsePopup('LikingEnvironment')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblLiking" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Rainy Day Performer:</td>
                                            <td colspan="6">
                                                <asp:CheckBox runat="server" ID="chkbxRainyDayPerformer"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Whip Must Required:</td>
                                            <td colspan="6">
                                                 <asp:CheckBox runat="server" ID="chkbxWhipMustRequired"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Running Style:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button38" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RunningStyle')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblRunningStyle" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Current Form:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button19" runat="server" Text="Add" OnClientClick="OpenHorsePopup('CurrentForm')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblCurrentForm" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Current Mission:</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnCurrentMission" runat="server" Text="Add" OnClientClick="OpenHorsePopup('CurrentMission')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblCurrentMission" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Shoe:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button30" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Shoe')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblShoe" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Shoe Metal:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button31" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ShoeDescription')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblShoeDescription" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Equipment:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button32" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseEquipment')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvEquipment" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvEquiupment" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Equipment" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bit:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button33" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseBit')" />
                                               
                                            </td>
                                        </tr>
                                       <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvBit" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvBit" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Bit" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                               <span style="color:red;"><b>4th Stage-Information</b></span> 
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Dosage Index (DI) - (Speed Rating):</td>
                                            <td colspan="6">
                                                <asp:TextBox runat="server" ID="txtbxDosageIndex"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Dosage Profile (DP) - (Stamina Rating):</td>
                                            <td colspan="6">
                                                <asp:TextBox runat="server" ID="txtbxDosageProfile"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Center Of Distribution (CD) - (Combined Influence Rating):</td>
                                            <td colspan="6">
                                                <asp:TextBox runat="server" ID="txtbxCenterofDistribution"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Physic:</td>
                                            <td colspan="6">
                                                <asp:RadioButtonList ID="rdbtnPhysicType" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Single Bone" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Double Bone" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                        
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Body Shape:</td>
                                            <td colspan="6">
                                                <asp:RadioButtonList ID="rdbtnBodyShape" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Pony" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Pony Medioker" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Medioker" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Perfect" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Exellent" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Giant" Value="6"></asp:ListItem>
                                                </asp:RadioButtonList>
                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Birth Defect:</td>
                                            <td colspan="6">
                                                <asp:TextBox runat="server" ID="txtbxBirthDefect" Width="800px"></asp:TextBox>
                                                <div id="Div7" style="height: 100px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ServiceMethod="BirthDefectList"
                                                    MinimumPrefixLength="0" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="txtbxBirthDefect" CompletionListElementID="Div7"
                                                    ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Saddle No.:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button36" runat="server" Text="Add" OnClientClick="OpenHorsePopup('SaddleNo')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblSaddleNo" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Track Star:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button34" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseTrackStar')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblTrackStar" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Direct Gate:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button35" runat="server" Text="Add" OnClientClick="OpenHorsePopup('DirectGate')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblDirectGate" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Target Race:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button26" runat="server" Text="Add" OnClientClick="OpenHorsePopup('TargetRace')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvTargetRace" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvTargetRace" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="GeneralRaceName" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="CenterName" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="SeasonName" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="YearName" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                               <span style="color:red;"><b>5th Stage-Information</b></span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Date Of Death:</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxDateofDeath" runat="server" Width="70px"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                                <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxDateofDeath" Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxDateofDeath"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Total Earned Stake Money:</td>
                                            <td colspan="6">
                                                <asp:Label runat="server" ID="Label7"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>My Observations:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button41" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MyObservation')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvObservation" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvObservation" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="AimedDuration" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="MyObservation" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Portfolio Complete:(*)</td>
                                            <td colspan="6">
                                                <asp:RadioButtonList runat="server" ID="rdbtnProfile" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Not Done" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Stage 1" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Stage 2" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Stage 3" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Stage 4" Value="5"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Swimming:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button11" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseSwimming')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="Div6" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GdviewHorseSwimming" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SwimmingDate" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="SwimmingRounds" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Treadmill:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button20" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HorseTreadmill')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="Div8" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GdvTreadmill" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="TreadmillDate" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="ValueA" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="ValueB" ItemStyle-Width="5%" />
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
                                                Total Horse Portfolio Created: <asp:Label ID="lblHorsePortfolio" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="BtnAddClick" ValidationGroup="Horse" /></td>
                                            <td>
                                                <asp:Button ID="btnHorseShow" runat="server" Text="Show" OnClick="btnHorseShow_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnPdf" runat="server" Text="PDF" /></td>
                                            <td>
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                                            <td>
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="Horse" /></td>
                                            <td>
                                                <asp:Button ID="btnImport" runat="server" Text="Import" />
                                                  <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                                    CancelControlID="Button2" BackgroundCssClass="Background">
                                                </asp:ModalPopupExtender>

                                            </td>
                                            <td>
                                                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnExportToday" runat="server" Text="Export Today" OnClick="btnExportToday_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
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
                                                <asp:Button ID="Button47" runat="server" Text="Close" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--</fieldset>--%>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbFamily" runat="server" HeaderText="Family" Height="500px" Width="100%" TabIndex="2" >
                <ContentTemplate>
                    <table style="margin: 0 auto;" align="left">
                        <tr>
                            <td>
                                <fieldset style="width: 100%" class="Userlogin">
                                    <table>
                                        <tr>
                                            <td colspan="8">
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="TabpanelHorseFamily" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana" Font-Size="12" />
                                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                                <asp:HiddenField runat="server" ID="HiddenField2" />
                                                <asp:HiddenField runat="server" ID="HiddenField3" />

                                            </td>
                                        </tr>
                                         <tr>
                                            <td>Horse:</td>
                                            <td colspan="5">
                                                <asp:TextBox runat="server" ID="txtbxHorseNameFamily" Width="700px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldFamilyHorseid" />
                                                <div id="divhorseDetailFamilyl" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddFamilyHorseList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxHorseNameFamily" CompletionListElementID="divhorseDetailFamilyl"
                                                        OnClientItemSelected="GetFamilyHorseID"
                                                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Dam Name:
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblDamName" runat="server"></asp:Label>
                                            </td>

                                        </tr>
                                       <tr>
                                            <td>Age Condition:</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="drpdwnFAgeCondition" runat="server"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Gender:</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="drpdwnFGender" runat="server"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                       <tr>
                                            <td>Base Center:</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="drpdwnFBaseCenter" runat="server"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                         <tr>
                                            <td>Profile:</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="drpdwnFProfile" runat="server"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total Family Member:</td>
                                            <td colspan="5">
                                                <asp:Label ID="lbltotalfamilyMember" runat="server"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        
                                    </table>
                                   
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    
                    <div id="dvHorseFamily" style="height: 220px; width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="GvHorseFamily" runat="server" Width="400%"
                            AutoGenerateColumns="False" DataKeyNames="HorseNameID"
                             ShowHeader="true" OnRowDataBound="GvHorseFamily_RowDataBound">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField  ItemStyle-Width="10%" DataField="HorseName" HeaderText="Horse Name" />
                                <asp:BoundField DataField="DOB" HeaderText="DOB" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="A C G" HeaderText="A C G" ItemStyle-Width="2%" />
                                <asp:BoundField  DataField="SireName" HeaderText="Sire Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Relation" HeaderText="Relation" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="Profile" HeaderText="Profile" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Nation" HeaderText="Nation" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="BaseCenter" HeaderText="BaseCenter" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="RunningCenter" HeaderText="RunningCenter" ItemStyle-Width="2%" />
                                <asp:BoundField  DataField="Owner" HeaderText="Owner" ItemStyle-Width="10%" />
                                <asp:BoundField  DataField="Trainer" HeaderText="Trainer" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Carrier Statistics" HeaderText="Carrier Statistics" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Distance" HeaderText="HD/MHD/ED" ItemStyle-Width="10%" />
                                <%--<asp:TemplateField HeaderText="HD/MHD/ED" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:label ID="lblDistance" runat="server" Width="25px" Text='<%# Bind("Distance") %>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField  DataField="Distance Performance" HeaderText="Distance Performance" ItemStyle-Width="10%" />
                                <asp:BoundField  DataField="Class" HeaderText="HC/MHC/EC" ItemStyle-Width="10%" />
                                <asp:BoundField  DataField="Bunch Group Performance" HeaderText="Bunch Group Performance" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Achievements" HeaderText="Achievements" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="StakeEarned" HeaderText="StakeEarned" ItemStyle-Width="5%" />
                               
                            </Columns>
                            <%--<HeaderStyle CssClass="FixedHeader" Height="20px" />--%>
                            <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    
                </ContentTemplate>
            </asp:TabPanel>
              <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Runs" Height="500px" Width="100%">
            </asp:TabPanel>
             <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Performances" Height="500px" Width="100%">
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanelAchivements" runat="server" HeaderText="Achivements" TabIndex="4" Height="750px" Width="100%">
                <ContentTemplate>
                    <table style="margin: 0 auto;" align="left">
                        <tr>
                            <td>
                                    <table>
                                        <tr>
                                            <td colspan="8">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="HorseAchievments" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana" Font-Size="12" />
                                                <asp:HiddenField runat="server" ID="hdnAchieveHorseId" />
                                               <%-- <asp:HiddenField runat="server" ID="hdnAchieveHorseNameId" />--%>
                                                <asp:HiddenField runat="server" ID="hdnfdAchieveHorseDetail" />
                                                <asp:HiddenField runat="server" ID="hdnfdAchieveHorseDoB" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Horse:</td>
                                            <td colspan="5">
                                                <asp:TextBox runat="server" ID="txtbxAchieveHorseName" Width="700px"></asp:TextBox>
                                                <div id="Div5" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxAchieveHorseName" CompletionListElementID="Div5"
                                                        ID="AutoCompleteExtender9" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <%--<asp:Button runat="server" ID="btnAchieveShow" Text="Show" OnClick="btnAchieveShow_OnClick" />
                                                <asp:Button runat="server" ID="btnAchieveClear" Text="Clear" OnClick="btnAchieveClear_OnClick" />--%>
                                            </td>
                                            <td></td>
                                        </tr>
                                     
                                        <tr>
                                            <td>Position:(*)</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxPosition" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please enter Position." ValidationGroup="HorseAchievments" ControlToValidate="txtbxPosition">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Race Name:(*)</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxRaceName" runat="server"  Width="1000px"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceNameAchivement" />
                                                <div id="divRaceNameAchivement" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="GetDivisionRaceName"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                        CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxRaceName" CompletionListElementID="divRaceNameAchivement"
                                                        OnClientItemSelected="GetDivisionRaceNameID"
                                                        ID="AutoCompleteExtender10" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please enter Race Name." 
                                                    ValidationGroup="HorseAchievments" ControlToValidate="txtbxRaceName">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                    </table>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAcheiveAdd" runat="server" Text="Add" OnClick="btnAcheiveAdd_Click" ValidationGroup="HorseAchievments" /></td>
                                            <td>
                                            <asp:Button runat="server" ID="btnAchieveClear" Text="Clear" OnClick="btnAchieveClear_OnClick" /></td>
                                            <td>
                                                <asp:Button ID="Button21" runat="server" Text="PDF" Enabled="false"/></td>
                                            <td>
                                                <asp:Button ID="Button42" runat="server" Text="Delete" OnClick="btnDeleteAchivement_Click" ValidationGroup="HorseAchievments" /></td>
                                            <td>
                                                <asp:Button ID="Button43" runat="server" Text="Import" Enabled="false"  /></td>
                                            <td>
                                                <asp:Button ID="Button44" runat="server" Text="Export" OnClick="btnExportAchivement_Click" Enabled="false" /></td>
                                            <td>
                                                <asp:Button ID="Button46" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                    </table>
                    <div id="dvHorseAchivements" style="height: 150px; width: 100%; overflow: auto;" runat="server">
                        <asp:GridView ID="GvHorseAchivements" runat="server" Width="100%"
                            AutoGenerateColumns="False" DataKeyNames="AchivementsID" OnSelectedIndexChanged="GvHorseAchivements_OnSelectedIndexChanged">
                            <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Horse Name" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameAchivement" Value='<%# Bind("HorseName") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldHorseNameIDAchievment" Value='<%# Bind("HorseNameID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnfieldDivisionRaceIDAchvment" Value='<%# Bind("DivisionRaceID") %>' />
                                        <asp:LinkButton Text='<%# Bind("HorseName") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Position" HeaderText="Position" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DivisionRaceName" HeaderText="Race Name" ItemStyle-Width="25%" />
                            </Columns>
                            <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                            <PagerStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbHCP" runat="server" HeaderText="Lineage Performance" Height="500px" Width="100%">
            </asp:TabPanel>

        </asp:TabContainer>

    </div>
</asp:Content>
