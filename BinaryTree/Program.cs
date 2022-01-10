using System;

namespace BinaryTree
{
    public class BTree
    {
        public Node root;
        public void Print(Node node, string seperator) // passed in first node, that the binary tree starts with and seperator
        {
            if (node == null) // nothing in binary tree
            {
                return;
            }
            // print left node branch and right node branch
            Console.WriteLine(seperator + node.number);
            Print(node.leftNode, seperator + "---"); // after every level, seperator increases
            Print(node.rightNode, seperator + "---");
        }
        // print function, no parameters
        public void PrintAll()
        {
            Print(root, ""); // passes in first element (root) and seperator (which is empty at first).
        }
        public int Walk(Node node) // passed in first node, that the binary tree starts with
        {
            int counter = 1; // counter that will count levels
            if (node != null) // if starting node exists in binary tree
            {
                Walk(node.leftNode);
                Walk(node.rightNode);
                counter++;
            }
            else
            {
                return 0; // binary tree empty, return null count
            }
            return counter;
        }
        // returns node, that has this number (In other words, searches if this number exists in tree)
        public Node Search(Node node, int findNumber) // passed in starting node and number to search in btree
        {
            if (node == null) // nothing in binary tree
            {
                return null;
            }
            if (node.number == findNumber) // if node number is equal to number to find (if number is found already in the first node)
            {
                return node;
            }
            // going through all nodes
            Node left = Search(node.leftNode, findNumber); // saving result of left branch
            Node right = Search(node.rightNode, findNumber); // saving result of right branch
            if (left != null) // if matching result is saved in Node left
            {
                return left;
            }
            if (right != null)// if matching result is saved in Node right
            {
                return right;
            }
            return null; // nothing is found
        }
        // adds a node to binary tree (first element - root)
        public void AddRoot(int numberToAdd) // passed in number to add
        {
            Node newRoot = new Node(numberToAdd);
            root = newRoot; // binary tree first element is created root element
        }
        public void AddAt(int numberToAdd, Node parentNode) // passed in number to add, node to add element to 
        {
            //if number to add is smaller than parentNode number, add it to the left side
            if (numberToAdd < parentNode.number) 
            {
                parentNode.leftNode = new Node(numberToAdd);
            }
            // if it is smaller, add it to the right side
            else
            {
                parentNode.rightNode = new Node(numberToAdd);
            }
        }
        // removes node where numberToRemove matches Node number
        public void Remove(int numberToRemove)
        {
            /*
             * If a node gets deleted and it has a left node that has a right node, the deleted node
             * gets replaced with the left node and all its contents below
             */
            //Node deleteNode = Search(root, numberToRemove); // finding node to delete
            Node current = root;
            Node parent = root;
            bool isLeftChild = false;
            while (current.number != numberToRemove)
            {
                parent = current;
                if (current.number > numberToRemove)
                {
                    current = current.leftNode;
                    isLeftChild = true;
                }
                else
                {
                    current = current.rightNode;
                    isLeftChild = false;
                }
                if (current == null)
                {
                    
                }
                if (current.leftNode == null && current.rightNode == null) // case 1
                {
                    if (current == root)
                    {
                        root = null;
                    }
                    else if (isLeftChild)
                    {
                        parent.leftNode = null;
                    }
                    else
                    {
                        parent.rightNode = null;
                    }
                }
                else if (current.leftNode == null) // case 2
                {
                    // has a right child
                    if (current == root)
                    {
                        root = current.rightNode;
                    }
                    if (isLeftChild)
                    {
                        parent.leftNode = current.rightNode;
                    }
                    else
                    {
                        parent.rightNode = current.rightNode;
                    }
                }
                else if (current.rightNode == null)
                {
                    // has a left child
                    if (current == root)
                    {
                        root = current.leftNode;
                    }
                    else if(isLeftChild)
                    {
                        parent.leftNode = current.leftNode;
                    }
                    else
                    {
                        parent.rightNode = current.leftNode;
                    }
                }
                else
                {
                    // using function getsuccessor
                    Node sucessor = GetSuccessor(current);
                    // if node to delete is root
                    if (current == root)
                    {
                        root = sucessor; // successor will be new root, original root gets deleted
                    }
                    if (isLeftChild)
                    {
                        parent.leftNode = sucessor;
                    }
                    else
                    {
                        parent.rightNode = sucessor;
                    }
                    sucessor.leftNode = current.leftNode;
                } 
            } 
        }
        // used for delete function
        public Node GetSuccessor(Node deletedNode) // successor - item that comes directly after
        {
            Node parentSuccessor = deletedNode;
            Node successor = deletedNode;
            Node current = successor.rightNode;
            while (current != null)
            {
                parentSuccessor = successor;
                successor = current;
                current = current.leftNode;
            }
            if (successor != current.rightNode)
            {
                parentSuccessor.leftNode = successor.rightNode;
                successor.rightNode = deletedNode.rightNode;
            }
            return successor;
        }
    }
    public class Node
    {
        public int number; // node value will be int
        // binary tree can only have two nodes under parent - left and right.
        public Node rightNode;
        public Node leftNode;
        public Node(int number)
        {
            this.number = number;
        }
    }
    class Program
    {
        public static void AddRootElement(BTree binaryTree)
        {
            Console.Write("Enter element to add as root: ");
            int rootElement = Convert.ToInt32(Console.ReadLine());
            binaryTree.AddRoot(rootElement);
            Console.WriteLine("Element {0} added as root!\n", rootElement);
        }
        public static void AddForParent(BTree binaryTree)
        {
            Console.Write("Enter parent element: ");
            int parentElement = Convert.ToInt32(Console.ReadLine());
            // checking if this element exists in binary tree
            Node foundParentNode = binaryTree.Search(binaryTree.root, parentElement); // finding number of parent node. passing tree root and parent element to find
            if (foundParentNode != null) // if element found
            {
                Console.WriteLine("Parent node found!");
                Console.Write("Enter element to add to parent: ");
                int element = Convert.ToInt32(Console.ReadLine());
                binaryTree.AddAt(element, foundParentNode);
                Console.WriteLine("Element {0} added to parent node {1}!", element, foundParentNode.number);
            }
            else
            {
                Console.WriteLine("No such node found!");
            }
        }
        public static void PrintFrom(BTree binaryTree)
        {
            Console.Write("Enter element to start printing from: ");
            int element = Convert.ToInt32(Console.ReadLine());
            // checking if this element exists in binary tree
            Node foundNode = binaryTree.Search(binaryTree.root, element); // finding node and saving it in foundNode
            // checking if node is found
            if (foundNode != null)
            {
                binaryTree.Print(foundNode, ""); // passing node to start from, starting seperator
            }
            else
            {
                Console.WriteLine("No such node found!");
            }
        }
        public static bool CheckIfExists(BTree binaryTree)
        {
            bool isFound;
            Console.Write("Enter element to find in binary tree: ");
            int elementToFind = Convert.ToInt32(Console.ReadLine());
            // checking if this element exists in binary tree
            Node foundNode = binaryTree.Search(binaryTree.root, elementToFind); // finding node and saving it in foundNode
            // displaying if found or not
            if (foundNode != null)
            {
                isFound = true;
                return isFound;
            }
            else
            {
                isFound = false;
                return isFound;
            }
        }
        public static void RemoveNode(BTree binaryTree)
        {
            Console.Write("Enter node number to remove: ");
            int numberToRemove = Convert.ToInt32(Console.ReadLine());
            try
            {
                binaryTree.Remove(numberToRemove);
                Console.WriteLine("Removal succesfull. Deleted node with number {0} and made replacements.", numberToRemove);
            }
            catch (Exception)
            {

                Console.WriteLine("Error. Node not found or couldn't delete this node.");
            }
        }
        static void Main(string[] args)
        {
            BTree binaryTree = new BTree(); // initialising binary tree
            //Node root = new Node(55); // making first node
            //binaryTree.root = root; // this node is root element of binary tree

            //Node rootLeft = new Node(67); // these nodes will be nodes under root
            //Node rootRight = new Node(62);
            //root.leftNode = rootLeft;
            //root.rightNode = rootRight;

            //Node rootLeftLeft = new Node(89); // nodes under rootLeft
            //Node rootLeftRight = new Node(89);
            //rootLeft.leftNode = rootLeftLeft;
            //rootLeft.rightNode = rootLeftRight;

            ////binaryTree.Print(root, ""); // passes in first element (root) and seperator (which is empty at first).
            ////binaryTree.PrintAll(); // print function, no parameters

            //Console.WriteLine(binaryTree.Walk(root)); // count of levels

            //Node foundNode = binaryTree.Search(root, 89); // passed in starting node and number to search in btree
            //Console.WriteLine(foundNode.number);

            //Node foundParentNode = binaryTree.Search(root, 67); // finding number of parent node
            //binaryTree.AddAt(222, foundParentNode, root);

            //binaryTree.Print(root, ""); // passes in first element (root) and seperator (which is empty at first).
            //binaryTree.PrintAll(); // print function, no parameters
            Console.WriteLine("MENU");
            Console.WriteLine("=====================");
            int choice = 10;
            do
            {
                Console.WriteLine("Choose your action: \n\n[0] Exit\n[1] Add a root node\n[2] Add element for parent\n[3] Print starting from node\n[4] Print all\n[5] Find node\n[6] Remove node\n[7] Display count of binary tree levels\n");
                Console.WriteLine("");
                int userInput = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                switch (userInput)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        AddRootElement(binaryTree);
                        break;
                    case 2:
                        AddForParent(binaryTree);
                        break;
                    case 3:
                        PrintFrom(binaryTree);
                        break;
                    case 4:
                        binaryTree.PrintAll();
                        break;
                    case 5:
                        bool answer = CheckIfExists(binaryTree);
                        Console.WriteLine("Is present in binary tree? {0}", answer);
                        break;
                    case 6:
                        RemoveNode(binaryTree);
                        break;
                    case 7:
                        Console.WriteLine("Number of binary tree levels: {0}", binaryTree.Walk(binaryTree.root));
                        break;
                    default:
                        Console.WriteLine("Theres no such choice");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 0);
        }
    }
}
