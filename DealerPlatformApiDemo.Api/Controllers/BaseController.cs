using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DealerPlatformApiDemo.Api.Controllers
{
    [EnableCors("any")]
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public string UserNo =>HttpContext.Items["userNo"].ToString();
        //var val = function(){  }
        // Action action = delegate(){
        //     return delegate(){
        //         return 1;
        //     }
        // };
        //  Action action  = ()=>()=>1
    }
}
