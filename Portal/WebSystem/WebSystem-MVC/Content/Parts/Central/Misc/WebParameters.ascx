<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebParameters.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebParametersController" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc1" %>
<uc1:WebGenericTab ID="WebGenericTab1" runat="server" Title="Parameters" />
<div runat="server" id="panelControlBox" class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default" Text="Add New"
            OnClick="cmdAdd_Click" />
        <div class="pull-right">
            <asp:Button ID="cmdDone" runat="server" CssClass="btn btn-default" Text="Done"
                OnClick="cmdDone_Click" />
        </div>
    </div>
</div>
<div runat="server" id="panelMain">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="23">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonEdit" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' Visible="false" />
                    <asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Custom_Delete"
                        ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" Visible="false" />
            <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="IsRequired" HeaderText="Required" SortExpression="IsRequired" />--%>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.WebParametersController">
        <SelectParameters>
            <asp:QueryStringParameter Name="objectId" QueryStringField="ObjectId" Type="Int32"
                DefaultValue="-1" />
            <asp:QueryStringParameter Name="recordId" QueryStringField="RecordId" Type="Int32"
                DefaultValue="-1" />
            <asp:QueryStringParameter Name="key" QueryStringField="Key" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<asp:Label runat="server" EnableViewState="false" ID="lblStatus" ForeColor="Red"></asp:Label>