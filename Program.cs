
using uTestAndForms.Data;
using uTestAndForms.Models;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IEmployeeRespository,MockUserRepository>();

builder.Services.AddSingleton<IDataAccess, DataAccess>();

builder.Services.AddSingleton<IgetConnection, getConnection>();

builder.Services.AddSingleton<IDeleteUser, DeleteUser>();



builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();



app.Run();