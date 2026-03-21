<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sportsfest.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.SportsfestController" %>
<img src="/Assets/Uploads/Image/Sites/integration-portal/Integration_Sportsfest2011v2.jpg" width="915"
    height="167" alt="MCGIsg Sportsfest 2011 Banner" style="display:none;" />
<div style="border:none; margin: 0px; padding: 0px; width: 915px; height: 167px; background-color: black;"></div>
<br />
<br />
<h2 runat="server" id="lblTitle">
</h2>
<div id="panelRegistration" runat="server">
    <div id="panelWhen" runat="server" visible="false">
        <div style="float: left; width: 100px">
            When:</div>
        <div>
            <strong runat="server" id="lblWhen"></strong>
        </div>
    </div>
    <div id="panelWhere" runat="server" visible="false">
        <div style="float: left; width: 100px">
            Where:</div>
        <div>
            <strong runat="server" id="lblWhere"></strong>&nbsp;<span runat="server" id="lblWhereExtra"
                visible="false"></span></div>
    </div>
    <br />
    <br />
    <h3>
        Player Information</h3>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblName" AssociatedControlID="txtName" runat="server">
            Name:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name"
                ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
        <asp:TextBox Columns="50" ID="txtName" runat="server"></asp:TextBox>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblGroupColor" AssociatedControlID="cboGroupColor"
            runat="server">Group Color:<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                runat="server" ErrorMessage="Group Color" ControlToValidate="cboGroupColor" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
        <asp:DropDownList ID="cboGroupColor" runat="server">
            <asp:ListItem Text="** SELECT COLOR **" Value=""></asp:ListItem>
            <asp:ListItem Text="* NO GROUP *" Value="NO GROUP"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblShirtSize" AssociatedControlID="cboShirtSize" runat="server">
            T-Shirt Size:<asp:RequiredFieldValidator ID="rfvShirtSize" runat="server" ErrorMessage="T-Shirt Size"
                ControlToValidate="cboShirtSize" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
        <asp:DropDownList ID="cboShirtSize" runat="server">
            <asp:ListItem Text="** SELECT **" Value="" Selected="True"></asp:ListItem>
            <asp:ListItem Text="S" Value="S"></asp:ListItem>
            <asp:ListItem Text="M" Value="M"></asp:ListItem>
            <asp:ListItem Text="L" Value="L"></asp:ListItem>
            <asp:ListItem Text="XL" Value="XL"></asp:ListItem>
            <asp:ListItem Text="XXL" Value="XXL"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblAge" AssociatedControlID="cboAge" runat="server">
            Age:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Age"
                ControlToValidate="cboAge" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
        <asp:DropDownList ID="cboAge" runat="server">
            <asp:ListItem Text="** SELECT **" Value=""></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblMobile" AssociatedControlID="txtMobile" runat="server">Mobile:</asp:Label>
        <asp:TextBox Columns="14" ID="txtMobile" runat="server"></asp:TextBox>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblCountry" AssociatedControlID="cboCountry"
            runat="server">Country</asp:Label>
        <asp:DropDownList ID="cboCountry" runat="server" DataSourceID="ObjectDataSourceCountries"
            DataTextField="CountryName" DataValueField="CountryCode">
        </asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSourceCountries" runat="server" SelectMethod="GetCountries"
            TypeName="WCMS.WebSystem.WebParts.Profile.SportsfestController"></asp:ObjectDataSource>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblLocale" AssociatedControlID="txtLocale" runat="server">Locale:</asp:Label>
        <asp:TextBox Columns="60" ID="txtLocale" runat="server"></asp:TextBox>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="lblSuggestion" AssociatedControlID="txtSuggestion"
            runat="server">Suggestion:</asp:Label>
        <asp:TextBox Columns="50" ID="txtSuggestion" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
    </div>
    <br />
    <h3>
        Sports Option 1 (choose only one)</h3>
    <asp:RadioButtonList DataTextField="Name" DataValueField="Id" ID="rblSet1" runat="server">
        <asp:ListItem>Basketball 5-on-5</asp:ListItem>
        <asp:ListItem>Volleyball</asp:ListItem>
        <asp:ListItem>Chess</asp:ListItem>
        <%--<asp:ListItem>Game of the Generals</asp:ListItem>
        <asp:ListItem>Scrabble</asp:ListItem>--%>
        <asp:ListItem><em>I'm not joining any game in option 1</em></asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <br />
    <h3>
        Sports Option 2 (choose only one)</h3>
    <asp:RadioButtonList DataTextField="Name" DataValueField="Id" ID="rblSet2" runat="server">
        <asp:ListItem>Badminton (Doubles)</asp:ListItem>
        <asp:ListItem>Table Tennis</asp:ListItem>
        <asp:ListItem><em>I'm not joining any game in option 2</em></asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <br />
    <br />
    <br />
    <h3>
        Please tick</h3>
    <asp:CheckBox ID="chkAgree1" Text="I am aware that these sport activities may involve physical contact that may cause injury and accident."
        runat="server" />
    <br />
    <asp:CheckBox ID="chkAgree2" Text="I must wear appropriate sports attire on the type of sport I would like to join."
        runat="server" />
    <div style="padding-top: 5px; padding-bottom: 5px">
        <em>* You can register only once. Kindly verify your information and choice of game/s
            before clicking Register.</em>
    </div>
    <br />
    <div>
        <asp:Button CssClass="Command" Width="85px" ID="cmdSubmit" runat="server" Text="Register"
            OnClick="cmdSubmit_Click" />
    </div>
    <br />
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
        ShowMessageBox="True" ShowSummary="False" />
</div>
<div id="panelDone" runat="server" visible="false">
    Thank you for registering. You have chosen the sport/s:&nbsp;<strong><span id="lblSports"
        runat="server"></span></strong>. Goodbye!
    <br />
    <br />
    <a href="" runat="server" id="linkReturn"></a>
</div>
