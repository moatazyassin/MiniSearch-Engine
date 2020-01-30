using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoComplete
{
    public class Node
    {
        public  Node parent ;
        //int space_indicator;  // bool
        int sent_indicator;  // bool
        public Dictionary<int, Node> child = new Dictionary<int, Node>();
       // public List<Node> c;
        List<int> subStringOf= new List<int>();
        string value;
        public Node()
        {
            parent = null;
        }
      
        public Node(Node par, int sent_indicator)
        {
            parent = par;
            this.sent_indicator = sent_indicator;          
        }
       
        public Node insert_node(string s, int index , int count)
        {
            //count++;
            string   st = s.ToLower();
            Node curruntNode = this;
            this.child = curruntNode.child;
            this.parent = curruntNode.parent;
            this.sent_indicator = curruntNode.sent_indicator;
            this.subStringOf = curruntNode.subStringOf;
            for (int i = 0; i < s.Length-1; i++)
            {
                    if (st[i]==' ')
                    {
                      
                        curruntNode = this;
                        continue;
                        
                    }

                    if (!curruntNode.child.ContainsKey('z'-st[i]))
                    {       
                        curruntNode.child.Add('z' - st[i],new Node(curruntNode, -1));
                        curruntNode = curruntNode.child['z' - st[i]];
                        curruntNode.subStringOf.Add(index);                                      
                    }
                    else
                    {
                        curruntNode = curruntNode.child['z' - st[i]];
                        curruntNode.subStringOf.Add(index);
                    }
                
            }
            curruntNode.sent_indicator = 1;
          return this; 
        }
        public List<int> search (string s ,Node root)
        {

            s = s.ToLower();
            int spelled_correctly = 0;
            List<int> exist1 = new List<int>();
            Node currentNode = root;
            for (int i = 0; i < s.Length; i++) // length-1 ??
            {
                    if (s[i]==' ')
                    {
                        currentNode = this;
                        spelled_correctly++;
                        continue;
                    }
                    if (!currentNode.child.ContainsKey('z' - s[i]))
                    {
                        spelled_correctly++;
                        break;            
                    }
                    currentNode = currentNode.child['z' - s[i]];
                spelled_correctly++;                
            }
            if (spelled_correctly == s.Length)
            {
                return currentNode.subStringOf;
            }
            return exist1;
        }
    
    }

}
