<%@ Control Language="C#" ClassName="WCMS.WebSystem.Apps.MusicCompetition.MCVoteResultV4"
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
    public WQuery Query = null;

    public bool IsVoteMasked { get; set; }
    public bool IsVoteLocked { get; set; }
    public DateTime CompetitionDate { get; set; }
    public int CompetitionId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var context = new WContext(this);
            var element = context.Element;

            var paramObj = context.GetParameterSet();
            //if (paramObj == null)
            //    paramObj = WebParameterSet.Get("ASOP Nomination");

            VoteUrl = ParameterizedWebObject.GetValue("VoteUrl", element, paramObj);
            EventTitle = ParameterizedWebObject.GetValue("EventTitle", "People's Choice Results", element, paramObj);
            ShortEventTitle = ParameterizedWebObject.GetValue("ShortEventTitle", "People's Choice Results", element, paramObj);

            Query = new WQuery();
            Query.BasePath = VoteUrl;

            CompetitionId = DataHelper.GetId(ParameterizedWebObject.GetValue("CompetitionId", element, paramObj));
            if (CompetitionId > 0)
            {
                var competition = MCCompetition.Provider.Get(CompetitionId);

                //LimitedMode = competition.IsVoteMasked; DataHelper.GetBool(ParameterizedWebObject.GetValue("LimitedMode", element, paramObj), false);
                IsVoteMasked = competition.IsVoteMasked;
                IsVoteLocked = competition.IsVoteLocked;
                CompetitionDate = competition.CompetitionDate;
                MultiView1.SetActiveView(competition.IsVoteMasked ? ViewLimitedMode : ViewFullMode);

                if (IsVoteLocked)
                {
                    lblVotingEnded.Visible = true;
                    //panelVoteAgain.Visible = false;
                }
            }
        }
    }

    public string ComputePercentage(int total, int count)
    {
        double per = 0;
        if (count > 0)
            per = (count / (double)total) * 100;

        return per.ToString("0.00") + "%";
    }
    
</script>
<%--<script type="text/javascript">
    $("#pageResult").live("pageinit", function (event) {
        $("#buttonVoteAgain").click(function () {
            location.href = "<%= VoteUrl %>";
        });
    });
</script>--%>
<% if (IsVoteLocked)
   { %>
<div><a style="margin-left: 0" href="<%=VoteUrl %>">Click here to view The Finalists</a></div>
<% }
   else if (IsVoteMasked)
   { %>
<div><a style="margin-left: 0" href="<%=VoteUrl %>">Click here to begin casting your vote</a></div>
<% }
   else
   { %>
<div><a style="margin-left: 0" href="<%=VoteUrl %>">Click here or click on an entry to cast your vote</a></div>
<% } %>
<br />
<h3 runat="server" id="lblVotingEnded" visible="false" style="text-align: center; color: orange; margin-bottom: 20px">{{ Voting Has Ended }}<br />
</h3>
<h3><%= ShortEventTitle %></h3>
<div class="vote_board">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewFullMode" runat="server">
            <strong>
                <ul class="vote_list">
                    <li>Title of Song</li>
                    <li>Composer and Interpreter</li>
                    <li class="text-center">Total Votes</li>
                    <%--<li>Official Votes</li>--%>
                </ul>
            </strong>
            <% 
                var candidates = MCCandidate.Provider.GetList(CompetitionId);
                var totalCandidates = candidates.Count();
                var votes = MCVote.Provider.GetList(CompetitionId);
                //var totalUnconfirmed = votes.Count();
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
                                  ConfirmedVotes = votes.Count(i => i.CandidateId == item.Id && i.Status == 1)
                                  //UnconfirmedVotes = votes.Count(i => i.CandidateId == item.Id),
                              };

                foreach (var result in results)
                {
                    var entryName = string.IsNullOrEmpty(result.Interpreter) || result.Name.Equals(result.Interpreter, StringComparison.InvariantCultureIgnoreCase) ?
                        result.Name : string.Format("{0}<br/>{1}", result.Name, result.Interpreter);
            %>
            <ul class="vote_list">
                <a href="<%= Query.Set("finalist", result.Id).BuildQuery() %>#entrydetails">
                    <li><strong><%= result.Entry %></strong></li>
                    <li>
                        <%= entryName %>
                    </li>
                    <%--<li><%= result.UnconfirmedVotes %></li>--%>
                    <li class="text-center"><%=  result.ConfirmedVotes %></li>
                </a>
            </ul>
            <%
                }
            %>
        </asp:View>
        <asp:View ID="ViewLimitedMode" runat="server">
            <strong>
                <ul class="vote_list">
                    <li>Title of Song</li>
                    <li class="text-center">Official Votes %</li>
                </ul>
            </strong>
            <% 
                var candidates = MCCandidate.Provider.GetList(CompetitionId);
                var totalCandidates = candidates.Count();
                var votes = MCVote.Provider.GetList(CompetitionId);
                var showNames = IsVoteLocked && CompetitionDate.AddDays(1) < DateTime.Now;
                //var totalUnconfirmed = votes.Count();
                var totalConfirmed = votes.Count(i => i.Status == 1);
                char candidateNum = 'A';
                int candidateNum2 = 1;

                var results = from item in candidates
                              orderby votes.Count(i => i.CandidateId == item.Id && i.Status == 1) descending
                              select new
                              {
                                  item.Id,
                                  item.Name,
                                  item.Entry,
                                  item.Interpreter,
                                  item.Lyricist,
                                  ConfirmedVotes = votes.Count(i => i.CandidateId == item.Id && i.Status == 1)
                                  //UnconfirmedVotes = votes.Count(i => i.CandidateId == item.Id),
                              };

                foreach (var result in results)
                {
                    var entryName = string.IsNullOrEmpty(result.Interpreter) || result.Name.Equals(result.Interpreter, StringComparison.InvariantCultureIgnoreCase) ?
                        string.Format("{0}<br/>{1}", result.Entry, result.Name) : string.Format("{0}<br/>{1} / {2}", result.Entry, result.Name, result.Interpreter);
            %>
            <ul class="vote_list">
                <a href="<%= VoteUrl %>">
                    <li><strong><%= showNames ? entryName : "Song " + candidateNum++ %></strong></li>
                    <li class="text-center"><%= !showNames || candidateNum2 <=5 ? ComputePercentage(totalConfirmed, result.ConfirmedVotes) : "-" %></li>
                </a>
            </ul>
            <%
                    candidateNum2++;
                }
            %>
        </asp:View>
    </asp:MultiView>
</div>
<br />
<br />
<div>
    <em>NOTE:</em>&nbsp;
    <br />
    * Votes derived from any form of internet scripting, automation or use of disposable emails will be considered unofficial votes and subject for removal.
    <br />
    * This voting system will not affect any scores during the finals night. The decision of the board of judges is final.
</div>

<%--<h3>People's Choice Results</h3>--%>