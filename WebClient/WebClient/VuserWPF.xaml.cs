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
    /// Interaction logic for VuserWPF.xaml
    /// </summary>
    public partial class VuserWPF : Window
    {
        public VuserWPF()
        {
            InitializeComponent();
        }
        public void RetriveArticles()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/vusers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("articles/get").Result;
            if(response.IsSuccessStatusCode)
            {
                var articles = response.Content.ReadAsAsync<IEnumerable<PocoArticles>>().Result;
                UserGrid.ItemsSource = articles;
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Get_Articles(object sender, RoutedEventArgs e)
        {
            RetriveArticles();
        }

        private void Button_Get_ArticleByName(object sender, RoutedEventArgs e)
        {
            string nameofArticle = txt_articleName.Text.ToLower().Trim();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/vusers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var url = "articles/" + nameofArticle;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var articles = response.Content.ReadAsAsync<PocoArticles> ().Result;
                List<PocoArticles> articleList = new List<PocoArticles>();
                articleList.Add(articles);
                UserGrid.ItemsSource = articleList;
            }
            else
            {
                MessageBox.Show(response.StatusCode + response.ReasonPhrase);
            }
        }

        private void Button_Get_ArticleByAuthorName(object sender, RoutedEventArgs e)
        {
            string Authorname = txt_authorname.Text.ToLower().Trim();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/JournalProjectWebApp/vusers/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var url = "articles/author/" + Authorname;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                var articles = response.Content.ReadAsAsync<IEnumerable<PocoArticles>>().Result;
                UserGrid.ItemsSource = articles;

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
    }
}
