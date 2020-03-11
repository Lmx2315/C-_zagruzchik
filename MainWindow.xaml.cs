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
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;

namespace Zagruzchik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public SerialPort serialPort1 = new SerialPort();

        private void button_comport_open_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);

            if (serialPort1.IsOpen == false)
            {
                // Allow the user to set the appropriate properties.
                serialPort1.PortName = textBox_comport.Text;
                serialPort1.BaudRate = 115200;
                serialPort1.DataReceived += OnDataReceived;

                // Set the read/write timeouts
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;

                try
                {
                    if (serialPort1.IsOpen == false)
                    {
                        serialPort1.Open();
                    }
                    Debug.WriteLine("open");
                    button_comport_open.Content = "close";
                    button_comport_open.Background = Brushes.Green;
                    button_comport_send.Background = Brushes.Green;
                    // здесь может быть код еще...
                }
                catch (Exception ex)
                {
                    // что-то пошло не так и упало исключение... Выведем сообщение исключения
                    Console.WriteLine(string.Format("Port:'{0}' Error:'{1}'", serialPort1.PortName, ex.Message));
                    button_comport_open.Background = myBrush;
                }
            }
            else
            {
                serialPort1.Close();
                button_comport_open.Content = "open";
                button_comport_open.Background = Brushes.Coral;
            }
        }


        public string console_text = "";
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialDevice = sender as SerialPort;
            var buffer = new byte[serialDevice.BytesToRead];
            serialDevice.Read(buffer, 0, buffer.Length);
    
            string z = Encoding.GetEncoding(1251).GetString(buffer);//чтобы видеть русский шрифт!!!

            // for (i = 0; i < buffer.Length; i++) z = z + Convert.ToString(buffer[i]);

            // process data on the GUI thread
            Application.Current.Dispatcher.Invoke(
                new Action(() =>
                {
                    //   Debug.WriteLine("чё-то принято!");
                    console_text = console_text + z;
                    //    Debug.WriteLine(":"+ z);
                    /*
                ... do something here ...
                */
                }));
        }

        private void button_comport_send_Click(object sender, RoutedEventArgs e)
        {

        }

        public bool[] Console_form = new bool[1];
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            //   Debug.WriteLine(":" + Console_form[0]);
            if (Console_form[0] == false)
            {
                form_consol1 newForm = new form_consol1("console1");
                Console_form[0] = true;
                newForm.Show();
                newForm.Owner = this;
            }
        }

        string proshivka = "";
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string filename;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filename = openFileDialog.FileName;
                proshivka = File.ReadAllText(filename);
                console_text = console_text + proshivka;
            }
        }
    }
}
