<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.SurveyStart"
    CodeBehind="SurveyStart.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td class="td">
            Click an item to start.
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid ID="grdSurveys" runat="server" CellPadding="3" BorderStyle="None" BorderColor="#CCCCCC"
                Width="100%" BorderWidth="1px" BackColor="White" AutoGenerateColumns="False"
                OnItemCommand="grdSurveys_ItemCommand">
                <FooterStyle ForeColor="#000066" BackColor="White" />
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999" />
                <ItemStyle ForeColor="#000066" />
                <HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#006699">
                </HeaderStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="Id" HeaderText="ID"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Title">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandName="survey" ID="Linkbutton1" NAME="Linkbutton1">
								<%# Eval("Title") %>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </td>
    </tr>
</table>
