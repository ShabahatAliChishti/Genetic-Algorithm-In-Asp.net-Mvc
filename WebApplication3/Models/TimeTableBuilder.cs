using TimeTable.Builder;
using TimeTableServer.Context;

class Program
    {
        static void Main(string[] args)
        {

            using (var dataContext = new DataContext())
            {
                Population population = new Population(1000, new TimeTableChromosome(dataContext),
                    new TimeTableChromosome.FitnessFunction(), new EliteSelection());

                int i = 0;
                while (true)
                {
                    population.RunEpoch();
                    i++;
                    if (population.FitnessMax >= 0.99 || i >= 1000)
                    {
                        break;
                    }
                }

                var timetable = (population.BestChromosome as TimeTableChromosome).Value.Select(chromosome =>
                    new TimeSlot()
                    {
                        CourseId = chromosome.CourseId,
                        PlaceId = chromosome.PlaceId,
                        Day = (DayOfWeek) chromosome.Day,
                        Start = chromosome.StartAt, End = chromosome.EndAt,
                        Id = Guid.NewGuid().ToString()
                    }
                ).ToList();
                dataContext.TimeSlots.AddRange(timetable);
                dataContext.SaveChanges();
            }


        }
    }