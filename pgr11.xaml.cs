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
using System.ComponentModel;
using System.Threading;

namespace Zagruzchik
{
    /// <summary>
    /// Логика взаимодействия для pgr11.xaml
    /// </summary>
    public partial class pgr11 : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public pgr11()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //          dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            dispatcherTimer.Start();
        }

        

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            if (main != null)
            {
                pbStatus.Value = (double)main.Progress_send;
            }
        }


    }
}
