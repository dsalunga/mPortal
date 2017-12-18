<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="WCMS.Framework" %>
<%--<%@ Import Namespace="WCMS.WebSystem.WebParts.Registration.ONEWebService" %>
<%@ Import Namespace="WCMS.WebSystem.WebParts.Registration.ONE" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<%
            webservice01wcmsorgSoapClient client = new webservice01wcmsorgSoapClient();
            //var response1 = client.CheckUserAppPermission1(ONEConstants.PORTAL_APP_ONE_KEY, "USER_ID", "pwd");
            var response2 = client.GetUserInfo1(ONEConstants.PORTAL_APP_ONE_KEY, "USER_ID");
        %>
        <h1><% =WConfig.Environment %></h1>
        <h1><% =response2.Tables.Count %></h1>--%>
        <div>Request.Url.AbsolutePath: <strong><% =Request.Url.AbsolutePath %></strong></div>
        <br />
        <%
            var sb = new StringBuilder();
            var allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                sb.AppendFormat("Drive {0}<br/>", d.Name);
                sb.AppendFormat("  File type: {0}<br/>", d.DriveType);
                if (d.IsReady == true)
                {
                    sb.AppendFormat("  Volume label: {0}<br/>", d.VolumeLabel);
                    sb.AppendFormat("  File system: {0}<br/>", d.DriveFormat);
                    sb.AppendFormat(
                        "  Available space to current user:{0, 15} bytes<br/>",
                        d.AvailableFreeSpace);

                    sb.AppendFormat(
                        "  Total available space:          {0, 15} bytes<br/>",
                        d.TotalFreeSpace);

                    sb.AppendFormat(
                        "  Total size of drive:            {0, 15} bytes<br/><br/> ",
                        d.TotalSize);
                }
            }

             %>

        <h1><% = sb.ToString() %></h1>
    </div>
    </form>
</body>
</html>
