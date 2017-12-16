<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminEvent.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminEvent" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="../../Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<%@ Register Src="../Central/Controls/SaveInFolder.ascx" TagName="SaveInFolder" TagPrefix="uc3" %>
<style type="text/css">
    #rblSendVia label {
        margin-right: 10px;
    }

    #gridRecipients tbody tr td {
        border-color: #ccc;
    }
</style>
<asp:HiddenField runat="server" ID="hRecipients" Value="" />
<asp:HiddenField runat="server" ID="hBaseGroup" ClientIDMode="Static" Value="" />
<uc1:TabControl ID="TabControl1" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
    runat="server" />
<asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
    <asp:View ID="viewGeneral" runat="server">
        <table width="100%" style="margin-top: 5px">
            <tr>
                <td style="width: 85px">Subject:
                    <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                        ErrorMessage="Subject" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="input" Columns="70"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">Message:
                </td>
                <td>
                    <FCKeditorV2:FCKeditor ID="txtMessage" runat="server" Height="350px" ToolbarSet="Default">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr>
                <td valign="top">Location:
                </td>
                <td style="vertical-align: top">
                    <table style="border-collapse: collapse">
                        <tr class="min-bottom-margin">
                            <td>
                                <asp:DropDownList ID="cboLocations" ClientIDMode="Static" CssClass="input" runat="server" DataTextField="Name"
                                    DataValueField="Id">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="cmdCheckLocation" ClientIDMode="Static" runat="server" CssClass="btn btn-default btn-sm" Text="Check availability"
                                    OnClick="cmdCheckLocation_Click" />&nbsp;<span id="lblLocationAvailability" runat="server"
                                        enableviewstate="false"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="chkOtherLocation" CssClass="aspnet-checkbox" ClientIDMode="Static" runat="server" Text="Other:" />
                                <asp:TextBox ID="txtLocation" ClientIDMode="Static" runat="server" CssClass="input" Columns="35"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Starts:
                </td>
                <td class="min-bottom-margin">
                    <table style="border-collapse: collapse">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" Columns="30" CssClass="input"></asp:TextBox>
                            </td>
                            <td>
                                <img onclick="ShowDateTimePicker(WCMS.Dom.Get('<% =txtStartDate.ClientID %>'));"
                                    alt="Select date/time..." title="Select date/time..." src="../../Assets/Images/calendar.gif"
                                    style="width: 17px; height: 17px; cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>Ends:
                </td>
                <td class="min-bottom-margin">
                    <table style="border-collapse: collapse">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" Columns="30" CssClass="input"></asp:TextBox>
                            </td>
                            <td>
                                <img onclick="ShowDateTimePicker(WCMS.Dom.Get('<% =txtEndDate.ClientID %>'));" alt="Select date/time..."
                                    title="Select date/time..." src="../../Assets/Images/calendar.gif" style="width: 17px; height: 17px; cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>Calendar:
                </td>
                <td>
                    <asp:DropDownList ID="cboCalendar" DataTextField="Name" CssClass="input" DataValueField="Id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="panelSaveTo" runat="server" visible="false">
                <td>Save to:
                </td>
                <td>
                    <uc3:SaveInFolder ID="SaveInFolder1" runat="server" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewRecurrence" runat="server">
        <table style="width: 100%">
            <tr>
                <td valign="top" colspan="2">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <fieldset>
                                    <legend>Recurrence</legend>
                                    <asp:RadioButtonList ID="rblRecurrence" CssClass="aspnet-radio" ClientIDMode="Static" runat="server" DataTextField="Name"
                                        DataValueField="Id">
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                            <td style="width: 25px">&nbsp;
                            </td>
                            <td valign="top">
                                <span id="spanWeekdays">
                                    <fieldset>
                                        <legend>Weekdays</legend>
                                        <asp:CheckBoxList ID="cblWeekdays" CssClass="aspnet-checkbox" runat="server">
                                        </asp:CheckBoxList>
                                    </fieldset>
                                </span>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 110px; vertical-align: top">Recurrence Ends:
                </td>
                <td style="vertical-align: text-top">
                    <table style="border-collapse: collapse;">
                        <tr class="min-bottom-margin">
                            <td>
                                <asp:TextBox ID="txtRepeatUntil" ClientIDMode="Static" CssClass="input" runat="server" Columns="30"></asp:TextBox>
                            </td>
                            <td>
                                <img id="imgRepeatUntilPicker" onclick="ShowDateTimePicker(WCMS.Dom.Get('<% =txtRepeatUntil.ClientID %>'));"
                                    alt="Select date/time..." longdesc="Select date/time..." src="../../Assets/Images/calendar.gif"
                                    style="width: 17px; height: 17px; cursor: pointer" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox Enabled="false" ID="chkNoEnd" CssClass="aspnet-checkbox" Text="Does not end" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewReminder" runat="server">
        <table style="width: 100%; margin-top: 5px">
            <tr>
                <td style="width: 90px">Send Before:
                </td>
                <td>
                    <asp:DropDownList ID="cboReminderBefore" runat="server" CssClass="input">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td valign="top">
                    Email Template:
                </td>
                <td>
                    <asp:DropDownList ID="cboTemplates" runat="server">
                    </asp:DropDownList>
                    &nbsp;<a href="#">Preview</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td valign="top">
                    Send To:
                </td>
                <td>
                    <asp:TextBox ID="txtReminderToEmail" runat="server" Columns="75" Rows="5" TextMode="MultiLine"></asp:TextBox>&nbsp;<input
                        id="cmdBrowse" onclick="ShowAccountBrowser('<% =txtReminderToEmail.ClientID %>', 21, 1, 1);"
                        type="button" value="Select..." />
                </td>
            </tr>--%>
            <tr>
                <td>Send Via:
                </td>
                <td>
                    <asp:RadioButtonList ID="rblSendVia" runat="server" CssClass="aspnet-radio" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" AutoPostBack="True" ClientIDMode="Static" Font-Bold="False">
                        <asp:ListItem Value="1">SMS</asp:ListItem>
                        <asp:ListItem Value="0">E-mail</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">SMS & E-mail</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <div id="panelRecipientMultiple" runat="server">
            <br />
            <div class="no-bottom-margin">
                <asp:TextBox ID="txtAdd" runat="server" ClientIDMode="Static" CssClass="input" Columns="50"></asp:TextBox>
                <asp:Button ID="cmdAdd" CssClass="btn btn-default btn-sm" runat="server" ClientIDMode="Static" OnClientClick="return Add_Click();" Text="Add..." OnClick="cmdAdd_Click" />
                <asp:Button ID="cmdReset" runat="server" CssClass="btn btn-default btn-sm" Text="Reset" OnClick="cmdReset_Click" />
            </div>
            <div class="table-responsive">
                <asp:GridView ClientIDMode="Static" CssClass="table table-borderless" ID="gridRecipients" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" Width="700px" CellPadding="2" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
                    ForeColor="#333333" OnRowCommand="GridView1_RowCommand" PageSize="7" EmptyDataText="There are no recipients. Please select recipients by clicking the Add button."
                    AllowPaging="True" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="Solid">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                            HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle HorizontalAlign="center" Width="20px" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                    CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to remove this item?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PageButtonCount="25" />
                </asp:GridView>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRecipients"
                TypeName="WCMS.WebSystem.WebParts.EventCalendar.AdminEvent">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hRecipients" Name="customRecipients" PropertyName="Value"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
        </div>
        <br />
    </asp:View>
    <asp:View ID="viewMisc" runat="server">
        <table width="100%" style="margin-top: 5px">
            <tr id="panelCategory" runat="server" visible="true">
                <td style="width: 125px">Category:
                </td>
                <td>
                    <asp:DropDownList ID="cboCategory" CssClass="input" runat="server" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Template/Theme:
                </td>
                <td>
                    <asp:DropDownList ID="cboTemplates" CssClass="input" DataTextField="Name" DataValueField="Id" runat="server"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<br />
<span id="lblStatus" runat="server" style="color: Red;" enableviewstate="false"></span>
<br />
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
        <div class="pull-right" runat="server" visible="false">
            <asp:Button CausesValidation="false" ID="cmdSendReminder" CssClass="btn btn-default" runat="server"
                Text="Send Reminder" OnClientClick="return confirm('Are you sure you want to send a reminder now?');"
                Visible="false" OnClick="cmdSendReminder_Click" />
            &nbsp;
                <asp:Button CausesValidation="false" ID="cmdDelete" CssClass="btn btn-default" runat="server"
                    Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                    OnClick="cmdDelete_Click" Visible="false" />
        </div>
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
<script type="text/javascript" src="/Content/Parts/EventCalendar/AdminEvent.ascx.js"></script>
