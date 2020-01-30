using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoComplete
{
    class _Dictionary
    {
        _Dictionary parant;
        _Dictionary[] child;
        int occurance;
        List<int> subStringOf = new List<int>();

        public _Dictionary()
        {
            parant = null;
            occurance = 0;
            child = new _Dictionary[122];
            for (int i = 0; i < 122; i++)
                child[i] = null;
        }
        public _Dictionary(_Dictionary parant, int occurance)
        {
            this.parant = parant;
            this.occurance = occurance;
            this.child = new _Dictionary[122];
            for (int i = 0; i < 122; i++)
                child[i] = null;
        }
        public _Dictionary insert_node(string s)
        {
            string st = s.ToLower();
            _Dictionary curruntNode = this;
            this.child = curruntNode.child;
            this.parant = curruntNode.parant;
            this.occurance = curruntNode.occurance;

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (curruntNode.child['z' - st[i]] == null)
                {
                    if (i == s.Length - 2)
                        curruntNode.child['z' - st[i]] = new _Dictionary(curruntNode, 1);
                    else
                    {
                        curruntNode.child['z' - st[i]] = new _Dictionary(curruntNode, 0);
                    }
                }
                if (i == s.Length - 2)
                    curruntNode.child['z' - st[i]].occurance = 1;
                curruntNode = curruntNode.child['z' - st[i]];
            }

            return this;
        }
        public string dfs_traversal(string input, _Dictionary dictionary)
        {
            input = input.ToLower();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < 'a' || input[i] > 'z') continue;

                if (dictionary.child['z' - input[i]] == null)
                    return input;
                if (i == input.Length - 1)
                    if (dictionary.child['z' - input[i]].occurance == 0)
                        return input;
                dictionary = dictionary.child['z' - input[i]];
            }
            return "";
        }
        public List<string> suggestion(string word, string[] dictionary)
        {
            int min = 999999;
            List<string> output = new List<string>();
            for (int i = 0; i < dictionary.Length; i++)
            {
                int x = MinEditDistance(word, dictionary[i], word.Length, dictionary[i].Length, 1, 1, 1);
                if (x < min)
                {
                    output = new List<string>();
                    min = x;
                    output.Add(dictionary[i]);
                }
                else if (x == min)
                {
                    output.Add(dictionary[i]);

                }
            }
            return output;
        }
        public int MinEditDistance(string word, string dict_i, int word_length, int dict_i_length, int delete_wight, int replace_wight, int insert_wight)
        {
            int[,] output = new int[word_length + 1, dict_i_length + 1];

            for (int i = 0; i <= word_length; i++)
            {
                for (int j = 0; j <= dict_i_length; j++)
                {

                    if (j == 0)
                    {
                        output[i, j] = i * delete_wight;

                    }
                    else if (i == 0)
                    {
                        output[i, j] = j * insert_wight;
                    }
                    else if (dict_i[j - 1] == word[i - 1])
                    {
                        output[i, j] = output[i - 1, j - 1];
                    }
                    else
                    {
                        int min1 = output[i - 1, j] + delete_wight;
                        int min2 = output[i, j - 1] + insert_wight;
                        int min3 = output[i - 1, j - 1] + replace_wight;
                        output[i, j] = Math.Min(Math.Min(min1, min2), min3);

                    }
                }

            }

            return output[word_length, dict_i_length];
        }

    }
}