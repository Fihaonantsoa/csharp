using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Reservation
{
    public class GradientButton : Button
    {
        public Color GradientTop { get; set; } = Color.DeepSkyBlue;
        public Color GradientBottom { get; set; } = Color.FromArgb(41, 128, 185);

        public Color HoverTop { get; set; } = Color.Aqua;
        public Color HoverBottom { get; set; } = Color.FromArgb(41, 128, 185);

        public Color BorderColor { get; set; } = Color.White;
        public int BorderSize { get; set; } = 1;
        public int BorderRadius { get; set; } = 10;

        private bool isHovering = false;
        private float animationProgress = 0f;
        private Timer animationTimer;

        public GradientButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);

            animationTimer = new Timer();
            animationTimer.Interval = 15;
            animationTimer.Tick += AnimateHover;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetRoundedRegion();
        }

        private void SetRoundedRegion()
        {
            using (GraphicsPath path = GetRoundPath(ClientRectangle, BorderRadius))
            {
                Region = new Region(path);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovering = true;
            animationTimer.Start();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovering = false;
            animationTimer.Start();
            base.OnMouseLeave(e);
        }

        private void AnimateHover(object sender, EventArgs e)
        {
            float speed = 0.08f;
            if (isHovering)
            {
                if (animationProgress < 1f)
                {
                    animationProgress += speed;
                    Invalidate();
                }
                else
                {
                    animationProgress = 1f;
                    animationTimer.Stop();
                }
            }
            else
            {
                if (animationProgress > 0f)
                {
                    animationProgress -= speed;
                    Invalidate();
                }
                else
                {
                    animationProgress = 0f;
                    animationTimer.Stop();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = ClientRectangle;

            Color topColor = Blend(GradientTop, HoverTop, animationProgress);
            Color bottomColor = Blend(GradientBottom, HoverBottom, animationProgress);

            float scale = 1f + 0.03f * animationProgress;
            int offsetX = (int)(Width * (1 - scale) / 2);
            int offsetY = (int)(Height * (1 - scale) / 2);
            Rectangle scaledRect = new Rectangle(offsetX, offsetY, (int)(Width * scale), (int)(Height * scale));

            using (GraphicsPath path = GetRoundPath(scaledRect, BorderRadius))
            using (LinearGradientBrush brush = new LinearGradientBrush(scaledRect, topColor, bottomColor, LinearGradientMode.Vertical))
            using (Pen borderPen = new Pen(BorderColor, BorderSize))
            {
                e.Graphics.FillPath(brush, path);
                if (BorderSize > 0)
                    e.Graphics.DrawPath(borderPen, path);
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, rect, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private Color Blend(Color c1, Color c2, float amount)
        {
            int Clamp(int value) => Math.Min(255, Math.Max(0, value));

            int r = Clamp((int)(c1.R + (c2.R - c1.R) * amount));
            int g = Clamp((int)(c1.G + (c2.G - c1.G) * amount));
            int b = Clamp((int)(c1.B + (c2.B - c1.B) * amount));

            return Color.FromArgb(r, g, b);
        }
        private GraphicsPath GetRoundPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter - 1, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter - 1, bounds.Bottom - diameter - 1, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter - 1, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

}
