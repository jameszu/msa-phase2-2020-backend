﻿/*using CanvasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanvasAPI.PeriodicJobs
{
    public interface IPeriodicCanvasJobs
    {
        public void CreateNewCanvas();

    }

	public class PeriodicCanvasJobs : IPeriodicCanvasJobs
	{
		private AppDatabase _context; 
		private int SIZE = 32;
		public PeriodicCanvasJobs(AppDatabase context)
		{
			_context = context;
		} 
		public void CreateNewCanvas()
		{
			var matrix = new ColorData[SIZE * SIZE];
			for (int i = 0; i < SIZE; i++)
			{
				for (int j = 0; j < SIZE; j++)
				{
					matrix[SIZE * i + j] = new ColorData
					{
						RowIndex = i,
						ColumnIndex = j,
						Hex = "#FFFFFF"
					};
				}
			}
			_context.Canvas.Add(new Models.Canvas { ColorData = matrix });
			_context.SaveChanges();
		}
	}
}
*/