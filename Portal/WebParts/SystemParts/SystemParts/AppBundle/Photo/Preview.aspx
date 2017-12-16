<%@ Page Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Photo._Sections_PG_Preview"
    CodeBehind="Preview.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Picture Gallery</title>
    <script language="javascript" type="text/javascript">
        //        function LoadImage(sSrc, sCaption) {
        //            var_imageFull = document.getElementById('imageFull');
        //            document.getElementById('spanImageCaption').innerHTML = sCaption;

        //            var_imageFull.removeAttribute("width");
        //            var_imageFull.src = '< =GalleryPath %>' + sSrc;
        //        }

        function ResizeImage(srcImage) {
            if (srcImage.width > 600) {
                srcImage.width = "600";
            }
        }
    </script>
    <style type="text/css">
        body
        {
            background-color: black;
            margin: 0px;
        }
        .caption
        {
            font-size: 14px;
            font-weight: bold;
            font-family: Tahoma;
            color: white;
        }
        .close
        {
            font-size: 9px;
            font-weight: normal;
            font-family: Tahoma;
            color: white;
            text-decoration: none;
        }
    </style>
</head>
<body onresize="javascript:ResizeImage(document.getElementById('imagePreview'));">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table border="0" cellpadding="5" cellspacing="0" align="center">
            <tr>
                <td align="center">
                    <img src="" alt="" runat="server" id="imagePreview" />
                </td>
            </tr>
            <tr>
                <td class="caption" align="center" id="tdCaption" runat="server">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <a class="close" title="Close this window" href="javascript:window.close();">[ CLOSE
                        WINDOW ]</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
