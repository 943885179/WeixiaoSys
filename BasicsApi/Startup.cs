using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using BasicsApi.conmon;
using Microsoft.OpenApi.Models;
using System.IO;

namespace BasicsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<RSASettings>(Configuration.GetSection("RSASettings"));
            services.AddScoped<WeixiaoErrorIMiddleware>();
            services.AddScoped<WeixiaoRequestIMiddleware>();
            services.AddSingleton<ILoggerHelper, LoggerHelper>();
            //services.AddSingleton<RSAHelper>();
            //services.AddSingleton<IRSAHelper, RSAHelper>();
            //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            //将配置绑定到JwtSettings实例中
            services.AddSwaggerGen(options =>
            {
                //options.CustomSchemaIds((type) => type.FullName);
                //配置第一个Doc
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API_1",
                    Version = "v1",
                    Description = "这是说明信息",//说明
                    TermsOfService = new Uri("https://github.com/943885179/CoreDemo"),
                    Contact = new OpenApiContact
                    {
                        Name = "mzj",
                        Email = "943885179@qq.com",
                        Url = new Uri("https://github.com/943885179/CoreDemo")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "许可证名字",
                        Url = new Uri("https://github.com/943885179/CoreDemo")
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "BasicsApi.xml");
                options.IncludeXmlComments(xmlPath);
            });
                var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //主要是jwt  token参数设置
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(10),
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = jwtSettings.Audience,//Audience
                    ValidIssuer = jwtSettings.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))//拿到SecurityKey
                    ////Token颁发机构
                    //ValidIssuer = jwtSettings.Issuer,
                    ////颁发给谁
                    //ValidAudience = jwtSettings.Audience,
                    ////这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    //ValidateIssuerSigningKey=true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    //ValidateLifetime=true,
                    ////允许的服务器时间偏移量
                    //ClockSkew=TimeSpan.Zero

                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("cores",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowAnyOrigin()
                    .AllowCredentials();
                    // builder.WithOrigins("http://example.com",
                    //                    "http://www.contoso.com");
                });
            });
            services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperConfigs>(), typeof(Startup));
            services.AddDbContext<WeixiaoSysContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseStaticFiles();
            //ErrorHandlingExtensions.UseErrorHandling(app);
            app.UseWeixiaoError();
            app.UserWeixiaoRequest();
            app.UseHttpsRedirection();
            app.UseRouting();
            // 跨域
            app.UseCors("cores");
            // 设置允许所有来源跨域
            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //});

            // 设置只允许特定来源可以跨域
            //app.UseCors(options =>
            //{
            //    options.WithOrigins("http://localhost:3000", "http://127.0.0.1"); // 允许特定ip跨域
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowCredentials();
            //});
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
