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

namespace WpfCompiti
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
        private CancellationTokenSource token = new CancellationTokenSource();
        private CancellationTokenSource token2 = new CancellationTokenSource();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (token == null)
            {
                token = new CancellationTokenSource();
            }
            Task.Factory.StartNew(() => Conteggio(token, 1000, lblRis));
        }
        private void Conteggio(CancellationTokenSource token, int max, Label lblRis)
        {
            for (int i = 0; i<=max; i++)
            {
                Dispatcher.Invoke(() => AggiornaUI(i, lblRis));
                Thread.Sleep(1000);
                Dispatcher.Invoke(() => AggiornaUI1(lblRis));
                Thread.Sleep(1000);
                if (token.Token.IsCancellationRequested)
                    break;
            }
            Dispatcher.Invoke(Finito);
        }

        private void Finito()
        {
            lblRis.Content = "Ho finito";
        }
        private void AggiornaUI (int i, Label lblRis)
        {
            lblRis.Content = $"Sto contando{i.ToString()}";
        }
        private void AggiornaUI1 (Label lblRis)
        {
            lblRis.Content = "Sto Aspettando";
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (token != null)
            {
                token.Cancel();
                token = null;
            }
        }

        private void BtnStart1_Click(object sender, RoutedEventArgs e)
        {
            int max = Convert.ToInt32(txtMax.Text);
            for (int i = 0; i <= max; i++)
            {
                if (token2 == null)
                {
                    token2 = new CancellationTokenSource();
                }
            }   
            Task.Factory.StartNew(() => Conteggio(token2, max, lblRis1));
            Dispatcher.Invoke(Finito1);
        }
        private void Finito1()
        {
            lblRis1.Content = "Ho finito";
        }
        

        private void BtnStop1_Click(object sender, RoutedEventArgs e)
        {
            if (token2 != null)
            {
                token2.Cancel();
                token2 = null;
            }
        }

        private void Grid_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
