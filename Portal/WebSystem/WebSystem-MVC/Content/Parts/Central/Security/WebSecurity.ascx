<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSecurity.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebSecurity" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc2" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<uc2:WebGenericTab ID="SecurityTab" runat="server" Title="Security" />
<asp:HiddenField ID="hidSetId" runat="server" Value="-1" />
<asp:HiddenField ID="hObjectId" runat="server" Value="-1" />
<asp:HiddenField ID="hRecordId" runat="server" Value="-1" />
<uc1:TabControl ID="TabControl1" ThemeName="green" runat="server" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
    SelectedIndex="0" />
<asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
    <asp:View ID="viewPublicAccounts" runat="server">
        <div class="no-bottom-margin" style="padding-bottom: 5px;">
            <asp:Button ID="cmdPublicAdd" ClientIDMode="Static" runat="server" CssClass="btn btn-default btn-sm" Text="Add..."
                OnClick="cmdPublicAdd_Click" OnClientClick="return AddPublic_Click();" />
            <asp:TextBox ID="txtPublicAdd" runat="server" CssClass="input" ClientIDMode="Static" Columns="50">
            </asp:TextBox>
        </div>
        <asp:GridView ID="gridPublicAccounts" runat="server" CssClass="table table-borderless" CellPadding="4" ForeColor="#333333"
            GridLines="None" Width="100%" DataKeyNames="Id" AutoGenerateColumns="False" OnRowCommand="gridPublicAccounts_RowCommand"
            AllowPaging="false" AllowSorting="True" DataSourceID="ObjectDataSourcePublic"
            PageSize="15">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                        <asp:ImageButton runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                            CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SecurityEntityName" HeaderStyle-HorizontalAlign="Left"
                    HeaderText="Account" SortExpression="SecurityEntityName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="SecurityEntityFullName" HeaderStyle-HorizontalAlign="Left"
                    HeaderText="Name" SortExpression="SecurityEntityFullName" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourcePublic" runat="server" SelectMethod="SelectPublicAccess"
            TypeName="WCMS.WebSystem.WebParts.Central.Security.WebSecurity">
            <SelectParameters>
                <asp:ControlParameter ControlID="hRecordId" DefaultValue="0" Name="recordId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hObjectId" DefaultValue="0" Name="objectId" PropertyName="Value"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
    <asp:View ID="viewAccounts" runat="server">
        <div class="no-bottom-margin" style="padding-bottom: 5px;">
            <asp:Button ID="cmdAdd" ClientIDMode="Static" runat="server" CssClass="btn btn-default btn-sm" Text="Add..."
                OnClick="cmdAdd_Click" OnClientClick="return Add_Click();" />
            <asp:TextBox ID="txtAdd" ClientIDMode="Static" CssClass="input" runat="server" Columns="50">
            </asp:TextBox>
        </div>
        <asp:GridView ID="gridAccounts" runat="server" CssClass="table table-borderless" CellPadding="4" ForeColor="#333333"
            GridLines="None" Width="100%" DataKeyNames="Id" AutoGenerateColumns="False" OnRowCommand="gridAccounts_RowCommand"
            AllowPaging="false" AllowSorting="True" DataSourceID="ObjectDataSourceMgmt" PageSize="15">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                        <asp:ImageButton runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                            CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SecurityEntityName" HeaderStyle-HorizontalAlign="Left"
                    HeaderText="Account" SortExpression="SecurityEntityName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="SecurityEntityFullName" HeaderStyle-HorizontalAlign="Left"
                    HeaderText="Name" SortExpression="SecurityEntityFullName" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceMgmt" runat="server" SelectMethod="SelectMgmtAccess"
            TypeName="WCMS.WebSystem.WebParts.Central.Security.WebSecurity">
            <SelectParameters>
                <asp:ControlParameter ControlID="hRecordId" DefaultValue="0" Name="recordId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hObjectId" DefaultValue="0" Name="objectId" PropertyName="Value"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
    <asp:View ID="viewAccountSecurity" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <strong>Permissions for&nbsp;<span runat="server" id="lblObjectName"></span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList DataTextField="Name" CssClass="aspnet-checkbox" DataValueField="Id" ID="cblPermisssions" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
        <div class="control-box">
            <div>
                <asp:Button ID="cmdOK" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="cmdOK_Click" />
                <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="viewPublicAccountSecurity" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <strong>Permissions for&nbsp;<span runat="server" id="lblPublicObjectName"></span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList DataTextField="Name" CssClass="aspnet-checkbox" DataValueField="Id" ID="cblPublicPermissions"
                        runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
        <div class="control-box no-botttom-margin">
            <div>
                <asp:Button ID="cmdPublicOK" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="cmdPublicOK_Click" />
                <asp:Button ID="cmdPublicCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
                    OnClick="cmdPublicCancel_Click" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="viewIPAddresses" runat="server">
        <div class="min-bottom-margin">
            <asp:Button ID="cmdAddIPAddress" runat="server" CssClass="btn btn-default btn-sm" Text="Add" OnClick="cmdAddIPAddress_Click" />
            <asp:TextBox ID="txtIPAddress" runat="server" Columns="50" CssClass="input">
            </asp:TextBox>
        </div>
        <asp:GridView ID="gridIPAddresses" runat="server" CssClass="table table-borderless" CellPadding="4" ForeColor="#333333"
            GridLines="None" Width="100%" DataKeyNames="Id" AutoGenerateColumns="False" OnRowCommand="gridIPAddresses_RowCommand"
            AllowPaging="false" AllowSorting="True" DataSourceID="ObjectDataSourceIP" PageSize="15">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" Visible="false" runat="server" CommandName="Custom_Edit"
                            ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                            CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IPAddress" HeaderStyle-HorizontalAlign="Left" HeaderText="IP Address"
                    SortExpression="IPAddress">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceIP" runat="server" SelectMethod="SelectIPAccess"
            TypeName="WCMS.WebSystem.WebParts.Central.Security.WebSecurity">
            <SelectParameters>
                <asp:ControlParameter ControlID="hObjectId" DefaultValue="0" Name="objectId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hRecordId" DefaultValue="0" Name="recordId" PropertyName="Value"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
</asp:MultiView>
<br />
<asp:Label runat="server" EnableViewState="false" ID="lblStatus" ForeColor="Red"></asp:Label>
<script type="text/javascript">
    function Add_Click() {
        var addValue = $("#txtAdd").val().Trim();

        if (addValue == "") {
            ShowAccountBrowser("txtAdd", -1, 1, 1, 1, "cmdAdd");
            return false;
        }

        return true;
    }

    function AddPublic_Click() {
        var addValue = $("#txtPublicAdd").val().Trim();

        if (addValue == "") {
            ShowAccountBrowser("txtPublicAdd", -1, 1, 1, 1, "cmdPublicAdd");
            return false;
        }

        return true;
    }
</script>