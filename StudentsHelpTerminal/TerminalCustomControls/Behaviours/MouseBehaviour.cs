using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;


namespace TerminalCustomControls.Behaviours
{ 
    public class MouseBehaviour : Behavior<FrameworkElement>
    {
        #region MousePositionProperty
        public static readonly DependencyProperty MousePositionPointProperty = DependencyProperty.Register(
           "MousePositionPoint", typeof(Point), typeof(MouseBehaviour), new PropertyMetadata(default(Point)));

        public Point MousePositionPoint
        {
            get { return (Point)GetValue(MousePositionPointProperty); }
            set { SetValue(MousePositionPointProperty, value); }
        }
        #endregion

        #region MousePositionRelativeProperty
        public static readonly DependencyProperty MousePositionRelativeProperty = DependencyProperty.Register(
           "MousePositionRelative", typeof(Point), typeof(MouseBehaviour), new PropertyMetadata(default(Point)));

        public Point MousePositionRelative
        {
            get { return (Point)GetValue(MousePositionRelativeProperty); }
            set { SetValue(MousePositionRelativeProperty, value); }
        }
        #endregion

        #region MouseYProperty
        public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register(
           "MouseY", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public double MouseY
        {
            get { return (double)GetValue(MouseYProperty); }
            set { SetValue(MouseYProperty, value); }
        }
        #endregion

        #region MouseXProperty
        public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register(
           "MouseX", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public double MouseX
        {
            get { return (double)GetValue(MouseXProperty); }
            set { SetValue(MouseXProperty, value); }
        }
        #endregion

        #region MouseYRelativeProperty
        public static readonly DependencyProperty MouseYRelativeProperty = DependencyProperty.Register(
           "MouseYRelative", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public double MouseYRelative
        {
            get { return (double)GetValue(MouseYRelativeProperty); }
            set { SetValue(MouseYRelativeProperty, value); }
        }
        #endregion

        #region MouseXRelativeProperty
        public static readonly DependencyProperty MouseXRelativeProperty = DependencyProperty.Register(
           "MouseXRelative", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public double MouseXRelative
        {
            get { return (double)GetValue(MouseXRelativeProperty); }
            set { SetValue(MouseXRelativeProperty, value); }
        }
        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
        }

        private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            Point pos = mouseEventArgs.GetPosition(AssociatedObject);
            MouseX = pos.X;
            MouseY = pos.Y;
            MouseXRelative = pos.X / AssociatedObject.ActualWidth;
            MouseYRelative = pos.Y / AssociatedObject.ActualHeight;
            MousePositionPoint = pos;
            MousePositionRelative = new Point(MouseXRelative, MouseYRelative);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        }
    }
    
}
