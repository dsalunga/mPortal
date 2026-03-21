<%@ Page Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.FileManager.FileManagerController"
    CodeBehind="FileManager.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Manager</title>
    <link rel="Stylesheet" type="text/css" href="Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            File Manager</h1>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <font size="2">Location:&nbsp;<strong>
                        <asp:Literal ID="lRoot" runat="server"></asp:Literal></strong></font>
                </td>
            </tr>
            <tr>
                <td height="15">
                    <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="ControlBox">
                    <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete the selected folders?');" runat="server" Text="Delete" OnClick="cmdDelete_Click"
                        Visible="false" />
                    <asp:TextBox ID="txtDir" runat="server"></asp:TextBox>&nbsp;
                    <asp:Button ID="cmdCreate" runat="server" Text="Create" OnClick="cmdCreate_Click" />&nbsp;
                    <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" OnClick="cmdRefresh_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridViewFolders" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Name" DataSourceID="ObjectDataSource2"
                        ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="No folders found."
                        OnRowCommand="GridViewFolders_RowCommand" PageSize="10">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:ImageButton Visible="false" ID="ImageButton1" runat="server" CommandName="Custom_Edit"
                                        ImageUrl="~/Images/Common/ico_edit.gif" AlternateText="Details" CommandArgument='<%# Eval("Name") %>' />
                                    <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Download" ImageUrl="~/Images/Common/ico_diskette.gif"
                                        AlternateText="Download" CommandArgument='<%# Eval("Name") %>' />
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Images/Common/ico_exit.gif"
                                        AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                        CommandArgument='<%# Eval("Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Folder Name" SortExpression="FolderName">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="cmdViewSubFolders" CommandName="View_SubFolders"
                                        Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Name") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DateModified" HeaderText="Date Modified" SortExpression="DateModified" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerSettings PageButtonCount="25" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetFolders"
                        TypeName="WCMS.WebSystem.FileManager.FileManagerController">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="~/" Name="path" QueryStringField="__path"
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td height="15">
                </td>
            </tr>
            <tr>
                <td valign="middle" class="ControlBox">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td nowrap="nowrap" width="100%">
                                <asp:Button ID="cmdDeleteFile" OnClientClick="return confirm('Are you sure you want to delete the selected files?');" runat="server" Text="Delete" OnClick="cmdDeleteFile_Click"
                                    Visible="false" /><input id="fileUploader" type="file" name="fileUploader" runat="server" /><asp:Button
                                        ID="cmdUpload" runat="server" Text="Upload" OnClick="cmdUpload_Click" />
                            </td>
                            <td nowrap="nowrap">
                                Link:&nbsp;
                                <asp:TextBox ID="txtLink" runat="server"></asp:TextBox>&nbsp;&nbsp;Filename:&nbsp;
                                <asp:TextBox ID="txtFilename" runat="server"></asp:TextBox>&nbsp;
                                <asp:Button ID="cmdCreateLink" runat="server" Text="Create Link" OnClick="cmdCreateLink_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridViewFiles" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource1"
                        ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridViewFiles_RowCommand"
                        EmptyDataText="No files found." PageSize="10">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                        <asp:TemplateField HeaderText="Actions">
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Images/Common/ico_edit.gif"
                                            AlternateText="Edit" CommandArgument='<%# Eval("FullPath") %>' />
                                        <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Download" ImageUrl="~/Images/Common/ico_diskette.gif"
                                            AlternateText="Download" CommandArgument='<%# Eval("Name") %>' />
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Images/Common/ico_exit.gif"
                                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                            CommandArgument='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="FullPath" DataTextField="Name" HeaderText="File Name"
                                SortExpression="Name" Target="_blank" />
                            <asp:BoundField DataField="SizeString" HeaderText="Size" SortExpression="Size">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Extension" HeaderText="Extension" SortExpression="Extension">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DateModified" HeaderText="Date Modified" SortExpression="DateModified" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerSettings PageButtonCount="25" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFiles"
                        TypeName="WCMS.WebSystem.FileManager.FileManagerController">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="~/" Name="path" QueryStringField="__path"
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
