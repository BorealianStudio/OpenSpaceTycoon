[![Join the chat at https://gitter.im/OpenSpaceTycoon/Lobby](https://badges.gitter.im/OpenSpaceTycoon/Lobby.svg)](https://gitter.im/OpenSpaceTycoon/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

    1 - Introduction
    2 - Installation de l'environement
    3 - Compilation
	4 - Testing
    5 - Contribution
    6 - Licence

	wiki : https://github.com/BorealianStudio/OpenSpaceTycoon/wiki
	FAQ : https://github.com/BorealianStudio/OpenSpaceTycoon/wiki/FAQ
	
1 - Introduction
	
Open space tycoon est un projet de jeu libre mettant le joueur au contrôle d'une société de transport de marchandise
insterstellaire. Le joueur devra réuissir à maximiser ses profits s'il veut pouvoir être victorieux. Pour se faire
il devra créer des routes commerciales récupérant des ressources dans des stations productrices pour les livrer
aux stations consomatrices.	
	
2 - Installation de l'environement de dev

Pour construire l'application il faut : 
	- Visual studio 2015
	- Unity 5.5+
		
Descendre les sources du projet et ouvrir la solution du modèle :
	/Code/Solution/OSTModele.sln
		
Lancer la compilation. Lors de la compilation, les dlls seront créées et copiées dans le dossier du projet unity.

Ouvrir le projet unity en donner le chemin du dossier : 
	/Client/OSTUnity
		
Dans Unity, ouvrir la scene Start et lancer le build désiré.

4 - Testing

Les test sont gérés en tant que projet dans visual studio. La méthodologie choisie est de faire du TDD ou 
"test driven developement". La particularité d'un tel type de dévelopement est de créer les test avant de faire
les dévelopements. Normalement, quand une tâche est prète à être implementée, les tests doivent être écrit.

Pour lancer les tests, ouvrir la solution visual studio : 
	/Code/Solution/OSTModele.sln
		
Dans le menu, choisir Test/Exécuter/Tous les tests
Cela lance une compilation si nécessaire, lance les tests et affiche l'explorateur de test. Vous pourrez y trouver les résultats.

5 - Contribution

Toutes contributions sont appréciées. afin de contribuer, consultez les "Issues" du github et choisissez en une qui a
un tag "ToImplement" que vous pouvez corriger.
	
Pour les détails sur les façons de contribuer, regardez la page wiki de la contribution :
https://github.com/BorealianStudio/OpenSpaceTycoon/wiki/Contributions

6 - Licence

Toutes les sources du projet Open space tycoon sont sous licence MIT. Pourquoi choisir l'open source? L'objectif du
projet est de créer un projet et qui sait, réunir des gens passionés par un concept. Open source ne veux pas dire
que chacun fait comme il le sent, chaque contribution doit être approuvé par un administrateur du projet.

	
	