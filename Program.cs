using Relaciones_entre_clases;
using Relaciones_entre_clases.Cars;
using Relaciones_entre_clases.Objects;
using Relaciones_entre_clases.Sistem;

#region
string welcomeTitle_filePath = "C:\\Users\\Nico\\source\\repos\\Relaciones entre clases\\Titles.txt";
string CongratulationTitle_filePath = "C:\\Users\\Nico\\source\\repos\\Relaciones entre clases\\Congratulations.txt";
string welcomeTitle = File.ReadAllText(welcomeTitle_filePath);
string CongratulationTitle = File.ReadAllText(CongratulationTitle_filePath);
string winnerTitle = "";
Console.WriteLine($"{welcomeTitle} \n");
Console.ReadKey();
#endregion

Battery smallBattery = new Battery(1800, "Small battery");
Battery mediumBattery = new Battery(2800, "Medium battery");
Battery mediumBattery2 = new Battery(2800, "Medium battery");
Battery mediumbattery3 = new Battery(2800, "Medium battery");
Battery bigBattery = new Battery(4000, "Big battery");

Charger SlowCharger = new Charger(8, 7);
Charger StandarCharger = new Charger(16, 3);
Charger turboCharger = new Charger(30, 1);

#region GameConfigurations

int start = 550;
int end = 560;
int intervalOfZones = 100;
List<int> batteryZones = new List<int>();
int raceLenght = 900;
bool HasSelectOptions = false;
Charger? chargerSelected = null;
int batteryPercent = 0;
float rechargeAmount = 0;
float rechargePercentage;
int distanceRange = 0;
int playerCount = 1;
float winnerTime = (float) int.MaxValue;
int winnerplayer;

for (int i = 0; i < 8; i++)
{
    List<int> zoneRange = Enumerable.Range(start, end - start + 1).ToList();
    batteryZones.AddRange(zoneRange);
    start += intervalOfZones;
    end += intervalOfZones;
}

#endregion 

//INHERITANCE - LISKOV SUBSTITUTION PRINCIPLE
ClassicModelCar player1 = new ClassicModelCar(2, mediumBattery);
ClassicModelCar player2 = new FirstNewModelCar(3, mediumBattery2);
ClassicModelCar player3 = new SecondNewModelCar(3, bigBattery);

