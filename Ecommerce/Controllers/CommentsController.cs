using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly EcommerceDbContext context;
        public CommentsController(EcommerceDbContext appDbContext)
        {
            context = appDbContext;
        }
        // GET: api/<CommentsController>
        [HttpGet]
        public List<Comment> Get()
        {
            return context.Comments.ToList();
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "";
        }

        // POST api/<CommentsController>
        [HttpPost]
        public void Post([FromBody] Comment comment)
        {
            context.AddAsync(comment);
            context.SaveChanges();
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
