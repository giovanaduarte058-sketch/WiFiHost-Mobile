using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace WiFiHostMobile;

[Activity(
    Label = "WiFi Host Mobile",
    MainLauncher = true,
    Exported = true,
    Theme = "@android:style/Theme.Material.NoActionBar",
    ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation |
                           Android.Content.PM.ConfigChanges.ScreenSize)]
public class MainActivity : Activity
{
    int dp(float value) => (int)(value * Resources!.DisplayMetrics!.Density + 0.5f);

    GradientDrawable Bg(int color, int radius = 16)
    {
        var d = new GradientDrawable();
        d.SetColor(new Color(color));
        d.SetCornerRadius(dp(radius));
        return d;
    }

    TextView Text(string value, float size, int color, bool bold = false)
    {
        var t = new TextView(this);
        t.Text = value;
        t.TextSize = size;
        t.SetTextColor(new Color(color));
        if (bold) t.SetTypeface(null, TypefaceStyle.Bold);
        return t;
    }

    Button MakeButton(string label)
    {
        var b = new Button(this);
        b.Text = label;
        b.TextSize = 15;
        b.SetTextColor(Color.White);
        b.Background = Bg(unchecked((int)0xFF2563EB), 14);
        return b;
    }

    public override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Window!.SetStatusBarColor(new Color(unchecked((int)0xFF0B1224)));
        Window.SetNavigationBarColor(new Color(unchecked((int)0xFF0B1224)));
        Window.DecorView!.SystemUiVisibility = 0;

        var root = new LinearLayout(this) { Orientation = Orientation.Vertical };
        root.SetPadding(dp(22), dp(28), dp(22), dp(22));
        root.SetBackgroundColor(new Color(unchecked((int)0xFF0B1224)));

        var title = Text("WiFi Host", 30, unchecked((int)0xFFFFFFFF), true);
        root.AddView(title);
        var subtitle = Text("Compartilhe a internet do seu celular", 15, unchecked((int)0xFFB8C7E6));
        var subLp = new LinearLayout.LayoutParams(-1, -2);
        subLp.BottomMargin = dp(24);
        root.AddView(subtitle, subLp);

        var card = new LinearLayout(this) { Orientation = Orientation.Vertical };
        card.SetPadding(dp(18), dp(18), dp(18), dp(18));
        card.Background = Bg(unchecked((int)0xFF17233D), 18);
        root.AddView(card, new LinearLayout.LayoutParams(-1, -2));

        card.AddView(Text("Ponto de acesso móvel", 20, unchecked((int)0xFFFFFFFF), true));
        var desc = Text("O Android controla o hotspot e define as opções de segurança. Este aplicativo abre a tela oficial do sistema para você ativá-lo.", 14, unchecked((int)0xFFD2DCF0));
        desc.SetLineSpacing(dp(4), 1f);
        var descLp = new LinearLayout.LayoutParams(-1, -2);
        descLp.TopMargin = dp(10);
        descLp.BottomMargin = dp(18);
        card.AddView(desc, descLp);

        var open = MakeButton("ABRIR CONFIGURAÇÕES DO HOTSPOT");
        card.AddView(open, new LinearLayout.LayoutParams(-1, dp(54)));
        open.Click += (_, _) => OpenHotspotSettings();

        var steps = Text("Como usar\n1. Abra as configurações do hotspot.\n2. Defina o nome e a senha da rede.\n3. Ative o ponto de acesso.\n4. Conecte seus outros dispositivos.", 15, unchecked((int)0xFFFFFFFF));
        steps.SetLineSpacing(dp(6), 1f);
        var stepsLp = new LinearLayout.LayoutParams(-1, -2);
        stepsLp.TopMargin = dp(24);
        root.AddView(steps, stepsLp);

        var note = Text("Observação: versões recentes do Android bloqueiam que aplicativos comuns liguem o hotspot ou escolham a senha diretamente. O controle é feito pelo sistema Android.", 12, unchecked((int)0xFFFFC875));
        note.SetLineSpacing(dp(4), 1f);
        var noteLp = new LinearLayout.LayoutParams(-1, -2);
        noteLp.TopMargin = dp(24);
        root.AddView(note, noteLp);

        SetContentView(root);
    }

    void OpenHotspotSettings()
    {
        Intent? intent = null;
        try { intent = new Intent("android.settings.TETHER_SETTINGS"); }
        catch { }

        try
        {
            if (intent is not null && intent.ResolveActivity(PackageManager) is not null)
            {
                StartActivity(intent);
                return;
            }
        }
        catch { }

        try
        {
            StartActivity(new Intent(Android.Provider.Settings.ActionWirelessSettings));
        }
        catch
        {
            Toast.MakeText(this, "Abra Configurações > Rede e Internet > Roteador Wi-Fi", ToastLength.Long)!.Show();
        }
    }
}
