<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddStakeMoney.aspx.cs" Inherits="VKATalk.Master.AddStakeMoney" %>
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
            height: 100px;
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
    </style>
        <script type="text/javascript" lang="javascript">


            function ShowPopup(message) {
                $(function () {
                    $("#dialog").html(message);
                    $("#dialog").dialog({
                        title: "Stake Money",
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
    <asp:ScriptManager id="ScripManager1" runat="server"></asp:ScriptManager>
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Stake Money</h1>
       <div id="dialog" style="display: none">
</div>
<table align="center">
  <tr>
    <td>
      <fieldset style="width:900px;" class="Userlogin">
            <table>
    <tr>
    <td colspan="2">
    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="StakeMoney" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana" 
     Font-Size="12" />
        <asp:HiddenField ID="hdnfieldStakeMoneyID" runat="server" />
    </td>
    </tr>
<tr>

<td style="width:200px;">Center:(*)</td>
<td><asp:DropDownList ID="drpdwnCenter" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please select Center Name"  ValidationGroup="StakeMoney" 
    ControlToValidate="drpdwnCenter" InitialValue="-1">*</asp:RequiredFieldValidator>
</td>
   <td>Season:(*)
                </td>
                <td>
                            <asp:DropDownList ID="drpdwnFromSeason" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select Season."  
                                 ValidationGroup="StakeMoney" ControlToValidate="drpdwnFromSeason" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
</tr>
                <tr>
                    <td>Year:(*)</td>
                        <td>
                             <asp:DropDownList ID="drpdwnFromYear" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Year."  
                                 ValidationGroup="StakeMoney" ControlToValidate="drpdwnFromYear" InitialValue="-1">*</asp:RequiredFieldValidator>
                        </td>
                    <td>Till Date:
                </td>
                <td>
                    <asp:TextBox ID="txtbxTillDate" runat="server" Width="75px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" TargetControlID="txtbxTillDate" Mask="99-99-9999"
                        ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender1"
                        ControlToValidate="txtbxTillDate"
                        InvalidValueMessage="Invalid date format."
                        Display="Dynamic"
                        IsValidEmpty="True"
                        InvalidValueBlurredMessage="*"
                        ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                        ValidationGroup="StakeMoney" />
                    <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                        Format="dd-MM-yyyy"></asp:CalendarExtender>
                </td>
</tr>

                <tr>
                    <td>Race Type:(*)</td>
                    <td><asp:RadioButtonList ID="rdbtnRaceType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="Handicap"></asp:ListItem>
                        <asp:ListItem Text="Terms"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                      <td>Handicap Rating Range</td>
                     <td><asp:DropDownList ID="drpdwnHandicapRatingRange" runat="server"></asp:DropDownList></td>
                </tr>
                 <tr>
                                         <td>Age Condition:</td>
                                        <td><asp:DropDownList ID="drpdwnAgeCondition" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                       <td>StakeMoney Table No:(*)</td>
                                    <td><asp:DropDownList ID="drpdwnTableNo" runat="server">
                                        <asp:ListItem Selected="True" Value="-1">--Please select--</asp:ListItem>
                                        <asp:ListItem Value="1">Table 0</asp:ListItem>
                                        <asp:ListItem Value="2">Table 1</asp:ListItem>
                                        <asp:ListItem Value="3">Table 2</asp:ListItem>
                                        <asp:ListItem Value="4">Table 3</asp:ListItem>
                                        <asp:ListItem Value="5">Table 4</asp:ListItem>
                                        <asp:ListItem Value="6">Table 5</asp:ListItem>
                                        <asp:ListItem Value="7">Table 6</asp:ListItem>
                                        <asp:ListItem Value="8">Table 7</asp:ListItem>
                                        <asp:ListItem Value="9">Table 8</asp:ListItem>
                                        <asp:ListItem Value="10">Table 9</asp:ListItem>
                                        <asp:ListItem Value="11">Table 10</asp:ListItem>
                                        <asp:ListItem Value="12">Table 11</asp:ListItem>
                                        <asp:ListItem Value="13">Table 12</asp:ListItem>
                                        <asp:ListItem Value="14">Table 13</asp:ListItem>
                                        <asp:ListItem Value="15">Table 14</asp:ListItem>
                                        <asp:ListItem Value="16">Table 15</asp:ListItem>
                                        <asp:ListItem Value="17">Table 16</asp:ListItem>
                                        <asp:ListItem Value="18">Table 17</asp:ListItem>
                                        <asp:ListItem Value="19">Table 18</asp:ListItem>
                                        <asp:ListItem Value="20">Table 19</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                            ErrorMessage="Please select StakeMoney Table No."  ValidationGroup="StakeMoney" ControlToValidate="drpdwnTableNo" InitialValue="-1">*</asp:RequiredFieldValidator>
                                    </td>
                     </tr>
                <tr>
                                                     
                <td>Stake Money:(*)</td>
                <td><asp:TextBox ID="txtbxStakeMoney" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter stake money."  
                            ValidationGroup="StakeMoney" ControlToValidate="txtbxStakeMoney">*</asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                            ControlToValidate="txtbxStakeMoney"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="StakeMoney"
                                            runat="server">*</asp:RegularExpressionValidator>
                </td>
                <td>Momentto Cost:</td>
                <td><asp:TextBox ID="txtbxMomenttoCost" runat="server"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtbxMomenttoCost"
                                            ValidationExpression="\d+"
                                            ErrorMessage="Please enter numbers only"
                                            ValidationGroup="StakeMoney"
                                            runat="server">*</asp:RegularExpressionValidator>
                </td>
                </tr>
                

<tr>
                     <td>Stake Money Earner:(*)
                </td>
                <td>
                            <asp:DropDownList ID="drpdwnStakeMoneyEarner" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please select Stake Money Earner."  
                                 ValidationGroup="StakeMoney" ControlToValidate="drpdwnStakeMoneyEarner" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
     <td>Fixed:</td>
<td><asp:CheckBox ID="chkbxstatus" runat="server" />
</td>
    </tr>
     
           <tr>
                                         <td>Placing:(*)</td>
                                    <td><asp:DropDownList ID="drpdwnPlacing" runat="server">
                                        <asp:ListItem Selected="True" Value="-1">--Please select--</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ErrorMessage="Please select Placing."  ValidationGroup="StakeMoney" 
                                            ControlToValidate="drpdwnPlacing" InitialValue="-1">*</asp:RequiredFieldValidator>
                                    </td>
                

                     </tr>
               <tr>
                                        
                <td>Percentage:(**)</td>
                <td><asp:TextBox ID="txtbxPercentage" runat="server"></asp:TextBox>
                </td>
                 <td>Amount:(**)</td>
                <td><asp:TextBox ID="txtbxAmount" runat="server"></asp:TextBox>
                </td>
                </tr>
                
                
</table>
            <table>
<tr>
    <td><asp:Button runat="server" ID="btnSave" Text="Add" OnClick="BtnSubmit_Click" ValidationGroup="StakeMoney" /></td>
    <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
    <td><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>
    <td><asp:Button ID="btnPdf" runat="server" Text="Pdf" /></td>
    <td><asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
    <td>  <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </asp:ModalPopupExtender></td>
    <td><asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" /></td>
    <td><asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
    <td><asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
</tr>
</table>
          </fieldset>
        </td>
      </tr>
    </table>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
            <table>
                <tr>
                    <td>File Upload:</td>
                    <td><asp:FileUpload ID="flupload" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnFileUpload" runat="server" Text="Upload" OnClick="btnFileUpload_Click" /></td>
                    <td><asp:Button ID="Button2" runat="server" Text="Close" /></td>
                </tr>
            </table>
            </asp:Panel>
            <div id="dvStakeMoney" style="height: 150px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvStakeMoney" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="StakeMoneyID" OnSelectedIndexChanged="GvStakeMoney_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                 <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("CenterName") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldstakemoneyearnerid" Value='<%# Bind("StakeMoneyEarnerID") %>'/>
                                 <asp:LinkButton Text='<%# Bind("CenterName") %>' ID="lnkSelect" runat="server" CommandName="Select" /> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromSeason" HeaderText="Season" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="FromYear" HeaderText="Year" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="TillDate" HeaderText="Till Date" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="RaceType" HeaderText="Race Type" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="HandicapRatingRange" HeaderText="Handicap Rating Range" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="AgeCondition" HeaderText="Age Condition" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="StakeMoneyTableNo" HeaderText="StakeMoney TableNo" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="StakeMoney" HeaderText="Stake Money" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="MomenttoCost" HeaderText="Momentto Cost" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="ProfileType" HeaderText="Stake Money Earner" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Placing" HeaderText="Placing" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Percentage" HeaderText="Percentage" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="5%" />

                    </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
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
