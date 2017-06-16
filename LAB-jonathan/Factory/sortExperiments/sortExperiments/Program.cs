using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            ExperimentCreator completeExperimentFactory = new CompleteExperimentCreator();
            ExperimentCreator bubbleExperFact = new BubbleExperimentCreator();

            IExperiment completeExper = completeExperimentFactory.FactoryMethode();
            IExperiment bubbleExper = bubbleExperFact.FactoryMethode();

            completeExper.start();

            bubbleExper.start();

            Console.ReadLine();

        }
    }
}
