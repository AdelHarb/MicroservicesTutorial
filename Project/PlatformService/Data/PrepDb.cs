namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                System.Console.WriteLine("--> Applying Migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                System.Console.WriteLine("Seeding Data...");

                context.Platforms.AddRange(
          new Platform() {Name = "Dot Net",Publisher = "Microsoft",Cost = "Free"},
                    new Platform() {Name = "Sql Server Express",Publisher = "Microsoft",Cost = "Free"},
                    new Platform() {Name = "Kubernetes",Publisher = "Cloud Native Computing Foundation",Cost = "Free"}
                );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("--> We already have data");
            }
        }
    }
}