<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignMode.ascx.cs"
    Inherits="WCMS.WebSystem.Controls.DesignModeController" EnableViewState="false" %>
<%@ Import Namespace="WCMS.Framework" %>
<style type="text/css">
    #designMode {
        /* #E8E8E8; */
        border: 2px solid #f44365;
        position: fixed;
        z-index: 10000;
        font-family: Georgia, Calibri;
        background-color: #fff;
        font-size: 10pt;
        color: #444;
        border-radius: 6px;
    }

        #designMode select {
            font-family: Georgia, Calibri;
            -webkit-appearance: menulist;
            box-sizing: border-box;
            -webkit-box-align: center;
            border: 1px solid #aaa;
            white-space: pre;
            -webkit-rtl-ordering: logical;
            color: black;
            background-color: white;
            cursor: default;
        }

        #designMode a, #designMode a:visited {
            /* color: #3380CC; */
            color: #3366B7;
            text-decoration: none;
        }

        #designMode select, #designMode a {
            font-size: 10pt;
        }

        #designMode img.icon {
            border-style: none;
            vertical-align: middle;
        }

        #designMode .toolbox-icons img.icon {
            width: 20px;
        }

        #designMode a:hover {
            color: #D95852;
        }

        #designMode .handle {
            background-image: -webkit-linear-gradient(top, #ee5f5b, #bd362f);
            min-height: 12px;
            padding: 2px;
            background-color: #f44365;
            width: auto;
            cursor: move;
        }

    #modeCtrl_mnSites img.icon {
        vertical-align: top !important;
        margin-right: 1px;
    }

    #modeCtrl_mnSites a.popout.dynamic, #mnSites a.dynamic {
        padding-right: 14px;
    }

    .IE8Fix {
        z-index: 100;
    }
</style>
<%
    var session = WSession.Current;
