
using System;
using DJBeautyAndThaiSpaBooking;
using NUnit.Framework;

namespace djbeautyandthaispa.tests.TimeClassTests
{
    [TestFixture]
    public class TimeTests
    {

        [TestCase("12.10")]
        [TestCase("12:10")]
        [TestCase("12 10")]
        public void ConvertStringHoursAndMinutesToTimeSpanFormat_WhereInputIsOnlyValueWithDifferentSeperator_ReturnCorrectTimeFormat(string testInput)
        {

            var result = Time.ConvertStringHoursAndMinutesToTimeSpanFormat(testInput);
            Assert.That(result, Is.EqualTo(new TimeSpan(12, 10, 00)));
        }
        [TestCase("10.12.20")]
        [TestCase("10.12")]
        [TestCase("10.12.123456.456487.54564654.4564")]
        public void ConvertStringHoursAndMinutesToTimeSpanFormat_WhereInputincludeSecond_ReturnCorrectResult(string testInput)
        {
            var result = Time.ConvertStringHoursAndMinutesToTimeSpanFormat(testInput);
            Assert.AreEqual(new TimeSpan(10, 12, 00), result);
        }
        [Test]
        [TestCase("1c:20")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(".")]
        [TestCase("21")]
        [TestCase("10:10:21:21")]
        [TestCase("1")]
        public void ValidateInputCorrectTimeSpanFormat_WhereValueContainLetterOrWrongNumberOfInput_ReturnFalse(string input)
        {
            var result = Time.ValidateInputCorrectTimeSpanFormat(input);
            Assert.IsFalse(result);
        }
        [TestCase("10:10")]
        [TestCase("10.00")]
        [TestCase("10 00")]
        public void ValidateInputCorrectTimeSpanFormat_WhereValueIsCorrectSequence_ReturnTrue(string input)
        {
            var result = Time.ValidateInputCorrectTimeSpanFormat(input);
            Assert.IsTrue(result);
        }
    }
}
