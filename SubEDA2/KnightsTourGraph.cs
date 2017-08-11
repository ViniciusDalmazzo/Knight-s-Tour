using System;
using System.Collections.Generic;
using System.Threading;

namespace SubEDA2
{
    public class KnightsTourGraph : Graph
    {
        private Dictionary<string, Node> dictionary;
        private int[,] initState;
        private int N;
        private int M;
        private int somaTarget = 0;

        public KnightsTourGraph(int N, int M)
        {
            initState = new int[N, M];
            dictionary = new Dictionary<string, Node>();
            this.N = N;
            this.M = M;
            int count = 1;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    initState[i, j] = 0;
                    somaTarget += count;
                    count++;
                }
            }
        }

        public void GenerateSumObs()
        {
            int count = 1;
            somaTarget = 0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (initState[i, j] != -1)
                    {
                        somaTarget += count;
                        count++;
                    }
                }
            }
        }

        public void AddObstacle(int xobs, int yobs)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if(i == yobs && j == xobs)
                    {
                        initState[i, j] = -1;
                    }
                }
            }
        }

        public int[,] GetSolution(int x, int y)
        {
            List<int[,]> resp = FindSolution(x, y);

            return resp[resp.Count-1];
            
        }

        

        public List<int[,]> FindSolution(int x, int y)
        {
            initState[y, x] = 1;
            AddNode(Encode(initState), initState, QntZeros(initState));

            nodes[0].X = x;
            nodes[0].Y = y;

            PriorityQueue<double, Node> queue = new PriorityQueue<double, Node>();

            queue.Insert(nodes[0].Value, nodes[0]);

            while (queue.count > 0)
            {
                Node atual = queue.Remove();

                //target
                if (TargetFound(atual))
                {
                    return BuildAnswer(atual);
                }

                List<Node> paths = FindPath(atual);

                foreach (Node path in paths)
                {
                    //target
                    if (TargetFound(atual))
                    {
                        return BuildAnswer(atual);
                    }

                    if (FindNode(path.Name) == null)
                    {
                        //path.Visited = true;
                        nodes.Add(path);
                        dictionary.Add(path.Name, path);
                        path.Parent = atual;
                        queue.Insert(path.Value, path);
                    }
                }
            }
            return null;
        }

        public List<Node> FindPath(Node n)
        {
            List<Node> paths = new List<Node>();
            int[,] table = (int[,])n.Info;
                       
            //cima direita
            if (ValidPosition(n.X - 2, n.Y + 1) && (table[n.Y + 1, n.X - 2] == 0) && (table[n.Y + 1, n.X - 2]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y + 1, n.X - 2] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X - 2, n.Y + 1) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y + 1, n.X - 2]);
                novo.X = n.X - 2;
                novo.Y = n.Y + 1;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y + 1, n.X - 2]));
                paths.Add(novo);
            }

            //cima esquerda
            if (ValidPosition(n.X - 2, n.Y - 1) && (table[n.Y - 1, n.X - 2] == 0) && (table[n.Y - 1, n.X - 2]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y - 1, n.X - 2] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X - 2, n.Y - 1) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y - 1, n.X - 2]);
                novo.X = n.X - 2;
                novo.Y = n.Y - 1;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y - 1, n.X - 2]));
                paths.Add(novo);
            }

            //direita cima
            if (ValidPosition(n.X - 1, n.Y + 2) && (table[n.Y + 2, n.X - 1] == 0) && (table[n.Y + 2, n.X - 1]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y + 2, n.X - 1] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X - 1, n.Y + 2) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y + 2, n.X - 1]);
                novo.X = n.X - 1;
                novo.Y = n.Y + 2;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y + 2, n.X - 1]));
                paths.Add(novo);
            }

            //direita baixo
            if (ValidPosition(n.X + 1, n.Y + 2) && (table[n.Y + 2, n.X + 1] == 0) && (table[n.Y + 2, n.X + 1]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y + 2, n.X + 1] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X + 1, n.Y + 2) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y + 2, n.X + 1]);
                novo.X = n.X + 1;
                novo.Y = n.Y + 2;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y + 2, n.X + 1]));
                paths.Add(novo);
            }

            //baixo direita
            if (ValidPosition(n.X + 2, n.Y + 1) && (table[n.Y + 1, n.X + 2] == 0) && (table[n.Y + 1, n.X + 2]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y + 1, n.X + 2] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X + 2, n.Y + 1) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y + 1, n.X + 2]);
                novo.X = n.X + 2;
                novo.Y = n.Y + 1;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y + 1, n.X + 2]));
                paths.Add(novo);
            }

            //baixo esquerda
            if (ValidPosition(n.X + 2, n.Y - 1) && (table[n.Y - 1, n.X + 2] == 0) && (table[n.Y - 1, n.X + 2]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y - 1, n.X + 2] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X + 2, n.Y - 1) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y - 1, n.X + 2]);
                novo.X = n.X + 2;
                novo.Y = n.Y - 1;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y - 1, n.X + 2]));
                paths.Add(novo);
            }

            //esquerda cima
            if (ValidPosition(n.X - 1, n.Y - 2) && (table[n.Y - 2, n.X - 1] == 0) && (table[n.Y - 2, n.X - 1]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y - 2, n.X - 1] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X - 1, n.Y - 2) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y - 2, n.X - 1]);
                novo.X = n.X - 1;
                novo.Y = n.Y - 2;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y - 2, n.X - 1]));
                paths.Add(novo);
            }

            //esquerda baixo
            if (ValidPosition(n.X + 1, n.Y - 2) && (table[n.Y - 2, n.X + 1] == 0) && (table[n.Y - 2, n.X + 1]) != -1)
            {
                int[,] aux = CopyTo(table);
                aux[n.Y - 2, n.X + 1] = n.Path + 1;

                Node novo = new Node(Encode(aux), aux, n.Nivel + 1, HeuristicVizinhos(aux, n.X + 1, n.Y - 2) + QntZeros(aux) + RestanteSoma(aux), aux[n.Y - 2, n.X + 1]);
                novo.X = n.X + 1;
                novo.Y = n.Y - 2;
                novo.Edges.Add(new Edge(novo, n, aux[n.Y - 2, n.X + 1]));
                paths.Add(novo);
            }

            return paths;
        }

        private List<int[,]> BuildAnswer(Node answer)
        {
            Stack<int[,]> s = new Stack<int[,]>();

            while (answer.Parent != null)
            {
                s.Push((int[,])answer.Info);
                answer = answer.Parent;
            }

            s.Push((int[,])answer.Info);

            List<int[,]> seq = new List<int[,]>();

            while (s.Count > 0)
            {
                int[,] x = s.Pop();
                seq.Add(x);               
            }

            return seq;
        }

        private int[,] CopyTo(int[,] table)
        {
            int[,] aux = new int[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    aux[i, j] = table[i, j];
                }
            }
            return aux;
        }

        private bool TargetFound(Node atual)
        {
            int[,] array = (int[,])atual.Info;

            if (somaTarget == SomaArray(array))
                return true;

            else if (QntZeros((int[,])atual.Info) == 0)
                return true;

            return false;
        }

        private int SomaArray(int[,] array)
        {
            int soma = 0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (array[i, j] != -1)
                        soma += array[i, j];
                }
            }

            return soma;
        }

        private Node FindNode(string name)
        {
            if (dictionary.ContainsKey(name))
                return dictionary[name];
            return null;
        }

        private bool ValidPosition(int x, int y)
        {
            return ((x < M && x >= 0) && ((y < N) && y >= 0)) ? true : false;
        }

        private String Encode(int[,] x)
        {
            String aux = "";

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    aux += x[i, j];
                }
            }
            return aux;
        }

        private double RestanteSoma(int[,] array)
        {
            double restanteSoma = 0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    restanteSoma += array[i, j];
                }
            }

            restanteSoma = somaTarget - restanteSoma;

            return restanteSoma;
        }

        private double QntZeros(int[,] array)
        {
            double qnt = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (array[i, j] == 0)
                        qnt++;
                }
            }
            return qnt;
        }
                
        private double HeuristicVizinhos(int[,] array, int x, int y)
        {
            double heuristic = 0;

            //cima direita
            if (ValidPosition(x - 2, y + 1) && array[y + 1, x - 2] == 0 && array[y + 1, x - 2] != -1)
            {
                heuristic++;
            }

            //cima esquerda
            if (ValidPosition(x - 2, y - 1) && array[y - 1, x - 2] == 0 && array[y - 1, x - 2] != -1)
            {
                heuristic++;
            }

            //direita cima
            if (ValidPosition(x - 1, y + 2) && array[y + 2, x - 1] == 0 && array[y + 2, x - 1] != -1)
            {
                heuristic++;
            }

            //direita baixo
            if (ValidPosition(x + 1, y + 2) && array[y + 2, x + 1] == 0 && array[y + 2, x + 1] != -1)
            {
                heuristic++;
            }

            //baixo direita
            if (ValidPosition(x + 2, y + 1) && array[y + 1, x + 2] == 0 && array[y + 1, x + 2] != -1)
            {
                heuristic++;
            }

            //baixo esquerda
            if (ValidPosition(x + 2, y - 1) && array[y - 1, x + 2] == 0 && array[y - 1, x + 2] != -1)
            {
                heuristic++;
            }

            //esquerda cima
            if (ValidPosition(x - 1, y - 2) && array[y - 2, x - 1] == 0 && array[y - 2, x - 1] != -1)
            {
                heuristic++;
            }

            //esquerda baixo
            if (ValidPosition(x + 1, y - 2) && array[y - 2, x + 1] == 0 && array[y - 2, x + 1] != -1)
            {
                heuristic++;
            }

            return heuristic;
        }
    }
}
