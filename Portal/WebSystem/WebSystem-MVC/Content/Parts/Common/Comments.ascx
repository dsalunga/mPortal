<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Common.CommentView" %>
<div class="wp-Notes">
    <div runat="server" id="panelNewNote">
        <asp:TextBox ID="txtNewNote" runat="server" placeholder="Enter Your Comment" Columns="75" Rows="7" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:CheckBox ID="chkSendSMS" runat="server" Text="Send SMS Alert" />
        <br />
        <br />
    </div>
    <div class="Notes">
        <asp:GridView ID="GridViewNotes" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CellPadding="0" ShowHeader="false" DataKeyNames="Id" DataSourceID="ObjectDataSourceComments"
            ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" PageSize="5"
            EmptyDataText="No comments to display.">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Comments">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <strong>
                            <%# Eval("User") %>&nbsp;@&nbsp;<%# Eval("DateCreated", "{0:dd-MMM-yyyy h:mm tt}") %></strong><br />
                        <pre class="content"><%# Eval("Content") %></pre>
                        <br />
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
        <asp:ObjectDataSource ID="ObjectDataSourceComments" runat="server" SelectMethod="SelectComments"
            TypeName="WCMS.WebSystem.WebParts.Common.CommentView">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="ticketId"
                    Type="Int32" />
                <asp:Parameter DefaultValue="" Name="userDisplayFormat" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
