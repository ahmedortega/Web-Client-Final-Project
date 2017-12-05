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

namespace WebClient
{
    /// <summary>
    /// Interaction logic for BuserWPF.xaml
    /// </summary>
    public partial class BuserWPF : Window
    {
        public BuserWPF()
        {
            InitializeComponent();
        }
        public void RetriveArticles()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/busers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("articles/get").Result;
            if (response.IsSuccessStatusCode)
            {
                var articles = response.Content.ReadAsAsync<IEnumerable<PocoArticles>>().Result;
                UserGrid.ItemsSource = articles;
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }
        }

        private void Button_Get_Articles(object sender, RoutedEventArgs e)
        {
            RetriveArticles();
        }

        private void Button_Get_Serial(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/busers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var id = txt_articleSerial.Text.Trim();
            var url = "articles/getbyid/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var article = response.Content.ReadAsAsync<PocoArticles>().Result;
                List<PocoArticles> articlesList = new List<PocoArticles>();
                articlesList.Add(article);
                UserGrid.ItemsSource = articlesList;
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }

        }
        private void Button_Delete_Article(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/busers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var id = txt_articleSerial.Text.Trim();
            var url = "articles/delete/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("The Article with this id: " + id + " Is Deleted");
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }
        }
        private void Button_SignOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainform = new MainWindow();
            this.Hide();
            mainform.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_add_article(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/busers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var id = txt_articleSerial.Text.Trim();
            PocoArticles article = new PocoArticles();

            if (!string.IsNullOrEmpty(txt_Title.Text.Trim()))
                article.title = txt_Title.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Subject.Text.Trim()))
                article.subject = txt_Subject.Text.Trim();
            if ( !string.IsNullOrEmpty(txt_Fname.Text.Trim()) )
                article.authorFname = txt_Fname.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Lname.Text.Trim()))
                article.authorLname = txt_Lname.Text.Trim();
            article.authorWorkYears = changeToInteger(txt_work.Text.Trim());
            article.authorId = changeToInteger(txt_A_id.Text.Trim());
            article.authorBirthYear = changeToInteger(txt_birth.Text.Trim());
            RetriveArticles();

            HttpResponseMessage response = client.PostAsJsonAsync("articles/post", article).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("The Article with this id: " + id + " Is Added");
                RetriveArticles();
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
                RetriveArticles();
            }
            RetriveArticles();
            clearTests();
        }
        /*
        bool result1 = int.TryParse(txt_work.Text.Trim(), out int val1);
        if (result1)
        {
            article.authorWorkYears = val1;
        }
        else
        {
            MessageBox.Show("the WorkBirth is the problem it might br Empty");
        }
        bool result2 = int.TryParse(txt_birth.Text.Trim(), out int val2);
        if (result2)
        {
            article.authorBirthYear = val2;
        }
        else
        {
            MessageBox.Show("the BirthYear is the problem it might br Empty");
        }
        bool result3 = int.TryParse(txt_A_id.Text.Trim(), out int val3);
        if (result3)
        {
            article.authorId = val3;
        }
        else
        {
            MessageBox.Show("the Author ID is the problem it might br Empty");
        }
        */

        private void Button_put_article(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/busers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var id = txt_articleSerial.Text.Trim();
            PocoArticles article = new PocoArticles();
            article.title = txt_Title.Text.Trim();
            article.subject = txt_Subject.Text.Trim();
            article.authorFname = txt_Fname.Text.Trim();
            article.authorLname = txt_Lname.Text.Trim();
            //int.TryParse(txt_birth, out article.authorBirthYear);
            article.authorWorkYears = changeToInteger(txt_work.Text.Trim());
            article.authorId = changeToInteger(txt_A_id.Text.Trim());
            article.authorBirthYear = changeToInteger(txt_birth.Text.Trim());
            var url = "articles/put/" + id;
            HttpResponseMessage response = client.PutAsJsonAsync(url, article).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("The Article with this id: " + id + " Is Updated");
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }
            RetriveArticles();
            clearTests();
        }

        private void txt_A_id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_A_id.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txt_A_id.Text = txt_A_id.Text.Remove(txt_A_id.Text.Length - 1);
            }
        }

        private void txt_birth_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*int.TryParse(txt_birth.Text.Trim(),out int val);
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_birth.Text.Trim(), "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txt_A_id.Text = txt_A_id.Text.Remove(txt_birth.Text.Length -1);
            }
            */
        }

        private void txt_work_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if (System.Text.RegularExpressions.Regex.IsMatch(txt_work.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txt_A_id.Text = txt_A_id.Text.Remove(txt_work.Text.Length - 1);
            }
            */
        }
        public void clearTests()
        {
            txt_articleSerial.Text = "";
            txt_Fname.Text = "";
            txt_Lname.Text = "";
            txt_Subject.Text = "";
            txt_Title.Text = "";
            txt_work.Text = "";
            txt_A_id.Text = "";
            txt_birth.Text = "";
        }
        public int changeToInteger(string s1)
        {
            int val = 0;
            if (s1 != null)
            {
                bool result1 = int.TryParse(s1.Trim(), out val);
                if (!result1)
                {
                    MessageBox.Show("U won't add Author. or the author fields are empty");
                }
            }
            return val;
        }
        private void Text_Password(Object sender, RoutedEventArgs args)
        {
        }

        private void Button_update_profile(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/busers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var password = PasswordLabel.Password.Trim();
            BUser buser = new BUser();

            if (!string.IsNullOrEmpty(text_BFname.Text.Trim()))
                buser.Fname = text_BFname.Text.Trim();
            if (!string.IsNullOrEmpty(text_BLname.Text.Trim()))
                buser.Lname = text_BLname.Text.Trim();
            if (!string.IsNullOrEmpty(text_BEmail.Text.Trim()))
               buser.Email = text_BEmail.Text.Trim();
            if (!string.IsNullOrEmpty(text_BPhone.Text.Trim()))
                buser.Phone = text_BPhone.Text.Trim();
            if (!string.IsNullOrEmpty(text_BUsername.Text.Trim()))
                buser.Username = text_BUsername.Text.Trim();
            buser.Password = B_Password.Password.Trim();
            var url = "articles/put/" + password;
            HttpResponseMessage response = client.PutAsJsonAsync(url, buser).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Ur Profile is Updated , Please sign in again");
                this.Hide();
                MainWindow main = new MainWindow();
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }
        }
    }
}
