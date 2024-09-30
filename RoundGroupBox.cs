using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Serialization;

namespace WooSungEngineering
{
    public class RoundGroupBox : XRPanel
    {
        private static int defaultHeight = 75;

        private static int defaultWidth = 300;

        public RoundGroupBox() : base()
        {
            WidthF = XRConvert.Convert(defaultWidth, GraphicsDpi.HundredthsOfAnInch, Dpi);
            HeightF = XRConvert.Convert(defaultHeight, GraphicsDpi.HundredthsOfAnInch, Dpi);

            this.ForeColor = SystemColors.Highlight;
            this.Borders = BorderSide.None;
            this.BorderColor = SystemColors.Highlight;
            _label = string.Empty;
        }

        private string _label;
        public string GroupBoxLabel
        {
            get { return _label; }
            set { _label = value; }
        }

        private float _radius = 10f;
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        private float _Offset = 10f;

        public float Offset
        {
            get { return _Offset; }
            set { _Offset = value; }
        }


        protected override void SerializeProperties(XRSerializer serializer)
        {
            base.SerializeProperties(serializer);
            serializer.SerializeValue("GroupBoxLabel", _label);
            serializer.SerializeValue("Radius", _radius);
            serializer.SerializeValue("Offset", _Offset);
        }

        protected override void DeserializeProperties(XRSerializer serializer)
        {
            base.DeserializeProperties(serializer);
            _label = Convert.ToString(serializer.DeserializeValue("GroupBoxLabel", typeof(string), ""));
            _radius = Convert.ToSingle(serializer.DeserializeValue("Radius", typeof(float), null));
            _Offset = Convert.ToSingle(serializer.DeserializeValue("Offset", typeof(float), null));
        }

        protected override VisualBrick CreateBrick(VisualBrick[] childrenBricks)
        {
            VisualBrick baseBrick = base.CreateBrick(childrenBricks);
            if (!(baseBrick is PanelBrick))
            {
                baseBrick = new PanelBrick(this);
            }

            return baseBrick;
        }

        protected override void PutStateToBrick(VisualBrick brick, PrintingSystemBase ps)
        {
            // Call the PutStateToBrick method of the base class.
            base.PutStateToBrick(brick, ps);
            //// Get the Panel brick which represents the current progress bar control.
            PanelBrick panel = (PanelBrick)brick;
            panel.Sides = BorderSide.None;
            panel.BackColor = Color.Transparent;
            panel.BorderColor = Color.Transparent;

            BrickCollectionBase col = new BrickCollectionBase(new PanelBrick());

            foreach (Brick br in panel.Bricks)
            {
                col.Add(br);
            }

            int width = Convert.ToInt32(Math.Round(panel.Rect.Width));
            int height = Convert.ToInt32(Math.Round(panel.Rect.Height));

            Bitmap bitmap = new Bitmap(width, height);
            Graphics gBmp = Graphics.FromImage(bitmap);
            gBmp.FillRectangle(new SolidBrush(this.Parent.BackColor), 0, 0, panel.Rect.Width, panel.Rect.Height);
            gBmp.SmoothingMode = SmoothingMode.HighQuality;
            RectangleF clientBounds = new RectangleF(new PointF(0, 0), new SizeF(panel.Rect.Width, panel.Rect.Height));

            float radius = _radius;
            string label = _label;

            float offset = _Offset;
            //Set the reduction of the GroupBox Border from the Rectangle.
            RectangleF rect = new RectangleF(clientBounds.X + offset, clientBounds.Y + offset, clientBounds.Width - offset * 2, clientBounds.Height - offset * 2);

            //Top, left start point of the border
            float x = rect.Left;
            float y = rect.Top;

            FontFamily fontfamily = new FontFamily("Arial");
            FontStyle fontStyle = FontStyle.Regular;

            //Create the String, starting offset by radius and a constant.
            //Y offset of 17 moves text to centre of horizontal line
            PointF origin = new PointF(x + radius + 15, y - 17);
            GraphicsPath spath = new GraphicsPath();
            SolidBrush sbrush = new SolidBrush(this.BorderColor);

            if (label != string.Empty)
            {
                spath.AddString(label, fontfamily, Convert.ToInt32(fontStyle), 26f, origin, StringFormat.GenericDefault);
                gBmp.FillPath(sbrush, spath);
                //Fillpath is used to get normal text.
            }

            //Starting point is top and at end of text plus offset, and proceeds clockwise.
            GraphicsPath lpath = new GraphicsPath();
            Pen pen1 = new Pen(this.BorderColor, this.BorderWidth);
            Pen pen2 = new Pen(Color.Transparent, this.BorderWidth);
            PointF startPoint = default(PointF);
            if (spath.PointCount == 0)
            {
                startPoint = origin;
            }
            else
            {
                startPoint = spath.GetLastPoint();
                startPoint.X += 20;
            }

            lpath.AddLine(startPoint.X - 5, y, x + rect.Width - (radius * 2), y);

            //Creates a U shapped border, with radiused corners. Each Arc joins to the next.
            lpath.AddArc(x + rect.Width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            lpath.AddArc(x + rect.Width - (radius * 2), y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
            lpath.AddArc(x, y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
            lpath.AddArc(x, y, radius * 2, radius * 2, 180, 90);

            //Completes line to start of text
            lpath.AddLine(x + radius, y, x + radius + 10, y);
            gBmp.DrawPath(pen1, lpath);
            //Draws the whole border.
            SolidBrush backBrush = new SolidBrush(this.BackColor);
            gBmp.FillPath(backBrush, lpath);


            spath.Dispose();
            lpath.Dispose();
            pen1.Dispose();
            pen2.Dispose();
            sbrush.Dispose();


            //add an image brick to represent the panel's borders
            ImageBrick ibr = new ImageBrick();
            ibr.Location = new PointF(0, 0);
            ibr.Size = new System.Drawing.SizeF(panel.Rect.Width, panel.Rect.Height);
            ibr.SizeMode = ImageSizeMode.StretchImage;
            ibr.Sides = BorderSide.None;
            ibr.Image = bitmap;

            panel.Bricks.Clear();
            panel.Bricks.Add(ibr);

            //add children bricks
            foreach (Brick br in col)
            {
                panel.Bricks.Add(br);
            }
        }
    }
}
