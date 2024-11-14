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
            int gene = r.Next(1,9999)%2;
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
            Console.WriteLine("Probability should be between 0 and 1");
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
        Individual newCandidates1 = pop[0].copyIndividual(); //getting the genes from parent 1
        Individual newCandidates2 = pop[1].copyIndividual(); //getting the genes from parent 2
        List<Individual> res = new List<Individual>(); //We need this to store 2 children (of class Individual)

        int coPoint = (int)(coProb*pop[0].chromosome.Count);

        for (int i=coPoint; i<pop[0].chromosome.Count; i++)
        {
            newCandidates1.chromosome[i] = pop[1].chromosome[i]; //Pass some genes from parent 2
        }
        newCandidates1.calCurrentFit();

    
        for (int i=coPoint; i<pop[1].chromosome.Count; i++)
        {
            newCandidates2.chromosome[i] = pop[0].chromosome[i]; //Pass some genes from parent 1
        }
        newCandidates2.calCurrentFit();

        res.Add(newCandidates1);
        res.Add(newCandidates2);
        return res; //There are 2 individuals in this variable
    }

    public void printPopulation()
    {
        for (int i=0; i<pop.Count; i++)
        {
            pop[i].printIndividual();
        }
    }
}
