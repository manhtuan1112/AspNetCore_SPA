# AspNetCoreDemo

*****Using Repository Partern and Unit Of Work (DRY is applied)*****  <br>
<strong>---The Data Access Layer---</strong> <br>
-There is a generic interface which includes all basic method action to manipulate with data (I named it IRepository) <br>
-IRepository should be implemented by an abstract class name RepositoryBase <br>
-All Repositories will inherit this RepositoryBase Class (Ex: PersonRepository)<br>
-If You have any custom methods that not in generic You can declare them into an interface <br>
(Ex: IPersonRepository should be implemented by PersonRepository and would split up to different interfaces in case many methods need  declaring => SOLID Principle(Interface Segregation Principle) )<br>


<strong>---The Service Layer---</strong> <br>
-Building an interface which includes business logic methods Ex: <b>IPersonService</b> <br>
(<em>Should be split up to different interfaces in case many methods need declaring => SOLID Principle(Interface Segregation Principle) </em> ) <br>
-Declare Service Classes which implements Service Interface (Ex: PersonService : IPersonService) <br>
-Apply Depenency Injection to use Repositories in the Data Layer  <br>
(Be Injected into Construtor and config service Container in StartUp.cs => Solid Priciple (Dependency Inversion Principle) ) <br>
