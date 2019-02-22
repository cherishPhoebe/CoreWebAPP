using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using DAO;
using Microsoft.EntityFrameworkCore;
using CoreWebApp.Extensions;

namespace CoreWebApp
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
            services.AddMvc();

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.NewGuid()));
            services.AddTransient<OperationService, OperationService>();

            // 配置session，session是基于IDistributed构建的，所以必须先配置 DistributedMemoryCache
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.Cookie.Name = ".Phoebe.Session";
                // 设置回话过期时间，独立于cookie的过期时间
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                // session是无锁的，如果两个请求都尝试修改回话的内容，最后一个会成功，此外，Session被实现为一个内容连贯的会话，就是说所有的内容都是一起存储。这就意味着，如果两个请求是在修改会话中不同的部分，不同键的值，他们还是会互相造成影响的。
            });

            // 使用缓存
            services.AddMemoryCache();
            // 数据库连接字符串
            var connection = @"Server=.;Database=Note;UID=sa;PWD=sa;";
            // 添加sql server 上下文
            services.AddDbContext<NoteContext>(options =>
            {
                options.UseSqlServer(connection);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            // 注册编码，防止中文乱码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            loggerFactory.AddNLog();
            // 自定义中间件
            app.UseHttpContextItems();
            app.UseStaticFiles();

            // 必须在UseMvc前调用，因为在调用UseSession之前访问session则会出现InvalidOpreationException的错误
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id:int?}");
            });
        }
    }
}
