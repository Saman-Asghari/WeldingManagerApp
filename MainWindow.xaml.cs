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
        public MainWindow()
        {
            
            InitializeComponent();

            using WeldingManagerDbContext context = new WeldingManagerDbContext();

            Customer customer = new Customer()
            {
                Email = "mehran@",
                Name = "mehran",
                Budget = 10000000,
            };
            context.Add(customer);
            context.SaveChanges();
        }

        private void SubmitCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow AddCustomer =new MainWindow();
        }
    }
}