using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using ArticlesViewer.Infrastructure;
using Azure.Storage.Blobs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CreateArticleCommand).Assembly);
});

builder.Services.AddSingleton(x => 
    new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobConnectionString")));

builder.Services.AddDbContext<AppDbContext>((DbContextOptionsBuilder options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    var identityOpt = builder.Configuration.GetSection("IdentityOptions");
    options.Password.RequiredLength = identityOpt.GetValue<int>("MinimumPasswordLength");
    options.User.RequireUniqueEmail = identityOpt.GetValue<bool>("RequireUniqueEmail");
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<IRepository<Article>, Repository<Article>>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IBlobRepository, BlobRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
