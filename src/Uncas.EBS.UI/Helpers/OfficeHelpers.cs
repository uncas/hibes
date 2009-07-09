using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Helpers
{
    public class OfficeHelpers
    {
        public void DownloadWord(Control control
            , string fileName
            , HttpResponse response)
        {
            response.Clear();

            response.AddHeader
                ("Content-Disposition"
                , string.Format("attachment;filename={0}.doc", fileName));
            response.ContentType = "application/msword";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter(sw))
                {
                    PrepareControlForExport(control);
                    control.RenderControl(writer);
                }
                response.Write(sw.ToString());
            }

            response.End();
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        /// <see cref="http://forums.asp.net/p/1362432/2817931.aspx"/>
        private void PrepareControlForExport
            (Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is Chart)
                {
                    current.Visible = false;
                }
                else if (current is CheckBox)
                {
                    current.Visible = false;
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                    i++;
                }
                else if (current is DropDownList)
                {
                    current.Visible = false;
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                    i++;
                }
                else if (current is HyperLink)
                {
                    current.Visible = false;
                    string text = (current as HyperLink).Text;
                    text = HttpContext.Current.Server.HtmlEncode(text);
                    control.Controls.AddAt(i, new LiteralControl(text));
                    i++;
                }
                else if (current is ImageButton)
                {
                    current.Visible = false;
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                    i++;
                }
                else if (current is LinkButton)
                {
                    current.Visible = false;
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                    i++;
                }
                else if (current is TextBox)
                {
                    current.Visible = false;
                    control.Controls.AddAt(i, new LiteralControl((current as TextBox).Text));
                    i++;
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }
    }
}