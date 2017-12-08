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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Headers;

public class Employee
{
    public int Id { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int UserType { get; set; }
}

namespace WebClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Text_Username(object sender, TextChangedEventArgs e)
        {

        }
        private void Text_Password(Object sender, RoutedEventArgs args)
        {
        }
        private void Button_Login(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var emp = new Employee();
            emp.Username = UsernameLabel.Text.Trim();
            emp.Password = PasswordLabel.Password.Trim();
            emp.UserType = ComboUserType.SelectedIndex + 1;
            if (emp.UserType == 1)
            {
                var response = client.PostAsJsonAsync("Login/searchV/", emp).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("U Logged in successfuly");
                    VuserWPF vuserfrom = new VuserWPF();
                    this.Hide();
                    vuserfrom.ShowDialog();
                }
                else
                {
                    MessageBox.Show("the Username or Password in Vistor User is not Correct");
                }
            }
            else if (emp.UserType == 2)
            {
                var response = client.PostAsJsonAsync("Login/searchB/", emp).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("U Logged in successfuly");
                    BuserWPF buserform = new BuserWPF();
                    this.Hide();
                    buserform.ShowDialog();

                }
                else
                {
                    MessageBox.Show("the Username or Password in Business User is not Correct");
                }
            }
            else if (emp.UserType == 3)
            {
                var response = client.PostAsJsonAsync("Login/searchA/", emp).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("U Logged in successfuly");
                    AdminWPF adminform = new AdminWPF();
                    this.Hide();
                    adminform.ShowDialog();
                }
                else
                {
                    MessageBox.Show("the Username or Password in Admin is not Correct");
                }
            }
            else
            {
                MessageBox.Show("The is some thing wrong");
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
/*var emp = new Employee();
emp.Username = UsernameLabel.Text.Trim();
emp.Password = PasswordLabel.Password.Trim();
emp.UserType = ComboUserType.SelectedIndex + 1;
var response = client.PostAsJsonAsync("Login/search/", emp).Result;
if (response.IsSuccessStatusCode)
{
    MessageBox.Show("U Logged in successfuly");
}
else
{
    MessageBox.Show(response.StatusCode + " With this Message " + response.ReasonPhrase);
}
*/

/*public int ComboChecker(string combovalue)
{
    int result = 0;
    switch(combovalue)
    {
        case "0":
            result = 1;
            break;
        case "1":
            result = 2;
            break;
        case "2":
            result = 3;
            break;
    }
    return result;
    switch (result)
    {
        case 1:
            MessageBox.Show("its 1");
            break;
        case 2:
            MessageBox.Show("its 2");
            break;
        case 3:
            MessageBox.Show("its 3");
            break;
    }
}
*/
