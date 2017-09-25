﻿using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetButton), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroSetButtonDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetButton : Control, iControl
    {

        #region Interfaces

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get
            {
                return StyleManager?.Style ?? style;
            }
            set
            {
                style = value;
                switch (value)
                {
                    case Style.Light:
                        ApplyTheme();
                        break;

                    case Style.Dark:
                        ApplyTheme(Style.Dark);
                        break;

                    case Style.Custom:
                        ApplyTheme(Style.Custom);
                        break;

                    default:
                        ApplyTheme();
                        break;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the The Author name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        /// <summary>
        /// Gets or sets the The Theme name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        /// <summary>
        /// Gets or sets the Style Manager associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
        public StyleManager StyleManager
        {
            get { return _StyleManager; }
            set
            {
                _StyleManager = value;
                Invalidate();
            }
        }

        #endregion Interfaces

        #region Global Vars

        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private MouseMode State;
        private Style style;
        private StyleManager _StyleManager;
        private static ButtonProperties prop;
        
        #endregion Internal Vars

        #region Constructors

        public MetroSetButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            prop = new ButtonProperties();
            Font = MetroSetFonts.Light(10);
            utl = new Utilites();
            mth = new Methods();
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (State)
            {
                case MouseMode.Normal:

                    using (SolidBrush BG = new SolidBrush(prop.NormalColor))
                    using (Pen P = new Pen(prop.NormalBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.NormalTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), mth.SetPosition());
                    }

                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (SolidBrush BG = new SolidBrush(prop.HoverColor))
                    using (Pen P = new Pen(prop.HoverBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.HoverTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), mth.SetPosition());
                    }

                    break;

                case MouseMode.Pushed:

                    using (SolidBrush BG = new SolidBrush(prop.PressColor))
                    using (Pen P = new Pen(prop.PressBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.PressTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), mth.SetPosition());
                    }

                    break;
            }
        }

        #endregion Draw Control

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        /// <param name="path">The path of the custom theme.</param>
        internal void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    prop.NormalColor = Color.FromArgb(238, 238, 238);
                    prop.NormalBorderColor = Color.FromArgb(204, 204, 204);
                    prop.NormalTextColor = Color.Black;
                    prop.HoverColor = Color.FromArgb(102, 102, 102);
                    prop.HoverBorderColor = Color.FromArgb(102, 102, 102);
                    prop.HoverTextColor = Color.White;
                    prop.PressColor = Color.FromArgb(51, 51, 51);
                    prop.PressBorderColor = Color.FromArgb(51, 51, 51);
                    prop.PressTextColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    break;

                case Style.Dark:
                    prop.NormalColor = Color.FromArgb(32, 32, 32);
                    prop.NormalBorderColor = Color.FromArgb(64, 64, 64);
                    prop.NormalTextColor = Color.FromArgb(204, 204, 204);
                    prop.HoverColor = Color.FromArgb(170, 170, 170);
                    prop.HoverBorderColor = Color.FromArgb(170, 170, 170);
                    prop.HoverTextColor = Color.White;
                    prop.PressColor = Color.FromArgb(240, 240, 240);
                    prop.PressBorderColor = Color.FromArgb(240, 240, 240);
                    prop.PressTextColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    break;
                    
                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ButtonDictionary)
                        {
                            if ((varkey.Key == null) || varkey.Key == null)
                            {
                                return;
                            }

                            if (varkey.Key == "NormalColor")
                            {
                                prop.NormalColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBorderColor")
                            {
                                prop.NormalBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalTextColor")
                            {
                                prop.NormalTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverColor")
                            {
                                prop.HoverColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBorderColor")
                            {
                                prop.HoverBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverTextColor")
                            {
                                prop.HoverTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressColor")
                            {
                                prop.PressColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBorderColor")
                            {
                                prop.PressBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressTextColor")
                            {
                                prop.PressTextColor = utl.HexColor((string)varkey.Value);
                            }
                        }
                    Refresh();
                    break;
            }
        }

        #endregion Theme Changing

        #region Properties

        [Browsable(false)]
        public new Color BackColor
        {
            get { return Color.Transparent; }
        }

        #endregion

        #region Events

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Pushed;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Normal;
            Invalidate();
        }

        #endregion Events
    }
}