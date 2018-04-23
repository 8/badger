using SkiaSharp;
using Badger.Model;

namespace Badger.Service
{
    public interface IBadgeService
    {
        void DrawBadge(SKCanvas canvas, BadgeModel badge);

        float GetWidth(BadgeModel badge);
    }

    public class BadgeService : IBadgeService
    {
        private SKPaint GetTextPaint(BadgeModel badge)
        {
            return new SKPaint { IsAntialias = true, IsStroke = false, Color = SKColors.White, TextSize = badge.Height - GetTopBottomMargin()*2 };
        }

        private SKPaint GetResultBackgroundPaint(BadgeModel badge)
        {
            return new SKPaint { IsAntialias = true, IsStroke = false, Color = badge.ResultBackgroundColor };
        }

        private SKPaint GetLabelBackgroundPaint(BadgeModel badge)
        {
            return new SKPaint { IsAntialias = true, IsStroke = false, Color = badge.LabelBackgroundColor};
        }

        private int GetOuterMargin() => 10;
        private int GetInnerMargin() => 5;
        private int GetTopBottomMargin() => 4;

        public float GetWidth(BadgeModel badge)
        {
            var paint = GetTextPaint(badge);

            int outerMargin = GetOuterMargin(),
                innerMargin = GetInnerMargin();

            var width = outerMargin
                      + paint.MeasureText(badge.Label)
                      + innerMargin
                      + innerMargin
                      + paint.MeasureText(badge.Result)
                      + outerMargin;

            return width;
        }

        public void DrawBadge(SKCanvas canvas, BadgeModel badge)
        {
            canvas.Clear();

            var textPaint = this.GetTextPaint(badge);
            float outerMargin = this.GetOuterMargin(),
                  innerMargin = this.GetInnerMargin();
            float leftTextWidth = textPaint.MeasureText(badge.Label);
            float leftSideWidth = outerMargin + leftTextWidth + innerMargin;
            float topBottomMargin = this.GetTopBottomMargin();
            float textY = badge.Height - topBottomMargin / 2 - textPaint.FontMetrics.Descent;
            float corner = 4;

            /* draw left background */
            canvas.DrawRoundRect(0, 0, leftSideWidth, badge.Height, corner, corner, this.GetLabelBackgroundPaint(badge));
            canvas.DrawRect(leftSideWidth - corner, 0, corner, badge.Height, this.GetLabelBackgroundPaint(badge));

            /* draw right background */
            var backgroundPaint = this.GetResultBackgroundPaint(badge);
            canvas.DrawRoundRect(leftSideWidth, 0,  innerMargin + textPaint.MeasureText(badge.Result) + outerMargin, badge.Height, corner, corner, backgroundPaint);
            canvas.DrawRect(leftSideWidth, 0, corner, badge.Height, backgroundPaint);

            /* write left text */
            canvas.DrawText(badge.Label, outerMargin, textY, textPaint);

            /* write right text */
            canvas.DrawText(badge.Result, x: leftSideWidth + innerMargin, y: textY, paint: textPaint);
        }
    }
}
