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


namespace WeldingManagerApp
{
    public partial class MainWindow : Window
    {
        public WeldingManagerDbContext Context;
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
            var orders = Context.Orders.ToList();
            foreach (var order in orders)
            {
                var listItem = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
                var checkBox = new CheckBox
                {
                    Content = order.Description, // Adjust this to your order's property
                    Tag = order, // Store the order in the Tag for reference
                    Margin = new Thickness(5)
                };
                checkBox.Checked += CheckBox_Checked;

                // Add checkbox to the stack panel
                listItem.Children.Add(checkBox);
                // Add the stack panel to the ListBox
                
                orderListBox.Items.Add(listItem);
            }
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
                orderListBox.Items.Remove((StackPanel)checkBox.Parent);
                
               
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
            addOrder.ShowDialog();
        }

        private void FinishingOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishedOrder finishedOrder = new FinishedOrder(Context);
            finishedOrder.ShowDialog();
        }
    }
}