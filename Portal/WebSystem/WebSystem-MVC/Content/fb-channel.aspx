<%@ Page Language="C#" AutoEventWireup="true"  ClassName="WCMS.WebSystem.FBChannel" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now.AddYears(1));
        Response.Cache.SetCacheability(HttpCacheability.Public);
        Response.Cache.SetValidUntilExpires(true);
    }

</script>

<script src="//connect.facebook.net/en_US/all.js"></script>