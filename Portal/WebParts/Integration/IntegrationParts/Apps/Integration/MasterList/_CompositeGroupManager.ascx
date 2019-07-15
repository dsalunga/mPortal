<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_CompositeGroupManager.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.CompositeGroupManager" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Register Src="Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>


<script runat="server">
    public string FormatName(string name, int objectId, int id)
    {
        WebQuery query = new WebQuery(this);
        if (objectId == WebObjects.WebGroup)
            return query.Set(WebColumns.ParentId, id).BuildQuery();

        query.Set(WebColumns.UserId, id);
        query.SetOpen("MemberEdit");
        return query.BuildQuery();
    }
</script>

<uc1:WebGroupTab ID="WebGroupTab1" runat="server" />
<asp:HiddenField runat="server" ID="hUserProfileUrl" Value="" />
<asp:HiddenField runat="server" ID="hGroupId" Value="" />
<div class="min-bottom-margin">
    <span class="input-append">
        <asp:TextBox ID="txtId" runat="server" CssClass="span2" ClientIDMode="Static" placeholder="Group ID or Email"></asp:TextBox>
        <div class="btn-group">
            <button runat="server" id="cmdAdd" onserverclick="cmdAdd_Click" clientidmode="static" class="btn" onclick="return Add_Click();">Add...</button>
            <button class="btn dropdown-toggle" data-toggle="dropdown">
                <span class="caret"></span>
            </button>
            <ul class="dropdown-menu">
                <li><a href="#" runat="server" id="linkNewGroup">New Group</a></li>
                <li><a href="#" runat="server" id="linkNewMember">New Member</a></li>
            </ul>
        </div>
    </span>
    &nbsp;
    <%--<a href="#" class="btn btn-warning edit" style="display: none">Edit</a>--%>
    <asp:Button ID="cmdRemove" OnClientClick="return confirm('Are you sure you want to remove/delete selected items?');" runat="server"
        class="btn btn-danger delete" OnClick="cmdRemove_Click" name="cmdRemove" Style="display: none" Text="Delete" />
    <div style="float: right">
        <span class="input-append">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="span2" ClientIDMode="Static" placeholder="Search"></asp:TextBox>
            <div class="btn-group">
                <button class="btn" type="button">Search</button>
                <button class="btn dropdown-toggle" data-toggle="dropdown">
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li><a href="#" runat="server" id="linkEditGroup">Edit Group</a></li>
                    <li><a href="#">Delete Group</a></li>
                    <li>
                        <asp:LinkButton ID="cmdDownload" runat="server" Text="Download" OnClick="cmdDownload_Click" /></li>
                </ul>
            </div>
        </span>
        <%--<asp:Button ID="cmdUp" runat="server" CssClass="btn" Text="Up" OnClick="cmdUp_Click" />--%>
    </div>
</div>
<div id="lblStatus" runat="server" class="alert alert-success"></div>
<asp:GridView CssClass="table" ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
    PageSize="15" EnableViewState="false">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <%--<asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="Center" Width="60px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Properties" CommandArgument='<%# Eval("Id") %>' />
                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="View_Users" AlternateText="View Users"
                    ImageUrl="~/Content/Assets/Images/TreeView/u.gif" CommandArgument='<%# Eval("Id") %>' />
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                    AlternateText="Children" ToolTip="Children" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" class="chk-main" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
            </HeaderTemplate>
            <ItemTemplate>
                <input type="checkbox" class="chk-item" value='<%# Eval("UniqueId") %>' name="chkChecked" />
            </ItemTemplate>
            <HeaderStyle CssClass="bootstrap-checkbox" Width="10px" />
            <ItemStyle CssClass="bootstrap-checkbox" HorizontalAlign="Center" VerticalAlign="Top" Width="10px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <img src='<%# (int)Eval("ObjectId") == WebObjects.WebGroup ? "/Content/Assets/Images/Common/folder2.gif" : "/Content/Assets/Images/Common/txt.gif" %>' />
                <a href='<%# FormatName(Eval("Name").ToString(), (int)Eval("ObjectId"), (int)Eval("Id")) %>' title='<%# Eval("Name") %>'><%# Eval("Name") %></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" Visible="false" />
        <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
            SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
            SortExpression="LastName" />
        <asp:HyperLinkField DataNavigateUrlFields="OwnerId" DataNavigateUrlFormatString="/Central/Security/WebUserHome/?UserId={0}"
            DataTextField="EmailOwner" HeaderText="Email / Owner" SortExpression="EmailOwner" Target="_blank"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="DateModified" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Modified"
            SortExpression="DateModified" Visible="false" />
        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-HorizontalAlign="Left" Visible="false" />
        <asp:TemplateField HeaderText="Active" SortExpression="Active">
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <img src='<%# WebHelper.SetStateImageIntActiveNull(Eval("Active")) %>' />
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
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.GroupManager">
    <SelectParameters>
        <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
            DefaultValue="-1" />
    </SelectParameters>
</asp:ObjectDataSource>
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
