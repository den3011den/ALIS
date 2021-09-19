using ALIS_DataAccess.Data;
using ALIS_DataAccess.Initializer;
using ALIS_DataAccess.Repository;
using ALIS_Utility.SendGrid;
using ALIS_Utility.StringEncryptionUtility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS
{
    public class Startup
    {

 
        public static void SetAppSettingValue(string key, string value, string appSettingsJsonFilePath = null)
        {

            var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

            jsonObj[key] = value;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(appSettingsJsonFilePath, output);
        }


        public void StartupEncrypt(IWebHostEnvironment env)
        {
            
            if (Configuration.GetValue<string>("Encrypted") == "NO")
            {
                var appConnectionString = Configuration.GetValue<string>("DefaultConnection");
                var appSendGridKey = Configuration.GetValue<string>("SendGridKey");
                var appSendGridEmailFrom = Configuration.GetValue<string>("SendGridEmailFrom");
                var appTpuSmtpUserName = Configuration.GetValue<string>("TpuSmtpUserName");
                var appTpuSmtpUserPassword = Configuration.GetValue<string>("TpuSmtpUserPassword");
                var appSyncfusionLicenseKey = Configuration.GetValue<string>("SyncfusionLicenseKey");

                appConnectionString = StringEncryptionUtility.Encrypt(appConnectionString);                                
                appSendGridKey = StringEncryptionUtility.Encrypt(appSendGridKey);
                appSendGridEmailFrom = StringEncryptionUtility.Encrypt(appSendGridEmailFrom);
                appTpuSmtpUserName = StringEncryptionUtility.Encrypt(appTpuSmtpUserName);
                appTpuSmtpUserPassword = StringEncryptionUtility.Encrypt(appTpuSmtpUserPassword);
                appSyncfusionLicenseKey = StringEncryptionUtility.Encrypt(appSyncfusionLicenseKey);

                SetAppSettingValue("DefaultConnection", appConnectionString, System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));
                SetAppSettingValue("SendGridKey", appSendGridKey, System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));
                SetAppSettingValue("SendGridEmailFrom", appSendGridEmailFrom, System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));
                SetAppSettingValue("TpuSmtpUserName", appTpuSmtpUserName, System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));
                SetAppSettingValue("TpuSmtpUserPassword", appTpuSmtpUserPassword, System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));
                SetAppSettingValue("SyncfusionLicenseKey", appSyncfusionLicenseKey, System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));
                SetAppSettingValue("Encrypted", "YES", System.IO.Path.Combine(env.ContentRootPath, "appsettings.json"));

            }


        }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;

        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             
            StartupEncrypt(_env);

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(                 
                StringEncryptionUtility.Decrypt(Configuration.GetValue<string>("DefaultConnection"))
                ));

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

            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<EmailOptions>(Configuration);

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

            app.UseFastReport();


            var SyncfusionLicenseKey = StringEncryptionUtility.Decrypt(Configuration.GetValue<string>("SyncfusionLicenseKey"));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicenseKey);

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
