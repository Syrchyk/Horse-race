using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

namespace WpfApp21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            objects = new object[5];
            objects[0] = H1;
            objects[1] = H2;
            objects[2] = H3;
            objects[3] = H4;
            objects[4] = H5;
        }

        private object[] objects;
        private int tops = 1;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(Horse, objects[i]);
            }
        }

        private void Horse(object obj)
        {
            while (Dispatcher.Invoke(() => ((ProgressBar)obj).Value < 100))
            {
                Dispatcher.Invoke(()=>((ProgressBar)obj).Value += new Random().Next(1, 10));
                Thread.Sleep(100);
            }
            AbortHorses(obj);
        }

        private void AbortHorses(object obj)
        {
            Dispatcher.Invoke(() => tb1.Text += $"{tops} rank - {((ProgressBar)obj).Name.Split('H').Last()} \t");
            Dispatcher.Invoke(()=>tops++);
            Thread.CurrentThread.Abort();
        }
    }
}
