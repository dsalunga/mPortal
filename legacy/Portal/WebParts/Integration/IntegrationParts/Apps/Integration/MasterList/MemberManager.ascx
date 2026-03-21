<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberManager.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.MemberManager" %>
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

<div class="masterlist-member-manager">
    <uc1:WebGroupTab ID="WebGroupTab1" runat="server" SelectedTab="Members" />
    <asp:HiddenField runat="server" ID="hUserProfileUrl" Value="" />
    <asp:HiddenField runat="server" ID="hUserProfileEditUrl" Value="" />
    <asp:HiddenField runat="server" ID="hGroupId" Value="" />
    <asp:HiddenField runat="server" ID="hIsManager" Value="0" />
    <div class="min-bottom-margin">
        <% if (IsManager)
           { %>
        <span class="input-append">
            <asp:TextBox ID="txtId" runat="server" CssClass="col-md-3" ClientIDMode="Static" placeholder="Group ID or Email"></asp:TextBox>
            <div class="btn-group">
                <asp:Button runat="server" ID="cmdAdd" OnClick="cmdAdd_Click" ClientIDMode="Static" CssClass="btn btn-default" OnClientClick="return Add_Click();" Text="Add..." />
                <button class="btn dropdown-toggle" data-toggle="dropdown">
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li><a href="#" runat="server" id="linkNewMember">Create New Member</a></li>
                </ul>
            </div>
        </span>
        &nbsp;
    <%--<a href="#" class="btn btn-warning edit" style="display: none">Edit</a>--%>
        <asp:Button ID="cmdRemove" OnClientClick="return confirm('Are you sure you want to remove/delete selected items?');" runat="server"
            class="btn btn-danger delete" OnClick="cmdRemove_Click" name="cmdRemove" Style="display: none" Text="Remove" />
        <% } %>
        <div class="pull-right">
            <span class="input-append">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="col-md-2" ClientIDMode="Static" placeholder="Enter keyword"></asp:TextBox>
                <div class="btn-group">
                    <button class="btn btn-default" type="button">Search</button>
                    <button class="btn dropdown-toggle" data-toggle="dropdown">
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton ID="cmdDownload" runat="server" Text="Download" OnClick="cmdDownload_Click" /></li>
                    </ul>
                </div>
            </span>
        </div>
    </div>
    <div id="lblStatus" runat="server" class="alert alert-success"></div>
    <asp:GridView CssClass="table image-noscale" ID="GridView1" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%"
        PageSize="25" EnableViewState="false">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" class="chk-main" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" class="chk-item" value='<%# Eval("Id") %>' name="chkChecked" />
                </ItemTemplate>
                <HeaderStyle CssClass="bootstrap-checkbox" Width="10px" />
                <ItemStyle CssClass="bootstrap-checkbox" HorizontalAlign="Center" VerticalAlign="Top" Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="64px" ItemStyle-Width="64px">
                <ItemTemplate>
                    <a href='<%# FormatName(Eval("Name").ToString(), (int)Eval("Id"), hUserProfileUrl.Value) %>' title='View details (Username: <%# Eval("Name") %>)'>
                        <img src='<%# Eval("PhotoPath") %>' alt="" style="width: 64px" width="64" /></a>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Username" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <img src='/Content/Assets/Images/Common/txt.gif' />
                <a href='<%# FormatName(Eval("Name").ToString(), (int)Eval("Id")) %>' title='<%# Eval("Name") %>'><%# Eval("Name") %></a>
            </ItemTemplate>
        </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Group ID" SortExpression="ExternalId" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%--<img src='/Content/Assets/Images/Common/txt.gif' />--%>
                    <a href='<%# FormatName(Eval("Name").ToString(), (int)Eval("Id"), hUserProfileEditUrl.Value) %>' title="Edit details"><%# DataHelper.FormatString((string)Eval("ExternalId"), "&lt;No Group ID&gt;") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Name" HeaderText="UserName" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" Visible="false" />--%>
            <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
                SortExpression="LastName" />
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Security/WebUserHome/?UserId={0}"
                DataTextField="Email" HeaderText="Email" SortExpression="Email" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Contact No."
                SortExpression="MobileNumber" Visible="true" />
            <asp:BoundField DataField="VoiceDesignation" HeaderStyle-HorizontalAlign="Left" HeaderText="Voice Designation"
                SortExpression="VoiceDesignation" />
            <asp:BoundField DataField="Position" HeaderStyle-HorizontalAlign="Left" HeaderText="Position"
                SortExpression="Position" Visible="false" />
            <%--<asp:BoundField DataField="DateModified" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Modified"
            SortExpression="DateModified" Visible="false" />--%>
            <%--<asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-HorizontalAlign="Left" Visible="false" />--%>
            <%--<asp:TemplateField HeaderText="Active" SortExpression="Active">
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <img src='<%# WebHelper.SetStateImageIntActiveNull(Eval("Active")) %>' />
            </ItemTemplate>
        </asp:TemplateField>--%>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="18" FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.MemberManager">
        <SelectParameters>
            <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        var isBatchChange = false;

        // Show/Hide Edit/Delete buttons
        var showHideButtons = function () {
            if (!isBatchChange) { // batch check/uncheck should not be running
                var showEdit = false, showDelete = false;
                var checkedItems = $('.chk-item:checked');
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
                $('.delete').css('display', showDelete ? 'inline-block' : 'none');
            }
        }

        // Check/Uncheck all
        $('.chk-main').click(function () {
            isBatchChange = true;
            $('.chk-item').attr('checked', $(this).is(':checked'));
            isBatchChange = false;
            showHideButtons();
        });

        // Trigger for any checked/unchecked item
        $('.chk-item').change(function () {
            showHideButtons();
        });
    });

    function Add_Click() {
        var addValue = $("#txtId").val().Trim();
        if (addValue == "") {
            ShowAccountBrowser("txtId", 21, 0, 0, 1, "cmdAdd");
            return false;
        }

        return true;
    }

</script>
