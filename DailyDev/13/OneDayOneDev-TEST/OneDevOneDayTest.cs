using OneDayOneDev_DayThirten;

namespace OneDayOneDev_DayThirten_TEST
{
    public class OneDevOneDayTest
    {
        [Fact]
        public void CreateNewTask_With_Empty_Title()
        {
            var _Datetime = new FakeDateTimeProvider { Today = new DateTime(2026, 02, 02) };
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
            var _Datetime = new SystemDateTimeProvider();
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
            var _Datetime = new SystemDateTimeProvider();
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
            var _Datetime = new SystemDateTimeProvider();
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
            var _Datetime = new SystemDateTimeProvider();
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
            var _Datetime = new SystemDateTimeProvider();
            //Arrange => prépare le contexte du test
            var service = new TaskService(_Datetime);
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            Assert.True(result.succes);
            var task = Assert.Single(service.GetTaskByTitle("test"));
            service.SetTaskCompleted(task.id);
            result = service.DeleteTask(task.id);
            Assert.False(result.succes);

        }
    }
}