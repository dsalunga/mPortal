namespace WCMS.WebSystem.WebParts.Ads
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
	///		Summary description for Ad.
	/// </summary>
	public partial class Ad : System.Web.UI.UserControl
	{

		#region Web Form designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.arAd.AdCreated += new System.Web.UI.WebControls.AdCreatedEventHandler(this.arAd_AdCreated);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
				this.LoadData();
		}

		private void LoadData()
		{
            WContext context = new WContext(this);
            //QueryParser qs = new QueryParser(this.ID);
            //ControlInfo ci = new ControlInfo(qs["ID"]);

            //string s_LOCATION = ci.Key(ControlInfoEnum.LocationType);
            //int i_ID = int.Parse(ci.Key(ControlInfoEnum.ItemID));

			object obj = SqlHelper.ExecuteScalar("ADS_SELECT_Ad_SITE",
				new SqlParameter("@LocationItemID", context.RecordId),
				new SqlParameter("@LocationType", context.ObjectId));

			if(obj != null)
				arAd.AdvertisementFile = "../../Assets/Uploads/Image/SECTIONS/ADS/xml/" + obj.ToString();
		}

		private void arAd_AdCreated(object sender, System.Web.UI.WebControls.AdCreatedEventArgs e)
		{
			int iStart, iEnd, iAD;
			string sURL = e.NavigateUrl;

			iStart = sURL.IndexOf("?ad=") + 4;
			iEnd = sURL.IndexOf("&");

			// GET AdID
			try
			{
				iAD = int.Parse(sURL.Substring(iStart, iEnd - iStart));

				//SqlData db = new SqlData();
				SqlHelper.ExecuteNonQuery("ADS_UPDATE_AdItem_Appearance",
					new SqlParameter("@AdItemID", iAD));
			}
			catch{}
		}
	}
}