//Not good practice, violation of single responsability principle
string ComputeTurn(ClassicModelCar player)
{
    float frame = 0;
    float time = 0;
    Console.Clear();
    Console.WriteLine($"Player {playerCount} Ready");
    Console.WriteLine("Press any key to start");
    Console.ReadKey();
    player.StartEngines();
    bool raceFinised = false;
    int distanceMade = 0;
    bool canCharge = false;
    bool recharging = false;
    int waitUltilAsk = 1;
    int ChargingTime = 0;
    bool askedForTurboMode = false;
    string animation = "";
    int steps = 0;
    bool moveDown = true;
    int carPositionPercent;

    List<string> answers = new List<string>
    {
        "No",
        "Yes"
    };

    Dictionary<string, Charger> chargerOptions = new Dictionary<string, Charger>
    {
        { $"Slow Charger X{SlowCharger.Uses}", SlowCharger },
        { $"Standar Charger X{StandarCharger.Uses}", StandarCharger },
        { $"Turbo Charger X{turboCharger.Uses}", turboCharger }
    };

    while (!raceFinised)
    {

       

        canCharge = batteryZones.Contains(distanceMade);
        frame++;
        time = frame / 30;
        Console.Clear();
        Console.WriteLine($"Player {playerCount} \n");
        distanceMade = player.X / 10;

        #region Menu expecializado
       
        string chargerSelectionTitle = "Select your charger";

        

        if (waitUltilAsk == 3)
            waitUltilAsk = 1;

        if (waitUltilAsk == 1 && canCharge && recharging == false)
        {
            if (distanceRange != distanceMade)
            {
                waitUltilAsk++;
                distanceRange = distanceMade;
                Console.WriteLine("Distance: " + distanceMade);
                Console.WriteLine("Current battery level: " + batteryPercent + "% ");
                
                string chargerZoneTitle = $"Charger zone is available. Do you wish to recharge your battery now?";
                string answer = Menu.DisplayMenuOptions(answers, chargerZoneTitle);
                if (answer == "Yes")
                {
                    recharging = true;
                }
            }
        }
        #endregion

        void InitRechargingProcess()
        {
            if (recharging)
            {
                if (!HasSelectOptions)
                {
                    Console.Clear();
                    chargerSelected = Menu.DisplayMenuOptions(chargerOptions, chargerSelectionTitle);
                    while (chargerSelected.Uses <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("you don't have more uses of this charger, please select another");
                        chargerSelected = Menu.DisplayMenuOptions(chargerOptions, chargerSelectionTitle);
                    }
                    HasSelectOptions = true;
                    chargerSelected.Uses--;
                    List<string> rechargeOptions = new List<string>
                {
                    "100%",
                    "75%",
                    "50%",
                    "25%"
                };

                    Console.Clear();
                    Console.WriteLine("Current battery level: " + batteryPercent + "% ");
                    string answer = Menu.DisplayMenuOptions(rechargeOptions, "How much would you recharge?");

                    switch (answer)
                    {
                        case "100%":
                            rechargePercentage = 100;
                            break;
                        case "75%":
                            rechargePercentage = 75;
                            break;
                        case "50%":
                            rechargePercentage = 50;
                            break;
                        case "25%":
                            rechargePercentage = 25;
                            break;
                        default:
                            rechargePercentage = 0;
                            break;
                    }
                    rechargeAmount = batteryPercent + rechargePercentage;
                }

                Console.WriteLine("Recharging battery...");

                if (player.Battery.Level >= player.Battery.Capacity || batteryPercent >= rechargeAmount)
                {
                    Console.Clear();
                    Console.WriteLine("Current battery level: " + batteryPercent + "%");
                    Console.WriteLine("Time passed: " + ChargingTime / 30);
                    Console.WriteLine("Your vehicle is ready, GO!");
                    Console.ReadKey();
                    recharging = false;
                    HasSelectOptions = false;
                    player.StartEngines();
                }
                else
                {
                    ChargingTime++;
                    player.StopEngines();
                    player.RechargeBattery(chargerSelected);
                }
            }
        }

        string ShowCarInfo(ITelemetrySystem telemetry) => telemetry.GetTelemetryData(batteryPercent, time, distanceMade);

        if (player is FirstNewModelCar || player is SecondNewModelCar) Console.WriteLine(ShowCarInfo((ITelemetrySystem)player));

        InitRechargingProcess();

        player.Accelerate();
        batteryPercent = (int)(player.Battery.Level / (player.Battery.Capacity / 100f));
        Console.WriteLine();

        if (moveDown)
        {
            animation.Replace("\r", "\n");
            steps++;
            if (steps == 5)
            {
                moveDown = false;
            }
        }
        else
        {
            animation.Replace("\n", "\r");
            steps--;
            if (steps == 0)
            {
                moveDown = false;
            }
        }

        Console.WriteLine(animation + player.Chassis);

        if (player is SecondNewModelCar secondNewModelCar && distanceMade > 580 && askedForTurboMode == false)
        {
            string answer = Menu.DisplayMenuOptions(answers, "Do you want to activate turbo mode?");
            if (answer == "Yes")
                secondNewModelCar.ActivateTurboMode();
            askedForTurboMode = true;
        }

        if (player.Battery.Level <= 0)
        {
            Console.Clear();
            raceFinised = true;
            Console.WriteLine("You run out of battery!!");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return $"Player {playerCount++} \n Did not finish";
        }

        int screenWidth = Console.LargestWindowWidth;
        int raceLengthPercent = (int)(raceLenght / raceLenght) * 100;
        carPositionPercent = (distanceMade  * 100) / raceLenght;
        string raceProgressVisualization = "";
        for (int j = 0; j < raceLengthPercent; j ++)
            raceProgressVisualization += "░";

        try
        {
            raceProgressVisualization = raceProgressVisualization.Remove(carPositionPercent, 1).Insert(carPositionPercent, "█");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Race completed");
        }

        Console.WriteLine(raceProgressVisualization);

        if (distanceMade >= raceLenght)
        {
            raceFinised = true;
            Console.WriteLine("You Finish the race");
            Console.WriteLine($"Your time is: {time}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            if (time < winnerTime)
            {
                winnerplayer = playerCount;
                winnerTime = time;
                winnerTitle = $"THE WINNER IS PLAYER {playerCount} \nTIME {winnerTime}!!!";
            }else if(time == winnerTime) {
                winnerTitle = $"IT'S A DRAW!!!";
            }

        }
        Thread.Sleep(32);
    }
    raceFinised = false;

    return $"Player {playerCount++} \n Time: {time}";
}

List<ClassicModelCar> team1 = new List<ClassicModelCar>();

List<string> scores = new List<string>();
team1.Add(player2);
team1.Add(player3);
team1.Add(player1);

foreach (var player in team1)
    scores.Add(ComputeTurn(player));

foreach (var score in scores)
    Console.WriteLine("\n" + score);

Console.WriteLine($"\n  {winnerTitle} \n {CongratulationTitle}\n");





