using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[] {
                new IdentityResources.OpenId()
            };
        }
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>()
            {
                new ApiResource("app","myapp1")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>() {//使用connect/token调用
                new Client
                    {
                        ClientId = "client",
                        // 没有交互性用户，使用 clientid/secret 实现认证。
                        AllowedGrantTypes = GrantTypes.ClientCredentials,//设置模式，客户端模式
                        //用于认证的密码
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        // scopes that client has access to
                        //客户端有权访问的范围（Scopes）
                        AllowedScopes = { "app" }
                    },
                new Client()
                {
                    ClientId="PasswordClient",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets={ new Secret("secretPassword".Sha256())},
                    AllowedScopes={ "app" }
                },
                 new Client()
                {
                    ClientId="ImpLicitClient",
                    AllowedGrantTypes=GrantTypes.Implicit,//OpenID Connect 简化模式（implicit）
                    ClientSecrets={ new Secret("implicitSecrets".Sha256()) },
                    RequireConsent=true,   //用户选择同意认证授权
                    RedirectUris={ "https://localhost:5008/signin-oidc" }, //指定允许的URI返回令牌或授权码(我们的客户端地址)
                    PostLogoutRedirectUris={ "https://localhost:5008/signout-callback-oidc" },//注销后重定向地址 
                    LogoUri="https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=3298365745,618961144&fm=27&gp=0.jpg",
                  ////运行访问的资源
                     AllowedScopes = {                       //客户端允许访问个人信息资源的范围
                      IdentityServerConstants.StandardScopes.Profile,
                      IdentityServerConstants.StandardScopes.OpenId,
                      IdentityServerConstants.StandardScopes.Email,
                      IdentityServerConstants.StandardScopes.Address,
                      IdentityServerConstants.StandardScopes.Phone,
                      "app"
                     }
                 },new Client()
                 {
                     ClientId = "mvc",
                     ClientName = "Hybrid",
                     AllowedGrantTypes = GrantTypes.Hybrid,
                     ClientSecrets =
                     {
                         new Secret("secret".Sha256())
                     },
                     RedirectUris           = { "http://localhost:5002/signin-oidc" },
                     PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                     AllowedScopes =
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         "app"
   
                     },
                     AllowOfflineAccess = true
                 }

            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>() {
                new TestUser(){
                 SubjectId="001",
                 Username="weixiao",
                 Password="123"
                }
            };
        }
      
    }
}