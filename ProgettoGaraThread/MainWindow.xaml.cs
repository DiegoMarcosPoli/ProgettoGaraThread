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
using System.Threading;

namespace ProgettoGaraThread
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t1;
        Thread t2;
        Thread t3;
        Random r;
        Thickness auto1Partenza;
        Thickness auto2Partenza;
        Thickness auto3Partenza;
        public MainWindow()
        {
            InitializeComponent();
            
            r = new Random();

            auto1Partenza = imgAuto1.Margin;
            auto2Partenza = imgAuto2.Margin;
            auto3Partenza = imgAuto3.Margin;
        }
        public void MetodoMovimento(Image img)   
        {
            int marginLeft = 0;
            int marginTop = 0;
            int marginBottom = 0;
            int marginRight = 0;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                marginLeft = (int)img.Margin.Left;
                marginTop = (int)img.Margin.Top;
                marginBottom = (int)img.Margin.Bottom;
                marginRight = (int)img.Margin.Right;
            }));

            while (marginLeft < 863)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(r.Next(1, 751)));
                marginLeft += 50;
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    img.Margin = new Thickness(marginLeft, marginTop, marginBottom, marginRight);
                }));
            }

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (img.Name.Contains("1"))
                {
                    lstPosizionifinali.Items.Add("Maserati(bianca)");
                }
                else if (img.Name.Contains("2"))
                {
                    lstPosizionifinali.Items.Add("Porche(gialla)");
                }
                else if (img.Name.Contains("3"))
                {
                    lstPosizionifinali.Items.Add("Ferrari(rossa)");
                }

                if (lstPosizionifinali.Items.Count == 3)
                {
                    btnInizio.IsEnabled = true;
                }
            }));
        }
        public void MuoviAuto1()
        {
            MetodoMovimento(imgAuto1);
        }
        public void MuoviAuto2()
        {
            MetodoMovimento(imgAuto2);
        }
        public void MuoviAuto3()
        {
            MetodoMovimento(imgAuto3);
        }

        private void btnInizio_Click(object sender, RoutedEventArgs e)
        {
            btnInizio.IsEnabled = false;
            lstPosizionifinali.Items.Clear();
            imgAuto1.Margin = auto1Partenza;
            imgAuto2.Margin = auto2Partenza;
            imgAuto3.Margin = auto3Partenza;
            t1 = new Thread(new ThreadStart(MuoviAuto1));
            t2 = new Thread(new ThreadStart(MuoviAuto2));
            t3 = new Thread(new ThreadStart(MuoviAuto3));
            t1.Start();
            t3.Start();
            t2.Start();

        }
    }
}
