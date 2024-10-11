using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfficeOpenXml;
using WeldingManagerApp.Data;
using WeldingManagerApp.Models;

namespace WeldingManagerApp
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        private ObservableCollection<Order> Orders;
        private WeldingManagerDbContext _context;

        public Report(WeldingManagerDbContext dbcontext)
        {
            InitializeComponent();
            Orders = new ObservableCollection<Order>(); // Initialize the collection
            _context = dbcontext;

        }

        private void FilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(OrderIdBox.Text))
            {
                GlobalFilter();
            }
            else
            {
                int orderId=int.Parse(OrderIdBox.Text);
                var order=_context.Orders.First(o => o.OrderId == orderId);
                string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string folderPath = System.IO.Path.Combine(baseDirectory, "Orders Photos");
                CreateOrderImage(order, folderPath);
            }
        }
        private void GlobalFilter() 
        {
            if (string.IsNullOrEmpty(CustomerIdBox.Text))
            {
                DatePriceFilter();
            }
            else
            {
                DateTime? StartDate = StartDateDatePicker.SelectedDate;
                DateTime? EndDate = EndDateDatePicker.SelectedDate;

                int MinPrice = int.Parse(StartPriceBox.Text);
                int MaxPrice = int.Parse(EndPriceBox.Text);
                int UserId = int.Parse(CustomerIdBox.Text);
                var FilteredOrders = _context.Orders.Where(o => o.EndTime > StartDate.Value
                                                     && o.EndTime < EndDate.Value
                                                     && o.lastPrice >= MinPrice
                                                     && o.lastPrice <= MaxPrice
                                                     && o.CustomerId== UserId).ToList();
                if (FilteredOrders.Any())
                {
                    SaveOrdersToExcel(FilteredOrders, StartDate.Value, EndDate.Value, MinPrice, MaxPrice,UserId);
                }
                else
                {
                    MessageBox.Show("No orders found for the selected criteria.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void DatePriceFilter()
        {
            DateTime? StartDate=StartDateDatePicker.SelectedDate;
            DateTime? EndDate = EndDateDatePicker.SelectedDate;

            int MinPrice = int.Parse(StartPriceBox.Text);
            int MaxPrice=int.Parse(EndPriceBox.Text);

            var FilteredOrders = _context.Orders.Where(o => o.EstimatedTime > StartDate.Value
                                                     && o.EstimatedTime < EndDate.Value
                                                     && o.lastPrice >= MinPrice
                                                     && o.lastPrice <= MaxPrice).ToList();
            if (FilteredOrders.Any())
            {
                SaveOrdersToExcel2(FilteredOrders, StartDate.Value, EndDate.Value, MinPrice, MaxPrice);
            }
            else
            {
                MessageBox.Show("No orders found for the selected criteria.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        
        private void SaveOrdersToExcel2(List<Order> orders, DateTime startDate, DateTime endDate, int minPrice, int maxPrice)
        {
            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(baseDirectory, "Report Data");

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // Create the folder if it doesn't exist
            }

            string fileName = $"Orders_{startDate:yyyy-MM-dd}_to_{endDate:yyyy-MM-dd}_Price_{minPrice}_to_{maxPrice}.xlsx";

            string filePath = System.IO.Path.Combine(folderPath, fileName);

            using(ExcelPackage package = new ExcelPackage()) 
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Orders");

                worksheet.Cells[1, 1].Value = "Order ID";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "PieceName";
                worksheet.Cells[1, 4].Value = "Difficulty";
                worksheet.Cells[1, 5].Value = "Base price";
                worksheet.Cells[1, 6].Value = "Last price";
                worksheet.Cells[1, 7].Value = "Estimated time";
                worksheet.Cells[1, 8].Value = "Customer Id";

                int row = 2;
                foreach(var order in orders)
                {
                    worksheet.Cells[row, 1].Value=order.OrderId;
                    worksheet.Cells[row, 2].Value = order.Description;
                    worksheet.Cells[row, 3].Value = order.PieceName;
                    worksheet.Cells[row, 4].Value = order.Level;
                    worksheet.Cells[row, 5].Value = order.BasePrice;
                    worksheet.Cells[row, 6].Value = order.lastPrice;
                    worksheet.Cells[row, 7].Value = order.EstimatedTime;
                    worksheet.Cells[row, 7].Style.Numberformat.Format = "yyyy-MM-dd";
                    worksheet.Cells[row, 8].Value = order.CustomerId;
                    row++;

                }

                FileInfo fileInfo=new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }

        private void SaveOrdersToExcel(List<Order> orders, DateTime startDate, DateTime endDate, int minPrice, int maxPrice,int UserId)
        {
            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(baseDirectory, "Report Data");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // Create the folder if it doesn't exist
            }

            string fileName = $"Orders_{startDate:yyyy-MM-dd}_to_{endDate:yyyy-MM-dd}_Price_{minPrice}_to_{maxPrice}_UserId_{UserId}.xlsx";

            string filePath = System.IO.Path.Combine(folderPath, fileName);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Orders");

                worksheet.Cells[1, 1].Value = "Order ID";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "PieceName";
                worksheet.Cells[1, 4].Value = "Difficulty";
                worksheet.Cells[1, 5].Value = "Base price";
                worksheet.Cells[1, 6].Value = "Last price";
                worksheet.Cells[1, 7].Value = "Estimated time";
                worksheet.Cells[1, 8].Value = "Customer Id";

                int row = 2;
                foreach (var order in orders)
                {
                    worksheet.Cells[row, 1].Value = order.OrderId;
                    worksheet.Cells[row, 2].Value = order.Description;
                    worksheet.Cells[row, 3].Value = order.PieceName;
                    worksheet.Cells[row, 4].Value = order.Level;
                    worksheet.Cells[row, 5].Value = order.BasePrice;
                    worksheet.Cells[row, 6].Value = order.lastPrice;
                    worksheet.Cells[row, 7].Value = order.EstimatedTime;
                    worksheet.Cells[row, 7].Style.Numberformat.Format = "yyyy-MM-dd";
                    worksheet.Cells[row, 8].Value = order.CustomerId;
                    row++;

                }

                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }


        private void CreateOrderImage(Order order, string folderPath)
        {
            // Image size and settings
            int width = 600;
            int height = 400;
            using var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);

            // Background color
            graphics.Clear(System.Drawing.Color.White); // Use System.Drawing.Color

            // Set text font and brush
            Font titleFont = new System.Drawing.Font(new System.Drawing.FontFamily("Arial"), 20, System.Drawing.FontStyle.Bold); // Fully qualify FontFamily
            Font textFont = new System.Drawing.Font(new System.Drawing.FontFamily("Arial"), 16); // Fully qualify FontFamily
            System.Drawing.Brush titleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkBlue); // Fully qualify Brush
            System.Drawing.Brush textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black); // Fully qualify Brush
            // Draw title
            graphics.DrawString($"Order Details", titleFont, titleBrush, new PointF(10, 10));

            // Draw order properties with spacing
            graphics.DrawString($"Order ID: {order.OrderId}", textFont, textBrush, new PointF(10, 60));
            graphics.DrawString($"Description: {order.Description}", textFont, textBrush, new PointF(10, 100));
            graphics.DrawString($"Base Price: {order.BasePrice:C}", textFont, textBrush, new PointF(10, 140));
            graphics.DrawString($"Finished Price: {order.lastPrice:C}", textFont, textBrush, new PointF(10, 180));
            graphics.DrawString($"Estimated Date: {order.EstimatedTime:yyyy-MM-dd}", textFont, textBrush, new PointF(10, 220));
            graphics.DrawString($"Finished Date: {order.EndTime:yyyy-MM-dd}", textFont, textBrush, new PointF(10, 260));
            graphics.DrawString($"Customer ID: {order.CustomerId}", textFont, textBrush, new PointF(10, 300));

            // Draw a border around the image
            using System.Drawing.Pen borderPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3); // Use System.Drawing.Pen
            graphics.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);

            // Save the image
            string imageFilePath = System.IO.Path.Combine(folderPath, $"Order_{order.OrderId}.png");
            bitmap.Save(imageFilePath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
