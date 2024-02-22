using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_tut
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
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            // Set up custom content types - associating file extension to MIME type
            // Bring in the following 'using' statement:
            // using Microsoft.AspNetCore.StaticFiles;
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            // The MIME type for .GLB and .GLTF files are registered with IANA under the 'model' heading
            // https://www.iana.org/assignments/media-types/media-types.xhtml#model
            provider.Mappings[".glb"] = "model/gltf+binary";
            provider.Mappings[".gltf"] = "model/gltf+json";

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "wwwroot", "threedModels")),
                RequestPath = "/threedModels",
                ContentTypeProvider = provider
            });
            // cause error
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            // Path.Combine(env.ContentRootPath, "threedModels")),
            //    RequestPath = "wwwroot/threedModels",
            //    ContentTypeProvider = provider
            //});

            //check diff between Directory.GetCurrentDirectory() and env.ContentRootPath
            //https://stackoverflow.com/questions/60005281/three-js-problem-in-loading-3d-model-in-browser
            //  app.UseStaticFiles(new StaticFileOptions
            //  {
            //      FileProvider = new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
            //      RequestPath = "/StaticFiles",
            //      ContentTypeProvider = provider
            //  });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
