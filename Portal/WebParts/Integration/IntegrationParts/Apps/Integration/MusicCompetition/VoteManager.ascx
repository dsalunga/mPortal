<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VoteManager.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.VoteManagerView" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<uc1:TabControl ID="TabControl1" ThemeName="default" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
    runat="server" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewVotes" runat="server">
        <div class="control-box no-bottom-margin">
            <div>
                <asp:DropDownList ID="cboCompetition" CssClass="input" AppendDataBoundItems="true" AutoPostBack="true" runat="server" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboCompetition_SelectedIndexChanged">
                    <asp:ListItem Text="- Select Competition -" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="cmdDownload" runat="server" Text="Download" CssClass="btn btn-default btn-sm" OnClick="cmdDownload_Click" />
                <asp:Button
                    ID="cmdHardReset" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to delete all votes?');" Text="Hard Reset" OnClick="cmdHardReset_Click" />
                <div class="pull-right">
                    <asp:TextBox ID="txtSearch" ClientIDMode="Static" Columns="25" runat="server" CssClass="input"></asp:TextBox>
                    <asp:Button ID="cmdSearch" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                        ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />

                </div>
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
            CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
            GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
            PageSize="50">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <HeaderStyle HorizontalAlign="center" Width="40px" />
                    <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/images/Common/ico_edit.gif"
                                ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />--%>
                        <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/images/Common/ico_exit.gif"
                            CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField Visible="false" DataField="Code" HeaderText="Voting Code" SortExpression="Code"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DateVoted" HeaderText="Date Voted" SortExpression="DateVoted"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="UserName" HeaderText="Username" SortExpression="UserName"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CandidateSong" HeaderText="Song" SortExpression="CandidateSong"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField Visible="false" DataField="CandidateComposer" HeaderText="Composer"
                    SortExpression="CandidateComposer" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Confirmed" ItemStyle-HorizontalAlign="Center" HeaderText="Confirmed"
                    SortExpression="Confirmed" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="IPAddress" HeaderText="IP Address" SortExpression="IPAddress"
                    HeaderStyle-HorizontalAlign="Left" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="White" />
            <PagerSettings PageButtonCount="25" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectVotes"
            TypeName="WCMS.WebSystem.Apps.MusicCompetition.VoteManagerView">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                <asp:ControlParameter ControlID="cboCompetition" DefaultValue="-2" Name="competitionId" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
    <%--
    <asp:View ID="viewCodes" runat="server">
        <div class="control-box">
            <div>
                <asp:Button ID="cmdDownloadCodes" runat="server" Text="Download" OnClick="cmdDownloadCodes_Click" />&nbsp;
                <asp:Button ID="cmdGenerate" runat="server" Text="Generate:" OnClick="cmdGenerate_Click" /><asp:DropDownList
                    ID="cboCodes" runat="server">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>250</asp:ListItem>
                </asp:DropDownList>
                <div class="pull-right">
                    <asp:TextBox ID="txtSearchCodes" Columns="25" runat="server"></asp:TextBox>
                    <asp:Button ID="cmdSearchCodes" runat="server" Text="Search" OnClick="cmdSearchCodes_Click" />&nbsp;<asp:Button
                        ID="cmdResetCodes" runat="server" Text="Reset" OnClick="cmdResetCodes_Click"
                        Width="55px" />
                </div>
            </div>
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource2" ForeColor="#333333"
                        GridLines="None" Width="100%" OnRowCommand="GridView2_RowCommand" AllowPaging="True"
                        PageSize="15">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <HeaderStyle HorizontalAlign="center" Width="40px" />
                                <ItemStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Code" HeaderText="Voting Code" SortExpression="Code" HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerSettings PageButtonCount="25" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SelectCodes"
                        TypeName="WCMS.WebSystem.Apps.MusicCompetition.VoteManagerView">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtSearchCodes" DefaultValue="" Name="keyword" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </asp:View>
    --%>
</asp:MultiView>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>
<script type="text/javascript">
    $(document).ready(function () {
        $("#cmdHardReset").click(function () {
            return confirm('Are you sure you want to perform a Hard Reset? This will delete all votes and voting codes.');
        });

        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
