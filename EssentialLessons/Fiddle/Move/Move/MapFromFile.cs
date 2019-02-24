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
        public int BONUS_COUNT;
        
        public Dictionary<string, string> ConsoleColor = new Dictionary<string, string>
        {
            ["WALL"] = "Green"
        };
      
        string level;
        public char[,] Walls;
        public char[,] GraficWalls;

        public IGraficPoint[,] GraficArray;


        public static int width, height;

        public MapFromFile(string level)
        {
            this.level = level;
            CountLines();
            Walls = new char[width, height];
            GraficWalls = new char[width, height];

            GraficArray = new IGraficPoint[width, height];

            ArrayFromFile();
            BONUS_COUNT = CountBonus();
            ConvertToGraficWalls(Walls);
            DrawMap(GraficArray);
        }
       
      void CountLines()
        {
            int count = 0;
            
            foreach (string line in File.ReadLines(level))
            {
                ++count;
                height = line.Length;
            }

            width = count;            
        }

        int CountBonus()
        {
            int count = 0;
            foreach (char point in Walls)
            {
                if(point == Grafic.BONUS)
                {
                    count++;
                }
            }
            return count;
        }

        void ArrayFromFile()
        {
            string[] STRArray = new string[height];
            List<string> STRList = new List<string>();

            foreach (string line in File.ReadLines(level))
            {
                STRList.Add(line);
            }

            STRArray = STRList.ToArray();

            for (int top = 0; top < width; top++)
            {
               string temp = STRArray[top];
               char[] tempArray = temp.ToCharArray();

               for (int left = 0; left < height; left++)
               {
                  Walls[top, left] = tempArray[left];                
               }                
            }
        }

        void ConvertToGraficWalls(char[,] Walls)
        {
            for (int top = 0; top < width; top++)
            {
                for (int left = 0; left < height; left++)
                {
                    if (Walls[top, left] == Grafic.WALL)
                    {
                        GraficArray[top, left] = new Grafic.GRAFIC_WALL();
                    }
                    else if (Walls[top, left] == Grafic.BARRICADE)
                    {
                        GraficArray[top, left] = new Grafic.GRAFIC_BARRICADE();
                    }
                    else if (Walls[top, left] == Grafic.BONUS)
                    {
                        GraficArray[top, left] = new Grafic.GRAFIC_BONUS();
                    }
                    else
                    {
                        GraficArray[top, left] = new Grafic.GRAFIC_SPACE();
                    }
                }
            }
        }

        void DrawBarricade(IGraficPoint point, int left, int top)
        {
            Console.SetCursorPosition(left, top);
           
            Console.ForegroundColor = (ConsoleColor)point.FOREGROUND;
            Console.BackgroundColor = (ConsoleColor)point.BACKGROUND;
            Console.Write(point.SYMBOL);
        }
        public bool IsBarricade(Point point)
        {
            if (Walls[point.Top, point.Left] == Grafic.BARRICADE || Walls[point.Top, point.Left] == Grafic.WALL)
            {
                return true;
            }
            else if (Walls[point.Top, point.Left] == Grafic.BONUS)
            {
                Bonus.Count++;
                Walls[point.Top, point.Left] = ' ';
                return false;
            }
            else if (Walls[point.Top, point.Left] == Grafic.EXIT)
            {                    
               Game.LevelNum++;
               Game game = new Game();
               game.StartGame();
               
               return false;
            }
            else
            {
                return false;
            }
        }
        public void Clear()
        {
            Console.Clear();
        }

        public void GenerateBarricades(IGraficPoint ch, int barricades_count)
        {
            Random random = new Random();
            int counter = 0;
            int top, left;
            void Random()
            {
                left = random.Next(0, width);
                top = random.Next(0, width);
            }
            while (counter < barricades_count)
            {
                Random();

                if (GraficArray[top, left].SYMBOL == '.')
                {
                    GraficArray[top, left] = ch;
                    Walls[top, left] = ch.SYMBOL;
                    DrawBarricade(ch, left, top);
                    counter++;
                }
            }
        }
        void DrawMap(IGraficPoint[,] Walls)
        {
            for (int top = 0; top < width; top++)
            {
                for (int left=0; left < height; left++)
                {
                    DrawBarricade(Walls[top, left], left, top );
                }
                Console.WriteLine();
            }
        }
    }
}
