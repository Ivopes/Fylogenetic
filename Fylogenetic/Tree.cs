using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fylogenetic
{
    internal class Tree
    {
        public Leaf root;
    }
    public class Leaf
    {
        public string name;
        public Leaf child1;
        public Leaf child2;
        public int child1Length;
        public int child2Length;
        public HashSet<char> Label;
        public int heigh;
        public int Index;
        public List<int> childIndexes;
    }
}
