from abc import ABC, abstractmethod
import random

# Özel Hata Sınıfı
class KuantumCokusuException(Exception):
    pass

# Arayüz Simülasyonu
class IKritik(ABC):
    @abstractmethod
    def acil_durum_sogutmasi(self):
        pass

# Soyut Sınıf
class KuantumNesnesi(ABC):
    def __init__(self, nesne_id, tehlike):
        self.id = nesne_id
        # GÜNCELLEME: Stabilite 0-100 arası random float
        self._stabilite = random.uniform(0, 100)
        self.tehlike_seviyesi = tehlike

    # Encapsulation
    @property
    def stabilite(self):
        return self._stabilite

    @stabilite.setter
    def stabilite(self, deger):
        if deger > 100:
            self._stabilite = 100
        else:
            self._stabilite = deger
        
        # Hata Fırlatma
        if self._stabilite <= 0:
            raise KuantumCokusuException(f"SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... Patlayan Nesne ID: {self.id}")

    @abstractmethod
    def analiz_et(self):
        pass

   
def durum_bilgisi(self):
    return f"[{type(self).__name__}] ID: {self.id} - Stabilite: %{self.stabilite:.2f} - Tehlike: {self.tehlike_seviyesi}"

# Alt Sınıflar
class VeriPaketi(KuantumNesnesi):
    def __init__(self, nesne_id):
        super().__init__(nesne_id, 1)

    def analiz_et(self):
        self.stabilite -= 5
        print("Veri içeriği okundu.")

class KaranlikMadde(KuantumNesnesi, IKritik):
    def __init__(self, nesne_id):
        super().__init__(nesne_id, 5)

    def analiz_et(self):
        self.stabilite -= 15

    def acil_durum_sogutmasi(self):
        self.stabilite += 50
        print(f"{self.id} soğutuldu.")

class AntiMadde(KuantumNesnesi, IKritik):
    def __init__(self, nesne_id):
        super().__init__(nesne_id, 10)

    def analiz_et(self):
        self.stabilite -= 25
        print("Evrenin dokusu titriyor...")

    def acil_durum_sogutmasi(self):
        self.stabilite += 50
        print(f"{self.id} soğutuldu (Kritik işlem).")

# MAIN LOOP
def main():
    envanter = []
    sayac = 1
    
    print("KUANTUM AMBARI PYTHON SÜRÜMÜ")
    
    while True:
        try:
            print("\n--- KONTROL PANELİ ---")
            print("1. Yeni Nesne Ekle")
            print("2. Envanteri Listele")
            print("3. Analiz Et")
            print("4. Soğutma Yap")
            print("5. Çıkış")
            secim = input("Seçim: ")

            if secim == "1":
                tur = random.randint(1, 3)
                yeni_id = f"NESNE-{sayac}"
                sayac += 1
                if tur == 1: envanter.append(VeriPaketi(yeni_id))
                elif tur == 2: envanter.append(KaranlikMadde(yeni_id))
                else: envanter.append(AntiMadde(yeni_id))
                print(f"{type(envanter[-1]).__name__} ({yeni_id}) eklendi.")
            
            elif secim == "2":
                for nesne in envanter:
                    print(nesne.durum_bilgisi())
            
            elif secim == "3":
                girilen_id = input("ID giriniz: ")
                bulunan = next((x for x in envanter if x.id == girilen_id), None)
                if bulunan: found = bulunan.analiz_et()
                else: print("Bulunamadı.")

            elif secim == "4":
                girilen_id = input("ID giriniz: ")
                bulunan = next((x for x in envanter if x.id == girilen_id), None)
                if bulunan:
                    if isinstance(bulunan, IKritik):
                        bulunan.acil_durum_sogutmasi()
                    else:
                        print("Bu nesne soğutulamaz!")
                else:
                    print("Bulunamadı.");

            elif secim == "5": break

        except KuantumCokusuException as e:
            print("\n*********************************")
            print(str(e).upper())
            print("*********************************")
            break
        except Exception as e:
            print(f"Hata: {e}")

if __name__ == "__main__":
    main()