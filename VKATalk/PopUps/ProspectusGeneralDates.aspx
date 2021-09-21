<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProspectusGeneralDates.aspx.cs" Inherits="VKATalk.PopUps.ProspectusGeneralDates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <title></title>
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
        .modalBackground { 
            background-color:#333333; 
            filter:alpha(opacity=70); 
            opacity:0.7; 
        } 
        .modalPopup { 
            background-color:#FFFFFF; 
            border-width:1px; 
            border-style:solid; 
            border-color:#CCCCCC; 
            padding:1px; 
            width:300px; 
            Height:200px; 
        }  

     </style>
    
    <script type="text/javascript">

        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Prospectus General Race Dates",
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
    <asp:ScriptManager ID="ToolkitScriptManager2" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Prospectus General Race Dates</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="PropGRaceDates" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                         <asp:HiddenField runat="server" ID="hdnfldprospectusid"/>
                        <%--<asp:HiddenField runat="server" ID="hrseDateofNameChange"/>--%>
                    </td    
                </tr>
                <tr>
                    <td>General Race Name:

                    </td>
                    <td>
                        <asp:label ID="lblGeneralRaceName" runat="server"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>Date Type(*):

                    </td>
                    <td>
                        <asp:DropDownList ID="drpdnTypeofDate" runat="server"></asp:DropDownList>
                       <%-- <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator2" Display="Dynamic" 
                            ValidationGroup="PropGRaceDates" runat="server" ControlToValidate="drpdnTypeofDate"
                            Text="*" ErrorMessage="Please select Date Type"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                
                <tr>
                    <td>Date Term:(*)
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdwnDateTerm" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="PropGRaceDates" runat="server" ControlToValidate="drpdwnDateTerm"
                            Text="*" ErrorMessage="Please select Date Type"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Not Allowed:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkboxNotAllowed" runat="server" OnCheckedChanged="chkboxNotAllowed_Change" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxFromDate" Width="70px" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxFromDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxFromDate"
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                           ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="PropGRaceDates" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxFromDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Time (hh:mm):
                    </td>
                    <td>
                        <asp:DropDownList Width="40px" ID="drpdwnhh" runat="server"></asp:DropDownList>:
                        <asp:DropDownList Width="40px" ID="drpdwnMM" runat="server">
                            <asp:ListItem Selected="True" Text="00" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="15" Value="1"></asp:ListItem>
                            <asp:ListItem Text="30" Value="2"></asp:ListItem>
                            <asp:ListItem Text="45" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="drpdwnAMPM" Width="40px" runat="server">
                            <asp:ListItem Selected="true" Text="AM" Value="1"></asp:ListItem>
                            <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                 <tr>
                    <td>Fees:</td>
                    <td>
                        <asp:TextBox ID="txtbxfees" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="PropGRaceDates"
                        ControlToValidate="txtbxfees" Text="*" ErrorMessage="Only numeric are allowed"  Display="Dynamic" 
                        ForeColor="Red" ValidationExpression="\d+" > </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Amount Percentage:</td>
                    <td>
                        <asp:TextBox ID="txtbxAmountPercentage" runat="server"></asp:TextBox>
                         <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="PropGRaceDates"
                        ControlToValidate="txtbxAmountPercentage" Text="*" ErrorMessage="Only numeric are allowed"  Display="Dynamic" 
                        ForeColor="Red" ValidationExpression="\d+" > </asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>In Words:</td>
                    <td>
                        <asp:TextBox ID="txtbxInWords" runat="server" Width="500px"></asp:TextBox>
                        <div id="Div7" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddAmountInWords"
                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtbxInWords" CompletionListElementID="Div7"
                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>
                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="PropGRaceDates"
                        ControlToValidate="txtbxfees" Text="*" ErrorMessage="Only numeric are allowed"  Display="Dynamic" 
                        ForeColor="Red" ValidationExpression="\d+" > </asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                 <tr>
                    <td>Reason Of Change:</td>
                    <td>
                        <asp:TextBox ID="txtbxReasonOfChange" runat="server" Width="500px"></asp:TextBox>
                         <div id="Div6" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddReasonOfChange"
                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtbxReasonOfChange" CompletionListElementID="Div6"
                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>
                    </td>
                </tr>
                 <tr>
                    <td>My Comments:</td>
                    <td>
                        <asp:TextBox ID="txtbxMyComments" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                         <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="txtbxMyComments" CompletionListElementID="Div1"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="1"></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="PropGRaceDates" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="PropGRaceDates"/>    
                        <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"  />
                        <asp:Button ID="btnClose" runat="server" Text="Close"  OnClick="btnClose_Click" />
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
             <div id="dvProspectus" style="height: 250px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvProspectus" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="GeneralDatesID" OnSelectedIndexChanged="GvProspectus_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Type of Date" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldtypeofdate" Value='<%# Bind("DateType") %>'/>
                                <asp:LinkButton Text='<%# Bind("DateType") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateTerm" HeaderText="Date Term" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Allowed" HeaderText="Allowed" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Time" HeaderText="Time" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Fees" HeaderText="Fees" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="AmountPercentage" HeaderText="Percentage" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="AmountInWords" HeaderText="In Words" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="ReasonOfChange" HeaderText="Reason Of Change" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="20%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    
    <script type="text/javascript">
      
        function refreshParentPage() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }
    </script>
</asp:Content>