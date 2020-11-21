using System;
using System.IO;
using System.Linq;
using System.Diagnostics;


internal class BinarySearchTree
{
	


	internal class Node
	{
		private readonly BinarySearchTree outerInstance;

		internal int key;
		internal int index;
		internal Node left, right;

		public Node(BinarySearchTree outerInstance, int item, int index)
		{

			this.outerInstance = outerInstance;
			key = item;
			left = right = null;
			this.index = index;
		}
	}
	// Root of BST 
	internal Node root;

	// Constructor 
	internal BinarySearchTree()
	{
		root = null;
	}

	// This method mainly calls insertRec() 
	public int TempArray()
	{
		string Path = @"BST15.txt";
		int[] array = { 0 };

		array = File.ReadAllText(Path).Split().Select(int.Parse).ToArray();
		return array.Length + 2;
	}
	internal virtual void insert(int key, int index)
	{
		root = insertRec(root, key, index);
	}
	internal virtual void insertBal(int key, int index)
	{
		root = insertRecBalance(root, key, index);
	}
	internal virtual void BalanceTree()
	{
		BinarySearchTree tree = new BinarySearchTree();
		string Path = @"BST15.txt";
		int[] array = { 0 };

		array = File.ReadAllText(Path).Split().Select(int.Parse).ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			tree.insertBal(array[i], i + 1);

		}

	}


	internal virtual Node insertRec(Node root, int key, int index)
	{


		if (root == null)
		{
			root = new Node(this, key, index);
			return root;
		}

		/* Otherwise, recur down the tree */
		if (key < root.key)
		{
			root.left = insertRec(root.left, key, index);
			root = balance_tree(root);
		}
		else if (key > root.key)
		{
			root.right = insertRec(root.right, key, index);
			root = balance_tree(root);
		}


		return root;
	}

	internal virtual Node insertRecBalance(Node root, int key, int index)
	{


		if (root == null)
		{
			root = new Node(this, key, index);
			return root;
		}

		/* Otherwise, recur down the tree */
		if (key < root.key)
		{
			root.left = insertRec(root.left, key, index);
			root = balance_tree(root);
		}
		else if (key > root.key)
		{
			root.right = insertRec(root.right, key, index);
			root = balance_tree(root);
		}


		return root;
	}

	internal virtual void inorder()
	{
		inorderRec(root);
	}


	internal virtual void inorderRec(Node root)
	{
		if (root != null)
		{
			inorderRec(root.left);
			Console.WriteLine(root.key);
			inorderRec(root.right);
		}
	}
	public void search(int key)
	{//and here
		
		root = search(root, key);

	}
	private Node search(Node root,
					int key)
	{

		if (root == null)
		{
			

			Console.WriteLine("Пещера не найдена, копаем проход");
             BinarySearchTree tree = new BinarySearchTree();
            string Path = @"BST15.txt";
            int[] array = { 0 };

            array = File.ReadAllText(Path).Split().Select(int.Parse).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                tree.insert(array[i], i + 1);

            }



            insert(key, array.Length+1);
            search( key);
            return null;

		}
		if (root.key == key)
		{
			// Console.WriteLine("1");
			Console.WriteLine("Пещера найдена, в ней: " + root.key + " золота");
			//Console.WriteLine(root.key);
			return root;
		}


		
		if (root.key > key)
		{

			Console.WriteLine(root.key);

			Console.WriteLine("Нужно идти в левый проход");

			return search(root.left, key);

		}

		// Key is smaller than root's key 

		Console.WriteLine(root.key);
		Console.WriteLine("Нужно идти в правый проход");
		return search(root.right, key);



	}
	public void Delete(int key)
	{//and here
		Console.WriteLine("Происходит обвал пещеры: " + key);
		root = Delete(root, key);

	}
	private Node Delete(Node root, int key)
	{

		Node parent;
		if (root == null)
		{ return null; }
		else
		{
			//left subtree
			if (key < root.key)
			{
				root.left = Delete(root.left, key);
				if (balance_factor(root) == -2)//here
				{
					if (balance_factor(root.right) <= 0)
					{
						root = RotateRR(root);
					}
					else
					{
						root = RotateRL(root);
					}
				}
			}
			//right subtree
			else if (key > root.key)
			{
				root.right = Delete(root.right, key);
				if (balance_factor(root) == 2)
				{
					if (balance_factor(root.left) >= 0)
					{
						root = RotateLL(root);
					}
					else
					{
						root = RotateLR(root);
					}
				}
			}
			//if target is found
			else
			{
				if (root.right != null)
				{
					//delete its inorder successor
					parent = root.right;
					while (parent.left != null)
					{
						parent = parent.left;
					}
					root.key = parent.key;
					root.right = Delete(root.right, parent.key);
					if (balance_factor(root) == 2)//rebalancing
					{
						if (balance_factor(root.left) >= 0)
						{
							root = RotateLL(root);
						}
						else { root = RotateLR(root); }
					}
				}
				else
				{   //if current.left != null
					return root.left;
				}
			}
		}
		return root;
	}
	private Node balance_tree(Node root)
	{
		int b_factor = balance_factor(root);
		if (b_factor > 1)
		{
			if (balance_factor(root.left) > 0)
			{
				root = RotateLL(root);
			}
			else
			{
				root = RotateLR(root);
			}
		}
		else if (b_factor < -1)
		{
			if (balance_factor(root.right) > 0)
			{
				root = RotateRL(root);
			}
			else
			{
				root = RotateRR(root);
			}
		}
		return root;
	}
	private int max(int l, int r)
	{
		return l > r ? l : r;
	}
	private int getHeight(Node root)
	{
		int height = 0;
		if (root != null)
		{
			int l = getHeight(root.left);
			int r = getHeight(root.right);
			int m = max(l, r);
			height = m + 1;
		}
		return height;
	}
	private int balance_factor(Node root)
	{
		int l = getHeight(root.left);
		int r = getHeight(root.right);
		int b_factor = l - r;
		return b_factor;
	}
	private Node RotateRR(Node key)
	{
		Node pivot = key.right;
		key.right = pivot.left;
		pivot.left = key;
		return pivot;
	}
	private Node RotateLL(Node key)
	{
		Node pivot = key.left;
		key.left = pivot.right;
		pivot.right = key;
		return pivot;
	}
	private Node RotateLR(Node key)
	{
		Node pivot = key.left;
		key.left = RotateRR(pivot);
		return RotateLL(key);
	}
	private Node RotateRL(Node parent)
	{
		Node pivot = parent.right;
		parent.right = RotateLL(pivot);
		return RotateRR(parent);
	}
	public static void Main(string[] args)
	{
		Console.WriteLine("Введите количество золота: ");
		int search = Convert.ToInt32(Console.ReadLine());
		Console.WriteLine("Введите пещеру для обвала: ");
		int delete = Convert.ToInt32(Console.ReadLine());


		BinarySearchTree tree = new BinarySearchTree();

		string Path = @"BST15.txt";
		int[] array = { 0 };

		array = File.ReadAllText(Path).Split().Select(int.Parse).ToArray();




		for (int i = 0; i < array.Length; i++)
		{
			tree.insert(array[i], i + 1);

		}
		//int MaxIndex = array.Length+2;

		//tree.BalanceTree();
		if (array.Contains(delete))
		{
			tree.Delete(delete);
		}
		else
        {
            Console.WriteLine("Среди пещер нет пещеры "+delete);

        }
		if (search != delete)
		{
			tree.search(search);
		}
		else
		{
			Console.WriteLine("Введенная пещера не может быть найдена, в ней произошел обвал");
		}
		
		
			
			
		
		//tree.inorder();
		//      tree.insert(5,1);
		//tree.insert(7, 2);
		//tree.insert(3, 3);
		//tree.insert(4, 4);
		//tree.insert(2, 5);

		//tree.Delete(36);
		//      tree.search(tree.root, search);


	}
}



