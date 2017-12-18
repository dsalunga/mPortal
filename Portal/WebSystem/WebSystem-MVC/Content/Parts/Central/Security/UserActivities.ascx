<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserActivities.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.UserActivitiesView" %>
<%@ Register Src="../Controls/WebUserTab.ascx" TagName="WebUserTab" TagPrefix="uc1" %>

<uc1:WebUserTab ID="WebUserTab1" runat="server" />
<asp:HiddenField ID="hUserFormatString" runat="server" Value="" />
<asp:HiddenField ID="hUserId" runat="server" Value="-1" />
<div class="control-box no-bottom-margin">
    <div>
        <asp:DropDownList ID="cboGroupBy" CssClass="input" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboGroupBy_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="">* Grouped By *</asp:ListItem>
            <%--<asp:ListItem Value="User">User</asp:ListItem>--%>
            <asp:ListItem Value="Event">Event</asp:ListItem>
            <asp:ListItem Value="IPAddress">IP Address</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="cboPeriod" runat="server" CssClass="input" AutoPostBack="True">
        </asp:DropDownList>
    </div>
</div>
<div class="table-responsive">
    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
        <asp:View ID="viewDetailed" runat="server">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSourceGrid" ForeColor="#333333" GridLines="None"
                PageSize="100" AutoGenerateColumns="False" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="View" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="18px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img alt="View details of this item" src="/Content/Assets/Images/Common/ico_edit.gif" class="imagelink" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EventName" SortExpression="EventName" HeaderText="Activity"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="EventDate" SortExpression="EventDate" HeaderText="Date and Time"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="User" Visible="false" HtmlEncode="false" SortExpression="User" HeaderText="User" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="IPAddress" SortExpression="IPAddress" HeaderText="IP Address"
                        HeaderStyle-HorizontalAlign="Left" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    PageButtonCount="20" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Central.Security.UserActivitiesView">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboPeriod" Name="fromDate" PropertyName="SelectedValue"
                        Type="DateTime" />
                    <asp:ControlParameter ControlID="hUserFormatString" Name="userFormatString"
                        PropertyName="Value" />
                    <asp:ControlParameter ControlID="hUserId" ConvertEmptyStringToNull="true" DefaultValue="-1" Name="userId" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="viewGrouped" runat="server">
            <asp:GridView ID="GridViewGrouped" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSourceGrouped" ForeColor="#333333" GridLines="None"
                PageSize="50" AutoGenerateColumns="False" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ColumnValue" SortExpression="ColumnValue" HeaderText="Grouped Column"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Count" SortExpression="Count" HeaderText="Count" HeaderStyle-HorizontalAlign="Left" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    PageButtonCount="20" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSourceGrouped" runat="server" SelectMethod="SelectGrouped"
                TypeName="WCMS.WebSystem.WebParts.Central.Security.UserActivitiesView">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboPeriod" Name="fromDate" PropertyName="SelectedValue"
                        Type="DateTime" />
                    <asp:ControlParameter ControlID="cboGroupBy" Name="groupBy" PropertyName="SelectedValue"
                        Type="String" />
                    <asp:ControlParameter ControlID="hUserId" ConvertEmptyStringToNull="true" DefaultValue="-1" Name="userId" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>
    </asp:MultiView>
</div>
<div class="control-box no-bottom-margin" runat="server" id="panelDelete">
    <div>
        <asp:Button ID="cmdClearLog" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Do you really want to delete all audit entries in specified period?');"
            runat="server" Text="Delete" OnClick="cmdClearLog_Click" />
        &nbsp;
            <asp:DropDownList ID="cboDelete" runat="server" CssClass="input">
            </asp:DropDownList>
        <div class="pull-right">
            <asp:ImageButton ID="cmdDownload" runat="server" ImageUrl="~/Content/Assets/Images/excel_icon.gif"
                ToolTip="Download in XML format" OnClick="cmdDownload_Click" />
        </div>
    </div>
</div>
