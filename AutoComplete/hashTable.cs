using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoComplete
{
    public class hashTable
    {
        #region variables
        const char Base_Case = 'a';
        const int alphbet = 26;
        fileop File_instance = new fileop();
        Sorting sorter = new Sorting();
        List<pair>[] hash_table = new List<pair>[alphbet];
        #endregion
        public hashTable()
        {

        }
        void start(List<pair>[] hash_table)
        {
            for (int i = 0; i < alphbet; i++)
            {
                hash_table[i] = new List<pair>();

            }
        }
        public int hash(char X)
        {
            X = Char.ToLower(X);
            return Convert.ToInt32(X - Base_Case);
        }
        public List<pair>[] construct_table()
        {
            start(hash_table);
            List<pair> ilist;
            ilist = File_instance.parse_file();
            for (int i = 0; i < ilist.Count; i++)
            {
                int index;
                string tmp = ilist[i].Second();
                char X = tmp[0];
                index = hash(X);
                hash_table[index].Add(ilist[i]);
            }
            return hash_table;
        }
        public List<pair> search_hash(string X)
        {
            List<pair> result = new List<pair>();
            if (X.Length == 0) { return result; }

            char C = X[0];
            int index;
            index = hash(C);
            result = hash_table[index];
            if (X.Length == 1) { return result; }
            else
            {
                List<pair> tmp = new List<pair>();
                int length = X.Length;
                string substring = null;
                for (int i = 0; i < result.Count; i++)
                {
                    substring = result[i].value(length);
                    substring = substring.ToLower();
                    X = X.ToLower();
                    if (X.Equals(substring)) { tmp.Add(result[i]); }
                }
                return tmp;
            }
        }
    }
}
