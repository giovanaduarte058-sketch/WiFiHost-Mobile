
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
    int dp(float value) =>
        (int)(value * Resources!.DisplayMetrics!.Density + 0.5f);

    GradientDrawable Bg(int color, int radius = 16)
    {
        var drawable = new GradientDrawable();
        drawable.SetColor(new Color(color));
        drawable.SetCornerRadius(dp(radius));
        return drawable;
    }

    TextView Text(string value, float size, int color, bool bold = false)
    {
        var text = new TextView(this);
        text.Text = value;
        text.TextSize = size;
        text.SetTextColor(new Color(color));

        if (bold)
            text.SetTypeface(null, TypefaceStyle.Bold);

        return text;
    }

    Button MakeButton(string label)
    {
        var button = new Button(this);
        button.Text = label;
        button.TextSize = 15;
        button.SetTextColor(Color.White);
        button.Background = Bg(unchecked((int)0xFF2563EB), 14);
        return button;
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        Window!.SetStatusBarColor(
            new Color(unchecked((int)0xFF0B1224)));

        Window.SetNavigationBarColor(
            new Color(unchecked((int)0xFF0B1224)));

        Window.DecorView!.SystemUiVisibility = 0;

        var root = new LinearLayout(this)
        {
            Orientation = Orientation.Vertical
        };

        root.SetPadding(dp(22), dp(28), dp(22), dp(22));
        root.SetBackgroundColor(
            new Color(unchecked((int)0xFF0B1224)));

        var title = Text(
            "WiFi Host",
            30,
            unchecked((int)0xFFFFFFFF),
            true
        );

        root.AddView(title);

        var subtitle = Text(
            "Compartilhe a internet do seu celular",
            15,
            unchecked((int)0xFFB8C7E6)
        );

        var subtitleParams =
            new LinearLayout.LayoutParams(-1, -2);

        subtitleParams.BottomMargin = dp(24);
        root.AddView(subtitle, subtitleParams);

        var card = new LinearLayout(this)
        {
            Orientation = Orientation.Vertical
        };

        card.SetPadding(dp(18), dp(18), dp(18), dp(18));
        card.Background =
            Bg(unchecked((int)0xFF17233D), 18);

        root.AddView(
            card,
            new LinearLayout.LayoutParams(-1, -2)
        );

        card.AddView(
            Text(
                "Ponto de acesso móvel",
                20,
                unchecked((int)0xFFFFFFFF),
                true
            )
        );

        var description = Text(
            "O Android controla o hotspot e define as opções de segurança. Este aplicativo abre a tela oficial do sistema para você ativá-lo.",
            14,
            unchecked((int)0xFFD2DCF0)
        );

        description.SetLineSpacing(dp(4), 1f);

        var descriptionParams =
            new LinearLayout.LayoutParams(-1, -2);

        descriptionParams.TopMargin = dp(10);
        descriptionParams.BottomMargin = dp(18);

        card.AddView(description, descriptionParams);

        var openButton =
            MakeButton("ABRIR CONFIGURAÇÕES DO HOTSPOT");

        card.AddView(
            openButton,
            new LinearLayout.LayoutParams(-1, dp(54))
        );

        openButton.Click += (_, _) => OpenHotspotSettings();

        var steps = Text(
            "Como usar\n" +
            "1. Abra as configurações do hotspot.\n" +
            "2. Defina o nome e a senha da rede.\n" +
            "3. Ative o ponto de acesso.\n" +
            "4. Conecte seus outros dispositivos.",
            15,
            unchecked((int)0xFFFFFFFF)
        );

        steps.SetLineSpacing(dp(6), 1f);

        var stepsParams =
            new LinearLayout.LayoutParams(-1, -2);

        stepsParams.TopMargin = dp(24);
        root.AddView(steps, stepsParams);

        var note = Text(
            "Observação: versões recentes do Android bloqueiam que aplicativos comuns liguem o hotspot ou escolham a senha diretamente. O controle é feito pelo sistema Android.",
            12,
            unchecked((int)0xFFFFC875)
        );

        note.SetLineSpacing(dp(4), 1f);

        var noteParams =
            new LinearLayout.LayoutParams(-1, -2);

        noteParams.TopMargin = dp(24);
        root.AddView(note, noteParams);

        SetContentView(root);
    }

    void OpenHotspotSettings()
    {
        Intent? intent = null;

        try
        {
            intent = new Intent("android.settings.TETHER_SETTINGS");
        }
        catch
        {
        }

        try
        {
            if (intent is not null &&
                intent.ResolveActivity(PackageManager) is not null)
            {
                StartActivity(intent);
                return;
            }
        }
        catch
        {
        }

        try
        {
            StartActivity(
                new Intent(Android.Provider.Settings.ActionWirelessSettings)
            );
        }
        catch
        {
            Toast.MakeText(
                this,
                "Abra Configurações > Rede e Internet > Roteador Wi-Fi",
                ToastLength.Long
            )!.Show();
        }
    }
}
