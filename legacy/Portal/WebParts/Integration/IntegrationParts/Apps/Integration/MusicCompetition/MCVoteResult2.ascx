<%@ Control Language="C#" ClassName="WCMS.WebSystem.Apps.MusicCompetition.MCVoteResult2"
    AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Import Namespace="WCMS.WebSystem.WebParts.Registration" %>
<script runat="server">

    public string VoteUrl { get; set; }
    public string EventTitle { get; set; }
    public string ShortEventTitle { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var set = WebParameterSet.Get("ASOP Nomination");

            VoteUrl = set.GetParameterValue("Vote-Url");

            var context = new WebPartContext(this);
            var paramObj = context.ParameterizedObject;

            EventTitle = paramObj.GetParameterValue("EventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");
            ShortEventTitle = paramObj.GetParameterValue("ShortEventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");

            var votingEnded = DataHelper.GetBool(paramObj.GetParameterValue("VotingEnded"), false);
            if (votingEnded)
            {
                lblVotingEnded.Visible = true;
                panelVoteAgain.Visible = false;
            }
        }
    }

    public string ComputePercentage(int total, int count)
    {
        double per = 0;

        if (count > 0)
        {
            per = (count / (double)total) * 100;
        }

        return per.ToString("0.00") + "%";
    }
    
</script>
<!DOCTYPE html>
<html>
<head>
    <title><% =EventTitle %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.css" />
    <link rel="stylesheet" href="/Content/Parts/MusicCompetition/res/css/style.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.js"></script>
    <script type="text/javascript">
        $("#pageResult").live("pageinit", function (event) {
            $("#buttonVoteAgain").click(function () {
                location.href = "<% =VoteUrl %>";
            });
        });
    </script>
</head>
<body>
    <div data-role="page" id="pageResult">
        <div data-role="header" data-theme="e">
            <h1><% =ShortEventTitle %></h1>
        </div>
        <div data-role="content">
            <h1 runat="server" id="lblVotingEnded" visible="false" style="text-align: center; color: orange;">{{ VOTING HAS ENDED }}<br /><br /></h1>
            <h1 style="text-align: center"><% =ShortEventTitle %></h1>
            <div>
                <table cellspacing="0" cellpadding="4" style="color: #333333; width: 100%; border-collapse: collapse;">
                    <tr align="left" style="color: White; background-color: #507CD1; font-weight: bold;">
                        <th style="text-align: left" scope="col">Song</th>
                        <%--<th style="text-align: left" scope="col">Composer / Interpreter</th>--%>
                        <%--<th style="text-align: right" scope="col">Total Votes</th>
                        <th style="text-align: right" scope="col">Official Votes</th>--%>
                        <th style="text-align: right" scope="col">Official Votes %</th>
                    </tr>
                    <% 
                        var candidates = MCCandidate.Provider.GetList();
                        var totalCandidates = candidates.Count();
                        var votes = MCVote.Provider.GetList();
                        var totalUnconfirmed = votes.Count();
                        var totalConfirmed = votes.Count(i => i.Status == 1);
                        char candidateNum = 'A';

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
                                      };

                        foreach (var result in results)
                        {
                    %>
                    <tr style="background-color: #EFF3FB;">
                        <td><% = "Song " + candidateNum++ %></td>
                        <%--<td><% =result.Entry %></td>--%>
                        <%--<td><% =result.Name %> / <% =result.Interpreter %></td>--%>
                        <%--<td style="text-align: right"><% = result.UnconfirmedVotes %></td>
                        <td style="text-align: right"><% = result.ConfirmedVotes %></td>--%>
                        <%--<td style="text-align: right"><% = ComputePercentage(totalUnconfirmed, result.UnconfirmedVotes) %></td>--%>
                        <td style="text-align: right; font-weight: bold"><% = ComputePercentage(totalConfirmed, result.ConfirmedVotes) %></td>
                    </tr>
                    <%
                        }
                    %>
                </table>
            </div>
            <p runat="server" id="panelVoteAgain" visible="true">
                <a href="#" id="buttonVoteAgain" data-role="button" data-ajax="false" data-inline="true" data-icon="refresh"
                    data-iconpos="right" data-theme="b">Vote again!</a>
            </p>
            <p>
                <em>NOTE:</em>&nbsp;Please note that this voting system will not affect any scores
                during the finals night. The decision of the board of judges is final.
            </p>
        </div>
        <div data-role="footer">
            <h4><% =ShortEventTitle %></h4>
        </div>
    </div>
</body>
</html>
