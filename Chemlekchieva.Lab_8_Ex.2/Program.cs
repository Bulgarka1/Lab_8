using System.Text;
using System.Text.Encodings;
using System.Text.Encodings.Web;
using System.Text.Json;

                var path = "tabl.csv";
                
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                Encoding encoding = Encoding.GetEncoding(1251);


                var lines = File.ReadAllLines(path,encoding);
                var cakes = new Cake[lines.Length - 1];
                for (var i = 1; i < lines.Length; i++)
                {
                    var splits = lines[i].Split(';');
                    var cake = new Cake();
                    cake.Наименование = splits[0];
                    cake.Себестоимость = Convert.ToInt32(splits[1]);
                    cake.Стоимость = Convert.ToInt32(splits[2]);
                    cake.Время=Convert.ToInt32(splits[3]);
                    cakes[i - 1] = cake;
                    Console.WriteLine(cake);
                }

                var result = "result.csv";
                using (StreamWriter streamWriter = new StreamWriter(result, false, encoding))
                {
                    streamWriter.WriteLine($"Name;Cost;Price;Time;Profit");
                    for (int i = 0; i < cakes.Length; i++)
                    {
                        streamWriter.WriteLine(cakes[i].ToExcel());
                    }
                }

                var jsonOptions = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Default
                };

                var json = JsonSerializer.Serialize(cakes, jsonOptions);
                File.WriteAllText("result.json", json);

                var stringJson = File.ReadAllText("result.json");
                var array = JsonSerializer.Deserialize<Cake[]>(stringJson);

        public class Cake
        {
            public string Наименование { get; set; }
            public int Себестоимость { get; set; }
            public int Стоимость { get; set; }
            public int Время { get; set; }
            public double Накрутка { get => Стоимость - Себестоимость; }
            public override string ToString()
            {
                return $"Наименование:{Наименование} Себестоимость: {Себестоимость} Стоимость: {Стоимость} Время: {Время} Накрутка:{Накрутка}";
            }

            public string ToExcel()
            {
                return $"{Наименование};{Себестоимость};{Стоимость};{Время};{Накрутка}";
            }
        }



