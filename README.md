# ManageEmployee
Bienvenue dans le projet ManageEmployee
## Requirements
Lancez l'application via Visual Studio 2022

## Setup
- Pour commencer initialiser la base de donnée en sql server grace au fichier à la racine : bddManageEmployees.sql

- Puis après avoir cloné le projet mettez vous à la racine de celui-ci dans votre explorateur du fichier et allez dans fichier et ouvrez un PowerShell.
  - lancez cette commande : dotnet ef dbcontext scaffold "Server=<VOTRE SERVER NAME>;Database=GestionEmployees;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir Infrastructures/Database/  -c ManageEmployeeDbContext -d -f
  - Ouvrez le projet dans Visual Studio 2022 version 17, ouvrez le fichier appsetings.json et modifiez cette ligne : "EmployeesDatabase": "Server=<VOTRE NOM DE SERVER>;Database=ManageEmployees;Trusted_Connection=True;TrustServerCertificate=True;"
 
Maintenant vous êtes apte à tester le projet enjoy !
