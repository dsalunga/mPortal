<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttendanceRequests.ascx.cs" Inherits="WCMS.WebSystem.Integration.LessonReviewer.AttendanceRequests" %>
<%--<asp:HiddenField ID="hidBaseGroupId" runat="server" Value="-1" />--%>
<asp:HiddenField ID="hPageId" runat="server" Value="-1" />
<div class="control-box">
    <div>
        <div id="dropDownPanel" runat="server" class="pull-left no-bottom-margin">
            <asp:DropDownList ToolTip="Status Filter" ID="cboStatus" AppendDataBoundItems="True" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="cboStatus_SelectedIndexChanged" CssClass="input">
                <asp:ListItem Text="* ALL STATUS *" Value="-1"></asp:ListItem>
                <asp:ListItem Selected="True" Text="PENDING" Value="0"></asp:ListItem>
                <asp:ListItem Text="APPROVED" Value="1"></asp:ListItem>
                <asp:ListItem Text="REJECTED" Value="2"></asp:ListItem>
                <asp:ListItem Text="IN PROGRESS" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" CssClass="btn btn-default btn-sm" OnClick="cmdRefresh_Click" />
            <%-- Member:&nbsp;
                <asp:DropDownList ID="cboGroups" AppendDataBoundItems="True" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True" Text="* All Members *" Value="-1"></asp:ListItem>
                </asp:DropDownList>--%>
        </div>
        <div class="pull-right col-md-4 col-sm-5 col-np">
            <div class="input-group">
                <asp:TextBox ID="txtSearch" Columns="25" runat="server" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click"
                        Width="55px" ClientIDMode="Static" CssClass="btn btn-default btn-sm" />&nbsp;<asp:Button ID="cmdReset" runat="server"
                            Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
                </span>
            </div>
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" CssClass="table table-borderless table-striped" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="0" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
        GridLines="None" Width="100%"
        AllowPaging="True" PageSize="25" EmptyDataText="No more records to display.">
        <%--<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="64px">
                <ItemTemplate>
                    <a href='<%# Eval("UserProfileUrl") %>' target="_blank" title="View Member details">
                        <img src='<%# Eval("PhotoPath") %>' alt="" width="64" /></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ServiceType" HeaderText="Service" SortExpression="ServiceType"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ServiceStartDate" HtmlEncode="true" DataFormatString="{0:dd-MMM-yyyy h:mm tt}"
                HeaderText="Service Start" SortExpression="ServiceStartDate" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="DateStarted" HtmlEncode="true" DataFormatString="{0:dd-MMM-yyyy h:mm tt}"
                HeaderText="Time Attended" SortExpression="DateStarted" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Duration" HtmlEncode="true" DataFormatString="{0:hh\:mm}"
                HeaderText="Duration" SortExpression="Duration" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px">
                <ItemTemplate>
                    <a href='<%# BuildUrl(Eval("Id")) %>' title="Click for more options">
                        <img src='<%# WCMS.WebSystem.Apps.Integration.MemberHelper.SetMakeUpStatusImage(Eval("Status")) %>'
                            alt="" width="48" /></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <%--<RowStyle BackColor="#F5F5E6" />--%>
        <%--<EditRowStyle BackColor="#2461BF" />--%>
        <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
        <PagerStyle HorizontalAlign="Left" CssClass="table-pager" />
        <%--<HeaderStyle BackColor="#000" Font-Bold="True" HorizontalAlign="Center" />--%>
        <%--<AlternatingRowStyle BackColor="White" />--%>
        <PagerSettings PageButtonCount="15" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.Integration.LessonReviewer.AttendanceRequests">
        <SelectParameters>
            <%--<asp:ControlParameter ControlID="cboGroups" DefaultValue="-1" Name="memberId" PropertyName="SelectedValue"
                        Type="Int32" />--%>
            <asp:ControlParameter ControlID="cboStatus" DefaultValue="0" Name="status" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
            <%--<asp:ControlParameter ControlID="hidBaseGroupId" DefaultValue="-1" Name="baseGroupId"
                        PropertyName="Value" Type="Int32" />--%>
            <asp:ControlParameter ControlID="hPageId" DefaultValue="-1" Name="pageId"
                        PropertyName="Value" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="-1" Name="userId" QueryStringField="UserId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
