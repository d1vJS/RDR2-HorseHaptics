# 🐴 HorseHaptics

🇬🇧 English · [🇹🇷 Türkçe aşağıda](#-türkçe)

A DualSense vibration mod for RDR2 that **improves the feeling of riding a horse**.

The controller vibrates lightly while the horse walks, faster and stronger as it speeds up, and most intensely at a gallop. The vibration stops when the horse stops, when you dismount or when you fall. It does not touch the adaptive triggers or the lightbar, so it works together with trigger mods such as Shtivi's DSX mod without causing problems.

> ⚠️ Use in story mode only. Script mods in RDR2 Online can get you banned.

## Installation

1. Create a **`scripts`** folder next to `RDR2.exe` if it doesn't exist.
2. Put `HorseHaptics.dll` in the **`scripts`** folder.
3. Turn on **Rumble to Haptics** in DSX.
4. Start the game and get on a horse.

**F9:** on/off · **F10:** reload settings

## Settings

On first run, a `HorseHaptics.ini` file is created in the `scripts` folder. Open it with Notepad, change it, save it, then press F10 in game.

- `Intensity` (default 40): Overall strength. Lower it if it vibrates too much.
- `Rhythm` (default 1.0): Beat speed. Try 0.8 if it feels too fast, 1.2 if too slow.
- `PulseMs` (default 35): Length of each pulse.
- `Mode` (default 1): 1 = follows Arthur's bounce, 0 = speed-based timer only.

## Script Hook is required

This mod reads information from inside the game. RDR2 doesn't let outside mods access it, so both of these are required. Installing them is simple, just copy files. Everything goes next to `RDR2.exe`:

- **Script Hook RDR2:** http://www.dev-c.com/rdr2/scripthookrdr2/
- **ScriptHookRDR2DotNet V2:** https://github.com/Halen84/ScriptHookRDR2DotNet-V2/releases

If either one is missing, the mod won't load at all.

## Uninstall

Delete `HorseHaptics.dll` and `HorseHaptics.ini` from the `scripts` folder.

## Building from source

If you want to build it yourself: open `HorseHaptics.csproj` in Visual Studio 2022, choose **Release** and build. First take `ScriptHookRDRNetAPI.dll` from ScriptHookRDR2DotNet V2 and put it in the same folder as `HorseHaptics.csproj` (the project looks for it as a reference). Copy the resulting `HorseHaptics.dll` to the `scripts` folder.

## Thanks

[Halen84](https://github.com/Halen84/ScriptHookRDR2DotNet-V2) (ScriptHookRDR2DotNet V2) · Alexander Blade (Script Hook RDR2) · [DSX](https://store.steampowered.com/app/1812620/DSX/)

This is an unofficial fan project and is not affiliated with Rockstar or Sony.

---

# 🇹🇷 Türkçe

RDR2'de **at hissiyatını artıran** DualSense titreşim eklentisi.

At yürürken hafif, hızlanınca daha sık ve güçlü, dört nala giderken en yoğun şekilde titrer. At durunca, attan inince ya da düşünce titreşim kesilir. Tetiklere ve ledbara dokunmaz, Shtivi'nin DSX modu gibi tetik modlarıyla birlikte çalışır, problem yaşatmaz.

> ⚠️ Sadece hikâye modunda kullan. RDR2 Online'da script modu banlanma sebebi olabilir.

## Kurulum

1. `RDR2.exe`'nin yanında **`scripts`** klasörü yoksa oluştur.
2. `HorseHaptics.dll`'i **`scripts`** klasörüne koy.
3. DSX'te **Rumble to Haptics**'i aç.
4. Oyunu aç, ata bin.

**F9:** aç/kapat · **F10:** ayarları yeniden yükle

## Ayarlar

İlk çalıştırmada `scripts` klasöründe `HorseHaptics.ini` oluşur. Not Defteri'yle aç, değiştir, kaydet, oyunda F10'a bas.

- `Intensity` (varsayılan 40): Genel şiddet. Çok titriyorsa düşür.
- `Rhythm` (varsayılan 1.0): Ritim hızı. Hızlıysa 0.8, yavaşsa 1.2 dene.
- `PulseMs` (varsayılan 35): Her vuruşun süresi.
- `Mode` (varsayılan 1): 1 = Arthur'un sallanmasına göre, 0 = sadece hıza göre.

## Çalışması için Script Hook gerekir

Bu eklenti oyunun içindeki bilgiyi okuyor. RDR2 dışarıdan eklentilerin buna erişmesine izin vermediği için ikisi de şart. Kurulumu çok basit, sadece dosya kopyalamak, hepsi `RDR2.exe`'nin yanına atılır:

- **Script Hook RDR2:** http://www.dev-c.com/rdr2/scripthookrdr2/
- **ScriptHookRDR2DotNet V2:** https://github.com/Halen84/ScriptHookRDR2DotNet-V2/releases

İkisi de yoksa eklenti hiç yüklenmez.

## Kaldırma

`scripts` klasöründeki `HorseHaptics.dll` ve `HorseHaptics.ini` dosyalarını sil.

## Kaynak koddan derleme

Kendin derlemek istersen: Visual Studio 2022 ile `HorseHaptics.csproj`'u aç, **Release** seç, build et. Önce ScriptHookRDR2DotNet V2'den `ScriptHookRDRNetAPI.dll` dosyasını alıp `HorseHaptics.csproj` ile aynı klasöre koy (proje bunu referans olarak arıyor). Çıkan `HorseHaptics.dll`'i `scripts` klasörüne at.

## Teşekkürler

[Halen84](https://github.com/Halen84/ScriptHookRDR2DotNet-V2) (ScriptHookRDR2DotNet V2) · Alexander Blade (Script Hook RDR2) · [DSX](https://store.steampowered.com/app/1812620/DSX/)

Resmî olmayan bir hayran projesidir, Rockstar veya Sony ile bağlantısı yoktur.
