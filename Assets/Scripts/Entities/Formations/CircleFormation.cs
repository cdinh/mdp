using UnityEngine;
using System.Collections;

public class CircleFormation : IFormation 
{
    private static int m_NumSockets;

	// Stores references to nodes that it is connected to.
    // Index of sockets starts from top-center and increments
    // clockwise.
	private struct CircleSockets
	{
		private Node[] m_Sockets;
		
		public CircleSockets(int numSockets)
		{
			m_Sockets = new Node[numSockets];
		}

        public Node Get(int index)
        {
            return m_Sockets[index];
        }
		
		public void Set(Node node, int index)
		{
			m_Sockets[index] = node;
		}
	}
	
    // Wrapper for food piece that adds its sockets
	private class Node
	{
        public CircleSockets Sockets
        {
            get { return m_Sockets; }
        }
        private CircleSockets m_Sockets;

        public Nutrient Nutrient
        {
            get { return m_Nutrient; }
        }
		private Nutrient m_Nutrient;
		
		public Node(Nutrient nutrient)
		{
            m_Sockets = new CircleSockets(m_NumSockets);
			m_Nutrient = nutrient;
		}
	
		public void Attach(Node n, int thisSocket, int theirSocket)
		{
			Sockets.Set(n, thisSocket);
			n.Sockets.Set(this, theirSocket);
		}
	}
	
	public void Arrange(Nutrient[] nutrients)
	{
        m_NumSockets = nutrients.Length;

        Node centerPiece = new Node(nutrients[0]);

		for (int i = 1; i < nutrients.Length; i++)
		{
            Node newPiece = new Node(nutrients[i]);
            AttachPiece(newPiece, ref centerPiece);
		}
	}

    private void AttachPiece(Node n, ref Node attachTo)
    {
        for (int i = 0; i < m_NumSockets; i++)
        {
            if (attachTo.Sockets.Get(i) == null)
            {
                attachTo.Attach(n, i, m_NumSockets - i - 1);
                return;
            }
        }

        // Reached if current node has no empty sockets
        for (int i = 0; i < m_NumSockets; i++)
        {

        }
    }
}
