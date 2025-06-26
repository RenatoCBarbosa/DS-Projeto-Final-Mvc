var builder = WebApplication.CreateBuilder(args);

// 1. Configuração para ignorar SSL em desenvolvimento (REMOVA EM PRODUÇÃO)
#if DEBUG
System.Net.ServicePointManager.ServerCertificateValidationCallback += 
    (sender, cert, chain, sslPolicyErrors) => true;
#endif

// 2. Configuração do HttpClient com política de certificado flexível
builder.Services.AddHttpClient("SomeeApi", client => 
{
    client.BaseAddress = new Uri("https://APIFINAL-OS-RENATO.msagi.somex.com/");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = 
        HttpCliecurl -o .gitignore https://raw.githubusercontent.com/dotnet/core/main/.gitignorentHandler.DangerousAcceptAnyServerCertificateValidator
});

// 3. Serviços essenciais
builder.Services.AddControllersWithViews();



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 4. Pipeline de configuração
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Habilita HSTS (remova se estiver com problemas de SSL)
}
else
{
    app.UseDeveloperExceptionPage();
}

// 5. Middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// 6. Rotas
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Locacao}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToController("Index", "Home");

app.Run();