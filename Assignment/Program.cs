using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportApp
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.Clear();
            string choice = null;

            List<(string home, string away, string result)>result=new List<(string home, string away, string result)>();
            var shooters=new Dictionary<string,int>();
            var dictionary = new Dictionary<string, (string position, int rating)>(){
            {"Luka Modric", ("MF", 88)},
            {"Marcelo Brozovic",("MF",86)},
            {"Mateo Kovacic", ("MF", 84)},
            {"Ivan Perišic", ("FW", 84)},
            {"Ivan Rakitic", ("MF", 82)},
            {"Joško Gvardiol", ("DF", 81)},
            {"Mario Pasalic", ("MF", 81)},
            {"Lovro Majer", ("MF", 80)},
            {"Dominik Livakovic", ("GK", 80)},
            {"Ante Rebic", ("FW", 80)},
            {"Josip Brekalo", ("MF", 79)},
            {"Borna Sosa", ("DF", 78)},
            {"Nikola Vlasic", ("MF", 78)},
            {"Dejan Lovren", ("DF", 78)},
            {"Mislav Orsic", ("FW", 77)},
            {"Marko Livaja", ("FW", 77)},
            {"Domagoj Vida", ("DF", 76)},
            {"Ante Budimir", ("FW", 78)},
            {"Marko Pjaca",("MF",75)},
            {"Luka Ivanusec",("MF",75)}
        };

        Dictionary<string, (int points, int goalDifference)> teamTable = new()
        {
            {"Croatia",(0,0) },
            {"Morocco",(0,0) },
            {"Belgium",(0,0) },
            {"Canada",(0,0) }
        };
        
            do
            {

                Options();
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AttendPractice(dictionary);
                        break;
                    case "2":
                        Console.Clear();
                        PlayGame(dictionary,result,shooters,teamTable);
                        Console.WriteLine("All games played!\n");

                        break;
                    case "3":
                        Console.Clear();
                        SortAndPrint(dictionary,result,shooters,teamTable);
                        //Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case "4":
                        Console.Clear();
                        ControlPlayer(dictionary);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Wrong entry!Try again!\n");
                        break;
                }
            } while (choice != "0");
        }


        static void Menu1()
        {   
            Console.WriteLine("\n\n-----------Statistics Menu-------------\n");
            Console.WriteLine("Welcome to statistics. Choose which action you want to make:\n");
            Console.WriteLine("1.Print in order\n" +
            "2.Print by rating ascending\n" +
            "3.Print by rating descending\n" +
            "4.Find players by name\n" +
            "5.Find players by rating\n" +
            "6.Find players by position\n" +
            "7.Print current team\n" +
            "8.Print shooters and goals\n" +
            "9.Print team score\n" +
            "10.Print all scores\n" +
            "11.Print team tables\n"+"0.Go back\n");
        }
        static void Options()
        {
            Console.WriteLine("------------Main Menu------------\n");
            Console.WriteLine("Welcome to main menu. Choose which action you want to make:");
            Console.WriteLine("1.Attend practice\n"+"2.Play game\n"+"3.Statistics\n"+"4.Control Player\n"+"0.Exit app");
        }
        static string AskName()
        {
            Console.WriteLine("Please enter information for the player you want to change");
            var nameToChange = GetName();
            return nameToChange;
        }
        static string GetName()
        {
            Console.WriteLine("Enter player's name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter player's last name:");
            var lastName = Console.ReadLine();
            var fullName = firstName.Trim() + " " + lastName.Trim();

            return fullName;

        }
        static void AttendPractice(Dictionary<string, (string position, int rating)> dictionary)
        {
            Console.Clear();
            Random rnd = new Random();
            foreach (var person in dictionary)
            {
                var oldPlayerRating = person.Value.rating;
                var percentage = rnd.Next(-5, 5) / 100f;
                int newRating = (int)(oldPlayerRating * (1 + percentage));
                string value = $"{person.Key}({person.Value.position})\nold rating:{oldPlayerRating}\nnew rating:{newRating}\n";
                Console.WriteLine(value);
                Console.WriteLine("");
                dictionary[person.Key]=(person.Value.position,newRating);//adding the new rating to the player
            }
            Console.WriteLine("Practice compleated!\n");       
        }
        static Dictionary<string, (string position, int rating)> ChooseTeam(Dictionary<string, (string position, int rating)> dictionary)
        {
            var sortedDictionary = from person in dictionary orderby person.Value.rating descending select person;
            int GK = 0, DF = 0, MF = 0, FW = 0;
            var teamDict = new Dictionary<string, (string position, int rating)>();
            foreach (var person in sortedDictionary)
            {
                if (person.Value.position == "GK" && GK < 1){
                    teamDict.Add(person.Key,person.Value);
                    GK++;
                }
                else if (person.Value.position == "DF" && DF < 4){
                    teamDict.Add(person.Key,person.Value);
                    DF++;
                }
                else if (person.Value.position == "FW" && FW < 3){
                    teamDict.Add(person.Key,person.Value);
                    FW++;
                }
                else if (person.Value.position == "MF" && MF < 3){
                    teamDict.Add(person.Key,person.Value);
                    MF++;
                }

            }
             return teamDict;
        }
        
        static List<(string, string, int)> makeList(Dictionary<string, (string, int)> dictionary)
        {
            var list = new List<(string, string, int)>();
            foreach (var person in dictionary)
            {
                list.Add((person.Key, person.Value.Item1, person.Value.Item2));
            }
            return list;
        }
    
        static void PlayGame(Dictionary<string, (string position, int rating)> dictionary,List<(string home, string away, string result)>result,Dictionary<string, int> shooters,Dictionary<string, (int points, int goalDifference)> teamTable)
        {
            Console.Clear();
            Random random = new Random();

            List<string>opponentss=new List<string>(){
                {"Canada"}, {"Morocco"}, {"Belgium" }
            };

            List<int>checkList=new List<int>();
            
            var temp=-1;
            var opponents=opponentss.ToArray();
            for (int kolo=0;kolo<3;kolo++)
            {   
                Console.WriteLine($"=========Round {kolo+1}=========");

                var index1=-1;
                do
                {
                    index1 = random.Next(opponents.Length);

                }while(index1==temp||checkList.Contains(index1));
                var opponent = opponents[index1];
                
                //popravit ovo
                var team1="";
                var team2="";
                if(index1.Equals(0)){
                    team1=opponents[1];
                    team2=opponents[2];

                }
                else if(index1.Equals(1)){
                    team1=opponents[0];
                    team2=opponents[2];
                }
                else{
                    team1=opponents[0];
                    team2=opponents[1];
                }

                Console.WriteLine($"Croatia playing against {opponent}!\n");
                var team = ChooseTeam(dictionary);
                if (team.Count == 11)
                {
                    var homeScore = random.Next(0, 5);
                    var awayScore = random.Next(0, 5);
                    Console.WriteLine($"Score after the game: {homeScore}:{awayScore}");

                    var homePoints = 0;
                    var awayPoints = 0;

                    if (homeScore > awayScore)
                    {
                        homePoints = 3;
                        awayPoints = 0;

                        //svakom igracu se povećava rating za 2% 
                        foreach (var person in team)
                        {
                            dictionary[person.Key] = (person.Value.position, (int)((person.Value.rating * 0.02)+person.Value.rating));
                        }
                        Console.WriteLine("Croatia won!\n");
                    }
                    else if (homeScore < awayScore)
                    {
                        homePoints = 0;
                        awayPoints = 3;

                        //svakom igracu se smanjuje rating za 2%
                        foreach (var person in team)
                        {
                            dictionary[person.Key] = (person.Value.position, (int)(person.Value.rating-(person.Value.rating * (0.02))));
                        }
                        Console.WriteLine($"{opponent} won!\n");
                    }
                    else
                    {//izjednaceni
                        homePoints = 1;
                        awayPoints = 1;
    
                    }
                    result.Add(("Croatia",opponent,$"{homeScore}:{awayScore}"));
                    //add points to table
                    teamTable["Croatia"] = (teamTable["Croatia"].points + homePoints, teamTable["Croatia"].goalDifference + (homeScore - awayScore));
                    teamTable[opponent] = (teamTable[opponent].points + homePoints, teamTable[opponent].goalDifference + (awayScore - homeScore));

                
                    Console.WriteLine("List of croatian shooters: ");
                    for (int i = 0; i < homeScore; i++)
                    {
                        var index = random.Next(team.Count);
                        var names = team.Keys.ToList();
                        Console.WriteLine($"{names[index]}");//dodaj mu rating
                        var item=names[index];
                        dictionary[item]=(dictionary[item].position,dictionary[item].rating+(int)(double)(dictionary[item].rating*0.05));
                        if(shooters.ContainsKey(names[index])==false){
                            shooters.Add(names[index],1);
                        }
                        else{
                            shooters[names[index]]++;
                        }
                            
                    }
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"\n{team1} playing against {team2}!\n");
                    var score1 = random.Next(0, 5);
                    var score2 = random.Next(0, 5);
                    Console.WriteLine($"Score after the game: {score1}:{score2}");
                
                    var points1 = 0;
                    var points2 = 0;

                    if (score1 > score2)
                    {
                        points1 = 3;
                        points2 = 0;
                        Console.WriteLine($" {team1} won!\n");
                    }
                    else if (score1 < score2)
                    {
                        points1 = 0;
                        points2 = 3;
                        Console.WriteLine($" {team2} won!\n");

                    }
                    else
                    {//izjednaceni
                        points1 = 1;
                        points2 = 1;
                    }
                    result.Add((team1,team2,$"{score1}:{score2}"));
                
                    teamTable[team1] = (teamTable[team1].points + points1, teamTable[team1].goalDifference + (score1 - score2));
                    teamTable[team2] = (teamTable[team2].points + points2, teamTable[team2].goalDifference + (score2 - score1));


                    temp=index1;
                    checkList.Add(temp);
                }
                else Console.WriteLine("Not enough players in the team!\n");
            }
        } 

        static void SortAndPrint(Dictionary<string, (string position, int rating)> dictionary,List<(string home, string away, string result)>result,Dictionary<string, int> shooters,Dictionary<string, (int points, int goalDifference)> teamTable)
        {
            Console.Clear();
            string menu = null;
            if (dictionary.Count == 0)
                return;
            do
            {
            
                Menu1();
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        foreach (var person in dictionary)
                        {
                            Console.WriteLine(person.Key + " " + person.Value.position + " " + person.Value.rating);
                        }
                        break;
                    case "2":
                        Console.Clear();
                        var sortedDictionary = from person in dictionary orderby person.Value.rating ascending select person;
                        foreach (var person in sortedDictionary)
                        {
                            Console.WriteLine(person.Key + " " + person.Value.position + " " + person.Value.rating);
                        }
                        break;
                    case "3":
                        //Ispis po ratingu silazno
                        Console.Clear();
                        var sortedDictionary1 = from person in dictionary orderby person.Value.rating descending select person;
                        foreach (var person in sortedDictionary1)
                        {
                            Console.WriteLine(person.Key + " " + person.Value.position + " " + person.Value.rating);
                        }
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter information for the player you want to find");
                        var searchedPlayer = GetName();
                        bool flag = false;
                        foreach (var person in dictionary)
                        {
                            if (searchedPlayer == person.Key)
                            {
                                Console.WriteLine($"{person.Key}\nPosition: {person.Value.position}\nRating:{person.Value.rating}\n");
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            Console.WriteLine("Player not found!");
                        }
                        break;
                    case "5":
                        Console.Clear();
                        var wantedRating = 0;
                        Console.WriteLine("Enter the rating you want to find players with");
                        int.TryParse(Console.ReadLine(), out wantedRating);

                        foreach (var person in dictionary)
                        {
                            if (wantedRating == person.Value.rating)
                                Console.WriteLine($"{person.Key}({person.Value.position})--{person.Value.rating}");
                        }
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Enter the position you want to find players with");
                        var wantedPosition = Console.ReadLine();

                        foreach (var person in dictionary)
                        {
                            if (wantedPosition == person.Value.position)
                                Console.WriteLine($"{person.Key}({person.Value.position})--{person.Value.rating}");
                            else Console.WriteLine("Player not in a dictionary!\n");
                        }
                        break;
                    case "7":
                        Console.Clear();
                        PrintDictionary(ChooseTeam(dictionary));
                        break;
                    case "8":
                        Console.Clear();
                        Console.WriteLine("Shooters are:");
                        foreach(var person in shooters)
                        {
                            Console.WriteLine($"{person.Key}\nGoals made:{person.Value}\n");
                        }
                        break;
                    case "9":
                        Console.Clear();
                        foreach (var item in result)
                        {
                            if(item.home=="Croatia"){
                                Console.WriteLine($"{item.home}-{item.away}  {item.result}");
                            }
                        }
                        break;
                    case "10":
                        Console.Clear();
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.home} {item.result} {item.away}");
                        }
                        break;
                    case "11":
                        Console.Clear();
                        Console.WriteLine("Country\t    Points\tGoal Difference");
                        foreach (var item in teamTable)
                        {
                            Console.WriteLine($"{item.Key}\t\t{item.Value.points}\t\t{Math.Abs(item.Value.goalDifference)}");
                
                        }
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (menu!="0");
            return;
        }

        static void ControlPlayer(Dictionary<string, (string position, int rating)> dictionary)
        {
            Console.Clear();
            string option = null;
    
                //Console.Clear();
                Console.WriteLine("\n-------------Control player------------\n");
                Console.WriteLine("1.Add new player\n" +
                "2.Delete player\n" +
                "3.Change player\n" +
                "0.Go back to main menu\n");
                option = Console.ReadLine();

            if(option!="0"){
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        AddPlayer(dictionary);
                        break;
                    case "2":
                        Console.Clear();
                        DeletePlayer(dictionary);
                        break;
                    case "3":
                        Console.Clear();
                        ChangePlayer(dictionary);
                        break;
                    default:
                        Console.Clear();
                        option = null;
                        break;
                }

            } 
            else{
                Console.Clear();
            }
            return;

        }
        static void AddPlayer(Dictionary<string, (string position, int rating)> dictionary)
        {
            bool flag = true;
            Console.Clear();
            var fullName = GetName();
            if(fullName.Length==1){
                Console.WriteLine("Name not valid!\n");
                return;
            }

            Console.WriteLine("Enter player position(GK,MF,FW,DF):");
            var playerPosition = Console.ReadLine().ToUpper();
            if (!playerPosition.Equals("MF") && !playerPosition.Equals("FW") && !playerPosition.Equals("DF") &&!playerPosition.Equals("GK"))

            {
                Console.WriteLine("Incorrect position!Try again!");
                return;
            }

            var playerRating = 0;
            Console.WriteLine("Enter player rating(range:1-100):");
            int.TryParse(Console.ReadLine(), out playerRating);
            if (playerRating < 1 || playerRating > 100){
                Console.WriteLine("Incorrect rating!Try again!");
                return;
            }
            if (dictionary.Count < 26)
            {
                foreach (var person in dictionary)
                {
                    if (person.Key.Contains(fullName))
                        flag = false;

                }
                if (flag == true)
                {
                    dictionary.Add(fullName, (playerPosition, playerRating));
                    Console.WriteLine("You have sucessfully added player!\n");
                }
                else
                    Console.WriteLine("Player already in dictionary!\n");

            }
            else Console.WriteLine("There are already 26 players in this team!\n");

            return;
        }
        static void DeletePlayer(Dictionary<string, (string position, int rating)> dictionary)
        {
            bool flag = false;

            Console.WriteLine("Enter the information about player you want to delete:");
            var deleteName = GetName();
            Console.WriteLine(deleteName);

            foreach (var person in dictionary)
            {
                if (person.Key.Contains(deleteName))
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                dictionary.Remove(deleteName);
                Console.WriteLine("You have sucessfully deleted player!\n");
            }
            else Console.WriteLine("Player not found!\n");

            return;
        }

        private static void ChangePlayer(Dictionary<string, (string position, int rating)> dictionary)
        {
            //fix come back to main menu
            bool flag = false;
            string choice = null;

            Console.WriteLine("You choose to change player! Enter the change you want to make\n");
            Console.WriteLine("1.Change player name and surname\n" +
                "2.Change player position\n" +
                "3.Change player rating\n" +
                "0.Go back");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                var nameToChange = AskName();

                if (dictionary.ContainsKey(nameToChange))
                {
                    Console.WriteLine("\nNow enter new information");
                    var newName = GetName();
                    if(newName.Length==1){
                        Console.WriteLine("Invalid input! Try again!\n");
                        return;
                    }

                    if (!dictionary.ContainsKey(newName))
                    {
                        var temp = dictionary[nameToChange];
                        dictionary.Remove(nameToChange);
                        dictionary[newName] = temp;
                        Console.WriteLine("Yu have successfully changed info!\n");
                    }
                }
            }
            else if (choice == "2")
            {
                var nameToChange = AskName();

                Console.WriteLine("Please enter new position:");
                var newPosition = Console.ReadLine();
                if (!newPosition.Equals("MF") && !newPosition.Equals("FW") && !newPosition.Equals("DF") &&!newPosition.Equals("GK"))
                {
                    Console.WriteLine("Incorrect position!Try again!");
                    return;
                }

                foreach (var person in dictionary)
                {
                    if (nameToChange == person.Key)
                        dictionary[nameToChange] = (newPosition, person.Value.rating);
                }
            }

            else if (choice == "3")
            {
                var newRating=0;
                var nameToChange = AskName();


                Console.WriteLine("Please enter new rating");
                int.TryParse(Console.ReadLine(), out newRating);
                if (newRating < 1 || newRating > 100)
                {
                    Console.WriteLine("Incorrect rating!Try again!");
                    return;
                }

                foreach (var person in dictionary)
                {
                    if (nameToChange == person.Key)
                        dictionary[nameToChange] = (person.Value.position, newRating);
                }
            }

            else if (choice == "0")
            {
                ControlPlayer(dictionary);
            }

            else Console.WriteLine("Wrong input!Try again!");
        }


        static void PrintDictionary(Dictionary<string, (string position, int rating)> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"{item.Key}({item.Value.position})  Rating {item.Value.rating}");
            }
            Console.WriteLine(" ");
        }

}
}
