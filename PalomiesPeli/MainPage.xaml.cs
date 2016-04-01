using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PalomiesPeli
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // butterfly
        private Palomies palomies;
        // game loop timer
        private DispatcherTimer timer;
        // which keys are pressed
        
        private bool RightPressed;
        private bool LeftPressed;


        public MainPage()
        {
            this.InitializeComponent();


            ApplicationView.PreferredLaunchWindowingMode
            = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1024, 720);

            // key listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            CreatePalomies();
            StartGame();
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    LeftPressed = false;
                    break;
                case VirtualKey.Right:
                    RightPressed = false;
                    break;
                default:
                    break;

            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    LeftPressed = true;
                    break;
                case VirtualKey.Right:
                    RightPressed = true;
                    break;
                default:
                    break;

            }
        }

        private void CreatePalomies()
        {
            palomies = new Palomies
            {
                LocationX = MyCanvas.Width / 2 - 75,
                LocationY = MyCanvas.Height / 2 - 66
            };
            // add to canvas
            MyCanvas.Children.Add(palomies);
            // show in right location
            palomies.SetLocation();
        }
        private void StartGame()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Start(); // stop!

        }
        private void Timer_Tick(object sender, object e)
        {
            // move palomies
            if (LeftPressed) palomies.Move(-1);
            if (RightPressed) palomies.Move(1);
            // update palomies location
            palomies.SetLocation();
        }
    }
}
