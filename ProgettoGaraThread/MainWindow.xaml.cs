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
        //random che verrà usato successivamente per rendere casuale la velocità dei thread
        Random r;
        //margini di ogni immagine che stanno a significare il punto di partenza di ogni immagine
        Thickness navicella1Partenza;
        Thickness navicella2Partenza;
        Thickness navicella3Partenza;
        public MainWindow()
        {
            InitializeComponent();
            
            //creazione del random in modo da non doverlo ricreare ogni volta che si clicca il bottone
            r = new Random();
            //assegnazione dei margini iniziali delle immagine
            navicella1Partenza = imgAuto1.Margin;
            navicella2Partenza = imgAuto2.Margin;
            navicella3Partenza = imgAuto3.Margin;
            //non impostiamo gli uri perchè le immagini non dovendo cambiare ma solo muoversi vengono impostate nel mainwindow
        }
    }
}
