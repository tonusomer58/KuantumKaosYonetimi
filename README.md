# Kuantum Kaos Yönetimi (Quantum Chaos Management)

Bu proje, "Omega Sektörü"ndeki Kuantum Veri Ambarı'nın simülasyonunu içeren bir Nesneye Dayalı Programlama (OOP) ödevidir.

## Proje Amacı

Evrenin en kararsız maddelerini dijital ortamda saklamak, analiz etmek ve patlamadan (Kuantum Çöküşü) gün sonunu getirmek.

## Kullanılan Teknolojiler ve Diller

Proje 4 farklı programlama dilinde, aynı mimari prensipler kullanılarak geliştirilmiştir:

- **C#** (.NET Core)
- **Java** (JDK)
- **Python** (3.2)
- **JavaScript** (Node.js)

## Mimari Yapı (OOP Prensipleri)

Proje şu prensipleri eksiksiz uygular:

1.  **Abstraction (Soyutlama):** `KuantumNesnesi` soyut sınıfı.
2.  **Encapsulation (Kapsülleme):** Stabilite değerinin 0-100 arasında tutulması.
3.  **Inheritance (Kalıtım):** `VeriPaketi`, `KaranlikMadde`, `AntiMadde` sınıfları.
4.  **Polymorphism (Çok Biçimlilik):** Ortak `AnalizEt()` ve `DurumBilgisi()` metotları.
5.  **Interface Segregation:** Sadece tehlikeli maddeler için `IKritik` arayüzü.
6.  **Exception Handling:** Özelleştirilmiş `KuantumCokusuException` hata yönetimi.
