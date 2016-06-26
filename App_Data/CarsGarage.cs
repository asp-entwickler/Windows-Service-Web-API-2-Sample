using Newtonsoft.Json;
using System.Collections.Generic;

namespace Windows_Service_Web_API_2_Sample.App_Data
{
    class CarsGarage
    {
        public List<Car> Cars { get; set; }

        private string jsonCars = @"[{
                        id : 1,
                        manufacturer: 'Porsche',
                        model: '911',
                        price: 135000,
                        img: '2004_Porsche_911_Carrera_type_997.jpg'
                    },{
                        id : 2,
                        manufacturer: 'Nissan',
                        model: 'GT-R',
                        price: 80000,
                        wiki:'http://en.wikipedia.org/wiki/Nissan_Gt-r',
                        img: '250px-Nissan_GT-R.jpg'
                    },{
                        id : 3,
                        manufacturer: 'BMW',
                        model: 'M3',
                        price: 60500,
                        wiki:'http://en.wikipedia.org/wiki/Bmw_m3',
                        img: '250px-BMW_M3_E92.jpg'
                    },{
                        id : 4,
                        manufacturer: 'Audi',
                        model: 'S5',
                        price: 53000,
                        wiki:'http://en.wikipedia.org/wiki/Audi_S5#Audi_S5',
                        img: '250px-Audi_S5.jpg'
                    },{
                        id : 5,
                        manufacturer: 'Audi',
                        model: 'TT',
                        price: 40000,
                        wiki:'http://en.wikipedia.org/wiki/Audi_TT',
                        img: '250px-2007_Audi_TT_Coupe.jpg'
                    }] ";

        public CarsGarage() {
            Cars = JsonConvert.DeserializeObject<List<Car>>(jsonCars);
        }

    }
}
