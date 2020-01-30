
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;


namespace AutoComplete
{
   
    public class fileop
    {
        public List<pair> parse_file()
        {
            /*
             * E:\Work\AutoComplete\AutoComplete\bin\Debug\resources
             */
            string path = System.IO.Path.GetFullPath("SearchLinks(Large).txt");
            StreamReader SR = new StreamReader(path);
            string file = SR.ReadToEnd();
            const char newline_delimeter = '\n';
            const char inline_delimeter = ',';
            SR.Close();
            string[] blocks = file.Split(new char[] { newline_delimeter }, StringSplitOptions.RemoveEmptyEntries);
            string[] tmp_line = new string[2];
            List<pair> iline = new List<pair>();

            for (int i = 0; i < blocks.Count(); i++)
            {
                if (i == 0)
                    continue;
                int j = 0;
                tmp_line = blocks[i].Split(new char[] { inline_delimeter }, StringSplitOptions.RemoveEmptyEntries);
                pair tmp = new pair(long.Parse(tmp_line[j]), tmp_line[j + 1]);
                iline.Add(tmp);
            }
            return iline;

        }
       // E:\College\AutoComplete\AutoComplete
        public string[] parse_Dictionary()
        {
            string dir =  System.IO.Path.GetFullPath("Dictionary (Large).txt");
            StreamReader SR = new StreamReader(dir);
            string file = SR.ReadToEnd();
            const char newline_delimeter = '\n';
            SR.Close();
            string[] blocks = file.Split(new char[] { newline_delimeter }, StringSplitOptions.RemoveEmptyEntries);
            return blocks;

        }
        public string[] parse_line( string sentence)
        {
            const char space_delimeter = ' ';
            string[] words = sentence.Split(new char[] { space_delimeter }, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }
    }
}
