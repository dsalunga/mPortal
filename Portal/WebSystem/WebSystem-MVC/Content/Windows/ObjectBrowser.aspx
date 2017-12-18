<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjectBrowser.aspx.cs"
    Inherits="WCMS.WebSystem.Windows.ObjectBrowser" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Object Browser</title>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <link href="<%=WebUtil.Version("~/content/assets/styles/websystem.min.css")%>" rel="stylesheet" type="text/css" media="all" />
    <link href="<%=WebUtil.Version("~/content/assets/styles/websystem.admin.min.css")%>" rel="stylesheet" type="text/css" media="all" />
</head>
<body>
    <div style="margin: 5px 0 8px 5px" class="Header">
        Object Browser
    </div>
    <form id="form1" runat="server">
        <input type="hidden" id="hidId" runat="server" value="" />
        <div style="margin: 2px 2px 6px 2px">
            Type:&nbsp;<asp:DropDownList ID="cboType" runat="server" AutoPostBack="True">
                <asp:ListItem Selected="True" Text="# All types #" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top" style="width: 180px">
                    <asp:TreeView ID="tvNavigation" runat="server" OnSelectedNodeChanged="tvNavigation_SelectedNodeChanged">
                    </asp:TreeView>
                </td>
                <td valign="top">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding: 5px 0 5px 0">
                                <span runat="server" id="lblBreadcrumb"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="ControlBox">
                                <div style="float: left">
                                    <asp:Button Enabled="false" ID="cmdAddFull" CssClass="Command" runat="server" Text="New Folder" />&nbsp;
                                <asp:Button ID="cmdUp" runat="server" Width="50px" CssClass="Command" Text="Up" OnClick="cmdUp_Click" />
                                </div>
                                <div style="float: right">
                                    <asp:TextBox ID="txtSearch" Columns="25" runat="server"></asp:TextBox>
                                    <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                                        ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" Width="55px" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                                    Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                                    PageSize="20" EmptyDataText="No folders found.">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Actions">
                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                                    AlternateText="Children" ToolTip="Children" CommandArgument='<%# Eval("Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Folder" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <PagerSettings PageButtonCount="50" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                                    TypeName="WCMS.WebSystem.Windows.ObjectBrowser">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="tvNavigation" DefaultValue="-1" Name="folderId"
                                            PropertyName="SelectedValue" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="Header">
                                    Objects
                                </div>
                                <asp:GridView ID="grdObjects" runat="server" AllowPaging="True" AllowSorting="True"
                                    CellPadding="4" DataSourceID="ObjectDataSource2" ForeColor="#333333" GridLines="None"
                                    Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="grdObjects_RowCommand"
                                    PageSize="18" EmptyDataText="No records found.">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Actions">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                                    ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                                                    ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="ObjectName" HeaderText="Type" SortExpression="ObjectName"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="RecordId" HeaderText="ID" SortExpression="RecordId" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified"
                                            HeaderStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <PagerSettings PageButtonCount="50" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SelectFiles"
                                    TypeName="WCMS.WebSystem.Windows.ObjectBrowser" OldValuesParameterFormatString="original_{0}">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="tvNavigation" DefaultValue="-1" Name="folderId"
                                            PropertyName="SelectedValue" Type="Int32" />
                                        <asp:ControlParameter ControlID="cboType" DefaultValue="-1" Name="objectId" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="ControlBox">
                    <asp:Button ID="cmdOpen" Width="75px" CssClass="Command" runat="server" Text="OK"
                        Enabled="False" OnClick="cmdOpen_Click" />
                    <asp:Button ID="cmdCancel" CssClass="Command" runat="server" Text="Cancel" OnClientClick="window.close();
    return false;" />
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        function Page_Load() {
            var hidId = WCMS.Dom.Get("hidId");
            if (hidId != null && hidId.value != "") {
                if (window.opener.returnValue.param != null) {
                    window.opener.returnValue.param.value = hidId.value;
                    window.close();
                }
            }
        }
        WCMS.Dom.AddEvent(window, "load", Page_Load);
        $(document).ready(function () {
            WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
        });
    </script>
</body>
</html>
