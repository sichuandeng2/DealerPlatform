using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealerPlatformApiDemo.Common.RedisHelper;
using DealerPlatformApiDemo.Core;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DealerPlatformApiDemo.Api.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new [] {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        //private readonly IRepository<Customers> _repository;

        public WeatherForecastController (ILogger<WeatherForecastController> logger /*, IRepository<Customers> repository*/ ) {
            _logger = logger;
            //_repository = repository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get () {
            //RedisHelper.SetStringMemory("testString","Hello Ace",TimeSpan.FromHours(1));
            //var str = RedisHelper.GetStringMemory("testString");
            //var dic = new Dictionary<string, ListByCustomsEntity>();
            // Dictionary<string,string>dic =new Dictionary<string, string>();
            // dic.Add("Id","1");
            // dic.Add("UserName","Ace");
            // RedisHelper.SetHashMemory("testHash",dic);

            // private System.String _CartGuid;
            // private System.String _CustomerNo;
            // private System.String _ProductNo;
            // private System.Int32 _ProductNum;
            // private System.Boolean _CartSelected;

            List<ShoppingCarts> carts = new List<ShoppingCarts> ();
            carts.Add (new ShoppingCarts {
                Id = 1,
                    CustomerNo = "AHAQWYZ",
                    CartGuid = Guid.NewGuid ().ToString (),
                    ProductNo = "asdasdxvcafca",
                    ProductNum = 10,
                    CartSelected = true
            });
            carts.Add (new ShoppingCarts {
                Id = 2,
                    CustomerNo = "AHAQWYZ",
                    CartGuid = Guid.NewGuid ().ToString (),
                    ProductNo = "asdasdxvcafca",
                    ProductNum = 8,
                    CartSelected = true
            });
            RedisHelper.SetHashMemory ("testEntityHash", carts);
            var list = RedisHelper.GetHashMemory<ShoppingCarts>("testEntityHash:1");
            RedisHelper.RemoveKey("testEntityHash:1");
            RedisHelper.HashDelete("testEntityHash:2","CustomerNo");
            //dic.Add("Id", new ListByCustomsEntity()
            //{
            //    Value = "5805",
            //    IsAnd = true,
            //    IsEq = true
            //});
            //dic.Add("AreaNo", new ListByCustomsEntity()
            //{
            //    Value = "06",
            //    IsAnd = true,
            //    IsEq = true
            //});
            //var customers = _repository.ListByCustoms(dic);
            //var ctms = _repository.ListByCustomWhereIn("Id",
            //    new[] { "5801", "5802", "5803", "5804", "5805", "5806" });
            ////func(var1, ...var2)
            //var ctms = _repository.ListByCustomWhereIn("Id", "5801", "5802", "5803", "5804", "5805", "5806");
            var rng = new Random ();
            return Enumerable.Range (1, 5).Select (index => new WeatherForecast {
                    Date = DateTime.Now.AddDays (index),
                        TemperatureC = rng.Next (-20, 55),
                        Summary = Summaries[rng.Next (Summaries.Length)]
                })
                .ToArray ();
        }

        //public void XXXX()
        //{
        //    return;
        //    string a = "xxxx";
        //}
    }
}