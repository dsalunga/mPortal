namespace WCMS.WebSystem.WebParts.Media
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
	///		Summary description for MediaHome.
	/// </summary>
	public partial class MediaHome : System.Web.UI.UserControl
	{	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            //QueryParser qs = new QueryParser(this.ID);
            WContext context = new WContext(this);
            //ControlInfo ci = new ControlInfo(qs["ID"]);

            //string sLocation = ci.Key(ControlInfoEnum.LocationType);
            //int iID = int.Parse(ci.Key(ControlInfoEnum.ItemID));

			using (SqlDataReader r = SqlHelper.ExecuteReader("MG_SELECT_MediaGallery",
					   new SqlParameter("@LocationItemID", context.RecordId),
					   new SqlParameter("@LocationType", context.ObjectId)))
			{
				rProducts.DataSource = r;
				rProducts.DataBind();
			}
		}

		protected string CreateLink(string sMID)
		{
            WContext qs = new WContext(this);
            //QueryParser qs = new QueryParser(this.ID);
			qs["SS"] = "MG";
			qs["MID"] = sMID;
			qs["P"] = "2";
			return qs.BuildQuery();
		}
	}
}
