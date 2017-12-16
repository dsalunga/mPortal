<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Article.TemplatesController"
    CodeBehind="AdminTemplateManager.ascx.cs" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Content/Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc1" %>
<asp:HiddenField ID="hidElementId" runat="server" Value="-1" />
<asp:Panel ID="pnlContent" runat="server" Width="100%">
    <div class="control-box">
        <div>
            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btnAdd_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-default" OnClick="btnDelete_Click" />
        </div>
    </div>
    <div>
        <asp:GridView ID="grvContent" runat="server" CellPadding="4" ForeColor="#333333" CssClass="table table-borderless"
            GridLines="None" DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
            DataSourceID="ObjectDataSourceTemplates" AllowSorting="True" OnRowCommand="grvContent_RowCommand">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <HeaderTemplate>
                        <input type="checkbox" value="chkMain" onclick="CheckAll(this, 'grvContent');" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" value='<%# Eval("Id") %>' name="grvContent" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="10px" />
                    <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%# Eval("Id") %>' CommandName="ContentEdit"
                            runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-HorizontalAlign="Left" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceTemplates" runat="server" SelectMethod="GetTemplates"
            TypeName="WCMS.WebSystem.WebParts.Article.TemplatesController"></asp:ObjectDataSource>
    </div>
</asp:Panel>
<asp:Panel ID="pnlContentEdit" runat="server" Width="100%" Visible="False">
    <table border="0" width="100%">
        <tr>
            <td style="width: 90px">ID:
            </td>
            <td>
                <asp:Literal ID="litID" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>Name:
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtName"
                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Date Format:
            </td>
            <td>
                <asp:TextBox ID="txtDateFormat" runat="server" Columns="100" CssClass="input"></asp:TextBox>
            </td>
        </tr>
        <%--<tr>
            <td>
                Template:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTemplate"
                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtTemplate" runat="server" Columns="75"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td valign="top">Image:
            </td>
            <td>
                <FCKeditorV2:FCKeditor ID="fckImage" runat="server" Height="180px" ToolbarSet="Banner"
                    Width="98%">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td valign="top">Item Template:
            </td>
            <td>
                <uc1:TextEditor Height="200px" ID="txtItemTemplate" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">List Template:
            </td>
            <td>
                <uc1:TextEditor Height="180px" ID="txtListTemplate" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">Details Template:
            </td>
            <td>
                <uc1:TextEditor Height="200px" ID="txtDetailsTemplate" runat="server" />
            </td>
        </tr>
    </table>
    <div class="control-box">
        <div>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                Text="Cancel" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Panel>
<%--
<asp:Panel ID="pnlTemplate" runat="server" Width="100%" Visible="false">
    <table border="0" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Multiple:
            </td>
        </tr>
        <tr>
            <td class="ControlBox">
                <asp:Button ID="btnEMDelete" runat="server" Text="Delete" Width="85px" OnClick="btnEMDelete_Click"
                    Height="30px" />
                <asp:Button ID="btnParseMultiple" runat="server" OnClick="btnParseMultiple_Click"
                    Text="Parse Template" Height="30px" />
                <asp:Button ID="btnDone" runat="server" OnClick="btnDone_Click" Text="Done" Width="85px"
                    Height="30px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvMultiple" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                    DataSourceID="ObjectDataSourceElementsMultiple" AllowSorting="True" OnRowCommand="grvMultiple_RowCommand">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="EID">
                            <HeaderTemplate>
                                <input type="checkbox" value="chkMain" onclick="CheckAll(this,'grvMultiple');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" value='<%# Eval("ColumnId") %>' name="grvMultiple" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="10px" />
                            <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" CommandArgument='<%# Eval("ColumnId") %>' CommandName="ContentMultiple"
                                    runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:TemplateField HeaderText="Tag">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemTemplate>
                                <%# ElementName((int)Eval("Id")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceElementsMultiple" runat="server" SelectMethod="GetElements"
                    TypeName="WCMS.WebSystem.WebParts.Article.TemplatesController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hidElementId" DefaultValue="-1" Name="templateId"
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="isSingle" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
                Single:
            </td>
        </tr>
        <tr>
            <td class="ControlBox">
                <asp:Button ID="btnESDelete" runat="server" Text="Delete" Width="85px" OnClick="btnESDelete_Click"
                    Height="30px" />
                <asp:Button ID="btnParseSingle" runat="server" OnClick="btnParseSingle_Click" Text="Parse Template"
                    Height="30px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvSingle" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="100%" AutoGenerateColumns="False" AllowPaging="True" DataSourceID="ObjectDataSourceElements"
                    AllowSorting="True" OnRowCommand="grvSingle_RowCommand">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="EID">
                            <HeaderTemplate>
                                <input type="checkbox" value="chkMain" onclick="CheckAll(this,'grvSingle');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" value='<%# Eval("ColumnId") %>' name="grvSingle" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="10px" />
                            <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" CommandArgument='<%# Eval("ColumnId") %>' CommandName="ContentSingle"
                                    runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:TemplateField HeaderText="Tag">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemTemplate>
                                <%# ElementName((int)Eval("Id")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceElements" runat="server" SelectMethod="GetElements"
                    TypeName="WCMS.WebSystem.WebParts.Article.TemplatesController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hidElementId" DefaultValue="-1" Name="templateId"
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="isSingle" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="plnElement" runat="server" Width="100%" Visible="false">
    <table border="0" width="100%">
        <tr>
            <td style="width: 50px">
                ID:
            </td>
            <td>
                <asp:Literal ID="litEID" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="width: 50px">
                Name:
            </td>
            <td>
                <asp:Literal ID="litEName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="width: 50px">
                Tag:
            </td>
            <td>
                <asp:DropDownList ID="ddlElementID" runat="server">
                    <asp:ListItem Value="1">ID</asp:ListItem>
                    <asp:ListItem Value="2">Title</asp:ListItem>
                    <asp:ListItem Value="3">Image</asp:ListItem>
                    <asp:ListItem Value="4">Description</asp:ListItem>
                    <asp:ListItem Value="5">Date</asp:ListItem>
                    <asp:ListItem Value="6">Content</asp:ListItem>
                    <asp:ListItem Value="7">Author</asp:ListItem>
                    <asp:ListItem Value="0">NONE</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table border="0" width="100%">
        <tr>
            <td class="ControlBox">
                <asp:Button ID="btnESave" runat="server" OnClick="btnESave_Click" Text="Save" Width="85px"
                    Height="30px" />
                <asp:Button ID="btnECancel" runat="server" OnClick="btnECancel_Click" Text="Cancel"
                    Width="85px" Height="30px" />
            </td>
        </tr>
    </table>
</asp:Panel>
--%>