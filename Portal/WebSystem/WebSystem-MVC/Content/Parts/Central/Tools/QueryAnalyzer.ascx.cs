using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class QueryAnalyzerPresenter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e) { }

        protected void cmdExecute_Click(object sender, System.EventArgs e)
        {
            //string sCode = txtCode.Text;
            //if (sCode != " $#!(" + DateTime.Now.Day.ToString() + " ")
            //{
            //    return;
            //}

            DataSet dataSet;
            SqlConnection conn;

            string queryString = txtQS.Text.Trim();
            char cDelim = '\u00b6';
            string queryBatch = txtQuery.Text.Trim();

            if (queryBatch == string.Empty)
                return;

            string[] queries = queryBatch.Replace("\r\nGO", cDelim.ToString()).Split(cDelim);

            // PREPARE CONNECTION STRING
            string connString;
            if (chkCustom.Checked)
            {
                connString = txtQS.Text.Trim();
            }
            else
            {
                if (queryString == string.Empty)
                    connString = SqlHelper.ConnString;
                else
                    connString = ConfigurationManager.ConnectionStrings[queryString].ConnectionString;
            }

            try
            {
                conn = new SqlConnection(connString);
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
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (fileName == string.Empty)
                {
                    lblMessage.Text = "Please select a file.";
                    return;
                }

                string folder = Server.MapPath(WebHelper.TEMP_DATA_PATH);

                // Create the directory if it does not exists.
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Save the uploaded file to the server.
                string filePath = folder + fileName;
                FileUpload1.PostedFile.SaveAs(filePath);

                if (queries.Length > 1)
                {
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(queryBatch, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    adapter.MissingMappingAction = MissingMappingAction.Passthrough;

                    // BEGIN INSERT
                    DataSet dataSetImport = new DataSet();
                    dataSetImport.ReadXml(filePath);

                    adapter.Update(dataSetImport.Tables[0]);
                }
            }
            else
            {

                if (queries.Length > 1)
                {
                    // MULTIPLE QUERIES
                    try
                    {
                        // EXECUTE EACH QUERY
                        string query;
                        int i = 0;

                        for (i = 0; i < queries.Length - 1; i++)
                        {
                            query = queries[i].Trim();
                            if (query != string.Empty)
                                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, query);
                        }

                        try
                        {
                            query = queries[i].Trim();
                            if (query != string.Empty)
                                dataSet = SqlHelper.ExecuteDataSet(connString, CommandType.Text, query);
                            else
                                return;
                        }
                        catch (Exception ex)
                        {
                            // CATCH THE EXCEPTION
                            lblMessage.Text = ex.Message;
                            return;
                        }

                        this.DisplayDataGrid(dataSet);
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
                        dataSet = SqlHelper.ExecuteDataSet(connString, CommandType.Text, queryBatch);
                    }
                    catch (Exception ex)
                    {
                        // CATCH THE EXCEPTION
                        lblMessage.Text = ex.Message;
                        return;
                    }

                    this.DisplayDataGrid(dataSet);
                }
            }

            lblMessage.Text = string.Empty;
        }

        private void DisplayDataGrid(DataSet ds)
        {
            bool isDownload = chkDownload.Checked;

            //  DOWNLOAD
            if (isDownload)
            {
                try
                {
                    string folder = Server.MapPath(WebHelper.TEMP_DATA_PATH);

                    // Create the directory if it does not exists.
                    //if (!Directory.Exists(sFolder))
                    //{
                    //    Directory.CreateDirectory(sFolder);
                    //}

                    string fileName = ds.DataSetName + ".xml";
                    string filePath = folder + "/" + fileName;

                    if (chkSchema.Checked)
                        ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
                    else
                        ds.WriteXml(filePath, XmlWriteMode.IgnoreSchema);

                    Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.WriteFile(filePath);
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
                foreach (DataTable dataTable in ds.Tables)
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

                    g.DataSource = dataTable.DefaultView;
                    g.DataBind();
                    phGrid.Controls.Add(g);
                    phGrid.Controls.Add(new LiteralControl("<br>"));
                }
            }
        }
    }
}