using CoreWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        public IApiRepository ItemsRepo { get; set; }
        private readonly TestSetting _testsetting;

        public ItemController(IApiRepository itemsRepo, IOptions<TestSetting> testsetting)
        {
            ItemsRepo = itemsRepo;
            _testsetting = testsetting.Value;
        }

        //[Route("/Error")]
        //public IActionResult Index()
        //{
        //    return Content("Error occurred with status code: "+HttpContext.Response.StatusCode.ToString());            // Handle error here
        //}


        [Produces("text/json")]
        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            //return null;
            //var a = 2;
            //var b = 1;

            //var x = a / (b-1);
            return ItemsRepo.GetAll();
        }

        [HttpGet("{name}", Name = "GetItemById")]
        public IActionResult GetOne(string name)
        {
            var item = ItemsRepo.GetOne(name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);//http 200 - OK
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            item.Value += _testsetting.Message;
            ItemsRepo.Add(item);
            //return CreatedAtRoute("GetItemById", new { name = item.Name }, item);
            return CreatedAtAction("GetOne", new { name = item.Name }, item); // http 201 -- Created

        }

        [HttpPut("{name}",Name ="UpdateItem")]
        public IActionResult Update(string name,[FromBody] Item item)
        {
            if (item == null|| item.Name != name)
            {
                return BadRequest();
            }
            
            var  foundItem= ItemsRepo.GetOne(name);
            if (foundItem == null)
            {
                return NotFound();
            }
            ItemsRepo.Update(item);
            return new NoContentResult(); // http 204 -- No content

        }

        [HttpDelete("{name}", Name = "DeleteItem")]
        public IActionResult Delete(string name)
        {
            var foundItem = ItemsRepo.GetOne(name);
            if (foundItem == null)
            {
                return NotFound();
            }
            ItemsRepo.Delete(name);
            return new NoContentResult(); // http 204 -- No content

        }

    }
}
