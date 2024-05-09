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
    }
}
