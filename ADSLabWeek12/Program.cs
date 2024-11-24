// See https://aka.ms/new-console-template for more information

List<double> data = ReadWriteFile.readData("dataset.csv"); //Read the dataset
// Individual id = new Individual(data); //An example to produce an individual
// id.printIndividual();


// Population pop = new Population(5, data); //Produce a population of size 5
// Console.WriteLine("Let's see the population: ");
// pop.printPopulation();

// Console.WriteLine("\n\nParents: ");
// pop.printParents();

// Console.WriteLine("\n\nAfter Crossover (produce 2 children): ");
// List<Individual> newChildren = pop.crossOver(0.5);
// newChildren[0].printIndividual();
// newChildren[1].printIndividual();


// Console.WriteLine("\nCheck parents again using another method: ");
// List<Individual> parents = pop.getParents();
// parents[0].printIndividual();
// parents[1].printIndividual();
// //You can also use the method pop.printParents()


// // Produce a mutant
// Individual mutant = pop.getParents()[0].copyIndividual();
// mutant.mutation(0.2);
// Console.WriteLine("\n\nThe mutant is: ");
// mutant.printIndividual();

// Console.WriteLine("\n\nAdding the new children and mutant to join the population");
// pop.addCandidates(newChildren[0], newChildren[1], mutant);

// Console.WriteLine("\n\nAfter sorting: ");
// pop.sortPopulation();
// pop.printPopulation();


GeneticAlgo ga = new GeneticAlgo();
ga.runGA(30, data);

//©ZairulMazwan©
