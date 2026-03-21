<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebResources.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebResources" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc1" %>
<uc1:WebGenericTab ID="WebGenericTab1" runat="server" Title="Resources" />
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default" runat="server" Text="Create New" OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdOpen" runat="server" CssClass="btn btn-default" Text="Browse..." CausesValidation="False"
            OnClick="cmdOpen_Click" />
        <div class="pull-right">
            <asp:DropDownList ID="cboContentTypes" DataValueField="Id" DataTextField="Text" runat="server"
                AutoPostBack="True" AppendDataBoundItems="true" CssClass="input" OnSelectedIndexChanged="cboContentTypes_SelectedIndexChanged">
                <asp:ListItem Value="-2">* Show All *</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
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
        <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="sourceHeaders" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Central.WebResources">
    <SelectParameters>
        <asp:ControlParameter ControlID="cboContentTypes" DefaultValue="-2" Name="contentTypeId"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
