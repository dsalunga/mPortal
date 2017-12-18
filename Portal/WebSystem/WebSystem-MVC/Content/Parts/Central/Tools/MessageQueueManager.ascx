<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageQueueManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.MessageQueueManager" %>
<asp:HiddenField ID="hUserFormatString" runat="server" Value="" />
<div class="control-box no-bottom-margin">
    <div>
        Group By:&nbsp;<asp:DropDownList ID="cboGroupBy" CssClass="input" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="cboGroupBy_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="">* None *</asp:ListItem>
            <asp:ListItem Value="User">User</asp:ListItem>
            <asp:ListItem Value="Event">Event</asp:ListItem>
            <asp:ListItem Value="IPAddress">IP Address</asp:ListItem>
        </asp:DropDownList>&nbsp;Starting:&nbsp;<asp:DropDownList ID="cboPeriod" CssClass="input" runat="server" AutoPostBack="True"></asp:DropDownList>
    </div>
</div>
<div class="table-responsive">
    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
        <asp:View ID="viewDetailed" runat="server">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-condensed table-borderless"
                CellPadding="4" DataSourceID="ObjectDataSourceGrid" ForeColor="#333333" GridLines="None"
                PageSize="20" AutoGenerateColumns="False" Width="100%" DataKeyNames="Id"
                OnRowCommand="GridView1_RowCommand">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="View">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img alt="View details of this item" src="/Content/Assets/Images/Common/ico_edit.gif"
                                class="imagelink" />
                            <asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Custom_Delete"
                                ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" AlternateText="Delete"
                                OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="User" HtmlEncode="false" SortExpression="User" HeaderText="Sender"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="To" SortExpression="To" HeaderText="To"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="ToFailed" SortExpression="ToFailed" HeaderText="ToFailed"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="EmailSubject" HtmlEncode="false" SortExpression="EmailSubject"
                        HeaderText="Email-Subject"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="EmailMessage" HtmlEncode="false" SortExpression="EmailMessage"
                        HeaderText="Email-Message"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="SmsMessage" HtmlEncode="false" SortExpression="SmsMessage"
                        HeaderText="SMS-Message"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DateCreated" SortExpression="DateCreated" HeaderText="Date-Created"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DateSent" SortExpression="DateSent" HeaderText="Date-Sent"
                        HeaderStyle-HorizontalAlign="Left" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    PageButtonCount="25" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Central.Tools.MessageQueueManager">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboPeriod" Name="fromDate" PropertyName="SelectedValue"
                        Type="DateTime" ConvertEmptyStringToNull="true" />
                    <asp:ControlParameter ControlID="hUserFormatString" Name="userFormatString"
                        PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="viewGrouped" runat="server">
            <asp:GridView ID="GridViewGrouped" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
                CellPadding="4" DataSourceID="ObjectDataSourceGrouped" ForeColor="#333333" GridLines="None"
                PageSize="20" AutoGenerateColumns="False" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ColumnValue" SortExpression="ColumnValue" HeaderText="Grouped Column"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Count" SortExpression="Count" HeaderText="Count" HeaderStyle-HorizontalAlign="Left" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    PageButtonCount="25" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSourceGrouped" runat="server" SelectMethod="SelectGrouped"
                TypeName="WCMS.WebSystem.WebParts.Central.Tools.MessageQueueManager">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboPeriod" Name="fromDate" PropertyName="SelectedValue"
                        Type="DateTime" ConvertEmptyStringToNull="true" />
                    <asp:ControlParameter ControlID="cboGroupBy" Name="groupBy" PropertyName="SelectedValue"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>
    </asp:MultiView>
</div>
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdClearLog" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Do you really want to delete all audit entries in specified period?');"
            runat="server" Text="Delete" OnClick="cmdClearLog_Click" />
        &nbsp;<asp:DropDownList ID="cboDelete" CssClass="input" runat="server"></asp:DropDownList>
        &nbsp;<asp:ImageButton ID="cmdDownload" runat="server" ImageUrl="~/Content/Assets/Images/excel_icon.gif"
            ToolTip="Download in XML format" OnClick="cmdDownload_Click" />
    </div>
</div>
