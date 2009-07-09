using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Uncas.EBS.UI.Helpers
{
    public class StyleHelpers
    {
        internal static void SetChartStyles(Chart chart)
        {
            chart.ImageType = ChartImageType.Png;
            chart.BackColor = Color.FromArgb(200, 210, 190);
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.Center;
            chart.BorderSkin.BorderDashStyle = ChartDashStyle.Solid;
            chart.BorderSkin.BorderWidth = 2;
            chart.BorderSkin.BorderColor = Color.FromArgb(26, 59, 105);

            foreach (Series series in chart.Series)
            {
                series.Color = Color.FromArgb(33, 99, 99);
                series.BorderColor = Color.FromArgb(180, 26, 59, 105);
            }

            var chartArea = chart.ChartAreas[0];
            chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BackSecondaryColor = Color.White;
            chartArea.BackColor = Color.FromArgb(64, 165, 191, 228);
            chartArea.ShadowColor = Color.Transparent;
            var area3DStyle = chartArea.Area3DStyle;
            area3DStyle.Rotation = 5;
            area3DStyle.Perspective = 5;
            area3DStyle.Inclination = 10;
            area3DStyle.PointDepth = 300;
            area3DStyle.IsRightAngleAxes = false;
            area3DStyle.WallWidth = 0;
            area3DStyle.IsClustered = false;
            area3DStyle.Enable3D = true;
            area3DStyle.LightStyle = LightStyle.Realistic;
            var axisY = chartArea.AxisY;
            var lineColor = Color.FromArgb(64, 64, 64, 64);
            axisY.LineColor = lineColor;
            axisY.MajorGrid.LineColor = lineColor;
            var axisX = chartArea.AxisX;
            axisX.LineColor = lineColor;
            axisX.MajorGrid.LineColor = lineColor;
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