<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CMS_Surveys"
    CodeBehind="WM_Surveys_01.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="ControlBox">
            <asp:Button ID="cmdCreate" CssClass="Command" runat="server" Text="Create Survey"
                OnClick="cmdCreate_Click" />
            <asp:Button ID="cmdDelete" runat="server" CssClass="Command" Text="Delete" OnClick="cmdDelete_Click" />
            <!--
			<asp:button id="cmdActivate" runat="server" Text="Activate"></asp:button>
			<asp:button id="cmdDeactivate" runat="server" Text="Deactivate"></asp:button>
			-->
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid ID="grdSurveys" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                CellPadding="3" BackColor="White" BorderWidth="1px" BorderColor="#CCCCCC" OnItemCommand="grdSurveys_ItemCommand">
                <FooterStyle ForeColor="#000066" BackColor="White" />
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999" />
                <ItemStyle Font-Size="10pt" Font-Names="Verdana" ForeColor="#000066" />
                <HeaderStyle Font-Size="10pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White"
                    BackColor="#006699" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="Id" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Id" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
                        DataFormatString="&lt;input type=&quot;checkbox&quot; name=&quot;chkChecked&quot; value=&quot;{0}&quot;&gt;">
                        <HeaderStyle Width="20px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Actions" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                ID="Imagebutton2" ToolTip="Edit" NAME="Imagebutton2" />
                            <asp:ImageButton runat="server" CommandName="pages" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                ID="Imagebutton3" ToolTip="Pages" NAME="Imagebutton2" />
                            <asp:ImageButton ToolTip="Results" runat="server" CommandName="csv" ImageUrl="~/Content/Assets/Images/excel_icon.gif"
                                ID="Imagebutton1" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Title" HeaderText="Survey Title"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Active">
                        <ItemStyle HorizontalAlign="Center" Width="45px" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageAlign="AbsMiddle" ImageUrl='<%# WebHelper.SetStateImage(Eval("IsActive")) %>'
                                ID="Image1" NAME="Image1" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </td>
    </tr>
</table>
