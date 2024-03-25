using Olx.DataAccess.Contexts;
using Olx.Domain.Entities;

namespace Olx.DataAccess.SeedData;

public class SeedData
{
    private readonly AppDbContext _context;

    public SeedData(AppDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        if (!_context.Category.Any())
        {
            // Seed categories
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Bolalar dunyosi" },
                new Category { Id = 2, Name = "Ko'chmas mulk" },
                new Category { Id = 3, Name = " Transport" },
                new Category { Id = 4, Name = "Ish" },
                new Category { Id = 5, Name = "Hayvonlar" },
                new Category { Id = 6, Name = "Uy va Bog'" },
                new Category { Id = 7, Name = "Elektron jihozlar" },
                new Category { Id = 8, Name = "Xizmatlar" },
                new Category { Id = 9, Name = "Moda va stil" },
                new Category { Id = 10, Name = "Xobbi, dam olish sport" },
                new Category {Name = "Bolalar kiyimi", ParentId = 1},
                new Category {Name = "Bolalar mebeli", ParentId = 1},
                new Category {Name = "Maktab o'quvchilari uchun mahsulotlar ", ParentId = 1},
                new Category {Name = "Bolalar oyoq kiyimi", ParentId = 1},
                new Category {Name = "O'yinchoqlar", ParentId = 1},
                new Category {Name = "Boshqa bolalar mahsulotlari", ParentId = 1},
                new Category {Name = "Bolalar kolyaskalari", ParentId = 1},
                new Category {Name = "Bolalar transporti", ParentId = 1},
                new Category {Name = "Bolalar avtoo'rindiqlari", ParentId = 1},
                new Category {Name = "Oziqlantirish", ParentId = 1},
                new Category {Name = "Sutkalik ijarasi", ParentId = 2},
                new Category {Name = "Garajlar", ParentId = 2},
                new Category {Name = "Kvartiralar", ParentId = 2},
                new Category {Name = "Tijorat binolari", ParentId = 2},
                new Category {Name = "Xususiy uylar", ParentId = 2},
                new Category {Name = "Yer uchastkasi", ParentId = 2},
                new Category {Name = "Yengil avtomashinalar", ParentId = 3},
                new Category {Name = "Moto ehtiyot qismlari va aksessuarlar", ParentId = 3},
                new Category {Name = "Tirkamalar", ParentId = 3},
                new Category {Name = "Suv transporti", ParentId = 3},
                new Category {Name = "Avto ehtiyot qismlari va aksessuarlar", ParentId = 3},
                new Category {Name = "Boshqa transport", ParentId = 3},
                new Category {Name = "Maxsus ehtiyot qismlari", ParentId = 3},
                new Category {Name = "Boshqa ehtiyot qismlari", ParentId = 3},
                new Category {Name = "Shinalar diskalar va g'ildiraklar", ParentId = 3},
                new Category {Name = "Avtobuslar", ParentId = 3},
                new Category {Name = "Qishloq xo'jalik texnikasi", ParentId = 3},
                new Category {Name = "Moto", ParentId = 3},
                new Category {Name = "Yuk mashinalari", ParentId = 3},
                new Category {Name = "Maxsus texnika uchun qismlar", ParentId = 3},
                new Category {Name = "Chakana savdo-sotuvlar", ParentId = 4},
                new Category {Name = "Yurisprudenitsiya va buhgalteriya", ParentId = 4},
                new Category {Name = "Turizm - dam olish - oyinlar", ParentId = 4},
                new Category {Name = "It - Telekom - kompyuterlar", ParentId = 4},
                new Category {Name = "Kotibiyat - Axo", ParentId = 4},
                new Category {Name = "Qisman bandlik", ParentId = 4},
                new Category {Name = "Transport logistikasi", ParentId = 4},
                new Category {Name = "Qo'riqlash - xavfsizlik", ParentId = 4},
                new Category {Name = "Talim", ParentId = 4},
                new Category {Name = "Ko'chmas mulk", ParentId = 4},
                new Category {Name = "Karyerani boshlash talabalar", ParentId = 4},
                new Category {Name = "Qurilish", ParentId = 4},
                new Category {Name = "Uy xodimlari", ParentId = 4},
                new Category {Name = "Madaniyat - sanat", ParentId = 4},
                new Category {Name = "Marketing - reklama - sanat", ParentId = 4},
                new Category {Name = "Xizmat korsatish", ParentId = 4},
                new Category {Name = "Barlar - restoranlar", ParentId = 4},
                new Category {Name = "Go'zallik - fitnes - sport", ParentId = 4},
                new Category {Name = "Tibbiyot - farmatsiya", ParentId = 4},
                new Category {Name = "Ishlab chiqarish - energetika", ParentId = 4},
                new Category {Name = "Boshqa mashg'ulotlar", ParentId = 4},
                new Category {Name = "Itlar", ParentId = 5},
                new Category {Name = "Kemiruvchilar", ParentId = 5},
                new Category {Name = "Topilmalar idorasi", ParentId = 5},
                new Category {Name = "Mushuklar", ParentId = 5},
                new Category {Name = "Qishloq xo'jalik hayvonlari", ParentId = 5},
                new Category {Name = "Boshqa hayvonlar", ParentId = 5},
                new Category {Name = "Akvarium baliqlari", ParentId = 5},
                new Category {Name = "Hayvonlar uchun mahsulotlar", ParentId = 5},
                new Category {Name = "Tekinga hayvonlar", ParentId = 5},
                new Category {Name = "Qushlar", ParentId = 5}, 
                new Category {Name = "To'qish", ParentId = 5}, 
                new Category {Name = "Mebel", ParentId = 6}, 
                new Category {Name = "Jihozlar", ParentId = 6}, 
                new Category {Name = "Xo'jalik jihozlari, maishiy kimyo", ParentId = 6}, 
                new Category {Name = "Bog' - tomorqa", ParentId = 6}, 
                new Category {Name = "Xona o'simliklari", ParentId = 6}, 
                new Category {Name = "Kanstovarlar - chiqim materialllari", ParentId = 6}, 
                new Category {Name = "Interyer jihozlari", ParentId = 6}, 
                new Category {Name = "Idish - tovoq, oshxona anjomlari", ParentId = 6}, 
                new Category {Name = "Oziq - ovqat, Ichimliklar", ParentId = 6}, 
                new Category {Name = "Qurilish, Tamirlash uchun tovarlar", ParentId = 6}, 
                new Category {Name = "Bog' anjomlari", ParentId = 6}, 
                new Category {Name = "Uy uchun boshqa mahsulotlar", ParentId = 6}, 
                new Category {Name = "Telefonlar", ParentId = 7}, 
                new Category {Name = "Audotexnika", ParentId = 7}, 
                new Category {Name = "Iqlim qurilmalari", ParentId = 7}, 
                new Category {Name = "Kompyuterlar", ParentId = 7}, 
                new Category {Name = "Oyin va oyin anjomlari", ParentId = 7}, 
                new Category {Name = "Yakka tartibdagi parvarish", ParentId = 7}, 
                new Category {Name = "Foto video", ParentId = 7}, 
                new Category {Name = "Uy uchun texnika", ParentId = 7}, 
                new Category {Name = "Aksessuarlar va komplekt jihozlar", ParentId = 7}, 
                new Category {Name = "Tv Videotexnika", ParentId = 7}, 
                new Category {Name = "Oshxona uchun texnika", ParentId = 7}, 
                new Category {Name = "Boshqa elektronika", ParentId = 7}, 
                new Category {Name = "Qurilish tamirlash xona tozalash", ParentId = 8}, 
                new Category {Name = "Enagalar kasalga qarovchilar", ParentId = 8}, 
                new Category {Name = "Enagalar kasalga qarovchilar", ParentId = 8}, 
                new Category {Name = "Talim sport", ParentId = 8}, 
                new Category {Name = "Turizm", ParentId = 8}, 
                new Category {Name = "Yuridik xizmatlar", ParentId = 8}, 
                new Category {Name = "Moliya xizmatlari", ParentId = 8}, 
                new Category {Name = "Xomashyo materiallari", ParentId = 8}, 
                new Category {Name = "Hayvonlar uchun xizmatlar", ParentId = 8}, 
                new Category {Name = "Tarjimonlar xizmatlari matnlarini terish", ParentId = 8}, 
                new Category {Name = "Mahsulotlar prokati", ParentId = 8}, 
                new Category {Name = "Tashishlar transport ijarasi", ParentId = 8}, 
                new Category {Name = "Gozallik salomatlik", ParentId = 8}, 
                new Category {Name = "Biznesni sotish", ParentId = 8}, 
                new Category {Name = "Avto moto xizmatlar", ParentId = 8}, 
                new Category {Name = "Boshqa xizmatlar", ParentId = 8}, 
                new Category {Name = "Reklama poligrafiya marketing internet", ParentId = 8}, 
                new Category {Name = "Qurilmalar", ParentId = 8}, 
                new Category {Name = "Oyinlar sanat foto video", ParentId = 8}, 
                new Category {Name = "Texnikaga xizmat korsatish tamirlash", ParentId = 8}, 
                new Category {Name = "Kiyim - kechak", ParentId = 9}, 
                new Category {Name = "Aksessuarlar", ParentId = 9}, 
                new Category {Name = "Toy uchun", ParentId = 9}, 
                new Category {Name = "Sovgalar", ParentId = 9}, 
                new Category {Name = "Moda turli turmaklar", ParentId = 9}, 
                new Category {Name = "Gozallik salomatlik", ParentId = 9}, 
                new Category {Name = "Qol soatlari", ParentId = 9}, 
                new Category {Name = "Antikvar kolleksiyalar", ParentId = 10}, 
                new Category {Name = "Kitoblar jurnallar", ParentId = 10}, 
                new Category {Name = "Musiqa anjomlari", ParentId = 10}, 
                new Category {Name = "Cd dvd plastinkalar kassetalar", ParentId = 10}, 
                new Category {Name = "Boshqalar", ParentId = 10}, 
                new Category {Name = "Chiptalar", ParentId = 10}, 
                new Category {Name = "Sport - dam olish", ParentId = 10}, 
                // Add more categories as needed
            };

            _context.Category.AddRange(categories);
            _context.SaveChanges();
        }

        if (!_context.Property.Any())
        {
            // Seed properties
            var properties = new List<Property>
            {
                new Property { Id = 1, Name = "Color", CategoryId = 1 },
                new Property { Id = 2, Name = "Size", CategoryId = 2 },
                new Property { Id = 3, Name = "Power Consumption", CategoryId = 3 },
                // Add more properties as needed
            };

            _context.Property.AddRange(properties);
            _context.SaveChanges();
        }

        if (!_context.PropertyValue.Any())
        {
            // Seed property values
            var propertyValues = new List<PropertyValue>
            {
                new PropertyValue { Id = 1, Value = "Black" },
                new PropertyValue { Id = 2, Value = "Medium" },
                new PropertyValue { Id = 3, Value = "Low" },
                // Add more property values as needed
            };

            _context.PropertyValue.AddRange(propertyValues);
            _context.SaveChanges();
        }

        if (!_context.User.Any())
        {

        }
    }
}