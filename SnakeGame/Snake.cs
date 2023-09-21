using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.Pkcs;
using System.Windows;

namespace SnakeGame
{
    internal class Snake
    {

        public List<Vector2D> position = new List<Vector2D>();
        private readonly Vector2D PERIODICITY;

        public Vector2D direction = new Vector2D(1, 0);
        public String strDirection = "D"; //D=Destra, A=Alto, G=Giu, S=Sinistra -> Parte da destra

        public Snake(int row_num, int col_num)
        {
            PERIODICITY = new Vector2D(row_num, col_num);
            for (int i = 0; i < 4; i++)
            { this.position.Add(new Vector2D(i, 2)); }
        }

        //Input direzione

        public void move(Vector2D direction)
        {
            this.direction = direction;
            Vector2D head = position[position.Count - 1];

            this.position.Add( (head + direction) % PERIODICITY);

            for(int i=0;i<this.position.Count;i++) {
                for(int j=0;j<this.position.Count;j++) {
                    if(i!=j) {
                        if(this.position[i].x == this.position[j].x && this.position[i].y == this.position[j].y) {
                            System.Environment.Exit(0);
                        }
                    }
                }
            }
            this.position.RemoveAt(0);

        

        }
    }
}
