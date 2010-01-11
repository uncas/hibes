using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using Uncas.EBS.UI.Helpers;

namespace Uncas.EBS.UI
{
    /// <summary>
    /// Class that presents the estimate as latex.
    /// </summary>
    public class EstimateAsLatex : IHttpHandler
    {
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
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

        private static int? GetIntFromQueryString
            (NameValueCollection queryString
            , string field)
        {
            int? result = null;
            if (!string.IsNullOrEmpty(queryString[field]))
            {
                result = int.Parse
                    (queryString[field]
                    , CultureInfo.InvariantCulture);
            }

            return result;
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <value>A value indicating whether it is reusable.</value>
        /// <returns>True if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}