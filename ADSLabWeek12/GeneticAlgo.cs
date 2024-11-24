using System.Data;

public class GeneticAlgo
{
    public void runGA(int generations, List<double> data)
    {
        //Create initial population
        Population myPop = new Population(20, data);

        //Sort the population based on their fitness values
        myPop.sortPopulation();
       
        // Console.WriteLine("==Parents==");
        // pop.getParents()[0].printIndividual();
        // pop.getParents()[1].printIndividual();

        //For results
        double [,] results = new double [generations,6]; //col-0:generation, col-1:p1, col-2:p2, col-3:c1, col-4:c2, col-5:mutant
        List<List<int>> solutions = new List<List<int>>();

        //We are going to use this crossover strategy. The crossover prob. is reducing by generation
        double crossOverProb = 0.9;
        double coRate = 0.9/generations;

        //Loop until number of generation
        for(int i=0; i<generations; i++)
        {
            Console.WriteLine("Generation "+i);
           
            //Write values to results for generation number, fitness for parents, and the solutions (the chromosome)
            results[i,0]=i;
            results[i,1]=myPop.pop[0].fitness;
            results[i,2]=myPop.pop[1].fitness;
            solutions.Add(myPop.pop[0].chromosome);

            //Produce 2 children using the crossOver method. The method will return a list of Individuals
            List<Individual> CO_candidates = myPop.crossOver(0.5);
            //Console.WriteLine("==Cross Over Candidates==");
            //CO_candidates[0].printIndividual();
            //CO_candidates[1].printIndividual();

            //Produce a mutant. Create an Individual from the best parent, the mutate their genes. Use copyIndividual and mutation for this operation.
            Individual mutant = myPop.getParents()[0].copyIndividual();
            mutant.mutation(0.2);
            // Console.WriteLine("==Mutant==");
            // mutant.printIndividual();

            //Add the new candidates to the population, i.e., child 1, child 2, and mutant. Use addCandidates method.
            myPop.addCandidates(CO_candidates[0], CO_candidates[1], mutant);

            results[i,3]=CO_candidates[0].fitness;
            results[i,4]=CO_candidates[1].fitness;
            results[i,5]=mutant.fitness;

            //Sort the population after the new candidates join the population
            myPop.sortPopulation();
            

            //Get the best individual from the population
            Individual best = myPop.getBestIndividual();
            //Console.WriteLine("Crossover Prob: "+crossOverProb);

            //Reduce the crossover probability
            crossOverProb -= coRate;

            // Console.WriteLine("==Population==");
            // pop.printPopulation();
            // Console.WriteLine("==Best Candidate==");
            // best.printIndividual();
        }
        // Console.WriteLine("==Population All Generations==");
        Console.WriteLine(myPop.getParents()[0].fitness);
        //Write results to csv files: writeFitnessResults, writeSolutions, writePopulation
        FileData.writeFitnessResults(results,"results.csv");
        FileData.writeSolutions(solutions,"solutions.csv");
        FileData.writePopulation(myPop,"Population.csv");

    }
}

//©ZairulMazwan©