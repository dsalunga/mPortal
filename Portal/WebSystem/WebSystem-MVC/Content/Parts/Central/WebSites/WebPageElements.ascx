<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPageElements.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPageElementsController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebPagePanelTab.ascx" TagName="PanelTab" TagPrefix="uc1" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc2" %>

<uc2:WebGenericTab ID="WebGenericTab1" runat="server" />
<br />
<uc1:PanelTab ID="PanelTab1" runat="server" />
<div style="margin: 8px 0px 2px 0px">
    Panel:&nbsp;<asp:DropDownList ID="cboPanel" CssClass="input" runat="server" DataSourceID="ObjectDataSourcePanels"
        DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="cboPanel_SelectedIndexChanged"
        AppendDataBoundItems="True">
        <asp:ListItem Selected="True" Value="-1"># All Panels #</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:CheckBox ID="chkActive" runat="server" CssClass="aspnet-checkbox" Checked="true" Text="Show Only Active Elements" AutoPostBack="True" OnCheckedChanged="chkActive_CheckedChanged" />
</div>
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default btn-sm" Text="New Element"
            OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdDelete" runat="server" CssClass="btn btn-default btn-sm" Text="Delete"
            OnClick="cmdDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" />
        <div class="pull-right">
            <asp:Button ID="cmdMoveTo" runat="server" CssClass="btn btn-default btn-sm" Text="Move To" OnClick="cmdMoveTo_Click" />
            <asp:DropDownList ID="cboPlaceholders" runat="server" CssClass="input" DataSourceID="ObjectDataSourcePanels"
                DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSourcePanels" runat="server" SelectMethod="GetPanels"
                TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebPageElementsController">
                <SelectParameters>
                    <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" />
                    <asp:QueryStringParameter Name="masterPageId" QueryStringField="MasterPageId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server"  CssClass="table table-borderless" AllowPaging="False" AllowSorting="True"
    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
    Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
    PageSize="15">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerSettings PageButtonCount="25" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
            </HeaderTemplate>
            <ItemTemplate>
                <input type='checkbox' value='<%# Eval("Key") %>' name='chkChecked' />
            </ItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions" Visible="false">
            <HeaderStyle HorizontalAlign="Center" Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ToolTop="Edit" CommandArgument='<%# Eval("Key") %>' />
                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Load_CMS" ImageUrl="~/Content/Assets/Images/Common/Objects.gif"
                    ToolTip="Settings" CommandArgument='<%# Eval("Key") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HtmlEncode="false" HeaderText="Name" SortExpression="Name"
            HeaderStyle-HorizontalAlign="Left" Visible="false">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
            SortExpression="NameUrl" HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:HyperLinkField>
        <asp:BoundField DataField="ObjectName" HeaderText="Owner" SortExpression="ObjectName"
            HeaderStyle-HorizontalAlign="Left" HtmlEncode="false">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="PartName" HeaderText="WebPart" SortExpression="PartName"
            HeaderStyle-HorizontalAlign="Left" HtmlEncode="false">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="PanelName" HeaderText="Panel" SortExpression="PanelName"
            HeaderStyle-HorizontalAlign="Left" HtmlEncode="false">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
            <ItemStyle HorizontalAlign="Center" Width="25px" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Active" SortExpression="Active">
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                    ID="Image1" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectElements"
    TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebPageElementsController">
    <SelectParameters>
        <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" />
        <asp:QueryStringParameter Name="masterPageId" QueryStringField="MasterPageId" Type="Int32" />
        <asp:ControlParameter ControlID="cboPanel" DefaultValue="-1" Name="templatePanelId"
            PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="chkActive" DefaultValue="true" Name="onlyActive" PropertyName="Checked" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
