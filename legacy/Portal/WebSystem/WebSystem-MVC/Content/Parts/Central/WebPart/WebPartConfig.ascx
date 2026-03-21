<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartConfig.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartConfigController" %>
<%@ Register Src="../Controls/WebPartTab.ascx" TagName="WebPartTab" TagPrefix="uc1" %>
<uc1:WebPartTab ID="WebPartTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAddCMS" runat="server" Text="Add" OnClick="cmdAddCMS_Click" CssClass="btn btn-default" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource2" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView2_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton5" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="FileName" HeaderText="File" SortExpression="FileName"
                HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="PageIdentity" HeaderText="Page Number" SortExpression="PageIdentity" />--%>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SelectConfig"
        TypeName="WCMS.WebSystem.WebParts.Central.WebPartConfigController">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="partId" QueryStringField="PartId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
