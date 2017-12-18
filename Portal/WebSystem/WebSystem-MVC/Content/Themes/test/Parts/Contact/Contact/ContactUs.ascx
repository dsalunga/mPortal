<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.ContactController"
    CodeBehind="ContactUs.ascx.cs" %>
<table cellspacing="10" cellpadding="0" width="100%" border="0">
    <tr>
        <td valign="top">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="viewForm" runat="server">
                    <table cellspacing="5" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 151px">
                                Name:<font color="red">*</font>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name" ControlToValidate="txtName"
                                    Display="None">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Columns="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Subject:<font color="red">*</font>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Subject"
                                    ControlToValidate="txtSubject" Display="None">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server" Columns="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 151px">
                                Message:<font color="red">*</font>
                                <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ErrorMessage="Message"
                                    ControlToValidate="txtMessage" Display="None">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMessage" runat="server" Columns="54" TextMode="MultiLine" Rows="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Email Address:<font color="red">*</font>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email Address"
                                    ControlToValidate="txtEmail" Display="None">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Valid e-mail format" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Columns="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Type of Inquiry:<font color="red">*</font>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboInquiryType" runat="server">
                                    <asp:ListItem Value="Feedback" Selected="True">Feedback</asp:ListItem>
                                    <asp:ListItem Value="Request">Request</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Country:<font color="red">*</font>
                                <asp:RequiredFieldValidator ID="vCountry" runat="server" ErrorMessage="Country" ControlToValidate="cboCountry"
                                    Display="None">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboCountry" runat="server" DataSourceID="ObjectDataSourceCountries"
                                    DataTextField="CountryName" DataValueField="CountryCode">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSourceCountries" runat="server" SelectMethod="GetCountries"
                                    TypeName="WCMS.WebSystem.WebParts.Contact.ContactController"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Send To:<font color="red">*</font>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboSendTo" runat="server" DataSourceID="ObjectDataSourceContacts"
                                    DataTextField="Name" DataValueField="ContactId">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSourceContacts" runat="server" SelectMethod="GetContacts"
                                    TypeName="WCMS.WebSystem.WebParts.Contact.ContactController"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Address:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddressLine1" runat="server" Columns="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Address 2:&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddressLine2" runat="server" Columns="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                City:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Columns="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                State (US Only):
                            </td>
                            <td>
                                <asp:DropDownList ID="cboState" runat="server" DataSourceID="ObjectDataSourceUSStates"
                                    DataTextField="StateName" DataValueField="StateCode">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSourceUSStates" runat="server" SelectMethod="GetUSStates"
                                    TypeName="WCMS.WebSystem.WebParts.Contact.ContactController"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Zip:
                            </td>
                            <td>
                                <asp:TextBox ID="txtZipCode" runat="server" Columns="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Phone:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhone" runat="server" Columns="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px">
                                Fax:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFax" runat="server" Columns="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px; font-style: italic">
                                <span style="color: Red">*</span>&nbsp;-&nbsp;required &nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="cmdSend" runat="server" Width="68px" Text="Send" OnClick="cmdSend_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="vsSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
                        HeaderText="Please provide all required fields:"></asp:ValidationSummary>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:View>
                <asp:View ID="viewThanks" runat="server">
                    <span id="lblThanks" runat="server"></span>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
</table>
