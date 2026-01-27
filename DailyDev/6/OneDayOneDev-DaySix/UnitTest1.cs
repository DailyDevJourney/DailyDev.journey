

using OneDayOneDev_DayFive;

namespace OneDayOneDev_DaySix
{
    public class UnitTest1
    {
        [Fact]
        public void GetNewId_Return_1_When_List_is_empty()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(new List<TaskItem>());
            //Act => j'apelle la méthode à tester
            var id = service.GetNewId();
            //Assert => Je vérifie le résultat
            Assert.Equal(1, id);
        }

        [Fact]
        public void GetLateTask_Return_When_dueDate_is_in_past_and_not_completed()
        {
            var tasks = new List<TaskItem> { 
                new TaskItem(1,"Past",DateTime.Today,DateTime.Today.AddDays(-1),false),
                new TaskItem(2,"Future",DateTime.Today,DateTime.Today.AddDays(1),false),
                };
            var service = new TaskService(tasks);

            var late = service.GetLateTask();

            Assert.Single(late);
            Assert.Equal(1, late[0].id);
        }


     }
        

}