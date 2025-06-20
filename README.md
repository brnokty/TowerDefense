# TowerDefense
Tower Defense
# Baran Oktay – Tower Defence Game Protatype


 
Oyunda genel kullandığım doyaları [Game] klasörü altında topladım,Böylelikle Asset dosyalarıyla karışıklık olmuyor.
 
 
Scriptlerimi ilgili olduğu kısımlarla alakalı klasörleyerek düzenli bir yapı elde ettim.
 
Oyunu resimde görünen Scene dosyasından oynuyoruz.
# Framework
Ben oyunu geliştirirken modülerlik, kodun daha anlaşılır olması ve yeniden kullanılabilmesinin kolay olması açısından Zenject Frameworkünü kullandım. 

# UI 
Oyunun inGamePanelinde ;
•	Kaçıncı Wavede olduğumuzu,
•	Kaç coinimiz olduğunu,
•	Sağ altta konacak tower için seçim butonları,
•	Seçilenin görünmesi için selected tower image ı
Bulunmakta.
 
Tower Satın alabildiğimiz 5 snin anlaşılabilir olması için ekranın orta üst kımında DoTweenle hareketli bir yazı koydum bu yazıyı gördüğümüzde ve TowerSelectPanelimiz interactable olduğunda anlıyoruzki tower satın alabiliriz.
 
Win Panelimiz.
 
Lose Panelimiz.

 
Tower eklediğimizde towerın rangeini belli eden sprite ımız ve üstünde health barımız var.
 
Enemylerimizin üstünde can barı bulunmakta.

# Scriptable System
 
ScriptableObjectlerimizResources klasörünün altında bulunuyor.

# Tower System
 
4 çeşit towerımız var bunları scriptableobject olarak oluşturuyoruz. 
 
Oluşturduğumuz towerlar burada buluyor.
 
 
Towerlar için oluşturduğumuz scriptable objectlerimizi GameInstallerın içinde Tower Data Listesine ekliyoruz böylelikle otomatik olarak UIda eklediğimiz towerların UIları oluşuyor extra bir şey yapmıyoruz.
  
Tower Data nın içinde Tower için gerekli olan parametreler bulunmakta;
Towerın çoğu özelliğini buradan kontrol edebiliyoruz(Range, Cost, Fire Rate, Damage gibi )

# Gun Tower
 
Bu tower düşmana saniyede 10 damage vuran projectile lar atıyor.
# Super Gun Tower
 
Bu towerda daha kısa range e saniyede 2 tane 10 damage vuran projectile lar atıyor.
# Healler Tower
 
Bu tower range i içinde bulunan canı azalan towerların 3 saniyede 10 canını yeniliyor. Ve alana giren enemyleride çok kısa yavaşlatıyor.
Slower Tower
 
Bu tower range i içindeki düşmanları yavaşlatmaya yarayan bir tower alana giren enemyler 3 sn için 4 te 1i hızına düşüyor.


# Enemy System
 
3 enemymiz bulunmakta şuan için.
 
Enemy Datadan Enemynin neredeyse tüm özelliklerini kontrol edebiliriz
# Runner
 
Bu enemy runner sadece base e ulaşmaya çalışıyor.
# Wizard
 
Bu enemymiz attacker bir enemy alanına giren towerlara büyü atarak hasar veriyor.
# Knight
 
Buda yine Runner bir karakter amacı base e ulaşmak ama canı daha yüksek ama hızı daha düşük.

# Wave System
 
Buraya waveleri ekleyerek wavelerin içeriğini düzenleyebiliyoruz.
 
Önce enemy type ı seçiyoruz ve alt kısımda enemy count kısmından kaç tane olmasını istediğimiz giriyoruz ve sırasıyla enemylerimiz oluşuyor.
 
GameInstaller a Wave scriptable objectlerimizi ekliyoruz ve oyun sırasıyla oynatıyor.


