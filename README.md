# Uçan Kuşlar: Tilkinin İntikamı

Bu proje, Uçan Kuşlar: Tilkinin İntikamı adında bir oyun ödevi için oluşturulmuştur. Oyun, temel olarak tilki karakterini kontrol ederek uçan kuşları avlayarak tilkinin özelliklerini geliştirmek üzerine kurulu.

- [Tanıtım Videosu](https://www.youtube.com/watch?v=1WQkhv3Sln4)

## Özellikler

- Tilki karakteri 4 yöne de yüreyebiliyor ve ileri sol ve sağa koşabiliyor.
- Tilki karakteri çift zıplayabiliyor.
- Kuşlar can barına sahip tilki saldırdıkça canları azalıyor. Canları tükendiğinde ise ölüyorlar.
- Kuşlar rastgele yönlere hareket edip belli aralıklarla zıplıyorlar.
- Tilki öldürdüğü kuşları markette satarak özelliklerini geliştirebiliyor

## Karşılaşılan Zorluklar

- **Kuş Davranışları**: Kuşların rastgele hareket etmesi ve zıplaması konusunda ne hızlı hareket edecekleri ve ne sıklıkla zıplayacakları gibi parametrelerin ayarlanması gerekiyordu. Bunları deneyerek ve parametreleri değiştirerek çözdüm. Ayrıca kuşların ne sıklıkla oluşacağı da önemliydi. Bunun için de kuşların oluşma sürelerini değiştirerek çözdüm.

- **Kontroller ve Oynanış Mekanikleri**: Oyundaki tilki hareketini addForce ile sağlandığı için bu parametrelerin doğru ayarlanması önemliydi. Ayrıca tilkinin zıplama ve koşma gibi hareketlerini de doğru bir şekilde ayarlamak gerekiyordu. Bunları maksimum çıkabilecekleri değerlerle sınırlayarak ve parametreleri deneyerek çözdüm.

- **Grafik ve Animasyonlar**: Oyundaki tilki ve kuşların animasyonları akıcı bir deneyim için önemlidir. Animasyon geçişlerini ayarlarken bazen istediğim akıcılığı sağlayamadım. Daha sonra bu sorunu animasyonların geçiş sürelerini ve hasExit gibi parametreleri kullanarak çözdüm.
