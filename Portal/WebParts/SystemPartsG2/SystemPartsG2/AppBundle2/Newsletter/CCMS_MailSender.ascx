<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Newsletter.MailSender" Codebehind="CCMS_MailSender.ascx.cs" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td valign="middle" nowrap="nowrap" class="control_box">
            <asp:Button ID="cmdSendPending" runat="server" Text="Set Status to Pending" OnClick="cmdSendPending_Click" Height="30px" />
            <asp:Button ID="cmdSent" runat="server" Text="Set Status to Done" OnClick="cmdSent_Click" Height="30px" />
            &nbsp;&nbsp;Set Status to:&nbsp;
            <asp:DropDownList ID="cboSelect" runat="server">
                <asp:ListItem Value="All">All</asp:ListItem>
                <asp:ListItem Value="Selected" Selected="True">Selected</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="eNewsletterID" OnRowCommand="GridView1_RowCommand" PageSize="15">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "eNewsletterID", "<input type='checkbox' value='{0}' name='chkChecked'>")%>
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Done" SortExpression="IsDone">
                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(DataBinder.Eval(Container, "DataItem.IsDone")) %>'
                                ID="Image1"></asp:Image>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Success" SortExpression="IsSuccess">
                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(DataBinder.Eval(Container, "DataItem.IsSuccess")) %>'
                                ID="Image1"></asp:Image>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmailAddress" HeaderText="Email" SortExpression="EmailAddress" />
                    <asp:BoundField DataField="DateTimeSent" HeaderText="Last Sent Date" SortExpression="DateTimeSent" />
                    
                </Columns>
                <PagerSettings PageButtonCount="50" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="Newsletter.SELECT_Emails" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td style="height: 25px">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            STEP 1: set recipient status to pending. &nbsp;&nbsp;STEP 2: select eNewsletter
            to send. &nbsp;&nbsp;STEP 3: click "Send eNewsletter"</td>
    </tr>
    <tr>
        <td valign="middle" nowrap="nowrap" class="control_box">
            Select eNewsletter:&nbsp;<asp:DropDownList ID="cboNewsletters" runat="server" DataSourceID="SqlDataSource2"
                DataTextField="Title" DataValueField="ENLEmailID">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="Newsletter.SELECT_eNewsletters" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            <asp:Button ID="cmdSend" runat="server" Text="Send eNewsletter" OnClick="cmdSend_Click" Height="30px" />
            <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" OnClick="cmdRefresh_Click" Height="30px" /></td>
    </tr>
    <tr>
        <td valign="middle" style="padding: 5px; font: bold 14px Verdana, Arial, Helvetica, sans-serif;
            color: #ff0000;" align="left">
            <asp:Literal ID="lProcesses" runat="server"></asp:Literal></td>
    </tr>
</table>
