using System;
using System.Collections.Generic;

// Özel Hata Sınıfı
class KuantumCokusuException : Exception
{
    public KuantumCokusuException(string id) : base($"SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... Patlayan Nesne ID: {id}") { }
}

// Arayüz (Interface)
interface IKritik
{
    void AcilDurumSogutmasi();
}

// Soyut Sınıf (Abstract Class)
abstract class KuantumNesnesi
{
    // Özellikler
    public string ID { get; set; }
    private double _stabilite;
    public int TehlikeSeviyesi { get; set; }

    // Encapsulation (Kapsülleme)
    public double Stabilite
    {
        get { return _stabilite; }
        set
        {
            if (value > 100) _stabilite = 100;
            else _stabilite = value;

            // Hata fırlatma kontrolü
            if (_stabilite <= 0)
            {
                throw new KuantumCokusuException(ID);
            }
        }
    }

    public KuantumNesnesi(string id, int tehlike)
    {
        ID = id;
        TehlikeSeviyesi = tehlike;
        // GÜNCELLEME: Stabilite 0-100 arası rastgele başlıyor
        // (Çökmemesi için en az 10 veriyoruz, ama tamamen şans)
        _stabilite = new Random().NextDouble() * 100;
    }

    // Soyut Metot
    public abstract void AnalizEt();

    // Durum Bilgisi
    public string DurumBilgisi()
    {
        return $"[{GetType().Name}] ID: {ID} - Stabilite: %{Stabilite:F2} - Tehlike: {TehlikeSeviyesi}";
    }
}

// Veri Paketi
class VeriPaketi : KuantumNesnesi
{
    public VeriPaketi(string id) : base(id, 1) { }

    public override void AnalizEt()
    {
        Stabilite -= 5;
        Console.WriteLine("Veri içeriği okundu.");
    }
}

// Karanlık Madde
class KaranlikMadde : KuantumNesnesi, IKritik
{
    public KaranlikMadde(string id) : base(id, 5) { }

    public override void AnalizEt()
    {
        Stabilite -= 15;
    }

    public void AcilDurumSogutmasi()
    {
        Stabilite += 50;
        Console.WriteLine($"{ID} soğutuldu.");
    }
}

// Anti Madde
class AntiMadde : KuantumNesnesi, IKritik
{
    public AntiMadde(string id) : base(id, 10) { }

    public override void AnalizEt()
    {
        Stabilite -= 25;
        Console.WriteLine("Evrenin dokusu titriyor...");
    }

    public void AcilDurumSogutmasi()
    {
        Stabilite += 50;
        Console.WriteLine($"{ID} soğutuldu (Zorlu işlem!).");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Generic List
        List<KuantumNesnesi> envanter = new List<KuantumNesnesi>();
        Random rnd = new Random();
        int sayac = 1;

        Console.WriteLine("KUANTUM AMBARI'NA HOŞ GELDİNİZ");

        while (true)
        {
            try
            {
                Console.WriteLine("\n--- KUANTUM AMBARI KONTROL PANELİ ---");
                Console.WriteLine("1. Yeni Nesne Ekle");
                Console.WriteLine("2. Tüm Envanteri Listele");
                Console.WriteLine("3. Nesneyi Analiz Et");
                Console.WriteLine("4. Acil Durum Soğutması Yap");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "1")
                {
                    int tur = rnd.Next(1, 4);
                    string yeniId = "NESNE-" + sayac++;
                    // Nesne oluşturulurken gecikme ekleyerek Random seed'inin değişmesini sağlıyoruz
                    System.Threading.Thread.Sleep(20);

                    if (tur == 1) envanter.Add(new VeriPaketi(yeniId));
                    else if (tur == 2) envanter.Add(new KaranlikMadde(yeniId));
                    else envanter.Add(new AntiMadde(yeniId));
                    Console.WriteLine($"{envanter[envanter.Count - 1].GetType().Name} ({yeniId}) ambara eklendi.");
                }
                else if (secim == "2")
                {
                    foreach (var nesne in envanter)
                    {
                        Console.WriteLine(nesne.DurumBilgisi());
                    }
                }
                else if (secim == "3")
                {
                    Console.Write("Analiz edilecek ID: ");
                    string id = Console.ReadLine();
                    var nesne = envanter.Find(x => x.ID == id);
                    if (nesne != null) nesne.AnalizEt();
                    else Console.WriteLine("Nesne bulunamadı!");
                }
                else if (secim == "4")
                {
                    Console.Write("Soğutulacak ID: ");
                    string id = Console.ReadLine();
                    var nesne = envanter.Find(x => x.ID == id);

                    if (nesne != null)
                    {
                        if (nesne is IKritik kritikNesne)
                        {
                            kritikNesne.AcilDurumSogutmasi();
                        }
                        else
                        {
                            Console.WriteLine("Bu nesne soğutulamaz!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nesne bulunamadı!");
                    }
                }
                else if (secim == "5") break;
            }
            catch (KuantumCokusuException ex)
            {
                Console.WriteLine("\n*********************************");
                Console.WriteLine(ex.Message.ToUpper());
                Console.WriteLine("*********************************");
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bilinmeyen hata: " + ex.Message);
            }
        }
    }
}