%>
<div id="designMode" style="top:<%=session.InDesignPanelTop%>px;left:<%=session.InDesignPanelLeft%>px;opacity:<%=session.IsDesignInitiated ? "1" : ".1"%>">
    <div class="handle" title="Move">
    </div>
    <div id="designPanelContent" runat="server" style="padding: 5px; display: block">
        <%--<div style="font-size: large; font-weight: bold; margin-bottom: 10px;">
            Control Box
        </div>--%>
        <span runat="server" id="panelModeChanger">
            <asp:DropDownList DataTextField="Name" DataValueField="Id" ID="cboPanels" runat="server"
                ToolTip="Select an option or a Panel to switch in Design Mode" AppendDataBoundItems="true"
                AutoPostBack="True" OnSelectedIndexChanged="cboPanels_SelectedIndexChanged">
                <asp:ListItem Selected="True" Text="# Live View #" Value="-2"></asp:ListItem>
                <asp:ListItem Text="* Design All *" Value="-1"></asp:ListItem>
            </asp:DropDownList>
            <asp:ImageButton ID="cmdToggle" CausesValidation="false" ImageUrl="/Content/Assets/Images/action_refresh.gif"
                runat="server" OnClick="cmdToggle_Click" ToolTip="Toggle between Design Mode and Preview Mode" />
            <br />
        </span>
        <div style="margin: 0 0 15px 0">
            <!-- Start of Web Page -->
            <%--<div runat="server" id="panelWebPage" visible="false" style="border-bottom: 1px solid #bbb; padding-bottom: 4px">
                <img class="icon" title="Click here for Edit Mode" src="/Content/Assets/Images/Common/folder.gif"
                    alt="Web Page" /><strong>Web Page</strong>
            </div>--%>
            <%--<div runat="server" id="panelNewPage" visible="false">
                <a id="linkNewPage" runat="server" href="#" title="Create a new page under the current site">
                    <img class="icon" title="Create a new page under the current site" alt="" src="/Content/Assets/Images/Common/image_new.gif" />New
                    Page</a>
            </div>--%>
            <div runat="server" id="panelPageEditMode" visible="false">
                <a id="linkPageEditMode" runat="server" href="#" title="Settings: Manage the content if this Element and set additional parameters if there's any.">
                    <img class="icon" alt="" src="/Content/Assets/Images/Common/Objects.gif" /><strong>Settings</strong></a>
            </div>
            <%--<div runat="server" id="panelPageConfigure" visible="false">
                <a id="linkPageConfigure" runat="server" href="#" title="Configure this Element, its properties, permissions, content, etc.">
                    <img class="icon" title="Configure this Element, its properties, permissions, content, etc."
                        alt="" src="/Content/Assets/Images/Common/ico_edit.gif" />Configure</a>
            </div>--%>
            <!-- Start of Web Site -->
            <%--<div runat="server" id="panelWebSite" visible="false" style="border-bottom: 1px solid #bbb; margin-top: 15px; padding-bottom: 4px">
                <img class="icon" src="/Content/Assets/Images/Common/WebSite.gif" alt="Web Site" /><strong>Web Site</strong>
            </div>--%>
            <%--<div id="panelNewSite" runat="server" visible="false">
                <a runat="server" id="linkNewSite" href="#">
                    <img class="icon" src="/Content/Assets/Images/Common/image_new.gif" alt="New Site" />New Site</a>
            </div>--%>
            <%--<div id="panelSiteConfigure" runat="server" visible="false">
                <a runat="server" id="linkSiteConfigure" href="#">
                    <img class="icon" src="/Content/Assets/Images/Common/WebSite.gif" alt="Configure" />Web Site</a>
            </div>--%>
        </div>
        <asp:Menu ID="mnSites" runat="server" EnableViewState="true" MaximumDynamicDisplayLevels="5"
            RenderingMode="List" ToolTip="Navigate to different pages and sites">
            <DynamicHoverStyle BackColor="#FFCC00" ForeColor="White" />
            <DynamicMenuStyle CssClass="IE8Fix" BackColor="#E3EAEB" BorderColor="#FFFF99" BorderWidth="1px"
                BorderStyle="Dashed" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <DynamicMenuItemStyle BackColor="#E3EAEB" HorizontalPadding="5px" VerticalPadding="2px" />
        </asp:Menu>
        <!-- Administration -->
        <a runat="server" id="linkLogOff" href="#" target="_top" title="Sign Out">
            <img class="icon" alt="Sign Out" src="/Content/Assets/Images/TreeView/l.gif" />Sign Out</a>
    </div>
    <div style="text-align: right" class="toolbox-icons">
        <div style="float: right; margin: 3px">
            &nbsp;<a href="javascript:;" id="showHideDesignMode" title="Expand/Collapse ToolBox"
                runat="server" style="font-weight: bold; text-decoration: none; font-size: 14pt; line-height: 22px"></a>
        </div>
        <div id="panelMiniIcons" runat="server" clientidmode="Static" style="float: right; padding: 0px;">
            <a id="linkEditMode" clientidmode="Static" visible="false" runat="server" href="#"
                title="Settings: Manage the content if this Element and set additional parameters if there's any.">
                <img class="icon" style="margin: 4px 0px 2px 4px" alt="" src="/Content/Assets/Images/Common/Objects.gif" /></a><%--<a id="linkConfigure" runat="server"
                        visible="false" href="#" title="Configure: Configure this Element, its properties, permissions, content, etc."><img
                            class="icon" title="Configure: Configure this Element, its properties, permissions, content, etc."
                            alt="" src="/Content/Assets/Images/Common/ico_edit.gif" style="margin: 4px 0px 2px 4px" /></a>--%><a
                                id="linkAdmin" clientidmode="Static" runat="server" href="#" title="Administration"><img
                                    class="icon" style="margin: 4px 0px 2px 4px" title="Administration" alt="" src="/Content/Assets/Images/Common/Gear.gif" /></a>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        var m = $("#showHideDesignMode");
        if (m != null) {
            var dm = $("#designMode")
            dm.draggable({ handle: 'div.handle' });
            dm.bind('dragstop', function (event, ui) {
                var ajaxUrl = "/Content/Handlers/AjaxHandler.ashx?Method=SetDesignPanel&Left=" + ui.offset.left +
                    "&Top=" + ui.offset.top +
                    "&" + (new Date()).valueOf();
                $.get(ajaxUrl);
                opacityActivate();
            });

            // opacity conflicts w/ the menu

            var opacityFlag = <% =(session.IsDesignInitiated ? "true" : "false") %>;
            //console.log('o-flag: ' + opacityFlag);
            var isHover = false, isHovering = false;
            var applyOpacity = function () {
                //console.log('applyOpacity, isHovering: ' + isHovering + ', opacityFlag:' + opacityFlag);
                if(isHovering && !opacityFlag){
                    dm.css('opacity', isHover ? 1 : 0.15);
                } else {
                    dm.css('opacity', opacityFlag ? 1 : 0.15);
                    dm.unbind('click', applyOpacity);
                    //console.log('dm click');
                    var ajaxUrl = '/Content/Handlers/AjaxHandler.ashx?Method=SetDesignPanel&Init=' + (opacityFlag ? 1 : 0) + '&' + (new Date()).valueOf();
                    $.get(ajaxUrl);
                }
            }
            var timeOpacity = function(){
                if(opacityFlag){
                    //console.log('trigger set timout')
                    setTimeout(function (){
                        opacityFlag = false;
                        applyOpacity();
                    }, 5000);
                }
            }
            var triggerOpacity = function(){
                if(!opacityFlag){
                    //console.log('click dm');
                    opacityFlag = true;
                    applyOpacity();
                    timeOpacity();
                }
            }
            dm.click(function () {
                triggerOpacity();
            });
            dm.hover(function(){
                // Hover In
                //console.log('in');
                if(!opacityFlag){
                    isHover = true;
                    isHovering = true;
                    applyOpacity();
                }
            }, function(){
                // Hover Out
                //console.log('out');
                isHover = false;
                if(!opacityFlag){
                    applyOpacity();
                }
                isHovering = false;
            });

            if(opacityFlag){
                timeOpacity();
            }

            m.click(function () {
                var panel = $("#designPanelContent");
                var pd = panel.css("display");

                // Toggle with transition
                var options = {};
                panel.toggle('slide', options, 'fast');

                // Persist the state of panel
                var isExpanded = (pd == "block" || pd == "") ? "0" : "1";
                $("#panelMiniIcons").css("display", isExpanded == "1" ? "none" : "");

                var ajaxUrl = "/Content/Handlers/AjaxHandler.ashx?Method=SetDesignPanel&Expanded=" + isExpanded + "&" + (new Date()).valueOf();
                m.get(0).innerHTML = isExpanded == "1" ? "&laquo;" : "&raquo;";
                $.get(ajaxUrl);
            });
        }
    });
</script>
