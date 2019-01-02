using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFlightSearchAPI.UnitTests
{
    public class MockDBContext
    {
        protected static Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> inputList) where T : class
        {
            var queryableList = inputList.AsQueryable();
            var mockedDbSet = new Mock<DbSet<T>>();

            mockedDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockedDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockedDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockedDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            return mockedDbSet;
        }
    }
}
