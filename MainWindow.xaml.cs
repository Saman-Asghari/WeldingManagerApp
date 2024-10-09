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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeldingManagerDbContext Context;
        public MainWindow()
        {
            
            InitializeComponent();

            Context = new WeldingManagerDbContext();

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

        private void SubmitCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUser AddCustomer =new AddUser(Context);
            AddCustomer.ShowDialog();
        }
    }
}