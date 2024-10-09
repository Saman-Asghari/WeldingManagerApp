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
using WeldingManagerApp.Data;

namespace WeldingManagerApp
{
    /// <summary>
    /// Interaction logic for FinishedOrder.xaml
    /// </summary>
    public partial class FinishedOrder : Window
    {
        private WeldingManagerDbContext _context;
        public FinishedOrder(WeldingManagerDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var Order = _context.Orders.SingleOrDefault(o => o.OrderId == int.Parse(OrderIdBox.Text));

            if(Order != null)
            {
                Order.lastPrice=int.Parse(FinishedPriceBox.Text);
                _context.SaveChanges();
            }
            else
            {
                // Handle case where the order does not exist
                MessageBox.Show("Order not found!");
            }
            this.Close();
        }
    }
}
