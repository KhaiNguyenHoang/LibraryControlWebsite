using LibaryControlWebsite.Models;
using LibaryControlWebsite.Models.Responsibility;
using LibaryControlWebsite.Models.Service;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// **1️⃣ Thêm dịch vụ vào container**
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryContext>();

// **2️⃣ Đăng ký các dịch vụ (Dependency Injection)**
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookcopyService, BookcopyService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookreviewService, BookreviewService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IWaitlistService, WaitlistService>();

// **3️⃣ Cấu hình xác thực (Authentication)**
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

// **4️⃣ Cấu hình quyền hạn (Authorization)**
builder.Services.AddAuthorization(options =>
{
    // Mặc định, tất cả các request sẽ được xác thực theo chính sách mặc định.
    options.FallbackPolicy = options.DefaultPolicy;
});

// **5️⃣ Cấu hình Session**
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// **6️⃣ Bật Razor Pages**
builder.Services.AddRazorPages();

var app = builder.Build();

// **7️⃣ Cấu hình middleware**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// **8️⃣ Middleware cho session, bảo mật và static files**
app.UseSession(); // ⚠ Đặt sau khi đã AddSession()
app.UseHttpsRedirection();
app.UseStaticFiles();

// **9️⃣ Middleware cho Authentication và Authorization**
app.UseRouting();
app.UseAuthentication(); // ✅ Đặt trước UseAuthorization()
app.UseAuthorization();

// **🔟 Cấu hình định tuyến mặc định**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

// **🔚 Chạy ứng dụng**
app.Run();
