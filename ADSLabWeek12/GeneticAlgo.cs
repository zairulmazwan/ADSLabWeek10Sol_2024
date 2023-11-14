using System.Collections.Generic;  
using System.Data;  

public class GeneticAlgo
{

    public void runGA(int generations, List<double> data)
    {
        //Create initial population
        Population pop = new Population(5, data);

        for(int i=0; i<generations; i++)
        {
            Console.WriteLine("Generation "+i);
             //Sort the population based on their fitness values
            pop.sortPopulation();

            //Pick the parents for reproduction - cross over
            pop.identifyParents();
            // Console.WriteLine("==Parents==");
            // pop.getParents()[0].printIndividual();
            // pop.getParents()[1].printIndividual();

            //Reproduction - cross over
            Individual c1 = new Individual(data);
            Individual c2 = new Individual(data);

            //Mutatant
            Individual mutant = pop.getParents()[0].deepCopy();
            mutant.mutation(0.5);
            // Console.WriteLine("==Mutant==");
            // mutant.printIndividual();

            //Perform mutation towards the best parent
            List<Individual> CO_candidates = pop.crossOver(0.5, pop.getParents());
            // Console.WriteLine("==Cross Over Candidates==");
            // CO_candidates[0].printIndividual();
            // CO_candidates[1].printIndividual();

            //Add the new candidates to the population
            pop.addCandidates(CO_candidates[0], CO_candidates[1], mutant);

            //Sort the population after the new candidates join the population
            pop.sortPopulation();

            //Get the best individual from the population
            Individual best = pop.getBestIndividual();

            // Console.WriteLine("==Population==");
            // pop.printPopulation();
            Console.WriteLine("==Best Candidate==");
            best.printIndividual();
        }
        Console.WriteLine("==Population All Generations==");
        pop.printPopulation();

       
    }
}
public class Individual
{
    public List<int> individual;
    public double fitness;
    public List<Double> data = new List<double>();

    public Individual(List<Double> dataset)
    {
        Random r = new Random();
        data = dataset;
        individual = new List<int>();
        for (int i=0; i<dataset.Count; i++)
        {
            int gene = r.Next(0,2);
            individual.Add(gene);
        }
        calCurrentFit();
    }

    public void calCurrentFit()
    {
        fitness = 0;
        double left=0, right=0;
        for(int i=0; i<individual.Count; i++)
        {
            if(individual[i]==0){
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
    public Individual deepCopy()
    {
        Individual other = (Individual)this.MemberwiseClone();  
        other.individual = new (individual);  
        other.fitness = fitness;  
        return other;  
    }

    // public List<int> copyIndividual()
    // {
    //     List<int> res = new List<int>();
    //     res = individual.ToList();
    //     calCurrentFit();
    //     return res;
    // }

    public void mutation (double rate)
    {
        Random r = new Random();
       
        if (rate>1.0)
        {
            Console.WriteLine("Rate should be between 0 and 1");
            return;
        }
        else
        {
            int mutate = (int)(rate*individual.Count);
            // Console.WriteLine("mutate "+mutate);
            for(int i=0; i<=mutate; i++)
            {
                int ind = r.Next(individual.Count);
                if(individual[ind]==0)
                {
                    individual[ind] = 1;
                }
                else
                {
                    individual[ind] = 0;
                }
            } 
        }
        calCurrentFit();
    }

    public void printIndividual()
    {
        for (int i=0; i<individual.Count; i++)
        {
            Console.Write(individual[i]);
            if (i<individual.Count-1)
                Console.Write(",");
        }
        Console.WriteLine("  "+fitness);
    }
}

public class Population
{
    List<Individual> pop = new List<Individual>();
    List<Individual> parents = new List<Individual>();

    public Population(int n, List<double> data)
    {
        for(int i=0; i<n; i++)
        {
            Individual ind = new Individual(data);
            pop.Add(ind);
        }
        sortPopulation();
        identifyParents();
    
    }

    public void identifyParents()
    {
        for (int i=0; i<2; i++)
        {
            parents.Add(pop[i]);
        }
    }

    public List<Individual> getParents ()
    {
        return parents;
    }

    public void printParents()
    {
        for (int i=0; i<parents.Count; i++)
        {
            parents[i].printIndividual();
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

    public List<Individual> crossOver (double coRate, List<Individual> parents)
    {
        Individual newCandidates1 = parents[0].deepCopy();
        Individual newCandidates2 = parents[1].deepCopy();
        List<Individual> res = new List<Individual>();

        int coPoint = (int)(coRate*parents[0].individual.Count);

        for (int i=coPoint; i<parents[0].individual.Count; i++)
        {
            newCandidates1.individual[i] = parents[1].individual[i];
        }
        newCandidates1.calCurrentFit();

    
        for (int i=coPoint; i<parents[1].individual.Count; i++)
        {
            newCandidates2.individual[i] = parents[0].individual[i];
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