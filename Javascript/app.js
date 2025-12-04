//cd Javascript
//node app.js

const readline = require("readline");

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

class KuantumCokusuException extends Error {
  constructor(id) {
    super(`SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... Patlayan Nesne ID: ${id}`);
  }
}

class KuantumNesnesi {
  constructor(id, tehlike) {
    if (this.constructor === KuantumNesnesi) {
      throw new Error("Abstract class cannot be instantiated.");
    }
    this.id = id;
    // GÜNCELLEME: Stabilite 0-100 arası random
    this._stabilite = Math.random() * 100;
    this.tehlikeSeviyesi = tehlike;
  }

  get stabilite() {
    return this._stabilite;
  }

  set stabilite(value) {
    if (value > 100) this._stabilite = 100;
    else this._stabilite = value;

    if (this._stabilite <= 0) {
      throw new KuantumCokusuException(this.id);
    }
  }

  analizEt() {
    throw new Error("Abstract method!");
  }

  durumBilgisi() {
    return `[${this.constructor.name}] ID: ${
      this.id
    } - Stabilite: %${this.stabilite.toFixed(2)} - Tehlike: ${
      this.tehlikeSeviyesi
    }`;
  }
}

class VeriPaketi extends KuantumNesnesi {
  constructor(id) {
    super(id, 1);
  }

  analizEt() {
    this.stabilite -= 5;
    console.log("Veri içeriği okundu.");
  }
}

class KaranlikMadde extends KuantumNesnesi {
  constructor(id) {
    super(id, 5);
  }

  analizEt() {
    this.stabilite -= 15;
  }

  acilDurumSogutmasi() {
    this.stabilite += 50;
    console.log(`${this.id} soğutuldu.`);
  }
}

class AntiMadde extends KuantumNesnesi {
  constructor(id) {
    super(id, 10);
  }

  analizEt() {
    this.stabilite -= 25;
    console.log("Evrenin dokusu titriyor...");
  }

  acilDurumSogutmasi() {
    this.stabilite += 50;
    console.log(`${this.id} soğutuldu (Kritik işlem).`);
  }
}

// MAIN LOOP
const envanter = [];
let sayac = 1;

function menu() {
  console.log("\n--- KONTROL PANELİ ---");
  console.log("1. Yeni Nesne Ekle");
  console.log("2. Envanteri Listele");
  console.log("3. Analiz Et");
  console.log("4. Soğutma Yap");
  console.log("5. Çıkış");

  rl.question("Seçim: ", (secim) => {
    try {
      if (secim === "1") {
        const tur = Math.floor(Math.random() * 3);
        const yeniId = `NESNE-${sayac++}`;
        if (tur === 0) envanter.push(new VeriPaketi(yeniId));
        else if (tur === 1) envanter.push(new KaranlikMadde(yeniId));
        else envanter.push(new AntiMadde(yeniId));
        console.log(
          `${
            envanter[envanter.length - 1].constructor.name
          } (${yeniId}) eklendi.`
        );
        menu();
      } else if (secim === "2") {
        envanter.forEach((n) => console.log(n.durumBilgisi()));
        menu();
      } else if (secim === "3") {
        rl.question("ID: ", (id) => {
          try {
            const n = envanter.find((x) => x.id === id);
            if (n) n.analizEt();
            else console.log("Bulunamadı.");
            menu();
          } catch (e) {
            handleError(e);
          }
        });
      } else if (secim === "4") {
        rl.question("ID: ", (id) => {
          const n = envanter.find((x) => x.id === id);
          if (n) {
            if (typeof n.acilDurumSogutmasi === "function") {
              n.acilDurumSogutmasi();
            } else {
              console.log("Bu nesne soğutulamaz!");
            }
          } else {
            console.log("Bulunamadı.");
          }
          menu();
        });
      } else if (secim === "5") {
        rl.close();
      } else {
        menu();
      }
    } catch (e) {
      handleError(e);
    }
  });
}

function handleError(e) {
  if (e instanceof KuantumCokusuException) {
    console.log("\n*********************************");
    console.log(e.message.toUpperCase());
    console.log("*********************************");
    process.exit(1);
  } else {
    console.log("Hata:", e.message);
    menu();
  }
}

console.log("KUANTUM AMBARI JS SÜRÜMÜ");
menu();
