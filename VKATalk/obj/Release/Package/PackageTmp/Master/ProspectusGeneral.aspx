<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProspectusGeneral.aspx.cs" Inherits="VKATalk.Master.ProspectusGeneral" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Add Master Prospectus</title>
    
    <script src="../Scripts/jquery1.7.2.min.js"></script>
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />



    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Prospectus General",
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
            var generalracename = document.getElementById('<%=txtbxGeneralRaceName.ClientID %>').value;
            var RaceID = document.getElementById('<%=hdnfieldprospectusid.ClientID %>').value;
            var RaceName = document.getElementById('<%=hdnfieldRaceName.ClientID %>').value;
            if (value === "ProspectusName") {
                window.open('../Popups/ProspectusGeneralRaceName.aspx?ProspectusGeneralId=' + RaceID + '&RaceName=' + RaceName, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
            }
            else if (value === "ProspectusNameNew") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusGeneralRaceNewName.aspx?ProspectusGeneralId=' + RaceID, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "Observation") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusGeneralObservation.aspx?ProspectusGeneralId=' + RaceID + '&GeneralRaceName=' + generalracename, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RaceDate") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusGeneralDates.aspx?ProspectusGeneralId=' + RaceID + '&GeneralRaceName=' + generalracename, '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "SeasonalCondition") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusGeneralSeasonalCondition.aspx?ProspectusGeneralId=' + RaceID + '&GeneralRaceName=' + generalracename + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
                }
            }
            else if (value === "RaceCard") {
                if (RaceID === "") {
                    alert("Please select Prospectus name.");
                } else {
                    window.open('../Popups/ProspectusGeneralRaceCardCondition.aspx?ProspectusGeneralId=' + RaceID + '&GeneralRaceName=' + generalracename + '&PageName=1&GeneralRaceNameID=0', '_blank', 'status=yes, toolbar=no, menubar=no, location=center');
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
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Prospectus General</h1></div>
    <div id="dialog" style="display: none">
    </div>
    <table align="center">
        <tr>
            <td>
                <fieldset style="width: 100%;" class="Userlogin">
                    <table>
                        <tr>
                            <td colspan="8">
                                <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="prospectusGeneral" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                                    Font-Size="12" />
                                <asp:HiddenField runat="server" ID="hdnfieldprospectusid" />
                                <asp:HiddenField runat="server" ID="hdnfieldpropectusgeneralracenameid" />
                                <asp:HiddenField runat="server" ID="hdnfieldRaceName" />
                            </td>
                        </tr>
                      
                        <tr>
                            <td>General Name:(*)</td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="txtbxGeneralRaceName" Width="700px"></asp:TextBox>
                                <div id="Div1" style="height: 300px; overflow-y: scroll;">
                                     <asp:AutoCompleteExtender ServiceMethod="AddProspectusGeneralList"
                                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                        CompletionInterval="500" EnableCaching="false" CompletionSetCount="100"
                                                        TargetControlID="txtbxGeneralRaceName" CompletionListElementID="Div1"
                                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>

                                </div>
                                <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_OnClick" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" />
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td>General Race Name:(*) & New Name(*)</td>
                            <td colspan="2">
                                <asp:Button ID="btnProspectusName" runat="server" Text="Add" OnClientClick="OpenHorsePopup('ProspectusName')" />
                                <asp:Button ID="Button8" runat="server" Text="New Name" OnClientClick="OpenHorsePopup('ProspectusNameNew')" />
                                <span style="padding-left: 140px">
                                    <asp:Label ID="lblProspectusCurrentName" runat="server"></asp:Label><br />

                                </span>
                            </td>
                            <td colspan="5"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <span style="padding-left: 300px">
                                    <asp:Label ID="lblProspectusExName" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                          <tr>
                            <td>General Race ID - Name ID:</td>
                            <td colspan="6">
                                <asp:Label ID="lblGeneralRaceIDNameID" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>Center:(*)</td>
                            <td colspan="6">
                                <asp:Label ID="lblCenterName" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>Season:(*)</td>
                            <td colspan="6">
                                <asp:Label ID="lblSeasonName" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>Year:(*)</td>
                            <td colspan="6">
                                <asp:Label ID="lblYearName" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>Race Date:(*)</td>
                            <td colspan="6">
                                <asp:Label ID="lblRaceDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Week Day:</td>
                            <td colspan="6">
                                 <asp:Label ID="lblWeekDay" runat="server"></asp:Label>
                               <%-- <span style="padding-left: 250px">
                                   
                                </span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>Race Day:(*)</td>
                            <td>
                                <asp:TextBox ID="txtbxRaceDay" runat="server"></asp:TextBox>
                                <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddRaceDayList"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxRaceDay" CompletionListElementID="Div6"
                                    ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter race day"  
                                    ValidationGroup="prospectusGeneral" ControlToValidate="txtbxRaceDay">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Time Slot of Race:(*)</td>
                            <td>
                                <asp:RadioButtonList runat="server" ID="rdbtnTimeSlot" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Morning" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="After Noon" Selected="True" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Evening" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>Main Race of The Day:</td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkbxMainRaceofDay" />
                            </td>
                        </tr>
                        <tr>
                            <td>Prospectus Serial Number:(*)</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtbxSerialNumber"></asp:TextBox>
                                <div id="Div7" style="height:300px; overflow-y:scroll;" ></div>
                                <asp:AutoCompleteExtender ServiceMethod="AddSerialNoList"
                                    MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtbxSerialNumber" CompletionListElementID="Div7"
                                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter serial number" 
                                     ValidationGroup="prospectusGeneral" ControlToValidate="txtbxSerialNumber">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="prospectusGeneral"
                                    ControlToValidate="txtbxSerialNumber" Text="*" ErrorMessage="Only numeric in serial number."  Display="Dynamic" 
                                    ForeColor="Red" ValidationExpression="^[0-9]+" > </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Season Race Number:</td>
                            <td colspan="6">
                                <asp:Label ID="lblSeasonRaceNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Day Race Number:</td>
                            <td colspan="6">
                                <asp:Label ID="lblDayRaceNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Race In Memory:</td>
                            <td colspan="6">
                                <asp:Label ID="lblRaceInMemoryOf" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Sponcer of the Race:</td>
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
                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="80%" />
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
                                 <asp:Label ID="lblMomenttoType" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>Momentto Presenter:</td>
                            <td colspan="6">
                              

                            </td>
                        </tr>
                         <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvMomenttoPresenter" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvMomenttoPresenter" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PROFESSIONALNAME" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Interested Profession:</td>
                            <td colspan="6">
                                <%--<span style="padding-left: 250px">
                                    <asp:Label ID="lblpresenterbackground" runat="server"></asp:Label>
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
                          
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvdistance" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvDistance" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="Distance" ItemStyle-Width="80%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>RaceType:(*)</td>
                            <td colspan="6">
                            </td>
                        </tr>
                      <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvracetype" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="gvracetype" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="RaceType" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="Category" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Handicap Rating Range(Class):</td>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvhandicapratingrange" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="gvhandicapratingrange" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="HandicapRatingRange" ItemStyle-Width="50%" />
                                            <asp:BoundField DataField="CLASSTYPE" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Eligble Handicap Rating Range (Class):</td>
                            <td colspan="6">
                               
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvEligiblehandicapratingrange" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="gveligiblehandicapratingrange" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="HandicapRatingRange" ItemStyle-Width="50%" />
                                            <%--<asp:BoundField DataField="CLASSTYPE" ItemStyle-Width="20%" />--%>
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillDate" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                         
                        <tr>
                            <td>Bunch Condition:</td>
                            <td colspan="6">
                                <%--<span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblbunchcondition" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>Maiden Horse Term:</td>
                            <td colspan="6">
                                <asp:Label ID="lblMaidenHorseTerm" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Age Condition:</td>
                            <td colspan="6">
                              <%--  <span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblAgeCondition" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Birth Year Condition:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtbxYearofBirth"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Permanent Condition:</td>
                           <%-- <td colspan="6">
                                <asp:Label ID="lblPermanentCondition" runat="server"></asp:Label>
                            </td>--%>
                        </tr>
                         <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvPermanent" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvPermanentCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PermanentCondition" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Other Condition:</td>
                         <%--   <td colspan="6">
                                <asp:Label ID="lblOtherCondition" runat="server"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvOtherCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvOtherCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="OtherCondition" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Seasonal Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="btnSeasonalCondition" runat="server" Text="Add" OnClientClick="OpenHorsePopup('SeasonalCondition')" />
                               <%-- <span style="padding-left: 30px">
                                    
                                </span>--%>
                                <%--<asp:Label ID="lblSeasonalCondition" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvSeasonalCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvSeasonalCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="SeasonalCondition" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                          <tr>
                            <td>Race Card Condition:</td>
                            <td colspan="6">
                                <asp:Button ID="btnRaceCard" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RaceCard')" />
                               <%-- <span style="padding-left: 30px">
                                    
                                </span>--%>
                               <%-- <asp:Label ID="lblRaceCard" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                         <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="DvRaceCardCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvRaceCardCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="RaceCardCondition" ItemStyle-Width="90%" />
                                            <asp:BoundField DataField="FromDate" ItemStyle-Width="10%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Handicap Weight As Per Gender:</td>
                            <td colspan="6"></td>
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
                                            <asp:BoundField DataField="HorseSex" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="HandicapWeight" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="HWAPGender" ItemStyle-Width="20%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Handicap Weight As Per Age Condition:</td>
                            <td colspan="6"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6">
                                <div id="dvAgeCondition" style="width: 80%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvAgeCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="AgeCondition" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="HandicapWeight" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="YEARNAME" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="TillYear" ItemStyle-Width="20%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Race Status:(*)</td>
                            <td colspan="6">
                               <%-- <span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblRaceStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Million:</td>
                            <td colspan="6">
                                <%--<span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblMillion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Sweep Stake:</td>
                            <td colspan="6">
                              <%--  <span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblSweepStake" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Classic:</td>
                            <td colspan="6">
                               <%-- <span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblClassic" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Grade:</td>
                            <td colspan="6">
                                <%--<span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblGraded" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>Grade No:</td>
                            <td colspan="6">
                                <%--<span style="padding-left: 250px">
                                    
                                </span>--%>
                                <asp:Label ID="lblGradedNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>HWP Condition:</td>
                            <td colspan="6"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6" style="width: 80%; table-layout:fixed;">
                                <div id="dvHWPCondition" style="width: 100%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GVHWPCondition" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="SrNo" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="PartNo" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="SecNo" ItemStyle-Width="10%" />
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
                            <td>Class Group:(*)</td>
                            
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="6" style="width: 80%; table-layout:fixed;">
                                <div id="dvclassgroup" style="width: 100%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="gvclassgroup" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                         <Columns>
                                            <asp:BoundField DataField="ClassGroupType" ItemStyle-Width="20%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>Race Abbriviation:(*)</td>
                            <td colspan="6">
                               <asp:Label ID="lblAbbreviation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Race History:</td>
                            <td colspan="6">
                            <%--    <asp:Label ID="lblRaceHistory" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                      <tr>
                            <td></td>
                            <td colspan="6" style="width: 80%; table-layout:fixed;">
                                <div id="dvracehistory" style="width: 100%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="gvracehistory" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                         <Columns>
                                            <asp:BoundField DataField="SNo" ItemStyle-Width="10%" />
                                             <asp:BoundField DataField="RaceHistory" ItemStyle-Width="40%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        
                       
                        <tr>
                            <td>Dates:</td>
                            <td colspan="6">
                                <asp:Button ID="Button1" runat="server" Text="Add" OnClientClick="OpenHorsePopup('RaceDate')" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                             <td colspan="6">
                                <div id="dvRaceDate" style="width: 100%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="gridvwGeneralDates" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="DateType" HeaderText="Date Type" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="DateTerm" HeaderText="Date Term" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="Allowed" HeaderText="Not Allowed" ItemStyle-Width="12%" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="4%" />
                                            <asp:BoundField DataField="Time" HeaderText="Time" ItemStyle-Width="4%" />
                                            <asp:BoundField DataField="Fees" HeaderText="Fees" ItemStyle-Width="4%" />
                                             <asp:BoundField DataField="AmountPercentage" HeaderText="Percentage" ItemStyle-Width="5%" />
                                             <asp:BoundField DataField="AmountInWords" HeaderText="In Words" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="ReasonOfChange" HeaderText="Reason Of Change" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="26%" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>Stake Contributor Horses:</td>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td>Stake Money Addition:</td>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td>Stake Money Calculation:</td>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td>Stake Money Distribution:</td>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td>My Observations:</td>
                            <td colspan="6">
                                <asp:Button ID="btnObservation" runat="server" Text="Add" OnClientClick="OpenHorsePopup('Observation')" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                             <td colspan="6">
                                <div id="DvObservation" style="width: 100%; overflow: auto;" runat="server" visible="false">
                                    <asp:GridView ID="GvObservation" runat="server" Width="100%"
                                        AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No Record Found">
                                        <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                                            HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="AimedDuration" HeaderText="Aimed Duration" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="Observation" HeaderText="Observation" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="ObservationRelatedType" HeaderText="Related Type" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="ObservationRelatedToName" HeaderText="Related Name" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TillDate" HeaderText="Till Date" ItemStyle-Width="10%" />
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
                                Total General Race Created: <asp:Label ID="lblGeneralRacecount" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="BtnAddClick" ValidationGroup="prospectusGeneral" /></td>
                           
                            <td>
                                <asp:Button ID="btnHorseShow" runat="server" Text="Show" ValidationGroup="prospectusGeneral" /></td>
                            <td>
                                <asp:Button ID="btnPdf" runat="server" Text="Pdf" ValidationGroup="prospectusGeneral" /></td>
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
                    <asp:Button ID="Button2" runat="server" Text="Close" /></td>
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
