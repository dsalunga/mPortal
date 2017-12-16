<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigWall.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Social.ConfigWall" %>
<%@ Register Src="~/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc2" %>
<uc2:TabControl ID="TabControl1" runat="server" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewConfig" runat="server">
        <table>
            <tr>
                <td style="width: 120px">Subscription Mode:
                </td>
                <td>
                    <asp:DropDownList ID="cboSubscriptionMode" runat="server">
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
                <td>
                    <asp:TextBox ID="txtGroupId" runat="server" Columns="50"></asp:TextBox>
                    <input id="cmdClientBrowse" onclick="ShowAccountBrowser('<% =txtGroupId.ClientID %>', <% =WCMS.Framework.WebObjects.WebGroup %>, 0);"
                        type="button" value="Browse..." />
                </td>
            </tr>
            <tr>
                <td style="width: 120px;">Ignore Subscriptions from Groups (separated by comma):
                </td>
                <td valign="top">
                    <asp:TextBox ID="txtIgnoreGroups" runat="server" Columns="45" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <input id="cmdIgnoreBrowse" onclick="ShowAccountBrowser('<% =txtIgnoreGroups.ClientID %>', <% =WCMS.Framework.WebObjects.WebGroup %>, 0, 1);"
                        type="button" value="Browse..." />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkUsePageParameter" runat="server" Text="Use Page Parameters" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewSubscriptions" runat="server">
        <div class="control-box">
            <div>
                <asp:RequiredFieldValidator ID="rfvPage" runat="server" ControlToValidate="txtNavigateURL"
                    ErrorMessage="Page to subscribe is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtNavigateURL" runat="server" Columns="50" />
                <asp:Button ID="cmdBrowse" OnClientClick="BrowseLink('<% =txtNavigateURL.ClientID %>'); return false;"
                    runat="server" Text="Browse..." CausesValidation="False" /><asp:Button ID="cmdAddFull"
                        runat="server" Text="Add" Width="85px" OnClick="cmdAddFull_Click" />
                &nbsp;
                    <asp:Button ID="cmdDelete" runat="server" Text="Delete" Width="85px" OnClick="cmdDelete_Click"
                        OnClientClick="return confirm('Are you sure you want to delete?');" CausesValidation="False" />
            </div>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
                Width="100%" GridLines="None" OnRowCommand="GridView1_RowCommand" PageSize="15">
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
                                AlternateText="Delete Page" ImageUrl="~/Images/Common/ico_exit.gif" OnClientClick="return confirm('Are you sure you want to delete this item?')" />
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Manage" CommandArgument='<%# Eval("Id") %>'
                                AlternateText="Manage Articles" CausesValidation="false" ToolTip="Manage Articles"
                                ImageUrl="~/Images/Common/Objects.gif" />
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
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Social.ConfigWall"></asp:ObjectDataSource>
        </div>
    </asp:View>
</asp:MultiView>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>