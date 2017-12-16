<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Newsletter.CONTENTCMS_eNewsletter" Codebehind="CCMS_eNewsletter.ascx.cs" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="viewGrid" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="control_box">
                    <asp:Button ID="btnAdd" Width="85px" runat="server" Text="Add" OnClick="btnAdd_Click"
                        Height="30px" />
                    <asp:Button ID="btnDelete" Width="85px" runat="server" Text="Delete" OnClick="btnDelete_Click"
                        Height="30px" /></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="SqlDataSource1" GridLines="None" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="ENLEmailID" OnRowCommand="GridView1_RowCommand">
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
                                    <%# DataBinder.Eval(Container.DataItem, "ENLEmailID", "<input type='checkbox' value='{0}' name='chkChecked'>")%>
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit_item" ImageUrl="~/_CMS/Images/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ENLEmailID") %>'>
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download">
                                <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="download_item" ImageUrl="~/_CMS/Images/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ENLEmailID") %>'>
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                            <asp:BoundField DataField="SenderEmail" HeaderText="Sender E-mail" SortExpression="SenderEmail" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="Newsletter.SELECT_eNewsletterEmail" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewEdit" runat="server">
        <table border="0">
            <tr>
                <td style="width: 140px">
                    ID:
                </td>
                <td>
                    <asp:Literal ID="litID" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 140px">
                    eNewsletter Title:
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                        ErrorMessage="eNewsletter Title">*</asp:RequiredFieldValidator></td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Columns="75"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 140px">
                    Sender E-mail:
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Sender E-mail">*</asp:RequiredFieldValidator></td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Columns="75"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <fckeditorv2:FCKeditor ID="fckContent" runat="server" Height="400px" Width="100%">
                    </fckeditorv2:FCKeditor>
                </td>
            </tr>
            <tr>
                <td class="control_box" colspan="2">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td nowrap style="height: 24px">
                                <asp:Button ID="btnSave" Width="85px" runat="server" Text="Save" OnClick="btnSave_Click" Height="30px" />
                                <asp:Button ID="btnUpdate" Width="85px" runat="server" Text="Update" OnClick="btnUpdate_Click" Height="30px" />
                                <asp:Button ID="btnCancel" Width="85px" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CausesValidation="False" Height="30px" /></td>
                            <td width="100%" style="height: 24px">
                                <asp:Label ID="lblNotify" runat="server" ForeColor="Red"></asp:Label></td>
                            <td style="height: 24px">
                                <asp:Button ID="cmdParse" runat="server" Text="Convert URL" OnClick="cmdParse_Click"
                                    CausesValidation="False" Height="30px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
            ShowMessageBox="True" ShowSummary="False" />
    </asp:View>
</asp:MultiView>