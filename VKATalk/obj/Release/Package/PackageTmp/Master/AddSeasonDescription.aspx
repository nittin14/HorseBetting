<%@ Page Language="C#"  MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="AddSeasonDescription.aspx.cs" Inherits="VKATalk.Master.AddSeasonDescription" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script src="../Scripts/jquery1.7.2.min.js"></script>
    <link href="../Styles/jquery-ui-1.8.9.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.8.9.js"></script>
     <script type="text/javascript">
         function ShowPopup(message) {
             $(function () {
                 $("#dialog").html(message);
                 $("#dialog").dialog({
                     title: "Season Description",
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
    <div style="padding-right:220px;">
<h1 style="text-align:center; font-size:xx-large; font-weight:bold; text-decoration:underline;">Season Description</h1>
        </div>
<div id="dialog" style="display: none">
</div>
<table align="center">
    <tr>
    <td>
        <fieldset style="width:100%" class="Userlogin">
            <table align="center">
                <tr>
                    <td colspan="5">
                    <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="SeasonDescription" ShowMessageBox="true" ShowSummary="false"  Font-Names="verdana"  Font-Size="12" />
                    </td>
                </tr>
                <tr>
                   <td>Center:(*)</td>
                    <td><asp:DropDownList ID="drpdwnCenterName" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic" 
                            ValidationGroup="SeasonDescription" runat="server" ControlToValidate="drpdwnCenterName"
                            Text="*" ForeColor="Red" ErrorMessage="Please select center name."></asp:RequiredFieldValidator>
                    </td>
                    <td>Year:(*)</td>
                    <td><asp:DropDownList ID="drpdwnYear" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                            ValidationGroup="SeasonDescription" runat="server" ControlToValidate="drpdwnYear"
                            Text="*" ForeColor="Red" ErrorMessage="Please select year name."></asp:RequiredFieldValidator>

                    </td>
                  <td>Season:(*)</td>
                    <td><asp:DropDownList ID="drpdwnSeason" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator3" Display="Dynamic" 
                            ValidationGroup="SeasonDescription" runat="server" ControlToValidate="drpdwnSeason"
                            Text="*" ForeColor="Red" ErrorMessage="Please select season name."></asp:RequiredFieldValidator>
                    </td>
   
                </tr>
                <tr>
                     <td>Season StartDate:(*)</td>
                    <td><asp:TextBox ID="txtbxdatepickerSSD" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskEndDate" CultureName="en-GB" runat="server" targetcontrolid="txtbxdatepickerSSD" 
                             Mask="99-99-9999" clearmaskonlostfocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskEndDate"
                            ControlToValidate="txtbxdatepickerSSD"
                            EmptyValueMessage="Please enter Season start date."
                            InvalidValueMessage="Invalid Season start date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="SeasonDescription" />
                               <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxdatepickerSSD"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                    <td>Season EndDate:(*)</td>
                    <td><asp:TextBox ID="txtbxdatepickerSED" runat="server"></asp:TextBox>
                          <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-GB" runat="server" targetcontrolid="txtbxdatepickerSED" Mask="99-99-9999" clearmaskonlostfocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                            ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtbxdatepickerSED"
                            EmptyValueMessage="Please enter Season end date."
                            InvalidValueMessage="Invalid Season end date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="SeasonDescription" />
                               <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxdatepickerSED"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                   <%--  <td>SeasonRNumberChange:(*)</td>
                    <td>
                        
                        <asp:RadioButtonList ID="rdbtnSeason" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="-1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                      </td>--%>
                 <%--  <td>Sub Season:(**)</td>
                   <td><asp:DropDownList ID="drpdwnSubseason" runat="server"></asp:DropDownList>
                      
                    </td>--%>
                   
   
                </tr>
              <%-- <tr>
                   <td>SubSeason StartDate:(**)</td>
                    <td><asp:TextBox ID="txtbxdatepickerSSSD" runat="server"></asp:TextBox>
                          <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" CultureName="en-GB" runat="server" targetcontrolid="txtbxdatepickerSSSD" Mask="99-99-9999" clearmaskonlostfocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                            ControlExtender="MaskedEditExtender2"
                            ControlToValidate="txtbxdatepickerSSSD"
                            EmptyValueMessage="Please enterSub Season start date."
                            InvalidValueMessage="Invalid Sub Season start date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="SeasonDescription" />
                        <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" runat="server" TargetControlID="txtbxdatepickerSSSD"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>

                    </td>
                     
                    <td>SubSeason EndDate:(**)</td>
                  <td><asp:TextBox ID="txtbxdatepickerSSED" runat="server"></asp:TextBox>
                          <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender3" CultureName="en-GB" runat="server" targetcontrolid="txtbxdatepickerSSED" Mask="99-99-9999" clearmaskonlostfocus="false" MaskType="None"></asp:MaskedEditExtender>
                       <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                            ControlExtender="MaskedEditExtender3"
                            ControlToValidate="txtbxdatepickerSSED"
                            EmptyValueMessage="Please enter Sub Season end date."
                            InvalidValueMessage="Invalid Sub Season end date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$|(^__-__-____$))"
                            ValidationGroup="SeasonDescription" />
                        <asp:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4" runat="server" TargetControlID="txtbxdatepickerSSED"
                                                    Format="dd-MM-yyyy"></asp:CalendarExtender>

                    </td>
                    
                 </tr>--%>
               
     </table>
              <table align="center">
                     <tr>
                        <td><asp:Button ID="BtnSubmit" runat="server" Text="Add" 
                                onclick="BtnSubmit_Click" ValidationGroup="SeasonDescription" /></td>
                        <td><asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" /></td>
                         <td><asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"/></td>
                         <td><asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td><asp:Button ID="btnImport" runat="server" Text="Import"/>
                            <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </asp:ModalPopupExtender>
                        </td>
                        <td><asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"/>
                        </td>
                         <td><asp:Button ID="btnExportToday" runat="server" Text="Export Today" /></td>
                        <td><asp:Button ID="btnPrint" runat="server" Text="Print"/></td>
                        <td><input type="button" name="CloseMe" value="Close" onclick="closeMe()" /></td>
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
           <div id="dvgridview" style="height: 360px; width: 100%; overflow: auto;" runat="server">
              <asp:GridView ID="Gv_SeasonDescription" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="SeasonDescriptionID" OnSelectedIndexChanged="Gridview_OnSelectedIndexChanged">
                 <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" 
                     HorizontalAlign="Center" />
                <Columns>
                <asp:BoundField DataField="RowCount" HeaderText="RowCount" ItemStyle-Width="10" />
                <asp:TemplateField HeaderText="Center" ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnfieldStatus" Value='<%# Bind("Center") %>' />
                        <asp:LinkButton Text='<%# Bind("Center") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-Width="30" />
                <asp:BoundField DataField="Season" HeaderText="Season" ItemStyle-Width="30" />
                <asp:BoundField DataField="SeasonStartDt" HeaderText="Season Start Date" ItemStyle-Width="30" />
                <asp:BoundField DataField="SeasonEndDt" HeaderText="Season End Date" ItemStyle-Width="30" />
                    <%--<asp:BoundField DataField="SubSeason" HeaderText="SubSeason" ItemStyle-Width="30" />
                    <asp:BoundField DataField="SubSeasonStartDt" HeaderText="Sub Season Start Date" ItemStyle-Width="30" />
                    <asp:BoundField DataField="SubSeasonEndDt" HeaderText="Sub Season End Date" ItemStyle-Width="30" />--%>
                    <%--<asp:BoundField DataField="SeasonRNumberChange" HeaderText="Season RNo Change" ItemStyle-Width="30" />--%>
                    </Columns>
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


