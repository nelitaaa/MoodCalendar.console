//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MoodCalendar
//{
//    public class MoodBST
//    {
//        private class Node
//        {
//            public DateTime Date;
//            public MoodEntry Entry;
//            public Node Left, Right;

//            public Node(DateTime date, MoodEntry entry)
//            {
//                Date = date;
//                Entry = entry;
//            }
//        }

//        private Node root;

//        public void Insert(MoodEntry entry)
//        {
//            root = Insert(root, entry);
//        }

//        private Node Insert(Node node, MoodEntry entry)
//        {
//            if (node == null)
//                return new Node(entry.Date, entry);

//            int cmp = entry.Date.CompareTo(node.Date);
//            if (cmp < 0)
//                node.Left = Insert(node.Left, entry);
//            else if (cmp > 0)
//                node.Right = Insert(node.Right, entry);
//            else
//                node.Entry = entry;

//            return node;
//        }

//        public MoodEntry Search(DateTime date)
//        {
//            return Search(root, date);
//        }

//        private MoodEntry Search(Node node, DateTime date)
//        {
//            if (node == null)
//                return null;

//            int cmp = date.CompareTo(node.Date);
//            if (cmp < 0)
//                return Search(node.Left, date);
//            else if (cmp > 0)
//                return Search(node.Right, date);
//            else
//                return node.Entry;
//        }
//    }

//}
