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
            //��appsettings.json�е�JwtSettings�����ļ���ȡ��JwtSettings�У����Ǹ������ط��õ�
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<RSASettings>(Configuration.GetSection("RSASettings"));
            services.AddScoped<WeixiaoErrorIMiddleware>();
            services.AddScoped<WeixiaoRequestIMiddleware>();
            services.AddSingleton<ILoggerHelper, LoggerHelper>();
            //services.AddSingleton<RSAHelper>();
            //services.AddSingleton<IRSAHelper, RSAHelper>();
            //���ڳ�ʼ����ʱ�����Ǿ���Ҫ�ã�����ʹ��Bind�ķ�ʽ��ȡ����
            //�����ð󶨵�JwtSettingsʵ����
            services.AddSwaggerGen(options =>
            {
                //options.CustomSchemaIds((type) => type.FullName);
                //���õ�һ��Doc
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API_1",
                    Version = "v1",
                    Description = "����˵����Ϣ",//˵��
                    TermsOfService = new Uri("https://github.com/943885179/CoreDemo"),
                    Contact = new OpenApiContact
                    {
                        Name = "mzj",
                        Email = "943885179@qq.com",
                        Url = new Uri("https://github.com/943885179/CoreDemo")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "���֤����",
                        Url = new Uri("https://github.com/943885179/CoreDemo")
                    }
                });
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "BasicsApi.xml");
                options.IncludeXmlComments(xmlPath);
            });
                var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            services.AddAuthentication(options =>
            {
                //��֤middleware����
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //��Ҫ��jwt  token��������
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ClockSkew = TimeSpan.FromSeconds(10),
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = jwtSettings.Audience,//Audience
                    ValidIssuer = jwtSettings.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))//�õ�SecurityKey
                    ////Token�䷢����
                    //ValidIssuer = jwtSettings.Issuer,
                    ////�䷢��˭
                    //ValidAudience = jwtSettings.Audience,
                    ////�����keyҪ���м��ܣ���Ҫ����Microsoft.IdentityModel.Tokens
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    //ValidateIssuerSigningKey=true,
                    ////�Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
                    //ValidateLifetime=true,
                    ////����ķ�����ʱ��ƫ����
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
            // ����
            app.UseCors("cores");
            // ��������������Դ����
            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //});

            // ����ֻ�����ض���Դ���Կ���
            //app.UseCors(options =>
            //{
            //    options.WithOrigins("http://localhost:3000", "http://127.0.0.1"); // �����ض�ip����
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
