using Hexagon.Lib.Coordinates;

namespace Hexagon.Lib
{
    public static class Shapes
    {
        /// <summary>
        /// Generate a hexagon shape to N levels.
        /// </summary>
        /// <param name="n">Level/layer count</param>
        /// <param name="sort">Must be sorted or not</param>
        /// <returns>Hexagons</returns>
        public static HashSet<Hex> Hexagons(int n, bool sort = false)
        {
            HashSet<Hex> cubes = [];

            for (int q = -n; q <= n; q++)
            {
                int r1 = Math.Max(-n, -q - n);
                int r2 = Math.Min(n, -q + n);

                for (int r = r1; r <= r2; r++)
                {
                    cubes.Add(new Hex(q, r, -q - r));
                }
            }

            if (sort)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return [.. sortedList];
            }

            return cubes;
        }

        /// <summary>
        /// Generate a parallelogram shape. WIP, DO NOT USE!
        /// </summary>
        /// <param name="q1">Min. width</param>
        /// <param name="q2">Max. width</param>
        /// <param name="r1">Min. length</param>
        /// <param name="r2">Max. length</param>
        /// <param name="sort">Sort hexagon</param>
        /// <returns></returns>
        public static HashSet<Hex> Parallelograms(int q1, int q2, int r1, int r2, bool sort = false)
        {
            HashSet<Hex> cubes = [];

            for (int q = q1; q <= q2; q++)
            {
                for (int r = r1; r <= r2; r++)
                {
                    cubes.Add(new Hex(q, r, -q - r));
                }
            }

            if (sort)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return [.. sortedList];
            }

            return cubes;
        }

        /// <summary>
        /// Generate a triangle shape. WIP, DO NOT USE!
        /// </summary>
        /// <param name="q1">Min. width</param>
        /// <param name="q2">Max. width</param>
        /// <param name="r1">Min. length</param>
        /// <param name="r2">Max. length</param>
        /// <param name="sort">Sort hexagon</param>
        /// <returns></returns>
        public static HashSet<Hex> Triangle(int size, bool sort = false)
        {
            HashSet<Hex> cubes = [];

            for (int q = 0; q <= size; q++)
            {
                for (int r = 0; r <= size - q; r++)
                {
                    cubes.Add(new Hex(q, r, -q - r));
                }
            }

            if (sort)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return [.. sortedList];
            }

            return cubes;
        }


        /// <summary>
        /// Generate a triangle pointy top shape. WIP, DO NOT USE!
        /// </summary>
        /// <param name="q1">Min. width</param>
        /// <param name="q2">Max. width</param>
        /// <param name="r1">Min. length</param>
        /// <param name="r2">Max. length</param>
        /// <param name="sort">Sort hexagon</param>
        /// <returns></returns>
        public static HashSet<Hex> TriangleTop(int size, bool sort = false)
        {
            HashSet<Hex> cubes = [];

            for (int q = 0; q <= size; q++)
            {
                for (int r = size - q; r <= size - q; r++)
                {
                    cubes.Add(new Hex(q, r, -q - r));
                }
            }

            if (sort)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return [.. sortedList];
            }

            return cubes;
        }
    }
}
