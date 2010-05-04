﻿namespace GMap.NET.WindowsForms
{
   using System.Drawing;
   using System.Drawing.Drawing2D;

   /// <summary>
   /// GMap.NET marker
   /// </summary>
   public class GMapToolTip
   {
      internal GMapMarker Marker;

      public Point ToolTipOffset;

      /// <summary>
      /// tooltip string format
      /// </summary>
      public StringFormat ToolTipFormat = new StringFormat();

      /// <summary>
      /// font for markers tooltip
      /// </summary>
#if !PocketPC
      public Font ToolTipFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold, GraphicsUnit.Pixel);
#else
      public Font ToolTipFont = new Font(FontFamily.GenericSansSerif, 6, FontStyle.Bold);
#endif

      /// <summary>
      /// tooltip pen
      /// </summary>
#if !PocketPC
      public Pen ToolTipPen = new Pen(Color.FromArgb(140, Color.MidnightBlue));
#else
      public Pen ToolTipPen = new Pen(Color.MidnightBlue);
#endif

      /// <summary>
      /// tooltip background color
      /// </summary>
#if !PocketPC
      public Brush ToolTipBackground = Brushes.AliceBlue;
#else
      public Brush ToolTipBackground = new System.Drawing.SolidBrush(Color.AliceBlue);
      public Brush TooltipForeground = new System.Drawing.SolidBrush(Color.Navy);
#endif

      public GMapToolTip(GMapMarker marker)
      {
         this.Marker = marker;
         this.ToolTipOffset = new Point(14, -44);

         this.ToolTipPen.Width = 2;

#if !PocketPC
         this.ToolTipPen.LineJoin = LineJoin.Round;
         this.ToolTipPen.StartCap = LineCap.RoundAnchor;
#endif

         this.ToolTipFormat.Alignment = StringAlignment.Center;
         this.ToolTipFormat.LineAlignment = StringAlignment.Center;
      }

      public virtual void Draw(Graphics g)
      {
         System.Drawing.Size st = g.MeasureString(Marker.ToolTipText, ToolTipFont).ToSize();
         System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Marker.LocalPosition.X, Marker.LocalPosition.Y, st.Width + Marker.Overlay.Control.TooltipTextPadding.Width, st.Height + Marker.Overlay.Control.TooltipTextPadding.Height);
         rect.Offset(ToolTipOffset.X, ToolTipOffset.Y);

         g.DrawLine(ToolTipPen, Marker.LocalPosition.X, Marker.LocalPosition.Y, rect.X, rect.Y + rect.Height / 2);

         g.FillRectangle(ToolTipBackground, rect);
         g.DrawRectangle(ToolTipPen, rect);

#if !PocketPC
         g.DrawString(Marker.ToolTipText, ToolTipFont, Brushes.Navy, rect, ToolTipFormat);
#else
         g.DrawString(Marker.ToolTipText, ToolTipFont, TooltipForeground, rect, ToolTipFormat);
#endif
      }
   }
}
