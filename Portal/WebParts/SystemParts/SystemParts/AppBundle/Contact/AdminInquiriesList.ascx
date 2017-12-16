<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.AdminInquiriesList"
    CodeBehind="AdminInquiriesList.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdDelete" CssClass="btn btn-default" OnClientClick="return confirm('Are you sure you want to delete the selected items?');" Text="Delete" runat="server" OnClick="cmdDelete_Click" />
        <asp:Button ID="cmdActive" CssClass="btn btn-default" Text="Mark As Read" runat="server"
            OnClick="cmdActive_Click" />
        <asp:Button ID="cmdDeactivate" CssClass="btn btn-default" Text="Mark As Unread" runat="server"
            OnClick="cmdDeactivate_Click" />
        <asp:Button ID="cmdDownload" CssClass="btn btn-default" Text="Download List" runat="server"
            OnClick="cmdDownload_Click" />
        &nbsp;Web Site:
            <asp:DropDownList ID="cboSites" CssClass="input" runat="server" OnSelectedIndexChanged="cboSites_SelectedIndexChanged"
                DataValueField="Id" DataTextField="Name" AppendDataBoundItems="True" AutoPostBack="True">
                <asp:ListItem Selected="True" Value=""># All Inquiries #</asp:ListItem>
            </asp:DropDownList>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="InquiryId" OnRowCommand="GridView1_RowCommand" PageSize="15"
        DataSourceID="ObjectDataSource1">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("InquiryID")%>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("InquiryId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetMsgImage((int)Eval("IsActive")) %>'
                        ID="Image1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SenderName" HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
                SortExpression="SenderName" />
            <asp:BoundField DataField="Subject" HeaderStyle-HorizontalAlign="Left" HeaderText="Subject"
                SortExpression="Subject" />
            <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                SortExpression="Email" />
            <asp:BoundField DataField="Phone" HeaderStyle-HorizontalAlign="Left" HeaderText="Phone"
                SortExpression="Phone" />
            <asp:BoundField DataField="InquiryType" HeaderStyle-HorizontalAlign="Left" HeaderText="Type"
                SortExpression="InquiryType" />
            <asp:BoundField DataField="SendTo" HeaderStyle-HorizontalAlign="Left" HeaderText="To"
                SortExpression="SendTo" />
            <asp:BoundField DataField="InqDateTime" HeaderStyle-HorizontalAlign="Left" HeaderText="Date"
                SortExpression="InqDateTime" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Contact.AdminInquiriesList"></asp:ObjectDataSource>
</div>
