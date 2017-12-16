<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.ContactController"
    CodeBehind="ContactUs.ascx.cs" %>
<%@ Register Assembly="WCMS.Common" Namespace="WCMS.Common.Controls" TagPrefix="cc1" %>
<div class="Contact-Us">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="viewForm" runat="server">
            <table cellspacing="5" cellpadding="0" border="0">
                <tr id="rowName" runat="server">
                    <td style="width: 120px">Name:<span style="color: red">*</span>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name" ControlToValidate="txtName"
                            Display="None">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" CssClass="span3 input" runat="server" Columns="55"></asp:TextBox>
                    </td>
                </tr>
                <tr id="rowNameDisplay" runat="server" visible="false">
                    <td style="width: 120px">Name:
                    </td>
                    <td>
                        <strong><span id="lblNameDisplay" runat="server"></span></strong>
                    </td>
                </tr>
                <tr id="rowEmail" runat="server">
                    <td>Email Address:<span style="color: red">*</span>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email Address"
                            ControlToValidate="txtEmail" Display="None">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Valid e-mail format" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" CssClass="span3 input" runat="server" Columns="55"></asp:TextBox>
                    </td>
                </tr>
                <tr id="panelContactNo" runat="server">
                    <td>Contact No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="input" Columns="30"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="panelSendTo" visible="false">
                    <td>Send To:<span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboSendTo" CssClass="input" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">Subject:<span style="color: red">*</span>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Subject"
                            ControlToValidate="txtSubject" Display="None">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubject" CssClass="span5 input" runat="server" Columns="70"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style="vertical-align: top">Message:<span style="color: red">*</span>
                        <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ErrorMessage="Message"
                            ControlToValidate="txtMessage" Display="None">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMessage" CssClass="span5 input" runat="server" Columns="68" TextMode="MultiLine" Rows="8"></asp:TextBox>
                    </td>
                </tr>
                <tr id="rowCode" runat="server">
                    <td style="vertical-align: top">Verification&nbsp;Code:<span style="color: red">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Verification Code"
                            ControlToValidate="txtVerificationCode" Display="None">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <cc1:ImageSecurity ID="ImageSecurity1" runat="server" />&nbsp;
                        <asp:TextBox MaxLength="5" ID="txtVerificationCode" CssClass="input" runat="server" Columns="10"></asp:TextBox>
                    </td>
                </tr>

                <%--<tr>
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
                                Fax:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFax" runat="server" Columns="30"></asp:TextBox>
                            </td>
                        </tr>--%>
                <tr>
                    <td style="font-style: italic">
                        <span style="color: Red">*</span>&nbsp;-&nbsp;required &nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="cmdSend" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdSend_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="vsSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
                HeaderText="Please provide all required fields:"></asp:ValidationSummary>
            <br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </asp:View>
        <asp:View ID="viewThanks" runat="server">
            <span id="lblThanks" runat="server"></span>
        </asp:View>
    </asp:MultiView>
</div>
