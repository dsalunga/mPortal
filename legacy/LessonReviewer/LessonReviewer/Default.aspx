<%@ Page Title="MCGI Make-Up Service" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WCMS.LessonReviewer._Default" %>

<%@ Register Assembly="Media-Player-ASP.NET-Control" Namespace="Media_Player_ASP.NET_Control"
    TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ClientIDMode="Static" ID="hEnableKeepAlive" runat="server" Value="0" />
    <h2 id="lblHeader" runat="server">
        &nbsp;
    </h2>
    <br />
    <div id="panelPlayerMain">
        <table width="100%" border="0">
            <tr>
                <td style="width: 600px;">
                    <div style="padding-right: 20px" id="panelPlayer">
                        <div runat="server" id="panelWMPlayer" clientidmode="Static">
                            <cc1:Media_Player_Control ClientIDMode="Static" ID="mediaPlayer" MovieURL="" runat="server"
                                Height="400px" Width="600px" AutoStart="true" />
                        </div>
                        <div runat="server" id="panelOtherPlayer" clientidmode="Static" visible="false">
                            <object id="nonIeWMPlayer" width="600px" height="400px" type="application/x-ms-wmp"
                                viewastext>
                                <param name="autoStart" value="True" />
                                <param runat="server" id="paramUrl" clientidmode="Static" name="URL" value="" />
                                <param name="enabled" value="True" />
                                <param name="balance" value="0" />
                                <param name="currentPosition" value="0" />
                                <param name="enableContextMenu" value="True" />
                                <param name="fullScreen" value="False" />
                                <param name="mute" value="False" />
                                <param name="playCount" value="1" />
                                <param name="rate" value="1" />
                                <param name="stretchToFit" value="False" />
                                <param name="uiMode" value="full" />
                            </object>
                        </div>
                        <div runat="server" id="panelToggleFullscreen" visible="false" style="margin-top: 3px;
                            float: left">
                            <input type="button" value="Toggle Fullscreen" onclick="ToggleFullscreen();" />
                        </div>
                        <div runat="server" id="panelParts" clientidmode="static" visible="false" style="margin-top: 3px;
                            float: right">
                            <asp:DropDownList ID="cboParts" ClientIDMode="Static" runat="server">
                                <asp:ListItem Value="">Go to...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </td>
                <td style="text-align: left; vertical-align: top">
                    <div>
                        Service Type:<br />
                        <asp:DropDownList ID="cboCategory" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            DataTextField="Value" DataValueField="Key" OnSelectedIndexChanged="cboCategory_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value=""># Select #</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:ObjectDataSource ID="ObjectDataSourceServices" TypeName="WCMS.LessonReviewer._Default"
                            SelectMethod="SelectServiceTypes" runat="server"></asp:ObjectDataSource>--%>
                        <br />
                        <br />
                        Service:<br />
                        <asp:DropDownList ID="cboServices" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            DataTextField="Value" DataValueField="Key" OnSelectedIndexChanged="cboServices_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value=""># Select #</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:GridView ID="gridServices" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            Font-Names="Verdana" Font-Size="9pt" DataSourceID="ObjectDataSourceFolders" EmptyDataText="No services found."
                            GridLines="None" ShowHeader="False">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFormatString="{0}" DataTextField="Name" DataNavigateUrlFields="Url" />
                            </Columns>
                        </asp:GridView>--%>
                        <%--<asp:ObjectDataSource ID="ObjectDataSourceFolders" runat="server" SelectMethod="SelectServiceInstances"
                            TypeName="WCMS.LessonReviewer._Default">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="serviceType" QueryStringField="ServiceType" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>--%>
                        <br />
                        <br />
                        Language:<br />
                        <asp:DropDownList ID="cboLanguage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboLanguage_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value=""># Select #</asp:ListItem>
                            <asp:ListItem Value="TL">Tagalog</asp:ListItem>
                            <asp:ListItem Value="EN">English</asp:ListItem>
                        </asp:DropDownList>
                        <div runat="server" id="panelPlaylist" clientidmode="static" visible="false">
                            <br />
                            Playlist:<br />
                            <asp:DropDownList ID="cboPlaylist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboPlaylist_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value=""># Play All #</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <br />
                        <div style="color: Red;">
                            <asp:Literal ID="lblMsg" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <!-- Debug: <% =debugString %> -->
    <script type="text/javascript">
        var maxFetchRetries = 8;

        var fetchState = 0; // 0 - begin
        var currentMediaIndex = 0;
        var targetMediaIndex = 0;
        var fetchRetries = 0;

        $(document).ready(function () {
            var enableKeepAlive = $("#hEnableKeepAlive").val();
            if (enableKeepAlive == "1") {
                ExecuteKeepAlive();
            }

            var cboSegment = $("#cboParts");
            if (cboSegment.length > 0) {
                cboSegment.change(function () {
                    // Segment Changed
                    var mediaIndex = cboSegment.val();
                    if (mediaIndex != "") {
                        var player = getPlayer();
                        if (player != null) {
                            if (getIEVersion() > 0) {
                                // Sets the current playing item.
                                player.controls.currentItem = player.currentPlaylist.item(parseInt(mediaIndex));

                                if (player.playState != 3 && player.playState != 6 && player.playState != 7 && player.playState != 9 && player.playState != 11) {
                                    // when not playing, buffering, waiting, transitioning, and reconnecting
                                    player.controls.play();
                                }
                            }
                            else {
                                /*
                                if (player.playState != 1) {
                                player.controls.stop();
                                }

                                // alert("Target: " + mediaIndex);
                                ResetSeeker();
                                targetMediaIndex = mediaIndex;
                                BeginSeekItem();
                                */

                                alert("As of the moment, this feature is only supported in Internet Explorer.");
                            }
                        }

                        cboSegment.val("");
                    }
                });
            }
        });

        function ShowPlayer(show) {
            $("#panelPlayer").css("display", show ? "" : "none");
        }

        function ResetSeeker() {
            fetchState = 0;
            fetchRetries = 0;
            currentMediaIndex = 0;
            targetMediaIndex = 0;
        }

        function InvokeSeeker() {
            setTimeout("BeginSeekItem();", 800);
        }

        function BeginSeekItem() {
            try {
                var player = getPlayer();
                if (player) {
                    if (player.playState == 3) {
                        if (fetchState == 0) {
                            // Initialiaze / Move to first file
                            fetchState = 1;
                            InvokeSeeker();
                        }
                        else if (fetchState == 1) {
                            var media = player.currentMedia;

                            currentMediaIndex++;

                            //alert("current: " + currentMediaIndex);

                            if (targetMediaIndex > currentMediaIndex) {
                                player.controls.next();

                                InvokeSeeker();
                            }
                            else {
                                // Complete here

                                //ShowPlayer(true);

                                //player.controls.stop();

                                //ResetSeeker();
                            }
                        }

                        fetchRetries = 0;
                    }
                    else if (fetchRetries < maxFetchRetries) {
                        fetchRetries++;

                        if (player.playState != 3 && player.playState != 6 && player.playState != 7 && player.playState != 9 && player.playState != 11) {
                            // when not playing, buffering, waiting, transitioning, and reconnecting
                            player.controls.play();
                        }

                        InvokeSeeker();
                    }
                    else {
                        //ShowPlayer(true);

                        alert("Sorry, unable to seek selected part.");
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
        }

        function getPlayer() {
            var player = $("#nonIeWMPlayer")[0];

            if (!player)
                player = $("#mediaPlayer > object")[0];

            return player;
        }

        function ToggleFullscreen() {
            var player = getPlayer();
            if (player) {
                if (player.playState != 3)
                    player.controls.play();

                if (player.playState == 3)
                    player.fullScreen = true;
            }
        }
    </script>
</asp:Content>
