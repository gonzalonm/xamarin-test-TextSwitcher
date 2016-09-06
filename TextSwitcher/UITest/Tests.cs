using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;

namespace UITest
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.StartApp();
        }

        /**
         * Checks the action bar title
         */
        [Test]
        public void TestCheckActionBarTitle()
        {
            // Get the text of action bar
            // action_bar_container has two levels: Toolbar and TextView
            var currentText = app.Query(x => x.Id("action_bar_container").Child(0).Child(0).Invoke("getText"));

            // Check for the text
            Assert.NotNull(currentText);
            Assert.AreEqual(currentText[0], "TextSwitcher");
        }

        /**
         * Checks for intro text
         */
        [Test]
        public void TestCheckIntroText()
        {
            // Get the intro text
            var currentText = app.Query(x => x.Id("Intro").Invoke("getText"));

            // Check for the text
            Assert.NotNull(currentText);
            Assert.AreEqual(currentText[0], "This sample illustrates the use of a TextSwitcher to display text. \n\nClick the button below to set new text in the TextSwitcher and observe the in and out fade animations. ");
        }

        /**
         * Checks if the 'Next' buttton is available
         */
        [Test]
        public void TestNextButtonAvailable()
        {
            // Wait for Next button
            app.WaitForElement(c => c.Marked("Button").Text("Next"));

            // Get the text button
            var currentText = app.Query(x => x.Id("Button").Invoke("getText").Value<string>());

            // Checks if the button has 'Next' as text
            Assert.NotNull(currentText);
            Assert.AreEqual(currentText[0], "Next");

            // Checks for the button text color
            // -1 is for White color based on 
            // https://developer.android.com/reference/android/graphics/Color.html#WHITE

            var backgroundResults = app.Query(x => x.Id("Button").Invoke("getCurrentTextColor").Value<int>());
            Assert.NotNull(backgroundResults);
            Assert.AreEqual(backgroundResults[0], -1);
        }

        /**
         * Checks if the counter increment after tap on 'Next' button
         */
        [Test]
        public void TestTapOnceOnNextButtonIncrementCounter()
        {
            // Tap on 'Next' button
            app.Tap(c => c.Marked("Button"));

            // Get the text view
            var currentText = app.Query(x => x.Id("Switcher").Child(0).Invoke("getText"));
            
            // Checks if the text is '1'
            Assert.NotNull(currentText);
            Assert.AreEqual(currentText[0], "1");
        }

        [Test]
        public void TestTapTwiceOnNextButtonIncrementCounter()
        {
            // Tap on 'Next' button
            app.Tap(c => c.Marked("Button"));
            app.Tap(c => c.Marked("Button"));

            // Get the text view
            var currentText = app.Query(x => x.Id("Switcher").Child(0).Invoke("getText"));

            // Checks if the text is '2'
            Assert.NotNull(currentText);
            Assert.AreEqual(currentText[0], "2");
        }

        [Test]
        public void TestCheckCounterTextColor()
        {
            // Checks for the button text color
            // -1 is for White color based on 
            // https://developer.android.com/reference/android/graphics/Color.html#WHITE

            var currentTextColor = app.Query(x => x.Id("Button").Invoke("getCurrentTextColor"));

            // Checks if the text color is white
            Assert.NotNull(currentTextColor);
            Assert.AreEqual(currentTextColor[0], -1);
        }
    }
}

