﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domineering_game.Tools
{
    public static class ExtensionMethods
    {
        public static int[][] GetDimArray(int h, int w)
        {
            int[][] cells = new int[h][];
            for (int i = 0; i < cells.Length; i++)
                cells[i] = new int[w];
            return cells;
        }

        public static int[][] CloneDimArray(int[][] from)
        {
            int[][] to = new int[from.Length][];
            for (int i = 0; i < from.Length; i++)
                to[i] = (int[])from[i].Clone();
            return to;
        }

        //https://stackoverflow.com/questions/21986909/convert-multidimensional-array-to-jagged-array-in-c-sharp
        public static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
    }
}
