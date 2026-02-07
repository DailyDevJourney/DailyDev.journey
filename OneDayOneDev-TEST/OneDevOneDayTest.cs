

using System.Threading.Tasks;

using OneDayOneDev;
using OneDayOneDev_DayThirteen;

namespace OneDayOneDev_Test
{
    public class OneDevOneDayTest
    {
        FakeDateTimeProvider _Datetime = new FakeDateTimeProvider { Today = DateTime.Today };

        #region TASKSERVICE
        [Fact]
        public void CreateNewTask_With_Empty_Title()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("",_Datetime.Today.ToString("dd/MM/yyyy"));
            //Assert => Je vérifie le résultat
            Assert.False(result.succes);
        }
        [Fact]
        public void CreateNewTask_With_Incorrect_Date()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test","99/99/999");
            //Assert => Je vérifie le résultat
            Assert.False(result.succes);
        }

        [Fact]
        public void CreateNewTask_OK()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            //Assert => Je vérifie le résultat
            Assert.True(result.succes);
            Assert.Single(service.GetTaskList());
        }

        [Fact]
        public void SetTaskCompleted_OK()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            Assert.True(result.succes);
            var task = Assert.Single(service.GetTaskByTitle("test"));
            result = service.SetTaskCompleted(identifiant: task.id);
            //Assert => Je vérifie le résultat
            Assert.True(result.succes);

            Assert.True(service.GetTaskById(task.id)!.Iscompleted);

        }


        [Fact]
        public void GetLateList_When_Date_Is_Before_Today()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", "01/01/1990");
            Assert.True(result.succes);
            Assert.Single(service.GetLateTask());
            
            
        }
        [Fact]
        public void DeleteTask_That_Is_Completed_Error()
        {
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            Assert.True(result.succes);
            var task = Assert.Single(service.GetTaskByTitle("test"));
            result = service.SetTaskCompleted(task.id);
            Assert.True(result.succes);
            result = service.DeleteTask(task.id);
            Assert.False(result.succes);

        }

        [Fact]
        public void GetNewID()
        {
            
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);

            //Act => j'apelle la méthode à tester
            var id = 0;

            for(int i = 0; i < 10; i++)
            {
                id = service.GetNewId();
                var result = service.CreateNewTask($"test{i}", null);
                Assert.True(result.succes);
            }

            for (int i = 3; i < 7; i++)
            {

                var result = service.DeleteTask(i);
                Assert.True(result.succes);
            }
            id = service.GetNewId();
            //Assert
            Assert.Equal(11, id);

            
        }

        #endregion

        #region TASKRULES
        [Fact]
        public void IsTaskLate_Past()
        {
          
            //Arrange => prépare le contexte du test
            var rules = new TaskRules();

            var taskitem = new TaskItem( 1,"test", DateTime.Today, new DateTime(2020, 1, 1));

            //Act => j'apelle la méthode à tester
            var result = rules.IsTaskLate(taskitem, DateTime.Today);
            Assert.True(result); 
        }

        [Fact]
        public void IsTaskLate_Today()
        {

            //Arrange => prépare le contexte du test
            var rules = new TaskRules();

            var taskitem = new TaskItem(1, "test", _Datetime.Today, DateTime.Today);

            //Act => j'apelle la méthode à tester
            var result = rules.IsTaskLate(taskitem, _Datetime.Today);
            Assert.False(result);


        }
        [Fact]
        public void IsTaskLate_Futur()
        {

            //Arrange => prépare le contexte du test
            var rules = new TaskRules();

            var taskitem = new TaskItem(1, "test", _Datetime.Today, new DateTime(2027, 1, 1));

            //Act => j'apelle la méthode à tester
            var result = rules.IsTaskLate(taskitem, _Datetime.Today);
            Assert.False(result);


        }

        #endregion
    }
}