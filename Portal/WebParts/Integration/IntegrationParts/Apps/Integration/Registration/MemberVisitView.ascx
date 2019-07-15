<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberVisitView.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.MemberVisitView" %>
<div class="wp-ODKVisit wp-ODKVisit-List no-bottom-margin">
    <asp:HiddenField ID="hTagFilter" runat="server" Value="" />
    <div class="control-box" runat="server" id="rowControls">
        <div>
            <div class="btn-group">
                <a id="linkNew" class="btn btn-default btn-sm" href="#" runat="server">New Record</a>
                <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                    <span class="caret"></span>
                    <span class="sr-only">Toggle Dropdown</span>
                </button>
                <ul class="dropdown-menu" role="menu">
                    <li>
                        <asp:LinkButton ID="cmdDownload" runat="server" Text="Download"
                            OnClick="cmdDownload_Click" /></li>
                </ul>
            </div>
            <asp:DropDownList ID="cboGroup" CssClass="input" DataTextField="Name" DataValueField="Id" runat="server"
                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboGroup_SelectedIndexChanged">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
            <div class="pull-right col-md-4 col-sm-5 col-np">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" CssClass="input-sm form-control" ClientIDMode="Static" Columns="20" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:Button ID="cmdSearch" CssClass="btn btn-default btn-sm" runat="server" Text="Search" ClientIDMode="Static" OnClick="cmdSearch_Click" />
                        <asp:Button ID="cmdReset" CssClass="btn btn-default btn-sm" runat="server" ClientIDMode="Static"
                            Text="Reset" OnClick="cmdReset_Click" Width="65px" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gridView" runat="server" CssClass="table table-borderless table-striped" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Id" DataSourceID="ObjectDataSourceVisits"
            GridLines="None" Width="100%" EmptyDataText="No records found."
            PageSize="25" OnRowCommand="gridView_RowCommand">
            <Columns>
                <%--<asp:TemplateField HeaderText="Actions" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                            ID="Imagebutton2" ToolTip="Edit" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="70px" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <img src='<%# Eval("PhotoUrl") %>' alt="" width="64" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="MemberName" HtmlEncode="false" HeaderText="Name" SortExpression="MemberName">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>--%>
                <asp:HyperLinkField DataNavigateUrlFields="EditUrl"
                    DataTextField="MemberName" HeaderText="Name" SortExpression="MemberName"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GroupName" HeaderText="Group" SortExpression="GroupName">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DateVisited" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Date Visited" SortExpression="DateVisited" HtmlEncode="true" DataFormatString="{0:yyyy-MMM-dd}" />
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton
                            ID="cmdDelete" Visible='<%# !(bool)Eval("ReadOnly") %>' CommandName="Custom_Delete"
                            runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" CommandArgument='<%# Eval("Id") %>'
                            ToolTip="Delete this item" OnClientClick="return confirm('Are you you want to delete this item?');" />
                        <input type="image" title="Print Preview" src="/Content/Assets/Images/print.png" onclick="ShowODKPrintPreview(<%# Eval("Id") %>); return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
            <HeaderStyle HorizontalAlign="Left" />
            <PagerSettings PageButtonCount="25" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceVisits" SelectMethod="Select" runat="server"
            TypeName="WCMS.WebSystem.Apps.Integration.ODKVisitView">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="-1" Name="groupId" QueryStringField="GroupId"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="readOnly" Type="Int32" />
                <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                <asp:QueryStringParameter DefaultValue="-1" Name="userId" QueryStringField="UserId"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hTagFilter" DefaultValue="" Name="tagFilter" PropertyName="Value" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <br />
    <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

</div>
<script type="text/javascript">
    function ShowODKPrintPreview(visitId) {
        WCMS.Dom.Open("/Content/Parts/Integration/ODKPrintPreview.ashx?Id=" + visitId + "&PageId=" + $("#__hidPageId").attr("value"));
    }

    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
