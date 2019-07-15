<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoardV2.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicCompetition.MessageBoardV2" %>
<asp:HiddenField runat="server" ID="hObjectId" Value="-1" />
<asp:HiddenField runat="server" ID="hRecordId" Value="-1" />

<div class="form_class">
    <p>
        <label>Name<asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name" ForeColor="Red" ControlToValidate="txtName">*</asp:RequiredFieldValidator></label><br />
        <asp:TextBox runat="server" ID="txtName" class="input_text rounded_corner"></asp:TextBox>
    </p>
    <p>
        <label>Email Address<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email" ForeColor="Red" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEmail" runat="server" ForeColor="Red" Text="*" ErrorMessage="Valid email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator></label><br />
        <asp:TextBox runat="server" ID="txtEmail" class="input_text rounded_corner"></asp:TextBox>
    </p>
    <p>
        <label>Message<asp:RequiredFieldValidator ID="rfvMessage" runat="server" ErrorMessage="Message" ForeColor="Red" ControlToValidate="txtComment">*</asp:RequiredFieldValidator></label><br />
        <asp:TextBox runat="server" ID="txtComment" class="input_textarea" TextMode="MultiLine"></asp:TextBox>
    </p>
    <button runat="server" id="cmdPost" class="submit_class" onserverclick="cmdPost_ServerClick">Submit</button>
</div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" HeaderText="The following are required:" ForeColor="Red" ShowSummary="False" />
<br />
<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSourceComments">
    <ItemTemplate>
        <div class="comments">
            <p class="name_class">
                <%# Eval("UserName") %>
            </p>
            <p class="message_class">
                <%# Eval("Content") %>
                <br />
                <%# Eval("DateCreated") %>
            </p>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:ObjectDataSource ID="ObjectDataSourceComments" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.Apps.Integration.MusicCompetition.MessageBoardV2">
    <SelectParameters>
        <asp:ControlParameter ControlID="hObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value" Type="Int32" />
        <asp:ControlParameter ControlID="hRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
