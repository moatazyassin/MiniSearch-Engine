using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoComplete
{
    /*
     * creats a new pair that carry each  predict and its weight
    */
    public class pair
    {
        #region variables
        long weight;
        string word;
        #endregion
        #region constructor
        public pair()
        {
            weight = 0;
            word = null;
        }
        public pair(long W, string Wo)
        {
            weight = W;
            word = Wo;
        }
#endregion
        #region fuctions
        public long First()
        {
            return weight;
        }
        public string Second()
        {
            return word;
        }
        public string value(int index)
        {
            string tmp = null;
            if (word.Length < index)
            {
                return tmp = "lol";
            }
            else { tmp = word.Substring(0, index); }

            return tmp;
        }
        #endregion
    }
}
