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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private WeldingManagerDbContext _context;
        public AddUser(WeldingManagerDbContext dbContext)
        {
            InitializeComponent();
            _context=dbContext;
        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var NewCustomer = new Customer()
            {
                Email= EmailBox.Text,
                Name=NameBox.Text,
                Budget=int.Parse(BudgetBox.Text),
            };
            _context.Customers.Add(NewCustomer);
            _context.SaveChanges();
            this.Close();
        }
    }
}
