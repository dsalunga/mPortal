// JavaScript Document

var Page = {

	// PAGE SETUP

	onload : null, // set this to a function on the page if you want custom code executed by the body.onload event

	init : function () { // onload handler (executed in the context of window obj)
		Page.adjustShadowLayout();
		// check if the page has registered body.onload handlers that should be executed
		if(!Page.onload && typeof PageOnload == "function") {
			Page.onload = PageOnload;
		}
		if(typeof Page.onload == "function") {
			Page.onload();
		}
	},

	// LAYOUT CONTROL

	adjustShadowLayout : function() { // adjust page shadow design
		var cnt_height = document.getElementById("content").offsetHeight;
		var win_height = (window.innerHeight)? window.innerHeight : document.body.clientHeight;
		var cnt_height = parseFloat(cnt_height);
		var win_height = parseFloat(win_height);
		var height = ((cnt_height > win_height)? cnt_height : win_height) + "px";

		var sl = document.getElementById("shadowLeft");
		if(sl) sl.style.height = height;
		var sr = document.getElementById("shadowRight");
		if(sr) sr.style.height = height;
	}
};

// REGISTER PAGE INIT HANDLER

//window.onload = Page.init;
window.attachEvent("onload", Page.init);

if(self.name == "") {
	//window.onresize = Page.adjustShadowLayout;
	window.attachEvent("onresize", Page.adjustShadowLayout);
}
