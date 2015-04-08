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
using System.Windows.Threading;

namespace PumpDAQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer visibleTimer;
        private DispatcherTimer fadeoutTimer;
        private SplashScreen splash;
        private int visibleTime = (4000); //milliseconds of splash visible time
        private int fadeoutTime = (1500); //milliseconds of splash fadeout time
        public MainWindow()
        {
            this.Visibility = Visibility.Hidden; 
            InitializeComponent();
            splashIn();
        }

        private void splashIn()
        {
            splash = new SplashScreen("Resources/Splash.png"); //ensure image property is set to Resource and not screen saver
            visibleTimer = new DispatcherTimer(); //timer controlling how long splash is visible
            visibleTimer.Interval = TimeSpan.FromMilliseconds(visibleTime);
            visibleTimer.Tick += showTimer_Tick; //when timer time is reached, call 'showTimer_Tick" to begin fadeout
            splash.Show(false, false); //display splash
            visibleTimer.Start();
        }
        private void showTimer_Tick(object sender, EventArgs e)
        {
            visibleTimer.Stop();
            visibleTimer = null; //clear the unused timer
            fadeoutTimer = new DispatcherTimer();
            fadeoutTimer.Interval = TimeSpan.FromMilliseconds(fadeoutTime); //a timer that runs while splash fades out and controlls when main window is displayed
            fadeoutTimer.Tick += fadeTimer_Tick; //when fadeout timer is reached, call 'fadeTimer_Tick' to show main window
            splash.Close(TimeSpan.FromMilliseconds(fadeoutTime)); //begin splash fadeout to close
            fadeoutTimer.Start();
        }


        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            fadeoutTimer.Stop();
            fadeoutTimer = null; //clear the unused timer
            splash = null; //clear the splash var
            MainWindowReady(); //call method to display main window
        }

        public void MainWindowReady()
        {
            this.Visibility = Visibility.Visible;
        }


    }
}
