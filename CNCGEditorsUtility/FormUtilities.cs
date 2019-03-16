using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CNCGEditors.Windows.Forms {
    internal static class FormUtilities {
        #region Constants
        private const BindingFlags BindingFlags_Public_Instance = BindingFlags.Public | BindingFlags.Instance;
        private const int ControlMinSize = 20,
                          Frm_MainMenu_Height = 20,
                          ScrollableControlMinSize = 50;
        #endregion

        #region Fields
        private static readonly Size Frm_Borders_CombinedSize = new Size(16, 38),
                                     Frm_Padding_Size = new Size(9, 9),
                                     LstBox_Borders_CombinedSize = new Size(1, 1),
                                     SpltPnl_Borders_Combined = new Size(4, 4);
        #endregion

        #region Structures
        [ComVisible(true)]
        [Serializable]
        private struct _Rectangle {
            #region Fields
            public static readonly _Rectangle Empty = new _Rectangle(Rectangle.Empty);
            private HorizontalLine horizontal;
            private VerticalLine vertical;
            #endregion

            #region Constructors
            public _Rectangle(Rectangle rectangle)
            : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height) {
            }
            public _Rectangle(HorizontalLine horizontal, VerticalLine vertical) {
                this.horizontal = horizontal;
                this.vertical = vertical;
            }
            public _Rectangle(Point location, Size size)
            : this(location.X, location.Y, size.Width, size.Height) {
            }
            public _Rectangle(int x, int y, int width, int height)
            : this(new HorizontalLine(x, width), new VerticalLine(y, height)) {
            }
            #endregion

            #region Operators
            public static bool operator ==(_Rectangle left, _Rectangle right) {
                return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
            }
            public static bool operator !=(_Rectangle left, _Rectangle right) {
                return !(left == right);
            }
            public static explicit operator _Rectangle(Rectangle rectangle) {
                return new _Rectangle(rectangle);
            }
            public static explicit operator Rectangle(_Rectangle rectangle) {
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }
            #endregion

            #region Methods
            public static _Rectangle FromLTRB(int left, int top, int right, int bottom) {
                return new _Rectangle(left, top, right - left, bottom - top);
            }
            public static _Rectangle Intersect(_Rectangle rectangle1, _Rectangle rectangle2) {
                HorizontalLine horizontal;
                VerticalLine vertical;

                horizontal = HorizontalLine.Intersect(rectangle1.Horizontal, rectangle2.Horizontal);
                if (horizontal.IsEmpty) {
                    return Empty;
                }
                vertical = VerticalLine.Intersect(rectangle1.Vertical, rectangle2.Vertical);
                return vertical.IsEmpty ? Empty : new _Rectangle(horizontal, vertical);
            }
            public static _Rectangle Union(_Rectangle rectangle1, _Rectangle rectangle2) {
                return new _Rectangle(HorizontalLine.Union(rectangle1.Horizontal, rectangle2.Horizontal), VerticalLine.Union(rectangle1.Vertical, rectangle2.Vertical));
            }
            public override bool Equals(object obj) {
                if (obj is _Rectangle) {
                    return (_Rectangle)obj == this;
                }
                if (obj is Rectangle) {
                    return (Rectangle)obj == (Rectangle)this;
                }
                return false;
            }
            public override int GetHashCode() {
                return
                unchecked(
                (int)
                ((uint)this.X ^ (((uint)this.Y << 13) | ((uint)this.Y >> 19)) ^ (((uint)this.Width << 26) | ((uint)this.Width >> 6)) ^
                 (((uint)this.Height << 7) | ((uint)this.Height >> 25))));
            }
            public override string ToString() {
                return "{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Width=" +
                       this.Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString(CultureInfo.CurrentCulture) + "}";
            }
            public void ChangeLeftWidthTopHeight(Size size, bool add) {
                this.ChangeLeftWidthTopHeight(size, add, add);
            }
            public void ChangeLeftWidthTopHeight(Size size, bool addWidth, bool addHeight) {
                this.ChangeLeftWidthTopHeight(size.Width, size.Height, addWidth, addHeight);
            }
            public void ChangeLeftWidthTopHeight(int width, int height, bool add) {
                this.ChangeLeftWidthTopHeight(width, height, add, add);
            }
            public void ChangeLeftWidthTopHeight(int width, int height, bool addWidth, bool addHeight) {
                this.ChangeLeftWidth(width, addWidth);
                this.ChangeTopHeight(height, addHeight);
            }
            public void ChangeLeftWidth(int width, bool add) {
                this.Horizontal.ChangeLeftWidth(width, add);
            }
            public void ChangeTopHeight(int height, bool add) {
                this.Vertical.ChangeTopHeight(height, add);
            }
            public bool Contains(Point point) {
                return this.Contains(point.X, point.Y);
            }
            public bool Contains(_Rectangle rectangle) {
                return this.Horizontal.Contains(rectangle.Horizontal) && this.Vertical.Contains(rectangle.Vertical);
            }
            public bool Contains(int x, int y) {
                return this.Horizontal.Contains(x) && this.Vertical.Contains(y);
            }
            public void Inflate(Size size) {
                this.Inflate(size.Width, size.Height);
            }
            public void Inflate(int width, int height) {
                this.InflateWidth(width);
                this.InflateHeight(height);
            }
            public void InflateHeight(int height) {
                this.Vertical.Inflate(height);
            }
            public void InflateWidth(int width) {
                this.Horizontal.Inflate(width);
            }
            public void Intersect(_Rectangle rectangle) {
                _Rectangle result;

                result = Intersect(rectangle, this);
                this.X = result.X;
                this.Y = result.Y;
                this.Width = result.Width;
                this.Height = result.Height;
            }
            public bool IntersectsWith(_Rectangle rectangle) {
                return Intersect(rectangle, this) != Empty;
            }
            public void Offset(Size size) {
                this.Offset(size.Width, size.Height);
            }
            public void Offset(int width, int height) {
                this.X += width;
                this.Y += height;
            }
            public Rectangle ToRectangle() {
                return (Rectangle)this;
            }
            public void Union(_Rectangle rectangle) {
                _Rectangle result;

                result = Union(rectangle, this);
                this.X = result.X;
                this.Y = result.Y;
                this.Width = result.Width;
                this.Height = result.Height;
            }
            #endregion

            #region Properties
            [Browsable(false)]
            public int Bottom {
                get {
                    return this.Vertical.Bottom;
                }
                set {
                    this.vertical.Bottom = value;
                }
            }
            public int Height {
                get {
                    return this.Vertical.Height;
                }
                set {
                    this.vertical.Height = value;
                }
            }
            [Browsable(false)]
            public HorizontalLine Horizontal {
                get {
                    return this.horizontal;
                }
                set {
                    this.horizontal = value;
                }
            }
            [Browsable(false)]
            public bool IsEmpty {
                get {
                    return this == Empty;
                }
            }
            [Browsable(false)]
            public int Left {
                get {
                    return this.X;
                }
                set {
                    this.X = value;
                }
            }
            [Browsable(false)]
            public Point Location {
                get {
                    return new Point(this.X, this.Y);
                }
                set {
                    this.X = value.X;
                    this.Y = value.Y;
                }
            }
            [Browsable(false)]
            public int Right {
                get {
                    return this.Horizontal.Right;
                }
                set {
                    this.horizontal.Right = value;
                }
            }
            [Browsable(false)]
            public Size Size {
                get {
                    return new Size(this.Width, this.Height);
                }
                set {
                    this.Width = value.Width;
                    this.Height = value.Height;
                }
            }
            [Browsable(false)]
            public int Top {
                get {
                    return this.Y;
                }
                set {
                    this.Y = value;
                }
            }
            [Browsable(false)]
            public VerticalLine Vertical {
                get {
                    return this.vertical;
                }
                set {
                    this.vertical = value;
                }
            }
            public int Width {
                get {
                    return this.Horizontal.Width;
                }
                set {
                    this.horizontal.Width = value;
                }
            }
            public int X {
                get {
                    return this.Horizontal.X;
                }
                set {
                    this.horizontal.X = value;
                }
            }
            public int Y {
                get {
                    return this.Vertical.Y;
                }
                set {
                    this.vertical.Y = value;
                }
            }
            #endregion
        }
        [ComVisible(true)]
        [Serializable]
        private struct HorizontalLine {
            #region Fields
            public static HorizontalLine Empty = new HorizontalLine();
            private int width,
                        x;
            #endregion

            #region Constructors
            public HorizontalLine(int x, int width) {
                this.x = x;
                this.width = width;
            }
            #endregion

            #region Operators
            public static bool operator ==(HorizontalLine left, HorizontalLine right) {
                return left.X == right.X && left.Width == right.Width;
            }
            public static bool operator !=(HorizontalLine left, HorizontalLine right) {
                return !(left == right);
            }
            #endregion

            #region Methods
            public static HorizontalLine FromLR(int left, int right) {
                return new HorizontalLine(left, right - left);
            }
            public static HorizontalLine Intersect(HorizontalLine horizontalLine1, HorizontalLine horizontalLine2) {
                int left, right;

                left = Math.Max(horizontalLine1.X, horizontalLine2.X);
                right = Math.Min(horizontalLine1.Right, horizontalLine2.Right);
                return right >= left ? FromLR(left, right) : Empty;
            }
            public static HorizontalLine Union(HorizontalLine horizontalLine1, HorizontalLine horizontalLine2) {
                return FromLR(Math.Min(horizontalLine1.X, horizontalLine2.X), Math.Max(horizontalLine1.Right, horizontalLine2.Right));
            }
            public override bool Equals(object obj) {
                return obj is HorizontalLine && (HorizontalLine)obj == this;
            }
            public override int GetHashCode() {
                return unchecked(this.X ^ this.Width);
            }
            public override string ToString() {
                return "{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Width=" + this.Width.ToString(CultureInfo.CurrentCulture) + "}";
            }
            public void ChangeLeftWidth(int width, bool add) {
                if (add) {
                    this.Left -= width;
                    this.Width += width;
                }
                else {
                    this.Left += width;
                    this.Width -= width;
                }
            }
            public bool Contains(int x) {
                return x >= this.X && x <= this.Right;
            }
            public bool Contains(HorizontalLine horizontalLine) {
                return horizontalLine.X >= this.X && horizontalLine.Right <= this.Right;
            }
            public void Inflate(int width) {
                this.X -= width;
                this.Width += 2 * width;
            }
            public void Intersect(HorizontalLine horizontalLine) {
                HorizontalLine result;

                result = Intersect(horizontalLine, this);
                this.X = result.X;
                this.Width = result.Width;
            }
            public bool IntersectsWith(HorizontalLine horizontalLine) {
                return Intersect(horizontalLine, this) != Empty;
            }
            public void Union(HorizontalLine horizontalLine) {
                HorizontalLine result;

                result = Union(horizontalLine, this);
                this.X = result.X;
                this.Width = result.Width;
            }
            #endregion

            #region Properties
            [Browsable(false)]
            public bool IsEmpty {
                get {
                    return this == Empty;
                }
            }
            [Browsable(false)]
            public int Left {
                get {
                    return this.X;
                }
                set {
                    this.X = value;
                }
            }
            [Browsable(false)]
            public int Right {
                get {
                    return this.X + this.Width;
                }
                set {
                    this.Width += value - this.Right;
                }
            }
            public int Width {
                get {
                    return this.width;
                }
                set {
                    this.width = value;
                }
            }
            public int X {
                get {
                    return this.x;
                }
                set {
                    this.x = value;
                }
            }
            #endregion
        }
        [ComVisible(true)]
        [Serializable]
        private struct VerticalLine {
            #region Fields
            public static VerticalLine Empty = new VerticalLine();
            private int height,
                        y;
            #endregion

            #region Constructors
            public VerticalLine(int y, int height) {
                this.y = y;
                this.height = height;
            }
            #endregion

            #region Operators
            public static bool operator ==(VerticalLine left, VerticalLine right) {
                return left.Y == right.Y && left.Height == right.Height;
            }
            public static bool operator !=(VerticalLine left, VerticalLine right) {
                return !(left == right);
            }
            #endregion

            #region Methods
            public static VerticalLine FromTB(int top, int bottom) {
                return new VerticalLine(top, bottom - top);
            }
            public static VerticalLine Intersect(VerticalLine verticalLine1, VerticalLine verticalLine2) {
                int bottom, top;

                top = Math.Max(verticalLine1.Y, verticalLine2.Y);
                bottom = Math.Min(verticalLine1.Bottom, verticalLine2.Bottom);
                return bottom >= top ? FromTB(top, bottom) : Empty;
            }
            public static VerticalLine Union(VerticalLine verticalLine1, VerticalLine verticalLine2) {
                return FromTB(Math.Min(verticalLine1.Y, verticalLine2.Y), Math.Max(verticalLine1.Bottom, verticalLine2.Bottom));
            }
            public override bool Equals(object obj) {
                return obj is VerticalLine && (VerticalLine)obj == this;
            }
            public override int GetHashCode() {
                return unchecked(this.Y ^ this.Height);
            }
            public override string ToString() {
                return "{Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString(CultureInfo.CurrentCulture) + "}";
            }
            public void ChangeTopHeight(int height, bool add) {
                if (add) {
                    this.Top -= height;
                    this.Height += height;
                }
                else {
                    this.Top += height;
                    this.Height -= height;
                }
            }
            public bool Contains(int y) {
                return y >= this.Y && y <= this.Bottom;
            }
            public bool Contains(VerticalLine verticalLine) {
                return verticalLine.Y >= this.Y && verticalLine.Bottom <= this.Bottom;
            }
            public void Inflate(int height) {
                this.Y -= height;
                this.Height += 2 * height;
            }
            public void Intersect(VerticalLine verticalLine) {
                VerticalLine result;

                result = Intersect(verticalLine, this);
                this.Y = result.Y;
                this.Height = result.Height;
            }
            public bool IntersectsWith(VerticalLine verticalLine) {
                return Intersect(verticalLine, this) != Empty;
            }
            public void Union(VerticalLine verticalLine) {
                VerticalLine result;

                result = Union(verticalLine, this);
                this.Y = result.Y;
                this.Height = result.Height;
            }
            #endregion

            #region Properties
            [Browsable(false)]
            public int Bottom {
                get {
                    return this.Y + this.Height;
                }
                set {
                    this.Height += value - this.Bottom;
                }
            }
            public int Height {
                get {
                    return this.height;
                }
                set {
                    this.height = value;
                }
            }
            [Browsable(false)]
            public bool IsEmpty {
                get {
                    return this == Empty;
                }
            }
            [Browsable(false)]
            public int Top {
                get {
                    return this.Y;
                }
                set {
                    this.Y = value;
                }
            }
            public int Y {
                get {
                    return this.y;
                }
                set {
                    this.y = value;
                }
            }
            #endregion
        }
        #endregion

        #region Methods
        public static bool ChangeMenuItemCheckState(object menuItemSender) {
            bool checkState;
            MenuItem menuItem;

            if (menuItemSender == null) {
                throw new ArgumentNullException("menuItemSender");
            }
            menuItem = (MenuItem)menuItemSender;
            checkState = !menuItem.Checked;
            menuItem.Checked = checkState;
            return checkState;
        }
        public static Size GetControlMinimumClientSize(Control control) {
            return GetControlMinimumClientRectangle(control).Size;
        }
        public static string GetSenderMenuItem_ParentMenuItem_Text(object sender) {
            if (sender == null) {
                throw new ArgumentNullException("sender");
            }
            return ((MenuItem)(((MenuItem)sender).Parent)).Text;
        }
        public static void SetListControlConvertEventArgsValue<T>(ListControlConvertEventArgs e, Func<T, string> func) {
            string value;

            if (func == null) {
                throw new ArgumentNullException("func");
            }
            value = e.ListItem as string;
            e.Value = value ?? func((T)e.ListItem);
        }
        private static _Rectangle GetControlMinimumClientRectangle(Control control) {
            bool isControlNumericUpDown,
                 useAutoSize,
                 useDock,
                 useMargin,
                 useMinimumSize;
            _Rectangle rectangle;
            ToolStrip toolStrip;

            if (control == null) {
                throw new ArgumentNullException("control");
            }
            isControlNumericUpDown = control is NumericUpDown;
            toolStrip = control as ToolStrip;
            rectangle = new _Rectangle(ShouldUseProperty(control, "Location") ? control.Location : Point.Empty, Size.Empty);
            useDock = ShouldUseProperty(control, "Dock");
            useMargin = ShouldUseProperty(control, "Margin") && control.Margin != Padding.Empty;
            useAutoSize = ShouldUseProperty(control, "AutoSize") && control.AutoSize;
            useMinimumSize = ShouldUseProperty(control, "MinimumSize") && !control.MinimumSize.IsEmpty;
            if (useDock) {
                if (!useMinimumSize && !useAutoSize && (isControlNumericUpDown || (toolStrip == null ? control.Controls.Count == 0 : toolStrip.Items.Count == 0))) {
                    if (control.Dock == DockStyle.None) {
                        rectangle.Size += control.Size;
                    }
                    else {
                        bool isControlScrollable;

                        isControlScrollable = control is ScrollableControl;
                        switch (control.Dock) {
                            case DockStyle.Top:
                                rectangle.Width += isControlScrollable ? ScrollableControlMinSize : Math.Min(ControlMinSize, control.Width);
                                rectangle.Height += control.Height;
                                break;
                            case DockStyle.Bottom:
                                rectangle.Width += isControlScrollable ? ScrollableControlMinSize : Math.Min(ControlMinSize, control.Width);
                                rectangle.ChangeTopHeight(control.Height, false);
                                break;
                            case DockStyle.Left:
                                rectangle.Width += control.Width;
                                rectangle.Height += isControlScrollable ? ScrollableControlMinSize : Math.Min(ControlMinSize, control.Height);
                                break;
                            case DockStyle.Right:
                                rectangle.ChangeLeftWidth(control.Width, false);
                                rectangle.Height += isControlScrollable ? ScrollableControlMinSize : Math.Min(ControlMinSize, control.Height);
                                break;
                            case DockStyle.Fill:
                                rectangle.Size += isControlScrollable
                                                  ? new Size(ScrollableControlMinSize, ScrollableControlMinSize)
                                                  : new Size(Math.Min(ControlMinSize, control.Width), Math.Min(ControlMinSize, control.Height));
                                break;
                        }
                    }
                }
                if (useMargin && control.Dock != DockStyle.Fill) {
                    switch (control.Dock) {
                        case DockStyle.None:
                            rectangle.ChangeLeftWidthTopHeight(control.Margin.Left, control.Margin.Top, false);
                            rectangle.Width += control.Margin.Right;
                            rectangle.Height += control.Margin.Bottom;
                            break;
                        case DockStyle.Top:
                            rectangle.Height += control.Margin.Bottom;
                            break;
                        case DockStyle.Bottom:
                            rectangle.ChangeTopHeight(control.Margin.Top, false);
                            break;
                        case DockStyle.Left:
                            rectangle.Width += control.Margin.Right;
                            break;
                        case DockStyle.Right:
                            rectangle.ChangeLeftWidth(control.Margin.Left, false);
                            break;
                    }
                }
            }
            else if (useMargin) {
                rectangle.ChangeLeftWidthTopHeight(control.Margin.Left, control.Margin.Top, false);
                rectangle.Width += control.Margin.Right;
                rectangle.Height += control.Margin.Bottom;
            }
            if (useAutoSize) {
                rectangle.Size += control.PreferredSize;
            }
            else if (useMinimumSize) {
                rectangle.Size += control.MinimumSize;
            }
            else {
                if (ShouldUseProperty(control, "Padding") && control.Padding != Padding.Empty) {
                    rectangle.Size += control.Padding.Size;
                    rectangle.Offset(-control.Padding.Left, -control.Padding.Top);
                }
                if (!isControlNumericUpDown) {
                    SplitContainer splitContainer;

                    //if (toolStrip == null) {
                    splitContainer = control as SplitContainer;
                    if (splitContainer == null) {
                        //{
                        //    List<HorizontalLine> control_controlsGroups_horizontal;
                        //    List<VerticalLine> control_controlsGroups_vertical;

                        //    control_controlsGroups_horizontal = new List<HorizontalLine>();
                        //    control_controlsGroups_vertical = new List<VerticalLine>();
                        //    foreach (Control _control in control.Controls) {
                        //        int last__control_controlGroup_index_horizontal,
                        //            last__control_controlGroup_index_vertical;
                        //        _Rectangle _control_minClientRectangle;

                        //        if (!_control.Visible) {
                        //            continue;
                        //        }
                        //        last__control_controlGroup_index_horizontal = -1;
                        //        last__control_controlGroup_index_vertical = -1;
                        //        _control_minClientRectangle = GetControlMinimumClientRectangle(_control);
                        //        for (int i = 0; i < control_controlsGroups_horizontal.Count; i++) {
                        //            HorizontalLine control_controlsGroup_horizontal;

                        //            control_controlsGroup_horizontal = control_controlsGroups_horizontal[i];
                        //            if (!control_controlsGroup_horizontal.IntersectsWith(_control_minClientRectangle.Horizontal)) {
                        //                continue;
                        //            }
                        //            control_controlsGroup_horizontal.Union(_control_minClientRectangle.Horizontal);
                        //            if (last__control_controlGroup_index_horizontal > -1) {
                        //                control_controlsGroup_horizontal.Union(control_controlsGroups_horizontal[last__control_controlGroup_index_horizontal]);
                        //                control_controlsGroups_horizontal.RemoveAt(last__control_controlGroup_index_horizontal);
                        //                i--;
                        //            }
                        //            control_controlsGroups_horizontal[i] = control_controlsGroup_horizontal;
                        //            last__control_controlGroup_index_horizontal = i;
                        //        }
                        //        for (int i = 0; i < control_controlsGroups_vertical.Count; i++) {
                        //            VerticalLine control_controlsGroup_vertical;

                        //            control_controlsGroup_vertical = control_controlsGroups_vertical[i];
                        //            if (!control_controlsGroup_vertical.IntersectsWith(_control_minClientRectangle.Vertical)) {
                        //                continue;
                        //            }
                        //            control_controlsGroup_vertical.Union(_control_minClientRectangle.Vertical);
                        //            if (last__control_controlGroup_index_vertical > -1) {
                        //                control_controlsGroup_vertical.Union(control_controlsGroups_vertical[last__control_controlGroup_index_vertical]);
                        //                control_controlsGroups_vertical.RemoveAt(last__control_controlGroup_index_vertical);
                        //                i--;
                        //            }
                        //            control_controlsGroups_vertical[i] = control_controlsGroup_vertical;
                        //            last__control_controlGroup_index_vertical = i;
                        //        }
                        //        if (last__control_controlGroup_index_horizontal == -1) {
                        //            control_controlsGroups_horizontal.Add(_control_minClientRectangle.Horizontal);
                        //        }
                        //        if (last__control_controlGroup_index_vertical == -1) {
                        //            control_controlsGroups_vertical.Add(_control_minClientRectangle.Vertical);
                        //        }
                        //    }
                        //    //control_controlsGroups_horizontal.Sort(
                        //    //                                       (horizontalLine1, horizontalLine2) =>
                        //    //                                       horizontalLine1.Left == horizontalLine2.Left
                        //    //                                       ? horizontalLine1.Width.CompareTo(horizontalLine2.Width)
                        //    //                                       : horizontalLine1.Left.CompareTo(horizontalLine2.Left));
                        //    //control_controlsGroups_vertical.Sort(
                        //    //                                     (verticalLine1, verticalLine2) =>
                        //    //                                     verticalLine1.Top == verticalLine2.Top
                        //    //                                     ? verticalLine1.Height.CompareTo(verticalLine2.Height)
                        //    //                                     : verticalLine1.Top.CompareTo(verticalLine2.Top));
                        //    //if (control_controlsGroups_horizontal.Count > 0) {
                        //    //    rectangle.Width += control_controlsGroups_horizontal[control_controlsGroups_horizontal.Count - 1].Right;
                        //    //}
                        //    //if (control_controlsGroups_vertical.Count > 0) {
                        //    //    rectangle.Height += control_controlsGroups_vertical[control_controlsGroups_vertical.Count - 1].Bottom;
                        //    //}
                        //    {
                        //        HorizontalLine control_controlsGroups_total_horizontal;
                        //        VerticalLine control_controlsGroups_total_vertical;

                        //        control_controlsGroups_total_horizontal = HorizontalLine.Empty;
                        //        control_controlsGroups_total_vertical = VerticalLine.Empty;
                        //        foreach (HorizontalLine horizontalLine in control_controlsGroups_horizontal) {
                        //            if (control_controlsGroups_total_horizontal == HorizontalLine.Empty) {
                        //                control_controlsGroups_total_horizontal = horizontalLine;
                        //            }
                        //            else {
                        //                control_controlsGroups_total_horizontal.Union(horizontalLine);
                        //            }
                        //        }
                        //        foreach (VerticalLine verticalLine in control_controlsGroups_vertical) {
                        //            if (control_controlsGroups_total_vertical == VerticalLine.Empty) {
                        //                control_controlsGroups_total_vertical = verticalLine;
                        //            }
                        //            else {
                        //                control_controlsGroups_total_vertical.Union(verticalLine);
                        //            }
                        //        }
                        //        rectangle.Width += control_controlsGroups_total_horizontal.Right;
                        //        rectangle.Height += control_controlsGroups_total_vertical.Bottom;
                        //    }
                        //}
                        {
                            Form form;

                            form = control as Form;
                            if (form == null) {
                                if (control is ListBox) {
                                    rectangle.Size += LstBox_Borders_CombinedSize;
                                }
                                else if (control is SplitterPanel) {
                                    rectangle.Size += SpltPnl_Borders_Combined;
                                }
                            }
                            else {
                                if (form.FormBorderStyle != FormBorderStyle.None) {
                                    rectangle.Size += Frm_Borders_CombinedSize;
                                }
                                if (form.Controls.Count > 0) {
                                    rectangle.Size += Frm_Padding_Size;
                                }
                                if (form.TopLevel) {
                                    Menu menu;

                                    menu = form.Menu;
                                    if (menu != null && menu.MenuItems.Count > 0) {
                                        rectangle.Height += Frm_MainMenu_Height;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        _Rectangle splitContainer_panel1_minimumSizeRectangle,
                                   splitContainer_panel2_minimumSizeRectangle;

                        splitContainer_panel1_minimumSizeRectangle = GetControlMinimumClientRectangle(splitContainer.Panel1);
                        splitContainer_panel2_minimumSizeRectangle = GetControlMinimumClientRectangle(splitContainer.Panel2);
                        if (splitContainer.Orientation == Orientation.Horizontal) {
                            rectangle.Width += Math.Max(splitContainer_panel1_minimumSizeRectangle.Width, splitContainer_panel2_minimumSizeRectangle.Width);
                            rectangle.Height += splitContainer_panel1_minimumSizeRectangle.Height + splitContainer_panel2_minimumSizeRectangle.Height;
                        }
                        else {
                            rectangle.Width += splitContainer_panel1_minimumSizeRectangle.Width + splitContainer_panel2_minimumSizeRectangle.Width;
                            rectangle.Height += Math.Max(splitContainer_panel1_minimumSizeRectangle.Height, splitContainer_panel2_minimumSizeRectangle.Height);
                        }
                        rectangle.Width += splitContainer.SplitterWidth;
                    }
                    //}
                    //else {
                    //}
                }
            }
            return rectangle;
        }
        private static bool ShouldUseProperty(Control control, string propertyName) {
            bool useProperty;
            BrowsableAttribute browsableAttribute;
            EditorBrowsableAttribute editorBrowsableAttribute;
            PropertyInfo propertyInfo;

            if (control == null) {
                throw new ArgumentNullException("control");
            }
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName");
            }
            propertyInfo = control.GetType().GetProperty(propertyName, BindingFlags_Public_Instance);
            if (propertyInfo == null) {
                return false;
            }
            browsableAttribute = null;
            editorBrowsableAttribute = null;
            useProperty = true;
            foreach (Attribute attribute in propertyInfo.GetCustomAttributes(true)) {
                if (editorBrowsableAttribute == null) {
                    editorBrowsableAttribute = attribute as EditorBrowsableAttribute;
                    if (editorBrowsableAttribute != null) {
                        useProperty = useProperty && editorBrowsableAttribute.State != EditorBrowsableState.Never;
                    }
                }
                if (browsableAttribute == null) {
                    browsableAttribute = attribute as BrowsableAttribute;
                    if (browsableAttribute != null) {
                        useProperty = useProperty && browsableAttribute.Browsable;
                    }
                }
                if (editorBrowsableAttribute != null && browsableAttribute != null) {
                    break;
                }
            }
            return useProperty;
        }
        #endregion
    }
}