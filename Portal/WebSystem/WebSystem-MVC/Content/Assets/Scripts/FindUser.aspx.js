// JScript File
var xmlHttp = null;

function showOverlay()
    {   
        var divOverlay = document.getElementById("divOverlay");
        var divResults = document.getElementById("divResults");
        var cmdSearch = document.getElementById("cmdSearch");
        //divOverlay.style.width = document.body.clientWidth;
        //divOverlay.style.height = document.body.clientHeight;
        //divOverlay.style.top = document.body.clientHeight / 2;
        //divOverlay.style.left = document.body.clientWidth / 2;
        divResults.style.display = "none";
        divOverlay.style.display = "";
        cmdSearch.disabled = true;
    }
   
function hideOverlay()
{
    var divOverlay = document.getElementById("divOverlay");
    var divResults = document.getElementById("divResults");
    var cmdSearch = document.getElementById("cmdSearch");
    
    divOverlay.style.display = "none";
    divResults.style.display = "";
    cmdSearch.disabled = false;
}

function cmdFind_Click()
{
    var sUserName = document.getElementById("txtUserName").value;
    var sFirstName = document.getElementById("txtFirstName").value;
    var sEmail = document.getElementById("txtEmail").value;
    var txtLastName = document.getElementById("txtLastName");
    var sLastName = txtLastName.value;
    
    var spanMsg = document.getElementById("spanMsg");
    
    if(sUserName.length==0 && sFirstName.length==0 && sLastName.length==0 && sEmail.length==0)
    {
        //alert("");
        spanMsg.innerHTML = "Please enter at least one search criteria.";
        txtLastName.focus();
        return false;
    }
    
    if((sUserName.length<2 && sUserName.length!=0) || (sFirstName.length<2 && sFirstName.length!=0) || (sLastName.length<2 && sLastName.length!=0) || (sEmail.length<2 && sEmail.length!=0))
    {
        spanMsg.innerHTML = "Minimum search criteria length is 2 characters.";
        //alert("Minimum search criteria length is 2 characters.");
        txtLastName.focus();
        return false;
    }
    
    spanMsg.innerHTML = "";
    
    showOverlay();
    
    xmlHttp.abort();
    xmlHttp.onreadystatechange = Data_Arrived;
    xmlHttp.open("GET", "../Handlers/FindUser.ashx?FirstName=" + sFirstName + "&UserName=" + sUserName + "&LastName=" + sLastName + "&Email=" + sEmail);
    xmlHttp.send();
    
    return false;
}

function tr_Click(userName)
{
    window.returnValue = userName;
    window.close();
}

function loadXMLDoc(fname)
{
    var xmlDoc;
// code for IE
    if (window.ActiveXObject)
    {
        xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
    }
  
    // code for Mozilla, Firefox, Opera, etc.
    else if (document.implementation && document.implementation.createDocument)
    {
        xmlDoc=document.implementation.createDocument("","",null);
    }
    else
    {
        alert('Your browser cannot handle this script');
    }
    
    xmlDoc.async=false;
    xmlDoc.load(fname);
    return(xmlDoc);
}

function Page_Load()
{
    if (window.XMLHttpRequest) {
      // If IE7, Mozilla, Safari, and so on: Use native object.
      xmlHttp = new XMLHttpRequest();
    }
    else
    {
      if (window.ActiveXObject) {
         // ...otherwise, use the ActiveX control for IE5.x and IE6.
         xmlHttp = new ActiveXObject('MSXML2.XMLHTTP.3.0');
      }
    }
}

function Data_Arrived()
{
    if(xmlHttp.readyState == 4)
    {
        hideOverlay();
        
        //if(xmlHttp.responseXML.xml != "")
        //{
            xml = xmlHttp.responseXML;
            xsl = loadXMLDoc("../XStyles/FindUser.aspx.xsl");

            // code for IE
            if (window.ActiveXObject)
            {
                ex = xml.transformNode(xsl);
                document.getElementById("divResults").innerHTML = ex;
            }
            
                    
        var txtLastName = document.getElementById("txtLastName");
        txtLastName.focus();
    }
}