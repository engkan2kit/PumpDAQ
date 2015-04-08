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
using System.Windows.Shapes;
using System.Threading;

namespace PumpDAQ
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class win_Splash : Window
    {
        public win_Splash()
        {
            InitializeComponent();
       

        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
