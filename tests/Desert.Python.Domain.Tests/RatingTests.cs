using System;
using Desert.Python.Domain.catalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Desert.Python.Domain.Tests;

[TestClass]
public class RatingTests
{
    [TestMethod]
    public void Can_Create_New_Rating()
    {
			var rating = new Rating(1, "Mike", "Great fit!");

			Assert.AreEqual(1, rating.Stars);
			Assert.AreEqual("Mike", rating.UserName);
			Assert.AreEqual("Great fit!", rating.Review);
    }

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Cannot_Create_Rating_With_Invalid_Stars()
		{
			var rating = new Rating(0, "Mike", "Great fit!");
		}

		[TestMethod]
		public void Can_Create_Add_Rating()
		{
			var item = new Item("Name", "Description", "Brand", 10.00m);
			var rating = new Rating(1, "Mike", "Great fit!");

			item.AddRating(rating);

			Assert.AreEqual(rating, item.Ratings[0]);
		}
}