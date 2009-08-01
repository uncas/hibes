using System.Collections.Specialized;
using System.Web;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class EstimateAsLatex : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            LatexHelpers lh = new LatexHelpers();
            int? maxPriority = GetIntFromQueryString
                (context.Request.QueryString
                , "MaxPriority");
            int? projectId = GetIntFromQueryString
                (context.Request.QueryString
                , "ProjectId");
            lh.DownloadLatexFromEstimate
                (projectId
                , maxPriority
                , context.Response);
        }

        private int? GetIntFromQueryString
            (NameValueCollection queryString
            , string field)
        {
            int? result = null;
            if (!string.IsNullOrEmpty(queryString[field]))
            {
                result = int.Parse(queryString[field]);
            }
            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}