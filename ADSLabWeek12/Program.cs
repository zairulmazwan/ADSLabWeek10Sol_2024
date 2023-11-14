// See https://aka.ms/new-console-template for more information

List<double> data = FileData.readData("data.csv");
// Individual id = new Individual(data);

// id.printIndividual();


// Population pop = new Population(5, data);
// pop.printPopulation();
// Console.WriteLine("Parents: ");
// pop.printParents();

// Console.WriteLine("After CO: ");
// List<Individual> n = pop.crossOver(0.5, pop.getParents());
// n[0].printIndividual();
// Console.WriteLine();
// n[1].printIndividual();

// Console.WriteLine("Check parents: ");
// pop.printParents();

// Individual n1 = new Individual(data);
// Individual n2 = new Individual(data);
// Console.WriteLine("New candidates : ");
// n1.printIndividual();
// n2.printIndividual();

// pop.addCandidates(n1, n2);

// Console.WriteLine("After sorting: ");
// pop.sortPopulation();
// pop.printPopulation();

// n1.mutation(0.5);
// n1.printIndividual();

GeneticAlgo ga = new GeneticAlgo();
ga.runGA(10, data);
