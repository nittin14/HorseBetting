<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceGuide.aspx.cs" Inherits="VKATalk.Reports.RaceGuide" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #main_container {
            margin: 7px 0;
        }
    </style>
   <script type="text/javascript">
    function tablePrint() {
        var display_setting = "toolbar=yes,location=no,directories=yes,menubar=yes,";
        display_setting += "scrollbars=yes,width=750, height=600, left=100, top=25";

        var content_innerhtml = document.getElementById("printable").innerHTML;
        var document_print = window.open("", "", display_setting);
        document_print.document.open();
        document_print.document.write('<html><head><title>Print Guide</title></head>');
        document_print.document.write('<body style="font-family:verdana; font-size:50px;" onLoad="self.close();" >');
        document_print.document.write(content_innerhtml);
        document_print.document.write('</body></html>');
        document_print.print();
        document_print.document.close();
        return false;
    }  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Race Guide</h1>
    <div id="dialog" style="display: none">
    </div>
    <%--<input type = "button" id = "btnPrint" value = "Print" onclick = "window.print()" />--%>
    <asp:LinkButton ID="lbPrint" runat="server" Text="Print Guide"
     OnClientClick="tablePrint();" />
    <div id="printable">
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="padding-left: 25px;">
                            <asp:Label ID="lblCener" Font-Size="X-Large" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="padding-left: 45px;">
                            <asp:Label ID="lblSeason" runat="server" Width="10%" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="padding-left: 65px;">
                            <asp:Label ID="lblYear" runat="server" Width="150px" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="padding-left: 75px;">
                            <asp:Label ID="lblRaceDate" runat="server" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="padding-left: 85px;">
                            <asp:Label ID="lblDivisionRaceID" runat="server" Width="10%" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="padding-left: 90px;">
                            <asp:DropDownList ID="drpdwnRaceNumber" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpdwnRaceNumber_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>

                </table>

            </td>
        </tr>


    </table>
    <div>
        <table style="width: 1500px;">
            <tr>
                <td>
                    <!-- Division Race Section ------------------------->
                    <asp:ListView runat="server" ID="lvCardRace" OnItemDataBound="lvCardRace_ItemDataBound">
                        <LayoutTemplate>
                            <table runat="server" id="table1" cellpadding="5" width="100%">
                                <tr runat="server" id="itemPlaceholder">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; border-collapse: collapse;">
                                <tr style="border: solid; border-color: blueviolet;">
                                    <td>
                                        <asp:HiddenField ID="hdnfieldDivisionRaceID" runat="server" Value='<%#Eval("DivisionRaceID") %>' />
                                        <%#Eval("DayRaceNo") %>
                            (<%#Eval("SeasonRaceNo") %>)-
                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("DivisionRaceName") %> ' />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Dist-<%#Eval("Distance") %> Mts
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <%# (Convert.ToString(Eval("ClassType")) == "") ? "" : " (" + Convert.ToString(Eval("ClassType")) +")" %>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <%#Eval("RaceTime") %> 
                                        <br />
                                        {<%#Eval("MasterRaceName") %>}-{<%#Eval("GeneralRaceName") %>}
                                    </td>

                                </tr>
                                <tr>
                                    <td>A
                            <%# (Convert.ToString(Eval("RaceStatus")) == "") ? "" : Convert.ToString(Eval("RaceStatus"))+"," %>
                                        <%# (Convert.ToString(Eval("Million")) == "") ? "" : Convert.ToString(Eval("Million"))+"," %>
                                        <%# (Convert.ToString(Eval("SweepStake")) == "") ? "" : Convert.ToString(Eval("SweepStake"))+"," %>
                                        <%# (Convert.ToString(Eval("Classic")) == "") ? "" : Convert.ToString(Eval("Classic"))+"," %>
                                        <%# (Convert.ToString(Eval("GradeNo")) == "") ? "" : Convert.ToString(Eval("GradeNo"))+"," %>
                                        <%# (Convert.ToString(Eval("RaceType")) == "") ? "" : Convert.ToString(Eval("RaceType"))+"," %>
                            Race for
                            <%# (Convert.ToString(Eval("BunchCondition")) == "") ? "" : Convert.ToString(Eval("BunchCondition")) %>
                            Horses 
                            <%# (Convert.ToString(Eval("HandicapRatingRange")) == "") ? "" : "Rated " + Convert.ToString(Eval("HandicapRatingRange")) %>
                                        <%# (Convert.ToString(Eval("ClassType")) == "") ? "" : " (" + Convert.ToString(Eval("ClassType")) +")," %>
                                        <%# (Convert.ToString(Eval("EHRR")) == "") ? "" : Convert.ToString(Eval("EHRR")) + " Eligible, " %>
                                        <%# (Convert.ToString(Eval("AgeCondition")) == "") ? "" : Convert.ToString(Eval("AgeCondition")) %>
                                        <%# (Convert.ToString(Eval("YearofBirth")) == "") ? "" : "(" + Convert.ToString(Eval("YearofBirth")) +") " %>
                                        <%# (Convert.ToString(Eval("Bunch")) == "") ? "" : " ,{"+ Convert.ToString(Eval("Bunch")) +"}" %>
                                        <%# (Convert.ToString(Eval("RaceAbbreviation")) == "") ? "" : " ,"+ Convert.ToString(Eval("RaceAbbreviation")) %>
                                        <%# (Convert.ToString(Eval("BunchGroupTriology")) == "") ? "" : " ,"+ Convert.ToString(Eval("BunchGroupTriology")) %>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Winner 
                            <asp:Label ID="Label22" runat="server" Text='<%# (Convert.ToString(Eval("MomenttoType")) == "") ? "" : "A "+ Convert.ToString(Eval("MomenttoType"))%>' />
                                        <asp:Label ID="Label24" runat="server" Text='<%# (Convert.ToString(Eval("MomenttoCost")) == "") ? "" : "(Worth Rs."+ Convert.ToString(Eval("MomenttoCost")) +") & " %>' />
                                        <%# (Convert.ToString(Eval("StakeMoney1")) == "") ? "" : "Stake Rs."+ Convert.ToString(Eval("StakeMoney1")) %>
                                        <%# (Convert.ToString(Eval("StakeMoney2")) == "") ? "" : ", Second Rs."+ Convert.ToString(Eval("StakeMoney2")) %>
                                        <%# (Convert.ToString(Eval("StakeMoney3")) == "") ? "" : ", Third Rs."+ Convert.ToString(Eval("StakeMoney3")) %>
                                        <%# (Convert.ToString(Eval("StakeMoney4")) == "") ? "" : ", Fourth Rs."+ Convert.ToString(Eval("StakeMoney4")) %>
                                        <%# (Convert.ToString(Eval("StakeMoney5")) == "") ? "" : ", Fifth Rs."+ Convert.ToString(Eval("StakeMoney5")) %>
                                        <%# (Convert.ToString(Eval("StakeMoney6")) == "") ? "" : ", Sixth Rs."+ Convert.ToString(Eval("StakeMoney6")) %>
                                        <%# (Convert.ToString(Eval("StakeMoney7")) == "") ? "" : ", Seventh Rs."+ Convert.ToString(Eval("StakeMoney7")) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%# (Convert.ToString(Eval("MomenttoPresenter")) == "") ? "" :"Momentto Presented by "+ Convert.ToString(Eval("MomenttoPresenter")) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text='<%# (Convert.ToString(Eval("MomenttoGiver")) == "") ? "" :"& Given by "+ Convert.ToString(Eval("MomenttoGiver")) %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" Text='<%# (Convert.ToString(Eval("PermanentCondition")) == "") ? "" : Convert.ToString(Eval("PermanentCondition")) %>' />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# (Convert.ToString(Eval("OtherCondition")) == "") ? "" : Convert.ToString(Eval("OtherCondition")) %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" Text='<%# (Convert.ToString(Eval("SeasonalCondition")) == "") ? "" : Convert.ToString(Eval("SeasonalCondition")) %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label33" runat="server" Text='<%# (Convert.ToString(Eval("RaceCardCondition")) == "") ? "" : Convert.ToString(Eval("RaceCardCondition")) %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%# (Convert.ToString(Eval("HWPCondition")) == "") ? "" : Convert.ToString(Eval("HWPCondition")) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%# (Convert.ToString(Eval("MemoirType")) == "") ? "" : "This Race is in Memory of A "+ Convert.ToString(Eval("MemoirType")) +" Named " %>
                                        <%# (Convert.ToString(Eval("MemoirName")) == "") ? "" : Convert.ToString(Eval("MemoirName")) %>
                                        <%# (Convert.ToString(Eval("Profile")) == "") ? "" : "(" + Convert.ToString(Eval("Profile")) +")" %>
                                        <%# (Convert.ToString(Eval("FromYear")) == "") ? "" : " Since " + Convert.ToString(Eval("FromYear")) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%# (Convert.ToString(Eval("Sponcer")) == "") ? "" :"Sponcer of This Race is "+ Convert.ToString(Eval("Sponcer")) %>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <%#Eval("RaceHistory") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%#Eval("MyObservationMasterRace") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%#Eval("MyObservationGeneralRace") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
<!-- Horse Section ----------------------------------->
                                        <asp:ListView ID="lvHorse" runat="server" OnItemDataBound="lvHorse_ItemDataBound">
                                            <LayoutTemplate>
                                                <table runat="server" id="table2">
                                                    <tr runat="server" id="itemPlaceHolder">
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <span style='background-color: Orange;'></span>
                                                            <asp:HiddenField ID="hdnfieldHorseNameID" runat="server" Value='<%#Eval("HorseNameID") %>' />
                                                            <asp:HiddenField ID="hdnfieldHorseID" runat="server" Value='<%#Eval("HorseID") %>' />

                                                            <%#Eval("HorseNo")%>
                                            -
                                            <asp:Label ID="Label33" runat="server" Font-Bold="true" ForeColor="Blue" Text='<%#Eval("HorseName") %> ' />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("HorseDOB") %>
                                            (<%#Eval("DOBType") %>)
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("Age") %>y
                                            <%#Eval("Color") %>
                                                            <%#Eval("Gender") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            (<%#Eval("BaseCenter") %>) (<%#Eval("RunningCenter") %>)
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            HR-<%#Eval("HandicapRating") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("DeclareWeight") %>Kg
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("JockeyName") %>
                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%# (Convert.ToString(Eval("HorseExName")) == "") ? "" : "(Ex: " + Convert.ToString(Eval("HorseExName")) + ")" %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="Label58" runat="server" Text='<%# (Convert.ToString(Eval("SireName")) == "") ? "" :Convert.ToString(Eval("SireName"))+ " - " %>' />
                                                            <asp:Label ID="Label14" BackColor="Orange" runat="server" Text='<%# (Convert.ToString(Eval("SireStandingNation")) == "") ? "" :Convert.ToString(Eval("SireStandingNation")) %>' />
                                                            <%#Eval("DamName") %>
                                                            <%# (Convert.ToString(Eval("Breeder")) == "") ? "" : " - (" + Convert.ToString(Eval("Breeder"))  %>
                                                            - <%#Eval("BreederPartner") %> )
                                                            <%# (Convert.ToString(Eval("BirthStud")) == "") ? "" : " - (" + Convert.ToString(Eval("BirthStud"))  %>
                                                            <%# (Convert.ToString(Eval("BirthStudOwner")) == "") ?  "" : " - " + Convert.ToString(Eval("BirthStudOwner")) + ")" %>
                                                        </td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td>
                                                            <hr />
                                                            <!-- gear section--->
                                                            <asp:Repeater ID="rpCardDeclaration" runat="server">
                                                                <HeaderTemplate>
                                                                    <table cellspacing="0" rules="all" border="1">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <%# Eval("CSY") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DateWithGap") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SNo") %><sup> <%# Eval("Placing") %></sup>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SwimmingPerformance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("TreadmillPerformance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("TrackPerformance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GatePracticePerformance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MockRacePerformance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("HorseBodyWeight") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("EquipmentAlias") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BitAlias") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BandageAlias") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("Shoe") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ShoeMetalAlias") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("HandicapRating") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("AcceptanceWeightGBC") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("JockeyAllowance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DeclareWeight") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CarriedWeight") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("JockeyName") %><sup>    <%# Eval("ProfessionalJockeyWeight") %></sup>
                                                                        </td>

                                                                    </tr>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                            
                                                            <!------ Professional SEction ------------------------>
                                                            <table style="border: 1px solid black; border-collapse: collapse;">
                                                                <tr>
                                                                    <td style="border: 1px solid black;">
                                                                        <%#Eval("Trainer Name") %>   
                                                                    </td>
                                                                    <td style="border: 1px solid black;">
                                                                        <%#Eval("Trainer Actual") %>   
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border: 1px solid black;">
                                                                        <%#Eval("Owner Name") %>
                                                                    </td>
                                                                    <td style="border: 1px solid black;">
                                                                        <%#Eval("Owner Actual") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border: 1px solid black;">
                                                                        <%#Eval("OwnerColor") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border: 1px solid black;">
                                                                        <%#Eval("OwnerPartner") %>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <!-- Performance section------------------->
                                                            <asp:Repeater ID="rpBunchPerformance" runat="server" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <table cellspacing="0" rules="all" border="1">
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%# Eval("BunchPerformance") %>
                                                                                        </td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </table>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>


                                                            <asp:Repeater ID="rpBunchClusterPerformance" runat="server" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <table cellspacing="0" rules="all" border="1">
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%# Eval("BunchClusterPerformance") %>
                                                                                        </td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </table>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>

                                                            <asp:Repeater ID="rpBunchGroupAlias" runat="server" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <table cellspacing="0" rules="all" border="1">
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%# Eval("BunchGroupAlias") %>
                                                                                        </td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </table>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>

                                                             <asp:Repeater ID="rpHorsePerformanceCalculator" runat="server">
                                                                <HeaderTemplate>
                                                                    <table cellspacing="0" rules="all" border="1">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <%# Eval("HorsePerformanceCalculation") %>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>


                                                            <asp:Repeater ID="rpDistancePerformanceCurrent" runat="server">
                                                                <HeaderTemplate>
                                                                    <table cellspacing="0" rules="all" border="1">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <%# Eval("DistancePerformanceCurrent") %>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>

                                                            <%--<asp:ListView ID="lvMasterBunchAlias" runat="server" OnItemDataBound="lvMasterBunchAlias_ItemDataBound" Visible="false">
                                                                <LayoutTemplate>
                                                                    <table runat="server" id="table2">
                                                                        <tr runat="server" id="itemPlaceHolder">
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border: 1px solid black; border-collapse: collapse;">
                                                                            <%# Eval("MasterBunchAlias") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>--%>

<!-- HOrse Family Section----------------------->
                                                            <asp:ListView ID="lvFamilyTree" runat="server" OnItemDataBound="lvFamilyTree_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <table runat="server" id="table2">
                                                                        <tr runat="server" id="itemPlaceHolder">
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <table style="font-family: arial, sans-serif; border-collapse: collapse; width: 90%; table-layout: fixed;">
                                                                        <tr>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 8%;">
                                                                                <asp:HiddenField ID="hdnfielddisperformatstyle" runat="server" Value='<%#Eval("DistancePerformanceStyleFormat") %>' />
                                                                                <asp:HiddenField ID="hdnfieldBunchGroupPerformanceStyleFormat" runat="server" Value='<%#Eval("BunchGroupPerformanceStyleFormat") %>' />
                                                                                <asp:Label ID="lblHorseName" runat="server" Text='<%#Eval("HorseName") %>' /></td>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 2%;">

                                                                                <%#Eval("A C G") %>

                                                                            </td>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 2%;">
                                                                                <%#Eval("Relation") %></td>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 1%;">
                                                                                <%#Eval("Profile") %></td>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 2%;">(<%#Eval("BaseCenter") %>)(<%#Eval("RunningCenter") %>)</td>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 7%;">
                                                                                <%#Eval("Trainer") %></td>
                                                                            <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 2%;">
                                                                                <%#Eval("Carrier Statistics") %></td>
                                                                          
                                                                            
                                                                            <tr>
                                                                                <td></td>
                                                                             <td colspan="2" style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 10%;">
                                                                               <%#Eval("Distance") %></td>
                                                                            <td colspan="3" style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 15%;">
                                                                                <asp:Label ID="Label50" runat="server" Text='<%#Eval("Distance Performance") %>' />
                                                                                <asp:Image src="../Images/TickMark.png" runat="server" Visible="false" ID="imgtickmark" />
                                                                            </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td colspan="2" style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 5%">
                                                                                <%#Eval("Class") %></td>
                                                                                <td colspan="3" style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 10%;">
                                                                                <asp:Label ID="Label56" runat="server" Text='<%#Eval("Bunch Group Performance") %>' /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                                    <td colspan="5" style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 5%;">
                                                                                        <%#Eval("Achievements") %>
                                                                                    </td>
                                                                                </tr>

                                                                        </tr>
                                                                       <%-- <tr>
                                                                               <td style="border: 1px solid #dddddd; text-align: left; padding: 8px; width: 10%;" colspan="11">
                                                                                <asp:Label ID="Label56" runat="server" Text='<%#Eval("Bunch Group Performance") %>' /></td>
                                                                        </tr>--%>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:ListView>
<!-- workout section------------------------->
                                                            <asp:ListView ID="lsviewhorseperformance" runat="server" OnItemDataBound="lsviewhorseperformance_ItemDataBound">
                                                                <LayoutTemplate>

                                                                    <table runat="server" id="table2">
                                                                        <tr runat="server" id="itemPlaceHolder">
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <table cellspacing="0" rules="all" border="1">
                                                                        <tr>
                                                                            <td colspan="10">
                                                                                <asp:Repeater ID="Repeater1" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all" border="1">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("SwimmingValue") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="10">
                                                                                <asp:Repeater ID="rptHorseVeterinaryProblems" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all" border="1">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("FromDate") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("TillDate") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("Disease") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td colspan="10">
                                                                                <asp:Repeater ID="rpTreadmill" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all" border="1">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("TreadmillDate") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("Value") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="10">
                                                                                <asp:Repeater ID="rpCardTrack" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all" border="1">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("TrackDate") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("CenterName") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("SR") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("WorkoutType") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("AGHR") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("DRHRHName") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("Rider") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("Trainer") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("DrawNo") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("MRCarriedWeight") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("DistanceBreakUp") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("TimeTaken") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("TrackAlias") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("VerdictMarginAlias") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("CommonComment") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("IndividualHorseComment") %>
                                                                                            </td>

                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="10">
                                                                                <asp:Repeater ID="RpStruckOut" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all" border="1">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                        <td>
                                                                                                <%# Eval("CSY") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("GeneralRaceDate") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("GeneralRaceName") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("AcceptanceWeightGBC") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("HandicapRatingRange") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("ClassType") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("AgeCondition") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("Distance") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>


<!-- Run Section-------------->
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("CSY") %>
                                                                            </td>
                                                                            <td style="width: 100px;">
                                                                                <asp:HiddenField ID="hdnfieldDivisionRaceDateSeason" runat="server" Value='<%#Eval("DivisionRaceDate") %>' />
                                                                                <%# Eval("DivisionRaceDate") %>
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                <%# Eval("SeasonRaceNo") %> <sup><%# Eval("Placing") %> </sup>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("RunQuality") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("RaceIntroduction") %> <sup><%# Eval("RaceAbrevationPower") %></sup>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("StrategicChange") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("HandicapRating") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("AcceptanceWeightGBC") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("JockeyAllowance") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("DeclareWeight") %>
                                                                            </td>
                                                                             <td>
                                                                                <%# Eval("CarriedWeight") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ProfessionalName") %> <sup><%# Eval("ProfessionalJockeyWeight") %></sup>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("VerdictMargin") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PenetrometerReading") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("FalseRails") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("WinnerTime") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("FinishTime") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("minutes") %>:<%# Eval("seconds") %>:<%# Eval("milliseconds") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("HorsePosition1") %>/<%# Eval("HorsePosition2") %>/<%# Eval("HorsePosition3") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("WinnerHorsePlacing") %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2"></td>
                                                                            <td colspan="8">
                                                                                <asp:Repeater ID="rpFactFinding" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("FactFinding") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Repeater ID="rpracecardconnectioncluster" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("RaceCardConnectionCluster") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                            <td colspan="8">
                                                                                <asp:Repeater ID="rpHorseHabitCurrentMissionForm" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table cellspacing="0" rules="all">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# Eval("HabitCurrentMissionForm") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2"></td>
                                                                            <td colspan="8"><%# Eval("Incident") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2"></td>
                                                                            <td colspan="8"><%# Eval("Commentory") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2"></td>
                                                                            <td colspan="8"><%# Eval("RaceDayReporter") %></td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                            <asp:Repeater ID="rpcardentry" runat="server">
                                                                <HeaderTemplate>
                                                                    <table cellspacing="0" rules="all" border="1">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <tr>
                                                                        <td>
                                                                            <%# Eval("RaceDate") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GeneralRaceName") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("HandicapRatingRange") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ClassType") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("AgeCondition") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("Distance") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("HdWghGBC") %>
                                                                        </td>
                                                                    </tr>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </td>
                                </tr>
                            </table>

                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server" align="center">

                                    <hr />


                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    </div>
</asp:Content>
