using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeldingManagerApp.Models;
using WeldingManagerApp.Data;
using System.Collections.ObjectModel;


namespace WeldingManagerApp
{
    public partial class MainWindow : Window
    {
        public WeldingManagerDbContext Context;
        private ObservableCollection<Order> ArrayOrders;
        public MainWindow()
        {
            
            InitializeComponent();
            Context = new WeldingManagerDbContext();
            LoadOrders();
            

            /*Customer customer = new Customer()
            {
                Email = "mehran@",
                Name = "mehran",
                Budget = 10000000,
            };
            Context.Add(customer);
            Context.SaveChanges();
            */
        }
        public void LoadOrders()
        {
            var orderList = Context.Orders.ToList();
            ArrayOrders = new ObservableCollection<Order>(orderList);

            // Add the stack panel to the ListBox

            orderListBox.ItemsSource=ArrayOrders;
            
        }
        public void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var order = checkBox.Tag as Order;
                Context.Orders.Remove(order);
                Context.SaveChanges();
                // Remove the corresponding order from the ListBox
               ArrayOrders.Remove(order);
            }
        }

        private void SubmitCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUser AddCustomer =new AddUser(Context);
            AddCustomer.ShowDialog();
        }

        private void SubmitOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            AddOrder addOrder =new AddOrder(Context);
            addOrder.OrderAdded += OnOrderAdded;
            addOrder.ShowDialog();
        }

        private void FinishingOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishedOrder finishedOrder = new FinishedOrder(Context);
            finishedOrder.ShowDialog();
        }
        private void OnOrderAdded(Order newOrder)
        {
            ArrayOrders.Add(newOrder); // Add the new order to the ObservableCollection
        }
    }
}