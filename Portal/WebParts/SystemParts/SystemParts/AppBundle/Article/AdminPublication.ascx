<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Article.AdminPublicationController"
    CodeBehind="AdminPublication.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Src="../Central/Controls/WebSiteElementSelector.ascx" TagName="WebSiteElementSelector"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/AdminTabControl.ascx" TagName="AdminTabControl" TagPrefix="uc4" %>
<uc4:AdminTabControl ID="AdminTabControl1" runat="server" />
<asp:Panel runat="server" ID="pnlContent" Width="100%">
    <div class="control-box no-bottom-margin">
        <div>
            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-default" Text="New" Width="85px" OnClick="btnAdd_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-default" OnClick="btnDelete_Click" OnClientClick="return confirm('Delete selected item(s)?');" />
            <%--&nbsp;&nbsp;
                <asp:Button ID="cmdDuplicate" runat="server" Text="Duplicate" Width="85px" Height="30px"
                    OnClick="cmdDuplicate_Click" />--%>
            <div class="pull-right">
                Show:&nbsp;<asp:DropDownList ID="cboSites" runat="server" CssClass="input"
                    AutoPostBack="True" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="-2"># All Sites #</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="grvContent" runat="server" CellPadding="4" ForeColor="#333333" CssClass="table table-borderless"
            GridLines="None" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
            DataSourceID="ObjectDataSource1" OnRowCommand="grvContent_RowCommand" AllowSorting="True"
            PageSize="15">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="CustomID" HeaderStyle-Width="10px">
                    <HeaderTemplate>
                        <input type="checkbox" value="chkMain" onclick="CheckAll(this, 'grvContent');" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" value='<%# Eval("Id") %>' name="grvContent" />
                    </ItemTemplate>
                    <HeaderStyle Width="10px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="38px">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%# Eval("Id") %>' CommandName="ContentEdit"
                            runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" />
                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                            CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("Date", "{0:yyyy-MM-dd}") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" SortExpression="CreatedByName"
                    HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Active" SortExpression="Active">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ToolTip="Toggle Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                            ID="Image1" CommandName="ToggleActive" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField Visible="false" DataField="Active" HeaderText="Active" SortExpression="Active"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <PagerSettings PageButtonCount="35" />
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Article.AdminPublicationController">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboSites" DefaultValue="-2" Name="siteId" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
<asp:Panel ID="pnlContentEdit" Visible="false" runat="server" Width="100%">
    <div style="margin-top: 5px">
        <uc1:TabControl ID="TabControl1" ThemeName="green" runat="server" />
    </div>
    <asp:MultiView ID="mtvContent" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewGeneral" runat="server">
            <table border="0" width="100%" cellpadding="0" cellspacing="3" style="margin: 3px 0 3px 0">
                <tr>
                    <td style="width: 73px">Title:
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                            Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="input" Columns="70" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 73px">Date:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="input">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlDay" runat="server" CssClass="input">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="input">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 73px">Description:
                    </td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="fckDescription" runat="server" Height="120px" ToolbarSet="Basic"
                            Width="98%">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 73px">Content:
                    </td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Height="400px" Width="98%">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td>Tags:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTags" runat="server" Columns="75" CssClass="input" />
                    </td>
                </tr>
            </table>
            <br />
        </asp:View>
        <asp:View ID="viewExtended" runat="server">
            <table border="0" width="100%" cellpadding="0" cellspacing="0" style="margin-top: 5px;">
                <tr>
                    <td style="width: 80px;">Content ID:
                    </td>
                    <td>
                        <asp:Literal ID="litID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Author:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAuthor" runat="server" Columns="75" CssClass="input" />
                    </td>
                </tr>
                <tr>
                    <td>Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSiteID" runat="server" CssClass="input">
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True" />
                    </td>
                </tr>
                <%--<tr>
                    <td valign="top">
                        Image:
                    </td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="fckImage" runat="server" Height="150px" ToolbarSet="Banner"
                            Width="98%">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>--%>
            </table>
        </asp:View>
        <asp:View ID="viewPublish" runat="server">
            <br />
            <uc2:WebSiteElementSelector ID="elementSelector" runat="server" />
        </asp:View>
    </asp:MultiView>
    <div class="control-box">
        <div>
            <asp:Button ID="cmdPublish" CssClass="btn btn-primary" runat="server" Text="Publish"
                OnClick="cmdPublish_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                CausesValidation="False" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Panel>
