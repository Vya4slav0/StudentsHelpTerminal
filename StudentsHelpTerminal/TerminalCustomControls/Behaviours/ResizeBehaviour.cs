using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace TerminalCustomControls.Behaviours
{
    public class ResizeBehaviour : Behavior<FrameworkElement>
    {
        #region AspectRatioYXProperty
        public static readonly DependencyProperty AspectRatioYXProperty = DependencyProperty.Register(
            "AspectRatioYX", typeof(double), typeof(ResizeBehaviour), new PropertyMetadata(default(double)));

        public double AspectRatioYX
        {
            get { return (double)GetValue(AspectRatioYXProperty); }
            set { SetValue(AspectRatioYXProperty, value); }
        }
        #endregion

        #region AspectRatioXYProperty
        public static readonly DependencyProperty AspectRatioXYProperty = DependencyProperty.Register(
            "AspectRatioXY", typeof(double), typeof(ResizeBehaviour), new PropertyMetadata(default(double)));

        public double AspectRatioXY
        {
            get { return (double)GetValue(AspectRatioXYProperty); }
            set { SetValue(AspectRatioXYProperty, value); }
        }
        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.SizeChanged += AssociatedObjectOnMouseMove;
        }

        private void AssociatedObjectOnMouseMove(object sender, RoutedEventArgs args)
        {
            AspectRatioYX = AssociatedObject.ActualHeight / AssociatedObject.ActualWidth;
            AspectRatioXY = AssociatedObject.ActualWidth / AssociatedObject.ActualHeight;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        }
    }
}
