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
                new Category { Id = 11, Name = "Bolalar kiyimi", ParentId = 1},
                new Category { Id = 12, Name = "Bolalar mebeli", ParentId = 1},
                new Category { Id = 13, Name = "Maktab o'quvchilari uchun mahsulotlar ", ParentId = 1},
                new Category { Id = 14, Name = "Bolalar oyoq kiyimi", ParentId = 1},
                new Category { Id = 15, Name = "O'yinchoqlar", ParentId = 1},
                new Category { Id = 16, Name = "Boshqa bolalar mahsulotlari", ParentId = 1},
                new Category { Id = 17, Name = "Bolalar kolyaskalari", ParentId = 1},
                new Category { Id = 18, Name = "Bolalar transporti", ParentId = 1},
                new Category { Id = 19, Name = "Bolalar avtoo'rindiqlari", ParentId = 1},
                new Category { Id = 20, Name = "Oziqlantirish", ParentId = 1},
                new Category { Id = 21, Name = "Sutkalik ijarasi", ParentId = 2},
                new Category { Id = 22, Name = "Garajlar", ParentId = 2},
                new Category { Id = 23, Name = "Kvartiralar", ParentId = 2},
                new Category { Id = 24, Name = "Tijorat binolari", ParentId = 2},
                new Category { Id = 25, Name = "Xususiy uylar", ParentId = 2},
                new Category { Id = 26, Name = "Yer uchastkasi", ParentId = 2},
                new Category { Id = 27, Name = "Yengil avtomashinalar", ParentId = 3},
                new Category { Id = 28, Name = "Moto ehtiyot qismlari va aksessuarlar", ParentId = 3},
                new Category { Id = 29, Name = "Tirkamalar", ParentId = 3},
                new Category { Id = 30, Name = "Suv transporti", ParentId = 3},
                new Category { Id = 31, Name = "Avto ehtiyot qismlari va aksessuarlar", ParentId = 3},
                new Category { Id = 32, Name = "Boshqa transport", ParentId = 3},
                new Category { Id = 33, Name = "Maxsus ehtiyot qismlari", ParentId = 3},
                new Category { Id = 34, Name = "Boshqa ehtiyot qismlari", ParentId = 3},
                new Category { Id = 35, Name = "Shinalar diskalar va g'ildiraklar", ParentId = 3},
                new Category { Id = 36, Name = "Avtobuslar", ParentId = 3},
                new Category { Id = 37, Name = "Qishloq xo'jalik texnikasi", ParentId = 3},
                new Category { Id = 38, Name = "Moto", ParentId = 3},
                new Category { Id = 39, Name = "Yuk mashinalari", ParentId = 3},
                new Category { Id = 40, Name = "Maxsus texnika uchun qismlar", ParentId = 3},
                new Category { Id = 41, Name = "Chakana savdo-sotuvlar", ParentId = 4},
                new Category { Id = 42, Name = "Yurisprudenitsiya va buhgalteriya", ParentId = 4},
                new Category { Id = 43, Name = "Turizm - dam olish - oyinlar", ParentId = 4},
                new Category { Id = 44, Name = "It - Telekom - kompyuterlar", ParentId = 4},
                new Category { Id = 45, Name = "Kotibiyat - Axo", ParentId = 4},
                new Category { Id = 46, Name = "Qisman bandlik", ParentId = 4},
                new Category { Id = 47, Name = "Transport logistikasi", ParentId = 4},
                new Category { Id = 48, Name = "Qo'riqlash - xavfsizlik", ParentId = 4},
                new Category { Id = 49, Name = "Talim", ParentId = 4},
                new Category { Id = 50, Name = "Ko'chmas mulk", ParentId = 4},
                new Category { Id = 51, Name = "Karyerani boshlash talabalar", ParentId = 4},
                new Category { Id = 52, Name = "Qurilish", ParentId = 4},
                new Category { Id = 53, Name = "Uy xodimlari", ParentId = 4},
                new Category { Id = 54, Name = "Madaniyat - sanat", ParentId = 4},
                new Category { Id = 55, Name = "Marketing - reklama - sanat", ParentId = 4},
                new Category { Id = 56, Name = "Xizmat korsatish", ParentId = 4},
                new Category { Id = 57, Name = "Barlar - restoranlar", ParentId = 4},
                new Category { Id = 58, Name = "Go'zallik - fitnes - sport", ParentId = 4},
                new Category { Id = 59, Name = "Tibbiyot - farmatsiya", ParentId = 4},
                new Category { Id = 60, Name = "Ishlab chiqarish - energetika", ParentId = 4},
                new Category { Id = 61, Name = "Boshqa mashg'ulotlar", ParentId = 4},
                new Category { Id = 62, Name = "Itlar", ParentId = 5},
                new Category { Id = 63, Name = "Kemiruvchilar", ParentId = 5},
                new Category { Id = 64, Name = "Topilmalar idorasi", ParentId = 5},
                new Category { Id = 65, Name = "Mushuklar", ParentId = 5},
                new Category { Id = 66, Name = "Qishloq xo'jalik hayvonlari", ParentId = 5},
                new Category { Id = 67, Name = "Boshqa hayvonlar", ParentId = 5},
                new Category { Id = 68, Name = "Akvarium baliqlari", ParentId = 5},
                new Category { Id = 69, Name = "Hayvonlar uchun mahsulotlar", ParentId = 5},
                new Category { Id = 70, Name = "Tekinga hayvonlar", ParentId = 5},
                new Category { Id = 71, Name = "Qushlar", ParentId = 5}, 
                new Category { Id = 72, Name = "To'qish", ParentId = 5}, 
                new Category { Id = 73, Name = "Mebel", ParentId = 6}, 
                new Category { Id = 74, Name = "Jihozlar", ParentId = 6}, 
                new Category { Id = 75, Name = "Xo'jalik jihozlari, maishiy kimyo", ParentId = 6}, 
                new Category { Id = 76, Name = "Bog' - tomorqa", ParentId = 6}, 
                new Category { Id = 77, Name = "Xona o'simliklari", ParentId = 6}, 
                new Category { Id = 78, Name = "Kanstovarlar - chiqim materialllari", ParentId = 6}, 
                new Category { Id = 79, Name = "Interyer jihozlari", ParentId = 6}, 
                new Category { Id = 80, Name = "Idish - tovoq, oshxona anjomlari", ParentId = 6}, 
                new Category { Id = 81, Name = "Oziq - ovqat, Ichimliklar", ParentId = 6}, 
                new Category { Id = 82, Name = "Qurilish, Tamirlash uchun tovarlar", ParentId = 6}, 
                new Category { Id = 83, Name = "Bog' anjomlari", ParentId = 6}, 
                new Category { Id = 84, Name = "Uy uchun boshqa mahsulotlar", ParentId = 6}, 
                new Category { Id = 85, Name = "Telefonlar", ParentId = 7}, 
                new Category { Id = 86, Name = "Audotexnika", ParentId = 7}, 
                new Category { Id = 87, Name = "Iqlim qurilmalari", ParentId = 7}, 
                new Category { Id = 88, Name = "Kompyuterlar", ParentId = 7}, 
                new Category { Id = 89, Name = "Oyin va oyin anjomlari", ParentId = 7}, 
                new Category { Id = 90, Name = "Yakka tartibdagi parvarish", ParentId = 7}, 
                new Category { Id = 91, Name = "Foto video", ParentId = 7}, 
                new Category { Id = 92, Name = "Uy uchun texnika", ParentId = 7}, 
                new Category { Id = 93, Name = "Aksessuarlar va komplekt jihozlar", ParentId = 7}, 
                new Category { Id = 94, Name = "Tv Videotexnika", ParentId = 7}, 
                new Category { Id = 95, Name = "Oshxona uchun texnika", ParentId = 7}, 
                new Category { Id = 96, Name = "Boshqa elektronika", ParentId = 7}, 
                new Category { Id = 97, Name = "Qurilish tamirlash xona tozalash", ParentId = 8}, 
                new Category { Id = 98, Name = "Enagalar kasalga qarovchilar", ParentId = 8}, 
                new Category { Id = 99, Name = "Enagalar kasalga qarovchilar", ParentId = 8}, 
                new Category { Id = 100, Name = "Talim sport", ParentId = 8}, 
                new Category { Id = 101, Name = "Turizm", ParentId = 8}, 
                new Category { Id = 102, Name = "Yuridik xizmatlar", ParentId = 8}, 
                new Category { Id = 103, Name = "Moliya xizmatlari", ParentId = 8}, 
                new Category { Id = 104, Name = "Xomashyo materiallari", ParentId = 8}, 
                new Category { Id = 105, Name = "Hayvonlar uchun xizmatlar", ParentId = 8}, 
                new Category { Id = 106, Name = "Tarjimonlar xizmatlari matnlarini terish", ParentId = 8}, 
                new Category { Id = 107, Name = "Mahsulotlar prokati", ParentId = 8}, 
                new Category { Id = 108, Name = "Tashishlar transport ijarasi", ParentId = 8}, 
                new Category { Id = 109, Name = "Gozallik salomatlik", ParentId = 8}, 
                new Category { Id = 110, Name = "Biznesni sotish", ParentId = 8}, 
                new Category { Id = 111, Name = "Avto moto xizmatlar", ParentId = 8}, 
                new Category { Id = 112, Name = "Boshqa xizmatlar", ParentId = 8}, 
                new Category { Id = 113, Name = "Reklama poligrafiya marketing internet", ParentId = 8}, 
                new Category { Id = 114, Name = "Qurilmalar", ParentId = 8}, 
                new Category { Id = 115, Name = "Oyinlar sanat foto video", ParentId = 8}, 
                new Category { Id = 116, Name = "Texnikaga xizmat korsatish tamirlash", ParentId = 8}, 
                new Category { Id = 117, Name = "Kiyim - kechak", ParentId = 9}, 
                new Category { Id = 118, Name = "Aksessuarlar", ParentId = 9}, 
                new Category { Id = 119, Name = "Toy uchun", ParentId = 9}, 
                new Category { Id = 120, Name = "Sovgalar", ParentId = 9}, 
                new Category { Id = 121, Name = "Moda turli turmaklar", ParentId = 9}, 
                new Category { Id = 122, Name = "Gozallik salomatlik", ParentId = 9}, 
                new Category { Id = 123, Name = "Qol soatlari", ParentId = 9}, 
                new Category { Id = 124, Name = "Antikvar kolleksiyalar", ParentId = 10}, 
                new Category { Id = 125, Name = "Kitoblar jurnallar", ParentId = 10}, 
                new Category { Id = 126, Name = "Musiqa anjomlari", ParentId = 10}, 
                new Category { Id = 127, Name = "Cd dvd plastinkalar kassetalar", ParentId = 10}, 
                new Category { Id = 128, Name = "Boshqalar", ParentId = 10}, 
                new Category { Id = 129, Name = "Chiptalar", ParentId = 10}, 
                new Category { Id = 130, Name = "Sport - dam olish", ParentId = 10}, 

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
                new Property { Id = 4, Name = "Ichki toifa",CategoryId = 11 },
                new Property { Id = 5, Name = "Holati", CategoryId = 11},
                new Property { Id = 6, Name = "Ish turi", CategoryId = 4},
                new Property { Id = 7, Name = "Bandlik turi", CategoryId = 4},
                new Property { Id = 8, Name = "Ish haqi", CategoryId= 4},
                new Property { Id = 9, Name = "Masofadan ishlash", CategoryId = 4},
                new Property { Id = 10, Name = "Onlayn ishga joylash", CategoryId = 4},
                new Property { Id = 11, Name = "Holati", CategoryId = 6},
                new Property { Id = 12, Name = "Holati", CategoryId = 9},
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
                new PropertyValue { Id = 4,Value = "Akita"},
                new PropertyValue { Id = 5,Value = "Alyaska malamuti"},
                new PropertyValue { Id = 6,Value = "Amerika buldogi"},
                new PropertyValue { Id = 7,Value = "Ingliz buldogi"},
                new PropertyValue { Id = 8,Value = "Baset"},
                new PropertyValue { Id = 9,Value = "Belgiya opchakasi"},
                new PropertyValue { Id = 10,Value = "Amerika kerli"},
                new PropertyValue { Id = 11,Value = "Bengal"},
                new PropertyValue { Id = 12,Value = "Sibir"},
                new PropertyValue { Id = 13,Value = "Turk angorasi"},
                new PropertyValue { Id = 14,Value = "Yapon bobteyli"},
                new PropertyValue { Id = 15,Value = "Siam"},

                
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