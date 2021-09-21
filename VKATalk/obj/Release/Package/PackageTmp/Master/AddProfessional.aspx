<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProfessional.aspx.cs" Inherits="VKATalk.Master.AddProfessional" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    title: "Professional",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function ShowPopupCurrentMission(message) {
            $(function () {
                $("#dialogCurrentMission").html(message);
                $("#dialogCurrentMission").dialog({
                    title: "Current Mission",
                    closeOnEscape: false,
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
            var drpdwnvalue = document.getElementById('<%=hdnProfessionalId.ClientID %>').value;
            var professionalNameID = document.getElementById('<%=hdnProfessionalId.ClientID %>').value;
            var professionalName = document.getElementById('<%=hdnfdProfessionalDetail.ClientID %>').value;
            var regnum = document.getElementById('<%=hdnfieldRegNum.ClientID %>').value;
            if (value == "ProfessionalName") {
                window.open('../Popups/ProfessionalName.aspx?ProfessionalValue=' + drpdwnvalue + '&RegNum=' + regnum + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value == "ProfessionalNewName") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalNewName.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Profile") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalProfile.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "BaseCenter") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalBaseCenter.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "OwnerColor") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalOwnerColor.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Status") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalStatus.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "CurrentStatus") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalCurrentStatus.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "AprenticeOf") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalJockeyAprenticeOf.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "BodyWeight") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalJockeyWeight.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "OtherName") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalOtherName.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "AssistantOf") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalTrainerAssistant.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Partner") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalPartners.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Relation") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalRelation.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "HomeDistance") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalHomeDistance.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "FavDistanceGroup") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalFavDistanceGroup.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "HomeClass") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalHomeClass.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "FavClassGroup") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalFavHomeClass.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Habits") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalHabit.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "ImportantDates") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalImportantDates.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "MyObservations") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalMyObservation.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Religion") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalReligion.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "AntiPerson") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalAntiPerson.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "ProfessionalBackground") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalBackground.aspx?ProfessionalValue=' + drpdwnvalue + '&ProfessionalName=' + professionalName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "JockeyAllowanceStage") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalJockeyAllowanceStage.aspx?ProfessionalNameID=' + drpdwnvalue , '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value == "Penalty") {
                if (drpdwnvalue == "") {
                    alert("Please select Professional name.");
                } else {
                    window.open('../Popups/ProfessionalPenalty.aspx?ProfessionalNameID=' + drpdwnvalue , '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Professional</h1>
        </div>
    <div id="dialog" style="display: none">
    </div>

    <div style="text-align: left;">
        
         <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100% ">
            <asp:TabPanel ID="tbHorse" runat="server" HeaderText="Portfolio" Height="500px" ScrollBars="Auto" Width="100%">
                <ContentTemplate>
                    <table style="margin: 0 auto;" align="left">
                        <tr>
                            <td>
                                <%--<fieldset style="width: 100%" class="Userlogin">--%>
                                    <table>
                                        <tr>
                                            <td colspan="8">
                                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="Professional" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana" Font-Size="12" />
                                                <asp:HiddenField runat="server" ID="hdnProfessionalId" />
                                                <asp:HiddenField runat="server" ID="hdnfdProfessionalDetail" />
                                                <asp:HiddenField runat="server" ID="hdnfdProfessionalDoB" />
                                                <asp:HiddenField runat="server" ID="hdnfieldRegNum" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Professional:</td>
                                            <td colspan="5">
                                                <asp:TextBox runat="server" ID="txtbxProfessionalName" Width="700px"></asp:TextBox>
                                                <div id="listPlacement" style="height: 300px; overflow-y: scroll;">
                                                    <asp:AutoCompleteExtender ServiceMethod="AddProfessionalList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="500" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxProfessionalName" CompletionListElementID="listPlacement"
                                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_OnClick" />
                                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td><span style="color: red;">Basic Information - {Comman For All}</span></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>Professional Name:(*) & New Name:(*)</td>
                                            <td colspan="1">
                                                <asp:Button ID="btnProfessionalName" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ProfessionalName')" />
                                                <asp:Button ID="Button8" runat="server" Text="New Name" OnClientClick="OpenHorsePopup('ProfessionalNewName')" />
                                               
                                            </td>
                                            <td colspan="5"></td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                             <td colspan="6">
                                                 <asp:Label ID="lblProfessionalCurrentName" runat="server"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                             <td colspan="6">
                                                <asp:Label ID="lblProfessionalExName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>Professional ID - NameID:</td>
                                            <td colspan="6">
                                                <asp:Label ID="lblProfessionalIDNameID" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Registration Number:</td>
                                            <td colspan="6">
                                                <asp:Label ID="lblRegistrationNumber" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Profile:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="btnProfile" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Profile')" />
                                                <%-- <span style="padding-left: 250px">
                                                    <asp:Label ID="lblProfessionalProfile" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvProfile" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvProfile" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProfileType" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Base Center:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button14" runat="server" Text="Add" OnClientClick="OpenHorsePopup('BaseCenter')" />
                                                <%-- <span style="padding-left: 250px">
                                                    <asp:Label ID="lblBaseCenter" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="divCenter" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvCenter" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CenterName" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Date Of Birth (Age):</td>
                                            <td colspan="6">
                                                <%--<asp:Label ID="lblDOB" runat="server"></asp:Label>--%>
                                                <asp:TextBox ID="txtbxdob" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender2" CultureName="en-GB" runat="server" TargetControlID="txtbxdob"
                                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                                <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                    ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtbxdob"
                                                    EmptyValueMessage="Please enter date."
                                                    InvalidValueMessage="Invalid date format."
                                                    Display="Dynamic"
                                                    IsValidEmpty="true"
                                                    InvalidValueBlurredMessage="*"
                                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                                                    ValidationGroup="Professional" />
                                                <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" runat="server" TargetControlID="txtbxdob"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Professional Type (*)</td>
                                            <td colspan="6">
                                                <asp:DropDownList runat="server" ID="drpdwnProfessionalType" />
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Permanent Status: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button19" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Status')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>Current Status: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button20" runat="server" Text="Add" OnClientClick="OpenHorsePopup('CurrentStatus')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblCurrentStatus" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span style="color: red;">Required Information if Profile is Jockey</span></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>Jockey's Master Trainer: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button3" runat="server" Text="Add" OnClientClick="OpenHorsePopup('AprenticeOf')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblAprenticeOf" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td>Other Name:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button2" runat="server" Text="Add" OnClientClick="OpenHorsePopup('OtherName')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblOtherName" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Jockey Weight: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button1" runat="server" Text="Add" OnClientClick="OpenHorsePopup('BodyWeight')" />
                                                <%--<span style="padding-left: 250px">
                                                    <asp:Label ID="lblBodyWeight" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvJockeyWeight" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvJockeyWeight" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="JockeyWeightType" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="BodyWeight" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="OverWeight" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td><span style="color: red;">Required Information if Profile is Trainer</span></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>Trainer's Master Trainer: (*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button4" runat="server" Text="Add" OnClientClick="OpenHorsePopup('AssistantOf')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblAssistanceOf" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td>Tr Licence Date:</td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtbxtrLicenseDate" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxtrLicenseDate"
                                                    Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtbxtrLicenseDate"
                                                    EmptyValueMessage="Please enter date."
                                                    InvalidValueMessage="Invalid date format."
                                                    Display="Dynamic"
                                                    IsValidEmpty="true"
                                                    InvalidValueBlurredMessage="*"
                                                    ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                                                    ValidationGroup="Professional" />
                                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxtrLicenseDate"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                                            </td>
                                        </tr>--%>
                                       <%-- <tr>
                                            <td>Body Weight:</td>
                                            <td colspan="6">
                                                <asp:TextBox runat="server" ID="txtbxbodyWeight"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td><span style="color: red;">Required Information if Profile is Owner</span></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>Partners:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button5" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Partner')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvPartner" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvPartner" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="PartnershipPercentage" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                      <%--  <tr>
                                            <td>Partnership Percentage:</td>
                                            <td colspan="6">
                                                <asp:Label ID="lblpartnershipercentage" runat="server"></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>Owner Color:(*)</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button18" runat="server" Text="Add" OnClientClick="OpenHorsePopup('OwnerColor')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblOwnerColor" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span style="color: red;">Advance Information - {Comman For All}</span></td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>Religion:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button16" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Religion')" />
                                                <span style="padding-left: 250px">
                                                    <asp:Label ID="lblReligion" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Relations:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button6" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Relation')" />
                                                <%--<span style="padding-left: 250px">
                                                    <asp:Label ID="lblRelation" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvRelation" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvRelation" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="35%" />
                                                            <asp:BoundField DataField="RelationType" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="Relation" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                          <%--  <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="RelationBreak" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="MyComments" ItemStyle-Width="15%" />--%>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Anti Person:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button17" runat="server" Text="Add" OnClientClick="OpenHorsePopup('AntiPerson')" />
                                                <%--<span style="padding-left: 250px">
                                                    <asp:Label ID="lblAntiPerson" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvAntiPerson" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvAntiPerson" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Profession:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button21" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ProfessionalBackground')" />
                                                <%-- <span style="padding-left: 250px">
                                                    <asp:Label ID="lblProfessionalBackground" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvProfessionalBackground" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvProfessionalBackground" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProfessionalBackground" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Important Dates:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button13" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ImportantDates')" />
                                               <%-- <span style="padding-left: 250px">
                                                    <asp:Label ID="lblImportantDates" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvImportantDates" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvImportantDates" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ImportantDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="Occassion" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="RelatedTo" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="RelatedToName" ItemStyle-Width="10%" />
                                                            
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Character:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button12" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Habits')" />
                                                <%-- <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHabit" runat="server"></asp:Label>
                                                </span>--%>
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
                                                            <asp:BoundField DataField="Habit" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="HabitDetails" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Home Distance:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button7" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HomeDistance')" />
                                                <%--  <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHomeDistance" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvHomeDistance" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvHomeDistance" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Distance" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="RatingMarkFormatStyle" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Home Distance Length:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button9" runat="server" Text="Add" OnClientClick="OpenHorsePopup('FavDistanceGroup')" />
                                                <%--<span style="padding-left: 250px">
                                                    <asp:Label ID="lblFavDistanceGroup" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvFavDistanceGroup" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvFavDistanceGroup" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="DistanceGroupType" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="RatingMarkFormatStyle" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Home Bunch:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button10" runat="server" Text="Add" OnClientClick="OpenHorsePopup('HomeClass')" />
                                                <%-- <span style="padding-left: 250px">
                                                    <asp:Label ID="lblHomeClass" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvHomeClass" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvHomeClass" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ClassType" ItemStyle-Width="50%" />
                                                            <asp:BoundField DataField="RatingMarkFormatStyle" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Home Bunch Group:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button11" runat="server" Text="Add" OnClientClick="OpenHorsePopup('FavClassGroup')" />
                                                <%--  <span style="padding-left: 250px">
                                                    <asp:Label ID="lblFavClassGroup" runat="server"></asp:Label>
                                                </span>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="DvFavClassGroup" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvFavClassGroup" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ClassGroupType" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="RatingMarkFormatStyle" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>My Observations:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button15" runat="server" Text="Add" OnClientClick="OpenHorsePopup('MyObservations')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvMyObservation" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvMyObservation" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="MyObservation" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="MyObservationInDetails" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Jockey Allowance Stage:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button23" runat="server" Text="Add" OnClientClick="OpenHorsePopup('JockeyAllowanceStage')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvJockeyAllowanceStage" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvJockeyAllowanceStage" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="JockeyAllowanceStage" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="StageStartDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="StartDRNo" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="EndDRNo" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="StageEndDate" ItemStyle-Width="5%" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Penalty:</td>
                                            <td colspan="6">
                                                <asp:Button ID="Button24" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Penalty')" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td colspan="6">
                                                <div id="dvpenalty" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                                    <asp:GridView ID="GvPenalty" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                                            HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Penalty" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="PenaltyReason" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="WorkOnApeal" ItemStyle-Width="5%" />
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
                                                Total Professional Portfolio Created: <asp:Label ID="lblProfCount" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="BtnAddClick" ValidationGroup="Professional" /></td>
                                            <td>
                                                <asp:Button ID="btnHorseShow" runat="server" Text="Show" OnClick="btnHorseShow_Click" ValidationGroup="Professional" /></td>
                                            <td>
                                                <asp:Button ID="btnPdf" runat="server" Text="PDF" /></td>
                                            <td>
                                            <td>
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" ValidationGroup="Professional" /></td>
                                            <td>
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  /></td>
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
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" OnClientClick="" /></td>
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
                                                <asp:Button ID="Button22" runat="server" Text="Close" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--</fieldset>--%>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbHDPOld" runat="server" HeaderText="Horses In Holding" Height="500px" Width="100%">
            </asp:TabPanel>
            <asp:TabPanel ID="tbHDP" runat="server" HeaderText="Horses in Past Holding" Height="500px" Width="100%">
            </asp:TabPanel>
            <asp:TabPanel ID="tbHCPOld" runat="server" HeaderText="Achivements" Height="500px" Width="100%">
            </asp:TabPanel>
        </asp:TabContainer>

        <%--
           File upload is not working in Tab Panel
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100% ">
            <asp:TabPanel ID="tbHorse" runat="server" HeaderText="Profile" Height="500px" ScrollBars="Auto" Width="100%">
                <ContentTemplate>
                    
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbHDPOld" runat="server" HeaderText="Old Performance" Height="500px" Width="100%">
            </asp:TabPanel>
            <asp:TabPanel ID="tbHDP" runat="server" HeaderText="Performances" Height="500px" Width="100%">
            </asp:TabPanel>
            <asp:TabPanel ID="tbHCPOld" runat="server" HeaderText="Achivements" Height="500px" Width="100%">
            </asp:TabPanel>
            <asp:TabPanel ID="tbHCP" runat="server" HeaderText="Lineage Performances" Height="500px" Width="100%">
            </asp:TabPanel>
        </asp:TabContainer>--%>
        
    </div>
    <script type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self"); /* url = "" or "about:blank"; target="_self" */
            win.close();
        }
    </script>
</asp:Content>
