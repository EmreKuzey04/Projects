using Application.Middlewares;
using Persistance.Contexts;

namespace WebAPI
{
    public static class PipelineConfiguration
    {
        public async static void ConfigureHttpPipeline(this WebApplication app)
        {
            //seedlemeyi çalıştırmak için

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TradewndContext>();
                await dbContext.SeedAsync();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomExceptionHandler(); // kendi eklediğim exception yakalayan middleware 

            app.UseAuthentication(); //bu sıralamayla koyulmalı bu ikisi 
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
