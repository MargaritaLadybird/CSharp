﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Move
{
    class MapFromFile
    {
        public int BONUS_COUNT = 0;
        public static char BARRICADE = 'X';
        public static char WALL = '*';
        public static char BONUS = '$';
        public static char POINT = '*';
        public static char EXIT = 'E';
                
        string level;
        public char[,] Walls;

        int width;
        int height;

        public MapFromFile(string level)
        {
            this.level = level;
            CountLines();
            Walls = new char[width, height];
        }
        public void Show()
        {
            foreach (string line in File.ReadLines(level))
            {
                Console.WriteLine(line);
            }
        }
      void CountLines()
        {
            int count = 0;
            
                foreach (string line in File.ReadLines(level))
                {
                ++count;
                width = line.Length;
                }

            height = count;
            
        }
        
        void ArrayFromFile()
        {
            
            for (int i = 0; i < Walls.Length; i++)
            {
                foreach (string line in File.ReadLines(level))
                {
                    string temp = line;
                    temp.ToCharArray();
                    for (int j = 0; j < temp.Length; j++)
                    {
                        Walls[i, j] = temp[j];                        
                    }
                }
            }
        }

        void DrawBarricade(char point, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(point);
        }


    }
}
