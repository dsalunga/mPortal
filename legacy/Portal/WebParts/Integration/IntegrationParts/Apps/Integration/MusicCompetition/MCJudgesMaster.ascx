<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MCJudgesMaster.ascx.cs" Inherits="WCMS.WebSystem.Apps.MusicCompetition.MCJudgesMaster" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework.Utilities" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>
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
    </style>
    <link href="<%=WebUtil.Version("~/content/plugins/bootstrap/css/bootstrap-responsive.min.css")%>" rel="stylesheet" />

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
                            Welcome, <%= JudgeName %>!
                        </p>
                        <div runat="server" id="panelMenuSection">
                            <%--<ul class="nav">
                                <li><a href=".">Home</a></li>
                                <li><a href="Vote/" target="_blank">Voting Page</a></li>
                                <li><a href="Judging/" target="_blank">Judges Scoring</a></li>
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
            <div class="row-fluid">
                <div class="col-md-2">
                    <img style="padding: 5px;" class="asoplogo img-rounded" src="/Content/Parts/MusicCompetition/v3-res/images/ASOP2013_logo_notification.png" width="239" />
                </div>
                <div class="col-md-10">
                    <h1>Master Score Tabulation</h1>
                </div>
            </div>

            <p>&nbsp;</p>
            <p style="text-align: right;">
                <button runat="server" id="cmdLockUnlock" onserverclick="cmdLockUnlock_ServerClick" class='btn btn-success' title="Click to toggle status"><i class="icon-retweet icon-white"></i>&nbsp;Scores: <%= IsLocked ? "LOCKED": "OPEN" %></button>
            </p>
            <h3>Best Song</h3>
            <%
                if (CompetitionId == -1)
                    throw new Exception("CompetitionId not found in parameters.");

                var candidates = MCCandidate.Provider.GetList(CompetitionId).OrderBy(i => i.Rank);
                var songScores = MCSongScore.Provider.GetList(CompetitionId);
                var interpreterScores = MCInterpreterScore.Provider.GetList(CompetitionId);

                var songTotalScores = new List<MCCandidateTotalScore>();
                var interpreterTotalScores = new List<MCCandidateTotalScore>();

                // Cache Songs Score
                foreach (var candidate in candidates)
                {
                    double candidateScoreTotal = 0;
                    foreach (var judge in Judges)
                    {
                        var score = songScores.FirstOrDefault(s => s.CandidateId == candidate.Id && s.JudgeId == judge.Id);
                        var scoreTotal = score != null && (score.Musicality >= 0 && score.LyricsMessage >= 0 && score.OverallImpact >= 0) ?
                            (score.Musicality * 0.3 + score.LyricsMessage * 0.4 + score.OverallImpact * 0.3) : -1;

                        if (scoreTotal > 0)
                            candidateScoreTotal += scoreTotal;
                    }

                    songTotalScores.Add(new MCCandidateTotalScore(candidate, candidateScoreTotal / Judges.Count()));
                }

                // Cache Interpreters Score
                foreach (var candidate in candidates)
                {
                    double candidateScoreTotal = 0;
                    foreach (var judge in Judges)
                    {
                        var score = interpreterScores.FirstOrDefault(s => s.CandidateId == candidate.Id && s.JudgeId == judge.Id);
                        var scoreTotal = score != null && (score.VoiceQuality >= 0 && score.Interpretation >= 0 && score.StagePresence >= 0 && score.OverallImpact >= 0) ?
                             (score.VoiceQuality * 0.4 + score.Interpretation * 0.3 + score.StagePresence * 0.2 + score.OverallImpact * 0.1) : -1;

                        if (scoreTotal > 0)
                            candidateScoreTotal += scoreTotal;
                    }

                    interpreterTotalScores.Add(new MCCandidateTotalScore(candidate, candidateScoreTotal / Judges.Count()));
                }

                            // Song Scores
            %>
            <table class="table table-striped table-hover table-condensed table-bordered">
                <thead>
                    <tr>
                        <th class="center">#</th>
                        <th style="width: 100px">&nbsp;</th>
                        <th>SONG</th>
                        <% foreach (var judge in Judges)
                           { %>
                        <th class="center"><%= AccountHelper.GetPrefixedName(judge, true).ToUpper() %></th>
                        <% } %>
                        <th class="center">TOTAL</th>
                    </tr>
                </thead>
                <% 
                    int rank = 0;
                    foreach (var songTotalScore in songTotalScores.OrderByDescending(i => i.TotalScore))
                    {
                        var candidate = songTotalScore.Candidate;

                        rank++;
                        //double candidateScoreTotal = 0;
                %>
                <tr data-entrynumber='<%= candidate.Id %>'>
                    <td class="center"><%= rank %></td>
                    <td>
                        <img src="<%=IntegrationConstants.MCBasePath %><%=CompetitionId %>/Photos/thumb/<%= candidate.GetPhotoFile() %>" class="entry-thumb" width="100" alt="" />
                    </td>
                    <td>
                        <span style="font-weight: bold; color: #DA4F49;"><%= candidate.Entry %></span><br />
                        COMPOSER/LYRICIST:&nbsp;<%= candidate.Name %><br />
                        INTERPRETER:&nbsp;<%= candidate.Interpreter %></td>
                    <% foreach (var judge in Judges)
                       {
                           var score = songScores.FirstOrDefault(s => s.CandidateId == candidate.Id && s.JudgeId == judge.Id);
                           var scoreTotal = score != null && (score.Musicality >= 0 && score.LyricsMessage >= 0 && score.OverallImpact >= 0) ?
                               (score.Musicality * 0.3 + score.LyricsMessage * 0.4 + score.OverallImpact * 0.3) : -1;

                           /*if (scoreTotal > 0)
                               candidateScoreTotal += scoreTotal;*/
                    %>
                    <td class="center"><%= scoreTotal >= 0 ? scoreTotal.ToString("N2") : string.Empty %></td>
                    <% } %>
                    <th class="center"><%= songTotalScore.TotalScore.ToString("N2") + "%" %></th>
                </tr>
                <% } %>
            </table>
            <p>&nbsp;</p>
            <h3>Best Interpreter</h3>
            <table class="table table-striped table-hover table-condensed table-bordered">
                <thead>
                    <tr>
                        <th class="center">#</th>
                        <th style="width: 100px">&nbsp;</th>
                        <th>INTERPRETER</th>
                        <% foreach (var judge in Judges)
                           { %>
                        <th class="center"><%= AccountHelper.GetPrefixedName(judge, true).ToUpper() %></th>
                        <% } %>
                        <th class="center">TOTAL</th>
                    </tr>
                </thead>
                <% 
                    rank = 0;

                    foreach (var interpreterTotalScore in interpreterTotalScores.OrderByDescending(i => i.TotalScore))
                    {
                        var candidate = interpreterTotalScore.Candidate;
                        //double candidateScoreTotal = 0;

                        rank++;
                %>
                <tr data-entrynumber='<%= candidate.Id %>'>
                    <td class="center"><%= rank %></td>
                    <td>
                        <img src="<%=IntegrationConstants.MCBasePath %><%=CompetitionId %>/Photos/thumb/<%= candidate.GetPhotoFile() %>" class="entry-thumb" width="100" alt="" />
                    </td>
                    <td>
                        <span style="font-weight: bold; color: #DA4F49;"><%= candidate.Interpreter %></span><br />
                        SONG TITLE:&nbsp;<%= candidate.Entry %><br />
                        COMPOSER/LYRICIST:&nbsp;<%= candidate.Name %></td>
                    <% foreach (var judge in Judges)
                       {
                           var score = interpreterScores.FirstOrDefault(s => s.CandidateId == candidate.Id && s.JudgeId == judge.Id);
                           var scoreTotal = score != null && (score.VoiceQuality >= 0 && score.Interpretation >= 0 && score.StagePresence >= 0 && score.OverallImpact >= 0) ?
                                (score.VoiceQuality * 0.4 + score.Interpretation * 0.3 + score.StagePresence * 0.2 + score.OverallImpact * 0.1) : -1;

                           /*if (scoreTotal > 0)
                               candidateScoreTotal += scoreTotal;*/
                    %>
                    <td class="center"><%= scoreTotal >= 0 ? scoreTotal.ToString("N2") : string.Empty %></td>
                    <% } %>
                    <th class="center"><%= interpreterTotalScore.TotalScore.ToString("N2") + "%" %></th>
                </tr>
                <% } %>
            </table>
            <p>&nbsp;</p>
            <p style="text-align: right;">
                <button runat="server" id="cmdLockUnlockVotes" onserverclick="cmdLockUnlockVotes_ServerClick" class='btn btn-success' title="Click to toggle status"><i class="icon-retweet icon-white"></i>&nbsp;Votes: <%= VotesLocked ? "LOCKED": "OPEN" %></button>
                <button runat="server" id="cmdMaskUnmaskVotes" onserverclick="cmdMaskUnmaskVotes_ServerClick" class='btn btn-success' title="Click to toggle vote masking (hiding)"><i class="icon-retweet icon-white"></i>&nbsp;Votes: <%= VotesMasked ? "MASKED": "DISPLAYED" %></button>
            </p>
            <h3>People's Choice Results</h3>
            <table class="table table-striped table-hover table-condensed table-bordered">
                <thead>
                    <tr>
                        <th class="center">#</th>
                        <th style="width: 100px">&nbsp;</th>
                        <th>SONG</th>
                        <th class="center">Total Votes</th>
                        <th class="center">Official Votes</th>
                        <th class="center">Official Votes %</th>
                    </tr>
                </thead>
                <% 
                    var totalCandidates = candidates.Count();
                    var votes = MCVote.Provider.GetList(CompetitionId);
                    var totalUnconfirmed = votes.Count();
                    var totalConfirmed = votes.Count(i => i.Status == 1);

                    var results = from item in candidates
                                  orderby votes.Count(i => i.CandidateId == item.Id && i.Status == 1) descending
                                  select new
                                  {
                                      item.Id,
                                      item.Name,
                                      item.Entry,
                                      item.Interpreter,
                                      item.Lyricist,
                                      ConfirmedVotes = votes.Count(i => i.CandidateId == item.Id && i.Status == 1),
                                      UnconfirmedVotes = votes.Count(i => i.CandidateId == item.Id),
                                      PhotoFile = item.GetPhotoFile()
                                  };

                    rank = 0;

                    foreach (var result in results)
                    {
                        rank++;
                %>
                <tr>
                    <td class="center"><%= rank %></td>
                    <td>
                        <img src="<%=IntegrationConstants.MCBasePath %><%=CompetitionId %>/Photos/thumb/<%= result.PhotoFile %>" width="100" alt="" />
                    </td>
                    <td>
                        <span style="font-weight: bold; color: #DA4F49;"><%= result.Entry %></span><br />
                        COMPOSER/LYRICIST:&nbsp;<%= result.Name %><br />
                        INTERPRETER:&nbsp;<%= result.Interpreter %></td>
                    <td class="center"><%= result.UnconfirmedVotes %></td>
                    <td class="center"><%=  result.ConfirmedVotes %></td>
                    <th class="center"><%=  ComputePercentage(totalConfirmed, result.ConfirmedVotes) %></th>
                </tr>
                <% } %>
            </table>
            <div class="alert alert-success">
                <strong>Stats</strong> Total Votes: <%=totalUnconfirmed %>, Official Votes: <%=totalConfirmed %>
            </div>
        </div>
    </form>
    <!-- /container -->

    <!-- Placed at the end of the document so the pages load faster -->
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap/js/bootstrap.min.js")%>"></script>
</body>
</html>
