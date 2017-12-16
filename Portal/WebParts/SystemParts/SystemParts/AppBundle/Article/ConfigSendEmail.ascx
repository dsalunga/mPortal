<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigSendEmail.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Article.ConfigSendEmail" %>
<asp:HiddenField runat="server" ID="hRecipients" Value="" />
<asp:HiddenField runat="server" ID="hExcluded" Value="" />
<asp:HiddenField runat="server" ID="hView" Value="" />
<h3>Send Alert Email</h3>
<br />
<strong style="font-size: larger; color: Green">Email Options</strong>
<br />
<br />
<div>
    Subject:&nbsp;<asp:TextBox ID="txtSubject" runat="server" CssClass="span6"></asp:TextBox>
    &nbsp;(<a runat="server" target="_blank" id="linkPreview" href="/Content/Parts/Article/EmailPreview.ashx?ArticleId="
        title="Preview this Article">Preview</a>)
</div>
<div>
    Send as:&nbsp;<asp:RadioButtonList CssClass="aspnet-radio" ID="radioSendAs" runat="server" ClientIDMode="Static"
        RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Selected="True" Value="0">To</asp:ListItem>
        <asp:ListItem Value="1">CC</asp:ListItem>
        <asp:ListItem Value="2">BCC</asp:ListItem>
    </asp:RadioButtonList>
</div>
<br />
<br />
<strong style="font-size: larger; color: Green">Recipients</strong>
<asp:RadioButtonList CssClass="aspnet-radio" ID="RadioButtonList1" runat="server" AutoPostBack="True" Enabled="False"
    Visible="false">
    <asp:ListItem Selected="True" Value="-1">Current List (List Name Here)</asp:ListItem>
    <asp:ListItem Value="-1">All Lists where this Article is published</asp:ListItem>
</asp:RadioButtonList>
<br />
<br />
<div style="padding-bottom: 3px;">
    Show:&nbsp;<span style="font-weight: bold" id="lblCurrentView" runat="server"></span>&nbsp;(<asp:LinkButton
        ID="cmdChangeView" runat="server" OnClick="cmdChangeView_Click"></asp:LinkButton>)
</div>
<div class="no-bottom-margin">
    <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default" Text="Add" OnClick="cmdAdd_Click" />
    <asp:TextBox ID="txtAdd" runat="server" Columns="60"></asp:TextBox>
    <input id="cmdBrowse" onclick="ShowAccountBrowser('<% =txtAdd.ClientID %>    ', -1, 1, 1);"
        type="button" class="btn btn-default" value="Browse..." />
    <asp:Button ID="cmdReset" CssClass="btn btn-default" runat="server" Text="Reset Recipients" OnClick="cmdReset_Click" />
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="15"
    EmptyDataText="There are no recipients found." AllowPaging="True">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName"
            HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="">
            <HeaderStyle HorizontalAlign="center" Width="20px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                    CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to remove this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGroups"
    TypeName="WCMS.WebSystem.WebParts.Article.ConfigSendEmail" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" DefaultValue="-1" />
        <asp:ControlParameter Name="view" ControlID="hView" PropertyName="Value" Type="String" />
        <asp:ControlParameter ControlID="hRecipients" Name="customRecipients" PropertyName="Value"
            Type="String" />
        <asp:ControlParameter ControlID="hExcluded" Name="exclude" PropertyName="Value" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<p id="lblStatus" runat="server" style="color: Red;">
</p>
<br />
<div class="control-box">
    <div>
        <asp:Button ID="cmdSend" runat="server" CssClass="btn btn-primary" Text="Send"
            OnClick="cmdSend_Click" />&nbsp;<asp:Button ID="cmdCancel" runat="server" Text="Cancel"
                CssClass="btn btn-default" OnClick="cmdCancel_Click" />
    </div>
</div>
