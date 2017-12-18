<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Agent.Dashboard" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdRun" runat="server" Text="Run" CssClass="btn btn-default" OnClick="cmdRun_Click"
            OnClientClick="return confirm('Are you sure you want to START the Task Scheduler Agent?');" />
        <asp:Button ID="cmdStop" runat="server" Text="Stop" CssClass="btn btn-default" OnClick="cmdStop_Click"
            OnClientClick="return confirm('Are you sure you want to STOP the Task Scheduler Agent?');" Visible="false" />
        <asp:Button ID="cmdTerminate" runat="server" Text="Terminate" CssClass="btn btn-default"
            OnClientClick="return confirm('Are you sure you want to TERMINATE the Task Scheduler Agent?');"
            OnClick="cmdTerminate_Click" />&nbsp;
                <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" OnClick="cmdRefresh_Click"
                    CssClass="btn btn-default" />
    </div>
</div>
<div style="padding: 5px;" align="left">
    <asp:Literal ID="lProcesses" runat="server"></asp:Literal>
</div>
