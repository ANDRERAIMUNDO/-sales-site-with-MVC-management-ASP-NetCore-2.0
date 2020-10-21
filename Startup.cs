using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LojaVirtual.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Session;
using LojaVirtual.Models.Libres.Sessao;
using LojaVirtual.Models.Libres.Login;
using Microsoft.CodeAnalysis.Options;
using LojaVirtual.Models.Libres.Email;
using System.Net.Mail;
using System.Net;
using LojaVirtual.Models.Libres.Middleware;

namespace LojaVirtual
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
            services.AddHttpContextAccessor();//aplicando injeção de dependencia Login
            services.AddScoped<IClienteRepository, ClienteRepository>();//aplicando a injenção de dependencia Cliente
            services.AddScoped<INewsLetterRepository, NewsletterRepository>();//aplicando a injeção dependencia Contato 
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();//aplicando a injeção dependencia Colaborador
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();//aplicand o injeção de dependencia Categoria


            //SMTP
            services.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:UserName"),
                    Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };
                return smtp;
            });

            services.AddScoped<GerenciarEmail>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session Configuração
            services.AddMemoryCache();//guardar os dados na memoria
            services.AddSession(
                options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(15);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });
            services.AddScoped<Sessao>();//compartilha essa classe no projeto
            services.AddScoped<LoginCliente>();//validadando classe sessão de armazenamento de dados
            services.AddScoped<LoginColaborador>();//validando classe sessão dde armazenamento de dados
            services.AddDistributedMemoryCache();
            services.AddMvc(options => options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x =>
            "O campo deve ser preenchido")).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddSessionStateTempDataProvider();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<LojaVirtualContext>(options
                => options.UseMySql(Configuration.GetConnectionString("LojaVirtualContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseCookiePolicy();
                app.UseSession();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                   );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
