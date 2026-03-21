<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="_CMS_Bindings" Codebehind="WebBindings.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="header" id="tdHeader" runat="server">
                Web Site Bindings</td>
        </tr>
        <tr>
            <td valign="middle" nowrap class="ControlBox">
                <asp:Button ID="cmdAddFull" runat="server" Text="Add" Width="85px" OnClick="cmdAddFull_Click"
                    Height="30px" />
                <asp:Button ID="cmdDone" runat="server" Text="Done" Width="85px" Height="30px" Font-Bold="True" OnClick="cmdDone_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                    Width="100%" AutoGenerateColumns="False" DataKeyNames="BindingID" OnRowCommand="GridView1_RowCommand"
                    PageSize="15">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Actions">
                            <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Admin/Images/ico_edit.gif"
                                    ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# Eval("BindingID") %>'>
                                </asp:ImageButton>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" CommandArgument='<%# Eval("BindingID") %>'
                                    ImageUrl="~/Admin/Images/ico_exit.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HostHeader" HeaderText="Host Header" SortExpression="HostHeader" />
                        <asp:BoundField DataField="Port" HeaderText="Port" SortExpression="Port" />
                        <asp:BoundField DataField="IPAddress" HeaderText="IP Address" SortExpression="IPAddress" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                    SelectMethod="GetData" TypeName="CMSDALTableAdapters.BindingsAdapter">
                    <DeleteParameters>
                        <asp:Parameter Name="BindingID" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:Parameter Name="BindingID" Type="Int32" ConvertEmptyStringToNull="true" />
                        <asp:QueryStringParameter Name="SiteID" QueryStringField="SiteID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
