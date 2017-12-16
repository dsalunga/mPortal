<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.SurveyFinish"
    CodeBehind="SurveyFinish.ascx.cs" %>
<table width="100%" height="100%" border="0" cellspacing="1" cellpadding="0">
    <tr>
        <td valign="middle">
            <table width="100%" border="0" cellspacing="10" cellpadding="0">
                <tr>
                    <td align="center">
                        <strong>
                            <asp:Literal ID="lTitle" runat="server"></asp:Literal></strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <!--
				<tr>
					<td align="center"><a class="more" href=".">Home</a></td>
				</tr>
				-->
            </table>
        </td>
    </tr>
</table>
