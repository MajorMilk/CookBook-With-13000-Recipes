using System.Collections.Generic;

namespace CookBook
{
    public class Recipe
    {
        public string Title { get; set; } = "";

        public List<string> Ingredients { get; set; } = new();
        public string Instructions { get; set; } = "";
        public string Image_Name { get; set; } = "";

        public List<string> Cleaned_Ingredients { get; set; } = new();

        public Recipe()
        {

        }


        public override string ToString()
        {
            string temp = Title + "\n\n";

            foreach(string s in Ingredients)
            {
                temp += s + '\n';
            }
            temp += "\n\n";

            string t = Instructions.Replace(". ", ".\n");
            temp += t;

            return temp;
        }
    }
}