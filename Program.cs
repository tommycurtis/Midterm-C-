using System;
using System.Collections.Generic;

namespace WorkoutLogger
{
    // Pubic Class workout entry
    public class WorkoutEntry
    {
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }   // in lbs or kg it should do both
    }

    class Program
    {
        static List<WorkoutEntry> workoutLog = new List<WorkoutEntry>();  // simple memory 

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Workout Logger!");

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();  // clean screen each time
                Console.WriteLine("=== Workout Logger Menu ===");
                Console.WriteLine("1. Log a new exercise");
                Console.WriteLine("2. View all logged exercises");
                Console.WriteLine("3. Calculate total volume");
                Console.WriteLine("4. Exit");
                Console.Write("\nPick an option (1-4): ");

                string choiceInput = Console.ReadLine();
                if (int.TryParse(choiceInput, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            LogNewExercise();
                            break;
                        case 2:
                            ViewLoggedExercises();
                            break;
                        case 3:
                            ShowTotalVolume();
                            break;
                        case 4:
                            keepRunning = false;
                            Console.WriteLine("Thanks for using the Workout Logger! Goodbye.");
                            break;
                        default:
                            Console.WriteLine("That wasn't a valid option. Try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please type a number 1-4.");
                }

                if (keepRunning)
                {
                    Console.WriteLine("\nPress any key to go back to the menu...");
                    Console.ReadKey();
                }
            }
        }

        // Add one excercise as a time
        static void LogNewExercise()
        {
            Console.Write("\nEnter exercise name (like Bench Press): ");
            string name = Console.ReadLine();

            Console.Write("How many sets? ");
            if (!int.TryParse(Console.ReadLine(), out int sets) || sets < 1)
            {
                Console.WriteLine("Sets must be a positive number.");
                return;
            }

            Console.Write("How many reps per set? ");
            if (!int.TryParse(Console.ReadLine(), out int reps) || reps < 1)
            {
                Console.WriteLine("Reps must be a positive number.");
                return;
            }

            Console.Write("Weight used (lbs or kg)? ");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
            {
                Console.WriteLine("Weight must be a positive number.");
                return;
            }

            // Create the entry and add it
            WorkoutEntry newEntry = new WorkoutEntry
            {
                ExerciseName = name,
                Sets = sets,
                Reps = reps,
                Weight = weight
            };

            workoutLog.Add(newEntry);
            Console.WriteLine($"\nLogged: {sets} sets of {reps} reps @ {weight} for {name}");
        }

        // Show everything that's been logged
        static void ViewLoggedExercises()
        {
            if (workoutLog.Count == 0)
            {
                Console.WriteLine("No exercises logged yet.");
                return;
            }

            Console.WriteLine("\n=== Your Logged Workouts ===");
            for (int i = 0; i < workoutLog.Count; i++)
            {
                WorkoutEntry e = workoutLog[i];
                double exerciseVolume = e.Sets * e.Reps * e.Weight;
                Console.WriteLine($"{i + 1}. {e.ExerciseName} - {e.Sets} sets x {e.Reps} reps @ {e.Weight} lbs/kg");
                Console.WriteLine($"   Volume for this exercise: {exerciseVolume}");
            }
        }

        // Calculate and show total volume
        static void ShowTotalVolume()
        {
            if (workoutLog.Count == 0)
            {
                Console.WriteLine("No exercises logged yet. Total volume = 0");
                return;
            }

            double totalVolume = 0;

            foreach (WorkoutEntry e in workoutLog)
            {
                totalVolume += e.Sets * e.Reps * e.Weight;   // standard volume formula
            }

            Console.WriteLine($"\nTotal workout volume: {totalVolume} (lbs or kg lifted)");
            Console.WriteLine("(This is all exercises added together)");
        }
    }
}