using OpenChart.NoteSkins;
using NUnit.Framework;
using System;

namespace OpenChart.Tests.NoteSkins
{
    public class TestNoteSkin
    {
        [Test]
        public void Test_AddKeyModeSkin_ThrowsIfExists()
        {
            var ns = new NoteSkin("test");
            var keySkin = new KeyModeSkin(4);

            ns.AddKeyModeSkin(keySkin);

            Assert.Throws<ArgumentException>(() => ns.AddKeyModeSkin(keySkin));
            Assert.Throws<ArgumentException>(() => ns.AddKeyModeSkin(new KeyModeSkin(4)));
        }

        [Test]
        public void Test_AddKeyModeSkin_CanAddMultipleSkins()
        {
            var ns = new NoteSkin("test");
            var keySkins = new KeyModeSkin[] {
                new KeyModeSkin(4),
                new KeyModeSkin(7),
                new KeyModeSkin(100),
            };

            ns.AddKeyModeSkin(keySkins[0]);
            ns.AddKeyModeSkin(keySkins[1]);
            ns.AddKeyModeSkin(keySkins[2]);

            Assert.AreSame(keySkins[0], ns.GetKeyModeSkin(keySkins[0].KeyCount));
            Assert.AreSame(keySkins[1], ns.GetKeyModeSkin(keySkins[1].KeyCount));
            Assert.AreSame(keySkins[2], ns.GetKeyModeSkin(keySkins[2].KeyCount));
        }

        [Test]
        public void Test_GetKeyModeSkin_GetsExistingSkin()
        {
            var ns = new NoteSkin("test");
            var keySkin = new KeyModeSkin(4);

            ns.AddKeyModeSkin(keySkin);

            Assert.AreSame(keySkin, ns.GetKeyModeSkin(4));
        }

        [Test]
        public void Test_GetKeyModeSkin_ReturnsNullWhenDoesntExist()
        {
            var ns = new NoteSkin("test");
            var keySkin = new KeyModeSkin(4);

            Assert.IsNull(ns.GetKeyModeSkin(4));
            ns.AddKeyModeSkin(keySkin);
            Assert.IsNotNull(ns.GetKeyModeSkin(4));
        }
    }
}
