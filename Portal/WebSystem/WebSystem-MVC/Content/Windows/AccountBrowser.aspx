<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountBrowser.aspx.cs"
    Inherits="WCMS.WebSystem.Windows.AccountBrowser" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Account Browser</title>
    <script src="<%= WebUtil.Version("~/content/assets/scripts/common.min.js") %>" type="text/javascript"></script>
    <script src="<%= WebUtil.Version("~/content/assets/scripts/wcms.core.min.js") %>" type="text/javascript"></script>
    <script src="<%= WebUtil.Version("~/content/assets/scripts/jquery.min.js") %>" type="text/javascript"></script>

    <link href="<%= WebUtil.Version("~/content/plugins/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= WebUtil.Version("~/content/assets/styles/websystem.min.css") %>" rel="stylesheet" type="text/css" media="all" />
    <link href="<%= WebUtil.Version("~/content/assets/styles/websystem.admin.min.css") %>" rel="stylesheet" type="text/css" media="all" />
</head>
<body>
    <h3 style="margin: 5px 0 5px 5px;" class="page-header1">
        Account Browser
    </h3>
    <form id="form1" runat="server">
        <input type="hidden" id="hidId" runat="server" value="-1" />
        <asp:HiddenField runat="server" ID="hBaseGroup" Value="" />
        <input type="hidden" id="hAppend" clientidmode="Static" runat="server" value="0" />
        <input type="hidden" id="hAccountDelimiter" clientidmode="Static" runat="server"
            value=";" />
        <div class="control-box">
            <div>
                <span runat="server" id="panelTypeSelector">
                    Type:&nbsp;<asp:DropDownList ID="cboType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboType_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="# All types #" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
                <div class="pull-right">
                    <asp:TextBox ID="txtSearch" Columns="25" runat="server"></asp:TextBox>
                    <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" CssClass="btn btn-default" />&nbsp;<asp:Button
                        ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="grdObjects" CssClass="table table-borderless" runat="server" CellPadding="4" ForeColor="#333333"
                GridLines="None" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdObjects_RowCommand"
                AllowPaging="True" AllowSorting="True" DataSourceID="ObjectDataSource1" PageSize="45" EmptyDataText="No records found.">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerSettings PageButtonCount="25" />
                <PagerStyle CssClass="table-pager" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type='checkbox' value='<%# Eval("UniqueName") %>' name='chkChecked' />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle HorizontalAlign="Center" Width="15px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                AlternateText="Edit" CommandArgument='<%# Eval("UniqueName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="E-mail" SortExpression="Email" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ObjectType" HeaderText="Type" SortExpression="ObjectType"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.Windows.AccountBrowser">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboType" DefaultValue="-1" Name="objectId" PropertyName="SelectedValue"
                        Type="Int32" />
                    <asp:ControlParameter ControlID="hBaseGroup" DefaultValue="" Name="baseGroup" PropertyName="Value" />
                    <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div class="control-box" runat="server" id="rowButtons">
            <div>
                <asp:Button ID="cmdOpen" CssClass="btn btn-default" runat="server" Text="Insert"
                    OnClick="cmdOpen_Click" />
                <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Close" OnClientClick="window.close(); return false;" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));

            var hidId = WCMS.Dom.Get("hidId");
            if (hidId != null && hidId.value != "-1") {
                var hidAppend = WCMS.Dom.Get("hAppend");
                var hidAccountDelimiter = WCMS.Dom.Get("hAccountDelimiter");

                var delimiter = hidAccountDelimiter != null ? hidAccountDelimiter.value : ";";
                var param = window.opener.returnValue.param;

                if (param.value.length > 0 && hidAppend != null && hidAppend.value == "1") {
                    // Appends result, instead of replace
                    if (param.value.length > 0 && param.value.substr(param.value - 1, 1) == delimiter) {
                        param.value += hidId.value;
                    }
                    else {
                        // Existing value has no delimiter
                        param.value += delimiter + hidId.value;
                    }
                }
                else {
                    // Replaces the existing value with this result
                    param.value = hidId.value;
                }

                var returnClick = window.opener.returnValue.returnClick;
                if (returnClick != null) {
                    returnClick.click();
                }
                window.close();
            }
        });
    </script>
</body>
</html>
