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
        Thickness auto1Partenza;
        Thickness auto2Partenza;
        Thickness auto3Partenza;
        public MainWindow()
        {
            InitializeComponent();
            
            //creazione del random in modo da non doverlo ricreare ogni volta che si clicca il bottone
            r = new Random();
            //assegnazione dei margini iniziali delle immagine
            auto1Partenza = imgAuto1.Margin;
            auto2Partenza = imgAuto2.Margin;
            auto3Partenza = imgAuto3.Margin;
            //non impostiamo gli uri perchè le immagini non dovendo cambiare ma solo muoversi vengono impostate nel mainwindow
        }
        public void MetodoMovimento(Image img)   //passiamo l'immagine da muovere
        {
            try
            {
                //prendiamo il margine dell'immagine interessata
                int marginLeft = 0;
                int marginTop = 0;
                int marginBottom = 0;
                int marginRight = 0;
                this.Dispatcher.BeginInvoke(new Action(() =>        //utilizziamo il dispatcher per prendere il margine
                {
                    marginLeft = (int)img.Margin.Left;// facciamo un cast a int perchè ci restituisce un double
                    marginTop = (int)img.Margin.Top;
                    marginBottom = (int)img.Margin.Bottom;
                    marginRight = (int)img.Margin.Right;
                }));
                //facciamo un ciclo fino al margine di arrivo
                while (marginLeft < 700)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(r.Next(1, 751)));//facciamo lo sleep con il random per avere velocità casuali
                    marginLeft += 50; //finito lo sleep muoviamo l'immagine aggiungendo 50 al margine sinistro (per muoverla verso destra)
                    this.Dispatcher.BeginInvoke(new Action(() =>    //utilizziamo il dispatcher per impostare il margine
                    {
                        img.Margin = new Thickness(marginLeft, marginTop, marginBottom, marginRight);     //impostiamo il nuovo margine
                    }));
                }
                //controlliamo il nome identificativo dell'immagine passata da cui possiamo capire se si tratta della navicella 1, della navicella 2 o della navicella 3
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (img.Name.Contains("1")) //perciò in base al nome dell'immagine aggiungiamo alla lista della classifica il nome della navicella 
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
                    //controlliamo se la lista ha tre elementi, in caso affermativo vuol dire che tutti i thread sono stati eseguiti e che perciò possiamo 
                    //riattivare il bottone per avviare i thread che era stato disattivato precedentemente (disattivato appena si clicca)
                    if (lstPosizionifinali.Items.Count == 3)
                    {
                        btnInizio.IsEnabled = true;
                    }
                }));
            }
            catch (Exception ex)
            {
                throw ex; //in caso di errore lanciamo l'eccezione al metodo chiamate
            }
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
