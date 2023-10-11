using uTestAndForms.Data;
using uTestAndForms.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmployeeRespository, MockUserRepository>();

builder.Services.AddTransient<IDataAccess, DataAccess>();

builder.Services.AddTransient<IgetConnection, getConnection>();

builder.Services.AddTransient<IDeleteUser, DeleteUser>();


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