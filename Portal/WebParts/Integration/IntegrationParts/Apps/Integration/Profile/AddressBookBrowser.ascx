<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressBookBrowser.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.AddressBookBrowser" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<asp:HiddenField ID="hidBaseGroupId" runat="server" Value="-1" />
<asp:HiddenField ID="hUserProfileUrlFormat" runat="server" Value="" />
<div class="member-directory">
    <div class="min-bottom-margin">
        <div class="pull-left">
            <asp:DropDownList ID="cboGroups" Visible="false" AppendDataBoundItems="True" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="cboGroups_SelectedIndexChanged" CssClass="input">
                <asp:ListItem Selected="True" Text="* ALL MEMBERS *" Value="-1"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="cboCelebrants" Visible="false" AppendDataBoundItems="True"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCelebrants_SelectedIndexChanged" CssClass="input">
                <asp:ListItem Selected="True" Text="* ALL CELEBRANTS *" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="pull-right col-md-4 col-sm-5 col-np">
            <div class="input-group">
                <asp:TextBox ID="txtSearch" Columns="25" CssClass="input-sm form-control" runat="server"></asp:TextBox>
                <div class="input-group-btn">
                    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-default btn-sm" OnClick="cmdSearch_Click" />
                    <asp:Button ID="cmdReset" CssClass="btn btn-default btn-sm" runat="server" Text="Reset" OnClick="cmdReset_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="GridView1" CssClass="table table-borderless table-striped" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand"
            AllowPaging="True" PageSize="25" EmptyDataText="No records found." ShowHeader="false">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="70px" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <a href='<%# Eval("UserProfileUrl") %>' title="View details">
                            <img src='<%# Eval("PhotoPath") %>' alt="" width="64" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" ItemStyle-Font-Bold="true" HeaderText="User Name"
                    SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" Visible="false" />
                <%--<asp:BoundField DataField="FullName" ItemStyle-CssClass="member-name" HeaderText="Name"
                    SortExpression="FullName" HeaderStyle-HorizontalAlign="Left"
                    HtmlEncode="false" ItemStyle-Font-Bold="true" Visible="false" />--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <span style="font-size: 17px"><%# Eval("FullName") %></span>
                        <%# DataHelper.FormatString((String)Eval("StatusText"), "", "<br/><div style=\"margin-top:3px\"><span style=\"background-color:yellow;padding:3px;font-size:normal;\">{0}</span></div>") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Info" HeaderStyle-HorizontalAlign="Left" SortExpression="Email">
                    <ItemTemplate>
                        <%# DataHelper.FormatString((String)Eval("Email"), "", "<i class=\"icon-envelope\"></i>&nbsp;{0}<br />") %>
                        <%# DataHelper.FormatString((String)Eval("ContactNo"), "", "<i class=\"icon-signal\"></i>&nbsp;{0}<br />") %>
                        <%# DataHelper.FormatString((String)Eval("MembershipDate", "{0:MMMM-yyyy}"), "", "<i class=\"icon-calendar\"></i>&nbsp;{0}<br />") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="ContactNo" HeaderText="Contact&nbsp;No" SortExpression="ContactNo"
                    HeaderStyle-HorizontalAlign="Left" />--%>
                <%--<asp:BoundField Visible="false" DataField="ExternalIDNo" HeaderText="Group ID" SortExpression="ExternalIDNo"
                    HeaderStyle-HorizontalAlign="Left" />--%>
                <%--<asp:BoundField DataField="MembershipDate" HtmlEncode="true" DataFormatString="{0:MMMM-yyyy}"
                    HeaderText="Membership&nbsp;Date" SortExpression="MembershipDate" HeaderStyle-HorizontalAlign="Left" />--%>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle HorizontalAlign="Left" CssClass="table-pager" />
            <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerSettings PageButtonCount="9" FirstPageText="First" LastPageText="Last" Position="Bottom" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
            TypeName="WCMS.WebSystem.WebParts.Profile.AddressBookBrowser">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboGroups" DefaultValue="-1" Name="groupId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                <asp:ControlParameter ControlID="hidBaseGroupId" DefaultValue="-1" Name="baseGroupId"
                    PropertyName="Value" Type="Int32" />
                <asp:Parameter Name="viewerIsManager" DefaultValue="false" Type="Boolean" />
                <asp:Parameter Name="forcePrivate" DefaultValue="false" Type="Boolean" />
                <asp:ControlParameter ControlID="hUserProfileUrlFormat" DefaultValue="" Name="userProfileUrlFormat"
                    PropertyName="Value" Type="String" />
                <asp:ControlParameter ControlID="cboCelebrants" DefaultValue="-1" Name="celebrantsFilter"
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
