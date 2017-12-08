using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Net.Http.Headers;
namespace WebClient
{
    /// <summary>
    /// Interaction logic for AdminWPF.xaml
    /// </summary>
    public partial class AdminWPF : Window
    {
        public AdminWPF()
        {
            InitializeComponent();
        }
        public void retrieve_all_users()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/Admins/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicaion/json"));
            HttpResponseMessage response = client.GetAsync("users/get").Result;
            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                User_Grid.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Error:Not Found and Users");
            }
        }

        private void btn_retrieve_all(object sender, RoutedEventArgs e)
        {
            retrieve_all_users();
        }

        private void btn_retrieve_by_type(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/Admins/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicaion/json"));
            var userType = text_User_Type.Text.Trim();
            var url = "users/" + userType;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                User_Grid.ItemsSource = users;
            }
        }

        private void btn_Delete_User(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/Admins/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("apllication/json"));
            var UserType = text_User_Type_Del.Text.Trim();
            var ID = text_User_ID_Del.Text.Trim();
            var url = "userdelete/" + UserType + "/" + ID;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode + "__ The User With ID:" + ID + "and His Type is:" + UserType + "is Deleted Succesful");
            }
            else
            {
                MessageBox.Show(response.StatusCode + "__ No User With this information");
            }
        }

        private void btn_add_user(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var url = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/Admins/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Employee emp = new Employee();
            if(!string.IsNullOrEmpty(txt_Fname.Text.Trim()))
                emp.Fname = txt_Fname.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Fname.Text.Trim()))
                emp.Lname = txt_Lname.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Phone.Text.Trim()))
                emp.Password = txt_Phone.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Username.Text.Trim()))
                emp.Username = txt_Username.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Password.Password.Trim()))
                emp.Password = txt_Password.Password.Trim();
            emp.Email = txt_Email_b.Text.Trim();
            emp.UserType = changeToInteger(txt_UserType_a_u.Text.Trim());
            if (emp != null && !string.IsNullOrEmpty(emp.Fname) && !string.IsNullOrEmpty(emp.Username)&& !string.IsNullOrEmpty(emp.Password))
            {
                url = "post/" + emp.UserType;
                response = client.PostAsJsonAsync(url, emp).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("The User is added");
                    retrieve_all_users();
                }
                else
                {
                    MessageBox.Show("the User didn't added");
                }
            }
            else
            {
                MessageBox.Show("please fill the User's information to Add");
            }

        }
        private void btn_update_user(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/Admins/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "";
            Employee emp = new Employee();
            emp.Fname = txt_Fname.Text.Trim();
            emp.Lname = txt_Lname.Text.Trim();
            emp.Password = txt_Phone.Text.Trim();
            emp.Username = txt_Username.Text.Trim();
            emp.Password = txt_Password.Password.Trim();
            emp.Email = txt_Email_b.Text.Trim();
            emp.UserType = changeToInteger(txt_UserType_a_u.Text.Trim());
            emp.Id = changeToInteger(txt_Id_u.Text.Trim());
            if (emp != null && !string.IsNullOrEmpty(emp.Fname) && !string.IsNullOrEmpty(emp.Username)&& !string.IsNullOrEmpty(emp.Password))
            {
                url = "userput/" + emp.UserType + "/" + emp.Id;
                HttpResponseMessage response = client.PutAsJsonAsync(url, emp).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("The User is Updated");
                    retrieve_all_users();
                }
                else
                {
                    MessageBox.Show("the User didn't Update");
                }
            }
            else
            {
                MessageBox.Show("please fill the User's information to Update");
            }
        }
        public int changeToInteger(string s1)
        {
            int val = 0;
            if (s1 != null)
            {
                bool result1 = int.TryParse(s1.Trim(), out val);
                if (!result1)
                {
                    MessageBox.Show("U won't add User. or the user fields are empty");
                }
            }
            return val;
        }
        private void Button_SignOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainform = new MainWindow();
            this.Hide();
            mainform.Show();
        }
    }
}
