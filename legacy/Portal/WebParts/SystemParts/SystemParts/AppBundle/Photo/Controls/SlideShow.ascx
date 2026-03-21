<%@ Control Language="C#" ClassName="WCMS.WebSystem.WebParts.Photo.Controls.SlideShow" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="WCMS.Utils" %>
<script runat="server">
    //protected string sMainLink;
    private int _Width = 528;
    private int _Height = 199;

    public int Width
    {
        set { _Width = value; }
        get { return _Width; }
    }

    public int Height
    {
        set { _Height = value; }
        get { return _Height; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
</script>
<!--slide show-->
<script language="JavaScript1.2" type="text/javascript">
    var slideshow_width= '<% =this.Width %>px' //'528px' //SET IMAGE WIDTH
    var slideshow_height= '<% =this.Height %>px' //'199px' //SET IMAGE HEIGHT
    var pause=6200 //SET PAUSE BETWEEN SLIDE (3000=3 seconds)

    var fadeimages=new Array()
    //SET 1) IMAGE PATHS, 2) optional link, 3), optional link target:
    //fadeimages[0]=["photo1.jpg", "", ""] //plain image syntax
    //fadeimages[1]=["photo2.jpg", "http://www.google.com", ""] //image with link syntax
    //fadeimages[2]=["photo3.jpg", "http://www.javascriptkit.com", "_new"] //image with link and target syntax
    <% 
        QueryParser qs = new QueryParser(this.Parent.ID, true);
        ControlInfo ci = new ControlInfo(qs["ID"]);
        string sLocation = ci.Key(ControlInfoEnum.LocationType);
        int iID = int.Parse(ci.Key(ControlInfoEnum.ItemID));
            
        using (SqlDataReader r = SqlHelper.ExecuteReader("Photo.SELECT_Pictures", 
            new SqlParameter("@SitePageItemID", iID),
            new SqlParameter("@PageType", sLocation)
                        ))
        {
           int i = 0;
           while(r.Read())
           { 
    %>
    
    fadeimages[<% =i %>]=["/Assets/Uploads/Image/SECTIONS/GALLERY/<% =r["ImageURL"] %>","",""]
    
    <% 
            i++;
            }
        }
    %>
    /*
    fadeimages[1]=["http://www.deped.gov.ph/cpanel/uploads/pcps.jpg","",""]
    fadeimages[2]=["http://www.deped.gov.ph/cpanel/uploads/foundation.jpg","",""]
    fadeimages[3]=["http://www.deped.gov.ph/cpanel/uploads/ambasadors.jpg","",""]
    fadeimages[4]=["http://www.deped.gov.ph/cpanel/uploads/afsyes.jpg","",""]
    fadeimages[5]=["http://www.deped.gov.ph/cpanel/uploads/Dialogteachers.jpg","",""]
    fadeimages[6]=["http://www.deped.gov.ph/cpanel/uploads/ceap.jpg","",""]
    */
    ////NO need to edit beyond here/////////////

    var preloadedimages=new Array()
    for (p=0;p<fadeimages.length;p++){
        preloadedimages[p]=new Image()
        preloadedimages[p].src=fadeimages[p][0]
    }

    var ie4=document.all
    var dom=document.getElementById

    if (ie4||dom)
        document.write('<div style="position:relative;width:'+slideshow_width+';height:'+slideshow_height+';overflow:hidden"><div  id="canvas0" style="position:absolute;width:'+slideshow_width+';height:'+slideshow_height+';top:0;left:0;filter:alpha(opacity=10);-moz-opacity:10"></div><div id="canvas1" style="position:absolute;width:'+slideshow_width+';height:'+slideshow_height+';top:0;left:0;filter:alpha(opacity=10);-moz-opacity:10;visibility: hidden"></div></div>')
    else
        document.write('<img  width="<% =this.Width %>" name="defaultslide" src="'+fadeimages[0][0]+'">')

    var curpos=10
    var degree=10
    var curcanvas="canvas0"
    var curimageindex=0
    var nextimageindex=1

    function fadepic(){
        if (curpos<100){
            curpos+=10
            if (tempobj.filters)
                tempobj.filters.alpha.opacity=curpos
            else if (tempobj.style.MozOpacity)
                tempobj.style.MozOpacity=curpos/101
        }
        else{
            clearInterval(dropslide)
            nextcanvas=(curcanvas=="canvas0")? "canvas0" : "canvas1"
            tempobj=ie4? eval("document.all."+nextcanvas) : document.getElementById(nextcanvas)
            tempobj.innerHTML=insertimage(nextimageindex)
            nextimageindex=(nextimageindex<fadeimages.length-1)? nextimageindex+1 : 0
            var tempobj2=ie4? eval("document.all."+nextcanvas) : document.getElementById(nextcanvas)
            tempobj2.style.visibility="hidden"
            setTimeout("rotateimage()",pause)
        }
    }

    function insertimage(i){
        var tempcontainer=fadeimages[i][1]!=""? '<a href="'+fadeimages[i][1]+'" target="'+fadeimages[i][2]+'">' : ""
        tempcontainer+='<img src="'+fadeimages[i][0]+'" border="0" width="<% =this.Width %>">'
        tempcontainer=fadeimages[i][1]!=""? tempcontainer+'</a>' : tempcontainer
        return tempcontainer
    }


    function rotateimage(){
        if (ie4||dom){
            resetit(curcanvas)
            var crossobj=tempobj=ie4? eval("document.all."+curcanvas) : document.getElementById(curcanvas)
            crossobj.style.zIndex++
            tempobj.style.visibility="visible"
            var temp='setInterval("fadepic()",100)'
            dropslide=eval(temp)
            curcanvas=(curcanvas=="canvas0")? "canvas1" : "canvas0"
        }
        else
            document.images.defaultslide.src=fadeimages[curimageindex][0]
        curimageindex=(curimageindex<fadeimages.length-1)? curimageindex+1 : 0
    }

    function resetit(what){
        curpos=10
        var crossobj=ie4? eval("document.all."+what) : document.getElementById(what)
        if (crossobj.filters)
            crossobj.filters.alpha.opacity=curpos
        else if (crossobj.style.MozOpacity)
            crossobj.style.MozOpacity=curpos/101
    }

    function startit(){
        var crossobj=ie4? eval("document.all."+curcanvas) : document.getElementById(curcanvas)
        crossobj.innerHTML=insertimage(curimageindex)
        rotateimage()
    }

    if (ie4||dom)
        window.onload=startit
    else
        setInterval("rotateimage()",pause)

</script>
<script runat="server">
    }
</script>
<!--end of slideshow-->
<asp:Literal ID="___LiteralID" Visible="false" runat="server" />