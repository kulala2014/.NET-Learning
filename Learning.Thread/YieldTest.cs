using System;
using System.Collections.Generic;
using System.Threading;

namespace LearningThread
{
    public static class YieldTest
    {
        public static void Show()
        {
            foreach (int i in Power(2, 8))
            {
                Console.WriteLine(i);
                Thread.Sleep(2000);
            }

            var theGalaxies = new Galaxies();
            foreach (var galaxy in theGalaxies.NextGalaxy)
            {
                Console.WriteLine(galaxy.Name + " " + galaxy.MegaLightYears.ToString());
            }
        }

        public static IEnumerable<int> Power(int num, int expont)
        {
            int result = 1;
            for (int i = 0; i< expont; i++)
            {
                Console.WriteLine("yield1" + i);
                result = num * result;
                yield return result;
                Console.WriteLine("yield2" + i);
            }
        }
    }

    public class Galaxies
    {
        public IEnumerable<Galaxy> NextGalaxy
        {
            get
            {
                yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
            }
        }
    }

    public class Galaxy
    {
        public String Name { get; set; }
        public int MegaLightYears { get; set; }
    }
}
