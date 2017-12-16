<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Content.AdminContent"
    CodeBehind="WM_Custom.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Src="~/Content/Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>

<asp:HiddenField runat="server" ID="hIsPlainTextDefault" Value="0" />
<asp:Panel runat="server" ID="pnlContent" Width="100%">
    <div class="control-box no-bottom-margin">
        <div>
            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btnAdd_Click" />
            <asp:Button ID="btnDelete" OnClientClick="return confirm('Delete selected item(s)?');"
                CssClass="btn btn-default" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            &nbsp;&nbsp;
                <asp:Button ID="cmdDuplicate" runat="server" Text="Duplicate" CssClass="btn btn-default"
                    OnClick="cmdDuplicate_Click" Enabled="False" />
            <div class="pull-right">
                &nbsp;Show:&nbsp;<asp:DropDownList ID="cboSites" CssClass="input" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                    <asp:ListItem Value="-2"># All Websites #</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <asp:GridView ID="grvContent" CssClass="table table-borderless" runat="server" CellPadding="4" ForeColor="#333333"
        GridLines="None" AutoGenerateColumns="False" AllowPaging="True" DataSourceID="ObjectDataSource1"
        OnRowCommand="grvContent_RowCommand" AllowSorting="True" Width="100%" PageSize="20">
        <PagerSettings PageButtonCount="25" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <HeaderTemplate>
                    <input type="checkbox" value="chkMain" onclick="CheckAll(this, 'grvContent');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" value='<%# Eval("Id") %>' name="grvContent" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="8px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:ImageButton CommandArgument='<%# Eval("Id") %>' CommandName="ContentEdit"
                        runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" />
                    <asp:ImageButton runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                <ItemStyle HorizontalAlign="center" Width="35px" />
                <ItemTemplate>
                    <asp:ImageButton CommandArgument='<%# Eval("Id") %>' CommandName="Toggle_Active"
                        runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Content.AdminContent">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboSites" DefaultValue="-2" Name="siteId" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
<asp:Panel ID="pnlContentEdit" Visible="false" runat="server" Width="100%">
    <uc1:TabControl ID="TabControl1" SelectedIndex="0" runat="server" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
    <br />
    <asp:MultiView ID="mtvContent" runat="server" ActiveViewIndex="0">
        <asp:View ID="viwBasic" runat="server">
            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 73px">Title:
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                            Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="input" Columns="75" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 73px">Content:
                    </td>
                    <td>
                        <uc2:TextEditor IsPlainTextDefault="false" ID="txtContent" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="chkActiveContent" ToolTip="Tick this if you are using content variables" CssClass="aspnet-checkbox" runat="server" Text="Active Content" Checked="false" /></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="viwAdvance" runat="server">
            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 73px;">Content ID:
                    </td>
                    <td>
                        <asp:Literal ID="litID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 73px">Web Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboSiteID" runat="server" CssClass="input">
                            <asp:ListItem Value="-1"># None #</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True" />
                        &nbsp;
                        <asp:CheckBox ID="chkEditorSensitive" CssClass="aspnet-checkbox" runat="server" Text="Editor Sensitive" Checked="false" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 73px">Description:
                    </td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="fckDescription" runat="server" Height="150px" ToolbarSet="Basic"
                            Width="98%">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <div class="control-box">
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                CausesValidation="False" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Panel>
