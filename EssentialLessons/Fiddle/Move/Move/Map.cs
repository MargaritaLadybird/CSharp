﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Move
{
    class Map
    {
        int width = 20, height = 20;
        public char[,] Walls = new char[20, 20];

        int WALL_PERCENTAGE = 5;
        public static char BARRICADE = 'X';
        public static char WALL = '*';
        public static char BONUS = '$';
        public static char POINT = '*';        

        public Map()
        {
            Console.CursorVisible = false;
            GenerateBorder();
            DrawBorder();            
        }

        void Barricade(char point, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(point);
        }
        void GenerateBorder()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Walls[i, j] = (i == 0 || i == width - 1 || j == 0 || j == height - 1) ? POINT : ' ';
                }
            }
            GenerateBarricades(BARRICADE);
            GenerateBarricades(BONUS);
        }
        void GenerateBarricades(char ch)
        {
            Random random = new Random();
            int counter = 0;
            int x, y;
            void Random()
            {
                x = random.Next(0, width);
                y = random.Next(0, width);
            }
            while (counter < ((Walls.Length / 100) * WALL_PERCENTAGE))
            {
                Random();

                if (Walls[x, y] == ' ')
                {
                    Walls[x, y] = ch;
                    counter++;
                }
            }
        }
        void DrawBorder()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.Write(Walls[i, j]);
                }
                Console.WriteLine();
            }
        }
        
    }
}