using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

namespace WCMS.Web
{
	/// <summary>
	/// Summary description for QueryAnalyzer.
	/// </summary>
	public partial class QueryAnalyzer : Page
    {
        protected void Page_Load(object sender, System.EventArgs e) { }

		protected void cmdExecute_Click(object sender, System.EventArgs e)
		{
            //string sCode = txtCode.Text;
            //if (sCode != " $#!(" + DateTime.Now.Day.ToString() + " ")
            //{
            //    return;
            //}

			DataSet ds;
            SqlConnection cn;

			string sQS = txtQS.Text.Trim();
			char cDelim = '\u00b6';
			string sQueryBatch = txtQuery.Text.Trim();

			if(sQueryBatch == string.Empty)
				return;

			string[] sQueries = sQueryBatch.Replace("\r\nGO", cDelim.ToString()).Split(cDelim);

            // PREPARE CONNECTION STRING
			string sConnString;
            if (chkCustom.Checked)
            {
                sConnString = txtQS.Text.Trim();
            }
            else
            {
                if (sQS == string.Empty)
                    sConnString = SqlHelper.ConnString;
                else
                    sConnString = ConfigurationManager.ConnectionStrings[sQS].ConnectionString;
            }

            try
            {
                cn = new SqlConnection(sConnString);
            }
            catch (Exception ex)
            {
                // CATCH THE EXCEPTION
                lblMessage.Text = ex.Message;
                return;
            }

            // CHECK THE FILE AND UPLOAD
            if (chkUpload.Checked)
            {
                string sFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (sFileName == string.Empty)
                {
                    lblMessage.Text = "Please select a file.";
                    return;
                }

                string sFolder = Server.MapPath("~/Admin/Data");

                // Create the directory if it does not exists.
                if (!Directory.Exists(sFolder))
                {
                    Directory.CreateDirectory(sFolder);
                }

                // Save the uploaded file to the server.
                string sFilePath = sFolder + sFileName;
                FileUpload1.PostedFile.SaveAs(sFilePath);

                if (sQueries.Length > 1)
                {
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(sQueryBatch, cn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    // BEGIN INSERT
                    DataSet dsImport = new DataSet();
                    dsImport.ReadXml(sFilePath);

                    da.Update(dsImport.Tables[0]);
                }
            }
            else
            {

                if (sQueries.Length > 1)
                {
                    // MULTIPLE QUERIES
                    try
                    {
                        // EXECUTE EACH QUERY
                        string sQuery;
                        int i = 0;

                        for (i = 0; i < sQueries.Length - 1; i++)
                        {
                            sQuery = sQueries[i].Trim();
                            if (sQuery != string.Empty)
                            {
                                SqlHelper.ExecuteNonQuery(cn, CommandType.Text, sQuery);
                            }
                        }

                        try
                        {
                            sQuery = sQueries[i].Trim();
                            if (sQuery != string.Empty)
                            {
                                ds = SqlHelper.ExecuteDataSet(sConnString, CommandType.Text, sQuery);
                            }
                            else
                            {
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            // CATCH THE EXCEPTION
                            lblMessage.Text = ex.Message;
                            return;
                        }

                        this.DisplayDataGrid(ds);
                    }
                    catch (Exception ex)
                    {
                        // CATCH THE EXCEPTION
                        lblMessage.Text = ex.Message;
                        return;
                    }
                }
                else
                {
                    //  SINGLE QUERY, TRY DISPLAYING A GRID
                    try
                    {
                        ds = SqlHelper.ExecuteDataSet(sConnString, CommandType.Text, sQueryBatch);
                    }
                    catch (Exception ex)
                    {
                        // CATCH THE EXCEPTION
                        lblMessage.Text = ex.Message;
                        return;
                    }

                    this.DisplayDataGrid(ds);
                }
            }

			lblMessage.Text = string.Empty;
		}

        private void DisplayDataGrid(DataSet ds)
        {
            bool bDownload = chkDownload.Checked;

            //  DOWNLOAD
            if (bDownload)
            {
                try
                {
                    string sFolder = Server.MapPath("~/Admin/Data");

                    // Create the directory if it does not exists.
                    //if (!Directory.Exists(sFolder))
                    //{
                    //    Directory.CreateDirectory(sFolder);
                    //}

                    string sFile = ds.DataSetName + ".xml";
                    string sFilePath = sFolder + "/" + sFile;

                    if (chkSchema.Checked)
                    {
                        ds.WriteXml(sFilePath, XmlWriteMode.WriteSchema);
                    }
                    else
                    {
                        ds.WriteXml(sFilePath, XmlWriteMode.IgnoreSchema);
                    }

                    Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
                    Response.WriteFile(sFilePath);
                    Response.End();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "[ " + ex.Message + " ]";
                }
                return;
            }
            else
            {
                foreach (DataTable dt in ds.Tables)
                {
                    DataGrid g = new DataGrid();

                    // FORMAT STYLE
                    g.Width = Unit.Percentage(100);
                    g.BorderColor = Color.FromName("#DEBA84");
                    g.BackColor = Color.FromName("#DEBA84");
                    g.BorderWidth = Unit.Pixel(1);
                    g.BorderStyle = BorderStyle.None;
                    g.CellPadding = 3;

                    // ITEM
                    g.ItemStyle.ForeColor = Color.FromName("#8C4510");
                    g.ItemStyle.BackColor = Color.FromName("#FFF7E7");

                    // HEADER
                    g.HeaderStyle.Font.Bold = true;
                    g.HeaderStyle.ForeColor = Color.White;
                    g.HeaderStyle.BackColor = Color.FromName("#A55129");
                    // END FORMAT

                    g.DataSource = dt.DefaultView;
                    g.DataBind();
                    phGrid.Controls.Add(g);
                    phGrid.Controls.Add(new LiteralControl("<br>"));
                }
            }
        }
	}
}
