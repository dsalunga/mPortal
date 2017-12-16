<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigDashboard.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Article.ConfigDashboard" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc2" %>

<asp:HiddenField ID="hSiteId" runat="server" Value="-1" />
<uc2:TabControl ID="TabControl1" runat="server" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewConfig" runat="server">
        <table style="margin-top: 5px">
            <tr>
                <td style="width: 130px">Subscription Mode:
                </td>
                <td>
                    <asp:DropDownList ID="cboSubscriptionMode" runat="server" CssClass="input">
                        <asp:ListItem Selected="True" Value="0">All User's Group Subscriptions</asp:ListItem>
                        <asp:ListItem Value="1">Specific Group Subscriptions</asp:ListItem>
                        <asp:ListItem Value="2">Subscription by this component</asp:ListItem>
                        <asp:ListItem Value="3">All User's Groups + This component</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Group-specific:
                </td>
                <td class="min-bottom-margin">
                    <asp:TextBox ID="txtGroupId" runat="server" Columns="50" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                    <input id="cmdClientBrowse" class="btn btn-default btn-sm" onclick="ShowAccountBrowser('txtGroupId', <% =WCMS.Framework.WebObjects.WebGroup %>, 0);"
                        type="button" value="Browse..." />
                </td>
            </tr>
            <tr>
                <td style="width: 120px;">Ignore Subscriptions from Groups (separated by comma):
                </td>
                <td valign="top" class="min-bottom-margin">
                    <asp:TextBox ID="txtIgnoreGroups" ClientIDMode="Static" runat="server" CssClass="input" Columns="45" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <input id="cmdIgnoreBrowse" class="btn btn-default btn-sm" onclick="ShowAccountBrowser('txtIgnoreGroups', <% =WCMS.Framework.WebObjects.WebGroup %>, 0, 1);"
                        type="button" value="Browse..." />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkUsePageParameter" CssClass="aspnet-checkbox" runat="server" Text="Use Page Parameters" />
                </td>
            </tr>
        </table>
        <%--<table>
            <tr>
                <td></td>
            </tr>
            %--<tr>
                <td align="right">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" CssClass="Command" Width="80px"
                        OnClick="cmdUpdate_Click" />
                </td>
            </tr>--%
        </table>--%>
    </asp:View>
    <asp:View ID="viewSubscriptions" runat="server">
        <div class="control-box">
            <div>
                <asp:RequiredFieldValidator ID="rfvPage" runat="server" ControlToValidate="txtNavigateURL"
                    ErrorMessage="Page to subscribe is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtNavigateURL" CssClass="input" ClientIDMode="Static" runat="server" Columns="50" />
                <asp:Button ID="cmdBrowse" ClientIDMode="Static" runat="server" Text="Browse..." CssClass="btn btn-default btn-sm" CausesValidation="False" /><asp:Button ID="cmdAddFull"
                    runat="server" Text="Add" CssClass="btn btn-default btn-sm" OnClick="cmdAddFull_Click" />
                &nbsp;
                    <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-sm" OnClick="cmdDelete_Click"
                        OnClientClick="return confirm('Are you sure you want to delete?');" CausesValidation="False" />
            </div>
        </div>
        <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
            Width="100%" GridLines="None" OnRowCommand="GridView1_RowCommand" PageSize="15" CssClass="table table-borderless">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" value='<%# Eval("Id") %>' name="chkChecked" />
                    </ItemTemplate>
                    <HeaderStyle Width="15px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                            AlternateText="Delete Page" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" OnClientClick="return confirm('Are you sure you want to delete this item?')" />
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Manage" CommandArgument='<%# Eval("Id") %>'
                            AlternateText="Manage Articles" CausesValidation="false" ToolTip="Manage Articles"
                            ImageUrl="~/Content/Assets/Images/Common/Objects.gif" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PageName" HeaderText="Page" SortExpression="PageName"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageUrl" HeaderText="Url"
                    SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:HyperLinkField>
            </Columns>
            <PagerSettings PageButtonCount="50" />
        </asp:GridView>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
            TypeName="WCMS.WebSystem.WebParts.Article.ConfigDashboard"></asp:ObjectDataSource>
        <script type="text/javascript">
            $("#cmdBrowse").click(function () {
                BrowseLink("txtNavigateURL", <%= WCMS.Framework.WPart.Get("Article").Id %>, "<%: hSiteId.Value %>"); return false;
            });
        </script>
    </asp:View>
</asp:MultiView>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
