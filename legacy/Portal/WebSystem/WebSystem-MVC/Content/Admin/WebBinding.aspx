<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="_CMS_Binding" Codebehind="WebBinding.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td class="header">
                Binding</td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            Host Header:</td>
                        <td>
                            <asp:TextBox ID="txtHostHeader" runat="server" Columns="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Port:</td>
                        <td>
                            <asp:TextBox ID="txtPort" runat="server" Columns="10">80</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td width="125">
                            IP Address:</td>
                        <td>
                            <asp:TextBox ID="txtIP" runat="server" Columns="30" ReadOnly="True">*</asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" class="ControlBox">
                <asp:Button ID="cmdUpdate" runat="server" Width="85px" Text="Update" OnClick="cmdUpdate_Click"
                    Font-Bold="True" Height="30px" />
                <asp:Button ID="cmdCancel" runat="server" Width="85px" Text="Cancel" OnClick="cmdCancel_Click"
                    CausesValidation="False" Height="30px" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="vsMessages" runat="server" HeaderText="The following are required:"
        ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
