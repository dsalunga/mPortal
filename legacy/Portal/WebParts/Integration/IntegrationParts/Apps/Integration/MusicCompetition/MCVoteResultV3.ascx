<%@ Control Language="C#" ClassName="WCMS.WebSystem.Apps.MusicCompetition.MCVoteResultV3"
    AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>

<script runat="server">

    public string VoteUrl { get; set; }
    public string EventTitle { get; set; }
    public string ShortEventTitle { get; set; }

    public bool LimitedMode { get; set; }
    public int CompetitionId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var set = WebParameterSet.Get("ASOP Nomination");

            VoteUrl = set.GetParameterValue("Vote-Url");

            var context = new WContext(this);
            var paramObj = context.ParameterizedObject;

            EventTitle = paramObj.GetParameterValue("EventTitle", "ASOP Asia & Oceania - People's Choice Nomination");
            ShortEventTitle = paramObj.GetParameterValue("ShortEventTitle", "ASOP Asia & Oceania - People's Choice Nomination");

            var votingEnded = DataHelper.GetBool(paramObj.GetParameterValue("VotingEnded"), false);
            if (votingEnded)
            {
                lblVotingEnded.Visible = true;
                panelVoteAgain.Visible = false;
            }

            CompetitionId = DataHelper.GetId(paramObj.GetParameterValue("CompetitionId"));
            LimitedMode = DataHelper.GetBool(paramObj.GetParameterValue("LimitedMode"), false);
            MultiView1.SetActiveView(LimitedMode ? ViewLimitedMode : ViewFullMode);
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
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = 1024" />

    <title><%= EventTitle %></title>

    <!--[if gte IE 9]>
      <style type="text/css">
        .gradient {
           filter: none;
        }
      </style>
    <![endif]-->

    <link href="/Content/Parts/MusicCompetition/v3-res/css/vote.css" type="text/css" rel="stylesheet" />

    <script src="/Content/Assets/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Content/Parts/MusicCompetition/v3-res/js/vote.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#panelVoteAgain").click(function () {
                location.href = "<%= VoteUrl %>";
            });

            $('#logo').animate({ height: '140px', width: '200px' }, 'fast');
            $('#viewresults').slideToggle('fast');
        });
    </script>

    <style type="text/css">
        body {
            background: #003427 url(/Content/Parts/MusicCompetition/v3-res/img-vote/bg_header.jpg) no-repeat;
            background-size: 100%, 100%;
            height: 100%;
            width: 1024px;
            margin: auto;
            padding: inherit;
            font-family: Arial, Verdana, Helvetica, sans-serif;
            /*font-weight: 700;*/
            color: white;
            font-size: 14px;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Parts/MusicCompetition/v3-res/img-vote/bg_header.jpg', sizingMethod='scale');
            -ms-filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Parts/MusicCompetition/v3-res/img-vote/bg_header.jpg', sizingMethod='scale')";
        }
    </style>
</head>
<body>
    <div id="container">
        <div id="logo" style="height: 140px">
            <img id="logoimage" src="/Content/Parts/MusicCompetition/v3-res/img-vote/asoplogo.png" alt="" width="260" />
        </div>

        <div id="darkcover"></div>

        <!-- Results -->
        <div id="viewresults" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 runat="server" id="lblVotingEnded" visible="false" class="shadow2 roundcorner2" style="text-align: center; color: red;">
                <br />
                <span class="textshadow1">{{ VOTING HAS ENDED }}</span>
                <br />
                <br />
            </h1>
            <h1 class="shadow2 roundcorner2"><span class="textshadow1"><%= ShortEventTitle %></span></h1>
            <%
                var candidates = MCCandidate.Provider.GetList(CompetitionId);
                var totalCandidates = candidates.Count();
                var votes = MCVote.Provider.GetList(CompetitionId);
                var totalUnconfirmed = votes.Count();
                var totalConfirmed = votes.Count(i => i.Status == 1);
            %>
            <div id="categories">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="ViewFullMode" runat="server">
                        <%--<th style="text-align: left" scope="col">Song</th>
                                <th style="text-align: left" scope="col">Composer / Interpreter</th>
                                <th style="text-align: right" scope="col">Total Votes</th>
                                <th style="text-align: right" scope="col">Official Votes</th>
                                <th style="text-align: right" scope="col">Official Votes %</th>--%>
                        <%--<td style="text-align: right"><%=  result.ConfirmedVotes %></td>--%>
                        <% 
                            var candidates = MCCandidate.Provider.GetList(CompetitionId);
                            var totalCandidates = candidates.Count();
                            var votes = MCVote.Provider.GetList(CompetitionId);
                            var totalUnconfirmed = votes.Count();
                            var totalConfirmed = votes.Count(i => i.Status == 1);
                            
                            var results = from item in candidates
                                          orderby item.Entry
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

                        %>
                        <div class="song_entry category">
                            Song
                            <div class="entrylist">
                                <% foreach (var result in results)
                                   { %>
                                <div><%= result.Entry %></div>
                                <% } %>
                            </div>
                        </div>
                        <div class="composer_entry category">
                            Composer / Interpreter
                            <div class="entrylist">
                                <% foreach (var result in results)
                                   { %>
                                <div><%= result.Name %> / <%= result.Interpreter %></div>
                                <% } %>
                            </div>
                        </div>
                        <div class="totalvotes_entry category">
                            Official Votes
                            <div class="entrylist">
                                <% foreach (var result in results)
                                   { %>
                                <div><%=  result.ConfirmedVotes %></div>
                                <% } %>
                            </div>
                        </div>
                        <div class="officialvotes_entry category">
                            Official Votes %
                            <div class="entrylist">
                                <% foreach (var result in results)
                                   { %>
                                <div><%=  ComputePercentage(totalConfirmed, result.ConfirmedVotes) %></div>
                                <% } %>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="ViewLimitedMode" runat="server">
                        <% 
                            var candidates = MCCandidate.Provider.GetList(CompetitionId);
                            var totalCandidates = candidates.Count();
                            var votes = MCVote.Provider.GetList(CompetitionId);
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
                        %>
                        <div class="song_entry2 category">
                            Song
                                <div class="entrylist">
                                    <% foreach (var result in results)
                                       { %>
                                    <div><%=  "Song " + candidateNum++ %></div>
                                    <% } %>
                                </div>
                        </div>
                        <div class="totalvotes_entry2 category">
                            Official Votes
                            <div class="entrylist">
                                <% foreach (var result in results)
                                   { %>
                                <div><%=  result.ConfirmedVotes %></div>
                                <% } %>
                            </div>
                        </div>
                        <div class="officialvotes_entry2 category">
                            Official Votes %
                            <div class="entrylist">
                                <% foreach (var result in results)
                                   { %>
                                <div><%=  ComputePercentage(totalConfirmed, result.ConfirmedVotes) %></div>
                                <% } %>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div>
                Total Votes: <%= totalUnconfirmed %>, Total Confirmed: <%= totalConfirmed %>
            </div>
            <br />
            <div runat="server" id="panelVoteAgain" clientidmode="static" visible="true" class="gbutton loginpad roundcorner1 border1 shadow2">
                <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Vote again!</span>
            </div>
            <p>
                <em>NOTE:</em>&nbsp;Please note that this voting system will not affect any scores
                during the finals night. The decision of the board of judges is final.
            </p>
        </div>
    </div>
</body>
</html>
