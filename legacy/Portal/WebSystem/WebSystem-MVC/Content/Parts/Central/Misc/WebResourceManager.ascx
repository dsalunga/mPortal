<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebResourceManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebResourceManager" %>
<h1 class="central page-header">Text Resources</h1>
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default btn-sm" runat="server" Text="Create New" OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdSynch" CssClass="btn btn-default btn-sm" runat="server" Text="Synch"
            OnClick="cmdSynch_Click" />
        <div class="pull-right">
            <asp:DropDownList ID="cboContentTypes" DataValueField="Id" CssClass="input" DataTextField="Text" runat="server"
                AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="cboContentTypes_SelectedIndexChanged">
                <asp:ListItem Value="-2">* All Types *</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="cboSites" runat="server" AutoPostBack="true" CssClass="input" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                <asp:ListItem Text="" Value="-2"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless table-condensed"
        CellPadding="4" DataSourceID="sourceHeaders" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="50">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" CssClass="HeaderStyleLeft" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle CssClass="HeaderStyleCenter" Width="18px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%--<asp:ImageButton ID="ImageButtonEdit" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />--%>
                    <asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Custom_Delete"
                        ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />--%>
            <asp:HyperLinkField DataNavigateUrlFields="TitleUrl" DataTextField="Title" HeaderText="Title"
                SortExpression="Title" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
            <asp:BoundField DataField="ContentType" HeaderText="Type" SortExpression="ContentType" />
            <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
        </Columns>
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="sourceHeaders" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Central.WebResourceManager">
    <SelectParameters>
        <asp:ControlParameter ControlID="cboContentTypes" DefaultValue="-2" Name="contentTypeId"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span runat="server" id="lblStatus"></span>