<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSessionManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.UserSessionManagerView" %>
<asp:HiddenField ID="hUserFormatString" runat="server" Value="" />
<style type="text/css">
    .req-data {
        color: #ccc;
    }
</style>
<div class="min-bottom-margin">
    <a class="btn btn-default" href="<%=Request.RawUrl %>">Refresh</a>
    <asp:Button ID="cmdEndSession" OnClientClick="return confirm('Are you sure you want to terminate the selected sessions?');" runat="server" ClientIDMode="Static"
        Text="Terminate" Style="display: none" CssClass="btn btn-danger delete-sessions" OnClick="cmdEndSession_Click" />
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="UserId" CssClass="table table-borderless table-condensed" OnRowCommand="GridView1_RowCommand"
        PageSize="35">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" class="chk-main-sessions" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("UserId") %>' class="chk-item-sessions" name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HtmlEncode="false" SortExpression="Name" HeaderText="Name"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ActivityStartDate" SortExpression="ActivityStartDate"
                HeaderText="Date Started" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastActivityDate" SortExpression="LastActivityDate"
                HeaderText="Last Activity" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="SessionTime" SortExpression="SessionTime" HeaderText="Session Time"
                DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="IdleTime" SortExpression="IdleTime" HeaderText="Idle Time"
                DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="IPAddress" ItemStyle-CssClass="prepare-geoip" SortExpression="IPAddress"
                HeaderText="IP Location" HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageName" HeaderText="Last Url"
                SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PageId" SortExpression="PageId" HeaderText="Page ID"
                HeaderStyle-HorizontalAlign="Left" />
        </Columns>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
            PageButtonCount="25" />
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Tools.UserSessionManagerView">
    <SelectParameters>
        <asp:ControlParameter ControlID="hUserFormatString" Name="userFormatString" PropertyName="Value" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<span id="lblReq"></span>
<script type="text/javascript">
    $(document).ready(function () {
        var isBatchChange = false;

        // Show/Hide Edit/Delete buttons
        var showHideButtons = function () {
            if (!isBatchChange) { // batch check/uncheck should not be running
                var showEdit = false, showDelete = false;
                var checkedItems = $('.chk-item-sessions:checked');
                var checkedCount = checkedItems.length;
                if (checkedCount == 1) {
                    showEdit = true;
                    showDelete = true;
                }
                else if (checkedCount > 1) {
                    showEdit = false;
                    showDelete = true;
                }

                //$('.edit').css('display', showEdit ? 'inline-block' : 'none');
                $('.delete-sessions').css('display', showDelete ? 'inline-block' : 'none');
            }
        }

        // Check/Uncheck all
        $('.chk-main-sessions').click(function () {
            isBatchChange = true;
            $('.chk-item-sessions').attr('checked', $(this).is(':checked'));
            isBatchChange = false;
            showHideButtons();
        });

        // Trigger for any checked/unchecked item
        $('.chk-item-sessions').change(function () {
            showHideButtons();
        });

        $('.req-data').click(function () {
            $('#lblReq').html('User Request Info: [' + $(this).data('req') + ']');
        });


        $('.prepare-geoip').each(function () {
            var text = $(this).text();
            if (text.indexOf('(') == -1) {
                $(this).html(text + " (<a class='to-geoip' data-ip='" + text + "' href='#'>Resolve Location</a>)");
            }
        });

        $('.to-geoip').each(function () {
            $(this).click(function (event) {
                event.preventDefault();

                var ip = $(this).data('ip');
                if (ip) {
                    var that = this;
                    $(that).removeClass('to-geoip');
                    ResolveGeoIp(function (loc) { $(that).replaceWith(loc); }, ip);
                }
            });
        });
    });
</script>
