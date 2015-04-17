#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion

namespace HeyMrDJ.CoreTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LookupTest
    {
#if DEBUG
        [TestMethod]
#endif
        public void SimpleLookupTest()
        {
            var l = new Lookup();
            l.T();
        }
    }
}
