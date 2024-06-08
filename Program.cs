using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace TimeZoneApp
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string cityName = "Brasília";  // Substitua pelo nome da cidade desejada
            dynamic currentTime = await GetCurrentTime(cityName);
            Console.WriteLine($"A hora atual em {cityName} é: {currentTime}");
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(currentTime));
        }

        static async Task<dynamic> GetCurrentTime(string cityName)
            {
                // Dicionário de cidades
                var cityToTimezone = new Dictionary<string, string>
                {
                    { "São Paulo", "America/Sao_Paulo" },
                    // Afeganistão
                    { "Cabul", "Asia/Kabul" },
                    // Albânia
                    { "Tirana", "Europe/Tirane" },
                    // Argélia
                    { "Argel", "Africa/Algiers" },
                    // Andorra
                    { "Andorra la Vella", "Europe/Andorra" },
                    // Angola
                    { "Luanda", "Africa/Luanda" },
                    // Antígua e Barbuda
                    { "Saint John's", "America/Antigua" },
                    // Argentina
                    { "Buenos Aires", "America/Argentina/Buenos_Aires" },
                    // Armênia
                    { "Yerevan", "Asia/Yerevan" },
                    // Austrália
                    { "Sydney", "Australia/Sydney" },
                    // Áustria
                    { "Viena", "Europe/Vienna" },
                    // Azerbaijão
                    { "Baku", "Asia/Baku" },
                    // Bahamas
                    { "Nassau", "America/Nassau" },
                    // Bahrain
                    { "Manama", "Asia/Bahrain" },
                    // Bangladesh
                    { "Daca", "Asia/Dhaka" },
                    // Barbados
                    { "Bridgetown", "America/Barbados" },
                    // Bielorrússia
                    { "Minsk", "Europe/Minsk" },
                    // Bélgica
                    { "Bruxelas", "Europe/Brussels" },
                    // Belize
                    { "Belmopan", "America/Belize" },
                    // Benin
                    { "Porto-Novo", "Africa/Porto-Novo" },
                    // Butão
                    { "Thimphu", "Asia/Thimphu" },
                    // Bolívia
                    { "Sucre", "America/La_Paz" },
                    // Bósnia e Herzegovina
                    { "Sarajevo", "Europe/Sarajevo" },
                    // Botswana
                    { "Gaborone", "Africa/Gaborone" },
                    // Brasil
                    { "Brasília", "America/Sao_Paulo" },
                    // Brunei
                    { "Bandar Seri Begawan", "Asia/Brunei" },
                    // Bulgária
                    { "Sófia", "Europe/Sofia" },
                    // Burquina Faso
                    { "Ouagadougou", "Africa/Ouagadougou" },
                    // Burundi
                    { "Gitega", "Africa/Bujumbura" },
                    // Cabo Verde
                    { "Praia", "Atlantic/Cape_Verde" },
                    // Camboja
                    { "Phnom Penh", "Asia/Phnom_Penh" },
                    // Camarões
                    { "Yaoundé", "Africa/Douala" },
                    // Canadá
                    { "Toronto", "America/Toronto" },
                    // República Centro-Africana
                    { "Bangui", "Africa/Bangui" },
                    // Chade
                    { "N'Djamena", "Africa/Ndjamena" },
                    // Chile
                    { "Santiago", "America/Santiago" },
                    // China
                    { "Pequim", "Asia/Shanghai" },
                    // Colômbia
                    { "Bogotá", "America/Bogota" },
                    // Comores
                    { "Moroni", "Indian/Comoro" },
                    // Congo, República Democrática do
                    { "Kinshasa", "Africa/Kinshasa" },
                    // Congo, República do
                    { "Brazzaville", "Africa/Brazzaville" },
                    // Costa Rica
                    { "San José", "America/Costa_Rica" },
                    // Croácia
                    { "Zagreb", "Europe/Zagreb" },
                    // Cuba
                    { "Havana", "America/Havana" },
                    // Chipre
                    { "Nicósia", "Asia/Nicosia" },
                    // República Checa
                    { "Praga", "Europe/Prague" },
                    // Dinamarca
                    { "Copenhague", "Europe/Copenhagen" },
                    // Djibouti
                    { "Djibouti", "Africa/Djibouti" },
                    // Dominica
                    { "Roseau", "America/Dominica" },
                    // República Dominicana
                    { "Santo Domingo", "America/Santo_Domingo" },
                    // Equador
                    { "Quito", "America/Guayaquil" },
                    // Egito
                    { "Cairo", "Africa/Cairo" },
                    // El Salvador
                    { "San Salvador", "America/El_Salvador" },
                    // Guiné Equatorial
                    { "Malabo", "Africa/Malabo" },
                    // Eritreia
                    { "Asmara", "Africa/Asmara" },
                     // Estônia      
                     { "Tallinn", "Europe/Tallinn" },
                    // Etiópia
                    { "Adis Abeba", "Africa/Addis_Ababa" },
                    // Fiji
                    { "Suva", "Pacific/Fiji" },
                    // Finlândia
                    { "Helsinque", "Europe/Helsinki" },
                    // França
                    { "Paris", "Europe/Paris" },
                    // Gabão
                    { "Libreville", "Africa/Libreville" },
                    // Gâmbia
                    { "Banjul", "Africa/Banjul" },
                    // Geórgia
                    { "Tbilisi", "Asia/Tbilisi" },
                    // Alemanha
                    { "Berlim", "Europe/Berlin" },
                    // Gana
                    { "Acra", "Africa/Accra" },
                    // Grécia
                    { "Atenas", "Europe/Athens" },
                    // Granada
                    { "Saint George's", "America/Grenada" },
                    // Guatemala
                    { "Cidade da Guatemala", "America/Guatemala" },
                    // Guiné
                    { "Conacri", "Africa/Conakry" },
                    // Guiné-Bissau
                    { "Bissau", "Africa/Bissau" },
                    // Guiana
                    { "Georgetown", "America/Guyana" },
                    // Haiti
                    { "Porto Príncipe", "America/Port-au-Prince" },
                    // Honduras
                    { "Tegucigalpa", "America/Tegucigalpa" },
                    // Hungria
                    { "Budapeste", "Europe/Budapest" },
                    // Islândia
                    { "Reykjavik", "Atlantic/Reykjavik" },
                    // Índia
                    { "Nova Deli", "Asia/Kolkata" },
                    // Indonésia
                    { "Jacarta", "Asia/Jakarta" },
                    // Irã
                    { "Teerã", "Asia/Tehran" },
                    // Iraque
                    { "Bagdá", "Asia/Baghdad" },
                    // Irlanda
                    { "Dublin", "Europe/Dublin" },
                    // Israel
                    { "Jerusalém", "Asia/Jerusalem" },
                    // Itália
                    { "Roma", "Europe/Rome" },
                    // Jamaica
                    { "Kingston", "America/Jamaica" },
                    // Japão
                    { "Tóquio", "Asia/Tokyo" },
                    // Jordânia
                    { "Amã", "Asia/Amman" },
                    // Cazaquistão
                    { "Nur-Sultan", "Asia/Almaty" },
                    // Quênia
                    { "Nairóbi", "Africa/Nairobi" },
                    // Quiribati
                    { "Tarawa", "Pacific/Tarawa" },
                    // Coreia do Norte
                    { "Pyongyang", "Asia/Pyongyang" },
                    // Coreia do Sul
                    { "Seul", "Asia/Seoul" },
                    // Kosovo
                    { "Pristina", "Europe/Belgrade" },
                    // Kuwait
                    { "Cidade do Kuwait", "Asia/Kuwait" },
                    // Quirguistão
                    { "Bishkek", "Asia/Bishkek" },
                    // Laos
                    { "Vientiane", "Asia/Vientiane" },
                    // Letônia
                    { "Riga", "Europe/Riga" },
                    // Líbano
                    { "Beirute", "Asia/Beirut" },
                    // Lesoto
                    { "Maseru", "Africa/Maseru" },
                    // Libéria
                    { "Monróvia", "Africa/Monrovia" },
                    // Líbia
                    { "Trípoli", "Africa/Tripoli" },
                    // Liechtenstein
                    { "Vaduz", "Europe/Vaduz" },
                    // Lituânia
                    { "Vilnius", "Europe/Vilnius" },
                    // Luxemburgo
                    { "Luxemburgo", "Europe/Luxembourg" },
                    // Madagascar
                    { "Antananarivo", "Indian/Antananarivo" },
                    // Malaui
                    { "Lilongwe", "Africa/Blantyre" },
                    // Malásia
                    { "Kuala Lumpur", "Asia/Kuala_Lumpur" },
                    // Maldivas
                    { "Malé", "Indian/Maldives" },
                    // Mali
                    { "Bamako", "Africa/Bamako" },
                    // Malta
                    { "Valletta", "Europe/Malta" },
                    // Ilhas Marshall
                    { "Majuro", "Pacific/Majuro" },
                    // Mauritânia
                    { "Nouakchott", "Africa/Nouakchott" },
                    // Maurício
                    { "Port Louis", "Indian/Mauritius" },
                    // México
                    { "Cidade do México", "America/Mexico_City" },
                    // Micronésia
                    { "Palikir", "Pacific/Pohnpei" },
                    // Moldávia
                    { "Chisinau", "Europe/Chisinau" },
                    // Mônaco
                    { "Mônaco-Ville", "Europe/Monaco" },
                    // Mongólia
                    { "Ulan Bator", "Asia/Ulaanbaatar" },
                    // Montenegro
                    { "Podgorica", "Europe/Podgorica" },
                    // Marrocos
                    { "Rabat", "Africa/Casablanca" },
                    // Moçambique
                    { "Maputo", "Africa/Maputo" },
                    // Mianmar (Birmânia)
                    { "Naypyidaw", "Asia/Yangon" },
                    // Namíbia
                    { "Windhoek", "Africa/Windhoek" },
                    // Nauru
                    { "Yaren", "Pacific/Nauru" },
                    // Nepal
                    { "Catmandu", "Asia/Kathmandu" },
                    // Holanda
                    { "Amsterdã", "Europe/Amsterdam" },
                    // Nova Zelândia
                    { "Wellington", "Pacific/Auckland" },
                    // Nicarágua
                    { "Manágua", "America/Managua" },
                    // Níger
                    { "Niamey", "Africa/Niamey" },
                    // Nigéria
                    { "Abuja", "Africa/Lagos" },
                    // Macedônia do Norte
                    { "Skopje", "Europe/Skopje" },
                    // Noruega
                    { "Oslo", "Europe/Oslo" },
                    // Omã
                    { "Mascate", "Asia/Muscat" },
                    // Paquistão
                    { "Islamabad", "Asia/Karachi" },
                    // Palau
                    { "Ngerulmud", "Pacific/Palau" },
                    // Palestina
                    { "Ramallah", "Asia/Hebron" },
                    // Panamá
                    { "Cidade do Panamá", "America/Panama" },
                    // Papua Nova Guiné
                    { "Port Moresby", "Pacific/Port_Moresby" },
                    // Paraguai
                    { "Assunção", "America/Asuncion" },
                    // Peru
                    { "Lima", "America/Lima" },
                    // Filipinas
                    { "Manila", "Asia/Manila" },
                    // Polônia
                    { "Varsóvia", "Europe/Warsaw" },
                    // Portugal
                    { "Lisboa", "Europe/Lisbon" },
                    // Catar
                    { "Doha", "Asia/Qatar" },
                    // Romênia
                    { "Bucareste", "Europe/Bucharest" },
                    // Rússia
                    { "Moscou", "Europe/Moscow" },
                    // Ruanda
                    { "Kigali", "Africa/Kigali" },
                    // São Cristóvão e Nevis
                    { "Basseterre", "America/St_Kitts" },
                    // Santa Lúcia
                    { "Castries", "America/St_Lucia" },
                    // São Vicente e Granadinas
                    { "Kingstown", "America/St_Vincent" },
                    // Samoa
                    { "Apia", "Pacific/Apia" },
                    // San Marino
                    { "San Marino", "Europe/San_Marino" },
                    // São Tomé e Príncipe
                    { "São Tomé", "Africa/Sao_Tome" },
                    // Arábia Saudita
                    { "Riade", "Asia/Riyadh" },
                    // Senegal
                    { "Dacar", "Africa/Dakar" },
                    // Sérvia
                    { "Belgrado", "Europe/Belgrade" },
                    // Seychelles
                    { "Victoria", "Indian/Mahe" },
                    // Serra Leoa
                    { "Freetown", "Africa/Freetown" },
                    // Singapura
                    { "Singapura", "Asia/Singapore" },
                    // Eslováquia
                    { "Bratislava", "Europe/Bratislava" },
                    // Eslovênia
                    { "Liubliana", "Europe/Ljubljana" },
                    // Ilhas Salomão
                    { "Honiara", "Pacific/Guadalcanal" },
                    // Somália
                    { "Mogadíscio", "Africa/Mogadishu" },
                    // África do Sul
                    { "Joanesburgo", "Africa/Johannesburg" },
                    // Sudão do Sul
                    { "Juba", "Africa/Juba" },
                    // Espanha
                    { "Madri", "Europe/Madrid" },
                    // Sri Lanka
                    { "Colombo", "Asia/Colombo" },
                    // Sudão
                    { "Cartum", "Africa/Khartoum" },
                    // Suriname
                    { "Paramaribo", "America/Paramaribo" },
                    // Suazilândia
                    { "Mbabane", "Africa/Mbabane" },
                    // Suécia
                    { "Estocolmo", "Europe/Stockholm" },
                    // Suíça
                    { "Berna", "Europe/Zurich" },
                    // Síria
                    { "Damasco", "Asia/Damascus" },
                    // Taiwan
                    { "Taipei", "Asia/Taipei" },
                    // Tajiquistão
                    { "Duchambe", "Asia/Dushanbe" },
                    // Tanzânia
                    { "Dodoma", "Africa/Dar_es_Salaam" },
                    // Tailândia
                    { "Bangcoc", "Asia/Bangkok" },
                    // Togo
                    { "Lomé", "Africa/Lome" },
                    // Tonga
                    { "Nucualofa", "Pacific/Tongatapu" },
                    // Trinidad e Tobago
                    { "Porto de Espanha", "America/Port_of_Spain" },
                    // Tunísia
                    { "Túnis", "Africa/Tunis" },
                    // Turquia
                    { "Ancara", "Europe/Istanbul" },
                    // Turcomenistão
                    { "Asgabate", "Asia/Ashgabat" },
                    // Tuvalu
                    { "Funafuti", "Pacific/Funafuti" },
                    // Uganda
                    { "Kampala", "Africa/Kampala" },
                    // Ucrânia
                    { "Kiev", "Europe/Kiev" },
                    // Emirados Árabes Unidos
                    { "Abu Dhabi", "Asia/Dubai" },
                    // Reino Unido
                    { "Londres", "Europe/London" },
                    // Estados Unidos
                    { "Nova Iorque", "America/New_York" },
                    // Uruguai
                    { "Montevidéu", "America/Montevideo" },
                    // Uzbequistão
                    { "Tashkent", "Asia/Tashkent" },
                    // Vanuatu
                    { "Port Vila", "Pacific/Efate" },
                    // Santa Sé
                    { "Cidade do Vaticano", "Europe/Vatican" },
                    // Venezuela
                    { "Caracas", "America/Caracas" },
                    // Vietnã
                    { "Hanói", "Asia/Ho_Chi_Minh" },
                    // Iêmen
                    { "Saná", "Asia/Aden" },
                    // Zâmbia
                    { "Lusaka", "Africa/Lusaka" },
                    // Zimbábue
                    { "Harare", "Africa/Harare" }
                };

                if (!cityToTimezone.ContainsKey(cityName))
                {
                    return $"Error: Timezone not found for city {cityName}";
                }

                string timezone = cityToTimezone[cityName];
                string apiKey = "KEY";  // Substitua pela sua chave de API da TimeZoneDB
                string url = $"http://api.timezonedb.com/v2.1/get-time-zone?key={apiKey}&format=json&by=zone&zone={timezone}";

                var client = new RestClient(url);
                var request = new RestRequest();

            try
                {
                    var response = await client.ExecuteAsync(request);

                    if (response.IsSuccessful)
                    {
                        var data = JObject.Parse(response.Content);
                        if (data["status"].ToString() == "OK")
                        {
                            long timestamp = data["timestamp"].ToObject<long>();
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                            string formattedTime = dateTimeOffset.ToString("HH:mm:ss");
                            return formattedTime;
                    }
                        else
                        {
                            return $"Error: {data["message"]}";
                        }
                    }
                    else
                    {
                        return $"Error: Unable to fetch time for city {cityName}. Error message: {response.ErrorMessage}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
        }
    }
}