// JScript File

function InvokeDefaultButton() 
{
    if (window.event.keyCode == 13)
    {
        var b = document.getElementById("ctl00_cph1_cmdSearch");
        b.focus();
        b.click();
    }
}

// Start of Grid functions

function Grid_CheckAll(chk)
{
	var grid = document.getElementById("ctl00_cph1_GridView1");
    var cmdDelete = document.getElementById("ctl00_cph1_cmdDelete");
        
    for(var i=1; i<grid.rows.length; i++)
    {
        if(grid.rows[i].childNodes(0).childNodes(0).tagName == "INPUT")
        {
            var hiddenId = grid.rows[i].childNodes(0).childNodes(0);
            
            if((chk.checked&&hiddenId.value=="")||(!chk.checked&&hiddenId.value!=""))
            {
                grid.rows[i].childNodes(1).click();
            }
        }
    }
}

function Grid_HeaderClick()
{
    var o = window.event.srcElement;
    if(o.tagName == "A")
    {
        Grid_UpdateState();
    }
}

function Grid_UpdateState()
{
    var cmdDelete = document.getElementById("ctl00_cph1_cmdDelete");
    var selectedRecords = document.getElementById("selectedRecords");
    var selectedCount = document.getElementById("selectedCount");
    
    selectedCount.innerText = "0";
    selectedRecords.style.visibility = "hidden";
    cmdDelete.disabled = true;
}

function Grid_PagerClick()
{
    var o = window.event.srcElement;
    if(o.tagName == "A")
    {
        Grid_UpdateState();
    }
}

function Grid_RowClick()
{
    var row = window.event.srcElement.parentElement;
    
    if(row.tagName == "TR")
    {
        var selectedRecords = document.getElementById("selectedRecords");
        var selectedCount = document.getElementById("selectedCount");
        var cmdDelete = document.getElementById("ctl00_cph1_cmdDelete");
        
        var hiddenId = row.childNodes(0).childNodes(0);
        var hiddenId1 = row.childNodes(0).childNodes(1);
        var count = parseInt(selectedCount.innerText);
        
        // not selected
        if(hiddenId.value==null||hiddenId.value=="")
        {
            // select
            hiddenId.value = hiddenId1.value;
            row.className = (row.className=="HoverRowStyle"||row.className=="RowStyle") ? "SelRowStyle" : "AltSelRowStyle";
            count ++;
        }
        else
        {
            // deselect
            row.className = (row.className=="HoverRowStyle"||row.className=="SelRowStyle") ? "RowStyle" : "AltRowStyle"; //hiddenId1.value;
            hiddenId.value = "";
            count --;
        }
        
        selectedCount.innerText = count;
        selectedRecords.style.visibility = (count == 0) ? "hidden" : "";
        cmdDelete.disabled = count == 0;
    }
}

function Grid_MouseOut()
{
    var row = window.event.srcElement.parentElement;
    
    if(row.tagName == "TR")
    {
        if(row.className.substr(0,3)!="Sel" && row.className.substr(0,6)!="AltSel")
        {
            var hiddenId = row.childNodes(0).childNodes(0);
            if(hiddenId.value==null||hiddenId.value=="")
            {
                // not selected
                row.className = (row.className.substr(0,3) == "Alt") ? "AltRowStyle" : "RowStyle";
            }
            else
            {
                // selected
                row.className = (row.className.substr(0,3) == "Alt") ? "AltSelRowStyle" : "SelRowStyle";
            }
        }
    }
}

function Grid_MouseOver()
{
    var row = window.event.srcElement.parentElement;
    
    if(row.tagName == "TR")
    {
        if(row.className.substr(0,3)!="Sel" && row.className.substr(0,6)!="AltSel")
        {
            row.className = (row.className.substr(0,3) == "Alt") ? "AltHoverRowStyle" : "HoverRowStyle";
        }
    }
}

function Grid_Load()
{
    var cbo = document.getElementById("ctl00_cph1_cboSearchOperator");
    cbo.attachEvent("onchange", Grid_ConfigureFilter);
    
    var hiddenReadOnly = document.getElementById("ctl00_cph1_hiddenReadOnly");
    if(hiddenReadOnly != null && hiddenReadOnly.value=="1")
    {
        document.getElementById("chkCheckedMain").style.display = "none";
    }
}

// End of Grid functions


function ShowDetails(RowID)
{
    var tableId = document.getElementById("ctl00_cph1_hiddenTableID").value;
    var hiddenReadOnly = document.getElementById("ctl00_cph1_hiddenReadOnly");
    
    if(hiddenReadOnly != null && hiddenReadOnly.value=="1")
    {
        var dialogUrl = "DataView.aspx?TableID=" + tableId + "&ID=" + WCMS.HttpUtility.UrlEncode(RowID);
        window.open(dialogUrl,"_blank","height=500,width=650,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes");
    }
    else
    {
        var dialogUrl = "Data.aspx?TableID=" + tableId + "&ID=" + WCMS.HttpUtility.UrlEncode(RowID);
        location.href = dialogUrl;
    }
    
    return false;
}

function Grid_ConfigureFilter()
{
    var cbo = document.getElementById("ctl00_cph1_cboSearchOperator");
    var span = document.getElementById("ctl00_cph1_spanSearch2");
    if(cbo.options[cbo.selectedIndex].value == "between")
    {
        span.style.display = "";
    }
    else
    {
        span.style.display = "none";
    }
    
    return true;
}


//function CheckAll(chk, chk_item)
//{
//	var i, chk_items, isChecked;
//	
//	chk_items = document.all.item(chk_item);
//	isChecked = chk.checked;
//	
//	if(chk_items == null)
//	{
//		return;
//	}
//	
//	if(chk_items.length > 0)
//	{
//		// MULTIPLE ITEMS
//		for(i=0; i<chk_items.length; i++)
//		{
//			chk_items[i].checked = isChecked;
//		}
//	}
//	else
//	{
//		// ONE ITEM ONLY
//		chk_items.checked = isChecked;
//	}
//}

window.attachEvent("onload", Grid_Load);
document.attachEvent("onkeydown", InvokeDefaultButton);