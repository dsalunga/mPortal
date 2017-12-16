<%@ Page Language="c#" Inherits="WCMS.WebSystem.WebParts.Newsletter.Confirm" Codebehind="Confirm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>e-Newsletter Subscription</title>
    <style type="text/css">
	body {
	font-family: verdana, arial, helvetica, sans-serif;
	background-color: #FFF;
	text-align: center;
	}
	
#content {
	width: 800px; 
	text-align:left; 
	margin-top:60px;
	margin-right: auto;
	margin-left: auto; 	
	}

#content h2
{
	color: 3366ff;
}	
</style>
</head>
<body>
    <form id="frmMain" runat="server">
        <div id="content">
            <h2>
                e-Newsletter Subscription</h2>
            <p runat="server" id="pMessage">
            </p>
            <p>
                <a href="/" runat="server" id="aHome" title=""></a>
            </p>
        </div>
    </form>
</body>
</html>
