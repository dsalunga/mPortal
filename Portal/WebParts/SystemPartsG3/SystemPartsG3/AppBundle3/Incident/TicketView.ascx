<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Incident.TicketView" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<style type="text/css">
    .ticket-field {
        float: left;
        margin-right: 20px;
        min-width: 385px;
    }

    .ticket-field-input-one {
        float: left;
        width: 240px;
    }

    .ticket-field-label {
        width: 125px;
        float: left;
    }

    .ticket-field-label2 {
        width: 100px;
        float: left;
    }

    .ticket-row {
        float: left;
        clear: both;
        margin-bottom: 5px;
    }

    pre.content {
        background-color: #fff;
        color: #333;
        font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helvetica, sans-serif;
        padding: 0px;
        margin: 0px; /* overflow-x: auto; */ /* Use horizontal scroller if needed; for Firefox 2, not needed in Firefox 3 */
        white-space: pre-wrap; /* css-3 */
        white-space: -moz-pre-wrap !important; /* Mozilla, since 1999 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: -o-pre-wrap; /* Opera 7 */ /*width: 99%; */
        word-wrap: break-word; /* Internet Explorer 5.5+ */
        border: none;
        border-radius: initial;
    }

    .fileUploadBox {
        display: block;
        margin-bottom: 0.96em;
        width: 500px;
    }

    div.alert-panel {
        font-size: larger;
        color: White;
        background-color: #FF0080;
        padding: 2px;
        display: inline;
        font-weight: bold;
    }

    div.notification {
        background-color: #348017;
    }
