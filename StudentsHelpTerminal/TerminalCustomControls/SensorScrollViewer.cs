using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TerminalCustomControls
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TerminalCustomControls"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TerminalCustomControls;assembly=TerminalCustomControls"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:SensorScrollViewer/>
    ///
    /// </summary>
    public class SensorScrollViewer : ScrollViewer
    {
        #region ScaleableProperty
        private static readonly DependencyProperty ScaleableProperty = DependencyProperty.Register(nameof(Scaleable), typeof(bool), typeof(SensorScrollViewer), new PropertyMetadata(true));

        public bool Scaleable
        {
            get => (bool)GetValue(ScaleableProperty);
            set => SetValue(ScaleableProperty, value);
        }
        #endregion

        #region ScrollableProperty
        private static readonly DependencyProperty ScrollableProperty = DependencyProperty.Register(nameof(Scrollable), typeof(bool), typeof(SensorScrollViewer), new PropertyMetadata(true));

        public bool Scrollable
        {
            get => (bool)GetValue(ScrollableProperty);
            set => SetValue(ScrollableProperty, value);
        }
        #endregion

        static SensorScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SensorScrollViewer), new FrameworkPropertyMetadata(typeof(SensorScrollViewer)));
        }

        Point previousMousePos;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!Scrollable) return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (previousMousePos == new Point()) previousMousePos = e.GetPosition(this);
                if (previousMousePos == e.GetPosition(this)) return;

                Point delta = new Point(e.GetPosition(this).X - previousMousePos.X,
                    e.GetPosition(this).Y - previousMousePos.Y);

                this.ScrollToHorizontalOffset(HorizontalOffset - delta.X);
                this.ScrollToVerticalOffset(VerticalOffset - delta.Y);
                previousMousePos = e.GetPosition(this);
            }
            else { previousMousePos = new Point(); }       
        }
    }
}
