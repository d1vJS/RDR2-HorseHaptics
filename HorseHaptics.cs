// HorseHaptics v3 - RDR2 at sürme titreşimi (DualSense + DSX için)
// v3: binicinin gövde sıçramasından (dikey hareket) vuruş anı yakalanır; yakalanamazsa zamanlayıcıya düşer.
// v2: ritim atın GERÇEK hızına bağlı (sabit 4 kademe yok), şiddet düşürüldü,
//     ayarlar scripts\HorseHaptics.ini dosyasından okunur (F10 = ayarları yeniden yükle).
// Tetiklere ve ledbara DOKUNMAZ. F9 = aç/kapat. Sadece hikâye modunda kullan.

using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using RDR2;
using RDR2.Native;

namespace HorseHaptics
{
    public class Main : Script
    {
        private const int PadIndex = 0;
        private const Keys ToggleKey = Keys.F9;
        private const Keys ReloadKey = Keys.F10;

        // Varsayılanlar (ini yoksa bunlar kullanılır)
        private float intensity = 40f;   // 0-100, genel şiddet
        private float rhythm = 1.0f;     // 1 = normal, 0.8 = daha seyrek, 1.2 = daha sık
        private int pulseMs = 35;        // her vuruşun süresi (kısa = daha "tık")
        private int minSpeedTenths = 8;  // bunun altında hız = titreşim yok (m/s x10)

        private bool enabled = true;
        private bool shaking = false;
        private int mode = 1;            // 1 = sıçrama senkronu (deneysel), 0 = sadece zamanlayıcı
        private float zAvg = 0f, prevH = 0f, prevV = 0f;
        private bool zInit = false;
        private int lastBeatAt = 0;
        private int nextBeatAt = 0;

        public Main()
        {
            LoadIni();
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Aborted += OnAbort;
            Interval = 0;
        }

        private string IniPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts", "HorseHaptics.ini");
        }

        private void LoadIni()
        {
            try
            {
                string p = IniPath();
                if (!File.Exists(p))
                {
                    File.WriteAllLines(p, new string[] {
                        "; HorseHaptics ayarları. Kaydet, oyunda F10'a bas.",
                        "; Intensity: genel şiddet 0-100 (çok titriyorsa düşür)",
                        "Intensity=40",
                        "; Rhythm: ritim hızı. 1.0 normal. Nal sesinden hızlıysa 0.8, yavaşsa 1.2 dene",
                        "Rhythm=1.0",
                        "; PulseMs: her vuruşun süresi ms. Küçük = daha kısa 'tık'",
                        "PulseMs=35",
                        "; Mode: 1 = atın sıçramasına senkron (deneysel), 0 = sadece hıza göre zamanlayıcı",
                        "Mode=1"
                    });
                    return;
                }
                foreach (string raw in File.ReadAllLines(p))
                {
                    string line = raw.Trim();
                    if (line.Length == 0 || line.StartsWith(";")) continue;
                    int eq = line.IndexOf('=');
                    if (eq < 0) continue;
                    string k = line.Substring(0, eq).Trim().ToLowerInvariant();
                    string v = line.Substring(eq + 1).Trim();
                    float f;
                    if (!float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out f)) continue;
                    if (k == "intensity") intensity = Math.Max(0f, Math.Min(100f, f));
                    else if (k == "rhythm") rhythm = Math.Max(0.3f, Math.Min(3f, f));
                    else if (k == "mode") mode = f >= 0.5f ? 1 : 0;
                    else if (k == "pulsems") pulseMs = (int)Math.Max(10f, Math.Min(200f, f));
                }
            }
            catch { }
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;
            if (!enabled || player == null || !player.IsOnHorse) { StopShake(); return; }

            Ped horse = player.CurrentMount;
            if (horse == null) { StopShake(); return; }

            float speed = ENTITY.GET_ENTITY_SPEED(horse.Handle); // m/s
            if (speed * 10f < minSpeedTenths) { StopShake(); zInit = false; return; }

            int now = Game.GameTime;

            // Ritim süresi (hıza göre beklenen vuruş aralığı)
            float beatsPerSec = (1.0f + speed * 0.30f) * rhythm;
            int beatMs = (int)(1000f / beatsPerSec);
            if (beatMs < 190) beatMs = 190;

            float freq = (20f + speed * 6f) * (intensity / 100f);
            if (freq > 255f) freq = 255f;
            if (freq < 1f) { StopShake(); return; }

            bool fire = false;
            if (mode == 1)
            {
                // Binicinin dikey hareketi: yavaş ortalamayı çıkar, dibe vurma anını yakala
                float z = player.Position.Z;
                if (!zInit) { zAvg = z; prevH = 0f; prevV = 0f; zInit = true; }
                zAvg += (z - zAvg) * 0.08f;
                float h = z - zAvg;
                float v = h - prevH;
                bool bottom = prevV < 0f && v >= 0f && prevH < -0.004f;
                prevH = h; prevV = v;
                if (bottom && now - lastBeatAt > (int)(beatMs * 0.55f)) fire = true;
                // Sıçrama yakalanamazsa zamanlayıcıya düş
                else if (now - lastBeatAt > (int)(beatMs * 1.6f)) fire = true;
            }
            else
            {
                if (now >= nextBeatAt) fire = true;
            }

            if (fire)
            {
                PAD.SET_CONTROL_SHAKE(PadIndex, pulseMs, (int)freq);
                shaking = true;
                lastBeatAt = now;
                nextBeatAt = now + beatMs;
            }
        }

        private void StopShake()
        {
            if (shaking) { PAD.STOP_CONTROL_SHAKE(PadIndex); shaking = false; }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == ToggleKey)
            {
                enabled = !enabled;
                if (!enabled) StopShake();
                RDR2.UI.Screen.PrintSubtitle(enabled ? "HorseHaptics: ACIK" : "HorseHaptics: KAPALI");
            }
            else if (e.KeyCode == ReloadKey)
            {
                LoadIni();
                RDR2.UI.Screen.PrintSubtitle("HorseHaptics: ayarlar yenilendi");
            }
        }

        private void OnAbort(object sender, EventArgs e) { StopShake(); }
    }
}
