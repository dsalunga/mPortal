<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Media.FullContent" Codebehind="FullContent.ascx.cs" %>
<table border="0" cellpadding="2" cellspacing="0" width="100%">
	<tr>
		<td><asp:Literal ID="lContent" Runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td></td>
	</tr>
	<tr>
		<td height="25" align="center" class="contentbl"><!--<a href="#" class="corpl">Back to 
				Bionlink Products page</a>&nbsp; | &nbsp;--><a href='<% =CreateLink() %>' class="corpl" title="Back to Product Catalog Main Page">Back 
				to Product Catalog Main Page</a></td>
	</tr>
</table>
