using System.Collections.Generic;  
using System.Data;
using System.Threading.Tasks.Dataflow;

public class GeneticAlgo
{

    public void runGA(int generations, List<double> data)
    {
        //Create initial population
        Population myPop = new Population(5, data);

        //Sort the population based on their fitness values
        myPop.sortPopulation();
       
        // Console.WriteLine("==Parents==");
        // pop.getParents()[0].printIndividual();
        // pop.getParents()[1].printIndividual();

        //For results
        double [,] results = new double [generations,6]; //col-0:generation, col-1:p1, col-2:p2, col-3:c1, col-4:c2, col-5:mutant
        List<List<int>> solutions = new List<List<int>>();

        double crossOverProb = 0.9;
        double coRate = 0.9/generations;

        for(int i=0; i<generations; i++)
        {
            Console.WriteLine("Generation "+i);
           
            //Write values to results for generation number, fitness for parents, and the solution (the chromosome)
            results[i,0]=i;
            results[i,1]=myPop.pop[0].fitness;
            results[i,2]=myPop.pop[1].fitness;
            solutions.Add(myPop.pop[0].chromosome);

            //Perform mutation towards the best parent
            List<Individual> CO_candidates = myPop.crossOver(0.5);
            Console.WriteLine("==Cross Over Candidates==");
            CO_candidates[0].printIndividual();
            CO_candidates[1].printIndividual();

            //Mutant
            Individual mutant = myPop.getParents()[0].copyIndividual();
            mutant.mutation(0.2);
            // Console.WriteLine("==Mutant==");
            // mutant.printIndividual();

            //Add the new candidates to the population
            myPop.addCandidates(CO_candidates[0], CO_candidates[1], mutant);

            results[i,3]=CO_candidates[0].fitness;
            results[i,4]=CO_candidates[1].fitness;
            results[i,5]=mutant.fitness;

            //Sort the population after the new candidates join the population
            myPop.sortPopulation();
            

            //Get the best individual from the population
            Individual best = myPop.getBestIndividual();
            Console.WriteLine("Crossover Prob: "+crossOverProb);

            //Reduce the crossover probability
            crossOverProb -= coRate;

            // Console.WriteLine("==Population==");
            // pop.printPopulation();
            // Console.WriteLine("==Best Candidate==");
            // best.printIndividual();
        }
        // Console.WriteLine("==Population All Generations==");
        // myPop.printPopulation();

        //Write results to csv files: writeFitnessResults, writeSolutions, writePopulation
        FileData.writeFitnessResults(results,"results.csv");
        FileData.writeSolutions(solutions,"solutions.csv");
        FileData.writePopulation(myPop,"Population.csv");

    }
}
public class Individual
{
    public List<int> chromosome;
    public double fitness;
    public List<Double> data = new List<double>();

    public Individual(List<Double> dataset)
    {
        Random r = new Random();
        data = dataset;
        chromosome = new List<int>();
        for (int i=0; i<dataset.Count; i++)
        {
            int gene = r.Next(0,2);
            chromosome.Add(gene);
        }
        calCurrentFit();
    }

    public void calCurrentFit()
    {
        fitness = 0;
        double left=0, right=0;
        for(int i=0; i<chromosome.Count; i++)
        {
            if(chromosome[i]==0){
                left+=data[i];
                // Console.WriteLine("Left "+currenSol[i]);
            }else{
                right+=data[i];
                // Console.WriteLine("Right "+currenSol[i]);
            }
        }
        fitness = Math.Round(Math.Abs(left-right),2);
    }

    public double getFitness()
    {
        calCurrentFit();
        return fitness;
    }

    //This method is to avoid from the object gets the reference.
    public Individual copyIndividual()
    {
        Individual other = (Individual)this.MemberwiseClone();  
        other.chromosome = new (chromosome);  
        other.fitness = fitness;  
        return other;  
    }

    public void mutation (double prob)
    {
        Random r = new Random();
       
        if (prob>1.0)
        {
            Console.WriteLine("Rate should be between 0 and 1");
            return;
        }
        else
        {
            int mutate = (int)(prob*chromosome.Count);
            // Console.WriteLine("mutate "+mutate);
            for(int i=0; i<=mutate; i++)
            {
                int ind = r.Next(chromosome.Count);
                if(chromosome[ind]==0)
                {
                    chromosome[ind] = 1;
                }
                else
                {
                    chromosome[ind] = 0;
                }
            } 
        }
        calCurrentFit();
    }

    public void printIndividual()
    {
        for (int i=0; i<chromosome.Count; i++)
        {
            Console.Write(chromosome[i]);
            if (i<chromosome.Count-1)
                Console.Write(",");
        }
        Console.WriteLine("  "+fitness);
    }
}

public class Population
{
    public List<Individual> pop = new List<Individual>();

    public Population(int n, List<double> data)
    {
        for(int i=0; i<n; i++)
        {
            Individual ind = new Individual(data);
            pop.Add(ind);
        }
        sortPopulation();
    }


    public List<Individual> getParents ()
    {
        List<Individual> parents = new List<Individual>();
        for (int i=0; i<2; i++)
        {
            parents.Add(pop[i]); //We pick the first 2 individual from pop
        }
        return parents;
    }

    public void printParents()
    {
        for (int i=0; i<2; i++)
        {
            pop[i].printIndividual();  //We pick the first 2 individual from pop
        }
    }
    public void addCandidates(Individual c1, Individual c2, Individual mutant)
    {
        pop.Add(c1);
        pop.Add(c2);
        pop.Add(mutant);
    }

    public void sortPopulation()
    {
        List<Individual> res = pop.OrderBy(o=>o.fitness).ToList();
        pop = res.ToList();
    }

    public Individual getBestIndividual()
    {
        Individual best = pop[0];
        return best;
    }

    public List<Individual> crossOver (double coProb)
    {
        Individual newCandidates1 = pop[0].copyIndividual();
        Individual newCandidates2 = pop[1].copyIndividual();
        List<Individual> res = new List<Individual>();

        int coPoint = (int)(coProb*pop[0].chromosome.Count);

        for (int i=coPoint; i<pop[0].chromosome.Count; i++)
        {
            newCandidates1.chromosome[i] = pop[1].chromosome[i];
        }
        newCandidates1.calCurrentFit();

    
        for (int i=coPoint; i<pop[1].chromosome.Count; i++)
        {
            newCandidates2.chromosome[i] = pop[0].chromosome[i];
        }
        newCandidates2.calCurrentFit();

        res.Add(newCandidates1);
        res.Add(newCandidates2);
        return res;
    }

    public void printPopulation()
    {
        for (int i=0; i<pop.Count; i++)
        {
            pop[i].printIndividual();
        }
    }

}


// List<Order> SortedList = objListOrder.OrderBy(o=>o.OrderDate).ToList();