<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceCardReport.aspx.cs" Inherits="VKATalk.Reports.RaceCardReport" %>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Race Card</h1>
    <div id="dialog" style="display: none">
    </div>
    <table>
       <tr>
           <td>
                <table>
       <tr>
           <td><asp:label ID="lblCener" Width="5%" Font-Size="X-Large" runat="server"  ForeColor="Black" Font-Bold="true"></asp:label>  </td>
           <td><asp:label ID="lblSeason" runat="server" Width="10%" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:label>  </td>
           <td><asp:label ID="lblYear" runat="server" Width="10%" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:label>  </td>
           <td><asp:label ID="lblRaceDate" runat="server" Width="10%" Font-Size="X-Large" ForeColor="Black" Font-Bold="true"></asp:label>  </td>
           <td><asp:label ID="Label18" runat="server" Font-Size="X-Large" ForeColor="Black" Font-Bold="true" Text='<%#Eval("RaceDay") %>'
               ></asp:label>  </td>
       </tr>
                    <tr>
                        <td colspan="5">
                            _____________________________________________________________________________________________________________________________________
                        </td>
                    </tr>
            </table>

           </td>
       </tr>
        
        <tr>
            <td>

        <asp:ListView runat="server" ID="lvCardRace" OnItemDataBound="ListView1_ItemDataBound"> 
            <LayoutTemplate>
                <table runat="server" id="table1" cellpadding="5" width="100%">
                    <tr runat="server" id="itemPlaceholder">
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate><%-- sets the custom content for the data item in a ListView control.--%>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnfieldDivisionRaceID" runat="server" Value='<%#Eval("DivisionRaceID") %>' />
                            <asp:Label ID="lblDayRaceNo" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("DayRaceNo") %>' />
                            <asp:Label ID="Label1" runat="server" Font-Size="Large" Text='(' />
                            <asp:Label ID="lblSeasonRaceNo" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("SeasonRaceNo") %>' />
                            <asp:Label ID="Label2" runat="server" Font-Size="Large" Text=')-' />
                            <asp:Label ID="NameLabel" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("DivisionRaceName") %> ' />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label3" runat="server" Font-Size="Large" Text='Dist-' />
                            <asp:Label ID="Label4" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("Distance") %> ' />
                            <asp:Label ID="Label5" runat="server" Font-Size="Large" Text='Mts' />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label34" runat="server" Font-Size="Large" Text='A' />
                            
                            
                            
                            <asp:Label ID="Label6" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("RaceStatus")) == "") ? "" : Convert.ToString(Eval("RaceStatus"))+"," %>' />
                            <asp:Label ID="Label7" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("Million")) == "") ? "" : Convert.ToString(Eval("Million"))+"," %>' />
                            <asp:Label ID="Label8" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("SweepStake")) == "") ? "" : Convert.ToString(Eval("SweepStake"))+"," %>' />
                            <asp:Label ID="lblClassicc" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("Classic")) == "") ? "" : Convert.ToString(Eval("Classic"))+"," %>' />
                            <asp:Label ID="Label10" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("GradeNo")) == "") ? "" : Convert.ToString(Eval("GradeNo"))+"," %>' />
                            <asp:Label ID="Label11" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("RaceType")) == "") ? "" : Convert.ToString(Eval("RaceType"))+"," %>' />
                            <asp:Label ID="Label12" runat="server" Font-Size="Large" Text='Race for' />
                            <asp:Label ID="lblBunchCondition" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("BunchCondition")) == "") ? "" : Convert.ToString(Eval("BunchCondition")) %>' />
                            <asp:Label ID="Label9" runat="server" Font-Size="Large" Text='Horses ' />
                            <asp:Label ID="Label13" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("HandicapRatingRange")) == "") ? "" : "Rated " + Convert.ToString(Eval("HandicapRatingRange")) %>' />
                            <asp:Label ID="Label15" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("ClassType")) == "") ? "" : " (" + Convert.ToString(Eval("ClassType")) +")," %>' />
                            
                            <asp:Label ID="Label20" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("EHRR")) == "") ? "" : Convert.ToString(Eval("EHRR")) + " Eligible, " %>' />
                            
                            <asp:Label ID="Label17" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("AgeCondition")) == "") ? "" : Convert.ToString(Eval("AgeCondition")) %>' />
                            <asp:Label ID="Label28" runat="server" Text='<%# (Convert.ToString(Eval("YearofBirth")) == "") ? "" : "(" + Convert.ToString(Eval("YearofBirth")) +") " %>' />
                            <asp:Label ID="Label35" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("Bunch")) == "") ? "" : " ,{"+ Convert.ToString(Eval("Bunch")) +"}" %>' />

                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Font-Size="Large" Text='Winner ' />
                            <asp:Label ID="Label22" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("MomenttoType")) == "") ? "" : "A "+ Convert.ToString(Eval("MomenttoType"))%>' />
                            <asp:Label ID="Label24" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("MomenttoCost")) == "") ? "" : "(Worth Rs."+ Convert.ToString(Eval("MomenttoCost")) +") & " %>' />
                            <asp:Label ID="Label19" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney1")) == "") ? "" : "Stake Rs."+ Convert.ToString(Eval("StakeMoney1")) %>' />
                            <asp:Label ID="Label23" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney2")) == "") ? "" : ", Second Rs."+ Convert.ToString(Eval("StakeMoney2")) %>' />
                            <asp:Label ID="Label25" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney3")) == "") ? "" : ", Third Rs."+ Convert.ToString(Eval("StakeMoney3")) %>' />
                            <asp:Label ID="Label26" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney4")) == "") ? "" : ", Fourth Rs."+ Convert.ToString(Eval("StakeMoney4")) %>' />
                            <asp:Label ID="Label40" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney5")) == "") ? "" : ", Fifth Rs."+ Convert.ToString(Eval("StakeMoney5")) %>' />
                            <asp:Label ID="Label41" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney6")) == "") ? "" : ", Sixth Rs."+ Convert.ToString(Eval("StakeMoney6")) %>' />
                            <asp:Label ID="Label43" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("StakeMoney7")) == "") ? "" : ", Seventh Rs."+ Convert.ToString(Eval("StakeMoney7")) %>' />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label51" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("MomenttoPresenter")) == "") ? "" :"Momentto Presented by "+ Convert.ToString(Eval("MomenttoPresenter")) %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("MomenttoGiver")) == "") ? "" :"& Given by "+ Convert.ToString(Eval("MomenttoGiver")) %>' />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label36" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("PermanentCondition")) == "") ? "" : Convert.ToString(Eval("PermanentCondition")) %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label37" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("SeasonalCondition")) == "") ? "" : Convert.ToString(Eval("SeasonalCondition")) %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label33" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("RaceCardCondition")) == "") ? "" : Convert.ToString(Eval("RaceCardCondition")) %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label38" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("HWPCondition")) == "") ? "" : Convert.ToString(Eval("HWPCondition")) %>' />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label39" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("MemoirType")) == "") ? "" : "This Race is in Memory of A "+ Convert.ToString(Eval("MemoirType")) +" Named " %>' />
                            <asp:Label ID="Label42" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("MemoirName")) == "") ? "" : Convert.ToString(Eval("MemoirName")) %>' />
                            <asp:Label ID="Label46" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("Profile")) == "") ? "" : "(" + Convert.ToString(Eval("Profile")) +")" %>' />
                            <asp:Label ID="Label47" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("FromYear")) == "") ? "" : " Since " + Convert.ToString(Eval("FromYear")) %>' />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label49" runat="server" Font-Size="Large" ForeColor="Black" Text='<%# (Convert.ToString(Eval("Sponcer")) == "") ? "" :"Sponcer of This Race is "+ Convert.ToString(Eval("Sponcer")) %>' />
                        </td>
                    </tr>
                   
                     <tr>
                        <td>
                            <asp:Label ID="Label53" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("RaceHistory") %> ' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label52" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("MyObservationMasterRace") %> ' />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label54" runat="server" Font-Size="Large" ForeColor="Black" Text='<%#Eval("MyObservationGeneralRace") %> ' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                            <asp:ListView ID="lvHorse" runat="server">
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
                                             <asp:Label ID="LabelSubCat" runat="server" Text='<%#Eval("HorseNo")%>'></asp:Label>
                                            <asp:Label ID="Label21" runat="server" Text='-' />
                                            <asp:Label ID="Label33" runat="server" Font-Bold="true" ForeColor="Blue" Text='<%#Eval("HorseName") %> ' />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("HorseDOB") %>
                                            (<%#Eval("DOBType") %>)
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("Age") %>y
                                            <%#Eval("Color") %>
                                            <%#Eval("Gender") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("BaseCenter") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            HR-<%#Eval("HandicapRating") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("DeclareWeight") %>Kg
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("JockeyName") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("Owner Name") %>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%#Eval("Trainer Name") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text='<%# (Convert.ToString(Eval("HorseExName")) == "") ? "" : "(Ex: " + Convert.ToString(Eval("HorseExName")) + ")" %>' />
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%#Eval("SireName") %> 
                                            <asp:Label ID="Label14" runat="server" Text='<%# (Convert.ToString(Eval("SireStandingNation")) == "") ? " - " :Convert.ToString(Eval("SireStandingNation")) + " - " %>' />
                                            <%#Eval("DamName") %>
                                            <asp:Label ID="Label16" runat="server" Text='<%# (Convert.ToString(Eval("BirthStud")) == "") ? "" : "(" + Convert.ToString(Eval("BirthStud")) + ")" %>' />
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


                <%--<tr id="Tr1" runat="server">
                    <td id="Td1" runat="server" align="center">
                        <asp:Label ID="NameLabel" runat="server" Font-Size="Large" Text='<%#Eval("DivisionRaceName") %> ' />
                        <asp:HiddenField ID="hdnfieldDivisionRaceID" runat="server" Value='<%#Eval("DivisionRaceID") %> ' />
                        <hr />
                        <asp:ListView ID="lvHorse" runat="server">
                            <LayoutTemplate>
                                <table runat="server" id="table2">
                                    <tr runat="server" id="itemPlaceHolder">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server">
                                    <td id="Td2" runat="server" align="left">
                                        <asp:Label ID="LabelSubCat" runat="server" Text='<%#Eval("HorseName")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        
                    </td>
                </tr>--%>
            </ItemTemplate>
         </asp:ListView>

            </td>
        </tr>
    </table>
</asp:Content>
