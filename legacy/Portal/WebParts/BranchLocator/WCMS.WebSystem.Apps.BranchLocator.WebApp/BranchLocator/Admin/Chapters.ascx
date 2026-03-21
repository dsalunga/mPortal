<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chapters.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.BranchLocator.WebOfficesController" %>
<div class="breadcrumb" style="margin-bottom: 10px; margin-top: 5px">
    <span runat="server" id="lblBreadcrumb"></span>&nbsp;<asp:LinkButton ID="cmdUp" runat="server" CssClass="label label-info" Visible="false" OnClick="cmdUp_Click"><span class='glyphicon glyphicon-arrow-up' aria-hidden='true'></span> Up</asp:LinkButton>&nbsp;<a id="linkEdit" runat="server" visible="false" class="label label-info" href="#"><span class='glyphicon glyphicon-edit' aria-hidden='true'></span> Edit</a>
</div>
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAddFull" CssClass="btn btn-default btn-sm" runat="server" Text="New Chapter"
            OnClick="cmdAddFull_Click" />&nbsp;
        <div class="pull-right">
            <asp:Button ID="cmdFix" CssClass="btn btn-default btn-sm" runat="server" Text="Fix Data" OnClick="cmdFix_Click" />
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="25px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Properties" CommandArgument='<%# Eval("Id") %>' />--%>
                    <%--<asp:ImageButton ID="ImageButton1" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Children" ToolTip="Children" Visible="false" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" Visible="false" CommandArgument='<%# Eval("Id") %>'
                        ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />&nbsp;--%>
                    <%--<asp:LinkButton ID="LinkDelete" runat="server" CssClass="btn1 1btn-default 1btn-sm" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                    &nbsp;--%>
                    <a href="<%# Eval("EditUrl") %>" title="View Children" class="btn1 1btn-default 1btn-sm"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="ChildrenUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" />
            <asp:HyperLinkField DataNavigateUrlFields="SetLocationUrl" DataTextFormatString="<span class='glyphicon glyphicon-map-marker' aria-hidden='true'></span> {0}" DataTextField="Address" HeaderText="Address"
                SortExpression="Address" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LatLng" HeaderText="Coordinates" SortExpression="LatLng" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="AccessType" HeaderText="Access" SortExpression="AccessType" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Id" HeaderStyle-Width="50px" HeaderText="ID" SortExpression="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="WCMS.WebSystem.Apps.BranchLocator.WebOfficesController">
        <SelectParameters>
            <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
