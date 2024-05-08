using Hexagon.Lib.Coordinates;

namespace Hexagon.Lib
{
    public static class Shapes
    {
        public static HashSet<CubeInteger> Hexagons(int n, bool sort = false)
        {
            HashSet<CubeInteger> cubes = [];

            for (int q = -n; q <= n; q++)
            {
                int r1 = Math.Max(-n, -q - n);
                int r2 = Math.Min(n, -q + n);

                for (int r = r1; r <= r2; r++)
                {
                    cubes.Add(new CubeInteger(q, r, -q - r));
                }
            }

            if (sort)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return sortedList.ToHashSet();
            }

            return cubes;
        }

        public static HashSet<CubeInteger> Parallelogram(int width, int height, bool sort = false)
        {
            HashSet<CubeInteger> cubes = [];

            // Parallelogram with hexagons
            for (int q = 0; q < width; q++)
            {
                int r1 = Math.Max(-q, -height);
                int r2 = Math.Min(width - q, height);

                for (int r = r1; r < r2; r++)
                {
                    cubes.Add(new CubeInteger(q, r, -q - r));
                }
            }

            if (sort)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return sortedList.ToHashSet();
            }

            return cubes;
        }

    }
}
