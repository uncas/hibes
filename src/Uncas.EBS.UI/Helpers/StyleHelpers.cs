using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Helpers
{
    public static class StyleHelpers
    {
        internal static void SetChartStyles(Chart chart)
        {
            SetBaseChartStyles(chart);

            SetChartSeriesStyles(chart.Series);

            SetChartAreaStyles(chart.ChartAreas[0]);
        }

        private static void SetBaseChartStyles(Chart chart)
        {
            chart.Width = Unit.Pixel(500);
            chart.ImageType = ChartImageType.Png;
            chart.BackColor = Color.FromArgb(200, 210, 190);
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.Center;
            chart.BorderSkin.BorderDashStyle = ChartDashStyle.Solid;
            chart.BorderSkin.BorderWidth = 2;
            chart.BorderSkin.BorderColor = Color.FromArgb(26, 59, 105);
        }

        private static void SetChartSeriesStyles
            (SeriesCollection seriesCollection)
        {
            foreach (Series series in seriesCollection)
            {
                series.Color = Color.FromArgb(33, 99, 99);
                series.BorderColor = Color.FromArgb(180, 26, 59, 105);
            }
        }

        private static void SetChartAreaStyles(ChartArea chartArea)
        {
            chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BackSecondaryColor = Color.White;
            chartArea.BackColor = Color.FromArgb(64, 165, 191, 228);
            chartArea.ShadowColor = Color.Transparent;

            SetArea3DStyles(chartArea.Area3DStyle);
            SetAxisStyles(chartArea.AxisX);
            SetAxisStyles(chartArea.AxisY);
        }

        private static void SetArea3DStyles(ChartArea3DStyle area3DStyle)
        {
            area3DStyle.Rotation = 5;
            area3DStyle.Perspective = 5;
            area3DStyle.Inclination = 10;
            area3DStyle.PointDepth = 300;
            area3DStyle.IsRightAngleAxes = false;
            area3DStyle.WallWidth = 0;
            area3DStyle.IsClustered = false;
            area3DStyle.Enable3D = true;
            area3DStyle.LightStyle = LightStyle.Realistic;
        }

        private static void SetAxisStyles(Axis axis)
        {
            var lineColor = Color.FromArgb(64, 64, 64, 64);

            axis.LineColor = lineColor;
            axis.MajorGrid.LineColor = lineColor;
        }

        internal static void SetIssueRowStyle(GridViewRow row
            , double? fractionElapsed)
        {
            if (!fractionElapsed.HasValue)
            {
                row.CssClass = "noTasks";
            }
            else if (fractionElapsed.HasValue
                && fractionElapsed.Value == 0d)
            {
                row.CssClass = "notStarted";
            }
            else if (fractionElapsed.HasValue
                && fractionElapsed.Value > 0d)
            {
                row.CssClass = "inProgress";
            }
        }
    }
}