<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Agent.TaskManager" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdNew" runat="server" Text="New Task" CssClass="btn btn-default btn-sm"
            OnClick="cmdNew_Click" />&nbsp;
                <asp:Button ID="cmdSync" runat="server" Text="Refresh" CssClass="btn btn-default btn-sm"
                    OnClick="cmdSync_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="input"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-default btn-sm" Text="Search" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="center" Width="40px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="StatusString" HeaderText="Recent Status" SortExpression="ExecutionStatus"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExecutionStartDate" HeaderText="Recent Start" SortExpression="ExecutionStartDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExecutionEndDate" HeaderText="Recent End" SortExpression="ExecutionEndDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="StartDate" HeaderText="Activation Date" SortExpression="StartDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="WeekdaysString" HeaderText="Weekdays" SortExpression="WeekdaysString"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="RecurrenceString" HeaderText="Recurrence" SortExpression="RecurrenceString"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="NextExecution" HeaderText="Next Execution" SortExpression="NextExecution"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Enabled" SortExpression="Enabled">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Enabled")) %>'
                        ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField Visible="false" DataField="TypeName" HeaderText="Type" SortExpression="TypeName"
                HeaderStyle-HorizontalAlign="Left" />
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.Agent.TaskManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>