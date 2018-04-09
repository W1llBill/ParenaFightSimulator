using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParenaFightSimulator
{
    class Program
    {
        public static string class1 = "static";
        public static string class2 = "static";
        public static string weapon1 = "static";
        public static string weapon2 = "static";



        static void Main(string[] args)
        {
        start:

            //intro
            Console.WriteLine("");
            Console.WriteLine("Welcome to Parena.fun Fight Simulator!");
            Console.WriteLine("");
            Console.WriteLine("List of Classes:");
            Console.WriteLine("Gladiator\tWarrior\t\tKnight");
            Console.WriteLine("Barbarian\tDwarf\t\tElf");
            Console.WriteLine("Orc\t\tTheif\t\tPirate");
            Console.WriteLine("Mercenary");
            Console.WriteLine("");
            Console.WriteLine("List of Weapons");
            Console.WriteLine("Spear\t\tSword & Shield\tMace");
            Console.WriteLine("2-Handed Sword\tAxe\t\tBow");
            Console.WriteLine("2-Handed Axe\tSaber\t\tOrcblade");
            Console.WriteLine("Daggers (NOTE: Dagger Special Attack Not Implemented.)");
            Console.WriteLine("");

            Console.WriteLine("Would you like to find the average win % of a fighter, or fight two together?");
            Console.WriteLine("Type 'average' or 'versus'");
            //determine if user wants average or versus
            string option1 = Console.ReadLine();
            if (option1 == "average")
            {
                //record user's chosen class and weapon
                Console.WriteLine("Please Input the fighter's class:");
                string avgclass = Console.ReadLine();
                Console.WriteLine("Please input the fighter's weapon;");
                string avgweapon = Console.ReadLine();
                Console.WriteLine("Fighter:");
                Console.WriteLine(avgclass);
                Console.WriteLine(avgweapon);
                //find the Initiative, Attack, Parry, and LP of chosen weapon and class combo
                int avgfighterIN = FighterIN(avgclass, avgweapon);
                int avgfighterAT = FighterAT(avgclass, avgweapon);
                int avgfighterPA = FighterPA(avgclass, avgweapon);
                int avgfighterLP = FighterLP(avgclass, avgweapon);
                Console.WriteLine("Initiative: " + avgfighterIN);
                Console.WriteLine("Attack: " + avgfighterAT);
                Console.WriteLine("Parry: " + avgfighterPA);
                Console.WriteLine("Lifepoints: " + avgfighterLP);

                Console.WriteLine("Is this correct? Y/N");
                //check if user inputed it all correctly
                ConsoleKeyInfo avginfo = Console.ReadKey();
                if (avginfo.Key == ConsoleKey.Y)
                {
                    consoleNewLine(1);
                }
                else
                {
                    consoleNewLine(3);
                    goto start;
                }

                consoleNewLine(2);

                //ask for the number of fighting simulations that the user wants to run
                Console.WriteLine("Enter the number of fights you wish to take place (NOTE: 1 means 1 fight against ALL other fighters:");

            avgfightnum:
                string avgfightstring = Console.ReadLine();
                int avgfightnum = 0;
                if (int.TryParse(avgfightstring, out avgfightnum))
                {
                    Console.WriteLine("{0} fights will take place.", avgfightnum);
                }
                else
                {
                    Console.WriteLine("Invalid fight number! Please input another number:");
                    goto avgfightnum;
                }

                //call AvgFight to fight chosen fighter against all other types of fighters
                AvgFight(avgclass, avgweapon, avgfightnum);

                //ask if the user wants to input another class, or quit
                Console.WriteLine("Press R to restart, or any other key to quit");
                ConsoleKeyInfo avginfo2 = Console.ReadKey();
                if (avginfo2.Key == ConsoleKey.R)
                {
                    consoleNewLine(3);
                    goto start;
                }
                else
                {
                    goto end;
                }

            }

            //ask for the user's chosen class and weapon for both fighters
            Console.WriteLine("Please input the first fighter's class:");
            string class1 = Console.ReadLine();
            Console.WriteLine("Please input the first fighter's weapon;");
            string weapon1 = Console.ReadLine();
            Console.WriteLine("Please input the second fighter's class:");
            string class2 = Console.ReadLine();
            Console.WriteLine("Please input the second fighter's weapon;");
            string weapon2 = Console.ReadLine();
            Console.WriteLine("Fighter 1:");
            Console.WriteLine(class1);
            Console.WriteLine(weapon1);
            //determine the stats of the class weapon combo for both fighters
            int fighter1IN = FighterIN(class1, weapon1);
            int fighter1AT = FighterAT(class1, weapon1);
            int fighter1PA = FighterPA(class1, weapon1);
            int fighter1LP = FighterLP(class1, weapon1);
            Console.WriteLine("Initiative: " + fighter1IN);
            Console.WriteLine("Attack: " + fighter1AT);
            Console.WriteLine("Parry: " + fighter1PA);
            Console.WriteLine("Lifepoints: " + fighter1LP);

            Console.WriteLine("Fighter 2:");
            Console.WriteLine(class2);
            Console.WriteLine(weapon2);
            int fighter2IN = FighterIN(class2, weapon2);
            int fighter2AT = FighterAT(class2, weapon2);
            int fighter2PA = FighterPA(class2, weapon2);
            int fighter2LP = FighterLP(class2, weapon2);
            Console.WriteLine("Initiative: " + fighter2IN);
            Console.WriteLine("Attack: " + fighter2AT);
            Console.WriteLine("Parry: " + fighter2PA);
            Console.WriteLine("Lifepoints: " + fighter2LP);

            //Determine if there is bonus AT because of IN deficit
            int difference;
            int ATBonus;
            if (fighter1IN > fighter2IN)
            {
                difference = (fighter1IN - fighter2IN);
                ATBonus = difference / 3;
                Console.WriteLine("Fighter 1 AT Bonus: " + ATBonus);
            }
            else
            {
                difference = (fighter2IN - fighter1IN);
                ATBonus = difference / 3;
                Console.WriteLine("Fighter 2 AT Bonus: " + ATBonus);
            }
            //ask the user if the information is correct
            Console.WriteLine("Is this correct? Y/N");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.Key == ConsoleKey.Y)
            {
                consoleNewLine(1);
            }
            else
            {
                consoleNewLine(3);
                goto start;
            }




        fights:

            consoleNewLine(2);

            //ask for the number of fights that the user wants to simulate
            Console.WriteLine("Enter the number of fights you wish to take place:");

        fightnum:

            string fightstring = Console.ReadLine();
            int fightnum = 0;
            if (int.TryParse(fightstring, out fightnum))
            {
                Console.WriteLine("{0} fights will take place.", fightnum);
            }
            else
            {
                Console.WriteLine("Invalid fight number! Please input another number:");
                goto fightnum;
            }

            int winner1 = 0;
            int winner2 = 0;
            int numfights = 0;

            //Determine who wins the fight
            while (fightnum > 0)
            {
                int winner = 0;
                //calls the Fight Function to simulate a fight
                winner = Fight(fighter1IN, fighter1AT, fighter1PA, fighter1LP, fighter2IN, fighter2AT, fighter2PA, fighter2LP);
                Console.WriteLine("Fighter " + winner + " wins!");
                fightnum = fightnum - 1;
                //if fighter one wins
                if (winner == 1)
                {
                    winner1 = winner1 + 1;
                    Console.WriteLine(winner1);
                }
                //if fighter two wins
                if (winner == 2)
                {
                    winner2 = winner2 + 1;
                    Console.WriteLine(winner2);
                }
                numfights = winner1 + winner2;
            }


            int percentwin = 0;
            int numerator = numfights;
            string overallwinner = "";

            //determine the winning percent of the fighter with the most wins
            if (winner1 > winner2)
            {
                percentwin = (winner1 * 100 / numerator);
                overallwinner = "Fighter 1 won ";
                Console.WriteLine(overallwinner + percentwin + "% of the " + numfights + " fights!");
            }
            else
            {
                if (winner2 > winner1)
                {
                    percentwin = (winner2 * 100 / numerator);
                    overallwinner = "Fighter 2 won ";
                    Console.WriteLine(overallwinner + percentwin + "% of the " + numfights + " fights!");
                }
                else
                {
                    percentwin = 50;
                    overallwinner = "It's a tie! Fighter 1 won ";
                    Console.WriteLine(overallwinner + percentwin + "% of the " + numfights + " fights!");
                }

            }

            //ask the user if they want to fight again, or quit
            Console.WriteLine("Press F to restart, R to reroll fights, or any other key to quit");
            ConsoleKeyInfo info2 = Console.ReadKey();
            if (info2.Key == ConsoleKey.F)
            {
                goto start;
            }
            if (info2.Key == ConsoleKey.R)
            {
                goto fights;
            }

        end:;

        }

        //Fight Function, outputs either 1 if fighter 1 wins or 2 if fighter 2 wins
        static int Fight(int fighter1IN, int fighter1AT, int fighter1PA, int fighter1LP, int fighter2IN, int fighter2AT, int fighter2PA, int fighter2LP)
        {
            int IN1 = fighter1IN;
            int AT1 = fighter1AT;
            int PA1 = fighter1PA;
            int LP1 = fighter1LP;

            int IN2 = fighter2IN;
            int AT2 = fighter2AT;
            int PA2 = fighter2PA;
            int LP2 = fighter2LP;

            //beginning of the fight
            //check who goes first

            //for when daggers special is implemented
            if (weapon1 == "daggers" || weapon2 == "daggers")
            {
                if (IN1 > IN2)
                {
                    while (LP1 > 0 || LP2 > 0)
                    {
                        //fighter 1 goes first
                        int roll = Roll();
                        Console.WriteLine(roll);
                        System.Threading.Thread.Sleep(roll);
                        //check if attack hits
                        if (roll <= AT1 && LP1 > 0 && LP2 > 0)
                        {
                            System.Threading.Thread.Sleep(roll);
                            roll = Roll();
                            Console.WriteLine(roll);
                            //check if parry blocks
                            if (roll >= PA2 && LP1 > 0 && LP2 > 0)
                            {
                                //attack hits. decrease health by 1
                                LP2 = LP2 - 1;
                                if (LP2 == 0)
                                {
                                    return 1;
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(roll);
                        roll = Roll();
                        Console.WriteLine(roll);
                        //check if attack hits,
                        if (roll <= AT2 && LP1 > 0 && LP2 > 0)
                        {
                            System.Threading.Thread.Sleep(roll);
                            roll = Roll();
                            Console.WriteLine(roll);
                            //check if parry blocks
                            if (roll >= PA1 && LP1 > 0 && LP2 > 0)
                            {
                                //attack hits, decrease health by 1
                                LP1 = LP1 - 1;
                                if (LP1 == 0)
                                {
                                    return 2;
                                }
                            }
                        }
                    }

                }
                else
                {
                    while (LP1 > 0 || LP2 > 0)
                    {
                        //fighter 2 goes first

                        int roll = Roll();
                        System.Threading.Thread.Sleep(roll);
                        Console.WriteLine(roll);
                        //check if attack hits
                        if (roll <= AT2 && LP1 > 0 && LP2 > 0)
                        {
                            System.Threading.Thread.Sleep(roll);
                            roll = Roll();
                            Console.WriteLine(roll);
                            //check if parry blocks
                            if (roll >= PA1 && LP1 > 0 && LP2 > 0)
                            {
                                //attack hits, decrease health by 1
                                LP1 = LP1 - 1;
                                if (LP1 == 0)
                                {
                                    return 2;
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(roll);
                        roll = Roll();
                        Console.WriteLine(roll);
                        //check if attack hits
                        if (roll <= AT1 && LP1 > 0 && LP2 > 0)
                        {
                            System.Threading.Thread.Sleep(roll);
                            roll = Roll();
                            Console.WriteLine(roll);
                            //check if parry blocks
                            if (roll >= PA2 && LP1 > 0 && LP2 > 0)
                            {
                                //attack hits, decrease health by 1
                                LP2 = LP2 - 1;
                                if (LP2 == 0)
                                {
                                    return 1;
                                }
                            }
                        }
                    }
                }
            }

            //check if fighter 1 goes first
            if (IN1 > IN2)
            {
                while (LP1 > 0 || LP2 > 0)
                {
                    //fighter 1 goes first
                    int roll = Roll();
                    Console.WriteLine(roll);
                    System.Threading.Thread.Sleep(roll);
                    //check if attack hits
                    if (roll <= AT1 && LP1 > 0 && LP2 > 0)
                    {
                        System.Threading.Thread.Sleep(roll);
                        roll = Roll();
                        Console.WriteLine(roll);
                        //check if parry blocks
                        if (roll >= PA2 && LP1 > 0 && LP2 > 0)
                        {
                            //attack hits, decrease health by 1
                            LP2 = LP2 - 1;
                            if (LP2 == 0)
                            {
                                return 1;
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(roll);
                    roll = Roll();
                    Console.WriteLine(roll);

                    //check if attack hits
                    if (roll <= AT2 && LP1 > 0 && LP2 > 0)
                    {
                        System.Threading.Thread.Sleep(roll);
                        roll = Roll();
                        Console.WriteLine(roll);
                        //check if parry blocks
                        if (roll >= PA1 && LP1 > 0 && LP2 > 0)
                        {
                            //attack hits, decrease health by 1
                            LP1 = LP1 - 1;
                            if (LP1 == 0)
                            {
                                return 2;
                            }
                        }
                    }
                }

            }
            else
            {
                while (LP1 > 0 || LP2 > 0)
                {
                    //fighter 2 goes first

                    int roll = Roll();
                    System.Threading.Thread.Sleep(roll);
                    Console.WriteLine(roll);
                    //check if attack hits
                    if (roll <= AT2 && LP1 > 0 && LP2 > 0)
                    {
                        System.Threading.Thread.Sleep(roll);
                        roll = Roll();
                        Console.WriteLine(roll);
                        //check if parry blocks
                        if (roll >= PA1 && LP1 > 0 && LP2 > 0)
                        {
                            //attack hits, decrease health by 1
                            LP1 = LP1 - 1;
                            if (LP1 == 0)
                            {
                                return 2;
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(roll);
                    roll = Roll();
                    Console.WriteLine(roll);
                    //check if attack hits
                    if (roll <= AT1 && LP1 > 0 && LP2 > 0)
                    {
                        System.Threading.Thread.Sleep(roll);
                        roll = Roll();
                        Console.WriteLine(roll);
                        //check if parry blocks
                        if (roll >= PA2 && LP1 > 0 && LP2 > 0)
                        {
                            //attack hits, decrease health by 1
                            LP2 = LP2 - 1;
                            if (LP2 == 0)
                            {
                                return 1;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        //Function to roll a number from 1 to 20, used for attacks and for sleep function
        static int Roll()
        {
            Random rnd = new Random();
            int roll = rnd.Next(1, 21);
            return roll;
        }

        //Fight function for when user wants to find the average win percent against all other fighters
        static string AvgFight(string fighterClass, string weapon, int numFights)
        {

            //create an array with a columb of classes, and 10 rows of weapons
            string[] classes = new string[10] { "gladiator", "warrior", "knight", "barbarian", "dwarf", "elf", "orc", "thief", "pirate", "mercenary" };
            string[] weapons = new string[11] { "", "spear", "sword & shield", "mace", "2-handed sword", "axe", "bow", "2-handed axe", "saber", "orcblade", "daggers" };
            string[,] fighters = new string[10, 11];
            int c = 0;
            int w = 1;
            int avgFightWins = 0;
            int avgNumFights = 0;

            //runs the for function for the indicated number of fights, each number equals 1 fight against all 100 combinations
            for (int numberOfFights = numFights; numberOfFights > 0; numberOfFights--)
            {
                //for each class, run the for statement
                for (c = 0; c < 10; c++)
                {
                    //for each weapon, run the for statement
                    for (w = 1; w < 11; w++)
                    {
                        //for each class and weapon, assign them to the 2 dimensional array
                        fighters[c, w] = weapons[w];
                    }
                    //reassign the first column of the 2d array to the list of the classes
                    fighters[c, 0] = classes[c];
                }

                for (c = 0; c < 10; c++)
                {
                    for (w = 1; w < 11; w++)
                    {
                        //assign stats to the opponent based on their class and weapon
                        int avgFighterIN = FighterIN(fighters[c, 0], fighters[c, w]);
                        int avgFighterAT = FighterAT(fighters[c, 0], fighters[c, w]);
                        int avgFighterPA = FighterPA(fighters[c, 0], fighters[c, w]);
                        int avgFighterLP = FighterLP(fighters[c, 0], fighters[c, w]);

                        //assign stats to the choosen fighter
                        int choosenFighterIN = FighterIN(fighterClass, weapon);
                        int choosenFighterAT = FighterAT(fighterClass, weapon);
                        int choosenFighterPA = FighterPA(fighterClass, weapon);
                        int choosenFighterLP = FighterLP(fighterClass, weapon);

                        //get the winner of the fight between the opponent and choosen fighter
                        int AvgFightWinner = Fight(choosenFighterIN, choosenFighterAT, choosenFighterPA, choosenFighterLP, avgFighterIN, avgFighterAT, avgFighterPA, avgFighterLP);
                        if (AvgFightWinner == 1)
                        {
                            //print if choosen fighter won
                            Console.WriteLine("Choosen fighter BEAT a " + classes[c] + " wielding a " + weapons[w] + "!");
                            //add 1 to win record, add 1 to number of fights; used to determine win percentage
                            avgFightWins = avgFightWins + 1;
                            avgNumFights = avgNumFights + 1;
                        }
                        else
                        {
                            //print if choosen fighter lost
                            Console.WriteLine("Choosen fighter LOST to a " + classes[c] + " wielding a " + weapons[w] + "!");
                            avgNumFights = avgNumFights + 1;
                        }
                    }

                }
            }

            //determine win percentage
            int avgWinPercent = 100 * avgFightWins / avgNumFights;
            Console.WriteLine("Your " + fighterClass + " wielding a " + weapon + " beat " + avgWinPercent + "% of opponents, out of a total " + avgNumFights + " fights!");
            return "";
        }

        //function to assign Initiative value based on class and weapon
        static int FighterIN(string fighterClass, string weapon)
        {
            int fighterIN = 0;

            fighterClass = fighterClass.ToLowerInvariant();

            //assign values based on inputed class
            switch (fighterClass)
            {
                case "gladiator":
                    fighterIN = 9;
                    break;

                case "warrior":
                    fighterIN = 9;
                    break;

                case "knight":
                    fighterIN = 9;
                    break;

                case "barbarian":
                    fighterIN = 7;
                    break;

                case "dwarf":
                    fighterIN = 7;
                    break;

                case "elf":
                    fighterIN = 11;
                    break;

                case "orc":
                    fighterIN = 7;
                    break;

                case "theif":
                    fighterIN = 11;
                    break;

                case "pirate":
                    fighterIN = 9;
                    break;

                case "mercenary":
                    fighterIN = 9;
                    break;
            }

            //assign values based on weapon
            weapon = weapon.ToLowerInvariant();

            switch (weapon)
            {
                case "spear":
                    fighterIN += 5;
                    break;

                case "sword & shield":
                    fighterIN -= 2;
                    break;

                case "mace":
                    fighterIN -= 1;
                    break;

                case "2-handed sword":
                    fighterIN += 2;
                    break;

                case "axe":
                    fighterIN -= 1;
                    break;

                case "bow":
                    fighterIN += 5;
                    break;

                case "2-handed axe":
                    fighterIN += 2;
                    break;

                case "saber":
                    fighterIN += 2;
                    break;

                case "orcblade":
                    fighterIN -= 4;
                    break;

                case "daggers":
                    fighterIN -= 3;
                    break;
            }

            //return the assigned value of Initiative
            return fighterIN;
        }

        //function to assign Attack value based on class and weapon
        static int FighterAT(string fighterClass, string weapon)
        {
            int fighterAT = 0;

            fighterClass = fighterClass.ToLowerInvariant();

            switch (fighterClass)
            {
                case "gladiator":
                    fighterAT = 10;
                    break;

                case "warrior":
                    fighterAT = 8;
                    break;

                case "knight":
                    fighterAT = 7;
                    break;

                case "barbarian":
                    fighterAT = 12;
                    break;

                case "dwarf":
                    fighterAT = 8;
                    break;

                case "elf":
                    fighterAT = 9;
                    break;

                case "orc":
                    fighterAT = 10;
                    break;

                case "theif":
                    fighterAT = 8;
                    break;

                case "pirate":
                    fighterAT = 9;
                    break;

                case "mercenary":
                    fighterAT = 11;
                    break;
            }

            weapon = weapon.ToLowerInvariant();

            switch (weapon)
            {

                case "mace":
                    fighterAT += 2;
                    break;

                case "2-handed sword":
                    fighterAT += 2;
                    break;

                case "axe":
                    fighterAT += 1;
                    break;

                case "bow":
                    fighterAT += 2;
                    break;

                case "2-handed axe":
                    fighterAT += 3;
                    break;

                case "orcblade":
                    fighterAT += 3;
                    break;

                case "daggers":
                    fighterAT -= 1;
                    break;
            }

            return fighterAT;
        }

        //function to assign Parry value based on class and weapon
        static int FighterPA(string fighterClass, string weapon)
        {
            int fighterPA = 0;

            fighterClass = fighterClass.ToLowerInvariant();

            switch (fighterClass)
            {
                case "gladiator":
                    fighterPA = 8;
                    break;

                case "warrior":
                    fighterPA = 10;
                    break;

                case "knight":
                    fighterPA = 11;
                    break;

                case "barbarian":
                    fighterPA = 7;
                    break;

                case "dwarf":
                    fighterPA = 11;
                    break;

                case "elf":
                    fighterPA = 8;
                    break;

                case "orc":
                    fighterPA = 9;
                    break;

                case "theif":
                    fighterPA = 9;
                    break;

                case "pirate":
                    fighterPA = 9;
                    break;

                case "mercenary":
                    fighterPA = 7;
                    break;
            }

            weapon = weapon.ToLowerInvariant();

            switch (weapon)
            {

                case "sword & shield":
                    fighterPA += 2;
                    break;

                case "2-handed sword":
                    fighterPA -= 1;
                    break;

                case "axe":
                    fighterPA -= 2;
                    break;

                case "bow":
                    fighterPA -= 2;
                    break;

                case "2-handed axe":
                    fighterPA -= 2;
                    break;

                case "saber":
                    fighterPA += 1;
                    break;

                case "daggers":
                    fighterPA += 2;
                    break;
            }
            return fighterPA;
        }

        //function to assign Lifepoints value based on class and weapon
        static int FighterLP(string fighterClass, string weapon)
        {
            int fighterLP = 3;

            fighterClass = fighterClass.ToLowerInvariant();

            switch (fighterClass)
            {
                case "gladiator":
                    fighterLP = 3;
                    break;

                case "warrior":
                    fighterLP = 3;
                    break;

                case "knight":
                    fighterLP = 3;
                    break;

                case "barbarian":
                    fighterLP = 3;
                    break;

                case "dwarf":
                    fighterLP = 3;
                    break;

                case "elf":
                    fighterLP = 3;
                    break;

                case "orc":
                    fighterLP = 3;
                    break;

                case "theif":
                    fighterLP = 3;
                    break;

                case "pirate":
                    fighterLP = 3;
                    break;

                case "mercenary":
                    fighterLP = 3;
                    break;
            }

            return fighterLP;
        }

        static void consoleNewLine(int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                Console.Write("\n");
            }
        }
    }
}







