<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestManagerRegister.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.RequestManagerRegister" %>
<asp:HiddenField ID="hUserProfileUrlFormat" runat="server" ClientIDMode="Static" Value="" />
<asp:HiddenField ID="hCentralProfileUrl" runat="server" ClientIDMode="Static" Value="" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewGrid" runat="server">
        <div class="integration-request-manager">
            <div class="min-bottom-margin">
                <asp:DropDownList DataTextField="CountryName" DataValueField="CountryCode" ID="cboCountries"
                    runat="server" AppendDataBoundItems="true" CssClass="input" AutoPostBack="true" OnSelectedIndexChanged="cboCountries_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                </asp:DropDownList>
                <div class="pull-right col-md-4 col-sm-5 col-np">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="input-sm form-control"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-default btn-sm" OnClick="cmdSearch_Click" />
                            <asp:Button ID="cmdReset" CssClass="btn btn-default btn-sm" runat="server" Text="Reset" OnClick="cmdReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="GridView1" CssClass="table table-borderless image-noscale" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
                    ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand"
                    AllowPaging="True" PageSize="25" EmptyDataText="There are no pending requests.">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                            <ItemTemplate>
                                <a href='<%# Eval("UserProfileUrl") %>' title="View details" target="_blank">
                                    <img src='<%# Eval("PhotoPath") %>' width="64" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="UserName" ItemStyle-Font-Bold="true" HeaderText="User Name"
                            SortExpression="UserName" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" />
                        </asp:BoundField>--%>
                        <asp:HyperLinkField DataTextField="UserName" DataNavigateUrlFields="CentralProfileUrl" HeaderText="Username" SortExpression="UserName" Target="_blank"
                            HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                            HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                            HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="E-mail" SortExpression="Email" />
                        <asp:BoundField DataField="CountryName" HeaderText="Country" SortExpression="CountryName"
                            HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DateCreated" HeaderText="Submitted" SortExpression="DateCreated"
                            HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Actions">
                            <HeaderStyle HorizontalAlign="center" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm" Text="Approve" CommandName="Approve"
                                    CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to APPROVE this request?');" Width="70px" Style="margin-bottom: 2px" />
                                <br />
                                <asp:Button ID="Button2" runat="server" Text="Reject" CssClass="btn btn-danger btn-sm" CommandName="Reject" Width="70px" CommandArgument='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F5F5E6" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PageButtonCount="25" />
                </asp:GridView>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Profile.RequestManagerRegister">
                <SelectParameters>
                    <asp:Parameter Name="group" DefaultValue="Members" Type="String" />
                    <asp:Parameter Name="approvers" DefaultValue="MM-Admin" Type="String" />
                    <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
                    <asp:ControlParameter ControlID="hUserProfileUrlFormat" DefaultValue="" Name="userProfileUrlFormat"
                        PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="hCentralProfileUrl" DefaultValue="" Name="centralProfileUrl"
                        PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="cboCountries" DefaultValue="-1" Name="countryCode" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:View>
    <asp:View ID="viewReject" runat="server">
        <div>
            <a id="linkUserProfileUrl" runat="server" href='#' title="View details" target="_blank">
                <img runat="server" id="imagePhotoPath" src='' width="300" /></a>
            <br />
            Requestor:&nbsp;<strong runat="server" id="lblRequestor"></strong>
        </div>
        <br />
        Rejection Reason:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Rejection Reason" ForeColor="Red" ControlToValidate="txtReason">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtReason" runat="server" CssClass="input" TextMode="MultiLine" Rows="6" Columns="50"></asp:TextBox>
        <br />
        <asp:Button ID="cmdRejectSubmit" runat="server" Text="Reject Request" CssClass="btn btn-primary" OnClick="cmdRejectSubmit_Click" />&nbsp;<a class="btn link-cancel" href="./">Cancel</a>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:" ShowMessageBox="True" ShowSummary="False" />
    </asp:View>
</asp:MultiView>
