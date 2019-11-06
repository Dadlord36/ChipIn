using System;
using System.Threading.Tasks;
using HttpRequests;
using NUnit.Framework;
using UnityEngine;

namespace RunTimeTests
{
    public class GuestRegistrationTest
    {
        [Test]
        public void  GuestRegistrationTestSimplePasses()
        {
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
/*        [UnityTest]
        public IEnumerator GuestRegistrationTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }*/
    }
}