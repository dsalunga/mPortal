using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class SurveywelcomeViewComponent : WViewComponent
    {
        private const string StatusKey = "Surveywelcome.StatusMessage";
        private const string ErrorKey = "Surveywelcome.ErrorMessage";

        public SurveywelcomeViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var postController = WcmsContext.Element?.GetParameterValue("PostController");
            if (string.IsNullOrWhiteSpace(postController) ||
                postController.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                postController = "CentralPartActions";
            }

            var model = new SurveywelcomeViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = postController,
                PostAction = WcmsContext.Element?.GetParameterValue("PostAction") ?? "Surveywelcome"
            };

            var listId = WcmsContext.GetId("ListId");
            if (listId > 0)
            {
                var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("GenericList") +
                          " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @ListId";
                using var reader = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@ListId", listId));
                if (reader.Read())
                {
                    model.ListId = DataUtil.GetId(reader["Id"]);
                    model.lblTitle = DataUtil.Get(reader, "Title");
                    model.lblMessage = DataUtil.Get(reader, "Description");
                }
            }
            else
            {
                var sql = "SELECT p.*, sp." + DbSyntax.QuoteIdentifier("ListId") +
                          " FROM " + DbSyntax.QuoteIdentifier("GenericListLink") + " sp" +
                          " INNER JOIN " + DbSyntax.QuoteIdentifier("GenericList") + " p ON sp." +
                          DbSyntax.QuoteIdentifier("ListId") + " = p." + DbSyntax.QuoteIdentifier("Id") +
                          " WHERE sp." + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId" +
                          " AND sp." + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId";
                using var reader = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@RecordId", model.RecordId),
                    DbHelper.CreateParameter("@ObjectId", model.ObjectId));
                if (reader.Read())
                {
                    model.ListId = DataUtil.GetId(reader["ListId"]);
                    model.lblTitle = DataUtil.Get(reader, "Title");
                    model.lblMessage = DataUtil.Get(reader, "Description");
                }
            }

            if (model.ListId <= 0)
            {
                model.ErrorMessage = "Survey not found.";
            }

            if (TempData.TryGetValue(StatusKey, out var status) && status != null)
                model.StatusMessage = status.ToString();

            if (TempData.TryGetValue(ErrorKey, out var error) && error != null)
                model.ErrorMessage = error.ToString();

            return View(model);
        }
    }

    public class SurveywelcomeViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int ListId { get; set; } = -1;
        public string PostController { get; set; } = "CentralPartActions";
        public string PostAction { get; set; } = "Surveywelcome";
        public string StatusMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public string lblTitle { get; set; } = string.Empty;
        public string lblMessage { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;

    }
}
