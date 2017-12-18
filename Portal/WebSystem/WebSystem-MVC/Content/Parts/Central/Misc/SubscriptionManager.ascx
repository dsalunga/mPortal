<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Misc.SubscriptionManager" %>
<%@ Register Src="../Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>
<uc1:WebGroupTab ID="WebGroupTab1" runat="server" />
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-sm" OnClick="cmdDelete_Click"
            OnClientClick="return confirm('Are you sure you want to delete?');" CausesValidation="False" />
        &nbsp;
        <asp:DropDownList ID="cboWebParts" DataTextField="Name" CssClass="input" DataValueField="Id" runat="server"
            DataSourceID="ObjectDataSourceParts" AppendDataBoundItems="true" AutoPostBack="True"
            OnSelectedIndexChanged="cboWebParts_SelectedIndexChanged">
            <asp:ListItem Selected="True" Text="" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        <div class="pull-right">

            <asp:ObjectDataSource ID="ObjectDataSourceParts" runat="server" SelectMethod="SelectPart"
                TypeName="WCMS.WebSystem.WebParts.Central.Misc.SubscriptionManager"></asp:ObjectDataSource>
            <asp:RequiredFieldValidator ID="rfvPage" runat="server" ControlToValidate="txtNavigateURL"
                ErrorMessage="Page to subscribe is required">*</asp:RequiredFieldValidator>
            <asp:TextBox ID="txtNavigateURL" runat="server" Columns="50" CssClass="input" />
            <asp:Button ID="cmdBrowse" CssClass="btn btn-default btn-sm" OnClientClick="BrowseLink('<% =txtNavigateURL.ClientID %>'); return false;"
                runat="server" Text="Browse..." CausesValidation="False" />&nbsp;<asp:Button ID="cmdAddFull"
                    runat="server" Text="Add" CssClass="btn btn-primary btn-sm" OnClick="cmdAddFull_Click" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
    Width="100%" GridLines="None" OnRowCommand="GridView1_RowCommand" PageSize="15">
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
                <input type="checkbox" value='<%# Eval("Id") %>' name="chkChecked">
            </ItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="Center" Width="40px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                    AlternateText="Delete Page" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" OnClientClick="return confirm('Are you sure you want to delete this item?')" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageUrl" HeaderText="Page"
            SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:HyperLinkField>
        <asp:BoundField DataField="GroupName" HeaderText="Group" SortExpression="GroupName"
            HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="PartName" HeaderText="WebPart" SortExpression="PartName"
            HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
    </Columns>
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Misc.SubscriptionManager" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:QueryStringParameter Name="groupId" QueryStringField="GroupId" Type="Int32"
            DefaultValue="-1" />
        <asp:ControlParameter ControlID="cboWebParts" DefaultValue="-1" Name="partId" PropertyName="SelectedValue"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
