using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoComplete
{
    public class Sorting
    {
        public Sorting()
        {
 
        }
        public string[] insert_sort(List<pair> ilist)
        {

            pair temp;
            int j = 0;
            for (int i = 0; i < ilist.Count; i++)
            {
                temp = ilist[i];

                j = i - 1;

                while (j >= 0 && ilist[j].First() > temp.First())
                {
                    pair tempP = ilist[j + 1];
                    ilist[j + 1] = ilist[j];
                    ilist[j] = tempP;
                    j--;
                }

            }

            string[] output = new string[ilist.Count];
            j = 0;
            for (int i = 0; i < ilist.Count; i++)
            {
                output[j] = ilist[i].Second();
                j++;
            }
            return output;
        }
     #region count
            //public string[] count_sort(List<pair> ilist)
            //{
            //    long max = 0;
            //    for (int i = 0; i < ilist.Count; i++)
            //    {
            //        if (max < ilist[i].First())
            //            max = ilist[i].First();
            //    }
            //    long[] count = new long[max + 1];
            //    string[] output = new string[ilist.Count + 1];
            //    for (int i = 0; i < ilist.Count; i++)
            //    {
            //        count[ilist[i].First()]++;

            //    }
            //    for (int i = 1; i < max + 1; i++)
            //    {
            //        count[i] += count[i - 1];
            //    }
            //   // int j = 0;
            //    for (int i = 0; i < ilist.Count; i++)
            //    {

            //        output[count[ilist[i].First()]] = ilist[i].Second();

            //        count[ilist[i].First()]--;
            //    }
            //    return output;
            //}
    #endregion
     #region merge
            public List<pair> merge_sort(List<pair> input)
            {
                List<pair> Result = new List<pair>();
                List<pair> Left = new List<pair>();
                List<pair> Right = new List<pair>();

                if (input.Count <= 1)
                    return input;

                int midpoint = input.Count / 2;
                    for (int i = 0; i < midpoint; i++)
                        Left.Add(input[i]);
                    for (int i = midpoint; i < input.Count; i++)
                        Right.Add(input[i]);

                    Left = merge_sort(Left);
                    Right = merge_sort(Right);
                    Result = Merge(Left, Right);

                return Result;
            }
             private List<pair> Merge(List<pair> Left, List<pair> Right)
            {
                List<pair> Result = new List<pair>();
                while (Left.Count > 0 && Right.Count > 0)
                {
                        if (Left[0].First() < Right[0].First())
                        {
                                Result.Add(Left[0]);
                                Left.RemoveAt(0);
                        }
                        else
                        {
                                Result.Add(Right[0]);
                                Right.RemoveAt(0);
                        }
                }

                while (Left.Count > 0)
                {
                        Result.Add(Left[0]);
                        Left.RemoveAt(0);
                }

                while (Right.Count > 0)
                {
                            Result.Add(Right[0]);
                            Right.RemoveAt(0);
                }

                return Result;
            }
            #endregion
        }
}
