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
using WeldingManagerApp.Models;

namespace WeldingManagerApp
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        private WeldingManagerDbContext _context;
        public event Action<Order> OrderAdded;

        public AddOrder(WeldingManagerDbContext dbContext)
        {
            InitializeComponent();
            _context= dbContext;
        }

        private void SubmitOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            int SelectedLevel=0;
            string SelectedLevelString;
            ComboBoxItem SelectedItem=(ComboBoxItem)DifficultyChoice.SelectedItem;
            SelectedLevelString=SelectedItem.Content.ToString();
            if(SelectedLevelString == "Hard")
            {
                SelectedLevel = 3;
            }
            if (SelectedLevelString == "Medium")
            {
                SelectedLevel = 2;
            }
            if (SelectedLevelString == "Easy")
            {
                SelectedLevel = 1;
            }
            var NewOrder = new Order()
            {
                BasePrice=int.Parse(BasePriceBox.Text),
                Description=DescriptionBox.Text,
                PieceName=PieceBox.Text,
                Level=SelectedLevel,
                CustomerId=int.Parse(CustomerIdBox.Text),
                EstimatedTime=DeliverTime.SelectedDate ?? DateTime.Now,
            };
            _context.Orders.Add(NewOrder);
            _context.SaveChanges();
            OrderAdded?.Invoke(NewOrder);
            this.Close();
        }
    }
}
