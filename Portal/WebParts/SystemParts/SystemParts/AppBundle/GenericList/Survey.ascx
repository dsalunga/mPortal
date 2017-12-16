<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.Survey"
    CodeBehind="Survey.ascx.cs" %>

<script language="javascript" type="text/javascript">
    function SurveyBack() {
        WCMS.Dom.Get("__SURVEY_ACTION").value = "BACK";
    }

    function SurveyNext() {
        WCMS.Dom.Get("__SURVEY_ACTION").value = "NEXT";
    }

    function SurveyRestart() {
        WCMS.Dom.Get("__SURVEY_ACTION").value = "RESTART";
    }
</script>

<input id="__SURVEY_ACTION" type="hidden" name="__SURVEY_ACTION">
<table cellspacing="1" cellpadding="0" border="0" align="center">
    <tr>
        <td align="center">
            <strong>
                <asp:Literal ID="lTitle" runat="server"></asp:Literal></strong><br />
            <asp:Literal ID="lPage" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table>
                <asp:Literal ID="lTemplate" Text="<tr><td valign=top colspan=3>{$Q$}</td></tr><tr><td valign=top colspan=3>{$C$}</td></tr><tr><td height=5></td></tr>"
                    Visible="False" runat="server">
                </asp:Literal>
                <asp:Literal ID="lTemplateH" Text="<tr><td valign=top>{$Q$}</td><td>&nbsp;</td><td valign=top>{$C$}</td></tr><tr><td height=5></td></tr>"
                    Visible="False" runat="server"></asp:Literal>
                <asp:Literal ID="lQuestions" runat="server"></asp:Literal>
                <!--
			        <div>
			        <table>
			        <tr><td><asp:label id="lblPosition" runat="server"></asp:label></td></tr>
			        <tr><td><strong><asp:label id="lblQuestion" runat="server"></asp:label></strong></td></tr>
			        <tr><td><asp:literal id="lChoices" runat="server"></asp:literal></td></tr>
			        </table>
			        </div>
			    -->
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="cmdNext" runat="server" Width="77px" CssClass="BUTTON" Text="Submit" />&nbsp;
            <asp:Button ID="cmdRestart" runat="server" Width="76px" CssClass="BUTTON" Text="Restart"
                Visible="False" />
        </td>
    </tr>
    <tr>
        <td>
            <font color="red">*</font><em> - Required</em>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
