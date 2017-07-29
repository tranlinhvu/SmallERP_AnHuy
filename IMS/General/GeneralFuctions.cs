using IMS.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace IMS.General
{
    class GeneralFuctions
    {

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj,string name) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T &&
                          (child as FrameworkElement).Name.Equals(name))
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child, name))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        public static int DeleteTable(string tableName, int id)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return -1;

            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteTable(tableName, id);

            if (result < 0)
            {
                MessageBox.Show("Không thể xóa do lỗi dữ liệu!", "IMS - Thông báo lỗi");                
            }
            else if (result == 0)
            {
                MessageBox.Show("Không thể xóa vì dòng dữ liệu có liên quan đối tượng khác", "IMS - Thông báo lỗi");
            }
            return result;
        }

        public static string GetExpiryEx(string refNoRaw, string expiryRule)
        {
            try
            {
                expiryRule.Replace(" ", "");

                string[] s = expiryRule.Split(',');

                if (s[2] == "0")
                {
                    return UString.Mid(refNoRaw, refNoRaw.Length - int.Parse(s[1]) - int.Parse(s[0]) + 1, int.Parse(s[0]));
                }
                else
                {
                    return UString.Mid(refNoRaw, refNoRaw.Length - int.Parse(s[1]), int.Parse(s[0]));
                }
            }
            catch
            {
                return "";
            }
        }

        public static string GetLottNo(string refNoRaw, string lottNoRule)
        {
            try
            {
                lottNoRule.Replace(" ", "");

                string[] s = lottNoRule.Split(',');

                if (s[2] == "0")
                {
                    return UString.Mid(refNoRaw, refNoRaw.Length - int.Parse(s[1]) - int.Parse(s[0]) + 1, int.Parse(s[0]));
                }
                else
                {
                    return UString.Mid(refNoRaw, refNoRaw.Length - int.Parse(s[1]), int.Parse(s[0]));
                }
            }
            catch
            {
                return "";
            }
        }
    }
    class GeneralParams
    {
        public static string ProductPurchaseOrder = "Đơn đặt mua hàng"; //Đơn đặt mua hàng
        public static string ProductPurchase = "Mua hàng"; //Mua thông thường
        public static string ProductReturn = "Nhận hàng trả"; //Nhận trả hàng

        public static string ProductSale = "Bán hàng"; //Bán hàng
        public static string ProductGiven = "Ký gửi"; //Ký gửi
        public static string ProductSaleOff = "Hàng tặng"; //Hàng tặng
        public static string ProductTraining = "Training"; //Training

        public static string purchaseKind = "Mua hàng"; //Mua hàng
        public static string saleKind = "Bán hàng"; //Bán hàng

        public static bool inputEdit = false;
    }
}
