<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartAdmin.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartAdminController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebPartTab.ascx" TagName="WebPartTab" TagPrefix="uc1" %>

<uc1:WebPartTab ID="WebPartTab1" runat="server" />
<div class="row">
    <div class="col-md-3 col-sm-4">
        <asp:TreeView ID="tvSections" runat="server" ImageSet="Msdn" NodeIndent="18" OnSelectedNodeChanged="tvSections_SelectedNodeChanged">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" />
            <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                NodeSpacing="1px" VerticalPadding="2px" />
        </asp:TreeView>
    </div>
    <div class="col-md-9 col-sm-8">
        <h1 class="central page-header" id="lblName" runat="server"></h1>
        <div id="tdSection" runat="server">
            <asp:Button ID="cmdEdit" runat="server" Text="Edit" CssClass="btn btn-default btn-sm" OnClick="cmdEdit_Click" />
            <%--<asp:Button ID="cmdCMS" runat="server" Text='Edit Controls' Height="30px" OnClick="cmdCMS_Click" />--%>
            <br />
            <br />
        </div>
        <%--<h1 class="central page-header">
            <span runat="server" id="lblName"></span>&nbsp;-&nbsp;Admin Controls
        </h1>--%>
        <div class="control-box no-bottom-margin">
            <div>
                <asp:Button ID="cmdAddFull" runat="server" Text="Add" CssClass="btn btn-default btn-sm" OnClick="cmdAddFull_Click" />
                <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-sm"
                    OnClick="cmdDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" />
                <asp:Button ID="cmdBack" runat="server" OnClick="cmdBack_Click" Text="Back" CssClass="btn btn-default btn-sm" />
                <div class="pull-right">
                    <asp:Button ID="cmdMove" runat="server" Text="Move To:" OnClick="cmdMove_Click" CssClass="btn btn-default btn-sm" />
                    <asp:DropDownList ID="cboSections" runat="server" CssClass="input">
                    </asp:DropDownList>
                    <asp:Button ID="cmdGO" runat="server" Text="GO" OnClick="cmdGO_Click" CssClass="btn btn-default btn-sm" />
                </div>
            </div>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-condensed table-borderless"
                CellPadding="4" DataSourceID="ObjectDataSource3" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                OnRowDeleted="GridView1_RowDeleted" PageSize="50">
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
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" value='<%# Eval("Id") %>' name="chkChecked" />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle HorizontalAlign="Center" Width="18px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton5" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
                            CommandArgument='<%# Eval("Id") %>' />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Visible" SortExpression="Visible">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Visible")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active" SortExpression="Active">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                                ID="Image2" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Site Ctx" SortExpression="InSiteContext">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("InSiteContext")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" SortExpression="AutoTitle">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("AutoTitle")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
            TypeName="WCMS.WebSystem.WebParts.Central.WebPartAdminController">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="-1" Name="partId" QueryStringField="PartId"
                    Type="Int32" />
                <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
                    DefaultValue="-1" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