</style>
<div class="wp-IncidentMgmt Item-View">
    <asp:HiddenField runat="server" ID="hRecipients" Value="" />
    <asp:HiddenField ID="hBatchGuid" runat="server" Value="" />
    <asp:HiddenField ID="hUserDisplayFormatString" runat="server" Value="" />
    <asp:HiddenField runat="server" ID="hRequestor" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="hSubmitter" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="hBaseGroup" ClientIDMode="Static" Value="" />
    <div runat="server" id="panelError" visible="false">
        <div class="alert-panel" id="panelAlert" runat="server" clientidmode="Static">
            <span runat="server" id="lblError"></span>
        </div>
        <br />
        <br />
    </div>
    <uc1:TabControl ID="TabControl1" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
        runat="server" />
    <br />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewGeneral" runat="server">
            <div class="wp-IncidentMgmt-General">
                <div class="ticket-row">
                    <div class="ticket-field">
                        <div class="ticket-field-label">
                            Ticket ID:&nbsp;
                        </div>
                        <div class="ticket-field-input-one">
                            <strong runat="server" id="lblTicketGuid" clientidmode="Static" style="float: left">&lt;PENDING&gt;</strong>
                        </div>
                    </div>
                    <div runat="server" id="panelSubmitter" visible="false" style="float: left;">
                        <div class="ticket-field-label2">
                            <label>
                                Submitter:&nbsp;</label>
                        </div>
                        <strong runat="server" id="lblSubmitter"></strong>
                    </div>
                </div>
                <br />
                <div class="ticket-row">
                    <div class="ticket-field">
                        <div class="ticket-field-label">
                            <label>
                                Date Submitted:&nbsp;</label>
                        </div>
                        <div class="ticket-field-input-one">
                            <strong style="float: left" runat="server" id="lblDateEntered"></strong>
                        </div>
                    </div>
                    <div style="float: left;">
                        <div class="ticket-field-label2">
                            <label>
                                Requestor:&nbsp;</label>
                        </div>
                        <strong runat="server" id="lblEnteredBy"></strong><span runat="server" id="panelChangeRequestor"
                            visible="false">&nbsp;(<a href="#" title="Change Requestor" onclick="ChangeClick();"><strong
                                style="color: #DA4D4D">Change</strong></a>)<asp:Button ID="cmdChangeRequestor" CausesValidation="false"
                                    ClientIDMode="Static" OnClick="cmdChangeRequestor_Click" runat="server" Style="display: none" /></span>
                    </div>
                </div>
                <div class="ticket-row">
                    <div class="ticket-field">
                        <div runat="server" id="panelRunningTime">
                            <div class="ticket-field-label">
                                <label>
                                    Running Time:&nbsp;</label>
                            </div>
                            <div class="ticket-field-input-one">
                                <strong style="float: left" runat="server" id="lblRunningTime">&lt;PENDING&gt;</strong>
                            </div>
                        </div>
                    </div>
                    <div class="ticket-field" runat="server" id="panelETA">
                        <div class="ticket-field-label2">
                            <label>
                                ETA:&nbsp;</label>
                        </div>
                        <div style="float: left">
                            <table runat="server" id="datePickerETA" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtETA" ClientIDMode="Static" CssClass="input" runat="server" Columns="30"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img onclick="ShowDateTimePicker(WCMS.Dom.Get('<% =txtETA.ClientID %>'));" alt="Select date/time..."
                                            title="Select date/time..." src="/Content/Assets/Images/calendar.gif" style="width: 17px; height: 17px; padding: 5px; cursor: pointer; box-sizing: content-box;" />
                                    </td>
                                </tr>
                            </table>
                            <strong runat="server" id="lblETA"></strong>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <div>
                    <label for="txtDescription">
                        Description:</label>
                    <br />
                    <asp:TextBox ID="txtDescription" ClientIDMode="Static" runat="server" Columns="80"
                        Rows="16" TextMode="MultiLine" Width="550px" CssClass="input"></asp:TextBox>
                    <pre runat="server" id="lblDescription" class="content" visible="false" style="border: 1px solid #bbb; padding: 3px; width: 550px"></pre>
                </div>
                <br />
                <div class="ticket-row">
                    <div class="ticket-field">
                        <div class="ticket-field-label">
                            <label for="cboCategory">
                                Request Category:</label>
                        </div>
                        <div class="ticket-field-input-one">
                            <asp:DropDownList ID="cboCategory" DataTextField="Name" DataValueField="Id" runat="server"
                                AppendDataBoundItems="true" CssClass="input">
                                <asp:ListItem Value="-1" Text="* SELECT *"></asp:ListItem>
                            </asp:DropDownList>
                            <strong runat="server" id="lblCategory" visible="false"></strong>
                        </div>
                    </div>
                    <div class="ticket-field" runat="server" id="panelStatus" clientidmode="Static">
                        <div class="ticket-field-label2">
                            <label for="cboStatus">
                                Status:</label><br />
                        </div>
                        <div style="float: left">
                            <asp:DropDownList ID="cboStatus" DataTextField="Name" DataValueField="Id" runat="server"
                                AppendDataBoundItems="true" CssClass="input">
                                <asp:ListItem Value="-1" Text="* SELECT *"></asp:ListItem>
                            </asp:DropDownList>
                            <strong runat="server" id="lblStatus" visible="false"></strong>
                        </div>
                    </div>
                </div>
                <br />
                <div class="ticket-row">
                    <div class="ticket-field">
                        <div class="ticket-field-label">
                            <label for="cboType">
                                Request Type:</label>
                        </div>
                        <div class="ticket-field-input-one">
                            <asp:DropDownList ID="cboType" DataTextField="Name" DataValueField="Id" runat="server"
                                AppendDataBoundItems="true" CssClass="input">
                            </asp:DropDownList>
                            <strong runat="server" id="lblType" visible="false"></strong>
                        </div>
                    </div>
                    <div class="ticket-field" runat="server" id="panelAssignedTo" clientidmode="Static">
                        <div class="ticket-field-label2">
                            <label for="cboAssignedUser">
                                Assigned To:</label>
                        </div>
                        <div style="float: left">
                            <asp:DropDownList ID="cboAssignedUser" DataTextField="Name" DataValueField="Id" runat="server"
                                AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="cboAssignedUser_SelectedIndexChanged" CssClass="input">
                                <asp:ListItem Value="-1" Text="* SELECT *"></asp:ListItem>
                            </asp:DropDownList>
                            <strong runat="server" id="lblAssignedUser" visible="false"></strong>
                        </div>
                    </div>
                </div>
                <div class="ticket-row">
                    <div class="ticket-field">
                        <div class="ticket-field-label">
                            <label for="cboUrgency">
                                Urgency:</label>
                        </div>
                        <div class="ticket-field-input-one">
                            <asp:DropDownList ID="cboUrgency" DataTextField="Name" DataValueField="Id" runat="server"
                                AppendDataBoundItems="true" CssClass="input">
                            </asp:DropDownList>
                            <strong runat="server" id="lblUrgency" visible="false"></strong>
                        </div>
                    </div>
                    <div id="panelSupportGroup" class="ticket-field" runat="server" clientidmode="Static">
                        <div class="ticket-field-label2">
                            <label for="cboGroup">
                                Support Group:</label>
                        </div>
                        <div style="float: left">
                            <strong id="lblAssignedToGroup" runat="server">&lt;PENDING&gt;</strong>
                        </div>
                    </div>
                </div>

            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:View>
        <asp:View ID="viewNotes" runat="server">
            <div runat="server" id="panelNewNote">
                <asp:Label ID="lblNewNote" runat="server" AssociatedControlID="txtNewNote" Text="New note:"></asp:Label>
                <br />
                <asp:TextBox ID="txtNewNote" runat="server" CssClass="span5 input" Columns="75" Rows="7" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:CheckBox ID="chkSendSMS" CssClass="aspnet-checkbox" runat="server" Text="Send SMS Alert" />
                <br />
                <br />
            </div>
            <div class="wp-IncidentMgmt-Notes">
                <asp:GridView ID="GridViewNotes" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="0" ShowHeader="false" DataKeyNames="Id" DataSourceID="ObjectDataSourceNotes"
                    ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" PageSize="5"
                    EmptyDataText="No notes to display.">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Comments">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <strong>
                                    <%# Eval("User") %>&nbsp;@&nbsp;<%# Eval("DateCreated", "{0:dd-MMM-yyyy h:mm tt}") %></strong><br />
                                <pre class="content"><%# Eval("Content") %></pre>
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" CssClass="grid-pager" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PageButtonCount="25" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceNotes" runat="server" SelectMethod="SelectNotes"
                    TypeName="WCMS.WebSystem.WebParts.Incident.TicketView">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ticketId" QueryStringField="TicketId"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="hUserDisplayFormatString" DefaultValue="" Name="userDisplayFormat"
                            PropertyName="Value" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </asp:View>
        <asp:View ID="viewAttachments" runat="server">
            <div class="wp-IncidentMgmt-Attachments">
                <div runat="server" id="panelAddAttachments" clientidmode="Static">
                    <div id="panelToAddAttachments" class="min-bottom-margin">
                        <input id="cmdAddAttachments" class="btn btn-default btn-sm" type="button" value="Add Attachments" />
                    </div>
                    <div id="panelUploadAttachments" style="display: none">
                        <div id="fileUploads" runat="server" clientidmode="Static">
                            <div class="field-row">
                                <strong>Select files to upload as attachments then click "Attach Files"</strong>
                            </div>
                            <asp:FileUpload ID="fileUpload1" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
                                runat="server" />
                            <asp:FileUpload ID="fileUpload2" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
                                runat="server" />
                            <asp:FileUpload ID="fileUpload3" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
                                runat="server" />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="cmdAttachmentUpload" CssClass="btn btn-default btn-sm" Width="100px" runat="server" Text="Attach Files"
                                OnClick="cmdAttachmentUpload_Click" />
                            <input id="cmdAttachmentCancel" style="width: 75px" class="btn btn-default btn-sm" type="button" value="Cancel" />
                        </div>
                        <br />
                        <div>
                            <p>
                                <strong>Note</strong>&nbsp;&nbsp;Upload size is limited to 100 MB per file.
                            </p>
                        </div>
                    </div>
                </div>
                <div id="panelAttachments" class="wp-IncidentMgmt-Attachments">
                    <asp:GridView ID="GridViewAttachments" CssClass="gridview-ctrl" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Id" DataSourceID="ObjectDataSourceAttachments"
                        ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="There are no attachments in this ticket."
                        OnRowCommand="GridViewAttachments_RowCommand" PageSize="20">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Download-File" ImageUrl="~/Content/Assets/Images/Common/txt.gif"
                                        AlternateText="View details" ToolTip="View details" CommandArgument='<%# Eval("Id") %>'
                                        Visible="false" /><asp:LinkButton runat="server" Font-Bold="true" ID="LinkButton1"
                                            CommandName="Download-File" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SizeString" HeaderText="Size" SortExpression="Size">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TypeName" HeaderText="Type" SortExpression="TypeName">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DateUploaded" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Date Uploaded" SortExpression="DateUploaded" DataFormatString="{0:dd-MMM-yyyy h:mm tt}" />
                            <asp:BoundField DataField="UploadedBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Uploaded By" SortExpression="UploadedBy" HtmlEncode="false" />
                            <asp:TemplateField HeaderText="Actions">
                                <HeaderStyle HorizontalAlign="center" Width="43px" />
                                <ItemStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Download-File" ImageUrl="~/Content/Assets/Images/Common/ico_diskette.gif"
                                        AlternateText="Download" ToolTip="Download" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton
                                            ID="ImageButton3" CommandName="Delete-File" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                                            CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#F5F5E6" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerSettings PageButtonCount="25" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourceAttachments" runat="server" SelectMethod="SelectAttachments"
                        TypeName="WCMS.WebSystem.WebParts.Incident.TicketView">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="-1" Name="ticketId" QueryStringField="TicketId"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="hBatchGuid" Name="batchGuid" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="hUserDisplayFormatString" DefaultValue="" Name="userDisplayFormat"
                                PropertyName="Value" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </asp:View>
        <asp:View ID="viewHistory" runat="server">
            <div class="wp-IncidentMgmt-History">
                <asp:GridView ID="GridViewHistory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="0" ShowHeader="false" DataKeyNames="Id" DataSourceID="ObjectDataSourceHistory"
                    ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" PageSize="5"
                    EmptyDataText="No history to display.">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Comments">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <strong>
                                    <%# Eval("User") %>&nbsp;@&nbsp;<%# Eval("DateCreated", "{0:dd-MMM-yyyy h:mm tt}") %></strong><br />
                                <pre class="content"><%# Eval("Content") %></pre>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" CssClass="grid-pager" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PageButtonCount="25" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceHistory" runat="server" SelectMethod="SelectHistory"
                    TypeName="WCMS.WebSystem.WebParts.Incident.TicketView">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ticketId" QueryStringField="TicketId"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="hUserDisplayFormatString" DefaultValue="" Name="userDisplayFormat"
                            PropertyName="Value" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </asp:View>
        <asp:View ID="viewNotifyAlso" runat="server">
            <div runat="server" id="panelNotifyAlsoControls" class="no-bottom-margin" style="margin-bottom: 10px;">
                <asp:TextBox ID="txtAdd" runat="server" ClientIDMode="Static" Columns="50" CssClass="input"></asp:TextBox>
                <asp:Button ID="cmdAddAlsoNotify" CssClass="btn btn-default btn-sm" runat="server" ClientIDMode="Static" OnClientClick="return AddNotifyAlso_Click();"
                    Width="60px" Text="Add..." OnClick="cmdAddNotifyAlso_Click" />
                <asp:Button ID="cmdReset" runat="server" CssClass="btn btn-default btn-sm" Text="Reset" OnClick="cmdReset_Click" />
            </div>
            <asp:GridView ClientIDMode="Static" ID="gridRecipients" CssClass="gridview-ctrl" runat="server" AllowSorting="True"
                AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
                ForeColor="#333333" Width="100%" OnRowCommand="GridViewAlsoNotify_RowCommand"
                PageSize="12" EmptyDataText="<span style='background-color:yellow'>There are no additional notification recipients.</span>"
                AllowPaging="True" EnableTheming="False" BorderColor="#CCCCCC"
                GridLines="None">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="E-mail" SortExpression="Email" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MobileNumber" HeaderText="Mobile Number" SortExpression="MobileNumber"
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
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetNotifyAlsoRecipients"
                TypeName="WCMS.WebSystem.WebParts.Incident.TicketView">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hRecipients" Name="customRecipients" PropertyName="Value"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
        </asp:View>
    </asp:MultiView>
    <br />
    <div id="buttonBarRow">
        <div id="buttonBar" class="buttonBar control-box">
            <div>
                <asp:Button ID="cmdUpdate" CssClass=" btn btn-primary" runat="server" Text="Update"
                    OnClick="cmdUpdate_Click" />
                <input id="cmdCancel" class="btn btn-default" runat="server"
                    type="button" value="Cancel" />
                &nbsp;
            <asp:CheckBox ID="chkSendSMSNew" CssClass="aspnet-checkbox" runat="server" AutoPostBack="true" Visible="false"
                Text="Send SMS Alert" OnCheckedChanged="chkSendSMSNew_CheckedChanged" />
            </div>
        </div>
    </div>
    <br />
    <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following fields are required:"
        ShowMessageBox="True" ShowSummary="False" />
    <script type="text/javascript">
        $(document).ready(function () {
            var panelAddAttachments = $("#panelAddAttachments");
            if (panelAddAttachments.length > 0) {
                ShowUploadAttachments(false);

                $("#cmdAddAttachments").click(function () {
                    ShowUploadAttachments(true);
                });

                $("#cmdAttachmentCancel").click(function () {
                    ShowUploadAttachments(false);
                });
            }
        });

        function ChangeClick() {
            var baseGroup = $("#hBaseGroup").val();
            ShowAccountBrowser('hRequestor', 21, 1, 0, 0, 'cmdChangeRequestor', baseGroup);
        }

        function ShowUploadAttachments(displayUpload) {
            $("#panelUploadAttachments").css("display", displayUpload ? "" : "none");
            $("#panelToAddAttachments").css("display", !displayUpload ? "" : "none");

            $("#panelAttachments").css("display", !displayUpload ? "" : "none");
        }

        function AddNotifyAlso_Click() {
            var addValue = $("#txtAdd").val().Trim();
            var baseGroup = $("#hBaseGroup").val();

            if (addValue == "") {
                ShowAccountBrowser("txtAdd", -1, 1, 1, 1, "cmdAddAlsoNotify", baseGroup);
                return false;
            }

            return true;
        }
    </script>
</div>
