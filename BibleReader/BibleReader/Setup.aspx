<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Setup.aspx.cs" Inherits="WCMS.BibleReader.Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Inherit">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="ControlBox">
                <div style="float: left">
                    <asp:Button ID="cmdRestoreData" OnClientClick="return confirm('This will delete all existing records in the selected objects. Are you sure you want to restore the recent backup for the selected objects?');"
                        runat="server" Text="Restore Selected" CssClass="Command" ToolTip="Restore the selected object data from recent backup"
                        OnClick="cmdRestoreData_Click" />
                    <asp:DropDownList ID="cboRestoredSelected" runat="server">
                        <asp:ListItem Selected="True" Text="Data" Value="0">
                        </asp:ListItem>
                        <asp:ListItem Text="Schema + Data" Value="1">
                        </asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="float: right">
                    <asp:Button ID="cmdBackup" runat="server" OnClientClick="return confirm('Are you sure you want to backup the database and delete the existing backup?');"
                        Text="Backup Database" CssClass="Command" OnClick="cmdBackup_Click" ToolTip="Create a backup of database objects" />
                    &nbsp;<asp:Button ID="cmdRestore" OnClientClick="return confirm('WARNING: This will delete all existing records in database. Are you sure you want to restore the recent database backup?');"
                        runat="server" ForeColor="Red" Text="Restore Database" CssClass="Command" OnClick="cmdRestore_Click"
                        ToolTip="Restore database objects from recent backup" />
                    &nbsp;
                    <asp:Button ID="cmdDrop" runat="server" OnClientClick="return confirm('Are you sure you want to drop all database objects?');"
                        Text="Drop Objects" OnClick="cmdDrop_Click" CssClass="Command" ToolTip="Drop all database objects" />
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div id="divMsgs" runat="server" style="padding: 5px;">
    </div>
</asp:Content>
