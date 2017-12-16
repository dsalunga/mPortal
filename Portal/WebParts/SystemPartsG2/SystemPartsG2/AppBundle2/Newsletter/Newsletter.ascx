<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Newsletter.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Newsletter.NewsletterView" %>
<div class="content_wrap">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewSubscribe" runat="server">
            <div id="splash_section" class="splash_section textshadow1">
                <p>
                    Get a chance to win 1 of the 3 Beatbox Mini-Speakers on the Elimination Night! (Terms & Conditions Applies).<br />
                    <br />
                    <img id="subscribe_button" src="/Content/Themes/ASOP/v2/division/images/subscribe.png" alt="subscribe now" />
                </p>
            </div>
            <div id="subscribe_section" class="subscribe">
                <form class="form_class" action="#" method="post">
                    <asp:TextBox runat="server" ID="txtName" CssClass="input_text rounded_corner" placeholder="Enter your Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name" ForeColor="Red" ControlToValidate="txtName">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="input_text rounded_corner" placeholder="Enter your Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email" ForeColor="Red" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    <div>
                        Gender:&nbsp;<asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">Male</asp:ListItem>
                            <asp:ListItem Value="0">Female</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button runat="server" style="margin-left: 145px" ID="cmdSubmit" CssClass="submit_class" Text="Submit" OnClick="cmdSubmit_Click" />
                    </div>
                    <asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Gender" ForeColor="Red" ControlToValidate="rblGender">*</asp:RequiredFieldValidator>
                </form>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:" ShowMessageBox="True" ShowSummary="False" />
            <script type="text/javascript">
                $(document).ready(function () {
                    $("#subscribe_button").click(function () {
                        $('#subscribe_section').slideToggle('fast');
                        $('#splash_section').slideToggle('fast');
                    });
                });
            </script>
        </asp:View>
        <asp:View ID="viewAlreadySubscribed" runat="server">
            <div class="splash_section textshadow1">
                <p>
                    You are already subscribed to this newsletter.
                </p>
                <br />
                <br />
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    setTimeout(function () { location.href = location.href; }, 10000);
                });
            </script>
        </asp:View>
        <asp:View ID="viewSuccess" runat="server">
            <div class="splash_section textshadow1">
                <p>
                    Thank you for subscribing. You will be receiving ASOP updates soon.
                </p>
                <br />
                <br />
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    setTimeout(function () { location.href = location.href; }, 10000);
                });
            </script>
        </asp:View>
    </asp:MultiView>
</div>
