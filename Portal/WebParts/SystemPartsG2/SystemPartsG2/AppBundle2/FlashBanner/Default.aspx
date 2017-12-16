<%@ Page Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.FlashBanner._Sections_FB_Default" Codebehind="Default.aspx.cs" %>

<%@ Register Src="Renderer.ascx" TagName="Renderer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Flash Banner</title>
</head>
<body style="background-color: Black">
    <form id="form1" runat="server">
    <div>
        <uc1:Renderer ID="Renderer1" runat="server" />
    
    </div>
    </form>
</body>
</html>
