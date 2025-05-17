var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(); 
builder.Services.AddHttpClient(); 

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization(); // ต้องมี
app.MapRazorPages();   

app.Run();

