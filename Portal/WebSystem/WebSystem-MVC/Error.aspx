<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.Page" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string msg = Request["Message"];
            string url = Request["Url"];
            if (!string.IsNullOrWhiteSpace(msg) || !string.IsNullOrWhiteSpace(url))
            {
                lblErrorMessage.InnerHtml = Server.UrlDecode(msg);
                lblUrl.InnerHtml = Server.UrlDecode(url);

                panelErrorInfo.Visible = true;
            }

            //var err = Session["LastError"] as Exception;
            //Exception err = Server.GetLastError();
            //if (err != null)
            //{
            //    err = err.GetBaseException();
            //    var url = Request["aspxerrorpath"];

            //    lblErrorMessage.InnerHtml = err.Message;
            //    lblUrl.InnerHtml = url ?? ""; //Server.UrlDecode(url);
            //    panelErrorInfo.Visible = true;
            //    //lblErrorMsg.Text = err.Message;
            //    //lblSource.Text = err.Source;
            //    //lblInnerEx.Text = (err.InnerException != null) ? err.InnerException.ToString() : "";
            //    //lblStackTrace.Text = err.StackTrace;
            //    Session["LastError"] = null;
            //}
        }
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <style type="text/css">
        body {
            font-family: verdana, arial, helvetica, sans-serif;
            background-color: #FFF;
            text-align: center;
        }

        #content {
            width: 800px;
            text-align: left;
            margin-top: 60px;
            margin-right: auto;
            margin-left: auto;
        }

            #content h2 {
                color: #3366ff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content">
        <h2>An error has occured, sorry.</h2>
        <p>
            We apologize because an error occurred and your request could not be completed.
        </p>
        <p>
            This error has been logged. If you have additional information that you believe
            may have caused this error please report the problem to us.
        </p>
        <br />
        <div runat="server" id="panelErrorInfo" visible="false">
            <p>
                <div>
                    Error Description:
                </div>
                <div>
                    <strong><span id="lblErrorMessage" runat="server"></span></strong>
                </div>
            </p>
            <p>
                <div>
                    Request Url:
                </div>
                <div>
                    <strong><span id="lblUrl" runat="server"></span></strong>
                </div>
            </p>
        </div>
    </div>
    </form>
</body>
</html>
