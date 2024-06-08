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
            string cityName = "Bras�lia";  // Substitua pelo nome da cidade desejada
            dynamic currentTime = await GetCurrentTime(cityName);
            Console.WriteLine($"A hora atual em {cityName} �: {currentTime}");
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(currentTime));
        }

        static async Task<dynamic> GetCurrentTime(string cityName)
            {
                // Dicion�rio de cidades
                var cityToTimezone = new Dictionary<string, string>
                {
                    { "S�o Paulo", "America/Sao_Paulo" },
                    // Afeganist�o
                    { "Cabul", "Asia/Kabul" },
                    // Alb�nia
                    { "Tirana", "Europe/Tirane" },
                    // Arg�lia
                    { "Argel", "Africa/Algiers" },
                    // Andorra
                    { "Andorra la Vella", "Europe/Andorra" },
                    // Angola
                    { "Luanda", "Africa/Luanda" },
                    // Ant�gua e Barbuda
                    { "Saint John's", "America/Antigua" },
                    // Argentina
                    { "Buenos Aires", "America/Argentina/Buenos_Aires" },
                    // Arm�nia
                    { "Yerevan", "Asia/Yerevan" },
                    // Austr�lia
                    { "Sydney", "Australia/Sydney" },
                    // �ustria
                    { "Viena", "Europe/Vienna" },
                    // Azerbaij�o
                    { "Baku", "Asia/Baku" },
                    // Bahamas
                    { "Nassau", "America/Nassau" },
                    // Bahrain
                    { "Manama", "Asia/Bahrain" },
                    // Bangladesh
                    { "Daca", "Asia/Dhaka" },
                    // Barbados
                    { "Bridgetown", "America/Barbados" },
                    // Bielorr�ssia
                    { "Minsk", "Europe/Minsk" },
                    // B�lgica
                    { "Bruxelas", "Europe/Brussels" },
                    // Belize
                    { "Belmopan", "America/Belize" },
                    // Benin
                    { "Porto-Novo", "Africa/Porto-Novo" },
                    // But�o
                    { "Thimphu", "Asia/Thimphu" },
                    // Bol�via
                    { "Sucre", "America/La_Paz" },
                    // B�snia e Herzegovina
                    { "Sarajevo", "Europe/Sarajevo" },
                    // Botswana
                    { "Gaborone", "Africa/Gaborone" },
                    // Brasil
                    { "Bras�lia", "America/Sao_Paulo" },
                    // Brunei
                    { "Bandar Seri Begawan", "Asia/Brunei" },
                    // Bulg�ria
                    { "S�fia", "Europe/Sofia" },
                    // Burquina Faso
                    { "Ouagadougou", "Africa/Ouagadougou" },
                    // Burundi
                    { "Gitega", "Africa/Bujumbura" },
                    // Cabo Verde
                    { "Praia", "Atlantic/Cape_Verde" },
                    // Camboja
                    { "Phnom Penh", "Asia/Phnom_Penh" },
                    // Camar�es
                    { "Yaound�", "Africa/Douala" },
                    // Canad�
                    { "Toronto", "America/Toronto" },
                    // Rep�blica Centro-Africana
                    { "Bangui", "Africa/Bangui" },
                    // Chade
                    { "N'Djamena", "Africa/Ndjamena" },
                    // Chile
                    { "Santiago", "America/Santiago" },
                    // China
                    { "Pequim", "Asia/Shanghai" },
                    // Col�mbia
                    { "Bogot�", "America/Bogota" },
                    // Comores
                    { "Moroni", "Indian/Comoro" },
                    // Congo, Rep�blica Democr�tica do
                    { "Kinshasa", "Africa/Kinshasa" },
                    // Congo, Rep�blica do
                    { "Brazzaville", "Africa/Brazzaville" },
                    // Costa Rica
                    { "San Jos�", "America/Costa_Rica" },
                    // Cro�cia
                    { "Zagreb", "Europe/Zagreb" },
                    // Cuba
                    { "Havana", "America/Havana" },
                    // Chipre
                    { "Nic�sia", "Asia/Nicosia" },
                    // Rep�blica Checa
                    { "Praga", "Europe/Prague" },
                    // Dinamarca
                    { "Copenhague", "Europe/Copenhagen" },
                    // Djibouti
                    { "Djibouti", "Africa/Djibouti" },
                    // Dominica
                    { "Roseau", "America/Dominica" },
                    // Rep�blica Dominicana
                    { "Santo Domingo", "America/Santo_Domingo" },
                    // Equador
                    { "Quito", "America/Guayaquil" },
                    // Egito
                    { "Cairo", "Africa/Cairo" },
                    // El Salvador
                    { "San Salvador", "America/El_Salvador" },
                    // Guin� Equatorial
                    { "Malabo", "Africa/Malabo" },
                    // Eritreia
                    { "Asmara", "Africa/Asmara" },
                     // Est�nia      
                     { "Tallinn", "Europe/Tallinn" },
                    // Eti�pia
                    { "Adis Abeba", "Africa/Addis_Ababa" },
                    // Fiji
                    { "Suva", "Pacific/Fiji" },
                    // Finl�ndia
                    { "Helsinque", "Europe/Helsinki" },
                    // Fran�a
                    { "Paris", "Europe/Paris" },
                    // Gab�o
                    { "Libreville", "Africa/Libreville" },
                    // G�mbia
                    { "Banjul", "Africa/Banjul" },
                    // Ge�rgia
                    { "Tbilisi", "Asia/Tbilisi" },
                    // Alemanha
                    { "Berlim", "Europe/Berlin" },
                    // Gana
                    { "Acra", "Africa/Accra" },
                    // Gr�cia
                    { "Atenas", "Europe/Athens" },
                    // Granada
                    { "Saint George's", "America/Grenada" },
                    // Guatemala
                    { "Cidade da Guatemala", "America/Guatemala" },
                    // Guin�
                    { "Conacri", "Africa/Conakry" },
                    // Guin�-Bissau
                    { "Bissau", "Africa/Bissau" },
                    // Guiana
                    { "Georgetown", "America/Guyana" },
                    // Haiti
                    { "Porto Pr�ncipe", "America/Port-au-Prince" },
                    // Honduras
                    { "Tegucigalpa", "America/Tegucigalpa" },
                    // Hungria
                    { "Budapeste", "Europe/Budapest" },
                    // Isl�ndia
                    { "Reykjavik", "Atlantic/Reykjavik" },
                    // �ndia
                    { "Nova Deli", "Asia/Kolkata" },
                    // Indon�sia
                    { "Jacarta", "Asia/Jakarta" },
                    // Ir�
                    { "Teer�", "Asia/Tehran" },
                    // Iraque
                    { "Bagd�", "Asia/Baghdad" },
                    // Irlanda
                    { "Dublin", "Europe/Dublin" },
                    // Israel
                    { "Jerusal�m", "Asia/Jerusalem" },
                    // It�lia
                    { "Roma", "Europe/Rome" },
                    // Jamaica
                    { "Kingston", "America/Jamaica" },
                    // Jap�o
                    { "T�quio", "Asia/Tokyo" },
                    // Jord�nia
                    { "Am�", "Asia/Amman" },
                    // Cazaquist�o
                    { "Nur-Sultan", "Asia/Almaty" },
                    // Qu�nia
                    { "Nair�bi", "Africa/Nairobi" },
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
                    // Quirguist�o
                    { "Bishkek", "Asia/Bishkek" },
                    // Laos
                    { "Vientiane", "Asia/Vientiane" },
                    // Let�nia
                    { "Riga", "Europe/Riga" },
                    // L�bano
                    { "Beirute", "Asia/Beirut" },
                    // Lesoto
                    { "Maseru", "Africa/Maseru" },
                    // Lib�ria
                    { "Monr�via", "Africa/Monrovia" },
                    // L�bia
                    { "Tr�poli", "Africa/Tripoli" },
                    // Liechtenstein
                    { "Vaduz", "Europe/Vaduz" },
                    // Litu�nia
                    { "Vilnius", "Europe/Vilnius" },
                    // Luxemburgo
                    { "Luxemburgo", "Europe/Luxembourg" },
                    // Madagascar
                    { "Antananarivo", "Indian/Antananarivo" },
                    // Malaui
                    { "Lilongwe", "Africa/Blantyre" },
                    // Mal�sia
                    { "Kuala Lumpur", "Asia/Kuala_Lumpur" },
                    // Maldivas
                    { "Mal�", "Indian/Maldives" },
                    // Mali
                    { "Bamako", "Africa/Bamako" },
                    // Malta
                    { "Valletta", "Europe/Malta" },
                    // Ilhas Marshall
                    { "Majuro", "Pacific/Majuro" },
                    // Maurit�nia
                    { "Nouakchott", "Africa/Nouakchott" },
                    // Maur�cio
                    { "Port Louis", "Indian/Mauritius" },
                    // M�xico
                    { "Cidade do M�xico", "America/Mexico_City" },
                    // Micron�sia
                    { "Palikir", "Pacific/Pohnpei" },
                    // Mold�via
                    { "Chisinau", "Europe/Chisinau" },
                    // M�naco
                    { "M�naco-Ville", "Europe/Monaco" },
                    // Mong�lia
                    { "Ulan Bator", "Asia/Ulaanbaatar" },
                    // Montenegro
                    { "Podgorica", "Europe/Podgorica" },
                    // Marrocos
                    { "Rabat", "Africa/Casablanca" },
                    // Mo�ambique
                    { "Maputo", "Africa/Maputo" },
                    // Mianmar (Birm�nia)
                    { "Naypyidaw", "Asia/Yangon" },
                    // Nam�bia
                    { "Windhoek", "Africa/Windhoek" },
                    // Nauru
                    { "Yaren", "Pacific/Nauru" },
                    // Nepal
                    { "Catmandu", "Asia/Kathmandu" },
                    // Holanda
                    { "Amsterd�", "Europe/Amsterdam" },
                    // Nova Zel�ndia
                    { "Wellington", "Pacific/Auckland" },
                    // Nicar�gua
                    { "Man�gua", "America/Managua" },
                    // N�ger
                    { "Niamey", "Africa/Niamey" },
                    // Nig�ria
                    { "Abuja", "Africa/Lagos" },
                    // Maced�nia do Norte
                    { "Skopje", "Europe/Skopje" },
                    // Noruega
                    { "Oslo", "Europe/Oslo" },
                    // Om�
                    { "Mascate", "Asia/Muscat" },
                    // Paquist�o
                    { "Islamabad", "Asia/Karachi" },
                    // Palau
                    { "Ngerulmud", "Pacific/Palau" },
                    // Palestina
                    { "Ramallah", "Asia/Hebron" },
                    // Panam�
                    { "Cidade do Panam�", "America/Panama" },
                    // Papua Nova Guin�
                    { "Port Moresby", "Pacific/Port_Moresby" },
                    // Paraguai
                    { "Assun��o", "America/Asuncion" },
                    // Peru
                    { "Lima", "America/Lima" },
                    // Filipinas
                    { "Manila", "Asia/Manila" },
                    // Pol�nia
                    { "Vars�via", "Europe/Warsaw" },
                    // Portugal
                    { "Lisboa", "Europe/Lisbon" },
                    // Catar
                    { "Doha", "Asia/Qatar" },
                    // Rom�nia
                    { "Bucareste", "Europe/Bucharest" },
                    // R�ssia
                    { "Moscou", "Europe/Moscow" },
                    // Ruanda
                    { "Kigali", "Africa/Kigali" },
                    // S�o Crist�v�o e Nevis
                    { "Basseterre", "America/St_Kitts" },
                    // Santa L�cia
                    { "Castries", "America/St_Lucia" },
                    // S�o Vicente e Granadinas
                    { "Kingstown", "America/St_Vincent" },
                    // Samoa
                    { "Apia", "Pacific/Apia" },
                    // San Marino
                    { "San Marino", "Europe/San_Marino" },
                    // S�o Tom� e Pr�ncipe
                    { "S�o Tom�", "Africa/Sao_Tome" },
                    // Ar�bia Saudita
                    { "Riade", "Asia/Riyadh" },
                    // Senegal
                    { "Dacar", "Africa/Dakar" },
                    // S�rvia
                    { "Belgrado", "Europe/Belgrade" },
                    // Seychelles
                    { "Victoria", "Indian/Mahe" },
                    // Serra Leoa
                    { "Freetown", "Africa/Freetown" },
                    // Singapura
                    { "Singapura", "Asia/Singapore" },
                    // Eslov�quia
                    { "Bratislava", "Europe/Bratislava" },
                    // Eslov�nia
                    { "Liubliana", "Europe/Ljubljana" },
                    // Ilhas Salom�o
                    { "Honiara", "Pacific/Guadalcanal" },
                    // Som�lia
                    { "Mogad�scio", "Africa/Mogadishu" },
                    // �frica do Sul
                    { "Joanesburgo", "Africa/Johannesburg" },
                    // Sud�o do Sul
                    { "Juba", "Africa/Juba" },
                    // Espanha
                    { "Madri", "Europe/Madrid" },
                    // Sri Lanka
                    { "Colombo", "Asia/Colombo" },
                    // Sud�o
                    { "Cartum", "Africa/Khartoum" },
                    // Suriname
                    { "Paramaribo", "America/Paramaribo" },
                    // Suazil�ndia
                    { "Mbabane", "Africa/Mbabane" },
                    // Su�cia
                    { "Estocolmo", "Europe/Stockholm" },
                    // Su��a
                    { "Berna", "Europe/Zurich" },
                    // S�ria
                    { "Damasco", "Asia/Damascus" },
                    // Taiwan
                    { "Taipei", "Asia/Taipei" },
                    // Tajiquist�o
                    { "Duchambe", "Asia/Dushanbe" },
                    // Tanz�nia
                    { "Dodoma", "Africa/Dar_es_Salaam" },
                    // Tail�ndia
                    { "Bangcoc", "Asia/Bangkok" },
                    // Togo
                    { "Lom�", "Africa/Lome" },
                    // Tonga
                    { "Nucualofa", "Pacific/Tongatapu" },
                    // Trinidad e Tobago
                    { "Porto de Espanha", "America/Port_of_Spain" },
                    // Tun�sia
                    { "T�nis", "Africa/Tunis" },
                    // Turquia
                    { "Ancara", "Europe/Istanbul" },
                    // Turcomenist�o
                    { "Asgabate", "Asia/Ashgabat" },
                    // Tuvalu
                    { "Funafuti", "Pacific/Funafuti" },
                    // Uganda
                    { "Kampala", "Africa/Kampala" },
                    // Ucr�nia
                    { "Kiev", "Europe/Kiev" },
                    // Emirados �rabes Unidos
                    { "Abu Dhabi", "Asia/Dubai" },
                    // Reino Unido
                    { "Londres", "Europe/London" },
                    // Estados Unidos
                    { "Nova Iorque", "America/New_York" },
                    // Uruguai
                    { "Montevid�u", "America/Montevideo" },
                    // Uzbequist�o
                    { "Tashkent", "Asia/Tashkent" },
                    // Vanuatu
                    { "Port Vila", "Pacific/Efate" },
                    // Santa S�
                    { "Cidade do Vaticano", "Europe/Vatican" },
                    // Venezuela
                    { "Caracas", "America/Caracas" },
                    // Vietn�
                    { "Han�i", "Asia/Ho_Chi_Minh" },
                    // I�men
                    { "San�", "Asia/Aden" },
                    // Z�mbia
                    { "Lusaka", "Africa/Lusaka" },
                    // Zimb�bue
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