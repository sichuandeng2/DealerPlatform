using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DealerPlatformApiDemo.Api.Models;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Service.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DealerPlatformApiDemo.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddAutoMapper (typeof (ServiceProfile));
            services.AddSwaggerGen (s => {
                s.SwaggerDoc ("v1", new OpenApiInfo {
                    Version = "v1",
                        Description = "DealerPlatformApiDemo"
                });
                //添加安全定义
                s.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
                    Description = "直接在下框中输入JWT生成的Token，格式为Bearer {token}，注意二者之间需要有空格",
                        Name = "Authorization", //jwt默认的参数名称
                        In = ParameterLocation.Header, //吧jwt存放在Header中  
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                });
                //添加安全要求 {value1,value2}
                s.AddSecurityRequirement (new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        }, new string[] { }
                    }
                });
            });
            services.AddCors (c => c.AddPolicy ("any", p => p.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ()));
            //SqlHelperW.ConStr = Configuration.GetConnectionString("ConStr");
            services.AddDbContext<SqlHelperW> ();
            services.AddDbContext<SqlHelperR> ();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //var i1 = typeof(Repository<>);
            //��ȡCore��ķ������ 
            var asmCore = Assembly.Load ("DealerPlatformApiDemo.Core");
            //��ȡ���������еķǳ�����
            var repository = asmCore.GetTypes ().FirstOrDefault (m => m.Name == "Repository`1");
            var irepository = repository.GetInterface ("IRepository`1").GetGenericTypeDefinition (); //asmCore.GetTypes().FirstOrDefault(m => m.Name == "IRepository`1");
            services.AddScoped (irepository, repository);

            //����Serviceҵ���߼���
            //var asmService1 = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory+ "ClassLibrary.Test.dll");
            var asmService = Assembly.Load ("DealerPlatformApiDemo.Service");
            //ͨ�������ȡ���ڲ����е���
            var serviceTypes = asmService.GetTypes ().Where (m => !m.GetTypeInfo ().IsAbstract && !m.GetTypeInfo ().IsEnum);
            foreach (var serviceType in serviceTypes) {
                foreach (var serviceInterface in serviceType.GetInterfaces ()) {
                    services.AddScoped (serviceInterface, serviceType);
                }
            }

            var token = new TokenManagerModel ();
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (x => {
                //是否用HTTPS
                x.RequireHttpsMetadata = false;
                //授权成功后是否存储令牌 
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    //获取用于签名验证的SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (token.Sectet)),
                    //获取令牌的颁发者
                    ValidIssuer = token.Issuer,
                    //获取访问观众
                    ValidAudience = token.Audience
                };
            });
            services.AddControllers();
            // .AddJsonOptions(m=>{
            //     m.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            
            //启动认证中间件
            app.UseAuthentication ();

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseSwagger ();
            app.UseSwaggerUI (s => {
                s.SwaggerEndpoint ("/swagger/v1/swagger.json", "DealerPlatformApiDemo");
            });
            app.UseRouting ();
            
            app.UseCors ();
            //Jwt用户验证（授权中间件）
            app.UseAuthorization ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}