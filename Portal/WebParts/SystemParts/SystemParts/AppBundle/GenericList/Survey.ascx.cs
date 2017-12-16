namespace WCMS.WebSystem.WebParts.GenericForm
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    /// <summary>
    ///		Summary description for Survey.
    /// </summary>
    public partial class Survey : System.Web.UI.UserControl
    {
        private int sSID;
        private int sRID;

        //private string sPRE_QUESTION;
        //private string sPRE_PID;
        //private string sPRE_RANK;
        private string sSURVEY_ACTION;

        //private string sRANK;
        //private string sQUESTION_ID;
        private int sPID;
        //private string sCHOICES;

        //private string sMAX;
        //private string sSTATE_INFO;


        private void Page_Load(object sender, System.EventArgs e)
        {
            WContext query = new WContext(this);

            sSID = query.GetId("ListId");

            //sPRE_PID = Request.Form["__PAGE_ID"];
            string sPRE_PID = null;

            object obj = ViewState["__PAGE_ID"];
            if (obj != null)
            {
                sPRE_PID = obj.ToString();
            }

            //sPRE_RANK = Request.Form["__RANK"];
            WContext context = new WContext(this);
            //ControlInfo ci = new ControlInfo(qs["ID"]);
            //string sLocation = ci.Key(ControlInfoEnum.LocationType);
            //int iID = int.Parse(ci.Key(ControlInfoEnum.ItemID));

            obj = SqlHelper.ExecuteScalar("GenericListLink_Get",
                new SqlParameter("@RecordId", context.RecordId),
                new SqlParameter("@ObjectId", context.ObjectId));
            if (obj != null)
            {
                // START: GET SURVEY ID #####################
                if (sSID < 1)
                {
                    // GET IT DIRECTLY TO THE DATABASE IF NOT IN QS
                    sSID = DataHelper.GetId(obj);
                }
                // END: GET SURVEY ID #####################
            }
            else
            {
                // NO SURVEY FOUND;
                return;
            }

            if (!Page.IsPostBack)
            {
                //Session.Timeout = 60;

                // INIT BUTTONS
                cmdNext.Attributes.Add("onclick", "SurveyNext();");
                cmdRestart.Attributes.Add("onclick", "SurveyRestart();");

                obj = SqlHelper.ExecuteScalar("GenericListPartition_GetNext",
                    new SqlParameter("@ListId", sSID));

                sPID = DataHelper.GetId(obj);

                // FIRST TIME (FIRST PAGE)
                using (SqlDataReader r = SqlHelper.ExecuteReader("GenericListColumn_GetPartition",
                          new SqlParameter("@PartitionId", sPID)))
                {
                    this.LoadQuestions(r);
                }
            }
            else
            {
                // NEXT PAGE OR BACK (CHECK ACTION)
                sSURVEY_ACTION = Request.Form["__SURVEY_ACTION"];

                //lblMessage.Text = sSURVEY_ACTION;

                switch (sSURVEY_ACTION)
                {
                    case "NEXT":
                        // START: GET SURVEY ID
                        sRID = query.GetId("_R" + sSID);
                        if (sRID < 1)
                        {
                            obj = ViewState["_R" + sSID];
                            if (obj == null)
                            {
                                int isCompleted = 0;

                                obj = SqlHelper.ExecuteScalar("GenericListRow_Set",
                                    new SqlParameter("@ListId", sSID),
                                    new SqlParameter("@IsCompleted", isCompleted)
                                    );

                                sRID = DataHelper.GetId(obj);
                                ViewState["_R" + sSID] = sRID;
                            }
                            else
                            {
                                sRID = DataHelper.GetId(obj);
                            }
                        }
                        // END: GET SURVEY ID

                        // GET ANSWERS
                        using (SqlDataReader r = SqlHelper.ExecuteReader("GenericListColumn_GetPartition",
                                  new SqlParameter("@PartitionId", int.Parse(sPRE_PID))
                                  ))
                        {
                            while (r.Read())
                            {
                                bool isAnswerNull = false;
                                int iQuestionID = (int)r["Id"];
                                string sAnswer = Request.Form["__ANSWER" + iQuestionID.ToString()];


                                // CHECK IF QUESTION HAS ANSWER
                                if (r["IsRequired"].ToString() == "1")
                                {
                                    if (string.IsNullOrEmpty(sAnswer))
                                    {
                                        isAnswerNull = true;
                                    }
                                    else
                                    {
                                        sAnswer = sAnswer.Trim();
                                        if (sAnswer == string.Empty)
                                        {
                                            isAnswerNull = true;
                                        }
                                    }

                                    if (isAnswerNull)
                                    {
                                        // NO ANSWER
                                        //string sSTATE_INFO = "<INPUT TYPE=\"HIDDEN\" NAME=\"__PAGE_ID\" VALUE=\"" + sPRE_PID + "\">";
                                        //sSTATE_INFO += "<INPUT TYPE=\"HIDDEN\" NAME=\"__RANK\" VALUE=\"" + sPRE_RANK + "\">";
                                        //lStateInfo.Text = sSTATE_INFO;
                                        ViewState["__PAGE_ID"] = sPRE_PID;

                                        lblMessage.Text = "Please fill-up all required fields.";

                                        // DISPLAY MESSAGE
                                        return;
                                    }
                                }

                                SqlHelper.ExecuteNonQuery("GenericListField_Set",
                                    new SqlParameter("@RowId", sRID),
                                    new SqlParameter("@ColumnId", iQuestionID),
                                    new SqlParameter("@Answer", sAnswer)
                                    );
                            }
                        }

                        // NEXT SET OF QUESTIONS
                        obj = SqlHelper.ExecuteScalar("GenericListPartition_GetNext",
                            new SqlParameter("@PrePartitionId", int.Parse(sPRE_PID)),
                            new SqlParameter("@ListId", sSID)
                            );

                        if (obj != null)
                        {
                            sPID = DataHelper.GetId(obj);

                            using (SqlDataReader r = SqlHelper.ExecuteReader("GenericListColumn_GetPartition",
                                      new SqlParameter("@PartitionId", sPID)))
                            {
                                this.LoadQuestions(r);
                            }
                        }
                        else
                        {
                            /* END OF SURVEY */

                            SqlHelper.ExecuteNonQuery("GenericListRow_Set",
                                new SqlParameter("@RowId", sRID),
                                new SqlParameter("@IsCompleted", 1)
                                );

                            var completionRedirect = query.Element.GetParameterValue("CompletionRedirect", null);

                            query["Open"] = string.IsNullOrEmpty(completionRedirect) ? "Finish" : completionRedirect;
                            //qs["SS"] = "SV";
                            query["ListId"] = sSID.ToString();
                            query.Redirect();
                        }
                        break;
                }
            }
        }

        private void LoadQuestions(SqlDataReader r)
        {
            const string sSELECT = "<tr><td align=\"left\"><select name=\"__ANSWER{QID}\">{SELECT}</select></td></tr>";

            //const string sT_Position = "{$P$}"; // QUESTION # TEMPLATE
            const string sT_Question = "{$Q$}"; // QUESTION TEXT TEMPLATE
            const string sT_Choices = "{$C$}"; // CHOICES TEMPLATE
            string sTemplate = lTemplate.Text; // SURVEY TEMPLATE
            string sTemplateH = lTemplateH.Text;

            lblMessage.Text = string.Empty;

            // SETUP PAGE INFO
            using (SqlDataReader r2 = SqlHelper.ExecuteReader("GenericListPartition_Get",
                       new SqlParameter("@PartitionId", sPID)
                       ))
            {
                if (r2.Read())
                {
                    lTitle.Text = r2["SurveyTitle"].ToString();

                    try
                    {
                        if ((bool)r2["ShowPageCaption"])
                        {
                            lPage.Text = r2["Title"].ToString();
                        }
                    }
                    catch { }

                    //string sSTATE_INFO = "<INPUT TYPE=\"HIDDEN\" NAME=\"__PAGE_ID\" VALUE=\"" + sPID + "\">";
                    //sSTATE_INFO += "<INPUT TYPE=\"HIDDEN\" NAME=\"__RANK\" VALUE=\"" + r["Rank"].ToString() + "\">";
                    //lStateInfo.Text = sSTATE_INFO;

                    ViewState["__PAGE_ID"] = sPID;
                }
            }
            //

            //bool bStateInfo = false;
            string sQUESTIONS = string.Empty;

            // LOOP THROUGH ALL QUESTIONS IN A SURVEY PAGE
            while (r.Read())
            {
                string sQuestionID = r["Id"].ToString();
                string sRank = r["Rank"].ToString();
                bool isHoriz = false;

                try
                {
                    isHoriz = r["IsHorizontal"].ToString() == "1";
                }
                catch { }

                string sQuestion = (isHoriz) ? sTemplateH : sTemplate;

                // LOAD QUESTION TEXT
                string sQuestionText = r["Label"].ToString();
                if (r["IsRequired"].ToString() == "1")
                {
                    sQuestionText += "&nbsp;<font color=red>*</font>";
                }

                sQuestion = sQuestion.Replace(sT_Question, sQuestionText);
                //lblQuestion.Text = sQuestionText;

                // RANK

                // LOAD CHOICES
                bool isSelect = false;

                string sCHOICES = string.Empty; // "<TABLE border=0 cellpadding=0 cellspacing=0>";
                using (SqlDataReader r2 = SqlHelper.ExecuteReader("GenericListColumnOption_GetChoices",
                           new SqlParameter("@ColumnId", int.Parse(sQuestionID))))
                {
                    while (r2.Read())
                    {
                        string sChoice = r2["Template"].ToString().Replace("{CAPTION}", r2["Caption"].ToString());
                        sChoice = sChoice.Replace("{QID}", sQuestionID);
                        sCHOICES += sChoice;

                        if ((int)r2["OptionTypeId"] == 9 && !isSelect)
                        {
                            isSelect = true;
                        }
                    }
                }

                // OPTIONS ARE SELECT BOX
                if (isSelect)
                {
                    sCHOICES = sSELECT.Replace("{QID}", sQuestionID).Replace("{SELECT}", sCHOICES);
                }
                sCHOICES = "<TABLE border=0 cellpadding=0 cellspacing=0>" + sCHOICES;
                sCHOICES += "</TABLE>";

                sQuestion = sQuestion.Replace(sT_Choices, sCHOICES);

                // SET PAGE INFO

                sQUESTIONS += sQuestion;
            }

            lQuestions.Text = sQUESTIONS;
        }
    }
}
