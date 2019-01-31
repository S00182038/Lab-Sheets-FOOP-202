using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
/*
    Arkadiusz Lesica
    Lab Sheet 1
    27/01/2019
 */
namespace Lab_Sheet_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declare class level variables
        private static Random gen = new Random();
        List<Band> listBands;
        List<Band> listFiltered;
        //file save in debug folder
        private const string Path = "bands.json";

        public MainWindow()
        {
            InitializeComponent();
        }
        //window loaded, initialise list of members & combobox
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBands = new List<Band>();
            //sorting by Band Name
            listBands.Sort();
            //Display
            listBands = GetBands();
            lbxBands.ItemsSource = listBands;
            //populate combo box 
            cbxGenre.ItemsSource = new string[] {"All", "Pop", "Rock", "Indie" };
            cbxGenre.SelectedIndex = 0;
        }
        //Creating  Bands data
        public List<Band> GetBands()
        {
            List<Band> Bands = new List<Band>();
            //Bands
            PopBand b1 = new PopBand(){BandName="Marron 5", YearFormed=2001,Members= "Adam Levine, Jesse Carmichael, Mickey Madden"};
            PopBand b2 = new PopBand() { BandName = "U 2", YearFormed = 1976, Members = "Adam Levine, Jesse Carmichael, Mickey Madden"};
            RockBand rb1 = new RockBand() { BandName = "Muse", YearFormed = 1994, Members = "Matt Bellamy, Dominic Howard, Chris Wolstenholme",};
            RockBand rb2 = new RockBand() { BandName = "Led Zeppelin", YearFormed = 1968, Members = "Matt Bellamy, Dominic Howard, Chris Wolstenholme", };
            IndieBand ib1 = new IndieBand() { BandName = "Steve Adey", YearFormed = 2006, Members = " Steve Adey", };
            IndieBand ib2 = new IndieBand() { BandName = "The Killers", YearFormed = 2001, Members = "Tommy Marth, Ray Suen", };
            //Albums
            Album b1_1 = new Album() { AlbumName = "Songs About Jane", ReleasedYear = GenerateRandomDate(), Sales=RandomNumberSales(2000000,500000) };
            Album b1_2 = new Album() { AlbumName = "Hands All Over", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 4000000) };
            Album b2_1 = new Album() { AlbumName = "Songs of Experiencee", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(2000000, 500000) };
            Album b2_2 = new Album() { AlbumName = "Songs of Innocence", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 4000000) };
            Album rb1_1 = new Album() { AlbumName = "The 2nd Law", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 500000) };
            Album rb1_2 = new Album() { AlbumName = "Origin of Symmetry", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 4000000) };
            Album rb2_1 = new Album() { AlbumName = "Physical Graffiti", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(2000000, 500000) };
            Album rb2_2 = new Album() { AlbumName = "Houses of the Holy", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 4000000) };
            Album ib1_1 = new Album() { AlbumName = "The Tower of Silence", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 500000) };
            Album ib1_2 = new Album() { AlbumName = "All Things Real", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(1000000, 4022900) };
            Album ib2_1 = new Album() { AlbumName = "Battle Born", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(8600000, 9000000) };
            Album ib2_2 = new Album() { AlbumName = "Sam's Town", ReleasedYear = GenerateRandomDate(), Sales = RandomNumberSales(3000000, 8000000) };
            //Add bands to list
            Bands.Add(b1);
            Bands.Add(b2);
            Bands.Add(rb1);
            Bands.Add(rb2);
            Bands.Add(ib1);
            Bands.Add(ib2);
           //Add albums to bands
            b1.Albums[0] = b1_1;
            b1.Albums[1] = b1_2;
            b2.Albums[0] = b2_1;
            b2.Albums[1] = b2_2;
            rb1.Albums[0] = rb1_1;
            rb1.Albums[1] = rb1_2;
            rb2.Albums[0] = rb2_1;
            rb2.Albums[1] = rb2_2;
            ib1.Albums[0] = ib1_1;
            ib1.Albums[1] = ib1_2;
            ib2.Albums[0] = ib1_1;
            ib2.Albums[1] = ib2_2;

            return Bands;
        }//end method GetBands
        //random year generator
        public static int GenerateRandomDate()
        {
            int range = 20 * 365; //20 years past          
            DateTime randomDate = DateTime.Today.AddDays(-gen.Next(range));
            return randomDate.Year;
        }
        //random sale generator 
        public static double RandomNumberSales(double sale1, double sale2)
        {
            double next = gen.NextDouble();
            return sale1 + (next * (sale2 - sale1));
        }
        //Display band details and albums
        private void LbxBands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Band band = lbxBands.SelectedItem as Band;
            DisplayBandsSelected(band);
        }
        //Displaying band details when band selected
        private void DisplayBandsSelected(Band band)
        {
            if (band != null)
            {
                tbkInfoBands.Text = band.DisplayBandDetails();
                lbxAlbums.ItemsSource = band.Albums;
            }
            else
            {
                tbkInfoBands.Text = "";
                lbxAlbums.ItemsSource = null;
            }
        }
       //album selection event
        private void LbxAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //selection and filtering
        private void CbxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listFiltered = new List<Band>();
            string query = cbxGenre.SelectedItem as string;
            //filter method
            FilterBandsByGenre(query);
        }
        // filtering to filter by Genre 
        private void FilterBandsByGenre(string query)
        {
            if (query != null)
            {
                if (query.Equals("All"))
                {
                    lbxBands.ItemsSource = listBands;
                }
                else if (query.Equals("Pop"))
                {
                    foreach (Band b in listBands)
                    {
                        if (b.GetType().Name.Equals("PopBand"))
                        {
                            listFiltered.Add(b);
                        }
                    }
                    lbxBands.ItemsSource = null;
                    lbxBands.ItemsSource = listFiltered;
                }
                else if (query.Equals("Rock"))
                {
                    foreach (Band b in listBands)
                    {
                        if (b.GetType().Name.Equals("RockBand"))
                        {
                            listFiltered.Add(b);
                        }
                    }
                    lbxBands.ItemsSource = null;
                    lbxBands.ItemsSource = listFiltered;
                }
                else if (query.Equals("Indie"))
                {
                    foreach (Band b in listBands)
                    {
                        if (b.GetType().Name.Equals("IndieBand"))
                        {
                            listFiltered.Add(b);
                        }
                    }
                    lbxBands.ItemsSource = null;
                    lbxBands.ItemsSource = listFiltered;
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }
        //Save data to file
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Band band = lbxBands.SelectedItem as Band;
           
            if (band!=null)
            {
                //write list to file
                string json = JsonConvert.SerializeObject(listBands, Formatting.Indented);
                //write to file
                using (StreamWriter sw = new StreamWriter(Path))
                {
                    sw.Write(json);
                }
                MessageBox.Show("Bands Saved ");
            }

        }
    }
}
