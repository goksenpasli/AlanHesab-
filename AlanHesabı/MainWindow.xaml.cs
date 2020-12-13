using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlanHesabı
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Hesaplama();
            ((VisualBrush)rct.Fill).Visual = Grid;
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dpimultiplier = PresentationSource.FromVisual(Application.Current.Windows[0]).CompositionTarget.TransformToDevice.M11;
            var hesaplama = DataContext as Hesaplama;
            var x = e.GetPosition(Şekil).X;
            var y = e.GetPosition(Şekil).Y;
            hesaplama?.Noktalar.Add(new Point(x, y));
            PathFigure figure = new PathFigure
            {
                StartPoint = new Point(x, y)
            };
            PathGeometry geometry = new PathGeometry();
            PolyLineSegment linesegment = new PolyLineSegment
            {
                Points = new PointCollection(hesaplama?.Noktalar)
            };
            figure.Segments.Add(linesegment);
            geometry.Figures.Add(figure);
            geometry.Freeze();
            hesaplama.GeometryData = geometry;
            hesaplama.Alan = geometry.GetArea() / (96 * dpimultiplier) / (96 * dpimultiplier) * 2.54 * 2.54 * 2.54 * 72 / (96 * dpimultiplier);
        }

        private void Grid_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VisualBrush b = (VisualBrush)rct.Fill;
            Point pos = e.MouseDevice.GetPosition(Şekil);
            Rect viewBox = b.Viewbox;
            double xoffset = viewBox.Width / 2.0;
            double yoffset = viewBox.Height / 2.0;
            viewBox.X = pos.X - xoffset;
            viewBox.Y = pos.Y - yoffset;
            b.Viewbox = viewBox;
            Canvas.SetLeft(cnv, pos.X - (rct.Width / 2));
            Canvas.SetTop(cnv, pos.Y - (rct.Height / 2));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => CbRenk.SelectedIndex = 7;
    }
}