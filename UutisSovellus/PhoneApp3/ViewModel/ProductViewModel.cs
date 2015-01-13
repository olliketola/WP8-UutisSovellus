using PhoneApp3.Model;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;
using System.Xml;



namespace PhoneApp3.ViewModel
{
    public class ProductViewModel
    {

        // Luodaan product luokasta kokoelma, joka voidaan näyttä esim. listboxissa.
        private ObservableCollection<Product> _Products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
            }
        }
        //  Kotimaan uutiseille
        private ObservableCollection<Product2> _Products2 = new ObservableCollection<Product2>();
        public ObservableCollection<Product2> Products2
        {
            get
            {
                return _Products2;
            }
            set
            {
                _Products2 = value;
            }
        }

        //Ulkomaan uutiset
        private ObservableCollection<Product3> _Products3 = new ObservableCollection<Product3>();
        public ObservableCollection<Product3> Products3
        {
            get
            {
                return _Products3;
            }
            set
            {
                _Products3 = value;
            }

        }

        //webclient jolla haetaan data
        public void GetRssFeed(string url)
        {

            var client = new WebClient();
            client.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    string data = "";
                    data = e.Result.ToString();
                   
                    handle(data);

                }
                else
                {
                    //virheen sattuessa
                    MessageBox.Show(e.Error.ToString());
                }
            };

            client.DownloadStringAsync(new Uri(url));


        }


        //Datan käsittelijä jota kutsutaan kun datan haku on suoritettu
        public void handle(string data)
        {
            //luodaan xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            //haetaan tarvittavat nodelistat
            XmlNodeList itemit = doc.GetElementsByTagName("item");
            XmlNodeList category = doc.GetElementsByTagName("category");
            XmlNodeList titlet = doc.GetElementsByTagName("title");
            XmlNodeList desc = doc.GetElementsByTagName("description");
            XmlNodeList link = doc.GetElementsByTagName("link");
            XmlNodeList kuva = doc.GetElementsByTagName("enclosure");
           

            string asd = category[0].InnerText;

           // MessageBox.Show(kuva[0].Attributes[0].NodeValue.ToString());
          
            Product product;
            Product2 product2;
            Product3 product3;

            try
            {
                int count = 0;

                //Haetaan valitun kategorian uutiset ja tallennetaan ne kokoelmaan
                    for (int i = 0; i < itemit.Count; i++)
                    {
                        count++;
                        if (asd == "Tuoreimmat uutiset") {

                            
                            product = new Product();
                            product.Linkki = new Uri(link[i+1].InnerText);
                            product.Name = titlet[i + 1].InnerText;
                            product.Uutinen = desc[i + 1].InnerText;
                            if (itemit[i].SelectSingleNode("enclosure") != null)
                            {
                                    product.kuva = kuva[count-1].Attributes[0].NodeValue.ToString();
                            }
                            else
                            {

                                count--;
                            }

                            Products.Add(product);

                        }

                       var kategoriat = itemit[i].SelectNodes("category");
                       for (int j = 0; j < kategoriat.Count; j++) {

                           if (kategoriat[j].InnerText == "Ulkomaat")
                           {
                               
                               product3 = new Product3();
                               product3.Name3 = titlet[i + 1].InnerText;
                               product3.Linkki3 = new Uri(link[i+1].InnerText);
                               product3.Uutinen3 = desc[i + 1].InnerText;
                               if (itemit[i].SelectSingleNode("enclosure") != null)
                               {
                                   product3.kuva3 = kuva[count-1].Attributes[0].NodeValue.ToString();
                               }
                           
                               Products3.Add(product3);

                           }
                           if (kategoriat[j].InnerText == "Kotimaa") 
                           {
                              
                               product2 = new Product2();
                               product2.Name2 = titlet[i + 1].InnerText;
                               product2.Uutinen2 = desc[i + 1].InnerText;
                               product2.Linkki2 = new Uri(link[i+1].InnerText);
                               if (itemit[i].SelectSingleNode("enclosure") != null)
                               {
                                   product2.kuva2 = kuva[count-1].Attributes[0].NodeValue.ToString();
                               }
                           
                               Products2.Add(product2);
                           
                           
                           }
                        }//for
                      }//for

   
            }//try
            catch (Exception e)
            {
              
            }

        }

    }
}