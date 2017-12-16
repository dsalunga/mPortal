<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketManagerView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Incident.TicketManagerView" %>
<div class="wp-IncidentMgmt Item-List no-bottom-margin">
    <asp:HiddenField ID="hParameterSet" runat="server" Value="" />
    <div class="control-box">
        <div>
            <span id="dropDownPanel" runat="server">
                <input id="cmdNewTicket" class="btn btn-default btn-sm" runat="server" type="button"
                    value="New Ticket" />
            </span>
            <div class="pull-right">
                <asp:DropDownList ID="cboFilterBy" CssClass="input" ToolTip="TICKET FILTER - Filters all tickets by group"
                    AppendDataBoundItems="True" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboFilterBy_SelectedIndexChanged">
                    <asp:ListItem Text="ALL TICKETS" Value="-2"></asp:ListItem>
                    <asp:ListItem Text="My Group" Value="-2"></asp:ListItem>
                    <asp:ListItem Text="My Tickets" Selected="True" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboSLA" AppendDataBoundItems="True" CssClass="input" ToolTip="SLA STATUS - Filters all tickets by SLA Status"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSLA_SelectedIndexChanged">
                    <asp:ListItem Text="ALL SLA" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="On-track" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Warning" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Critical" Value="3"></asp:ListItem>
                    <asp:ListItem Text="N/A" Value="4"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboStatus" AppendDataBoundItems="True" CssClass="input" ToolTip="STATUS FILTER - Filters all tickets by Status"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                    <asp:ListItem Text="ALL STATUS" Value="-1"></asp:ListItem>
                    <asp:ListItem Selected="True" Text="All Open" Value="-2"></asp:ListItem>
                    <asp:ListItem Text="All Closed" Value="-3"></asp:ListItem>
                    <asp:ListItem Text="Submitted" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Assigned" Value="1"></asp:ListItem>
                    <asp:ListItem Text="In Progress" Value="2"></asp:ListItem>
                    <asp:ListItem Text="On Hold" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Completed" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="cmdRefresh" CssClass="btn btn-default btn-sm" runat="server" Text="Refresh" OnClick="cmdRefresh_Click" />
                &nbsp;
                    <asp:TextBox ID="txtSearch" Columns="25" CssClass="span2 input" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-default btn-sm" Text="Search" OnClick="cmdSearch_Click"
                    ClientIDMode="Static" />
                <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default btn-sm" ToolTip="Removes all custom filters and sets the default filters" ClientIDMode="Static" OnClick="cmdReset_Click" />
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="GridView1" CssClass="table table-condensed table-borderless grid-padding clear" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand"
            AllowPaging="True" PageSize="18" EmptyDataText="There are no tickets to display. Please click &quot;New Ticket&quot; to raise new request.">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="" Visible="false">
                    <HeaderStyle HorizontalAlign="center" Width="20px" />
                    <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:ImageButton runat="server" CommandName="ViewItem" ImageUrl="~/Images/Common/ico_edit.gif"
                            AlternateText="View details" ToolTip="View details" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TicketGuidDisplay" HeaderText="ID" SortExpression="TicketGuid"
                    HeaderStyle-HorizontalAlign="Left" HtmlEncode="false" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="UrgencyDisplay" HeaderText="Urgency" SortExpression="Urgency"
                    HeaderStyle-HorizontalAlign="Left" HtmlEncode="false" />
                <asp:BoundField DataField="Name" HeaderText="Requestor" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                    HtmlEncode="false" />
                <asp:BoundField DataField="AssignedUserDisplay" HeaderText="Assigned To" SortExpression="AssignedUser"
                    HeaderStyle-HorizontalAlign="Left" HtmlEncode="false" />
                <asp:BoundField DataField="AssignedGroup" HeaderText="Support Group" SortExpression="AssignedGroup"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DateCreated" HtmlEncode="true" HeaderText="Date Submitted"
                    SortExpression="DateCreated" DataFormatString="{0:dd-MMM h:mm tt}" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="StatusDisplay" HeaderText="Status" HtmlEncode="false"
                    SortExpression="Status" HeaderStyle-HorizontalAlign="Left" />
                <%--<asp:BoundField DataField="Duration" HtmlEncode="true" DataFormatString="{0:hh\:mm}"
                        HeaderText="Duration" SortExpression="Duration" HeaderStyle-HorizontalAlign="Left" />--%>
                <%--<asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <a href='<%# BuildUrl(Eval("Id")) %>' title="Click for more options">
                                <img src='<%# WCMS.WebSystem.WebParts.Registration.ProfileHelper.SetMakeUpStatusImage(Eval("Status")) %>'
                                    alt="" /></a>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
            </Columns>
            <RowStyle BackColor="#F5F5E6" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle HorizontalAlign="Left" CssClass="grid-pager" />
            <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="White" />
            <PagerSettings PageButtonCount="25" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
            TypeName="WCMS.WebSystem.WebParts.Incident.TicketManagerView">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboFilterBy" DefaultValue="-1" Name="filterBy" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="cboStatus" DefaultValue="-2" Name="status" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="cboSLA" DefaultValue="-1" Name="slaStatus" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hParameterSet" DefaultValue="" Name="parameterSet"
                    PropertyName="Value" />
                <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <br />
    <br />
    <div>
        <div style="float: left; width: 80px">
            <strong>SLA Status:</strong>
        </div>
        <div style="float: left; width: 90px">
            <div style="width: 15px; height: 15px; background-color: #25EF2B; float: left; margin-top: 2px; margin-right: 4px;">
            </div>
            On-track
        </div>
        <div style="float: left; width: 90px">
            <div style="width: 15px; height: 15px; background-color: #ffff00; float: left; margin-top: 2px; margin-right: 4px;">
            </div>
            Warning
        </div>
        <div style="float: left; width: 90px">
            <div style="width: 15px; height: 15px; background-color: #F44365; float: left; margin-top: 2px; margin-right: 4px;">
            </div>
            Critial
        </div>
        <div style="float: left;">
            <div style="width: 15px; height: 15px; background-color: #777; float: left; margin-top: 2px; margin-right: 4px;">
            </div>
            N/A
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
        });
    </script>
</div>
