using System;

namespace Tetris
{
    class ShapeMove
    {
        public int x;
        public int y;
        public int[,] matrix;           
        public int sizeMatrix;             
        public int[,] nextMatrix;       
        public int sizeNextMatrix;
        Random r = new Random();


        public int[,] tetr1 = new int[4, 4] // Muodot
        {
            {0,0,0,0},
            {1,1,1,1},
            {0,0,0,0},
            {0,0,0,0},
        };

        public int[,] tetr2 = new int[3, 3]
        {
            {0,2,2,},
            {2,2,0,},
            {0,0,0,},
        };

        public int[,] tetr3 = new int[3, 3]
        {
            {0,3,0},
            {3,3,3},
            {0,0,0},
        };

        public int[,] tetr4 = new int[3, 3]
        {
            {0,0,4},
            {4,4,4},
            {0,0,0},
        };
        public int[,] tetr5 = new int[2, 2]
        {
            {5,5},
            {5,5},
        };
        public int[,] tetr6 = new int[3, 3]
            {
            {6,0,0},
            {6,6,6},
            {0,0,0},
            };
        public int[,] tetr7 = new int[1, 1]
            {
            {7},
            };
        public int[,] tetr8 = new int[3, 3]
        {
            {8,8,0,},
            {0,8,8,},
            {0,0,0,},
        };

        public ShapeMove(int x1, int y1)    // Muotojen luoja
        {
            x = x1;
            y = y1;
            matrix = GenerateMatrix(r.Next(1, 9));
            sizeMatrix = (int)Math.Sqrt(matrix.Length);
            if(matrix == tetr7)
            {
                x = 4;
                y = 0;
            }
            nextMatrix = GenerateMatrix(r.Next(1, 9));
            sizeNextMatrix = (int)Math.Sqrt(nextMatrix.Length);
        }
        
        public void ResetShape(int _x, int _y)              // Uuden muodon luonti aluelle
        {
            x = _x;
            y = _y;
            matrix = nextMatrix;
            sizeMatrix = (int)Math.Sqrt(matrix.Length);
            if (matrix == tetr7)
            {
                x = 4;
                y = 0;
            }
            nextMatrix = GenerateMatrix(r.Next(1, 9));
            sizeNextMatrix = (int)Math.Sqrt(nextMatrix.Length);
        }
        public int[,] GenerateMatrix(int rnd)              // Luo uusi muoto
        {
            int[,] _matrix = tetr1;
            switch (rnd)
            {
                case 1:
                    _matrix = tetr1;
                    break;
                case 2:
                    _matrix = tetr2;
                    break;
                case 3:
                    _matrix = tetr3;
                    break;
                case 4:
                    _matrix = tetr4;
                    break;
                case 5:
                    _matrix = tetr5;
                    break;
                case 6:
                    _matrix = tetr6;
                    break;
                case 7:
                    _matrix = tetr7;
                    break;
                case 8:
                    _matrix = tetr8;
                    break;
            }
            return _matrix;
        }
        public void RotateShape()           // Muodon käännös

        {
            int[,] tempMatrix = new int[sizeMatrix, sizeMatrix]; 

            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    tempMatrix[i, j] = matrix[j, sizeMatrix - 1 - i];
                }
            }
            matrix = tempMatrix;

            int offset1 = 9 - (x + sizeMatrix);

            if (offset1 < 0)        // Jos käänös tapahtu rajan vieressä, siiretään muoto 
            {
                for (int i = 0; i < Math.Abs(offset1); i++)
                    MoveLeft();
            }

            if (x < 0)
            {
                for (int i = 0; i < Math.Abs(x) + 1; i++)
                    MoveRight();
            }
        }
        public void MoveDown()  // muotojen siirto funktiot
        {
                    y++;
        }
        public void MoveRight()
        {
                    x++;
        }
        public void MoveLeft()
        {
                    x--;
        }
    }
}