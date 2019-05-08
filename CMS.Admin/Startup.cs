using Application;
using Application.DepartmentApp;
using Application.MenuApp;
using Application.RoleApp;
using Application.UserApp;
using Domain.IRepositories;
using EntityFrameworkCore;
using EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // 初始化映射关系
            ZYMapper.Initialize();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("Default");
            // 添加数据库上下文
            services.AddDbContext<ZYDBContext>(options =>
            {
                options.UseSqlServer(sqlConnectionString);

            });
            

            // 依赖注入
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleAppService, RoleAppService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // 添加Session服务,管理会话状态
            services.AddSession();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });

            // 初始化数据
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
