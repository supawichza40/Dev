using DJBeautyAndThaiSpaBooking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace djbeautyandthaispa.tests.ReceptionTests
{
    [TestFixture]
    public class ReceptionTests
    {
        [Test]
        public void TakeBooking_WithNoParameter_UpdateEmployeeBooking() 
        {
            Reception sut = new Reception("Dear", new List<Employee> 
            {
                new Employee("Joy", new string[] {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday" }) 

            });
            Mock<Reader> moqReader = new Mock<Reader>();
            Mock<Reception> re = new Mock<Reception>();
            
            sut.TakeBooking();
            var numberOfBook = sut.todayStaffList[0].Workingtime.Count;
            Assert.That(numberOfBook, Is.EqualTo(1));
        }
    }
}
