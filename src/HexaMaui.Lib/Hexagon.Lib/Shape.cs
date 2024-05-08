using Hexagon.Lib.Coordinates;

namespace Hexagon.Lib
{
    public static class Shapes
    {
        public static HashSet<CubeInteger> Hexagons(int n, bool order = false)
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

            if (order)
            {
                var sortedList = cubes.ToList();
                sortedList.Sort();

                return sortedList.ToHashSet();
            }

            return cubes;
        }

    }
}
