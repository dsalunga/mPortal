<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.RegistrationManager" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdDownload" runat="server" Text="Download (CSV)" CssClass="btn btn-default"
            OnClick="cmdDownload_Click" />
        <asp:Button ID="cmdDownloadXml" runat="server" Text="Download (XML)" CssClass="btn btn-default"
            OnClick="cmdDownloadXml_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-default" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="center" Width="18px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');"
                        ToolTip="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Country" HeaderText="COUNTRY" SortExpression="Country"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Locale" HeaderText="LOCAL" SortExpression="Locale" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExternalId" HeaderText="CHURCH ID" SortExpression="ExternalId"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Name" HeaderText="NAME" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Age" HeaderText="AGE" SortExpression="Age" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Gender" HeaderText="GENDER" SortExpression="Gender" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Designation" HeaderText="DESIGNATION" SortExpression="Designation"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ArrivalDate" HeaderText="ARRIVAL" SortExpression="ArrivalDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Airline" HeaderText="AIRLINE" SortExpression="Airline"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="FlightNo" HeaderText="FLIGHT NO" SortExpression="FlightNo"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="DepartureDate" HeaderText="DEPARTURE" SortExpression="DepartureDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Address" HeaderText="ADDRESS" SortExpression="Address"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PlaceType" HeaderText="PLACE TYPE" SortExpression="PlaceType"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="EntryDate" HeaderText="REG. DATE" SortExpression="EntryDate"
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
        TypeName="WCMS.WebSystem.WebParts.Profile.RegistrationManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>
