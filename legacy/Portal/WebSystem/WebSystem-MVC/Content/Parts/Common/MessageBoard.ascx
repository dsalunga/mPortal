<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoard.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Common.MessageBoard" %>
<asp:HiddenField runat="server" ID="hObjectId" Value="-1" />
<asp:HiddenField runat="server" ID="hRecordId" Value="-1" />
<style type="text/css">
    h3 {
        background-color: #eee;
        padding: 10px;
        -webkit-border-radius: 8px;
        border-radius: 8px;
        color: #926b00;
    }

    .required {
        color: red;
    }

    .floatleft {
        float: left;
    }

    .comment_name {
        -webkit-border-radius: 5px;
        border-radius: 5px;
        background-color: #333;
        width: 15%;
        padding: 2%;
        float: left;
        color: white;
    }

    .comment_text div {
        font-size: small;
        margin-top: 4px;
    }

    .comment_text {
        -webkit-border-radius: 5px;
        border-radius: 5px;
        background-color: #ddd;
        color: #926b00;
        width: 77%;
        padding: 2%;
        float: left;
        margin-bottom: 10px;
    }

    .commentbox:nth-child(even) .comment_name {
        float: right;
    }

    .commentbox:nth-child(even) .comment_text {
        float: right;
    }

    .commentbox {
        margin: 7px 0;
        height: 100px;
        clear: both;
    }
</style>

<%--<h1>TALK about ASOP</h1>--%>

<%--<h3 id="Comment">Comments</h3>--%>

<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSourceComments">
    <ItemTemplate>
        <div class="commentbox">
            <div class="comment_name">
                <%# Eval("UserName") %>
            </div>
            <div class="comment_text">
                <%# Eval("Content") %><div><%# Eval("DateCreated") %></div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:ObjectDataSource ID="ObjectDataSourceComments" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Common.MessageBoard">
    <SelectParameters>
        <asp:ControlParameter ControlID="hObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value" Type="Int32" />
        <asp:ControlParameter ControlID="hRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<h3 id="PostComment">Post a Message</h3>
<br />
<div>
    <span>Name<span class="required">*<asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name" ForeColor="Red" ControlToValidate="txtName">*</asp:RequiredFieldValidator></span>
    </span>
    <br />
    <asp:TextBox runat="server" ID="txtName" CssClass="span5"></asp:TextBox><br />
    <span>Email<span class="required">*<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email" ForeColor="Red" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEmail" runat="server" ForeColor="Red" Text="*" ErrorMessage="Valid email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator></span>
    </span>
    <br />
    <asp:TextBox runat="server" ID="txtEmail" CssClass="span5"></asp:TextBox><br />
    <span>Message<span class="required">*<asp:RequiredFieldValidator ID="rfvMessage" runat="server" ErrorMessage="Message" ForeColor="Red" ControlToValidate="txtComment">*</asp:RequiredFieldValidator></span></span><br />
    <asp:TextBox runat="server" ID="txtComment" CssClass="span5" Rows="6" TextMode="MultiLine"></asp:TextBox><br />
    <button runat="server" id="cmdPost" onserverclick="cmdPost_ServerClick" class="btn btn-primary">Submit</button>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" HeaderText="The following are required:" ForeColor="Red" ShowSummary="False" />
<br />
<br />
