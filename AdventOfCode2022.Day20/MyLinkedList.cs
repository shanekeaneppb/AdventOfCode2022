namespace AdventOfCode2022.Day20
{
    public class MyLinkedList
    {
        public class Node
        {
            public int Value;
            public Node Previous;
            public Node Next;

            public Node(int value)
            {
                Value = value;
            }

            //public override string ToString()
            //{
            //    return $"{Value}, Next = {Next}, Previous = {Previous}";
            //}
        }

        public Node Start;
        public Node End;
        public int Length;

        public void Add(int value)
        {
            Node newNode = new Node(value);
            if (Length == 0)
            {
                newNode.Previous = newNode;
                newNode.Next = newNode;
                Start = newNode;
                End = newNode;
            }
            else
            {
                // initialise new node at end
                newNode.Previous = End;
                newNode.Next = Start;

                // Maintain the loop
                End.Next = newNode;
                Start.Previous = newNode;

                // Set the new end node
                End = newNode;
            }
            Length++;
        }

        public void Move(int numberToMove)
        {
            //Console.WriteLine($"Current list = {this}");
            int stepsToMove = numberToMove % (Length - 1);
            int i, index;
            Node nodeToMove = Start, newNodePosition;
            for(i = 0; i < Length; i++)
            {
                if (nodeToMove.Value == numberToMove)
                    break;
                nodeToMove = nodeToMove.Next;
            }
            newNodePosition = nodeToMove;

            if (stepsToMove == 0)
                return;
            if(numberToMove < 0)
            {
                for(i = 0; i < Math.Abs(stepsToMove); i++)
                {
                    newNodePosition = newNodePosition.Previous;
                }
            }
            else if (numberToMove > 0)
            {
                for (i = 0; i < Math.Abs(stepsToMove); i++)
                {
                    newNodePosition = newNodePosition.Next;
                }
            }
            // Remove the node that we are moving
            nodeToMove.Previous.Next = nodeToMove.Next;
            nodeToMove.Next.Previous = nodeToMove.Previous;
            //Console.WriteLine($"After Removing = {this}");

            // Insert the new node
            nodeToMove.Previous = newNodePosition;
            nodeToMove.Next = newNodePosition.Next;

            newNodePosition.Next.Previous = nodeToMove;
            newNodePosition.Next = nodeToMove;

        }


        public override string ToString()
        {
            string str = "";
            Node node = Start;
            for(int i = 0; i < Length; i++)
            {
                str += $"{node.Value}, ";
                node = node.Next;
            }
            return str;
        }
    }
}


