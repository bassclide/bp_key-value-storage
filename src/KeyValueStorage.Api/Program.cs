namespace KeyValueStorage.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<KeyValueStorage.Core.Repository>();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddSwaggerGen();
        
        builder.WebHost.ConfigureKestrel(options =>
        {
            // Set the limit to 500 MB (in bytes)
            // options.Limits.MaxRequestBodySize = 524288000; 
    
            // OR set it to null to allow unlimited size
            options.Limits.MaxRequestBodySize = null;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}