using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoComplete
{
    public partial class Form1 : MaterialForm
    {
        #region Definitions
             List<int> exist = new List<int>();
             fileop file_operation = new fileop();
             List<pair>pre_list = new List<pair>();
             _Dictionary D = new _Dictionary();
             Sorting sortar = new Sorting();
             string []dictionary_strings;
             string[] split_4_spell = new string[] { "" };
             Node trie = new Node();
             List<int> result = new List<int>();
        #endregion

        public Form1()
        {
            InitializeComponent();
            var skinmanger = MaterialSkinManager.Instance;
            skinmanger.AddFormToManage(this);
            skinmanger.Theme = MaterialSkinManager.Themes.LIGHT;
            skinmanger.ColorScheme = new ColorScheme(Primary.Orange400, Primary.Orange400, Primary.Red100, Accent.Orange400, TextShade.BLACK);
        }
        void GetSuggestions(string input)
        {
            List<string> wrong_word = new List<string>();
            wrong_word.Add("Suggestions:");
            wrong_word.AddRange(D.suggestion(input, dictionary_strings));
            listBox3.DataSource = wrong_word;
            listBox3.Show();
            listBox1.Hide();
        }
        void check(string[] input)
        {
            List<string> wrong_word=new List<string>() ;
            wrong_word.Add("Not Spelled Correctly");

            for (int i = 0; i < input.Length; i++)
            {
                if (D.dfs_traversal(input[i], D) != "")
                {
                     wrong_word.Add(input[i]);
     
                }
            }
              listBox2.DataSource = wrong_word;
              listBox2.Show();
              listBox1.Hide();
        }
        void construct_Dictionary()
        {
            for (int i = 0; i < dictionary_strings.Length; i++)
            {
                if (i == 0)
                    continue;
                D.insert_node(dictionary_strings[i]);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Hide();
            listBox2.Hide();
            listBox3.Hide();
            materialRadioButton1.Checked = true;
            // pre-processing goes here.....
            pre_list = file_operation.parse_file();
            dictionary_strings = file_operation.parse_Dictionary();
            construct_Dictionary(); 
            int counter = 0;
            int i;
            for ( i = 0; i<pre_list.Count -1  ; i++)
            {
                trie = trie.insert_node(pre_list[i].Second(),i,counter);
            }
        }
        List<int> BS (List<int> older, List<int> newer)
        {
            result = new List<int>();
            int[] tmpArr = newer.ToArray();

            for (int i = 0; i < older.Count(); i++)
            {
                int found = Array.BinarySearch(tmpArr, older[i]);
                if (found >= 0)
                    result.Add(older[i]);
            }
            return result;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.Focused)
                input.Text = (string)listBox1.SelectedItem;
        }
        void Search(String X)
        {
            listBox2.Hide();
            List<pair> searching_result = new List<pair>();
            List<int> existTwo = new List<int>();
            List<string> data = new List<string>();
            string[] lol;
            if (X.Length > 0 && X[X.Length - 1] != ' ')
            {
                existTwo = trie.search(X, trie);
            }
            else
            {
                for (int i = 0; i < exist.Count(); i++)
                {
                    int index = exist[i];
                    if (pre_list[index].Second().StartsWith(X))
                        searching_result.Add(pre_list[index]);
                }
            }
            if (X.Length > 1 && X[X.Length - 2] == ' ')
            {
                if (exist.Count < existTwo.Count)
                    exist = BS(exist, existTwo);
                else
                    exist = BS(existTwo, exist);

                for (int i = 0; i < exist.Count(); i++)
                {
                    int index = exist[i];
                    if (pre_list[index].Second().StartsWith(X))
                        searching_result.Add(pre_list[index]);
                }

            }
            else if (X.Length > 0 && X[X.Length - 1] != ' ')
            {
                for (int i = 0; i < existTwo.Count(); i++)
                {
                    int index = existTwo[i];
                    if (pre_list[index].Second().ToLower().StartsWith(X))
                        searching_result.Add(pre_list[index]);
                }
                exist = existTwo;
            }


            if (searching_result.Count == 0 && X.Length > 1)
            {
                lol = X.Split(' ');
                check(lol);
                for (int i = 0; i < lol.Length; i++)
                {
                    existTwo = trie.search(lol[i], trie);
                    if (i > 0)
                    {
                        if (exist.Count < existTwo.Count)
                            exist = BS(exist, existTwo);
                        else
                            exist = BS(existTwo, exist);
                    }
                    else
                    {
                        exist = existTwo;
                    }
                }
                for (int i = 0; i < exist.Count(); i++)
                {
                    int index = exist[i];
                    searching_result.Add(pre_list[index]);
                }
            }
            #region count sort
            //if (materialRadioButton1.Checked == true)
            //{
            //    string[] count_sort = new string[result.Count + 1];
            //    count_sort = sortar.count_sort(result);
            //    for (int i = result.Count; i > 0; i--)
            //    {
            //        data.Add(count_sort[i]);
            //    }
            //    listBox1.DataSource = data;
            //}
            #endregion
            #region merge_sort
            if (materialRadioButton2.Checked == true)
            {
                List<pair> merge_sort = new List<pair>();
                merge_sort = sortar.merge_sort(searching_result);
                int C = searching_result.Count - 1;
                 
                for (int i = C ; i >= 200; i--)
                {
                    data.Add(merge_sort[i].Second());
                }
                listBox1.DataSource = data;
            }
            listBox1.Show();
            materialLabel2.Text = "Number of Search Result is " +data.Count();
            #endregion
            #region insertion_Sort
            if (materialRadioButton3.Checked == true)
            {
                string[] insertion_sort = new string[searching_result.Count];
                insertion_sort = sortar.insert_sort(searching_result);
                for (int i = searching_result.Count - 1; i > 0; i--)
                {
                    data.Add(insertion_sort[i]);
                }
                listBox1.DataSource = data;

            }
            #endregion
        }
   
        private void input_TextChanged(object sender, EventArgs e)
        {
            string X = input.Text.ToString();
            int LE = X.Length;
            Search(X);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 1 && listBox2.SelectedIndex > 0)
            {
                string searchStr = (string)listBox2.SelectedItem;
                input.SelectionStart = input.Text.IndexOf(searchStr);
                input.SelectionLength = searchStr.Length;
                GetSuggestions(searchStr);
            }
            if (listBox2.Items.Count == 1) listBox2.Hide();
            listBox2.SelectedIndex = 0;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.Items.Count > 1 && listBox3.SelectedIndex > 0 && input.SelectedText.Length > 0)
            {
                input.SelectedText = ((string)listBox3.SelectedItem).Replace("\r", "");
                listBox3.Hide();
            }
        }
     }
}       
