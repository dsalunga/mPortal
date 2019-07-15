<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberAccess.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.MemberAccessView" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Register Src="Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>

<script runat="server">
    public string FormatName(string name, int id, string format)
    {
        var query = new WQuery(this);
        query.Set(WebColumns.UserId, id);
        query.SetOpen("MemberEdit");
        return query.BuildQuery();
    }

</script>

<uc1:WebGroupTab ID="WebGroupTab1" runat="server" SelectedTab="Access" />
<div id="lblStatus" runat="server" class="alert"></div>
<asp:HiddenField ID="hGroupId" runat="server" Value="-1" />
<div id="accessManager">
    <div class="min-bottom-margin">
        <asp:TextBox ID="txtId" runat="server" CssClass="col-md-3" Columns="60" ClientIDMode="Static" ToolTip="Enter Email, Username, or Group ID separated by semicolon" placeholder="Email, Username, or Group ID"></asp:TextBox>
        <asp:Button ID="cmdAdd" OnClientClick="return AddUser('txtId','cmdAdd');" runat="server" ClientIDMode="Static"
            Text="Add..." OnClick="cmdAdd_Click" CssClass="btn btn-default" />
        &nbsp;
                <asp:Button ID="cmdRevoke" OnClientClick="return confirm('Are you sure you want to remove access to the selected accounts?');" runat="server" ClientIDMode="Static"
                    Text="Revoke" CssClass="btn btn-danger delete-users" Style="display: none" OnClick="cmdRevoke_Click" />
    </div>
    <asp:GridView CssClass="table table-borderless" ID="GridViewUsers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSourceUsers" ForeColor="#333333"
        GridLines="None" Width="100%" PageSize="50"
        AllowPaging="True">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" class="chk-main-users" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("Id") %>' class="chk-item-users" name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="55px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Member-Profile/?UserId={0}"
                DataTextField="UserName" HeaderText="Username" SortExpression="UserName" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                SortExpression="Email" />
            <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
                SortExpression="LastName" />
            <asp:BoundField DataField="MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile"
                SortExpression="MobileNumber" Visible="false" />
            <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Granted"
                SortExpression="DateJoined" />
            <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSourceUsers" runat="server" SelectMethod="SelectUsers"
        TypeName="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.MemberAccessView">
        <SelectParameters>
            <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="status" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <script type="text/javascript">
        function AddUser(textBoxId, buttonAdd) {
            var addValue = $("#" + textBoxId).val().Trim();
            if (addValue == "") {
                ShowAccountBrowser(textBoxId, <% =WebObjects.WebUser %>, 0, 0, 1, buttonAdd);
                return false;
            }

            return true;
        }

        $(document).ready(function () {
            var isBatchChange = false;

            // Show/Hide Edit/Delete buttons
            var showHideButtons = function () {
                if (!isBatchChange) { // batch check/uncheck should not be running
                    var showEdit = false, showDelete = false;
                    var checkedItems = $('.chk-item-users:checked');
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
                    $('.delete-users').css('display', showDelete ? 'inline-block' : 'none');
                }
            }

            // Check/Uncheck all
            $('.chk-main-users').click(function () {
                isBatchChange = true;
                $('.chk-item-users').attr('checked', $(this).is(':checked'));
                isBatchChange = false;
                showHideButtons();
            });

            // Trigger for any checked/unchecked item
            $('.chk-item-users').change(function () {
                showHideButtons();
            });
        });
    </script>
</div>
