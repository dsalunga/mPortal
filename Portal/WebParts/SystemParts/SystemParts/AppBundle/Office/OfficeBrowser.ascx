<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfficeBrowser.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Office.OfficeBrowser" %>
<div style="float: left">
    <asp:DropDownList ID="cboLevel1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLevel1_SelectedIndexChanged">
    </asp:DropDownList>
    &nbsp;&#9658;&nbsp;
    <asp:DropDownList ID="cboLevel2" DataTextField="Name" DataValueField="Id" runat="server"
        AutoPostBack="true" OnSelectedIndexChanged="cboLevel2_SelectedIndexChanged">
    </asp:DropDownList>
    &nbsp;&#9658;&nbsp;
    <asp:DropDownList ID="cboLevel3" DataTextField="Name" DataValueField="Id" runat="server"
        AutoPostBack="true">
    </asp:DropDownList>
</div>
<div style="float: right">
    <asp:TextBox ID="txtSearch" Columns="15" runat="server"></asp:TextBox>
    <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" />
    <asp:Button ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" />
</div>
<br />
<br />
<div>
    <asp:GridView ID="GridView1" CssClass="grid-padding" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
        ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="45px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" ImageUrl="/Content/Assets/Images/Common/ico_edit2.gif" runat="server"
                        NavigateUrl='<%# Eval("OfficeUrl") %>' ToolTip="View details"></asp:HyperLink><asp:ImageButton
                            ID="ImageButton2" runat="server" CommandName="View_Children" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                            AlternateText="Children" ToolTip="View Next Level Items" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:HyperLinkField DataNavigateUrlFields="OfficeUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" Target="_blank" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:BoundField DataField="Name" ItemStyle-Font-Bold="true" HeaderText="Name" SortExpression="Name"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" SortExpression="EmailAddress"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson"
                HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" ToolTip="View Details" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="View_Children" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Children" ToolTip="View Next Level Items" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Office.OfficeBrowser">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboLevel1" DefaultValue="-1" Name="level1" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="cboLevel2" DefaultValue="-1" Name="level2" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="cboLevel3" DefaultValue="-1" Name="level3" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<div>
    Office Details
</div>
