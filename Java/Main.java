import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.Scanner;

// cd Java
// & "C:\Users\tonus\.vscode\extensions\redhat.java-1.50.0-win32-x64\jre\21.0.9-win32-x86_64\bin\java.exe" Main.java

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        List<KuantumNesnesi> envanter = new ArrayList<>();
        Random rnd = new Random();
        int sayac = 1;

        System.out.println("KUANTUM AMBARI JAVA SÜRÜMÜ");

        while (true) {
            try {
                System.out.println("\n--- KONTROL PANELİ ---");
                System.out.println("1. Yeni Nesne Ekle");
                System.out.println("2. Envanteri Listele");
                System.out.println("3. Analiz Et");
                System.out.println("4. Soğutma Yap");
                System.out.println("5. Çıkış");
                System.out.print("Seçim: ");
                String secim = scanner.nextLine();

                if (secim.equals("1")) {
                    int tur = rnd.nextInt(3);
                    String yeniId = "NESNE-" + sayac++;
                    if (tur == 0)
                        envanter.add(new VeriPaketi(yeniId));
                    else if (tur == 1)
                        envanter.add(new KaranlikMadde(yeniId));
                    else
                        envanter.add(new AntiMadde(yeniId));
                    System.out.println(yeniId + " eklendi.");
                } else if (secim.equals("2")) {
                    for (KuantumNesnesi kn : envanter) {
                        System.out.println(kn.durumBilgisi());
                    }
                } else if (secim.equals("3")) {
                    System.out.print("ID giriniz: ");
                    String id = scanner.nextLine();
                    boolean bulundu = false;
                    for (KuantumNesnesi kn : envanter) {
                        if (kn.getId().equals(id)) {
                            kn.analizEt();
                            bulundu = true;
                            break;
                        }
                    }
                    if (!bulundu)
                        System.out.println("Bulunamadı.");
                } else if (secim.equals("4")) {
                    System.out.print("ID giriniz: ");
                    String id = scanner.nextLine();
                    boolean bulundu = false;
                    for (KuantumNesnesi kn : envanter) {
                        if (kn.getId().equals(id)) {
                            if (kn instanceof IKritik) {
                                ((IKritik) kn).acilDurumSogutmasi();
                            } else {
                                System.out.println("Bu nesne soğutulamaz!");
                            }
                            bulundu = true;
                            break;
                        }
                    }
                    if (!bulundu)
                        System.out.println("Bulunamadı.");
                } else if (secim.equals("5"))
                    break;

            } catch (KuantumCokusuException e) {
                System.out.println("\n*********************************");
                System.out.println(e.getMessage().toUpperCase());
                System.out.println("*********************************");
                break;
            }
        }
        scanner.close();
    }
}

// Diğer Sınıflar
class KuantumCokusuException extends RuntimeException {
    public KuantumCokusuException(String id) {
        super("SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... Patlayan Nesne ID: " + id);
    }
}

interface IKritik {
    void acilDurumSogutmasi();
}

abstract class KuantumNesnesi {
    protected String id;
    private double stabilite;
    protected int tehlikeSeviyesi;

    public KuantumNesnesi(String id, int tehlike) {
        this.id = id;
        this.tehlikeSeviyesi = tehlike;
        // GÜNCELLEME: Stabilite 0-100 arası random
        this.stabilite = Math.random() * 100;
    }

    public String getId() {
        return id;
    }

    public double getStabilite() {
        return stabilite;
    }

    public void setStabilite(double deger) {
        if (deger > 100)
            this.stabilite = 100;
        else
            this.stabilite = deger;

        if (this.stabilite <= 0) {
            throw new KuantumCokusuException(this.id);
        }
    }

    public abstract void analizEt();

    public String durumBilgisi() {
        return "ID: " + id + " - Stabilite: %" + String.format("%.2f", stabilite) + " - Tehlike: " + tehlikeSeviyesi;
    }
}

class VeriPaketi extends KuantumNesnesi {
    public VeriPaketi(String id) {
        super(id, 1);
    }

    @Override
    public void analizEt() {
        setStabilite(getStabilite() - 5);
        System.out.println("Veri içeriği okundu.");
    }
}

class KaranlikMadde extends KuantumNesnesi implements IKritik {
    public KaranlikMadde(String id) {
        super(id, 5);
    }

    @Override
    public void analizEt() {
        setStabilite(getStabilite() - 15);
    }

    @Override
    public void acilDurumSogutmasi() {
        setStabilite(getStabilite() + 50);
        System.out.println(id + " soğutuldu.");
    }
}

class AntiMadde extends KuantumNesnesi implements IKritik {
    public AntiMadde(String id) {
        super(id, 10);
    }

    @Override
    public void analizEt() {
        setStabilite(getStabilite() - 25);
        System.out.println("Evrenin dokusu titriyor...");
    }

    @Override
    public void acilDurumSogutmasi() {
        setStabilite(getStabilite() + 50);
        System.out.println(id + " soğutuldu.");
    }
}