namespace Supermarket.API.Extensions;

public static class MiddlewareConfiguration
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API V1");
        //    });
        //}

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API V1");
        });

        return app;
    }
}
