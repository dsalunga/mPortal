<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Article.AdminTemplates"
    CodeBehind="AdminTemplateComposer.ascx.cs" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:MultiView ID="mtvTemplate" runat="server" ActiveViewIndex="0">
    <asp:View ID="viwContent" runat="server">
        <div class="control-box">
            <div>
                <asp:Button ID="btnAddTemplate" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btnAddTemplate_Click" />
            </div>
        </div>
        <div>
            <asp:GridView ID="grvContent" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                OnRowCommand="grvContent_RowCommand" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Single">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("Name") %>'
                                CommandName="EditSingle" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" AlternateText="Edit" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Multiple">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("Name") %>'
                                CommandName="EditMultiple" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" AlternateText="Edit" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
            </asp:GridView>
        </div>
    </asp:View>
    <asp:View ID="viwEdit" runat="server">
        <div class="control-box">
            <div>
                <asp:Button ID="btnSaveTemplate" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveTemplate_Click" />
                <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div>
            <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Height="450px"
                Width="100%">
            </FCKeditorV2:FCKeditor>
        </div>
    </asp:View>
    <asp:View ID="viwAdd" runat="server">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 45px">Name:
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox>.ascx
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <br />
        <div class="control-box">
            <div>
                <asp:Button ID="btnSaveRegistration" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveRegistration_Click" />
                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click"
                    CausesValidation="False" />
            </div>
        </div>
    </asp:View>
</asp:MultiView>
<asp:HiddenField ID="hidValue" runat="server" />
