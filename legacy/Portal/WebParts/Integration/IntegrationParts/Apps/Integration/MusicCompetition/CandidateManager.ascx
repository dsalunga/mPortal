<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CandidateManager.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.CandidateManagerView" %>
<input type="hidden" id="hCopyMode" runat="server" clientidmode="static" value="0" />
<%--<div class="alert fade in hidden" id="copy-alert">
    <strong>Next step:</strong> Now, to complete the copy process, select the destination Competition from the dropdown.
</div>--%>
<div class="control-box no-bottom-margin">
    <div>
        <asp:DropDownList ID="cboCompetition" CssClass="input" AppendDataBoundItems="true" AutoPostBack="true" runat="server" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboCompetition_SelectedIndexChanged">
            <asp:ListItem Text="- Select Competition -" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        <a runat="server" id="linkNew" class="btn btn-default">New Song</a>
        <button id="copy-button" type="button" class="btn btn-success hide" data-toggle="button" title="Select items, click this and select the target Competition">Copy Selected</button>
        <asp:Button ID="cmdDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete the selected items?');" Text="Delete" OnClick="cmdDelete_Click" ClientIDMode="Static" CssClass="btn btn-danger delete hide" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="input"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" CssClass="btn btn-default btn-sm" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
    PageSize="35">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" name="chkCheckedMain" class="chk-main" onclick="CheckAll(this, 'chkChecked');" />
            </HeaderTemplate>
            <ItemTemplate>
                <input type='checkbox' value='<%# Eval("Id") %>' class="chk-item" name='chkChecked' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="" Visible="false">
            <HeaderStyle HorizontalAlign="center" Width="20px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />--%>
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                    CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="Entry" HeaderText="Song" SortExpression="Entry"
            HeaderStyle-HorizontalAlign="Left" />--%>
        <asp:HyperLinkField DataNavigateUrlFields="EntryUrl" DataTextField="Entry" HeaderText="Song"
            SortExpression="Entry" HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:HyperLinkField>
        <asp:BoundField DataField="ComposerLyricist" HeaderText="Composer / Lyricist" SortExpression="ComposerLyricist" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Interpreter" HeaderText="Interpreter" SortExpression="Interpreter" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="SourceUrl" HeaderText="Audio URL" SortExpression="SourceUrl"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="PhotoFile" HeaderText="Photo File" SortExpression="PhotoFile"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Competition" HeaderText="Competition" SortExpression="Competition"
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
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.Apps.MusicCompetition.CandidateManagerView">
    <SelectParameters>
        <asp:ControlParameter ControlID="cboCompetition" DefaultValue="-2" Name="competitionId" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>
<script type="text/javascript">
    $(document).ready(function () {
        var isBatchChange = false;

        // Show/Hide Edit/Delete buttons
        var showHideButtons = function () {
            if (!isBatchChange) { // batch check/uncheck should not be running
                var showEdit = false, showDelete = false;
                var checkedItems = $('.chk-item:checked');
                var checkedCount = checkedItems.length;

                // Show/Hide Copy
                if (checkedCount > 0)
                    $('#copy-button').removeClass('hide');
                else
                    $('#copy-button').addClass('hide');

                if (checkedCount == 1) {
                    //showEdit = true;
                    showDelete = true;
                }
                else if (checkedCount > 1) {
                    //showEdit = false;
                    showDelete = true;
                }

                //$('.edit').css('display', showEdit ? 'inline-block' : 'none');

                if (showDelete)
                    $('.delete').removeClass('hide');
                else
                    $('.delete').addClass('hide');

            }
        }

        // Check/Uncheck all
        $('.chk-main').click(function () {
            isBatchChange = true;
            $('.chk-item').attr('checked', $(this).is(':checked'));
            isBatchChange = false;
            showHideButtons();
        });

        // Trigger for any checked/unchecked item
        $('.chk-item').change(function () {
            showHideButtons();
        });

        $("#copy-button").click(function () {
            var selected = !$(this).hasClass('active');
            $('#hCopyMode').val(selected ? '1' : '0');

            //if (selected) {
            //    $('#copy-alert').removeClass('hidden');
            //} else {
            //    $('#copy-alert').addClass('hidden');
            //}
            // some indicators, behaviors
        });
    });
</script>
