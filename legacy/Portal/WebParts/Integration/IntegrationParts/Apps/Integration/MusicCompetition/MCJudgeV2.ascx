<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MCJudgeV2.ascx.cs" Inherits="WCMS.WebSystem.Apps.MusicCompetition.MCJudgeV2" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title><%= CompetitionName %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <link href="<%=WebUtil.Version("~/content/plugins/bootstrap/css/bootstrap.min.css")%>" rel="stylesheet" />
    <style>
        body {
            padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
        }

        table .center, table .center input {
            text-align: center;
            vertical-align: middle;
        }

        .entry-thumb {
            width: 100px;
        }

        .entry-thumb-edit {
            width: 60px;
            overflow: hidden;
        }

        .entry-number {
            width: 20px;
        }

        .entry-score {
            max-width: 150px;
        }

        .entry-name {
            width: 40%;
        }

        .entry-edit-area {
            height: 320px;
            overflow: auto;
            margin-bottom: 20px;
        }
    </style>
    <link href="<%=WebUtil.Version("~/content/plugins/bootstrap/css/bootstrap-responsive.min.css")%>" rel="stylesheet" />

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
</head>

<body>
    <form runat="server" id="formMain">
        <asp:HiddenField ID="hCompetitionId" runat="server" Value="-1" />
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="navbar-inner">
                <div class="container">
                    <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </a>
                    <a class="brand" href="."><%= CompetitionName %></a>
                    <div class="nav-collapse collapse">
                        <p class="navbar-text pull-right">
                            Welcome, <%=JudgeName %>!
                        </p>
                        <div runat="server" id="panelMenuSection">
                            <%--<ul class="nav">
                                <li class="active"><a href=".">Home</a></li>
                                <li><a href="Vote/" target="_blank">Voting Page</a></li>
                                <li><a href="/Static/Login/?Mode=LogOff">Sign out</a></li>
                            </ul>--%>
                        </div>
                        <span class="brand"></span>
                    </div>
                    <!--/.nav-collapse -->
                </div>
            </div>
        </div>
        <div class="container">
            <% if (!IsEdit)
               { %>
            <div class="row-fluid">
                <div class="col-md-2">
                    <img style="padding: 5px;" class="asoplogo img-rounded" src="/Content/Parts/MusicCompetition/v3-res/images/ASOP2013_logo_notification.png" width="239" />
                </div>
                <div class="col-md-10">
                    <h1>Judges Scoring System</h1>
                    <p>
                        Use this page to enter the Songs' and Interpreters' scores. Click an entry title or image to view details.<br />
                        Click the <strong>Edit Scores</strong> button to enter scores and click the <strong>Save Scores</strong> button to reflect the entered values.<br />
                        All scores must be entered <strong>in scale 0 to 100</strong>. The system will automatically consider the weight of each criteria.
                    </p>
                </div>
            </div>
            <% }
               else
               { %>
            All scores must be entered <strong>in scale 0 to 100</strong>. The system will automatically consider the weight of each criteria.
            <% } %>

            <%--<p>&nbsp;</p>--%>
            <p style="text-align: right;">
                <% if (!IsLocked)
                   { %>
                <%--<a runat="server" id="linkRefresh" href="#" class="btn btn-primary">Refresh</a>--%>
                <a runat="server" id="linkEdit" href="#" class="btn btn-warning"><i class="icon-edit icon-white"></i>&nbsp;Edit Scores</a>
                <button runat="server" id="cmdUpdate" onserverclick="cmdUpdate_ServerClick" class="btn btn-success"><i class="icon-ok icon-white"></i>&nbsp;Save Scores</button>
                <a runat="server" id="linkCancel" href="#" class="btn btn-default">Cancel</a>
                <% }
                   else
                   { %>
                <code><strong>Scores: LOCKED</strong></code>
                <% } %>
            </p>
            <ul class="nav nav-tabs">
                <li <%= string.IsNullOrEmpty(View) ? "class='active'" : "" %>>
                    <a runat="server" id="linkSong" href="#"><strong>Best Song</strong></a>
                </li>
                <li <%= View.Equals("Interpreter") ? "class='active'" : "" %>>
                    <a runat="server" id="linkInterpreter" href="#"><strong>Best Interpreter</strong></a>
                </li>
            </ul>
            <asp:MultiView ID="MultiViewMain" runat="server" ActiveViewIndex="0">
                <asp:View ID="viewSong" runat="server">
                    <asp:MultiView ID="MultiViewSong" runat="server" ActiveViewIndex="0">
                        <asp:View ID="viewSongShow" runat="server">
                            <table class="table table-striped table-hover table-condensed">
                                <thead>
                                    <tr>
                                        <th class="center">#</th>
                                        <th style="width: 100px">&nbsp;</th>
                                        <th>SONG</th>
                                        <th class="center">MUSICALITY (30%)</th>
                                        <th class="center">LYRICS (40%)</th>
                                        <th class="center">OVER-ALL IMPACT (30%)</th>
                                        <th class="center">TOTAL (100%)</th>
                                    </tr>
                                </thead>
                                <asp:Repeater ID="Repeater3" runat="server" DataSourceID="ObjectDataSource1">
                                    <ItemTemplate>
                                        <tr data-entrynumber='<%# Eval("Id") %>'>
                                            <td class="center"><%# Eval("Index") %></td>
                                            <td><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                <img src="<%=IntegrationConstants.MCBasePath %><%= hCompetitionId.Value %>/Photos/thumb/<%# Eval("EvalPhotoFile") %>" class="entry-thumb" width="100" alt="" /></a>
                                            </td>
                                            <td><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                <span style="font-weight: bold; color: #DA4F49;"><%# Eval("Entry") %></span></a><br />
                                                COMPOSER/LYRICIST:&nbsp;<%# Eval("Name") %><br />
                                                INTERPRETER:&nbsp;<%# Eval("Interpreter") %></td>
                                            <td class="center"><%# Eval("Musicality") %></td>
                                            <td class="center"><%# Eval("LyricsMessage") %></td>
                                            <td class="center"><%# Eval("OverallImpact") %></td>
                                            <th class="center"><%# Eval("Total") %></th>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </asp:View>
                        <asp:View ID="viewSongEdit" runat="server">
                            <div>
                                <table class="table table-striped table-hover table-condensed" style="margin-bottom: 0">
                                    <thead>
                                        <tr>
                                            <th class="center entry-number">#</th>
                                            <th class="entry-thumb-edit">&nbsp;</th>
                                            <th class="entry-name">SONG</th>
                                            <th class="center entry-score">MUSICALITY (30%)</th>
                                            <th class="center entry-score">LYRICS (40%)</th>
                                            <th class="center entry-score">OVER-ALL IMPACT (30%)</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <div class="entry-edit-area">
                                <table class="table table-striped table-hover table-condensed" style="margin-bottom: 0">
                                    <%--<thead>
                                    <tr>
                                        <th class="center">#</th>
                                        <th class="entry-thumb-edit">&nbsp;</th>
                                        <th>SONG</th>
                                        <th class="center">MUSICALITY (30%)</th>
                                        <th class="center">LYRICS (40%)</th>
                                        <th class="center">OVER-ALL IMPACT (30%)</th>
                                    </tr>
                                </thead>--%>
                                    <asp:Repeater ID="Repeater4" runat="server" DataSourceID="ObjectDataSource1">
                                        <ItemTemplate>
                                            <tr data-entrynumber='<%# Eval("Id") %>'>
                                                <td class="center entry-number"><%# Eval("Index") %></td>
                                                <td class="entry-thumb-edit"><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                    <img src="<%=IntegrationConstants.MCBasePath %><%= hCompetitionId.Value %>/Photos/thumb/<%# Eval("EvalPhotoFile") %>" class="entry-thumb-edit" alt="" /></a>
                                                </td>
                                                <td class="entry-name"><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                    <span style="font-weight: bold; color: #DA4F49;"><%# Eval("Entry") %></span></a><br />
                                                    COMPOSER/LYRICIST:&nbsp;<%# Eval("Name") %><br />
                                                    INTERPRETER:&nbsp;<%# Eval("Interpreter") %></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_mu_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("Musicality") %>' /></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_lm_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("LyricsMessage") %>' /></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_oi_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("OverallImpact") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </asp:View>
                <asp:View ID="viewInterpreter" runat="server">
                    <asp:MultiView ID="MultiViewInterpreter" runat="server" ActiveViewIndex="0">
                        <asp:View ID="viewInterpreterShow" runat="server">
                            <table class="table table-striped table-hover table-condensed">
                                <thead>
                                    <tr>
                                        <th class="center">#</th>
                                        <th style="width: 100px">&nbsp;</th>
                                        <th>INTERPRETER</th>
                                        <th class="center">VOICE QUALITY (40%)</th>
                                        <th class="center">INTERPRETATION (30%)</th>
                                        <th class="center">STAGE PRESENCE (20%)</th>
                                        <th class="center">OVER-ALL IMPACT (10%)</th>
                                        <th class="center">TOTAL (100%)</th>
                                    </tr>
                                </thead>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource2">
                                    <ItemTemplate>
                                        <tr data-entrynumber='<%# Eval("Id") %>'>
                                            <td class="center"><%# Eval("Index") %></td>
                                            <td><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                <img src="<%=IntegrationConstants.MCBasePath %><%= hCompetitionId.Value %>/Photos/thumb/<%# Eval("EvalPhotoFile") %>" class="entry-thumb" width="100" alt="" /></a>
                                            </td>
                                            <td><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                <span style="font-weight: bold; color: #DA4F49;"><%# Eval("Interpreter") %></span></a><br />
                                                SONG TITLE:&nbsp;<%# Eval("Entry") %><br />
                                                COMPOSER/LYRICIST:&nbsp;<%# Eval("Name") %></td>
                                            <td class="center"><%# Eval("VoiceQuality") %></td>
                                            <td class="center"><%# Eval("Interpretation") %></td>
                                            <td class="center"><%# Eval("StagePresence") %></td>
                                            <td class="center"><%# Eval("OverallImpact") %></td>
                                            <th class="center"><%# Eval("Total") %></th>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </asp:View>
                        <asp:View ID="viewInterpreterEdit" runat="server">
                            <div>
                                <table class="table table-striped table-hover table-condensed" style="margin-bottom: 0">
                                    <thead>
                                        <tr>
                                            <th class="center entry-number">#</th>
                                            <th class="entry-thumb-edit">&nbsp;</th>
                                            <th class="entry-name">INTERPRETER</th>
                                            <th class="center entry-score">VOICE QUALITY (40%)</th>
                                            <th class="center entry-score">INTERPRETATION (30%)</th>
                                            <th class="center entry-score">STAGE PRESENCE (20%)</th>
                                            <th class="center entry-score">OVER-ALL IMPACT (10%)</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <div class="entry-edit-area">
                                <table class="table table-striped table-hover table-condensed" style="margin-bottom: 0">
                                    <%--<thead>
                                    <tr>
                                        <th class="center">#</th>
                                        <th class="entry-thumb-edit">&nbsp;</th>
                                        <th>INTERPRETER</th>
                                        <th class="center">VOICE QUALITY (40%)</th>
                                        <th class="center">INTERPRETATION (30%)</th>
                                        <th class="center">STAGE PRESENCE (20%)</th>
                                        <th class="center">OVER-ALL IMPACT (10%)</th>
                                    </tr>
                                </thead>--%>
                                    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="ObjectDataSource2">
                                        <ItemTemplate>
                                            <tr data-entrynumber='<%# Eval("Id") %>'>
                                                <td class="center entry-number"><%# Eval("Index") %></td>
                                                <td class="entry-thumb-edit"><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                    <img src="<%=IntegrationConstants.MCBasePath %><%= hCompetitionId.Value %>/Photos/thumb/<%# Eval("EvalPhotoFile") %>" class="entry-thumb-edit" alt="" /></a>
                                                </td>
                                                <td class="entry-name"><a href="<%=FinalistUrl %>?finalist=<%# Eval("Id") %>#entrydetails" target="_blank">
                                                    <span style="font-weight: bold; color: #DA4F49;"><%# Eval("Interpreter") %></span></a><br />
                                                    SONG TITLE:&nbsp;<%# Eval("Entry") %><br />
                                                    COMPOSER/LYRICIST:&nbsp;<%# Eval("Name") %></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_vq_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("VoiceQuality") %>' /></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_in_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("Interpretation") %>' /></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_sp_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("StagePresence") %>' /></td>
                                                <td class="center entry-score">
                                                    <input name='__txt_oi_<%# Eval("Id") %>' class="input-mini" type="text" value='<%# Eval("OverallImpact") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </asp:View>
            </asp:MultiView>
            <p style="text-align: right">
                <% if (!IsLocked)
                   { %>
                <a runat="server" id="linkEdit2" href="#" class="btn btn-warning"><i class="icon-edit icon-white"></i>&nbsp;Edit Scores</a>
                <button runat="server" id="cmdUpdate2" onserverclick="cmdUpdate_ServerClick" class="btn btn-success"><i class="icon-ok icon-white"></i>&nbsp;Save Scores</button>
                <a runat="server" id="linkCancel2" href="#" class="btn btn-default">Cancel</a>
                <% }
                   else
                   { %>
                <code><strong>Scores: LOCKED</strong></code>
                <% } %>
            </p>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSongScores"
                TypeName="WCMS.WebSystem.Apps.MusicCompetition.MCJudgeV2">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hCompetitionId" DefaultValue="-1" Name="competitionId" PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetInterpreterScores"
                TypeName="WCMS.WebSystem.Apps.MusicCompetition.MCJudgeV2">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hCompetitionId" DefaultValue="-1" Name="competitionId" PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>

    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        ExecuteSessionCheck(location.href, 3 * 60 * 1000);
    </script>
</body>
</html>
