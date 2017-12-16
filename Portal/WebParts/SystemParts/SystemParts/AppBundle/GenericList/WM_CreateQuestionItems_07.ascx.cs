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
	///		Summary description for CreateQuestionItems.
	/// </summary>
	public partial class CreateQuestionItems : System.Web.UI.UserControl
	{
		private string sSID;
		private string sQID;
		private string sOID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			sSID = Request.QueryString["ListId"];
			sQID = Request.QueryString["ColumnId"];
			sOID = Request.QueryString["OptionId"];
			string sOTID = Request.QueryString["OptionTypeId"];

			if(!Page.IsPostBack)
			{
				cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete the selected items?');");

				// LOAD CONTROL TYPES
				using (SqlDataReader r = SqlHelper.ExecuteReader(CommandType.Text, "SELECT * FROM GenericListColumnOptionType ORDER BY Label"))
				{
					cboTypes.DataSource = r;
					cboTypes.DataTextField = "Label";
					cboTypes.DataValueField = "Id";
					cboTypes.DataBind();
				}
				try
				{
					cboTypes.SelectedValue = sOTID;
				}
				catch{}

				if(sOID!=null && sOID!=string.Empty)
				{
					using (SqlDataReader r = SqlHelper.ExecuteReader(CommandType.Text, "SELECT * FROM GenericListColumnOption WHERE Id=@OptionId ORDER BY Rank",
							   new SqlParameter("@OptionId", int.Parse(sOID))))
					{
						if(r.Read())
						{
							txtOptionID.Text = r["Id"].ToString();
							cboTypes.SelectedValue = r["OptionTypeId"].ToString();
							txtItemText.Text = r["Caption"].ToString();
							txtItemRanking.Text = r["Rank"].ToString();
						}
					}
				}
				else
				{
					txtOptionID.Text = "# Pending #";
				}

				if(sQID!=null && sQID!=string.Empty)
				{
					this.LoadData();
				}
			}
		}

		private void LoadData()
		{
            DataSet ds = SqlHelper.ExecuteDataSet("GenericListColumnOption_Get",
				new SqlParameter("@ColumnId", int.Parse(sQID)));

			grdOptions.DataSource = ds.Tables[0].DefaultView;
			grdOptions.DataBind();
		}

		protected void cmdDone_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			query.Remove("ColumnId");
			query.Remove("OptionId");
            query.LoadAndRedirect("WM_SurveyQuestions_05.ascx");
		}

        protected void cmdInsert_Click(object sender, System.EventArgs e)
		{
			string sOptionTypeID = cboTypes.SelectedValue;
			string sItemText = txtItemText.Text.Trim();
			int iRank = 0;
			int iDefaultValue = 0;

			try
			{
				iRank = int.Parse(txtItemRanking.Text.Trim());
			}
			catch{}

			if(sOID!=null && sOID!=string.Empty)
			{
				// UPDATE
                SqlHelper.ExecuteNonQuery("GenericListColumnOption_Set",
					new SqlParameter("@OptionId", int.Parse(sOID)),
					new SqlParameter("@OptionTypeId", int.Parse(sOptionTypeID)),
					new SqlParameter("@Rank", iRank),
					new SqlParameter("@Caption", sItemText),
					new SqlParameter("@DefaultValue", iDefaultValue)
					);
			}
			else
			{
				// INSERT
                SqlHelper.ExecuteNonQuery("GenericListColumnOption_Set",
					new SqlParameter("@ColumnId", int.Parse(sQID)),
					new SqlParameter("@OptionTypeId", int.Parse(sOptionTypeID)),
					new SqlParameter("@Rank", iRank),
					new SqlParameter("@Caption", sItemText),
					new SqlParameter("@DefaultValue", iDefaultValue)
					);
			}

			var query = new WQuery(this.Request.QueryString);
			query.Remove("OptionId");
			query["OptionTypeId"] = sOptionTypeID;

            query.Redirect();
		}

        protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];

			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericListColumnOption WHERE Id IN (" + sChecked + ")");
				this.LoadData();
			}
		}

        protected void grdOptions_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sID = e.Item.Cells[0].Text;
			QueryParser qs = new QueryParser(this.Request.QueryString);

			switch(e.CommandName)
			{
				case "edit":
					qs["OptionId"] = sID;

                    qs.Redirect();
					break;
			}
		}
	}
}
