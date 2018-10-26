using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tibos.Test1.Dto;
using Tibos.Test1.Model;

namespace Tibos.Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ValuesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [AlwaysAccessibleAttribute(Name ="test")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Users user = new Users()
            {
                id=1,
                email = "505613913@qq.com",
                mobile = "666",
                nick_name = "tibos"
            };
            var dto = _mapper.Map<UsersDto>(user);

            return new string[] { "体验jenkins", "XXXXXXXXX" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
