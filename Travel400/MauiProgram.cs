using Microsoft.Extensions.Logging;
using Travel400.Service;
using Supabase;

namespace Travel400
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // 🟢 إعداد Supabase
            var supabaseUrl = "https://zfmosgesavmejatbjbxc.supabase.co"; // ✅ استبدلها بالرابط الخاص بك
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InpmbW9zZ2VzYXZtZWphdGJqYnhjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTAyNjI4NzcsImV4cCI6MjA2NTgzODg3N30.4Ol8mJ2uIaXymsfcmzNVbbJUswvjLamBSjnZaP0nLVE";       // ✅ استبدلها بالمفتاح الخاص بك
            var supabase = new Supabase.Client(supabaseUrl, supabaseKey);

            // 🟢 تسجيل Supabase وخدمة البيانات
            builder.Services.AddSingleton(supabase);
            builder.Services.AddSingleton<SupabaseService>();  // تستخدمها بدل DataService

            // (اختياري): إذا أردت الاحتفاظ بـ DataService لمهام أخرى
            // builder.Services.AddSingleton<DataService>();

            return builder.Build();
        }
    }
}
