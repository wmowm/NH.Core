using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Service;
using Tibos.Service.Contract;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Tibos.Confing.autofac
{
    public class DefaultModule : Module
    {
        //Autofac容器
        protected override void Load(ContainerBuilder builder)
        {


            //拦截器
            //builder.Register(c => new AOPTest());

            //注入类
            builder.RegisterType<UsersService>().As<UsersIService>().PropertiesAutowired().EnableInterfaceInterceptors();

        }
    }
}
