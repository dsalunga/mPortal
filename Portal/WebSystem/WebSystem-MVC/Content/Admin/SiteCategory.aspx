<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="CMS.SiteCategory" Title="Untitled Page" Codebehind="SiteCategory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td class="header">
                Site Category
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td width="125">
                            Category Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                ErrorMessage="Category Name">*</asp:RequiredFieldValidator></td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Image:</td>
                        <td>
                            <asp:TextBox ID="txtFilename" runat="server" ReadOnly="True" Columns="50"></asp:TextBox>&nbsp;
                            <input id="cmdFile" style="width: 88px; height: 20px" type="button" value="Upload"
                                name="cmdFile" runat="server"></td>
                    </tr>
                    <tr>
                        <td>
                            Blurb:</td>
                        <td>
                            <asp:TextBox ID="txtBlurb" runat="server" TextMode="MultiLine" Rows="4" Columns="75"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Rank:</td>
                        <td>
                            <asp:TextBox ID="txtRank" runat="server" Columns="25">100</asp:TextBox>
                            <asp:CheckBox ID="chkMenu" runat="server" Text="Show In Menu"></asp:CheckBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" class="control_box">
                <asp:Button ID="cmdUpdate" runat="server" Width="85px" Text="Update" OnClick="cmdUpdate_Click" Font-Bold="True" Height="30px" />
                <asp:Button ID="cmdCancel" runat="server" Width="85px" Text="Cancel" OnClick="cmdCancel_Click"
                    CausesValidation="False" Height="30px" /></td>
        </tr>
    </table>
</asp:Content>
