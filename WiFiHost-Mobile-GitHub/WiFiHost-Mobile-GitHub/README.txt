WIFI HOST MOBILE - C# / .NET 10 ANDROID

Este projeto cria um APK Android em C#.
O botão abre as configurações oficiais de hotspot do Android.

IMPORTANTE:
- Android moderno não permite que aplicativos comuns ativem/desativem livremente o hotspot nem definam SSID/senha por API pública.
- O hotspot com compartilhamento de internet é configurado pelo próprio Android.
- A disponibilidade do hotspot depende do aparelho, operadora e versão do Android.

REQUISITOS:
- .NET 10 SDK
- workload Android
- Android SDK e Java/JDK configurados

COMPILAR:
Abra o terminal nesta pasta e execute:

dotnet workload install android

dotnet publish -f net10.0-android -c Release

O APK normalmente será gerado em:
bin\Release\net10.0-android\

Se o SDK Android não estiver instalado, instale os componentes Android pelo Visual Studio Installer.
Não é necessário instalar o Android Studio se o SDK estiver instalado pelo Visual Studio.
