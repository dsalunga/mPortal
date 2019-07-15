<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberSessions.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.MakeUpView.MemberSessionView" %>
<asp:HiddenField ID="hUserFormatString" runat="server" Value="" />
<style type="text/css">
    .req-data {
        color: #ccc;
    }
</style>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="UserId" CssClass="table table-borderless table-condensed" OnRowCommand="GridView1_RowCommand"
        PageSize="35" EmptyDataText="No sessions found.">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Name" HtmlEncode="false" SortExpression="Name" HeaderText="Name"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MakeUpStartDate" SortExpression="MakeUpStartDate"
                HeaderText="Started" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MakeUpTime" SortExpression="MakeUpTime" HeaderText="Duration"
                DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="SessionTime" SortExpression="SessionTime" HeaderText="Total Time"
                DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:BoundField DataField="IdleTime" SortExpression="IdleTime" HeaderText="Idle Time"
                DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageName" HeaderText="Last Url"
                SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="PageId" SortExpression="PageId" HeaderText="Page ID"
                HeaderStyle-HorizontalAlign="Left" />--%>
        </Columns>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
            PageButtonCount="25" />
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.Apps.Integration.MakeUpView.MemberSessionView">
    <SelectParameters>
        <asp:ControlParameter ControlID="hUserFormatString" Name="userFormatString" PropertyName="Value" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<span id="lblReq"></span>
<script type="text/javascript">
    $(document).ready(function () {
        $('.req-data').click(function () {
            $('#lblReq').html('User Request Info: [' + $(this).data('req') + ']');
        });
    });
</script>
