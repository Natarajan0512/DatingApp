
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
   
        public Startup(IConfiguration config)
        {
            _config = config;

        }

		public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
	  	{
			    services.AddDbContext<DataContext>(options => 
				{
					options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
				});
            	services.AddControllers();
				services.AddCors();
	  	}
	
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
			app.UseDeveloperExceptionPage();
			}
			
			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

			app.UseAuthorization();

			app.UseEndpoints (endpoints =>
			{
			endpoints.MapControllers();
			});

		}
	}
}