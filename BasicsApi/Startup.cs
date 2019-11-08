using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Models;
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

            //���ڳ�ʼ����ʱ�����Ǿ���Ҫ�ã�����ʹ��Bind�ķ�ʽ��ȡ����
            //�����ð󶨵�JwtSettingsʵ����
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            services.AddAuthentication(options => {
                //��֤middleware����
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => {
                //��Ҫ��jwt  token��������
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //Token�䷢����
                    ValidIssuer = jwtSettings.Issuer,
                    //�䷢��˭
                    ValidAudience = jwtSettings.Audience,
                    //�����keyҪ���м��ܣ���Ҫ����Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
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
            services.AddAutoMapper(typeof(Startup));
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
