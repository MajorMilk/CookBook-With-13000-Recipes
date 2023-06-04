using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CookBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> RECIPELIST = new();

        private int SelectedRecipe = 0;

        private string EXEPATH = "";

        private string DATAPATH = "";

        private string IMAGEPATH = "";

        private string savedRecipePath = "";


        public MainWindow()
        {
            InitializeComponent();
            
            //Recipe data must be in a folder named "DATA" inside the EXEs directory.
            //Image data should be stored in a sub-directory in the data path names "Images"
            string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            EXEPATH = System.IO.Path.GetDirectoryName(exePath);

            DATAPATH = EXEPATH + @"\DATA\";

            IMAGEPATH = DATAPATH + @"Images\";

            savedRecipePath = EXEPATH + @"\SavedRecipes\";

            string? json = File.ReadAllText(DATAPATH + "recipes.json");

            RECIPELIST = JsonConvert.DeserializeObject<List<Recipe>>(json);
            json = null;

            ListLengthBox.Text = RECIPELIST.Count.ToString();

            FillFields();
        }

        private void FillFields()
        {
            var r = RECIPELIST[SelectedRecipe];

            TitleBlock.Text = r.Title;
            InstructionsBox.Text = r.Instructions;
            IngredientsBox.ItemsSource = r.Ingredients;


            string imgPath = IMAGEPATH + r.Image_Name + ".jpg";
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(imgPath, UriKind.RelativeOrAbsolute);
            bmp.EndInit();

            RecipeImageSlot.Source = bmp;

        }

        private void SelectionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(int.TryParse(SelectionBox.Text, out int result) && result < RECIPELIST.Count)
            {
                if (result < 1) result = 1;
                SelectedRecipe = result - 1;
                FillFields();
            }

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe < RECIPELIST.Count - 1) SelectedRecipe++;
            else SelectedRecipe = 0;
            SelectionBox.Text = (SelectedRecipe + 1).ToString();
            FillFields();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe > 0) SelectedRecipe--;
            else SelectedRecipe = RECIPELIST.Count - 1;


            SelectionBox.Text = (SelectedRecipe + 1).ToString();
            FillFields();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var r = RECIPELIST[SelectedRecipe];
            string tmp = r.ToString();
            File.WriteAllText(savedRecipePath + r.Title + ".txt", tmp);
            MessageBox.Show(@"Your recipe has been saved to exe\SavedRecipes\" + r.Title + ".txt", "Recipe Saved");
        }
    }
}
