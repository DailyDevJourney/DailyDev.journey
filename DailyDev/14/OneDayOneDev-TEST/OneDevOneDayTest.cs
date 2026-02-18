

using OneDayOneDev;
using OneDayOneDev.Command;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Repository;
using OneDayOneDev.Resultdata;
using OneDayOneDev.Service;
using OneDayOneDev.Utils;
using System.Threading.Tasks;

namespace OneDayOneDev_Test
{
    public class OneDevOneDayTest
    {
        static IDateTimeProvider _Datetime = new FakeDateTimeProvider { Today = DateTime.Today };
        static TaskRules _rules = new TaskRules();
        static FileHandler _FileHandler = new FileHandler(_Datetime);
        static Log _LogHandler = new Log(_FileHandler);
        static TaskRepository _Repository = new TaskRepository();

        static TaskService service = new TaskService(_rules, _LogHandler, _Repository, _Datetime);

        #region TASKSERVICE
        [Fact]
        public void CreateNewTask_With_Empty_Title()
        {
            //Arrange => prépare le contexte du test
            
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("",_Datetime.Today.ToString("dd/MM/yyyy"));
            //Assert => Je vérifie le résultat
            Assert.False(result.Success);
        }
        [Fact]
        public void CreateNewTask_With_Incorrect_Date()
        {
            //Arrange => prépare le contexte du test
            
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test","99/99/999");
            //Assert => Je vérifie le résultat
            Assert.False(result.Success);
        }

        [Fact]
        public void CreateNewTask_OK()
        {
            //Arrange => prépare le contexte du test
           
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            //Assert => Je vérifie le résultat
            Assert.True(result.Success);
        }

        [Fact]
        public void SetTaskCompleted_OK()
        {
            //Arrange => prépare le contexte du test
            
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            Assert.True(result.Success);
            var task = result.Data;
            result = service.SetTaskCompleted(identifiant: task.id);
            //Assert => Je vérifie le résultat
            Assert.True(result.Success);

            Assert.True(service.GetTaskById(task.id)!.Iscompleted);

        }


        [Fact]
        public void GetLateList_When_Date_Is_Before_Today()
        {
            //Arrange => prépare le contexte du test
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", "01/01/1990");
            Assert.True(result.Success);
            Assert.NotEmpty(service.GetLateTask() );
            
            
        }
        [Fact]
        public void DeleteTask_That_Is_Completed_Error()
        {
            //Arrange => prépare le contexte du test
            //Act => j'apelle la méthode à tester
            var result = service.CreateNewTask("test", _Datetime.Today.ToString("dd/MM/yyyy"));
            Assert.True(result.Success);
            var task = result.Data;
            result = service.SetTaskCompleted(task.id);
            Assert.True(result.Success);
            result = service.DeleteTask(task.id);
            Assert.True(result.Success);

        }

       

        #endregion

        #region TASKRULES
        [Fact]
        public void IsTaskLate_Past()
        {
          
            //Arrange => prépare le contexte du test
            var rules = new TaskRules();

            var taskitem = new TaskItem("test", DateTime.Today, new DateTime(2020, 1, 1));

            //Act => j'apelle la méthode à tester
            var result = rules.IsTaskLate(taskitem, DateTime.Today);
            Assert.True(result); 
        }

        [Fact]
        public void IsTaskLate_Today()
        {

            //Arrange => prépare le contexte du test
            var rules = new TaskRules();

            var taskitem = new TaskItem("test", _Datetime.Today, DateTime.Today);

            //Act => j'apelle la méthode à tester
            var result = rules.IsTaskLate(taskitem, _Datetime.Today);
            Assert.False(result);


        }
        [Fact]
        public void IsTaskLate_Futur()
        {

            //Arrange => prépare le contexte du test
            var rules = new TaskRules();

            var taskitem = new TaskItem("test", _Datetime.Today, new DateTime(2027, 1, 1));

            //Act => j'apelle la méthode à tester
            var result = rules.IsTaskLate(taskitem, _Datetime.Today);
            Assert.False(result);


        }

        #endregion

        [Fact]
        public void Undo_test()
        {
            //command mannager undo
            CommandManager cmd = new CommandManager();
            
            //Ajout de deux taches
            var task = new TaskItem("test 1", _Datetime.Today, _Datetime.Today);
            //Assert => Je vérifie le résultat
            
            AddTaskCommand add = new AddTaskCommand(service, task);
            
            Assert.True(cmd.Execute(add).Success);

            task = new TaskItem("test 2", _Datetime.Today, _Datetime.Today);

            add = new AddTaskCommand(service, task);

            Assert.True(cmd.Execute(add).Success);

            //Undo
            Assert.True(cmd.CanUndo());
            cmd.Undo();

            //Ajout tache
            task = new TaskItem("test 3", _Datetime.Today, _Datetime.Today);

            add = new AddTaskCommand(service, task);

            Assert.True(cmd.Execute(add).Success);

            //Redo doit dire faux
            Assert.False(cmd.CanRedo());


            
        }
    }
}