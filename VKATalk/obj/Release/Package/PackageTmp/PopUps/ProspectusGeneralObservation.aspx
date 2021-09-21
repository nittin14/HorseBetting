<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProspectusGeneralObservation.aspx.cs" Inherits="VKATalk.PopUps.ProspectusGeneralObservation" %>
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
                    title: "Prospect General Observation",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function GetHorseID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdnfieldRelatedNameID.ClientID %>').value = HdnKey;
        }

        function SetContextKey() {
            $find('<%=AutoCompleteExtender4.ClientID%>').set_contextKey($get("<%=drpdwnRelatedType.ClientID %>").value);
            <%--$find('<%=AutoCompleteExtender4.ClientID%>').set_contextKey($get("<%=hdnfieldRelatedNameID.ClientID %>").value + ','
            + $get("<%=drpdwnRelatedType.ClientID %>").value);--%>
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager2" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Prospectus General Observation</h1>
        <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="PropGObservation" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
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
                    <td>Aimed Duration:(*)
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdbtnaimedduration" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Text="Short Term" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Long Term" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>  
                <tr>
                    <td>Observation:(*)

                    </td>
                    <td>
                        <asp:TextBox ID="txtbxObservation" runat="server" Width="1000px"></asp:TextBox>
                        <div id="listPlacement1" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="ObservationAutoFiller"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                           CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxObservation" CompletionListElementID="listPlacement1"
                                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="rqv" runat="server" ErrorMessage="Please enter Observation" ValidationGroup="PropGObservation" ControlToValidate="txtbxObservation">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Observation Related to Type:
                    </td>
                    <td><asp:DropDownList ID="drpdwnRelatedType" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Observation Related to Name:</td>
                     <td>
                                
                                <asp:TextBox ID="txtbxObservationRelatedName" runat="server" style="width: 480px;" onkeyup="SetContextKey()"></asp:TextBox>
                         <asp:HiddenField runat="server" ID="hdnfieldRelatedNameID" />
                         <div id="listPlacement55" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                           CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxObservationRelatedName" CompletionListElementID="listPlacement55"
                                                            OnClientItemSelected="GetHorseID"
                                                            ID="AutoCompleteExtender4" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                               <%-- <div id="div9" style="height: 300px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ServiceMethod="AddHorseList"
                                        MinimumPrefixLength="1" CompletionListCssClass="AutoExtender"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="100"
                                        TargetControlID="txtbxObservationRelatedName" CompletionListElementID="div9"
                                        OnClientItemSelected="GetHorseID"
                                        ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>--%>
                            </td>
                </tr>
               <tr>
                    <td>Reason:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxReason" runat="server" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="listPlacement" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="ReasonAutoFiller"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" 
                                                           CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxReason" CompletionListElementID="listPlacement"
                                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>From Date:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxFromDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="mskDateAvailable" CultureName="en-GB" runat="server" TargetControlID="txtbxFromDate" Mask="99-99-9999" 
                            ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                            ControlExtender="mskDateAvailable"
                            ControlToValidate="txtbxFromDate"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                           ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="PropGObservation" />
                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtbxFromDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Till Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxTillDate" runat="server"></asp:TextBox>
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
                            ValidationGroup="PropGObservation" />
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtbxTillDate"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                 
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComment" runat="server" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                       <asp:AutoCompleteExtender ServiceMethod="AddCommentList"
                                                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="50"
                                                            TargetControlID="txtbxComment" CompletionListElementID="Div1"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
                                                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="1"></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="PropGObservation" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="PropGObservation"/>    
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
             <div id="dvProspectus" style="height: 150px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvProspectus" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="GeneralObservationID" OnSelectedIndexChanged="GvProspectus_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Aimed Duration" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" id="hdnfieldObservation" Value='<%# Bind("AimedDuration") %>'/>
                                <asp:HiddenField runat="server" id="hdnfieldRelatedNameID" Value='<%# Bind("ObservationRelatedToNameID") %>'/>
                                <asp:LinkButton Text='<%# Bind("AimedDuration") %>' ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Observation" HeaderText="Observation" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="ObservationRelatedToType" HeaderText="ObservationRelatedToType" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ObservationRelatedToName" HeaderText="ObservationRelatedToName" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ObservationRelatedToDOB" HeaderText="ObservationRelatedToDOB" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="ObservationRelatedToRegNo" HeaderText="ObservationRelatedToRegNo" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Reason" HeaderText="Reason" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="TillDate" HeaderText="Till Date" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="MyComments" HeaderText="Comments" ItemStyle-Width="15%" />
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