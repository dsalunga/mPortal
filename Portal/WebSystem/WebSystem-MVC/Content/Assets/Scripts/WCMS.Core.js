var WCMS = {
    // @@@@@@@@@@@@@@
    // Ajax namespace
    Ajax: {
        CreateInstance: function (func, addr) {
            // a = Ajax Object
            var a = null;

            if (window.XMLHttpRequest) {
                // If IE7, Mozilla, Safari, and so on: Use native object.
                a = new XMLHttpRequest();
            }
            else {
                if (window.ActiveXObject) {
                    // ...otherwise, use the ActiveX control for IE5.x and IE6.
                    a = new ActiveXObject('MSXML2.XMLHTTP.3.0');
                }
                else {
                    alert("Could not create AJAX object.");
                }
            }

            if (func != null) {
                a.onreadystatechange = function () {
                    if (a.readyState == 4) {
                        func(a);
                    }
                }

                if (addr != null) {
                    a.open("GET", addr);
                    a.send();
                }
            }

            return a;
        },

        // use the ajax object to send multiple requests
        // a = Ajax Object, addr = Request Address, func = OnDataArrived function call
        Get: function (a, addr, func) {
            if (a != null) {
                // if recent call has been made, reset the object
                if (a.readyState == 4) a.abort();

                a.onreadystatechange = function () {
                    if (a.readyState == 4) {
                        //alert("data arrived");
                        func(a);
                    }
                };

                a.open("GET", addr, true);
                a.send();

                //alert("data sent: " + addr);
            }
            else {
                alert("AJAX is null");
            }
        }
    },

    // @@@@@@@@@@@@@@@
    // Converter class
    Convert: {
        ToString: function (obj) {
            return obj + "";
        }
    },

    // Form Utilities
    Form: {
        SetDefaultSubmit: function (txt, cmd) {
            txt.keypress(function (e) {
                if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
                    cmd.focus();
                    cmd.click();

                    return true;
                }
            });
        },

        DisableButtonsOnFormSubmit: function () {
            $('form').submit(function () {
                /* // On submit disable its submit button
                $('input[type=submit]', this).attr('disabled', 'disabled');
                $('select', this).attr('disabled', 'disabled');
                $('input[type=text]', this).attr('readonly', 'readonly');
                $('textarea', this).attr('readonly', 'readonly');
                */

                // Does not work yet - DO NOT USE

                if (typeof (ValidatorOnSubmit) == 'function' && ValidatorOnSubmit() == false) {
                    return false;
                } else {
                    $("input[type=submit]")
                        .click(function () { return false })
                        .fadeTo(200, 0.5);
                    return true;
                }
            });
        }
    },

    HttpUtility: {

        // public method for url encoding
        UrlEncode: function (string) {
            // The Javascript escape and unescape functions do not correspond
            // with what browsers actually do...
            var SAFECHARS = "0123456789" + 				// Numeric
					            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + // Alphabetic
					            "abcdefghijklmnopqrstuvwxyz" +
					            "-_.!~*'()"; 				// RFC2396 Mark characters
            var HEX = "0123456789ABCDEF";

            var plaintext = string;
            var encoded = "";
            for (var i = 0; i < plaintext.length; i++) {
                var ch = plaintext.charAt(i);
                if (ch == " ") {
                    encoded += "+"; 			// x-www-urlencoded, rather than %20
                } else if (SAFECHARS.indexOf(ch) != -1) {
                    encoded += ch;
                } else {
                    var charCode = ch.charCodeAt(0);
                    if (charCode > 255) {
                        alert("Unicode Character '"
                                    + ch
                                    + "' cannot be encoded using standard URL encoding.\n" +
				                      "(URL encoding only supports 8-bit characters.)\n" +
						              "A space (+) will be substituted.");
                        encoded += "+";
                    } else {
                        encoded += "%";
                        encoded += HEX.charAt((charCode >> 4) & 0xF);
                        encoded += HEX.charAt(charCode & 0xF);
                    }
                }
            } // for

            return encoded;
        },

        // public method for url decoding
        UrlDecode: function (string) {
            // Replace + with ' '
            // Replace %xx with equivalent character
            // Put [ERROR] in output if %xx is invalid.
            var HEXCHARS = "0123456789ABCDEFabcdef";
            var encoded = string;
            var plaintext = "";
            var i = 0;
            while (i < encoded.length) {
                var ch = encoded.charAt(i);
                if (ch == "+") {
                    plaintext += " ";
                    i++;
                } else if (ch == "%") {
                    if (i < (encoded.length - 2)
					            && HEXCHARS.indexOf(encoded.charAt(i + 1)) != -1
					            && HEXCHARS.indexOf(encoded.charAt(i + 2)) != -1) {
                        plaintext += unescape(encoded.substr(i, 3));
                        i += 3;
                    } else {
                        alert('Bad escape combination near ...' + encoded.substr(i));
                        plaintext += "%[ERROR]";
                        i++;
                    }
                } else {
                    plaintext += ch;
                    i++;
                }
            } // while

            return plaintext;
        }
    },

    // @@@@@@@@@@@@@@@@@@
    // HTML DOM namespace
    Dom: {
        Get: function (s) {
            return document.getElementById(s);
        },

        GetFromHidden: function (id) {
            var hid = document.getElementById(id);
            if (hid != undefined) {
                return document.getElementById(hid.value);
            }

            return null;
        },

        GetByName: function (s) {
            return document.getElementsByName(s);
        },

        /*
        GetDocumentHeight : function() {
        var body = screen.getBody();
        var innerHeight = (defined(self.innerHeight)&&!isNaN(self.innerHeight))?self.innerHeight:0;
        if (!document.compatMode || document.compatMode=="CSS1Compat") {
        var topMargin = parseInt(CSS.get(body,'marginTop'),10) || 0;
        var bottomMargin = parseInt(CSS.get(body,'marginBottom'), 10) || 0;
        return Math.max(body.offsetHeight + topMargin + bottomMargin,
        document.documentElement.clientHeight,
        document.documentElement.scrollHeight, screen.zero(self.innerHeight));
        }
            
        return Math.max(body.scrollHeight, body.clientHeight,
        screen.zero(self.innerHeight));
        },
        */

        RecreateElement: function recreateElement(old_Element) {
            var attribs = new Array(
                "type", "id", "tabIndex",
                "className", "name",
                "onpropertychange");

            var size = attribs.length;
            var values = new Array(size);
            var old_type = old_Element.nodeName;

            // store old object's attributes
            for (i = 0; i < size; i++) {
                values[i] = old_Element.getAttribute(attribs[i]);
            }

            var oParent = old_Element.parentElement;
            oParent.removeChild(old_Element);

            // create new object
            var oNew = document.createElement(old_type);

            // set new properties
            for (i = 0; i < size; i++) {
                oNew.setAttribute(attribs[i], values[i]);
            }

            // add to parent
            oParent.appendChild(oNew);
        },

        Navigate: function (url) {
            window.navigate(url);
        },

        ShowModalDialog: function (page) {
            var ret = window.showModalDialog(page, "",
                "dialogWidth:615px;dialogHeight:650px;help:no;resizable:yes");

            //alert(retValue);
            if (ret != null && ret != "") {
                return ret;
            }
        },

        ShowModal: function (url, args, features, showOverlay) {
            if (showOverlay) {
                // Show overlay
                //des.Effects.Overlay.Show();
            }

            var rValue = window.showModalDialog(url, args, features);

            if (showOverlay) {
                // Hide overlay
                //des.Effects.Overlay.Hide();
            }

            return rValue;
        },

        Show: function (url, w, h) {
            var width = w == null ? 900 : w;
            var height = h == null ? 600 : h;

            var wLeft = (screen.availWidth / 2) - (width / 2);
            var wTop = (screen.availHeight / 2) - (height / 2);

            window.open(url, null, 'scrollbars=yes,resizable=yes,width=' + width + ',height=' + height + ',left=' + wLeft + ',top=' + wTop);
        },

        Open: function (url, w, h) {
            var width = w == null ? 900 : w;
            var height = h == null ? 600 : h;

            var wLeft = (screen.availWidth / 2) - (width / 2);
            var wTop = (screen.availHeight / 2) - (height / 2);

            window.open(url, null, 'toolbar=no,menubar=no,scrollbars=yes,resizable=yes,width=' + width + ',height=' + height + ',left=' + wLeft + ',top=' + wTop);
        },

        InvokeDefaultButton: function (button) {
            if (window.event.keyCode == 13) {
                var b = document.getElementById(button);
                b.focus();
                b.click();
            }
        },

        AddEvent: function (objectRef, eventName, eventMethod) {
            if (document.addEventListener) {
                // Firefox
                objectRef.addEventListener(eventName, eventMethod, false);
            }
            else {
                // IE
                objectRef.attachEvent('on' + eventName, eventMethod);
            }
        },
		
		AddEvent2: function (objectRef, eventName, eventMethod) {
            if (document.addEventListener) {
                // Firefox
                objectRef.addEventListener(eventName, eventMethod, false);
            }
            else {
                // IE
                objectRef.attachEvent(eventName, eventMethod);
            }
        },

        FireEvent: function (objRef, eventName) {
            if (objRef.fireEvent) {
                objRef.fireEvent('on' + eventName); // for IE
            } else if (document.createEvent && objRef.dispatchEvent) {
                var evt = document.createEvent("HTMLEvents");
                evt.initEvent(eventName, true, true);
                objRef.dispatchEvent(evt); // for DOM-compliant browsers
            }
        },

        Confirm: function (msg) {
            return confirm(msg);
        }
    },

    // # Text Namespace #
    Text: {
        LimitInputBox: function (oInput, max_Chars) {
            if (oInput.value.length > max_Chars)
                oInput.value = oInput.value.substring(0, max_Chars);
        },

        toProperCase: function (str) {
            return str.toLowerCase().replace(/^(.)|\s(.)/g,
                function ($1) { return $1.toUpperCase(); });
        }
    },

    Misc: {
        DisableOnTextChange: function (txt, c) {
            var i = document.getElementById(c);
            if (txt.value == "") {
                i.disabled = true;
            }
            else {
                i.disabled = false;
            }
        }
    },

    DateTime: {
        Ticks: (new Date()).valueOf()
    },

    Effects: {
        Overlay: {
            Show: function () {
                // Hide all SELECT
                var selects = document.getElementsByTagName("SELECT");
                if (selects.length > 0) {
                    for (var i = 0; i < selects.length; i++) {
                        selects[i].style.visibility = "hidden";
                    }
                }

                // Apply overlay
                var divOverlay = document.getElementById("wcms_Overlay");
                if (divOverlay == null) {
                    divOverlay = document.createElement("DIV");
                    divOverlay.id = "wcms_Overlay";
                    document.body.appendChild(divOverlay);
                }

                divOverlay.className = "ShadeOverlay";
                //divOverlay.style.height = WCMS.Dom.GetDocumentHeight();
                divOverlay.style.display = "block";
            },

            Hide: function () {
                // Show all SELECT
                var selects = document.getElementsByTagName("SELECT");
                if (selects.length > 0) {
                    for (var i = 0; i < selects.length; i++) {
                        selects[i].style.visibility = "";
                    }
                }

                // Remove overlay
                var divOverlay = document.getElementById("wcms_Overlay");
                if (divOverlay != null) {
                    divOverlay.className = "";
                    document.body.removeChild(divOverlay);
                }
            }
        }
    },

    // @@@@@@@@@@@@@
    // XML Namespace
    Xml: {
        Load: function (path) {
            // add browser validation? xml component version?
            var xmlDoc = new ActiveXObject("Msxml2.DOMDocument.3.0");
            xmlDoc.async = false;

            if (path != null && path != "") {
                xmlDoc.load(path); //"books.xml");
            }

            return xmlDoc;
        },

        // xml_doc can be a path? and also a xmldoc object?
        // NOTE: Do not put xml files in ASP.NET special folders
        Transform: function (xmlObject, xslPath) {
            var xmlXslt;

            // code for IE
            if (window.ActiveXObject) {
                xmlXslt = new ActiveXObject("Microsoft.XMLDOM");
            }
            /*
            else if (document.implementation && document.implementation.createDocument)
            {
            // code for Mozilla, Firefox, Opera, etc.
            xmlXslt = document.implementation.createDocument("","",null);
            }
            */
            else {
                alert("create Microsoft.XMLDOM failed");
            }

            xmlXslt.async = false;
            xmlXslt.load(xslPath);

            if (window.ActiveXObject) {
                return xmlObject.transformNode(xmlXslt);
            }
            /*
            else if (document.implementation && document.implementation.createDocument)
            {
            xsltProcessor = new XSLTProcessor();
            xsltProcessor.importStylesheet(xmlXslt);
                
            return xsltProcessor.transformToFragment(xml,document);
            //document.getElementById("example").appendChild(resultDocument);
            }
            */
            else {
                alert("transform failed");
            }

        }
    }
}

String.prototype.Trim = function () {
    // Use a regular expression to replace leading and trailing 
    // spaces with the empty string
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

String.prototype.GetFirstWord = function () {
    var firstWordIndex = this.indexOf(" ");
    if (firstWordIndex >= 0) {
        return this.substring(0, firstWordIndex);
    }

    return this;
}

String.prototype.IsEmpty = function () {
    // how to get the string value?
    return this.Trim() == "";
}

String.prototype.isEmpty = function () {
    // how to get the string value?
    return this.Trim() == "";
}