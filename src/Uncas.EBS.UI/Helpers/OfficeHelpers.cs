﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Helpers
{
    /// <summary>
    /// Helpers for handling download to Office.
    /// </summary>
    public class OfficeHelpers
    {
        /// <summary>
        /// Downloads the word.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="response">The response.</param>
        public void DownloadWord(Control control
            , string fileName
            , HttpResponse response)
        {
            string fileExtension = ".doc";
            string contentType = "application/msword";
            DownloadToProgram
                (control
                , fileName
                , response
                , fileExtension
                , contentType);
        }

        /// <summary>
        /// Downloads the excel.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="response">The response.</param>
        public void DownloadExcel(Control control
            , string fileName
            , HttpResponse response)
        {
            string fileExtension = ".xls";
            string contentType = "application/vnd.ms-excel";
            DownloadToProgram
                (control
                , fileName
                , response
                , fileExtension
                , contentType);
        }

        private void DownloadToProgram
            (Control control
            , string fileName
            , HttpResponse response
            , string fileExtension
            , string contentType)
        {
            response.Clear();

            string headerValue
                = string.Format
                    (CultureInfo.InvariantCulture
                    , "attachment;filename={0}{1}"
                    , fileName
                    , fileExtension);
            response.AddHeader
                ("Content-Disposition"
                , headerValue);
            response.ContentType = contentType;

            using (StringWriter sw
                = new StringWriter(CultureInfo.CurrentCulture))
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
        /// Replace any of the contained controls with literals.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <see>http://forums.asp.net/p/1362432/2817931.aspx</see>
        private void PrepareControlForExport
            (Control control)
        {
            var transforms
                = ControlTransformBase.GetControlTransforms();

            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];

                foreach (ControlTransformBase transform in transforms)
                {
                    if (transform.IsT(current))
                    {
                        current.Visible = false;
                        control.Controls.AddAt
                            (i
                            , new LiteralControl(transform.ControlToString(current)));
                        i++;
                    }
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        #region Private classes

        /// <summary>
        /// Represents a control to transform.
        /// </summary>
        private abstract class ControlTransformBase
        {
            internal Func<Control, string> ControlToString { get; set; }

            internal static IEnumerable<ControlTransformBase>
                GetControlTransforms()
            {
                var transforms = new ControlTransformBase[]
                {
                    new ControlTransform<Chart>
                        ((Control c) 
                            => string.Empty),
                    
                    new ControlTransform<DropDownList>
                        ((Control c) 
                            => (c as DropDownList).SelectedItem.Text),
                    
                    new ControlTransform<CheckBox>
                        ((Control c)
                            => (c as CheckBox).Checked
                            ? "True"
                            : "False"),

                    new ControlTransform<HyperLink>
                        ((Control c)
                            => HttpContext.Current.Server.HtmlEncode
                            ((c as HyperLink).Text)),

                    new ControlTransform<ImageButton>
                        ((Control c)
                            => (c as ImageButton).AlternateText),

                    new ControlTransform<LinkButton>
                        ((Control c)
                            => (c as LinkButton).Text),

                    new ControlTransform<TextBox>
                        ((Control c)
                            => (c as TextBox).Text)
                };

                return transforms;
            }

            internal abstract bool IsT(Control c);
        }

        /// <summary>
        /// Represents a control of type T to transform.
        /// </summary>
        /// <typeparam name="T">The type of the control.</typeparam>
        private class ControlTransform<T> : ControlTransformBase
            where T : Control
        {
            internal ControlTransform(Func<Control, string> toString)
            {
                this.ControlToString = toString;
            }

            internal override bool IsT(Control c)
            {
                return c is T;
            }
        }

        #endregion
    }
}