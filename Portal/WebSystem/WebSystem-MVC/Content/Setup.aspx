<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="WCMS.WebSystem.Setup" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Portal: Online Setup</title>
    <link href="<%=WebUtil.Version("~/content/plugins/bootstrap/css/bootstrap.min.css")%>" rel="stylesheet" />
    <link rel="stylesheet" media="all" type="text/css" href="<%=WebUtil.Version("~/content/assets/styles/websystem.min.css")%>" />
    <link rel="stylesheet" media="all" type="text/css" href="<%=WebUtil.Version("~/content/assets/styles/websystem.controls.min.css")%>" />
    <link rel="stylesheet" media="all" type="text/css" href="<%=WebUtil.Version("~/content/assets/styles/websystem.admin.min.css")%>" />
    <style type="text/css">
        .aspnet-checkbox input[type="checkbox"] + label, .aspnet-radio input[type="radio"] + label {
            display: inline;
        }

        .aspnet-checkbox input[type="checkbox"], .aspnet-radio input[type="radio"] {
            margin: 5px 5px 5px 0;
        }

        .border {
            padding: 10px;
        }

        .actionPane {
            width: 50%;
        }
    </style>
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server">
            <div>
                <a href="/">Home</a>&nbsp;|&nbsp;<a href="/Central/" target="_top">Administration</a>&nbsp;|&nbsp;<a
                    href="./Setup.aspx" target="_top">Refresh</a>
                <br />
                <br />
                <uc1:TabControl ID="TabControl1" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
                    runat="server" />
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viewGeneral" runat="server">
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <h3 class="">
                                        Detected Errors
                                    </h3>
                                    <br />
                                    <div id="divErrors" runat="server">
                                    </div>
                                </td>
                                <td class="actionPane" valign="top">
                                    <fieldset>
                                        <legend>System Tools</legend>
                                        <div class="border">
                                            <asp:Button ID="cmdReset" runat="server" Text="System Reset" CssClass="btn btn-default" OnClick="cmdReset_Click"
                                                ToolTip="Resets all sessions and reloads the application domain" />
                                            <asp:Button ID="cmdSecureMe" runat="server" Text="Secure Me" CssClass="btn btn-default" ToolTip="Renames this page to prevent future access."
                                                OnClick="cmdSecureMe_Click" />
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="viewWebObject" runat="server">
                        <div class="control-box">
                            <div>
                                <asp:Button ID="cmdSelect" runat="server" Text="Select" CssClass="btn btn-default" ToolTip="Select/Deselect checked objects"
                                    OnClick="cmdSelect_Click" />
                                <asp:Button ID="cmdRestoreData" OnClientClick="return confirm('This will delete all existing records in the selected objects. Are you sure you want to restore the recent backup for the selected objects?');"
                                    runat="server" Text="Restore &#9658;" CssClass="btn btn-default" ToolTip="Restore the selected object data from recent backup"
                                    OnClick="cmdRestoreDataSelected_Click" />
                                <asp:DropDownList ID="cboRestoredSelected" runat="server">
                                    <asp:ListItem Selected="True" Text="Data" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Schema + Data" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkAutoReset" CssClass="aspnet-checkbox" Text="Reset on Restore" ToolTip="Auto System Reset on DB Restore"
                                    Checked="true" runat="server" />
                                <div class="pull-right">
                                    <asp:Button ID="cmdBackup" runat="server" OnClientClick="return confirm('Are you sure you want to backup the database and delete the existing backup?');"
                                        Text="DB Backup" CssClass="btn btn-default" OnClick="cmdBackup_Click" ToolTip="Create a backup of database objects" />
                                    &nbsp;<asp:Button ID="cmdRestore" OnClientClick="return confirm('WARNING: This will delete all existing records in database. Are you sure you want to restore the recent database backup?');"
                                        runat="server" Text="DB Restore" CssClass="btn btn-danger" OnClick="cmdRestore_Click"
                                        ToolTip="Restore database objects from recent backup" />
                                    &nbsp;
                                        <asp:Button ID="cmdDrop" runat="server" OnClientClick="return confirm('Are you sure you want to drop all database objects?');"
                                            Text="Drop Objects" OnClick="cmdDrop_Click" CssClass="btn btn-default" ToolTip="Drop all database objects" />
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True"
                                CellPadding="4" DataSourceID="sourceHeaders" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                                PageSize="20" CssClass="table table-condensed table-borderless">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#507CD1" CssClass="HeaderStyleLeft" Font-Bold="True" ForeColor="White" />
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
                                        <HeaderStyle CssClass="HeaderStyleCenter" Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButtonEdit" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                                AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' ToolTip="Edit" />
                                            <asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Custom_Delete"
                                                ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                                CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="IdentityColumn" HeaderText="Identity" SortExpression="IdentityColumn" />
                                    <asp:BoundField DataField="LastRecordId" HeaderText="Last Record" SortExpression="LastRecordId" />
                                    <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                                    <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                                    <asp:TemplateField HeaderText="Selected">
                                        <HeaderStyle CssClass="HeaderStyleCenter" Width="20px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" CommandName="Custom_Select" ImageUrl='<%# WebHelper.SetStateImage(Eval("IsSelected")) %>'
                                                AlternateText="Select" ToolTip="Select" CommandArgument='<%# Eval("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="sourceHeaders" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.Setup"></asp:ObjectDataSource>
                        </div>
                    </asp:View>
                    <asp:View runat="server" ID="viewEditObject">
                    </asp:View>
                </asp:MultiView>
                <br />
                <br />
                <div id="divMsgs" runat="server" style="padding: 5px;">
                </div>
            </div>
        </form>
    </div>
</body>
</html>
