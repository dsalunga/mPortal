<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.Controls.TabControlV1"
    EnableViewState="true" CodeBehind="TabControlV1.ascx.cs" %>
<div runat="server" class="tab-control default" id="divTabNav" clientidmode="Static">
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr runat="server" id="trTab" clientidmode="Static">
            <%--
            <td>
                <div class="TabButton" runat="server" id="divTables">
                    <asp:LinkButton ID="cmdTables" runat="server" Text="Mapping&nbsp;Tables" OnClick="cmdTables_Click" /></div>
            </td>
            <td style="width: 3px;" nowrap="nowrap">
            </td>
            --%>
            <td style="width: 100%">
            </td>
        </tr>
        <tr>
            <td colspan="100">
                <div class="TabBar">
                </div>
            </td>
        </tr>
    </table>
</div>
