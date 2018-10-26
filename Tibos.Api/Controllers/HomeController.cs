using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tibos.Api.Annotation;
using Tibos.Confing.autofac;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.Service;
using Tibos.Service.Contract;
namespace Tibos.Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public UsersIService _UsersService { get; set; }
        public IMapper _IMapper { get; set; }


        // GET api/values
        [HttpGet]
        [AlwaysAccessibleAttribute]
        public IEnumerable<string> Get()
        {
            var config = JsonConfigurationHelper.GetAppSettings<ManageConfig>("ManageConfig.json", "ManageConfig");
            var res = _UsersService.Get(1);
            return new string[] { res.id.ToString(), res.user_name };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            Users user = new Users()
            {
                id = 1,
                email = "505613913@qq.com",
                mobile = "666",
                nick_name = "tibos"
            };
            var dto = _IMapper.Map<UsersDto>(user);
            return await Task.Run<string>(()=> {return Test(); });
        }

        private string Test()
        {
            return "666";
        }



    }



   

}
