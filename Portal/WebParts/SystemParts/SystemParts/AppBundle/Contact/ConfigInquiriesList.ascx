<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.ConfigInquiriesList"
    CodeBehind="ConfigInquiriesList.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<asp:HiddenField ID="hObjectId" runat="server" Value="-1" />
<asp:HiddenField ID="hRecordId" runat="server" Value="-1" />
<uc1:TabControl ID="TabControl1" runat="server" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewBasic" runat="server">
        <table>
            <tr>
                <td>Mode:
                </td>
                <td>
                    <asp:DropDownList ID="cboMode" runat="server">
                        <asp:ListItem Text="Auto Mode" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Public Mode" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Authenticated Mode" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>Default Recipient:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSourceContacts"
                        DataTextField="Name" DataValueField="ContactId">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSourceContacts" runat="server" SelectMethod="GetContacts"
                        TypeName="WCMS.WebSystem.WebParts.Contact.ConfigInquiriesList"></asp:ObjectDataSource>
                </td>
            </tr>
            <%--<tr>
                <td align="right" colspan="2">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" Height="28px" OnClick="cmdUpdate_Click"
                        Width="100px" />
                </td>
            </tr>--%>
        </table>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    </asp:View>
    <asp:View ID="viewAdvanced" runat="server">
        <div class="control-box">
            <div>
                <asp:Button ID="cmdDelete" CssClass="btn btn-default" OnClientClick="return confirm('Are you sure you want to delete the selected items?');" Text="Delete" runat="server" OnClick="cmdDelete_Click" />
                <asp:Button ID="cmdActive" CssClass="btn btn-default" Text="Mark As Read" runat="server"
                    OnClick="cmdActive_Click" />
                <asp:Button ID="cmdDeactivate" CssClass="btn btn-default" Text="Mark As Unread" runat="server"
                    OnClick="cmdDeactivate_Click" />
                <asp:Button ID="cmdDownload" CssClass="btn btn-default" Text="Download List" runat="server"
                    OnClick="cmdDownload_Click" />
                <div id="Div1" runat="server" visible="false">
                    &nbsp;Web Site:
                        <asp:DropDownList ID="cboSites" runat="server" OnSelectedIndexChanged="cboSites_SelectedIndexChanged"
                            DataValueField="Id" DataTextField="Name" AppendDataBoundItems="True" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value=""># All Inquiries #</asp:ListItem>
                        </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="table-responsive clear-left">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
                CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False"
                DataKeyNames="InquiryId" OnRowCommand="GridView1_RowCommand" PageSize="15" DataSourceID="ObjectDataSource1">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type='checkbox' value='<%# Eval("InquiryID")%>' name='chkChecked' />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View">
                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                AlternateText="Edit" CommandArgument='<%# Eval("InquiryId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Subject" HeaderStyle-HorizontalAlign="Left" HeaderText="Subject"
                        SortExpression="Subject" />
                    <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                        SortExpression="Email" />
                    <asp:BoundField DataField="SenderName" HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
                        SortExpression="SenderName" />
                    <%--<asp:BoundField DataField="InquiryType" HeaderStyle-HorizontalAlign="Left" HeaderText="Type"
                                SortExpression="InquiryType" />
                            <asp:BoundField DataField="SendTo" HeaderStyle-HorizontalAlign="Left" HeaderText="Sent To"
                                SortExpression="SendTo" />--%>
                    <asp:BoundField DataField="InqDateTime" HeaderStyle-HorizontalAlign="Left" HeaderText="Date/Time"
                        SortExpression="InqDateTime" />
                    <asp:TemplateField HeaderText="Status">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# WebHelper.SetMsgImage((int)Eval("IsActive")) %>'
                                ID="Image1" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Contact.ConfigInquiriesList" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value"
                        Type="Int32" />
                    <asp:ControlParameter ControlID="hRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:View>
</asp:MultiView>