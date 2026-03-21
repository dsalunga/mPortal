<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailPreview.aspx.cs" Inherits="WCMS.WebSystem.WebParts.Article.EmailPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        h3
        {
            font-size: 150%;
        }
        
        input, select, textarea
        {
            font: 11px/18px 'Lucida Grande' , 'Lucida Sans Unicode' ,Verdana,Arial,Helvetica,sans-serif;
        }
        
        body, p, li, dt, dd, label
        {
            color: #000000;
            font-family: 'Lucida Sans' , 'Lucida Grande' , 'Lucida Sans Unicode' ,sans-serif;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img border="0" alt="" src="/Templates/website/assets/images/logo.png" />
        <br />
        <h3 id="lblTitle" runat="server"></h3>
        <em>
            <div runat="server" id="lblPublishedDate">
                Published:&nbsp;December 8, 2009</div>
        </em>
        <table width="100%" border="0">
            <tbody>
                <tr>
                    <td style="height: 5px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="lblContent" runat="server"></div>
                        <br />
                        <br />
                        <em>Permalink:</em>
                        <br />
                        <a href="" id="linkPermalink" runat="server"></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
