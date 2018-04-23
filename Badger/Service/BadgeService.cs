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

        private SKPaint GetTextShadowPaint(BadgeModel badge)
        {
            var paint = this.GetTextPaint(badge);
            paint.Color = new SKColor(0x33, 0x33, 0x33, 0xff);
            return paint;
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
            float textY = badge.Height / 2 + textPaint.FontMetrics.Bottom;
            float corner = 4;

            /* draw left background */
            var labelBackgroundPaint = this.GetLabelBackgroundPaint(badge);
            canvas.DrawRoundRect(0, 0, leftSideWidth, badge.Height, corner, corner, labelBackgroundPaint);
            canvas.DrawRect(leftSideWidth - corner, 0, corner, badge.Height, labelBackgroundPaint);

            /* draw right background */
            var resultBackgroundPaint = this.GetResultBackgroundPaint(badge);
            canvas.DrawRoundRect(leftSideWidth, 0,  innerMargin + textPaint.MeasureText(badge.Result) + outerMargin, badge.Height, corner, corner, resultBackgroundPaint);
            canvas.DrawRect(leftSideWidth, 0, corner, badge.Height, resultBackgroundPaint);

            /* write left text */
            var textShadowPaint = this.GetTextShadowPaint(badge);
            float shadowFactor = 1.05f;
            canvas.DrawText(badge.Label, outerMargin, textY * shadowFactor, textShadowPaint);
            canvas.DrawText(badge.Label, outerMargin, textY, textPaint);

            /* write right text */
            float rightTextX = leftSideWidth + innerMargin;
            canvas.DrawText(badge.Result, x: rightTextX, y: textY * shadowFactor, paint: textShadowPaint);
            canvas.DrawText(badge.Result, x: rightTextX, y: textY, paint: textPaint);
        }
    }
}
