# vp technical test  

Notre webservice se compose de 2 ressources :

/api/authenticate ( pas besoin d’authentification ). Paramètres du ws « email » et « password ». Retourne « true » ou « false »

/api/confidentials ( authentifié ). Paramètre « email ». Retourne le  « true » ou « false ».
 
Pour le service /api/confidentials, Nous sommes servi de l'exemple fourni par AWS et de sa classe Signer : http://docs.aws.amazon.com/AmazonS3/latest/dev/RESTAuthentication.html



Comment se compose notre solution  :

-VP_Test_WebApp   :  Le projet WebAPi Core 2.1 et son controller AuthController. Nous avons utilisé un handler d'authentification "BasicAuthenticationHandler" afin de pouvoir utiliser les attribut [AllowAnonymous] et [Authorize]  au sein du controller.

-VP_Test_WebApp.Common  : Contient ce qui peut etre commun à l'ensemble des projets. ( ex : les models )

-Vp_Test_WebApp.ConfidentialTest  : Il imlplémente un programm de test pour notre ressource /api/confidentials puisque celle-ci nécéssite la création d'un algo bien spécifique pour l'authentification.

-VP_Test_WebApp.Services.Implementation : Contient le service AuthService permettant de valider les deux ressources.

-VP_Test_WebApp.Services.Interface  : Les interfaces et dans notre exemple IAuthservice.  Cela nous permettra plus tard d'implémenter une projet de test plus facilement. De plus, nous avons utiliser SimpleInjector afin de gérer l'injection de Authservice au sein de notre controller dans VP_Test_WebApp.

-Vp_Test_WebApp.XunitTests  :  Implémente les tests de AuthService via Xunit et pourrait également le faire pour AuthController.  Néanmoins les Packages Xunit et AutoFixture ne sont pas encore Up sur Core 2.0+ donc il faudra revoir son implémentation sinon lancer les codes en ligne de commande.

