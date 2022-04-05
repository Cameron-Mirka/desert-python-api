using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Desert.Python.Domain.catalog;
using Desert.Python.Data;

namespace Desert.Python.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CatalogController : ControllerBase
	{

		private readonly StoreContext _db;

		public CatalogController(StoreContext db)
		{
			_db = db;
		}

		[HttpGet]
		public IActionResult GetItems()
		{
			return Ok(_db.Items);
		}

		[HttpGet("{id:int}")]
		public IActionResult GetItem(int id)
		{
			var item = _db.Items.Find(id);

			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[HttpPost]
		public IActionResult NewItem(Item item)
		{
			_db.Items.Add(item);
			_db.SaveChanges();
			return Created($"/catalog/{item.Id}", item);
		}

		[HttpPost("{id:int}/ratings")]
		public IActionResult PostRating(int id, [FromBody] Rating rating)
		{
			var item = _db.Items.Find(id);

			if (item == null)
			{
				return NotFound();
			}

			item.AddRating(rating);
			_db.SaveChanges();

			return Ok(item);
		}

		[HttpPut("{id:int}")]
		public IActionResult UpdateItem(int id, Item item)
		{
			if (id != item.Id)
			{
				return BadRequest();
			}

			if (_db.Items.Find(id) == null)
			{
				return NotFound();
			}

			_db.Entry(item).State = EntityState.Modified;
			_db.SaveChanges();

			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public IActionResult DeleteItem(int id)
		{
			var item = _db.Items.Find(id);

			if (item == null)
			{
				return NotFound();
			}

			_db.Items.Remove(item);
			_db.SaveChanges();

			return Ok();
		}
	}
}