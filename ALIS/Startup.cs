using ALIS_DataAccess.Data;
using ALIS_DataAccess.Initializer;
using ALIS_DataAccess.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection")
                ));

            /*services.AddDefaultIdentity<IdentityUser>()                 
                 .AddEntityFrameworkStores<ApplicationDbContext>();*/

            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddDefaultTokenProviders()
                 .AddDefaultUI()
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDistributedMemoryCache();

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorTypeRepository, AuthorTypeRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookCopiesCirculationRepository, BookCopiesCirculationRepository>();
            services.AddScoped<IBookCopiesOperationTypeRepository, BookCopiesOperationTypeRepository>();
            services.AddScoped<IBookCopyRepository, BookCopyRepository>();
            services.AddScoped<IBooksToAuthorRepository, BooksToAuthorRepository>();
            services.AddScoped<IBooksToTagRepository, BooksToTagRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGrifRepository, GrifRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            




            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            dbInitializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
