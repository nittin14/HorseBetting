<%@ Page AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProfessionalImportantDates.aspx.cs" Inherits="VKATalk.PopUps.ProfessionalImportantDates" Language="C#" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <title></title>
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
      <script type="text/javascript">
          function ShowPopup(message) {
              $(function () {
                  $("#dialog").html(message);
                  $("#dialog").dialog({
                      title: "Professional ImportantDates",
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h1 style="text-align: center; font-size: xx-large; font-weight: bold; text-decoration: underline;">Add Professional Important Dates</h1>
       <div id="dialog" style="display: none">
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="VSummary" runat="server" ValidationGroup="ImportantDates" ShowMessageBox="true" ShowSummary="false" Font-Names="verdana"
                            Font-Size="12" />
                    </td>
                </tr>
               <tr>
                    <td>Profesional Name:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblProfessionalNameFirst"></asp:Label>
                        <asp:HiddenField runat="server" id="hdnfieldProfessionalName"/>
                        <asp:HiddenField runat="server" ID="professionalId"/>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblProfessionalNameSecond"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Date:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxDates" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom" runat="server" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" CultureName="en-GB" runat="server" TargetControlID="txtbxDates" 
                            Mask="99-99-9999" ClearMaskOnLostFocus="false" MaskType="None"></asp:MaskedEditExtender>
                         <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                            ControlExtender="MaskedEditExtender2"
                            ControlToValidate="txtbxDates"
                            EmptyValueMessage="Please enter date."
                            InvalidValueMessage="Invalid date format."
                            Display="Dynamic"
                            IsValidEmpty="true"
                            InvalidValueBlurredMessage="*"
                            ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"
                            ValidationGroup="ImportantDates" />
                        <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" runat="server" TargetControlID="txtbxDates"
                            Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Occassion:(*)
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdwnOcassion" runat="server"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtbxOccassion" runat="server"></asp:TextBox>
                        <div id="Div1" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddOcassionList"
                            MinimumPrefixLength="0" CompletionListCssClass="AutoExtender" 
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxOccassion" CompletionListElementID="Div1"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-1" ErrorMessage="Please select Occassion." 
                            ValidationGroup="ImportantDates" ControlToValidate="drpdwnOcassion">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Related To:(*)
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdwnRelatedTo" runat="server"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtbxRelated" runat="server"></asp:TextBox>
                        <div id="Div2" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddRelatedToList"
                            MinimumPrefixLength="0" CompletionListCssClass="AutoExtender" 
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxRelated" CompletionListElementID="Div2"
                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-1" 
                            ErrorMessage="Please select Related To." ValidationGroup="ImportantDates" ControlToValidate="drpdwnRelatedTo">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Related To Name:(*)
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxRelatedtoName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Related To Name." ValidationGroup="ImportantDates" ControlToValidate="txtbxRelatedtoName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr>
                    <td>My Comments:
                    </td>
                    <td colspan="3">
                       <asp:TextBox ID="txtbxComment" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        <div id="listPlacement" style="height:300px; overflow-y:scroll;" ></div>
                          <asp:AutoCompleteExtender ServiceMethod="AddCommentsList"
                            MinimumPrefixLength="1" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtbxComment" CompletionListElementID="listPlacement"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="btnSave_Click" ValidationGroup="ImportantDates" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" ValidationGroup="ImportantDates"/>
                        <asp:Button runat="server" id="btnDelete" text="Delete" OnClick="btnDelete_Click" ValidationGroup="ImportantDates"/>    
                        <asp:Button ID="btnImport" runat="server" Text="Import" />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnImport"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
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
            <div id="dvHomeDistance" style="height: 350px; width: 100%; overflow: auto;" runat="server">
                <asp:GridView ID="GvHomeDistance" runat="server" Width="100%"
                    AutoGenerateColumns="False" DataKeyNames="ImportantDatesID" OnSelectedIndexChanged="GvHomeDistance_OnSelectedIndexChanged">
                    <EmptyDataRowStyle Font-Names="Calibri" Font-Size="Medium" ForeColor="Red"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                            <ItemTemplate>
                                 <asp:HiddenField runat="server" id="hdnfieldStatus" Value='<%# Bind("Dates") %>'/>
                                 <asp:LinkButton Text='<%# Bind("Dates") %>' ID="lnkSelect" runat="server" CommandName="Select" /> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Ocassion" HeaderText="Occassion" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="Relation" HeaderText="Related To" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="RelatedToName" HeaderText="Related To Name" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="MyComments" HeaderText="My Comments" ItemStyle-Width="40%" />
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