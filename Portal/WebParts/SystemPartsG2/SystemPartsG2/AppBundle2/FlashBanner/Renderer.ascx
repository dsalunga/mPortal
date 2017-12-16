<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.FlashBanner._Sections_FB_Renderer"
    CodeBehind="Renderer.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<script src="<%=WebUtil.Version("~/content/parts/flashbanner/ac_runactivecontent.min.js")%>" language="javascript"></script>
<script language="javascript" type="text/javascript">
    if (AC_FL_RunContent == 0) {
        alert("This page requires AC_RunActiveContent.js.");
    } else {
        AC_FL_RunContent(
			'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0',
			'width', '550',
			'height', '400',
			'src', '/Content/Parts/FlashBanner/FlashBanner',
			'quality', 'high',
			'pluginspage', 'http://www.macromedia.com/go/getflashplayer',
			'align', 'middle',
			'play', 'true',
			'loop', 'true',
			'scale', 'noborder',
			'wmode', 'window',
			'devicefont', 'false',
			'id', 'FlashBanner',
			'bgcolor', '#ffffff',
			'name', 'FlashBanner',
			'menu', 'true',
			'allowFullScreen', 'false',
			'allowScriptAccess', 'sameDomain',
			'movie', '/Content/Parts/FlashBanner/FlashBanner',
			'salign', 'lt'
			); //end AC code
    }
</script>
<noscript>
    <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
        width="550" height="400" id="Object1" align="middle">
        <param name="allowScriptAccess" value="sameDomain" />
        <param name="allowFullScreen" value="false" />
        <param name="movie" value="/Content/Parts/FlashBanner/FlashBanner.swf" />
        <param name="quality" value="high" />
        <param name="scale" value="noborder" />
        <param name="salign" value="lt" />
        <param name="bgcolor" value="#ffffff" />
        <embed src="/Content/Parts/FlashBanner/FlashBanner.swf" quality="high" scale="noborder"
            salign="lt" bgcolor="#ffffff" width="550" height="400" name="FlashBanner" align="middle"
            allowscriptaccess="sameDomain" allowfullscreen="false" type="application/x-shockwave-flash"
            pluginspage="http://www.macromedia.com/go/getflashplayer" />
    </object>
</noscript>
