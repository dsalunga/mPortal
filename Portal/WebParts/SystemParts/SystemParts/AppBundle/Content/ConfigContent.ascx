<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Content._Sections_C_CMS_Custom"
    CodeBehind="ConfigContent.ascx.cs" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Src="../../Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<div style="padding: 0 0 4px 0" runat="server" id="linkViewAll">
    <table border="0" cellpadding="0">
        <tr>
            <td rowspan="2">
                <a href="#" id="linkManage" runat="server">
                    <img src="/Content/Assets/Images/misc.png" class="TaskListIcon" border="0" />
                </a>
            </td>
            <td class="Header">Manage All Contents
            </td>
        </tr>
        <tr>
            <td valign="top">Manage all contents across the portal
            </td>
        </tr>
    </table>
</div>
<uc1:TabControl ID="editTab" OnSelectedTabChanged="editTab_SelectedTabChanged" SelectedIndex="0"
    runat="server" />
<asp:MultiView ID="mtvContent" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewEdit" runat="server">
        <table border="0" width="100%" cellpadding="0" cellspacing="3" style="margin: 3px 0 3px 0">
            <tr>
                <td style="width: 100px;">Title:<asp:RequiredFieldValidator ForeColor="Red" ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="col-md-6 input" />
                </td>
            </tr>
            <tr>
                <td valign="top">Content:
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
        <br />
        <div class="control-box">
            <div>
                <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Text="Publish"
                    OnClick="btnSave_Click" />
                <asp:Button ID="cmdSaveDraft" CssClass="btn btn-default" runat="server" Text="Save As Draft"
                    OnClick="cmdSaveDraft_Click" />
                <%--<asp:Button ID="cmdCancel" CssClass="Command" runat="server" Text="Cancel" Width="85px"
                                OnClick="btnCancel_Click" CausesValidation="False" />--%>
                <%--<asp:Button ID="cmdPreview" CssClass="Command" runat="server" 
                                Text="Save &amp; Preview" onclick="cmdPreview_Click" />--%>
                <div class="pull-right">
                    <asp:Button ID="cmdNew" CssClass="btn btn-default" runat="server" Text="New" CausesValidation="false" OnClick="cmdNew_Click" />
                    <asp:Button ID="cmdBrowse" CssClass="btn btn-default" runat="server" Text="Open..." OnClick="cmdBrowse_Click"
                        CausesValidation="false" />
                    <asp:Button ID="cmdEditCurrent" CssClass="btn btn-default" runat="server" Text="Reload" OnClick="cmdEditCurrent_Click" />
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="viewHistory" runat="server">
        <asp:GridView ID="grdHistory" EmptyDataText="There are no history for content." runat="server"
            CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
            Width="100%" OnRowCommand="grdHistory_RowCommand">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                        <%--<asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                                AlternateText="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="VersionNo" HeaderText="Version" SortExpression="VersionNo"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DateModified" HeaderText="Date/Time" SortExpression="DateModified"
                    HeaderStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
    </asp:View>
    <asp:View ID="viewDraft" runat="server">
        <asp:GridView ID="grdDraft" EmptyDataText="There are no drafts for this content."
            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
            OnRowCommand="grdDraft_RowCommand" Width="100%">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                        <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                            AlternateText="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DateModified" HeaderText="Date/Time" SortExpression="DateModified"
                    HeaderStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
    </asp:View>
    <asp:View ID="viwAdvance" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>Content ID:
                </td>
                <td style="height: 21px">
                    <asp:Literal ID="litID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Site ID:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSiteID" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>Rank:
                </td>
                <td>
                    <asp:DropDownList ID="ddlRank" runat="server">
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text=" Active" Checked="True" />
                </td>
            </tr>
            <tr>
                <td valign="top">Description:
                </td>
                <td>
                    <FCKeditorV2:FCKeditor ID="txtDescription" runat="server" Height="150px" ToolbarSet="Basic"
                        Width="100%">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewInfo" runat="server">
        No content details available.
    </asp:View>
</asp:MultiView>
<asp:HiddenField ID="hidId" runat="server" Value="-1" />
