<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.ContactUsViewV2"
    CodeBehind="ContactUsV2.ascx.cs" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
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
                        <asp:TextBox ID="txtContactNo" CssClass="input" runat="server" Columns="30"></asp:TextBox>
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
                        <asp:TextBox ID="txtMessage" CssClass="span5 input" Columns="60" runat="server" TextMode="MultiLine" Rows="8"></asp:TextBox>
                    </td>
                </tr>
                <tr id="rowCode" runat="server">
                    <td style="vertical-align: top">Verification&nbsp;Code:<span style="color: red">*</span>
                    </td>
                    <td>
                        <recaptcha:RecaptchaControl
                            ID="recaptcha"
                            runat="server"
                            PublicKey="6LclMtgSAAAAAGpj_YrYlQOiQoALTYbzLK0RZbhx"
                            PrivateKey="6LclMtgSAAAAAOolOxcLEStMUljNt_tgMLOO_3O2" Theme="clean" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="font-style: italic">
                        <span style="color: Red">*</span>&nbsp;-&nbsp;required &nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="cmdSend" runat="server" CssClass="btn btn-primary Command" Text="Submit" OnClick="cmdSend_Click"></asp:Button>
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
