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
using System.Drawing;
using System.Windows.Media.Animation;

namespace View
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        public void buttonStartClicked(object sender, EventArgs e)
        {
            draw.circle(400, 400, 200, 200, MainCanvas);
            draw.circle(750, 600, 200, 200, MainCanvas);
            draw.circle(900, 900, 200, 200, MainCanvas);
        }

        class draw
        {
            public static void circle(int x, int y, int width, int height, Canvas cv)
            {
                Ellipse circle = new Ellipse()
                {
                    Width = width,
                    Height = height,
                    Stroke = Brushes.Red,
                    Fill = Brushes.Red,
                    StrokeThickness = 6
                };

                cv.Children.Add(circle);

                circle.SetValue(Canvas.LeftProperty, (double)x);
                circle.SetValue(Canvas.TopProperty, (double)y);

            }
        }
    }


}
