using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Mechanics.Cell;
using Mechanics.Exceptions;
using System.Collections.Generic;
using Mechanics.Geometry;
using Mechanics.Geometry.Range;
using Mechanics.Grid;

namespace Mechanics.GridManager
{
    [Serializable]
    public class GridManager : IGridManager
    {
        internal static GridManager Load(Stream stream)
        {
            var formatter = new BinaryFormatter();

            using (var compress = new DeflateStream(stream, CompressionMode.Decompress))
            {
                return (GridManager)formatter.Deserialize(compress);
            }
        }

        public void Save(Stream stream)
        {
            var formatter = new BinaryFormatter();

            using (var compress = new DeflateStream(stream, CompressionLevel.Optimal))
            {
                formatter.Serialize(compress, this);
            }
        }

        public IGrid SetCell(Point p, NumericValue value)
        {
            var tos = _grids.Peek();
            var newGrid = tos.SetCell(p, value);

            newGrid = ClearRange(p, value, newGrid);

            CheckWhetherThereIsASolution(newGrid);

            _grids.Push(newGrid);
            _redo.Clear();

            return newGrid;
        }

        private void CheckWhetherThereIsASolution(IGrid newGrid)
        {
            for (int x = 0; x < Grid.Grid.Extension; x++)
            {
                CheckWhetherThereIsASolution(newGrid, new Vertical(x));
            }

            for (int y = 0; y < Grid.Grid.Extension; y++)
            {
                CheckWhetherThereIsASolution(newGrid, new Horizontal(y));
            }

            for (int x = 0; x < Grid.Grid.Extension; x += Grid.Grid.ExtensionNeighborhood)
            {
                for (int y = 0; y < Grid.Grid.Extension; y += Grid.Grid.ExtensionNeighborhood)
                {
                    CheckWhetherThereIsASolution(newGrid, new Neighbor(new Point(x, y)));
                }
            }
        }

        private static readonly List<NumericValue> _allNumericValues = Cell.Cell._allNumericValues.ToList();


        private void CheckWhetherThereIsASolution(IGrid grid, IEnumerable<Point> range)
        {
            var points = range.ToList();
            if (_allNumericValues.Exists(value => !points.Exists(p => grid[p].MayHaveValue(value))))
            {
                throw new NoMoreSolutionException();
            }
        }



        private static IGrid ClearRange(Point p, NumericValue value, IGrid newGrid)
        {
            foreach (var rangeP in new Range(p))
            {
                if (newGrid[rangeP].IsDefined)
                {
                    continue;
                }
                newGrid = newGrid.ExcludeValueFromCell(rangeP, value);
                if (newGrid[rangeP].IsDefined)
                {
                    newGrid = ClearRange(rangeP, newGrid[rangeP].Value, newGrid);
                }
            }
            return newGrid;
        }

        public IGrid CurrentGrid
        {
            get
            {
                return _grids.Peek();
            }
        }

        public bool CanUndo()
        {
            return _grids.Count > 1;
        }

        public IGrid Undo()
        {
            _redo.Push(_grids.Pop());
            return _grids.Peek();
        }

        public bool CanRedo()
        {
            return _redo.Any();
        }

        public IGrid Redo()
        {
            var toRedo = _redo.Pop();
            _grids.Push(toRedo);

            return toRedo;
        }

        public bool CanReset()
        {
            return _grids.Count != 1 || _redo.Count != 0;
        }


        private readonly Stack<IGrid> _grids = new Stack<IGrid>();
        private readonly Stack<IGrid> _redo = new Stack<IGrid>();

        public static IGridManager CreateNewGridManager()
        {
            return new GridManager();
        }

        private GridManager()
        {
            var grid = Factory.Instance.CreateEmptyGrid();
            _grids.Push(grid);
        }

        public override bool Equals(object obj)
        {
            var other = obj as GridManager;
            return other != null && SameContent(this, other);
        }

        private static bool SameContent(GridManager gridManager, GridManager other)
        {
            var gridsA = gridManager._grids.ToList();
            var gridsB = other._grids.ToList();

            if (gridsA.Count != gridsB.Count)
            {
                return false;
            }

            for (int i = 0; i < gridsA.Count; i++)
            {
                if (!gridsA[i].Equals(gridsB[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _grids.Count;
        }
    }
}
