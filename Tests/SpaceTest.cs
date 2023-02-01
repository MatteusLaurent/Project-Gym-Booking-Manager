using Gym_Booking_Manager;
using static Gym_Booking_Manager.Space;
using System;

// There are many tools for improving how you write your tests,
// but it's good enough to keep things simple to get things started.

// You can google-search to find C# and MSTest documentation
// from https://learn.microsoft.com to find out more.

namespace Tests
{
    [TestClass]
    public class SpaceTest
    {
        [TestMethod]
        public void CreateSpace()
        {
            Space testSpace = new Space(Space.Category.Studio, "Test Studio");
            Assert.IsNotNull(testSpace);
        }

        [TestMethod]
        public void CreateSpaceFromDictionaryBadCategoryException()
        {
            bool threw = false;
            var constructionArgs = new Dictionary<String, String>()
                {
                    { "category", "Sudio" },
                    { "name", "Dance Studio" }
                };

            try
            {
                Space sudioSpace = new Space(constructionArgs);
            }
            catch (Exception e)
            {
                threw = true;
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
            }

            Assert.IsTrue(threw);
        }

        [TestMethod]
        public void SpaceToString()
        {
            Space testSpace = new Space(Space.Category.Studio, "Test Studio");
            Assert.AreEqual(testSpace.ToString(), "Some nice string representation"); // Fix this
        }

        [TestMethod]
        public void SpaceCSVify()
        {
            Space testSpace = new Space(Space.Category.Studio, "Test Studio");
            Assert.AreEqual(testSpace.CSVify(), "category:Studio,name:Test Studio");
        }

        [TestMethod]
        public void SpaceCompareToSpace()
        {
            SortedSet<Space> sortedSpaces = new SortedSet<Space>();
            Space testStudio = new Space(Space.Category.Studio, "Test Studio");
            Space testHall = new Space(Space.Category.Hall, "Test Hall");

            sortedSpaces.Add(testStudio);
            sortedSpaces.Add(testHall);

            var spaceEnumerator = sortedSpaces.GetEnumerator();

            Assert.AreSame(testHall, spaceEnumerator.Current);
            spaceEnumerator.MoveNext();
            Assert.AreSame(testStudio, spaceEnumerator.Current);
        }

        
    }
}