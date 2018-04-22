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

            /* draw left background */
            canvas.DrawRect(
                0,
                0,
                w: leftSideWidth,
                h: badge.Height,
                paint: this.GetLabelBackgroundPaint(badge)
                );

            /* draw right background */
            canvas.DrawRect(
                leftSideWidth,
                0,
                w: innerMargin + textPaint.MeasureText(badge.Result) + outerMargin,
                h: badge.Height,
                paint: this.GetResultBackgroundPaint(badge)
                );

            /* write left text */
            canvas.DrawText(badge.Label, outerMargin, textY, textPaint);

            /* write right text */
            canvas.DrawText(badge.Result, x: leftSideWidth + innerMargin, y: textY, paint: textPaint);
        }
    }
}
